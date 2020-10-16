using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleTests
{
  
    class WriteToFile    {

        public StreamWriter sw = new StreamWriter("threadingCSV.txt");
        public void Write(string s)
        {
            lock (sw)
            {
                sw.WriteLine(s.ToString());
            }
        }
    }
    class MyThread
    {
        public Thread Thrd;
        public static WriteToFile wf = new WriteToFile();
        // Сконструировать новый поток,
        public MyThread(string name)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            Thrd.Start(); // начать поток
        }
        // Начать выполнение нового потока.
        void Run()
        {
            using (TextFieldParser parser = new TextFieldParser(@"crimes.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    for (int i = 1; i < parser.LineNumber; i++)
                    {

                        string[] lines = parser.ReadLine().Split(',');
                        string line = parser.ReadLine();

                        var year = ConvertToYear(lines[2]);

                        if (year == 2015)
                        {
                            wf.Write(line);
                        }

                    }

                }
            }
        }

        public int ConvertToYear(string a)
        {
            string[] date = a.Split('/');
            string[] date_year = date[2].Split();
            var year = Convert.ToInt32(date_year[0]);
            return year;
        }
    }
}
