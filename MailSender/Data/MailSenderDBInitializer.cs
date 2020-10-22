using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailSender.Data
{
    class MailSenderDBInitializer
    {
        private readonly MailSenderDB _db;
        public MailSenderDBInitializer(MailSenderDB db)
        {
            _db = db;
        }
        public void Initialize()
        {
            //здесь выполняем всю необходимую логику, чтоб инициализировать БД
            _db.Database.Migrate();//пытаемся мигрировать БД
            InitializeRecipients();
            InitializeMessages();
            InitializeSenders();
            InitializeServers();
        }

        private void InitializeRecipients()
        {
            if (_db.Recipients.Any()) return;
            _db.Recipients.AddRange(TestData.Recipients);
            _db.SaveChanges();//добавление реципиентов
        }
        private void InitializeSenders()
        {
            if (_db.Senders.Any()) return;
            _db.Senders.AddRange(TestData.Senders);
            _db.SaveChanges();//добавление отправителей
        }

        private void InitializeMessages()
        {
            if (_db.Messages.Any()) return;
            _db.Messages.AddRange(TestData.Messages);
            _db.SaveChanges();//добавление сообщений
        }
        private void InitializeServers()
        {
            if (_db.Servers.Any()) return;
            _db.Servers.AddRange(TestData.Servers);
            _db.SaveChanges();//добавление серверов
        }
    }
}
