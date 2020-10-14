using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    static class CriticalSectionTests
    {
        public static void Start()
        {
            //LockSyncronizationTest();
            //эти объекты как стартовый пистолет для беговых дорожек, когда для страта в один период времени, либо сначала запустить потом приостановить
            ManualResetEvent manual_reset_event = new ManualResetEvent(false);
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            //создаем перемменную базового класса и в нее записываем autoRestEvent
            //EventWaitHandle starter = autoResetEvent;
            EventWaitHandle starter = manual_reset_event;
            for (var i = 0; i < 10; i++)
            {
                var local_i = i;
                new Thread(() =>
                {
                    Console.WriteLine($"Поток {local_i} запущен");
                    starter.WaitOne();
                    Console.WriteLine($"Поток {local_i} завершил свою работу");
                    starter.Reset();// исп при manual_reset_event
                }).Start();
            }
            Console.WriteLine("Все потоки созданы и готовы к работе");
            Console.ReadLine();
                
            starter.Set();
            Console.ReadLine();
            ////с помощью мьютекса можно организовать проверку на второй экземпляр в системе
            //// выполняет синхронизацию между процессами
            //Mutex mutex_1 = new Mutex(true,"Тестовый мьютекс", out var created1);

            //Mutex mutex_2 = new Mutex(true, "Тестовый мьютекс", out var created2);

            //mutex_1.WaitOne();// застрянет дот тех пор пока кто-то не освободит мьютекс
            //mutex_1.ReleaseMutex();// освобождает в первом потоке мьютекс

            //Semaphore semaphore = new Semaphore(0,10);// семафор это мютекс в кот можно войти много раз указ ограничение вторым параметром 

            //semaphore.WaitOne();// для каждого потока 10 потоков вызывают 10 waitone
            //semaphore.Release();
           

        }
        private static void LockSyncronizationTest()
        {
            var threads = new Thread[10];
            for (var i = 0; i < threads.Length; i++)
            {
                var local_i = i;
                threads[i] = new Thread(() => PrintData($"Message from thread {local_i}", 10));
                //так нельзя писать, происходить замыкание из-за лямбда выражения, которое формирует свое сама внутри
            }
            for (var i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }
        }
        private static readonly object __SyncRoot = new object();

        private static void PrintData(string Message, int Count)
        {
            for (var i = 0; i < Count; i++)
            {
                lock (__SyncRoot)
                {
                    Console.Write("Thread id: {0};", Thread.CurrentThread.ManagedThreadId);
                    Console.Write("\t");
                    Console.Write(Message);
                    Console.WriteLine();

                }
                //Синоним по действиям lock, Монитор исп. тогда когда нельзя применять lock в асинхронных потоках 
                //Monitor.Enter(__SyncRoot);
                //try
                //{
                //    Console.Write("Thread id: {0};", Thread.CurrentThread.ManagedThreadId);
                //    Console.Write("\t");
                //    Console.Write(Message);
                //    Console.WriteLine();
                //}
                //finally
                //{
                //    Monitor.Exit(__SyncRoot);
                //}
            }
        }
    }
    //класс который пишет данные в журнал
    //сделать его синхронизированным, через механизм отправки ему сообщений через один поток
   // [Syncronization] остался для .NET Framework!!!
    public class FileLogger:ContextBoundObject
    {
        private readonly string _LogFileName;
        public FileLogger(string LogFileName)
        {
            _LogFileName = LogFileName;

        }
        public void Log(string Message)
        {
            File.WriteAllText(_LogFileName, Message);
        }

    }
}
