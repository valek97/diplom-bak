using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace Bot_Vk_2
{
    public partial class Form2 : Form
    {
        public string Data
        {
            get { return textBox1.Text; }
            //set { textBox1.Text = value; }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = "",
                Password = "",
                Settings = Settings.All,
                TwoFactorAuthorization = () =>
                {

                    Form3 form = new Form3();
                    form.Show();
                    return DataBank.Text;




                }

                







            });
        }


    }
}
