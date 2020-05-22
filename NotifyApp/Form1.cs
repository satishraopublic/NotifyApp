using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotifyApp
{
    public partial class Form1 : Form
    {
        Container container;
        ContextMenuStrip contextMenu1;
        ToolStripMenuItem menuItem1;
        public Form1()
        {
            InitializeComponent();
            InitiateNotifyIcon();
        }

        NotifyIcon icon;
        private void InitiateNotifyIcon()
        {
            container = new System.ComponentModel.Container();
            contextMenu1 = new System.Windows.Forms.ContextMenuStrip();
            menuItem1 = new System.Windows.Forms.ToolStripMenuItem();

            // Initialize contextMenu1
            contextMenu1.Items.AddRange(
                        new System.Windows.Forms.ToolStripMenuItem[] { menuItem1 });

            // Initialize menuItem1
            menuItem1.Text = "E&xit";
            menuItem1.Click += new System.EventHandler(menuItem1_Click);

            // Set up how the form should be displayed.
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Text = "Notify Icon Example";

            // Create the NotifyIcon.
            var notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new Icon(@"..\..\..\services.ico");

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon1.ContextMenuStrip = contextMenu1;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Text = "Form1 (NotifyIcon example)";
            notifyIcon1.Visible = true;

            // Handle the DoubleClick event to activate the form.
            notifyIcon1.DoubleClick += new System.EventHandler(notifyIcon1_DoubleClick);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Double Click");
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            MessageBox.Show(item.Text);
        }

        protected override void OnResize(EventArgs e)
        {
            ShowNotifyIcon();
            base.OnResize(e);
        }

        private void ShowNotifyIcon()
        {
            if(icon != null)   icon.Visible = true;
        }
    }
}
