using System;
using MailSender.lib.Interfaces;
using MailSender.lib.Service;
using MailSender.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MailSender
{
    public partial class App
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureServices(ConfigureServices)
            .Build();
        // доступ у контейнеру
        public static IServiceProvider Services => Hosting.Services;
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();// AddSingleton создается один раз и выдается один и тот же 
            services.AddTransient<IMailService, SmtpMailService>();//AddTransient  каждый раз будет создаваться новый
            // services.AddScoped<>() исполь в веб программировании  получает один и тот же объект как только подключение 
            //завершается то вссе объекты сгенерированные уничтожаются
        }
    }
}
