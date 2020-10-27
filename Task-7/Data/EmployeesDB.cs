using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Task_7.Data.Entities;

namespace Task_7.Data
{
    public class EmployeesDB : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Group> Groups { get; set; }
        public EmployeesDB(DbContextOptions<EmployeesDB> options) : base(options) { }// подключение к БД
    }
}
