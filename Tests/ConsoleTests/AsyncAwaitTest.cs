using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    static class AsyncAwaitTest
    {
        private static void PrintThreadInfo(string Message="")
        {
            var current_thread = Thread.CurrentThread;
            Console.WriteLine($"Thread id :{current_thread.ManagedThreadId}, Task id :{Task.CurrentId}, {Message} ");
        }
        // асинхронный метод  не должен возвращать void , если написать async то никак его не сделаете асинхронным
        //Метод всегда должен возвращать задачу Task
        //Ключевое слово void позволено использовать для того чтобы можно было писать асинхронные обработчики событий
        //То есть все что написано до слова await выполняется в том потоке в кот его выпускаете
        public static async Task StartAsync()
        {
            Console.WriteLine("Асинхронный метод");
            PrintThreadInfo("При входе в метод  StartAsync");
            //var result_task = GetStringResultAsync();
            //var result = await result_task;
            var result = await GetStringResultReallyAsync();
            Console.WriteLine($"The result was obtained {result}");
            PrintThreadInfo("При печати результата");

            var result2 = await GetStringResultReallyAsync();
            Console.WriteLine($"The result was obtained {result2}");
            PrintThreadInfo("При печати результата");

            for (int i = 0; i < 10; i++)
            {
                var result3 = await GetStringResultReallyAsync();
                Console.WriteLine($"The result was obtained {result3}");
                PrintThreadInfo("При печати результата");
            }
        }

        private static async Task<string> GetStringResultAsync()
        {
            PrintThreadInfo("В псевдоасинхронном методе");
            
            return DateTime.Now.ToString();
            //return Task.FromResult(DateTime.Now.ToString());
        }

        private static Task<string> GetStringResultReallyAsync()
        {
            PrintThreadInfo("В начале асинхронного метода");
            return Task.Run(() =>
            {
                PrintThreadInfo("Внутри асинхронного метода");
                return DateTime.Now.ToString();
            });
            //return DateTime.Now.ToString();
            //return Task.FromResult(DateTime.Now.ToString());
        }

        public static async Task ProcedssDataTestAsync()
        {
            var messages = Enumerable.Range(1, 50).Select(i => $"Message {i}");
            //создаем перечисление задач
            var tasks = messages.Select(msg => Task.Run(() => LowSpeedPrinter(msg)));
            Console.WriteLine(">>>Подготовка к запуску обработки сообщений");
            //для запуска задач
            var runing_tasks = tasks.ToArray();
            Console.WriteLine(">>>Задачи созданы");
            await Task.WhenAll(runing_tasks);
            Console.WriteLine(">>>Обработка всех сообщений завершена");


        }

        private static void LowSpeedPrinter(string msg)
        {
            Thread.Sleep(100);
            Console.WriteLine($">>>Threads id {Thread.CurrentThread.ManagedThreadId} Начинаю обработку сообщение {msg}");
            Console.WriteLine($">>>Threads id {Thread.CurrentThread.ManagedThreadId}, Сообщение {msg} обработано");
        }
    }
}
