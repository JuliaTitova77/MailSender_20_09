using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MailSender.Data.Stores.InMemory
{
    class SendersStoreInMemory : IStore<Sender>
    {
        public Sender Add(Sender Item)
        {
            if (TestData.Senders.Contains(Item)) return Item;
            Item.Id = TestData.Senders.DefaultIfEmpty().Max(r => r.Id) + 1;
            TestData.Senders.Add(Item);
            return Item;
        }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            TestData.Senders.Remove(item);
        }


        public IEnumerable<Sender> GetAll() => TestData.Senders;


        public Sender GetById(int Id) => GetAll().FirstOrDefault(r => r.Id == Id);


        public void Update(Sender item) { }

    }
}
