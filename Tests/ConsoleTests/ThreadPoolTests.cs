using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    static class ThreadPoolTests
    {
        public static void Start()
        {
            var message = Enumerable.Range(1, 3)// массив из 100 сообщений
                .Select(i => $"Message {i}")
                .ToArray();
            var start = DateTime.Now;
            var timer = Stopwatch.StartNew();
            ThreadPool.GetAvailableThreads(out var available_worker_threads, out var available_completion_threads);
            ThreadPool.GetMinThreads(out var min_worker_threads, out var min_completion_threads);
            ThreadPool.GetMaxThreads(out var max_worker_threads, out var max_completion_threads);
            //ThreadPool.SetMinThreads(4, 4);
            //ThreadPool.SetMaxThreads(16, 16);

            for (var i = 0; i < message.Length; i++)
            {
                //var local_i = i;
                //не контролируем номер потока
                //new Thread(() => ProcessMessage(message[local_i])) { IsBackground = true }.Start();
                ThreadPool.QueueUserWorkItem(o => ProcessMessage((string)o), message[i]);
            }

            var delta = DateTime.Now - start;
            timer.Stop();
            //Console.Title = "Обработка заняла" + delta.TotalSeconds;
            Console.Title = "Обработка заняла" + timer.Elapsed.TotalSeconds;
        }

        //метод по обработке сообщений
        private static void ProcessMessage(string message)
        {
            Console.WriteLine($"Обработка сообщения {message}");
            Thread.Sleep(100);
            Console.WriteLine($"Обработка сообщения {message} закончена");
        }
    }
}
