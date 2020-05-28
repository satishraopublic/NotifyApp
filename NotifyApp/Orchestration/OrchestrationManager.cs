using Microsoft.Extensions.Hosting;
using NotifyApp.Communication;
using NotifyApp.Messages;
using NotifyApp.Orchestration;
using NotifyApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NotifyApp
{
    public class OrchestrationManager : TrayIconApplicationContext, IHostedService
    {
        IconInformerService _iconInformer;

        private readonly List<IOrchestrationClient> _registeredService = new List<IOrchestrationClient>();

         public OrchestrationManager(ICommunicationHub communicationHub, IconInformerService iconInformer): base(communicationHub, iconInformer.GetIconText())
        {

            _iconInformer = iconInformer;
            this.TrayIcon.Icon = new Icon(_iconInformer.GetIconPath());
            _communicationHub.Subscribe<OrchestrationRegistration>(this);

            SetContextMenu();
        }

        private void Refresh()
        {
            SetContextMenu();
        }
        private void SetContextMenu()
        {
            this.ContextMenu.Items.Clear();
            SetContextMenuForRegisteredServices();
            SetContextMenuForOrchestrationManager();
        }

        private void SetContextMenuForOrchestrationManager()
        {
            this.ContextMenu.Items.Add(new System.Windows.Controls.Separator());
            var item = new System.Windows.Controls.MenuItem();
            item.Header = "Settings...";
            item.Click += this.SettingsContextMenuClickHandler;
            item.FontWeight = FontWeights.Bold;
            this.ContextMenu.Items.Add(item);
            this.ContextMenu.Items.Add(new System.Windows.Controls.Separator());
            item = new System.Windows.Controls.MenuItem();
            item.Header = "About...";
            item.Click += this.AboutContextMenuClickHandler;
            item.FontWeight = FontWeights.Bold;
            this.ContextMenu.Items.Add(item);
            this.ContextMenu.Items.Add(new System.Windows.Controls.Separator());
            item = new System.Windows.Controls.MenuItem();
            item.Header = "Exit";
            item.Click += this.ExitContextMenuClickHandler;
            item.FontWeight = FontWeights.Bold;
            this.ContextMenu.Items.Add(item);
        }

        private void SetContextMenuForRegisteredServices()
        {
            if (_registeredService?.Any() == true)
            {
                foreach (var serv in _registeredService)
                {
                    foreach (var item in serv.GetMenu())
                    {
                        this.ContextMenu.Items.Add(item);
                    }
                }
            }
        }

        private void ExitContextMenuClickHandler(object sender, EventArgs e)
        {
            Shutdown(0);
        }

        private void AboutContextMenuClickHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SettingsContextMenuClickHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Run();
            await ExecuteAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override void ReceiveMessage(ICommunicationParticipant sender, object message)
        {
            if(message is OrchestrationRegistration)
            {
                Register((message as OrchestrationRegistration).Registrer);
            }
        }

        private void Register(IOrchestrationClient registrer)
        {
            _registeredService.Add(registrer);
            Refresh();
        }

    }

}
