using MailSender.lib.Interfaces;
using MailSender.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailSender.Data.Stores.InDB
{
    class RecipientsStoreInDB : IStore<Recipient>
    {
        private readonly MailSenderDB _db;
        public RecipientsStoreInDB(MailSenderDB db)
        {
            _db = db;
        }
        public Recipient Add(Recipient item)
        {
            _db.Entry(item).State = EntityState.Added;
            //_db.Recipients.Add(item);
            _db.SaveChanges();
            return item;
        }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            //_db.Recipients.Remove(item);
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            
        }

        public IEnumerable<Recipient> GetAll() => _db.Recipients.ToArray();


        public Recipient GetById(int Id) => _db.Recipients.FirstOrDefault(t => t.Id == Id);
        //public Recipient GetById(int Id) => _db.Recipients.Find(Id);
        //public Recipient GetById(int Id) => _db.Recipients.SingleOrDefault(t => t.Id == Id);
        public void Update(Recipient item)
        {
            _db.Entry(item).State = EntityState.Modified;
            //_db.Recipients.Update(item);
            _db.SaveChanges();
           
        }
    }
}
