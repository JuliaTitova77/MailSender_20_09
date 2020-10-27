using System;
using System.Collections.Generic;
using System.Text;
using Task_7.Data.Entities;

namespace Task_7
{
    public interface IStore<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        T Add(T item);
        void Update(T item);
        void Delete(int Id);
    }
    
}
