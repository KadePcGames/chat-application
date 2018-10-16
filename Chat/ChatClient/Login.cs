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

            bool isValid = bool.Parse(Chat.Recieve());

            Chat.username = usrBox.Text;
            Chat.password = passBox.Text;

            if (isValid)
            {
                Form1 main = new Form1();
                main.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Invalid username or password");
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