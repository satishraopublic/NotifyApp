using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyApp.Communication
{
    public interface ICommunicationParticipant
    {
       void ReceiveMessage(ICommunicationParticipant sender, object message);
    }
}
