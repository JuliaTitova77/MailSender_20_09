using System.Collections.Generic;

namespace MailSender.lib.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string Server, int Port, bool SSL, string Login, string Password);
    }

    //умеет отправлять почту методом Send
    public interface IMailSender
    {
       void Send(string SendAddress, string RecipientAddress, string Subject, string Body);
        // метод, который делает массовую рассылку по списку адресов получателей
       void Send(string SendAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body);
       void SendParallel(string SendAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body);
    }
}
