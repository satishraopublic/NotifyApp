using NotifyApp.Orchestration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyApp.Messages
{
    public class OrchestrationRegistration
    {
        public IOrchestrationClient Registrer { get; private set; }
        public OrchestrationRegistration(IOrchestrationClient registrer)
        {
            Registrer = registrer;
        }
    }
}
