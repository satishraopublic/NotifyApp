using NotifyApp.Communication;
using NotifyApp.Messages;
using NotifyApp.Orchestration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NotifyApp.BackgroundWorkers
{
    public class ScrapeEngine : CommunicatorBackgroundService
    {
        public ScrapeEngine(ICommunicationHub hub) : base(hub)
        {
            _backgroundServiceName = "Scrape Engine Service";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(2000);
            }
        }

        #region "Menu Management"
        List<System.Windows.Controls.MenuItem> menu;
        public override List<System.Windows.Controls.MenuItem> GetMenu()
        {
            if (menu == null)
            {
                menu = new List<System.Windows.Controls.MenuItem>();
                var item = new System.Windows.Controls.MenuItem
                {
                    Header = "Show Scrape Engine Status...",
                    Name = "ShowScrapeEngine"
                };
                item.Click += ShowScrapeEngineStatus;
                menu.Add(item);
            }
            return menu;
        }

        private void ShowScrapeEngineStatus(object sender, EventArgs e)
        {
            MessageBox.Show("Scrape Engine is running");
        }
        #endregion

    }
}
