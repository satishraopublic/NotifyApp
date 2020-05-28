using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotifyApp.Communication
{
    public class CommunicationHub : ICommunicationHub
    {
        Dictionary<Type, List<ICommunicationParticipant>> subscriptions;
        public CommunicationHub()
        {
            subscriptions = new Dictionary<Type, List<ICommunicationParticipant>>();
        }

        public void Subscribe<MessageType>(ICommunicationParticipant caller)
        {
            if (!subscriptions.ContainsKey(typeof(MessageType)))
            {
                subscriptions.Add(typeof(MessageType), new List<ICommunicationParticipant>() { caller });
            }
            else
            {
                subscriptions[typeof(MessageType)]?.Add(caller);
            }
        }


        public void SendMessage(ICommunicationParticipant participant, object message)
        {
            if (subscriptions.ContainsKey(message.GetType()))
            {
                List<ICommunicationParticipant> subscribers = subscriptions[message.GetType()];
                if (subscribers?.Any() == true)
                {
                    foreach (var sub in subscribers)
                    {
                        sub.ReceiveMessage(participant, message);
                    }
                }
            }
        }
    }
}
