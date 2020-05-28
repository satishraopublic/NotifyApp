using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotifyApp.BackgroundWorkers;
using NotifyApp.Communication;
using NotifyApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotifyApp
{
    public static class program
    {
        [System.STAThreadAttribute()]
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public static void Main()
        {
            CreateHostBuilder().Build().Run();
        }

        private static IHostBuilder CreateHostBuilder()
        {
            var host = Host.CreateDefaultBuilder()
                   .ConfigureServices(
                   (context, services) => {
                    // Register all Dependencies
                    services.AddSingleton<ICommunicationHub, CommunicationHub>();
                       services.AddSingleton<IconInformerService>();

                    // Register all Background Services
                    services.AddHostedService<Listener>();
                       services.AddHostedService<ScrapeEngine>();
                       services.AddHostedService<AutomationService>();

                    // Finally Register Notified Service
                    // This service needs to be registered last
                    // since this is the UI thread 
                    services.AddHostedService<OrchestrationManager>();
                   });
            return host;
        }

    }
}
