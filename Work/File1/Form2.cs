using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace File
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            treeView1.Nodes.Add("В Сети");
            treeView1.Nodes.Add("Не в Сети");
            timer1.Start();
        }
        string accessToken = Program.API.AccessToken;
        int userId = Program.API.UserId;
        XmlDocument profile;
        XmlDocument fr;
    

        private void Form2_Load(object sender, EventArgs e)
        {
            file myVK = new file(accessToken);
            profile = myVK.GetProfile(userId);
            fr = myVK.GetFriends(userId);
          
            string first_name = profile["response"]["user"]["first_name"].InnerText;
            int online = Int32.Parse(profile["response"]["user"]["online"].InnerText); 
            int col = fr["response"].ChildNodes.Count; 
            
            if (online == 1)
                label1.Text = ("Онлайн");
            else
                label1.Text = ("Не в Сети");

            

            foreach (XmlNode friend in fr["response"].ChildNodes) 
            {
                int fri = Int32.Parse(friend["online"].InnerText);
                if (fri == 1)
                {
                    treeView1.Nodes[0].Nodes.Add(friend["first_name"].InnerText + friend["last_name"].InnerText);
                }
                else
                {
                    treeView1.Nodes[1].Nodes.Add(friend["first_name"].InnerText + friend["last_name"].InnerText);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Требуется указать текст сообщения для публикации на стене.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                Program.API.WallPost(this.userId, textBox1.Text);
                MessageBox.Show("Запись успешно опубликована на стене пользователя!\r\n\r\n.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }
    }

