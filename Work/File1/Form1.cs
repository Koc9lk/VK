using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Specialized;

namespace File
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int appId = 3734446;
        private enum VkontakteScopeList
        {
            notify = 1,
            friends = 2,        
            wall = 8192,
  
        }
        private int scope = (int)(VkontakteScopeList.friends  | VkontakteScopeList.wall | VkontakteScopeList.notify);

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(String.Format("http://api.vkontakte.ru/oauth/authorize?client_id={0}&scope={1}&display=popup&response_type=token", appId, scope));
           
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().IndexOf("access_token") != -1)
            {
                string accessToken = "";
                int userId = 0;
                string[] parts = e.Url.AbsoluteUri.Split('#');

                Regex myReg = new Regex(@"(?<name>[\w\d\x5f]+)=(?<value>[^\x26\s]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match m in myReg.Matches(e.Url.ToString()))
                {

                    if (m.Groups["name"].Value == "access_token")
                    {
                        accessToken = m.Groups["value"].Value;
                    }

                    else if (m.Groups["name"].Value == "user_id")
                    {
                        userId = Convert.ToInt32(m.Groups["value"].Value);
                    }
                }
         
                Program.API = new file(accessToken);
                Program.API = new file(userId, accessToken);
                Form2 main = new Form2();
                this.Hide();
                main.Show();
            }
        }
    }
   }
