using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Task_7.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using Task_7.Data.Entities;



namespace Task_7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string connection_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Employees.DB;Integrated Security=True";
            //var service_collection = new ServiceCollection();
            //service_collection.AddDbContext<EmployeesDB>(o => o.UseSqlServer(connection_str));
            //var service = service_collection.BuildServiceProvider();

            using (var db = new EmployeesDB(new DbContextOptionsBuilder<EmployeesDB>().UseSqlServer(connection_str).Options))
            {
                await db.Database.EnsureCreatedAsync();                
                
                var k = 0;
                if (await db.Employees.CountAsync() == 0)
                {
                    EmployeesFromFile.MyThread thread = new EmployeesFromFile.MyThread();
                    thread.Thrd.Join();
                    for(var i = 0; i< EmployeesFromFile.GetEmployees.Count; i++)
                    {
                        await db.Employees.AddAsync(EmployeesFromFile.GetEmployees[i]);
                    }
                    await db.SaveChangesAsync();
                }
            }
            Console.WriteLine("Основной поток закончил работу");
            Console.ReadLine();
        }     
               
                
    }
}
