using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace Bot_3
{
    public partial class Form1 : Form
    {
        Thread[] Thread = new Thread[2];
        public int user_id;
        public int group_Otkuda, group_Kuda;

        public Form1()
        {
            InitializeComponent();
        }

        private void Send(string Text)
        {
            // richTextBox1.AppendText(Text + "\n");

            this.Invoke((MethodInvoker)delegate ()
            {
                richTextBox1.AppendText($"{Text}\n");
                richTextBox1.ScrollToCaret();
            });

            //richTextBox1.AppendText($"{Text}" + "\n");
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
                Variable.token = AuthToken.Result.access_token;
                user_id = AuthToken.Result.user_id;
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
            string response = vk.groupeGetById(Convert.ToInt32(textBox3.Text));

            Parse.GetById GrouInfo = JsonConvert.DeserializeObject<Parse.GetById>(response);
            group_Otkuda = Convert.ToInt32(textBox3.Text);
            Send($"Группа откуда  будем загружать посты. Название группы:{GrouInfo.response[0].name}, id {GrouInfo.response[0].id}");

        }
        /// <summary>
        /// куда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            string response = vk.groupeGetById(Convert.ToInt32(textBox4.Text));
            group_Kuda = Convert.ToInt32(textBox4.Text);
            Parse.GetById GrouInfo = JsonConvert.DeserializeObject<Parse.GetById>(response);
            Send($"Группа куда будем загружать посты. Название группы:{GrouInfo.response[0].name} id {GrouInfo.response[0].id}");
        }
        /// <summary>
        /// Старт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Varb();

            Thread[0] = new Thread(delegate() { work(); });
            Thread[0].Start();
            
            
        }
        /// <summary>
        /// Заполнить данные
        /// </summary>
        void Varb ()
        {
            group_Otkuda = Convert.ToInt32(textBox3.Text);
            group_Kuda = Convert.ToInt32(textBox4.Text);
            
        }


        private void groupBox6_Enter(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Работа с данными
        /// </summary>

        void work()
        {
            AddInTableImage();
            Send("Поток успешно остановлен, выберите посты которые будем загружать и нажмите продолжить");
            Thread[0].Suspend();

            this.Invoke((MethodInvoker)delegate ()
            {
                Variable.dataGridView = dataGridView1;
            });

            
            UploadImageWallPost();


        }


        /// <summary>
        /// Загрузка картинки на страницу
        /// </summary>
        void UploadImageWallPost()
        {
            for (int i = 0; i < Variable.dataGridView.Rows.Count-1; i++)
            {
                string test = Variable.dataGridView[0, i].Value.ToString();
               // string att = Variable.dataGridView[7, i].Value.ToString();
                if (test == "True")
                {
                    vk.wallPost(-group_Kuda, Variable.dataGridView[3, i].Value.ToString(),

                        Variable.dataGridView[7, i].Value.ToString());
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Thread[0].Resume();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread[0].Abort();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread[0].Suspend();
        }
        /// <summary>
        /// Добавить дату
        /// </summary>
        
        private void button8_Click(object sender, EventArgs e)
        {
            DateTime Dt = new DateTime(dateTimePicker1.Value.Year,
                                        dateTimePicker1.Value.Month,
                                        dateTimePicker1.Value.Day,
                                        dateTimePicker1.Value.TimeOfDay.Hours,
                                        dateTimePicker1.Value.TimeOfDay.Minutes,
                                        dateTimePicker1.Value.TimeOfDay.Seconds
                                                                 );
            var data = UnixTimeEf.UnixEncoding(Dt);

            // richTextBox1.Text = UnixTimeEf.UnixEncoding(Dt).ToString();

            try
            {
                dataGridView1[9, dataGridView1.CurrentRow.Index].Value = data.ToString();

                richTextBox1.Text = dataGridView1[9, dataGridView1.CurrentRow.Index].Value.ToString();
            }
            catch { }

        }

        /// <summary>
        /// Добавление в таблицу картинок
        /// </summary>
        void AddInTableImage()
        {
            
            var WallGet =  JsonConvert.DeserializeObject<Parse.wallGet>(vk.WallGet(-group_Otkuda));
            for (int i = 0; i < WallGet.response.items.Count(); i++)
               {
                
                string text = WallGet.response.items[i].text;           //Текст
                int Likes = WallGet.response.items[i].likes.count;      //Лайки
                int Repost = WallGet.response.items[i].reposts.count;   //Репосты
                int Comment = WallGet.response.items[i].comments.count; //Комментарии



                string ImageTableAdd = "",  //Данные фото
                    ImageTable= "";         //Первая картинка поста
                for (int j = 0; j < WallGet.response.items[i].attachments.Count(); j++)
                {
                    if (WallGet.response.items[i].attachments[j].type == "photo")
                    {
                        try
                        {
                            int owner_idImage = WallGet.response.items[i].attachments[j].photo.owner_id;                    //id откуда берем картинку
                            int idImage = WallGet.response.items[i].attachments[j].photo.id;                                //id картинки
                            if (j==0)//Если равно 0, то берем главную картинку
                                ImageTable = WallGet.response.items[i].attachments[0].photo.sizes[6].url;                     //Ссылка на картинку

                            ImageTableAdd = $"photo{owner_idImage}_{idImage}"+ImageTableAdd;                                //Добавление вложений в переменную
                            WebClient wc = new WebClient();
                            Image img = new Bitmap(wc.OpenRead(ImageTable));//Открытие картинки
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                dataGridView1.Rows.Add(false, -group_Otkuda, img, text, Likes, Comment, Repost, ImageTableAdd);
                            });//Добавление в таблицу
                            
                        }
                        catch
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                dataGridView1.Rows.Add(false, -group_Otkuda, null, text, Likes, Comment, Repost, "", "");
                            });
                            
                        }
                    }       //Проверка вложения на фото
                }           //Перебор вложений поста

                Send($"Добавил картинку и вывел пост{i}");
                 }
          //  richTextBox1.Text = wallGet.response.items[0].text;
          //  richTextBox1.Text = WallGet.response.items[0].attachments[0].photo.sizes[6].url;
        }




    }

    class Parse
    {

        public class GetById
        {
            public Response[] response { get; set; }

            public class Response
            {
                public int id { get; set; }
                public string name { get; set; }
                public string screen_name { get; set; }
                public int is_closed { get; set; }
                public string type { get; set; }
                public int is_admin { get; set; }
                public int is_member { get; set; }
                public int is_advertiser { get; set; }
                public string photo_50 { get; set; }
                public string photo_100 { get; set; }
                public string photo_200 { get; set; }
            }
        }


        public class wallGet
        {
            public Response response { get; set; }
            public class Response
            {
                public int count { get; set; }
                public Item[] items { get; set; }
                public string next_from { get; set; }
            }

            public class Item
            {
                public int id { get; set; }
                public int from_id { get; set; }
                public int owner_id { get; set; }
                public int date { get; set; }
                public string type { get; set; }
                public int marked_as_ads { get; set; }
                public string post_type { get; set; }
                public string text { get; set; }
                public int signer_id { get; set; }
                public int is_pinned { get; set; }
                public Attachment1[] attachments { get; set; }
                public Post_Source post_source { get; set; }
                public Comments comments { get; set; }
                public Likes likes { get; set; }
                public Reposts reposts { get; set; }
                public Views views { get; set; }
                public bool is_favorite { get; set; }
                public Ads_Easy_Promote ads_easy_promote { get; set; }
                public int edited { get; set; }
                public Activity activity { get; set; }
                public Copyright copyright { get; set; }
                public Copy_History[] copy_history { get; set; }
            }

            public class Post_Source
            {
                public string type { get; set; }
            }

            public class Comments
            {
                public int count { get; set; }
                public int can_post { get; set; }
                public bool groups_can_post { get; set; }
            }

            public class Likes
            {
                public int count { get; set; }
                public int user_likes { get; set; }
                public int can_like { get; set; }
                public int can_publish { get; set; }
            }

            public class Reposts
            {
                public int count { get; set; }
                public int user_reposted { get; set; }
            }

            public class Views
            {
                public int count { get; set; }
            }

            public class Ads_Easy_Promote
            {
                public int type { get; set; }
            }

            public class Activity
            {
                public string type { get; set; }
                public Comment[] comments { get; set; }
            }

            public class Comment
            {
                public int id { get; set; }
                public int from_id { get; set; }
                public int post_id { get; set; }
                public int owner_id { get; set; }
                public object[] parents_stack { get; set; }
                public int date { get; set; }
                public string text { get; set; }
                public Likes1 likes { get; set; }
                public Thread thread { get; set; }
                public Attachment[] attachments { get; set; }
            }

            public class Likes1
            {
                public int count { get; set; }
                public int user_likes { get; set; }
                public int can_like { get; set; }
            }

            public class Thread
            {
                public int count { get; set; }
            }

            public class Attachment
            {
                public string type { get; set; }
                public Photo photo { get; set; }
            }

            public class Photo
            {
                public int id { get; set; }
                public int album_id { get; set; }
                public int owner_id { get; set; }
                public int user_id { get; set; }
                public Size[] sizes { get; set; }
                public string text { get; set; }
                public int date { get; set; }
                public string access_key { get; set; }
            }

            public class Size
            {
                public string type { get; set; }
                public string url { get; set; }
                public int width { get; set; }
                public int height { get; set; }
            }

            public class Copyright
            {
                public int id { get; set; }
                public string link { get; set; }
                public string type { get; set; }
                public string name { get; set; }
            }

            public class Attachment1
            {
                public string type { get; set; }
                public Photo1 photo { get; set; }
                public Audio audio { get; set; }
                public Link link { get; set; }
                public Video video { get; set; }
                public Poll poll { get; set; }
                public Doc doc { get; set; }
                public Article article { get; set; }
            }

            public class Photo1
            {
                public int id { get; set; }
                public int album_id { get; set; }
                public int owner_id { get; set; }
                public int user_id { get; set; }
                public Size1[] sizes { get; set; }
                public string text { get; set; }
                public int date { get; set; }
                public string access_key { get; set; }
                public int post_id { get; set; }
            }

            public class Size1
            {
                public string type { get; set; }
                public string url { get; set; }
                public int width { get; set; }
                public int height { get; set; }
            }

            public class Audio
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public string artist { get; set; }
                public string title { get; set; }
                public int duration { get; set; }
                public int date { get; set; }
                public string url { get; set; }
                public int genre_id { get; set; }
                public bool is_licensed { get; set; }
                public bool is_hq { get; set; }
                public Ads ads { get; set; }
                public string access_key { get; set; }
                public string track_code { get; set; }
                public bool is_explicit { get; set; }
                public int album_id { get; set; }
                public int lyrics_id { get; set; }
                public Album album { get; set; }
                public Main_Artists[] main_artists { get; set; }
            }

            public class Ads
            {
                public int duration { get; set; }
                public string content_id { get; set; }
                public int puid22 { get; set; }
                public int puid1 { get; set; }
            }

            public class Album
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public string title { get; set; }
                public string access_key { get; set; }
                public Thumb thumb { get; set; }
            }

            public class Thumb
            {
                public string photo_34 { get; set; }
                public string photo_68 { get; set; }
                public string photo_135 { get; set; }
                public string photo_270 { get; set; }
                public string photo_300 { get; set; }
                public string photo_600 { get; set; }
                public int width { get; set; }
                public int height { get; set; }
            }

            public class Main_Artists
            {
                public string name { get; set; }
                public bool is_followed { get; set; }
                public bool can_follow { get; set; }
                public string id { get; set; }
                public string domain { get; set; }
            }

            public class Link
            {
                public string url { get; set; }
                public string title { get; set; }
                public string description { get; set; }
                public string target { get; set; }
                public bool is_favorite { get; set; }
                public Preview_Article preview_article { get; set; }
                public Amp amp { get; set; }
                public Photo3 photo { get; set; }
                public string caption { get; set; }
            }

            public class Preview_Article
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public string owner_name { get; set; }
                public string owner_photo { get; set; }
                public string state { get; set; }
                public bool can_report { get; set; }
                public string title { get; set; }
                public string subtitle { get; set; }
                public int views { get; set; }
                public int shares { get; set; }
                public bool is_favorite { get; set; }
                public string url { get; set; }
                public string view_url { get; set; }
                public string access_key { get; set; }
                public int published_date { get; set; }
                public Photo2 photo { get; set; }
            }

            public class Photo2
            {
                public int id { get; set; }
                public int album_id { get; set; }
                public int owner_id { get; set; }
                public Size2[] sizes { get; set; }
                public string text { get; set; }
                public int date { get; set; }
            }

            public class Size2
            {
                public string type { get; set; }
                public string url { get; set; }
                public int width { get; set; }
                public int height { get; set; }
            }

            public class Amp
            {
                public string url { get; set; }
                public int views { get; set; }
                public bool is_favorite { get; set; }
            }

            public class Photo3
            {
                public int id { get; set; }
                public int album_id { get; set; }
                public int owner_id { get; set; }
                public Size3[] sizes { get; set; }
                public string text { get; set; }
                public int date { get; set; }
            }

            public class Size3
            {
                public string type { get; set; }
                public string url { get; set; }
                public int width { get; set; }
                public int height { get; set; }
            }

            public class Video
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public string title { get; set; }
                public int duration { get; set; }
                public string description { get; set; }
                public int date { get; set; }
                public int comments { get; set; }
                public int views { get; set; }
                public int local_views { get; set; }
                public string photo_130 { get; set; }
                public string photo_320 { get; set; }
                public string photo_640 { get; set; }
                public string photo_800 { get; set; }
                public bool is_favorite { get; set; }
                public string access_key { get; set; }
                public int user_id { get; set; }
                public string platform { get; set; }
                public int can_add { get; set; }
                public string track_code { get; set; }
                public int width { get; set; }
                public int height { get; set; }
                public string photo_1280 { get; set; }
                public string first_frame_320 { get; set; }
                public string first_frame_160 { get; set; }
                public string first_frame_0 { get; set; }
                public string first_frame_130 { get; set; }
                public string first_frame_720 { get; set; }
                public string first_frame_1024 { get; set; }
                public string first_frame_1280 { get; set; }
                public string first_frame_800 { get; set; }
            }

            public class Poll
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public int created { get; set; }
                public string question { get; set; }
                public int votes { get; set; }
                public int answer_id { get; set; }
                public Answer[] answers { get; set; }
                public int anonymous { get; set; }
            }

            public class Answer
            {
                public int id { get; set; }
                public string text { get; set; }
                public int votes { get; set; }
                public float rate { get; set; }
            }

            public class Doc
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public string title { get; set; }
                public int size { get; set; }
                public string ext { get; set; }
                public string url { get; set; }
                public int date { get; set; }
                public int type { get; set; }
                public int is_licensed { get; set; }
                public string access_key { get; set; }
            }

            public class Article
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public string owner_name { get; set; }
                public string owner_photo { get; set; }
                public string state { get; set; }
                public bool can_report { get; set; }
                public string title { get; set; }
                public string subtitle { get; set; }
                public int views { get; set; }
                public int shares { get; set; }
                public bool is_favorite { get; set; }
                public string url { get; set; }
                public string view_url { get; set; }
                public string access_key { get; set; }
                public int published_date { get; set; }
                public Photo4 photo { get; set; }
            }

            public class Photo4
            {
                public int id { get; set; }
                public int album_id { get; set; }
                public int owner_id { get; set; }
                public int user_id { get; set; }
                public Size4[] sizes { get; set; }
                public string text { get; set; }
                public int date { get; set; }
            }

            public class Size4
            {
                public string type { get; set; }
                public string url { get; set; }
                public int width { get; set; }
                public int height { get; set; }
            }

            public class Copy_History
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public int from_id { get; set; }
                public int date { get; set; }
                public string post_type { get; set; }
                public string text { get; set; }
                public Attachment2[] attachments { get; set; }
                public Post_Source1 post_source { get; set; }
            }

            public class Post_Source1
            {
                public string type { get; set; }
            }

            public class Attachment2
            {
                public string type { get; set; }
                public Video1 video { get; set; }
            }

            public class Video1
            {
                public int id { get; set; }
                public int owner_id { get; set; }
                public string title { get; set; }
                public int duration { get; set; }
                public string description { get; set; }
                public int date { get; set; }
                public int comments { get; set; }
                public int views { get; set; }
                public int local_views { get; set; }
                public string photo_130 { get; set; }
                public string photo_320 { get; set; }
                public string photo_640 { get; set; }
                public string photo_800 { get; set; }
                public bool is_favorite { get; set; }
                public string access_key { get; set; }
                public string platform { get; set; }
                public int can_add { get; set; }
                public string track_code { get; set; }
            }

        }



    }
}
