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

namespace Bot_3
{
    public partial class Form1 : Form
    {
        public string token;
        public int user_id;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Send(string Text)
        {
            richTextBox1.AppendText(Text + "\n");
           // richTextBox1.AppendText(AuthToken.Result.access_token);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                var auth = new AuthToken(textBox1.Text, textBox2.Text);
                if (auth.Auth() == true)
                    Send("Авторизация удалась:" + AuthToken.Result.user_id);
                else
                    Send("Авторизация не удалась, проверьте данные");
            
            // string[] Data = File.ReadAllLines("login.txt");
            //Authoreg();

        }
        void Authoreg()
        {
            string response = vk.oAuth(textBox1.Text, textBox2.Text);
            //JObject json = JObject.Parse(response);
            JObject Json = JObject.Parse(response);
            if (response.Contains("error") || textBox1.Text.Length < 5 || textBox2.Text.Length < 5)
                label3.Text = "проверьте данные";
            else
            {
                token = Json["access_token"].ToString();
                user_id = (int)Json["user_id"];
                label3.Text = "Авторизация удалась";
                
            }





        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Кол строк в логе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            groupBox6.Text = $"Лог [{richTextBox1.Lines.Count()}]";
        }
        /// <summary>
        /// Откуда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// куда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
