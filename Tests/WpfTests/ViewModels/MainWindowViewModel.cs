using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using WpfTests.Infrastructure.Commands;
using WpfTests.ViewModels.Base;


namespace WpfTests.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Тестовое окно";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
            //set
            //{
            //    if (_Title == value) return;
            //    _Title = value;
            //    //OnPropertyChanged("Title");
            //    //OnPropertyChanged(nameof(Title));
            //    OnPropertyChanged();
            //}
        }

        public DateTime CurrentTime => DateTime.Now;        
       
        private bool _TimerEnabled = true;

        public bool TimerEnabled {
            get => _TimerEnabled;
            set
            {
                if (!Set(ref _TimerEnabled, value)) return;
                _Timer.Enabled = value;
            } 
        }
        private readonly Timer _Timer;
        // команда, к кторой можем привязать кнопку и которая отображает диалог
        private ICommand _ShowDialogCommand;
        public ICommand ShowDialogCommand => _ShowDialogCommand
            ??= new LambdaCommand(OnShowDialogCommandExecuted);
        private void OnShowDialogCommandExecuted(object p)
        {
            MessageBox.Show("Hello World!");
        }
        public MainWindowViewModel()
        {
            _Timer = new Timer(100);
            _Timer.Elapsed += OnTimerElapsed;
            _Timer.AutoReset = true;
            _Timer.Enabled = true;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }
    }
}
