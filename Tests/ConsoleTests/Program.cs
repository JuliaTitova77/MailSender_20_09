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
            //TPLOverview.Start();
            var task = AsyncAwaitTest.StartAsync();//таск формируется самостоятельно компилятором (а так сами бы ручкаим делали 
            //с помощью ключевого слова new
            var process_manages = AsyncAwaitTest.ProcedssDataTestAsync();
            Console.WriteLine("Тестовая задача запущена");

            // task.Wait();
            Task.WaitAll(task, process_manages);
            Console.WriteLine("Главный поток работу закончил");
            Console.ReadLine();
        }
    }
}

       

   

