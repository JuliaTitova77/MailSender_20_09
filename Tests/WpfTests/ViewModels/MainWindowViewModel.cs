using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using WpfTests.ViewModels.Base;


namespace WpfTests.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Тестовое окно";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
            //set
            //{
            //    if (_Title == value) return;
            //    _Title = value;
            //    //OnPropertyChanged("Title");
            //    //OnPropertyChanged(nameof(Title));
            //    OnPropertyChanged();
            //}
        }

        public DateTime CurrentTime => DateTime.Now;
        private readonly Timer _Timer;
        private bool _TimerEnabled = true;

        public bool TimerEnabled {
            get => _TimerEnabled;
            set
            {
                if (!Set(ref _TimerEnabled, value)) return;
                _Timer.Enabled = value;
            } 
        }
        public MainWindowViewModel()
        {
            _Timer = new Timer(100);
            _Timer.Elapsed += OnTimerElapsed;
            _Timer.AutoReset = true;
            _Timer.Enabled = true;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }
    }
}
