using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void registerAcc_Click(object sender, EventArgs e)
        {
            if (!Chat.Connect())
            {
                MessageBox.Show("Failed to reach the server, try again later!");
                return;
            }

            Chat.SendData($"reg\\{usrBox.Text}\\{passBox.Text}");
            
            Chat.username = usrBox.Text;
            Chat.password = passBox.Text;

            this.Close();
        }
    }
}
