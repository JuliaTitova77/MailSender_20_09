using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfTests.Infrastructure.Commands;

namespace WpfTests
{   
    
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            //Этот вызов позволяет разорвать процесс выполнения программы
            //Мы находились в потоке ThreadId = 1
            await Task.Yield();//Даем время перерисоваться интерфейсу сделать все что он хочет
            //Мы снова возвращаемся в первый поток ThreadId = 1, но это касается только потока ThreadId = 1
            var dialog = new OpenFileDialog
            {
                Title = "Выбор файла для чтения",
                Filter = "Текстовые файлы(*.txt)|*.txt|Все файлы (*.*)|*.*",
                RestoreDirectory = true
            };

            //using (StreamReader reader = new StreamReader(dialog.FileName))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = await reader.ReadLineAsync().ConfigureAwait(false);
            //        var words = line.Split(' ');

            //        //Thread.Sleep(100);// Замедляет настолько основной поток, что виснет приложение. Так делать нельзя Нужно разсинхронизировать 
            //        //await Task.Delay(1);

            //        //Посчитать число слов в файле, но работа вся идет в основном потоке хоть и асинхронно
            //        foreach (var word in words)
            //            if (dict.ContainsKey(word))
            //                dict[word]++;
            //            else
            //                dict.Add(word, 1);
            //        // PrograssInfo.Value = reader.BaseStream.Position / (double)reader.BaseStream.Length;
            //        PrograssInfo.Dispatcher.Invoke(() =>
            //            PrograssInfo.Value = reader.BaseStream.Position / (double)reader.BaseStream.Length);
            //    }
            //}
            ////Result.Text = $"Число слов {dict.Count}";
            ///
            //var count = dict.Count;
            //Result.Dispatcher.Invoke(() => Result.Text = $"Число слов {dict.Count}");

            if (dialog.ShowDialog() != true) return;
            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            StartAction.IsEnabled = false;
            CancelAction.IsEnabled = true;

            _ReadingFileCancellation = new CancellationTokenSource();
            var cancel = _ReadingFileCancellation.Token;
            IProgress<double> progress = new Progress<double>(p => ProgressInfo.Value = p);// в прогрессе интерфейс реализован не явно поэтому var здесь не подойдет

            //Обернем вывод и отмену в try catch чтобы при отмене операции не славливать исключение
            try
            {               
                var count = await GetWordsCountAsync(dialog.FileName, progress, cancel);
                Result.Text = $"Число слов {count}";
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Операция чтения файла была отменена");
                Result.Text = "---";
                progress.Report(0);
            }

            CancelAction.IsEnabled = false;
            StartAction.IsEnabled = true;
            
        }

        private static async Task<int> GetWordsCountAsync( string FileName, IProgress<double> Progress = null,CancellationToken Cancel = default)
        {
            //Мы находимя в потоке ThreadId = 7
            //await Task.Yield();// у нас забрали поток 7 в пул потоков
            //Теперь мы например в ThreadId = 12(заранее не знаем), а возможно и обратно в поток 7
            //чтоб дальше было в пуле потоков нужно добавить ConfigureAwait но его нет в методе Yield, доб в папке Extensions класс для этого
            var thread_id = Thread.CurrentThread.ManagedThreadId;

            await Task.Yield().ConfigureAwait(false);

            var thread_id2 = Thread.CurrentThread.ManagedThreadId;

            var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            Cancel.ThrowIfCancellationRequested(); //проверка отменена ли операция Она тормозит выполнение операции но нужна
            using (StreamReader reader = new StreamReader(FileName))
            {
                while (!reader.EndOfStream)
                {
                    Cancel.ThrowIfCancellationRequested();//проверка отменена ли операция
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);//добавляя метод ConfigureAwait(false) то мы командуем перенестись в другой поток 
                    //ConfigureAwait(false)-- требование к "вернуться" в произвольный поток из пула потоков
                    //при написании библиотек обязательно использовать ConfigureAwait(false) это должно быть везде
                    var words = line.Split(' ');

                    //Thread.Sleep(100);// Замедляет настолько основной поток, что виснет приложение. Так делать нельзя Нужно разсинхронизировать 
                    //await Task.Delay(1,Cancel).ConfigureAwait(false);

                    //Посчитать число слов в файле, но работа вся идет в основном потоке хоть и асинхронно
                    foreach (var word in words)
                        if (dict.ContainsKey(word))
                            dict[word]++;
                        else
                            dict.Add(word, 1);
                    // PrograssInfo.Value = reader.BaseStream.Position / (double)reader.BaseStream.Length;
                    //PrograssInfo.Dispatcher.Invoke(() =>
                    //  PrograssInfo.Value = reader.BaseStream.Position / (double)reader.BaseStream.Length);
                    Progress?.Report(reader.BaseStream.Position / (double)reader.BaseStream.Length);
                }
            }
            //чтоб вернуться в пул потоков и чтоб код выполняющийся ниже выполнялся в первом потоке
            //тогда нам нужен метод расширения для диспетчера

            var thread_id3 = Thread.CurrentThread.ManagedThreadId;

            await  App.Current.Dispatcher;//.GetAwaiter();

            var thread_id4 = Thread.CurrentThread.ManagedThreadId;
            //Result.Text = $"Число слов {dict.Count}";
            return dict.Count;

        }

        private CancellationTokenSource _ReadingFileCancellation;

        private void OnCancelReadingClick(object sender, RoutedEventArgs e)
        {
            _ReadingFileCancellation?.Cancel();
        }
    }
}
