using Microsoft.Extensions.Hosting;
using NotifyApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotifyApp
{
    public class NotifiedServiceApplicationContext : TrayIconApplicationContext, IHostedService
    {
        IconInformerService _iconInformer;
        public NotifiedServiceApplicationContext(IconInformerService iconInformer): base(iconInformer.GetIconText())
        {

            _iconInformer = iconInformer;
            this.TrayIcon.Icon = new Icon(_iconInformer.GetIconPath());

            this.ContextMenu.Items.Add("&Settings...", null, this.SettingsContextMenuClickHandler).Font = new Font(this.ContextMenu.Font, FontStyle.Bold);
            this.ContextMenu.Items.Add("-");
            this.ContextMenu.Items.Add("&About...", null, this.AboutContextMenuClickHandler);
            this.ContextMenu.Items.Add("-");
            this.ContextMenu.Items.Add("E&xit", null, this.ExitContextMenuClickHandler);

            Application.Run(this);

        }

        private void ExitContextMenuClickHandler(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutContextMenuClickHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SettingsContextMenuClickHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ExecuteAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

    }

    public class TrayIconApplicationContext: ApplicationContext
    {
        private readonly ContextMenuStrip _contextMenu;
        protected ContextMenuStrip ContextMenu
        {
            get { return _contextMenu; }
        }

        private readonly NotifyIcon _notifyIcon;
        protected NotifyIcon TrayIcon
        {
            get { return _notifyIcon; }
        }
        
        protected TrayIconApplicationContext(string notifyIconText)
        {
            _contextMenu = new ContextMenuStrip();

            Application.ApplicationExit += Application_ApplicationExit;

            _notifyIcon = new NotifyIcon
            {
                ContextMenuStrip = _contextMenu,
                Text = notifyIconText,
                Visible = true
            };

            this.TrayIcon.MouseDoubleClick += this.TrayIconDoubleClickHandler;
            this.TrayIcon.MouseClick += this.TrayIconClickHandler;

        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.OnApplicationExit(e);
        }

        protected virtual void OnApplicationExit(EventArgs e)
        {
            if(_contextMenu != null)  _contextMenu.Dispose();
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
            }
        }
        protected virtual void OnTrayIconClick(MouseEventArgs e)
        { }

        protected virtual void OnTrayIconDoubleClick(MouseEventArgs e)
        { }

        private void TrayIconClickHandler(object sender, MouseEventArgs e)
        {
            this.OnTrayIconClick(e);
        }

        private void TrayIconDoubleClickHandler(object sender, MouseEventArgs e)
        {
            this.OnTrayIconDoubleClick(e);
        }
    }
}
