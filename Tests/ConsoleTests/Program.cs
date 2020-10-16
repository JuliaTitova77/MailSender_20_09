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
            int[,] first = new int[10, 10];
            int[,] second = new int[10, 10];
            int[,] result = new int[10, 10];
            //TPLOverview.Start();
            var task = Task.Run(() => MulMatrixTPL.FillMatrix(first));
            var cont_task = task.ContinueWith(t => MulMatrixTPL.FillMatrix(second));       
            cont_task.ContinueWith(t => MulMatrixTPL.MulMatrix(first, second, result));
            cont_task.ContinueWith(t => MulMatrixTPL.Print(result, "Перемножение двух матриц"));

            Console.WriteLine("Главный поток работу закончил");
            Console.ReadLine();
        }
    }
}

       

   

