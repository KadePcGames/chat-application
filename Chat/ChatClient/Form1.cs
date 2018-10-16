using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Task.Run(() => OutputMessages());
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (!Chat.SendData($"msg\\{Chat.username}\\{Chat.password}\\{inputBox.Text}"))
                MessageBox.Show("Failed to send the message");
        }

        private void OutputMessages()
        {
            while (true)
            {
                string[] data = Chat.Recieve().Split('\\');

                this.Invoke(new Action(() => outputBox.AppendText(data[0] + "\n")));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Chat.SendData($"exit\\{Chat.username}\\{Chat.password}");
        }
    }
}
