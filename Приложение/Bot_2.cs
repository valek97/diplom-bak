using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Bot_2
{
    public partial class Form1 : Form
    {
        public string token;
        public int user_id;
        int groupe = -157199051;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void Send(string Text)
        {
            richTextBox1.AppendText(Text + "\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*var auth = new work.AuthToken(Login.Text, Pass.Text);
            if (auth.Auth() == true)
                Send("Авторизация удалась:" + auth.Result.user_id);
            else
                Send("Авторизация не удалась, проверьте данные");
                */
             Authoreg();

        }
        void Authoreg()
        { 
            string response = work.vk.oAuth(Login.Text,Pass.Text );
            JObject json = JObject.Parse(response);
            if (response.Contains("error") || Login.Text.Length < 5 || Pass.Text.Length < 5)
                label3.Text = "Проверьте данные";
            else;
            {
                token = json["access_token"].ToString();
                user_id = (int)json ["user_id"];
                label3.Text = "Авторизация не удалась";
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            groupBox6.Text = $"Лог [{richTextBox1.Lines.Count()}]";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string response = work.vk.groupeGetById(Convert.ToInt32( textBox3.Text));
            richTextBox1.Text = response;
        }
    }
}
