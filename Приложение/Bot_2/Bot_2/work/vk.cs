using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Leaf.xNet;
namespace Bot_2.work
{
    class vk
    {
        public static string oAuth(string username, string password)
        {
            HttpRequest request = new HttpRequest();
            RequestParams Params = new RequestParams();
            Params["username"] = username;
            Params["password"] = password;
            Params["grant_type"] = "password";
            Params["client_id"] = 6769897;
            Params["client_secret"] = "kVBeEoVu11o8zh8FgO41";
            Params["v"] = Variables.V;
            string response = request.Get("http://oauth.vk.com/token", Params).ToString();

            return response;




        }
    }
}
