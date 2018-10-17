using System;
using System.Windows.Forms;
using System.Net.Sockets;

namespace ChatClient
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void connectAndLogin_Click(object sender, EventArgs e)
        {
            if (!Chat.Connect())
            {
                MessageBox.Show("Failed to reach the server, try again later!");
                return;
            }

            Chat.SendData($"login\\{usrBox.Text}\\{passBox.Text}");

            string[] data = Chat.Recieve();

            bool isValid = bool.Parse(data[0]);

            if (isValid)
            {
                Chat.username = usrBox.Text;
                Chat.password = passBox.Text;
                Form1 main = new Form1();
                this.Hide();
                main.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("An error occured: " + data[1]);
        }

        private void register_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            this.Hide();
            reg.ShowDialog();
            this.Show();
        }
    }
}