using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TPLOverview.Start();

            Console.WriteLine("Главный поток работу закончил");
            Console.ReadLine();
        }
    }
}

       

   

