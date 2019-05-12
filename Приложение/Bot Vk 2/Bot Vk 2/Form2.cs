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


        delegate void Message();
       
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Hello()
        {
            Form3 form = new Form3();
            form.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                Login = "valya-kritenko@yandex.ru ",
                Password = "89190376493Fkmaf5",
                Settings = Settings.All,
                TwoFactorAuthorization = () =>
                {
                    var frm = new Form3();
                    frm.ShowDialog();
                    return frm.Code;
                }
                









            });
        }


    }
}
