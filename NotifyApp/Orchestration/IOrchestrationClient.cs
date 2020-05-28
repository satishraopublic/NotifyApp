using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyApp.Orchestration
{
    public interface IOrchestrationClient : IContextMenuProvider
    {
        void RegisterWithOrchestrationManager();
    }
}
