using Microsoft.Extensions.Hosting;
using NotifyApp.Messages;
using NotifyApp.Orchestration;
using NotifyApp.Services.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace NotifyApp.Communication
{
    public class CommunicatorBackgroundService : BackgroundService, ICommunicationParticipant, IOrchestrationClient
    {
        protected ICommunicationHub _communicationHub;
        protected string _backgroundServiceName = "Background Worker";
        protected ServiceTypes _serviceTypes = ServiceTypes.CommunicatorBackgroundService;


        public CommunicatorBackgroundService(ICommunicationHub hub)
        {
            _communicationHub = hub;
        }

        public virtual List<MenuItem> GetMenu()
        {
            throw new NotImplementedException();
        }

        public virtual void ReceiveMessage(ICommunicationParticipant sender, object message)
        {
        }

        public void RegisterWithOrchestrationManager()
        {
            _communicationHub.SendMessage(this, new OrchestrationRegistration(this));
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            RegisterWithOrchestrationManager();
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
