using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Вывод информации в RichTextBox
        /// </summary>
        /// <param name="Text"></param>
        private void Send (string Text)
        {
            richTextBox1.AppendText(Text + "\n");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var auth = new work.AuthToken(Login.Text, Pass.Text);
                if (auth.Auth() == true)
                    Send("Авторизация удалась:" + auth.Result.user_id);
                else
                    Send("Авторизация не удалась, проверьте данные");
            }
            else if (radioButton2.Checked == true) { }
            else if(radioButton3.Checked == true) { }
            else if(radioButton4.Checked == true) { }
        }
    }
}
