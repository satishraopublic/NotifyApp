using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotifyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotifyApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            await CreateHostBuilder().Build().RunAsync();
        }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices(
                (context, services) => {
                    services.AddHostedService<Worker>();
                    services.AddHostedService<NotifiedServiceApplicationContext>();
                    services.AddSingleton<IconInformerService>();
                }
                );
        }
    }
}
