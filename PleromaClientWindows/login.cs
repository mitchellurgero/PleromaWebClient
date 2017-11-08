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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.instance = instanceUrl.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Please restart the application for changes to take affect.");
            this.Close();
        }
    }
}
