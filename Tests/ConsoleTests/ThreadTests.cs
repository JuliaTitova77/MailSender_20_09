using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    static class ThreadTests
    {
        public static void Start()
        {
            var main_thread = Thread.CurrentThread;
            var main_thread_id = main_thread.ManagedThreadId;
            main_thread.Name = "Главный поток";

            //TimerMethod();
            var timer_thread = new Thread(TimerMethod);
            timer_thread.Name = "Поток часов";
            timer_thread.IsBackground = true;
            timer_thread.Start();

            //timer_thread.Interrupt();
            //var printer_thread = new Thread(PrintMessage)
            //{
            //    IsBackground = true,
            //    Name = "Parameter printer"
            //};

            //printer_thread.Start("Hello world");
            var message = "Hello world_1";
            var count = 10;
            //new Thread(() => PrintMessage(message, count)) { IsBackground = true }.Start();
            var print_task = new PrintMessageTask(message, count);
            print_task.Start();
            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
            Console.WriteLine("Останавливаю время....");
            var current_process = System.Diagnostics.Process.GetCurrentProcess();
            //Process.Start("calc.exe");
            timer_thread.Priority = ThreadPriority.BelowNormal;

            __TimerWork = false;
            if (!timer_thread.Join(100))
                timer_thread.Interrupt();
            //if (timer_thread.IsAlive)
            //    timer_thread.Abort();
            // timer_thread.Abort() не работает в .NET CORE


            //timer_thread.Interrupt();
            //timer_thread.Abort();

        }
        private static void PrintMessage(string Message, int Count)
        {
            Print();
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            for (var i = 0; i < Count; i++)
            {
                Console.WriteLine("id:{0}\t{1}", thread_id, Message);
                Thread.Sleep(10);
            }

        }

        private static volatile bool __TimerWork = true;
        private static void TimerMethod()
        {
            Print();
            while (__TimerWork)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss:ffff");
                Thread.Sleep(100);
                //Thread.SpinWait(10); очень быстрый цикл без ухода в спячку, в внутренних циклах
            }
        }
        private static void Print()
        {
            var thread = Thread.CurrentThread;

            Console.WriteLine("id:{0}; name:{1}; priority:{2}",
                thread.ManagedThreadId, thread.Name, thread.Priority);
        }
    }
}
