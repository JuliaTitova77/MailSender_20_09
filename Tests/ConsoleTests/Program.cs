using MailSender.lib.Interfaces;
using MailSender.lib.Service;
using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {

            IEncryptorService cryptor = new Rfc_2898Encryptor();
            var str = "Hello world!";
            const string password = "MailSender!";
            var crypted_str = cryptor.Encrypt(str, password);
            var decryptor_str = cryptor.Decrypt(crypted_str, password);
        }
    }
}
