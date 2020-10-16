using System;
using System.Threading;

namespace ConsoleTests
{
    class MathTask
    {

        private readonly Thread _CalculationThread;
        private int _Result;
        private bool _IsCompleted;

        public bool IsCompleted => _IsCompleted;
        public int Result
        {
            get
            {
                if (!_IsCompleted)// если результат не получен вычисления, то синхронизируем с потоком
                    _CalculationThread.Join();
                return _Result;
            }
        }

        public MathTask(Func<int> Calculation)
        {
            _CalculationThread = new Thread(
                () =>
                {
                    _Result = Calculation();
                    _IsCompleted = true;
                })
            { IsBackground = true };
        }
        public void Start() => _CalculationThread.Start();

        private static void ParallelInvokeMethod()
        {
            Console.WriteLine($"ThrId:{(int)Thread.CurrentThread.ManagedThreadId} - finished");
            Thread.Sleep(250);
            Console.WriteLine($"ThrId:{Thread.CurrentThread.ManagedThreadId} - finished");
        }

        private static void ParallelInvokeMethod(string message)
        {
            Console.WriteLine($"ThrId:{Thread.CurrentThread.ManagedThreadId} - started {message}");
            Thread.Sleep(250);
            Console.WriteLine($"ThrId:{Thread.CurrentThread.ManagedThreadId} - started {message}");
        }

        private static int Sum(int v)
        {
            var sum = 0;
            for (int i = 0; i < v; i++)
            {
                sum += v;
            }
            return sum;
        }

        private static int Factorial(int v)
        {
            var factorial = 1;
            for (int i = 0; i < v; i++)
            {
                factorial *= v;
            }
            return factorial;
        }
    }

}
