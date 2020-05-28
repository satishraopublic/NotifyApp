using Microsoft.Extensions.Hosting;
using NotifyApp.Communication;
using NotifyApp.Messages;
using NotifyApp.NativeMethods;
using NotifyApp.NativeMethods.Keyboard;
using NotifyApp.Orchestration;
using NotifyApp.Services.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace NotifyApp.BackgroundWorkers
{
    public class Listener : CommunicatorBackgroundService, IOrchestrationClient
    {

        bool _isListen = false;
        public Listener(ICommunicationHub hub): base(hub)
        {
            _backgroundServiceName = "LOB Listener Service";
            SubscribeToCommunication();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            WinApi.MSG msg;
            int ret = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_isListen)
                {
                    Keyboard.set_hook();
                    ret = WinApi.GetMessage(out msg, IntPtr.Zero, 0, 0);

                    if (ret <= 0)
                    {
                        break;
                    }
                    WinApi.TranslateMessage(ref msg);
                    WinApi.DispatchMessage(ref msg);
                }
                else
                {
                    await Task.Delay(2000);
                }
            }
        }

        #region "Communication Management"
        private void SubscribeToCommunication()
        {
            _communicationHub.Subscribe<InitiateListeningMessage>(this);
            _communicationHub.Subscribe<SuspendListeningMessage>(this);
        }
        public override void ReceiveMessage(ICommunicationParticipant sender, object message)
        {
            if(message is InitiateListeningMessage)
            {
                _isListen = true;
            }
            else if (message is SuspendListeningMessage)
            {
                _isListen = false;
            }
        }
        #endregion

        #region "Menu Management"
        List<System.Windows.Controls.MenuItem> menu;
        public override List<System.Windows.Controls.MenuItem> GetMenu()
        {
            if(menu == null)
            {
                menu = new List<System.Windows.Controls.MenuItem>();
                var item = new System.Windows.Controls.MenuItem
                {
                    Header = "Start Listening",
                    Name = "ListenToggle",
                    IsChecked = false
                };
                item.Click += ToggleListening;
                menu.Add(item);
            }
            return menu;
        }

        private void ToggleListening(object sender, EventArgs e)
        {
            System.Windows.Controls.MenuItem item = sender as System.Windows.Controls.MenuItem;
            if (item != null)
            {
                if (string.Equals(item.Header, "Start Listening"))
                {
                    item.Header = "Stop Listening";
                    item.IsChecked = true;
                    StartListening();
                }
                else
                {
                    item.Header = "Start Listening";
                    item.IsChecked = false;
                    StopListening();
                }
            }
        }

        private void StopListening()
        {
            
        }

        private void StartListening()
        {
            
        }
        #endregion
    }
}
