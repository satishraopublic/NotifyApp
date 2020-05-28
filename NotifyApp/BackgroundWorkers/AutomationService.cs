using FlaUI.Core;
using FlaUI.UIA3;
using NotifyApp.Automation;
using NotifyApp.Communication;
using NotifyApp.Messages;
using NotifyApp.Orchestration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace NotifyApp.BackgroundWorkers
{
    public class AutomationService : CommunicatorBackgroundService, IOrchestrationClient
    {
        private AutomationBase _automation;
        private AutomationHelper _elementFinder;
        private ITreeWalker _treeWalker;
        private AutomationElement _rootElement;

        public AutomationService(ICommunicationHub hub) : base(hub)
        {
            _backgroundServiceName = "Automation Service";
            SubscribeToCommunication();
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Init();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            DisposeAutomationObjects();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(400);
            }
        }

        private void DisposeAutomationObjects()
        {
            _elementFinder.ControlReleasedOverElement += ControlReleasedOverElement;
            _elementFinder.ControlReleasedOverElement -= ControlReleasedOverElement;

            _rootElement = null;
            _treeWalker = null;
            _automation = null;
            _elementFinder = null;
        }

        private void Init()
        {
            _automation = new UIA3Automation();
            // Initialize TreeWalker
            _treeWalker = _automation.TreeWalkerFactory.GetControlViewWalker();
            _rootElement = _automation.GetDesktop();

            _elementFinder = new AutomationHelper(_automation);
            _elementFinder.ControlReleasedOverElement += ControlReleasedOverElement;
        }

        private void ControlReleasedOverElement(AutomationElement obj)
        {
            if (ElementIsTopLevel(obj))
            {
            }
            else
            {

            }
        }


        private bool ElementIsTopLevel(AutomationElement obj)
        {
            if (Equals(obj.Parent, _rootElement)) return true;
            return false;
        }



        #region "Communication Management"
        private void SubscribeToCommunication()
        {
            _communicationHub.Subscribe<InitiateElementDetection>(this);
            _communicationHub.Subscribe<SuspendElementDetection>(this);
        }
        public override void ReceiveMessage(ICommunicationParticipant sender, object message)
        {
            if (message is InitiateListeningMessage)
            {
                _isDetectElement = true;
            }
            else if (message is SuspendListeningMessage)
            {
                _isDetectElement = false;
            }
        }
        #endregion

        #region "Menu Management"
        List<System.Windows.Controls.MenuItem> menu;
        private bool _isDetectElement=false;

        public override List<System.Windows.Controls.MenuItem> GetMenu()
        {
            if (menu == null)
            {
                menu = new List<System.Windows.Controls.MenuItem>();
                var item = new System.Windows.Controls.MenuItem
                {
                    Header = "Start Element Detection", 
                    Name = "ElementDetector",
                    IsChecked = false
                };
                item.Click += ToggleElementDetection;
                menu.Add(item);
            }
            return menu;
        }

        private void ToggleElementDetection(object sender, EventArgs e)
        {
            System.Windows.Controls.MenuItem item = sender as System.Windows.Controls.MenuItem;
            if (item != null)
            {
                if (string.Equals(item.Header, "Start Element Detection"))
                {
                    item.Header = "Stop Element Detection";
                    item.IsChecked = true;
                    StartElementDetection();
                }
                else
                {
                    item.Header = "Start Element Detection";
                    item.IsChecked = false;
                    StopElementDetection();
                }
            }
        }

        private void StopElementDetection()
        {
            _elementFinder.Stop();
        }

        private void StartElementDetection()
        {
            _elementFinder.Start();
        }
        #endregion

    }
}
