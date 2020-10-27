using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Task_7.Data.Entities;

namespace Task_7
{
    public class EmployeesFromFile
    {
        public static List<Employee> GetEmployees { get; set; }
        public class MyThread
        {
            public Thread Thrd;
            public MyThread()
            {
                Thrd = new Thread(this.Run);
                Thrd.Start();

            }
            void Run()
            {
                Encoding win1251 = Encoding.GetEncoding("windows-1251");
                using (TextFieldParser parser = new TextFieldParser(@"FIO-DB.csv", win1251))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    var group = new Group
                    {
                        Employees = new List<Employee>()
                    };
                    GetEmployees = new List<Employee>();
                    while (!parser.EndOfData)
                    {
                        //Process row
                        string[] fields = parser.ReadFields();


                        //string[] lines = parser.ReadLine().Split(',');
                        string line = parser.ReadLine();
                        var employee = new Employee
                        {
                            Name = fields[0],
                            Email = fields[1],
                            PhoneNumber = fields[2]
                        };
                       
                        group.Employees.Add(employee);
                        GetEmployees.Add(employee);
                            
                        //GetEmployees = fields
                        //    .Distinct()
                        //.Select(j => new Employee
                        //{
                        //    Name = fields[0],
                        //    Email = fields[1],
                        //    PhoneNumber = fields[2]
                        //})
                        //.ToList();
                        
                        
                    }
                }
            }

        }
    }
}
