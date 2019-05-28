using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using Newtonsoft.Json;
namespace Bot_3
{
    class AuthToken
    { /// <summary>
      /// логин
      /// </summary>
        private string Login { get; set; }
        /// <summary>
        /// пароль
        /// </summary>
        private string Password { get; set; }
        /// <summary>
        /// Результат пользователя
        /// </summary>
        public static result Result { get; set; }
        public class result
        {
            /// <summary>
            /// токен
            /// </summary>
            public string access_token { get; set; }
            /// <summary>
            /// время жизни токена
            /// </summary>
            public int expires_in { get; set; }
            /// <summary>
            /// id пользователя
            /// </summary>
            public int user_id { get; set; }
        }

        /// <summary>
        /// Передача данных для токена
        /// </summary>
        public AuthToken(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
        }

        public bool Auth()
        {
            HttpRequest request = new HttpRequest();
            string response = "", error = "";
            try { response = request.Post("https://oauth.vk.com/token?grant_type=password&client_id=2274003&client_secret=hHbZxrka2uZ6jB1inYsH&username=" + Login + "&password=" + Password).ToString(); }
            catch (HttpException ex) { error = ex.HttpStatusCode.ToString(); }

            if (error == "Unauthorized")
                return false;
            else
            {
                Result = JsonConvert.DeserializeObject<result>(response);
                return true;
            }
        }
    }
}
