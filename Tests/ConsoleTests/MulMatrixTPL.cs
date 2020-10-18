using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    public static class MulMatrixTPL
    {      

        public static void FillMatrix(int[,] arr)
        {
            //int[,] matrix = new int[row, column];
           
            Random random = new Random(DateTime.Now.Millisecond);
            Parallel.For(0, arr.GetLength(0), i =>
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = random.Next(0, 10);
                }
            });
            //return matrix;
        }

        public static void MulMatrix(int[,] a, int[,] b, int[,] c)
        {
            int row = a.GetLength(0);
            int column = b.GetLength(1);

            Parallel.For(0, row, i =>
            {
                for (int z = 0; z < column; z++)
                {
                    int value = 0;
                    for (int j = 0; j < column; j++)
                    {
                        value += a[i, j] * b[j, z];
                    }
                    c[i, z] = value;
                }

            });
        }
            
        public static void Print(int[,]arr,string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i,j] + " ");
                }
                Console.WriteLine("\n");
            }
        }
        
    }
}
