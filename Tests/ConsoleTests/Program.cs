using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTests.Data;
using ConsoleTests.Data.Entityes;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleTests
{
   
    class Program
    {
        static async Task Main(string[] args)
        {
            const string connection_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.DB;Integrated Security=True";

            //var servce_collection = new ServiceCollection();
            //servce_collection.AddDbContext<StudentsDB>(opt => opt.UseSqlServer(connection_str));

            //var services = servce_collection.BuildServiceProvider();

            //new DbContextOptionsBuilder().sq
            //new StudentsDB()

            //using (var db = services.GetRequiredService<StudentsDB>())
            //{

            //}       

            //db очень коротко живущая сущность и на переод общения с БД чем больше живет тем больше кеширует и тормознее становится
            //при большом ко-ве записей нужно разбить их по 200 или 500 записей для заноса в БД
            //контекст создается на один запрос
            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connection_str).Options))
            {
                var k = 0;
                if (await db.Students.CountAsync() == 0)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        var group = new Group
                        {
                            Name = $"Группа {i}",
                            Description = $"Описание группы {i}",
                            Students = new List<Student>()
                        };

                        for (var j = 0; j < 10; j++)
                        {
                            var student = new Student
                            {
                                Name = $"Студент {k}",
                                Surname = $"Surname {k}",
                                Patronymic = $"Patronymic {k}"
                               
                            };
                            k++;
                            group.Students.Add(student);
                        }

                        await db.Groups.AddAsync(group);
                    }

                    await db.SaveChangesAsync();
                }
            }
            // вывести всех студентов в группе 5
            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connection_str).Options))
            {
                var students = await db.Students
                  .Include(s => s.Group) // JOIN формирует Join к таблице 
                  .Where(s => s.Group.Name == "Группа 5")
                   .ToArrayAsync();// отправляет в БД получаем массив студентов

                foreach (var student in students)
                    Console.WriteLine("[{0}] {1} - {2}", student.Id, student.Name, student.Group.Name);
            }

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }
    }
}