using System;
using MailSender.lib.Interfaces;
using MailSender.lib.Service;
using MailSender.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MailSender
{
    public partial class App
    {
        private static IHost _Hosting;

        public static IHost Hosting => _Hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureAppConfiguration(cfg => cfg
            .AddJsonFile("appconfig.json",true,true)//true,true говорит о том что файл явл опциональным и если отсутствует то не беда
            .AddXmlFile("appsetting.xml",true,true)
            )
            .ConfigureLogging(log => log
            .AddConsole()
            .AddDebug()
            )//добавляеи систему логгирования, вывод в дебаг
            .ConfigureServices(ConfigureServices)
            .Build();
        // доступ у контейнеру
        public static IServiceProvider Services => Hosting.Services;
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();// AddSingleton создается один раз и выдается один и тот же 

#if DEBUG
            services.AddSingleton<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();//AddTransient  каждый раз будет создаваться новый
            // services.AddScoped<>() исполь в веб программировании  получает один и тот же объект как только подключение 
            //завершается то вссе объекты сгенерированные уничтожаются
#endif
            services.AddSingleton<IEncryptorService, Rfc2898Encryptor>();
        }
    }
}
