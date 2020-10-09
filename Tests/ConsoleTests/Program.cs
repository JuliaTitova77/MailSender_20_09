using System;
using System.Diagnostics;
using System.Threading;


namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            CriticalSectionTests.Start();
            //ThreadTests.Start();
            Console.WriteLine("Главный поток работу закончил");
            Console.ReadLine();
        }


    }
}
