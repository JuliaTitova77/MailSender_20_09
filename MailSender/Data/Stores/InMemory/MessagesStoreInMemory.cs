using MailSender.lib.Interfaces;
using MailSender.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MailSender.Data.Stores.InMemory
{
    class MessagesStoreInMemory : IStore<Message>
    {
        public Message Add(Message Item)
        {
            if (TestData.Messages.Contains(Item)) return Item;
            Item.Id = TestData.Messages.DefaultIfEmpty().Max(r => r.Id) + 1;
            TestData.Messages.Add(Item);
            return Item;
        }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            TestData.Messages.Remove(item);
        }


        public IEnumerable<Message> GetAll() => TestData.Messages;


        public Message GetById(int Id) => GetAll().FirstOrDefault(r => r.Id == Id);


        public void Update(Message item) { }

    }
}
