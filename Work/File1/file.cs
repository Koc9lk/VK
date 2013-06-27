using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Xml;
using System.Threading;
using System.Collections;

namespace File
{
    class file
    {
        public int UserId = 0;
        public string AccessToken = "";
            public file(string accessToken)
        {
            this.AccessToken = accessToken;
            accessToken = AccessToken;
        }
            public file(int userId, string accessToken)
        {
            this.UserId = userId;
            this.AccessToken = accessToken;
        }
        private XmlDocument ExecuteCommand(string name, NameValueCollection qs)
        {
            XmlDocument result = new XmlDocument();
            result.Load(String.Format("https://api.vkontakte.ru/method/{0}.xml?access_token={1}&{2}", name, AccessToken, String.Join("&", from item in qs.AllKeys select item + "=" + qs[item])));
            return result;
        }
        public XmlDocument GetProfile(int uid)
        {
            NameValueCollection qs = new NameValueCollection();
            qs["uid"] = uid.ToString();
            qs["fields"] = "uid,first_name,last_name,nickname,domain,sex,bdate," +
            "city,country,timezone,photo,has_mobile,rate,contacts,education,online";
            return ExecuteCommand("getProfiles", qs);
        }
        public XmlDocument GetFriends(int uid)
        {
            NameValueCollection qs = new NameValueCollection();
            qs["uid"] = uid.ToString();
            qs["fields"] = "uid,first_name,last_name,nickname,domain,sex,bdate,city,country,timezone,photo,has_mobile,rate,contacts,education,online";
            return ExecuteCommand("friends.get", qs);
        }
        public bool WallPost(int uid, string message)
        {
            NameValueCollection qs = new NameValueCollection();
            qs["owner_id"] = uid.ToString();
            qs["message"] = message;
            ExecuteCommand("wall.post", qs);
            return true;
        } 
    }
}
