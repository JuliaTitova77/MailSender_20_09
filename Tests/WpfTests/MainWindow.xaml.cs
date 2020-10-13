using System.Threading;
using System.Windows;

namespace WpfTests
{   
    
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComputerResultButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //new Thread(() => ResultText.Text = GetResultHard()).Start();// так нельзя использовать из потока в котором они не были созданы
            new Thread(() =>
            {
                var result = GetResultHard();
                Application.Current.Dispatcher.Invoke(() => ResultText.Text = result);//в этом случае приложение будет работать корректно и
                //интерфейс виснуть не будет

            })
            { IsBackground = true}.Start();
               
               
            
        }

        private string GetResultHard()
        {
            for(var i = 0; i < 500; i++)
            {
                Thread.Sleep(10);
            }
            return "Hello World";
        }
    }
}
