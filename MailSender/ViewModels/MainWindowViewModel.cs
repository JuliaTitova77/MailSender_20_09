using MailSender.ViewModels.Base;
using MailSender.Models;
using System.Collections.ObjectModel;
using MailSender.Data;
using System.Windows.Input;
using MailSender.Infrastructure.Commands;
using System.Windows;
using System.Linq;
using MailSender.lib.Interfaces;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private  readonly IMailService _MailService;

        private string _Title = "Тестовое окно";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
            
        }

        //в данных полях модель представления будет хранить свои данные
        private ObservableCollection<Server> _Servers;
        private ObservableCollection<Sender> _Senders;
        private ObservableCollection<Recipient> _Recipients;
        private ObservableCollection<Message> _Messages;

        public ObservableCollection<Sender> Senders { 
            get=> _Senders;
            set=> Set(ref _Senders,value);
        }

        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }

        public ObservableCollection<Message> Messages
        {
            get => _Messages;
            set => Set(ref _Messages, value);
        }

        

        public ObservableCollection<Server> Servers
        {
            get => _Servers;
            set => Set(ref _Servers, value);
        }

        private Server _SelectedServer;
        public Server SelectedServer
        {
            get => _SelectedServer;
            set => Set(ref _SelectedServer, value);
        }

        private Sender _SelectedSender;
        public Sender SelectedSender
        {
            get => _SelectedSender;
            set => Set(ref _SelectedSender, value);
        }

        private Recipient _SelectedRecipient;
        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }

        private Message _SelectedMessage;
        public Message SelectedMessage
        {
            get => _SelectedMessage;
            set => Set(ref _SelectedMessage, value);
        }

        #region Команды
        
        #region CreateNewServerCommand

        //Создание нового сервера

        private ICommand _CreateNewServerCommand;

        public ICommand CreateNewServerCommand => _CreateNewServerCommand
            ??= new LambdaCommand(OnCreateNewServerCommandExecuted, CanCreateNewServerCommandExecute);

        private bool CanCreateNewServerCommandExecute(object p)=> true;

        private void OnCreateNewServerCommandExecuted(object p)
        {
            MessageBox.Show("Создание нового сервера", "Управление серверами");
        }

        #endregion

        #region EditNewServerCommand

        //редактирование нового сервера

        private ICommand _EditNewServerCommand;

        public ICommand EditNewServerCommand => _EditNewServerCommand
            ??= new LambdaCommand(OnEditNewServerCommandExecuted, CanEditNewServerCommandExecute);

        private bool CanEditNewServerCommandExecute(object p) => p is Server || SelectedServer!= null;

        private void OnEditNewServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return; 
            MessageBox.Show($"Редактирование сервера {server.Address}", "Управление серверами");
        }

        #endregion

        #region DeleteNewServerCommand

        //удаление нового сервера

        private ICommand _DeleteNewServerCommand;

        public ICommand DeleteNewServerCommand => _DeleteNewServerCommand
            ??= new LambdaCommand(OnDeleteNewServerCommandExecuted, CanDeleteNewServerCommandExecute);

        private bool CanDeleteNewServerCommandExecute(object p) => p is Server || SelectedServer != null;

        private void OnDeleteNewServerCommandExecuted(object p)
        {
            var server = p as Server ?? SelectedServer;
            if (server is null) return;
            Servers.Remove(server); // удаляем сервер из списка
            SelectedServer = Servers.FirstOrDefault();// сбрасываем SelectedServer
      
            MessageBox.Show($"Удаление сервера {server.Address}", "Управление серверами");
        }

        #endregion

        #region  Command SendMailCommand - Отправка почты

        //редактирование нового сервера

        /// <summary>Отправка почты</summary>
        private ICommand _SendMailCommand;

        /// <summary> Отправка почты</summary>
        public ICommand SendMailCommand => _SendMailCommand
            ??= new LambdaCommand(OnSendMailCommandExecuted, CanSendMailCommandExecute);

        /// <summary>проверка возможности выполнения - Отправка почты</summary>
       
        private bool CanSendMailCommandExecute(object p)
        {
            if (SelectedServer is null) return false;
            if (SelectedSender is null) return false;
            if (SelectedRecipient is null) return false;
            if (SelectedMessage is null) return false;
            return true;

        }


        /// <summary>Логика выполнения - Отправка почты</summary>
       
        private void OnSendMailCommandExecuted(object p)
        {
            var server  = SelectedServer;
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;

            var mail_sender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
            mail_sender.Send(sender.Address, recipient.Address, message.Subject, message.Body);// команда отправки почты
            if (server is null) return;
            MessageBox.Show($"Редактирование сервера {server.Address}", "Управление серверами");
        }

        #endregion

        #endregion
        public MainWindowViewModel(IMailService MailService)
        {
            _MailService = MailService;
            Servers = new ObservableCollection<Server>(TestData.Servers);
            Senders = new ObservableCollection<Sender>(TestData.Senders);
            Recipients = new ObservableCollection<Recipient>(TestData.Recipients);
            Messages = new ObservableCollection<Message>(TestData.Messages);
        }
    }
}
