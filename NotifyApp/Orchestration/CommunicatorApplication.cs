using NotifyApp.Communication;
using NotifyApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NotifyApp
{
    public class CommunicatorApplication : Application, ICommunicationParticipant
    {
        protected ICommunicationHub _communicationHub;
        public CommunicatorApplication(ICommunicationHub communicationHub)
        {
            _communicationHub = communicationHub;
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        public virtual void ReceiveMessage(ICommunicationParticipant sender, object message)
        {

        }
    }
}
