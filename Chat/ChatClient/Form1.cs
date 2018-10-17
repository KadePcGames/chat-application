using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Task.Run(() => DataListener());
        }

        private async void send_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (Encoding.UTF8.GetBytes(inputBox.Text).Length > 1024)
                {
                    MessageBox.Show("Message is too long");
                    return;
                }

                if (!Chat.SendData($"msg\\{Chat.username}\\{Chat.password}\\{inputBox.Text}"))
                    MessageBox.Show("Failed to send the message");
                else
                    this.Invoke(new Action(() => inputBox.Text = ""));
            });
        }

        private void DataListener()
        {
            while (true)
            {
                string[] data = Chat.Recieve();

                this.Invoke(new Action(() => outputBox.AppendText(data[0] + "\n")));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Chat.SendData($"exit\\{Chat.username}\\{Chat.password}");
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
