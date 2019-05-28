using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNetStandard;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bot_3
{
    class vk
    {
        /// <summary>
        /// Однофакторная авторизация
        /// </summary>
        
        /// <returns></returns>
        public static string oAuth(string username, string password)
        {
            HttpRequest request = new HttpRequest();
            RequestParams Params = new RequestParams();
            Params["username"] = username;
            Params["password"] = password;
            Params["grant_type"] = "password";
            Params["client_id"] = 6769897;
            Params["client_secret"] = "kVBeEoVu11o8zh8FgO41";
            Params["v"] = Variable.V;
            string response = request.Get("https://oauth.vk.com/token", Params).ToString();
            return response;
            




        }
    }
}
