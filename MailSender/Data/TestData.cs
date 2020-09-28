using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailSender.Data
{
    static class TestData
    {
        public static List<Sender> Senders { get; } = Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Name = $"Отправитель {i}",
                Address = $"sender {i}@server.ru"
            })
            .ToList();

        public static List<Recipient> Recipients { get; } = Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Name = $"Получатель {i}",
                Address = $"recipient {i}@server.ru"
            })
            .ToList();

        public static List<Server> Servers { get; } = Enumerable.Range(1, 10)
            .Select(i => new Server
            {
                Address = $"smtp.server{i}.com",
                Login = $"Login-{i}",
                Password = $"Password -{i}",
                UseSSL = i % 2 == 0
                
            })
            .ToList();
        public static List<Message> Messages { get; } = Enumerable.Range(1, 20)
            .Select(i => new Message
            {
                Subject = $"Сообшение {i}",
                Body = $"Текст сообщения {i}"
            })
            .ToList();
    }
}
