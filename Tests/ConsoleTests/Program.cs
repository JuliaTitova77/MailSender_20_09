using System;
using System.Diagnostics;
using System.Threading;


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
            Console.WriteLine("Главный поток работу закончил");
            Console.ReadLine();
        }


    }
}
