using MailSender.lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.lib.Service
{
    public class SmtpMailService : IMailService
    {
        public SmtpMailService()
        {

        }
        public IMailSender GetSender(string Server, int Port, bool SSL, string Login, string Password)
        {
            return new SmtpMailSender(Server, Port, SSL, Login, Password);
        }
    }

    internal class SmtpMailSender : IMailSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _Ssl;
        private readonly string _Login;
        private readonly string _Password;

        public SmtpMailSender(string Address,int Port, bool SSL, string Login, string Password)
        {
            _Address = Address;
            _Port = Port;
            _Ssl = SSL;
            _Login = Login ;
            _Password = Password;
        }

        public void Send(string SendAddress, string RecipientAddress, string Subject, string Body)
        {
            var from = new MailAddress(SendAddress);
            var to = new MailAddress(RecipientAddress);

            using (var message = new MailMessage(from, to))
            {

                message.Subject = Subject;
                message.Body = Body;

                using (var client = new SmtpClient(_Address, _Port))
                {
                    client.EnableSsl = _Ssl;

                    client.Credentials = new NetworkCredential
                    {
                        UserName = _Login,
                        Password = _Password
                    };

                    try
                    {
                        client.Send(message);
                    }
                    catch (SmtpException e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
            }
        }

        public void Send(string SendAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
        {
            foreach (var recipient_address in RecipientsAddresses)
            {
                Send(SendAddress, recipient_address, Subject, Body);

            }
        }

        public async Task SendAsync(string SendAddress, string RecipientAddress, string Subject, string Body,
            CancellationToken Cancel = default)
        {
            var from = new MailAddress(SendAddress);
            var to = new MailAddress(RecipientAddress);

            using (var message = new MailMessage(from, to))
            {

                message.Subject = Subject;
                message.Body = Body;

                using (var client = new SmtpClient(_Address, _Port))
                {
                    client.EnableSsl = _Ssl;

                    client.Credentials = new NetworkCredential
                    {
                        UserName = _Login,
                        Password = _Password
                    };

                    try
                    {
                        //client.Send(message); добавляем асинхронности
                        Cancel.ThrowIfCancellationRequested();
                        await client.SendMailAsync(message).ConfigureAwait(false);
                             
                    }
                    catch (SmtpException e)
                    {
                        Trace.TraceError(e.ToString());
                        throw;
                    }
                }
            }
        }

        //последовательная асинхронная рассылка не параллельная
        public async Task SendAsync(string SendAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body,
            IProgress<(string Recipient, double Percent)> Progress = null, CancellationToken Cancel = default)
        {

            var recipients = RecipientsAddresses.ToArray();
            var count = recipients.Length;

            for (int i = 0; i < count; i++)
            {
                Cancel.ThrowIfCancellationRequested();
                await SendAsync(SendAddress, recipients[i], Subject, Body, Cancel).ConfigureAwait(false);
                Progress?.Report((recipients[i],i / (double)count));
            }
            
        }

        public void SendParallel(string SendAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body)
        {
            foreach (var recipient_address in RecipientsAddresses)
            {
                ThreadPool.QueueUserWorkItem(o => Send(SendAddress, recipient_address, Subject, Body));               

            }
        }

        public async Task SendParallelAsync(string SendAddress, IEnumerable<string> RecipientsAddresses, string Subject, string Body,
             CancellationToken Cancel = default)
        {
            //формируем все задачи по отправке почты мы их наметили что они так будут выглядеть
            IEnumerable<Task> tasks = RecipientsAddresses
                .Select(recipient_address => SendAsync(SendAddress, recipient_address, Subject, Body, Cancel));

            //запускаем их и ожидаем их завершение. А выполнится метод tasks именно здесь. Метод WhenAll перечислит перечисление, оно создаст
            //все эти задачи и оно запустится
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
        // так можно передать библиотеку Dll в проект кроме GAC 
        //[DllImport("file_name.dll")]
        //private static extern void MethodName(string str);
    }
}
