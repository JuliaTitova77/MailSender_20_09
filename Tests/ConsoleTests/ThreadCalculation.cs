using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
    class ThreadCalculation
    {
        public static int ResultMul = 1;
        public static int ResultSum = 0;      

       

        public static void MulResult(object count)
        {
            var data = (int)count;

            while (data > 0)
            {
                ResultMul *= data;
                data--;
            }
            Thread.Sleep(100);
            Console.WriteLine($"Вычисление факториала числа {(int)count}");
            Console.WriteLine($"Факториал числа {(int)count} = {ResultMul}");
            Console.WriteLine($"Поток #1 работу окончил");
        }
            
           
        public static void SumResult(object count)
        {
            var data = (int)count;

            while (data > 0)
            {
                ResultSum += data;
                data--;
            }

            Thread.Sleep(100);
            Console.WriteLine($"Вычисление суммы целых чисел {(int)count}");
            Console.WriteLine($"Сумма целых чисел {(int)count} = {ResultSum}");
            Console.WriteLine($"Поток #2 работу окончил");

        }
    }
}
