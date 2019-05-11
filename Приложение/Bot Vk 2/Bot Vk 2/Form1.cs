using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot_Vk_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = DataBank.Text;
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            
            


            
                Form2 form2 = (Form2)System.Windows.Forms.Application.OpenForms["Form2"]; // Где Form2 это имя формы содежащаяся в переменной Name.
                if (form2 == null) // Если форма не существует, то создаём
                {
                    Form2 fm2 = new Form2(); // Создание нового экземпляра формы
                    fm2.Show(); // Отображаем форму
                }
                else
                    form2.Activate(); // Активируем форму на передний план (из трея или заднего плана)
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = DataBank.Text;
            
        }
    }
}
