using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace ConsoleTests
{
   
    class Program
    {
        static void Main(string[] args)
        {
            //CriticalSectionTests.Start();
            //ThreadTests.Start();
            //ThreadPoolTests.Start();

            Console.Write($"Введите число для расчетов N: ");
            var N = Int32.Parse(Console.ReadLine());
            //ThreadCalculation.Run(N);
            Thread thread1 = new Thread(ThreadCalculation.MulResult);
            Thread thread2 = new Thread(ThreadCalculation.SumResult);
            thread1.Start(N);
            thread2.Start(N);

            MyThread myThread = new MyThread("Запись в файл");
            myThread.Thrd.Join();
            Console.WriteLine("Главный поток работу закончил");           
               
            Console.ReadLine();
        }


    }
}
