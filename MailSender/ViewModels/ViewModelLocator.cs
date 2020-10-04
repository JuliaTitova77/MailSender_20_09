
using Microsoft.Extensions.DependencyInjection;

// Специальный объект, который из любой точки разметки получить то что вам нужно. 
// Состоит из свойств, каждый из которых представляет viewvodel
// он за нас будет создавать объекты сервисов  создавая new
namespace MailSender.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
