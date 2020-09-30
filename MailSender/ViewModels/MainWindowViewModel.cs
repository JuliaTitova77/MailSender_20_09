using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Тестовое окно";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
            
        }
    }
}
