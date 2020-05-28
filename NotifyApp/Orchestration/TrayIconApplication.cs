using Hardcodet.Wpf.TaskbarNotification;
using NotifyApp.Communication;
using NotifyApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace NotifyApp
{
    public class TrayIconApplication : CommunicatorApplication
    {
        private System.Windows.Controls.ContextMenu _contextMenu;
        protected System.Windows.Controls.ContextMenu ContextMenu
        {
            get { return _contextMenu; }
        }

        private TaskbarIcon _notifyIcon;
        protected TaskbarIcon TrayIcon
        {
            get { return _notifyIcon; }
        }

        protected TrayIconApplication(ICommunicationHub communicationHub, string notifyIconText) : base(communicationHub)
        {
            Dispatcher.Invoke(() =>
            {
                _contextMenu = new System.Windows.Controls.ContextMenu();

                Exit += Application_ApplicationExit;

                _notifyIcon = new TaskbarIcon
                {
                    ContextMenu = _contextMenu,
                    ToolTipText = notifyIconText,
                    Visibility = System.Windows.Visibility.Visible
                };

                this.TrayIcon.TrayMouseDoubleClick += TrayIcon_TrayMouseDoubleClick;
            });
        }

        private void TrayIcon_TrayMouseDoubleClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.OnTrayIconDoubleClick(e);
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.OnApplicationExit(e);
        }

        protected virtual void OnApplicationExit(EventArgs e)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visibility = System.Windows.Visibility.Hidden;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
            _contextMenu = null;
        }
        protected virtual void OnTrayIconClick(MouseEventArgs e)
        { }

        protected virtual void OnTrayIconDoubleClick(RoutedEventArgs e)
        { }

        private void TrayIconClickHandler(object sender, MouseEventArgs e)
        {
            this.OnTrayIconClick(e);
        }

    }
}
