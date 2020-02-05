using System;
using Microsoft.MobileBlazorBindings;
using Microsoft.Extensions.Hosting;
using Xamarin.Forms;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace TestMobileBlazorBindingsApp
{
    public class App : Application
    {
        public App()
        {
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    //services.AddSingleton<AppState>();
                })
                .Build();

            MainPage = new ContentPage();
            host.AddComponent<HelloWorld>(parent: MainPage);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public static class MobileBlazorBindingsHost
    {
        public static IHostBuilder CreateDefaultBuilder()
        {
            // Inspired by Microsoft.Extensions.Hosting.Host, which can be seen here:
            // https://github.com/dotnet/extensions/blob/master/src/Hosting/Hosting/src/Host.cs
            // But slightly modified to work on all of Android, iOS, and UWP.

            var builder = new HostBuilder();

            builder.UseContentRoot(Directory.GetCurrentDirectory());

            builder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConsole(configure => configure.DisableColors = true);
                logging.AddDebug();
                logging.AddEventSourceLogger();
            })
            .UseDefaultServiceProvider((context, options) =>
            {
                var isDevelopment = context.HostingEnvironment.IsDevelopment();
                options.ValidateScopes = isDevelopment;
                options.ValidateOnBuild = isDevelopment;
            });

            return builder;
        }
    }
}
