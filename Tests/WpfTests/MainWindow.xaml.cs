using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;

namespace WpfTests
{   
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static class DataMessage
        {
            public static MailAddress to = new MailAddress("julia.titova@gmail.com", "Юлия");
            public static MailAddress from = new MailAddress("julia.titova@outlook.com", "Юлия");

            public static MailMessage message = new MailMessage(from, to);

        }

        private void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DataMessage.message.Subject = TitleMail.Text + " от " + DataMessage.from.DisplayName + " " + DateTime.Now;
                DataMessage.message.Body = TextMail.Text + " " + DateTime.Now;
                var client = new SmtpClient("smtp.outlook.com", 587);

                client.Credentials = new NetworkCredential
                {
                    UserName = LoginEdit.Text,
                    Password = PasswordEdit.Password

                };
                client.EnableSsl = true;
                client.Send(DataMessage.message);
            }
            catch
            {
                MessageBox.Show("Ошибка отправления!", "Сообщение", MessageBoxButton.OK);
            }
           
                
            MessageBox.Show("Работа завершена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

            Console.ReadLine();
                
           
                
            

           
        }
    }
}
