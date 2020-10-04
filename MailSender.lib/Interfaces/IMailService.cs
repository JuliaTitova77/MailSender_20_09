

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
    }
}
