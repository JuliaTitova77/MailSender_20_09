using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.lib.Interfaces
{
    public interface IncryptorService
    {
        string Encrypt(string str, string Password);
        string Deccrypt(string str, string Password);
        byte[] Encrypt(byte[] data, string Password);
        byte[] Decrypt(byte[] data, string Password);
    }
}
