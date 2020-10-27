using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_7.Data;
using Task_7.Data.Entities;

namespace Task_7
{
    class EmployeesStoreInDB : IStore<Employee>
    {
        private readonly EmployeesDB _empl;
        public EmployeesStoreInDB(EmployeesDB empl)
        {
            _empl = empl;   
        }
        public Employee Add(Employee item)
        {
            if (EmployeesFromFile.GetEmployees.Contains(item)) return item;
            item.Id = EmployeesFromFile.GetEmployees.DefaultIfEmpty().Max(r => r.Id) + 1;
            EmployeesFromFile.GetEmployees.Add(item);            
            return item;
        }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            //_db.Recipients.Remove(item);
            _empl.Entry(item).State = EntityState.Deleted;
            _empl.SaveChanges();
        }

        public IEnumerable<Employee> GetAll() => _empl.Employees.ToArray();


        public Employee GetById(int Id) => _empl.Employees.FirstOrDefault(t => t.Id == Id);
        

        public void Update(Employee item)
        {
            _empl.Entry(item).State = EntityState.Modified;
            _empl.SaveChanges();
        }
    }
}
