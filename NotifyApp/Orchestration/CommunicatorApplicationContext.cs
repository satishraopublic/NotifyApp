using NotifyApp.Communication;
using NotifyApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NotifyApp
{
    public class CommunicatorApplicationContext : Application, ICommunicationParticipant
    {
        protected ICommunicationHub _communicationHub;
        public CommunicatorApplicationContext(ICommunicationHub communicationHub)
        {
            _communicationHub = communicationHub;
        }

        public virtual void ReceiveMessage(ICommunicationParticipant sender, object message)
        {

        }
    }
}
