using Gecko;
using Gecko.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PleromaClientWindows
{
    public partial class Form1 : Form
    {
        public GeckoWebBrowser geckoWebBrowser = new GeckoWebBrowser { Dock = DockStyle.Fill };
        public Form1()
        {
            
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            this.Controls.Add(geckoWebBrowser);
            geckoWebBrowser.Dock = DockStyle.Fill;
            geckoWebBrowser.DocumentCompleted += GeckoWebBrowser1_DocumentCompleted;
            geckoWebBrowser.DocumentTitleChanged += GeckoWebBrowser1_DocumentTitleChanged;
            if (Properties.Settings.Default.instance != "null")
            {
                geckoWebBrowser.Navigate(Properties.Settings.Default.instance);
            } else
            {
                login l = new login();
                l.ShowDialog();
            }
            
        }

        private void GeckoWebBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = geckoWebBrowser.DocumentTitle;
            GeckoDocument doc = geckoWebBrowser.Document;
            GeckoNodeCollection gnc = doc.GetElementsByClassName("notification unseen");
            if (gnc.Count() >= 1)
            {
                //Popup notifications?
                FlashWindow.Start(this);
            } else
            {
                FlashWindow.Stop(this);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Maximised)
            {
                WindowState = FormWindowState.Maximized;
                Location = Properties.Settings.Default.Location;
                Size = Properties.Settings.Default.Size;
            }
            else if (Properties.Settings.Default.Minimised)
            {
                WindowState = FormWindowState.Minimized;
                Location = Properties.Settings.Default.Location;
                Size = Properties.Settings.Default.Size;
            }
            else
            {
                Location = Properties.Settings.Default.Location;
                Size = Properties.Settings.Default.Size;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
                Properties.Settings.Default.Maximised = true;
                Properties.Settings.Default.Minimised = false;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Location = Location;
                Properties.Settings.Default.Size = Size;
                Properties.Settings.Default.Maximised = false;
                Properties.Settings.Default.Minimised = false;
            }
            else
            {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
                Properties.Settings.Default.Maximised = false;
                Properties.Settings.Default.Minimised = true;
            }
            Properties.Settings.Default.Save();
        }
        private void GeckoWebBrowser1_DocumentCompleted(object sender, EventArgs e)
        {
            // Here you can add the coding to perform after document loaded
            //GeckoElement ghe = geckoWebBrowser.Document.GetElementById("app");
            //GeckoNodeCollection gnc = geckoWebBrowser.Document.GetElementsByClassName("notification unseen");

        }
    }
}
