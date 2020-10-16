using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    static class TPLOverview
    {
        public static void Start()
        {
            //new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Console.Title = DateTime.Now.ToString();
            //        Thread.Sleep(100);
            //    }
            //})
            //{ IsBackground = true }.Start();

            //new Task(() =>
            //{
            //    while (true)
            //    {
            //        Console.WriteLine(DateTime.Now);
            //        Thread.Sleep(100);
            //    }
            //}).Start();


            //CriticalSectionTests.Start();
            //ThreadTests.Start();
            //ThreadPoolTests.Start();

            //var factorial = new MathTask(() => Factorial(10));
            //var sum = new MathTask(() => Sum(10));

            //factorial.Start();
            //sum.Start();
            //Console.WriteLine("Факториал {0}; сумма {1}", factorial, sum);

            Action<string> printer = str => Console.WriteLine($"Сообщение [th id:[{Thread.CurrentThread.Name},{Thread.CurrentThread.ManagedThreadId}]:{str}");
            printer("Hello World!");
            printer.Invoke("123");

            //printer.BeginInvoke("QWE", null, null);// в коре отключили

            //Parallel.Invoke(
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    () => Console.WriteLine("Еще один метод"));

            //!!!! Можем задать важные опции например для работы в два потока
            //Parallel.Invoke(
            //    new ParallelOptions { MaxDegreeOfParallelism = 2},
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    ParallelInvokeMethod,
            //    () => Console.WriteLine("Еще один метод"));

            //Можем подсчитать количество итераций
            //var result  = Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 2}, (i, state)=>
            //{
            //    printer(i.ToString());
            //    if (i > 10)
            //        state.Break();
            //});
            //Console.WriteLine("Выполнение {0} итераций", result.LowestBreakIteration);

            var messages = Enumerable.Range(1, 150).Select(i => $"Message {i}").ToArray();// позволяет хранить сообщения в памяти, 
            //если убрать ToArray то сообщения выводятся на экран без сохранения
            // напечатать сообщения

            //Parallel.ForEach(messages, message => printer(message));
            //с указанием с каким ко-во потоков ему разрешено работать
            //Parallel.ForEach(messages, new ParallelOptions { MaxDegreeOfParallelism = 2},message => printer(message));
            //вывести сообщения у кот 0 в конце
            //foreach (var message in messages.Where(msg => msg.EndsWith("0")))
            //    printer(message);
            // вывести у кот 0  в конце в массив и оттуда распечатать
            //messages
            //    .Where(msg => msg.EndsWith("0"))
            //    .ToList()
            //    .ForEach(msg => printer(msg));
            // посчитать количество сообщений с 0 на конце
            //****************************************************
            //TPL в упрощенном варианте

            //var cancellation = new CancellationTokenSource();
            //var messages_count = messages                
            //        .AsParallel()
            //        .WithDegreeOfParallelism(2)
            //        .WithCancellation(cancellation.Token)
            //        .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
            //        .Where(msg =>
            //        {
            //            printer(msg);
            //            return msg.EndsWith("0");
            //        })
            //        .AsSequential()//переход с этого момента на последовательное выполнение оставшихся операций(Count). 
            //        .Count();


            //var task = new Task(() => printer("Hello World!"));
            //task.Start();

            ////вызов метода ContinueWith формирует новую задачу
            //var cont_task = task.ContinueWith(t=> Console.WriteLine("Задача {0} завершилась ", t.Id),
            //    TaskContinuationOptions.OnlyOnRanToCompletion);
            //cont_task.ContinueWith(t => { }, TaskContinuationOptions.OnlyOnFaulted);// когда задача провалилась

            var printer_task = Task.Run(() => printer("Hello world"));
            //var printer_task2 = Task.Factory.StartNew(obj => printer((string)obj), "Hello world");

            printer_task.Wait(); //не рекомендуется к использованию из-за мертвой блокировки, которая может возникнуть

            //var result_task = new Task<int>(() =>//не рекомендуется к использованию из-за мертвой блокировки, которая может возникнуть
            //{
            //    Thread.Sleep(100);
            //    return 42;
            //});

            // Дано три задачи в разное время и надо дождаться когда они все завершаться
            var result_task = Task.Run(() =>
            {
                Thread.Sleep(100);
                return 42;
            });

            var result_task2 = Task.Run(() =>
            {
                Thread.Sleep(500);
                return 13;
            });

            var result_task3 = Task.Run(() =>
            {
                Thread.Sleep(10);
                return 13;
            });

            var result = result_task.Result;//костыли! Основной поток зависнет не рекомендуется к использованию из-за мертвой блокировки, которая может возникнуть
            //var result1 = result_task.Result;
            //var resul2 = result_task.Result;

            Task.WaitAll(result_task, result_task2, result_task3);//костыли! Основной поток зависнет не рекомендуется к использованию из-за мертвой блокировки, которая может возникнуть
            var index = Task.WaitAny(result_task, result_task2, result_task3);//костыли! Основной поток зависнет не рекомендуется к использованию из-за мертвой блокировки, которая может возникнуть

        }
    }

}
