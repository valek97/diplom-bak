using Bot_Vk.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace Bot_Vk
{
    public partial class Form1 : Form
    {

        VkApi vkApi = new VkApi();


        public Form1()
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* int appId = 6769897;
             string email = "valya-kritenko@yandex.ru";
             string password = "89190376493Fkmaf5";
             VkNet.Enums.Filters.Settings settings = VkNet.Enums.Filters.Settings.All;
                 */
            vkApi.Authorize(new ApiAuthParams
            {
                ApplicationId = 6769897,
                Login = "valya-kritenko@yandex.ru",
                Password = "89190376493Fkmaf5",
                Settings = VkNet.Enums.Filters.Settings.All,
                TwoFactorAuthorization = () =>
                {

                    Console.WriteLine("Enter Code:");
                    return Console.ReadLine();

                }
            });
        }


    }
}

        



 

