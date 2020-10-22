using MailSender.lib.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.lib.Interfaces
{
    public interface IStore<T> where T: Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        T Add(T item);
        void Update(T item);
        void Delete(int Id);
    }
}
