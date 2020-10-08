using MailSender.lib.Interfaces;
using MailSender.lib.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MailSender.lib.Tests.Service
{   
    
    [TestClass]
    public class Rfc2898EncryptorTests
    {
        private IEncryptorService _Encryptor = new Rfc2898Encryptor();

        static Rfc2898EncryptorTests() { }
        public Rfc2898EncryptorTests()
        {

        }
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {

        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {

        }
        [TestInitialize]
        public void TestInitialize()
        {

        }
        //Выполняется модульный тест
        [TestCleanup]
        public void TestCleanUp()
        {

        }

        [ClassCleanup]
        public static void ClassCleanUp(TestContext context)
        {

        }
        [AssemblyCleanup]
        public static void AssemblyCleanUp(TestContext context)
        {

        }

        [TestMethod]
        public void Encrypt_Hello_World_and_Decrypt_with_Password()
        {
            const string str = "Hello world";
            const string password = "Password";

            var encrypted_str = _Encryptor.Encrypt(str, password);
            var decrypted_str = _Encryptor.Decrypt(encrypted_str, password);
            //var wrong_pass_decrypted = _Encryptor.Decrypt(encrypted_str, "QWE");

            Assert.AreNotEqual(str, encrypted_str);
            Assert.AreEqual(str, decrypted_str);
            //Assert.AreNotEqual(str, wrong_pass_decrypted);
        }

        [TestMethod,ExpectedException(typeof(CryptographicException))]
        public void Wrong_Decryption_thrown_CryptografyExeption() 
        {
            const string str = "Hello world";
            const string password = "Password";

            var encrypted_str = _Encryptor.Encrypt(str, password);
            
            var wrong_pass_decrypted = _Encryptor.Decrypt(encrypted_str, "QWE");
           
        }
    }
    


}
