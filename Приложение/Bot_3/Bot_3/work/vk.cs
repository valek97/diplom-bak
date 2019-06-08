using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using xNetStandard;

namespace Bot_3
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
            Params["v"] = Variable.V;
            
            string response = AuthToken.Result.access_token;
            return response;




        }



        /// <summary>
        /// Получить значение со страницы
        /// </summary>
        /// <param name="owner_id">id страницы</param>
        /// <param name="extended">возвращать полученное значение</param>
        /// <param name="offset">Смещение</param>
        /// <param name="count">Получить нужное кол-во</param>
        /// <returns></returns>
        public static string WallGet(int owner_id, bool extended = false, int offset = 0, int count = 100)
        {
            HttpRequest request = new HttpRequest();
            RequestParams Params = new RequestParams();

            Params["owner_id"] =  owner_id;
            Params["extended"] = extended.GetHashCode();

            Params["offset"] = offset;
            Params["count"] = count;
            Params["access_token"] = AuthToken.Result.access_token;
            Params["v"] = Variable.V;
            string response = request.Get("https://api.vk.com/method/" + "wall.get", Params).ToString();
            return response;


        }

        /// <summary>
        /// Сделать пост в группе
        /// </summary>
        /// <param name="owner_id">id страницы</param>
        /// <param name="message">сообщение поста</param>
        /// <param name="attachements">вложение поста</param>
        /// <param name="close_comments">открыты или закрыты комментарии</param>
        /// <returns></returns>

        public static string wallPost(int owner_id, string message, string attachements = null, string publish_date = null, bool close_comments = false)
        {
            HttpRequest request = new HttpRequest();
            RequestParams Params = new RequestParams();
            Params["owner_id"] = owner_id;
           
            Params["message"] = message;

            Params["attachements"] = attachements;
            Params["publish_date"] = publish_date;
            Params["close_comments"] = close_comments.GetHashCode();
            Params["access_token"] = AuthToken.Result.access_token;
            Params["v"] = Variable.V;
            string response = request.Get("https://api.vk.com/method/" + "wall.post", Params).ToString();
            return response;



        }


        /// <summary>
        /// Возвращает список сообществ пользователя
        /// </summary>
        /// <param name="user_id">Идентификатор пользователя, информация о сообществах которую требуется получить</param>
        /// <param name="extended">полная инфа о группах пользователя = 1</param>
        /// <param name="offset">смещение подмножества сообществ</param>
        /// <param name="count">кол сообществ информацию о которых нужно вернуть</param>
        /// <returns></returns>
        public static string groupeGet(int user_id, bool extended = true, int offset = 0, int count = 100)

        {

            HttpRequest request = new HttpRequest();
            RequestParams Params = new RequestParams();
            Params["user_id"] = user_id;
            Params["extended"] = extended.GetHashCode();
            Params["offset"] = offset;
            Params["count"] = count;
            Params["access_token"] = AuthToken.Result.access_token;
            Params["v"] = Variable.V;
            string response = request.Get("https://api.vk.com/method/" + "groupe.post", Params).ToString();
            return response;


        }



        /// <summary>
        /// Возвращает информацию о заданном сообществе или нескольких сообществах
        /// </summary>
        /// <param name="group_ids">идентификаторы или короткие имена сообществ</param>
        /// <param name="fields">список дополнительных полей, которые необходимо вернуть. </param>
        /// <returns></returns>
        public static string groupeGetById(int group_ids, string fields = null)

        {

            HttpRequest request = new HttpRequest();
            RequestParams Params = new RequestParams();
            Params["group_ids"] = group_ids;
            Params["fields"] = fields;

            Params["access_token"] = AuthToken.Result.access_token;
            Params["v"] = Variable.V;
            string response = request.Get("https://api.vk.com/method/" + "groups.getById", Params).ToString();
            return response;


        }

        public static string photoGetWallUploadServer(int group_id)

        {

            HttpRequest request = new HttpRequest();
            RequestParams Params = new RequestParams();
            Params["group_id"] = group_id.ToString().Replace("-", ""); ;

            Params["access_token"] = AuthToken.Result.access_token;
            Params["v"] = Variable.V;
            string response = request.Get("https://api.vk.com/method/" + "photos.getWallUploadServer", Params).ToString();
            return response;


        }

        public static string uploadPhoto(string PathPhoto, string upload_url)

        {

            HttpRequest request = new HttpRequest();
            request.AddFile("photo", PathPhoto, PathPhoto);
            string response = request.Post(upload_url).ToString();
            return response;


        }


        public void SavePhoto(string LinkPhoto, string PathSave = "")

        {

            HttpRequest request = new HttpRequest();
            request.Get(LinkPhoto).ToFile(PathSave);


        }


        public static string SaveWallPhoto(int owner_id, string photo, int server, string hash, string access_token,
            string caption = null, string latitude = null, string longitude = null)
        {
            string owner = "user_id";
            if (owner_id.ToString().Contains("-"))
                owner = "groupe_id";
            HttpRequest request = new HttpRequest();
            RequestParams Params = new RequestParams();
            Params[owner] = owner_id.ToString().Replace("-", "");
            Params["photo"] = photo.Replace("\\", "");
            Params["server"] = server;
            Params["hash"] = hash;
            Params["caption"] = caption;
            Params["latitude"] = latitude;
            Params["access_token"] = AuthToken.Result.access_token;
            Params["v"] = Variable.V;
            string response = request.Get("https://api.vk.com/method/" + "photos.saveWallPhoto", Params).ToString();
            return response;

        }

    }
}
