using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

        Task SendAsync(string SendAddress, string RecipientAddress, string Subject, string Body,CancellationToken Cancel = default);
        Task SendAsync(string SendAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body,
            System.IProgress<(string Recipient, double Percent)> Progress = null,CancellationToken Cancel = default);
        Task SendParallelAsync(string SendAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body,
            System.IProgress<(string Recipient, double Percent)> Progress = null, CancellationToken Cancel = default);
    }
}
