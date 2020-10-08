using MailSender.lib.Models;

using System.Windows.Controls;


namespace MailSender.Views
{
    /// <summary>
    /// Логика взаимодействия для RecipientEditor.xaml
    /// </summary>
    public partial class RecipientEditor : UserControl
    {
        public RecipientEditor() => InitializeComponent();
        

        private void OnDataValidationError(object sender, ValidationErrorEventArgs e)
        {
            //Control control = (Control)Sender;
            //if (e.Action == ValidationErrorEventAction.Added)
            //{
            //    control.ToolTip = e.Error.ErrorContent.ToString();
            //}
            //else
            //{
            //    control.ClearValue(ToolTipProperty);
            //}
        }
    }
}
