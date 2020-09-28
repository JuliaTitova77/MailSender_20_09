using System.Net.Mail;
using System.Windows;
using MailSender.lib;
using MailSender.Models;


namespace MailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // ServerList.ItemsSource = TestData.Servers;
        }

        private void OnSendButton_Click(object sender, RoutedEventArgs e)
        {
            sender = SendersList.SelectedItem as Sender;
            if (sender is null) return;
            var recipient = RecipientsList.SelectedItem as Recipient;
            if (recipient is null) return;
            var message = MessagesList.SelectedItem as Message;
            if (message is null) return;
            var server = ServerList.SelectedItem as Server;
            if (server is null) return;
            var send_service = new MailSenderService
            {
                ServerAddress = server.Address,
                ServerPort = server._Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password
            };
            try
            {
                send_service.SendMessage(sender.Address,recipient.Address,message.Body);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show("Ошибка при отправке почты" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

       
    }
}
