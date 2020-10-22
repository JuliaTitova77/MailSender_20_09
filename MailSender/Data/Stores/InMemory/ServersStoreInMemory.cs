using MailSender.lib.Interfaces;
using MailSender.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace MailSender.Data.Stores.InMemory
{
    class ServersStoreInMemory : IStore<Server>
    {
        public Server Add(Server Item)
        {
            if (TestData.Servers.Contains(Item)) return Item;
            Item.Id = TestData.Servers.DefaultIfEmpty().Max(r => r.Id) + 1;
            TestData.Servers.Add(Item);
            return Item;
        }

        public void Delete(int Id)
        {
            var item = GetById(Id);
            if (item is null) return;
            TestData.Servers.Remove(item);
        }


        public IEnumerable<Server> GetAll() => TestData.Servers;


        public Server GetById(int Id) => GetAll().FirstOrDefault(r => r.Id == Id);


        public void Update(Server item) { }

    }
}
