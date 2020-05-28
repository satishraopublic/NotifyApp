using NotifyApp.Services.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyApp.Messages
{
    public class NewServiceAddedMessage
    {
        public string ServiceName { get; set; }

        public ServiceTypes ServiceType { get; set; }

    }
}
