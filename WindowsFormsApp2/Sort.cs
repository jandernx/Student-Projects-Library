using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    class Sort
    {
        public static Array id = Array.CreateInstance(typeof(int), 40);
        public static Array author = Array.CreateInstance(typeof(string), 40);
        public static Array topic = Array.CreateInstance(typeof(string), 40);
        public static Array description = Array.CreateInstance(typeof(string), 40);
        public static Array Link_PDF = Array.CreateInstance(typeof(string), 40);
        public static Array Link_pic = Array.CreateInstance(typeof(string), 40);
        public static Array likes = Array.CreateInstance(typeof(int), 40);
        public static Array views = Array.CreateInstance(typeof(int), 40);
        public static Array id_prjct = Array.CreateInstance(typeof(int), 40);
        public static Array Username = Array.CreateInstance(typeof(string), 40);

        public static Array topic_copy = Array.CreateInstance(typeof(string), 40);
        public static Array likes_copy = Array.CreateInstance(typeof(int), 40);

        public static Array topicOnButtons = Array.CreateInstance(typeof(string), 40);
        public static Array authorOnButtons = Array.CreateInstance(typeof(string), 40);
        public static Array descriptionOnButtons = Array.CreateInstance(typeof(string), 40);
        public static Array Link_PDFOnButtons = Array.CreateInstance(typeof(string), 40);
        public static Array Link_picOnButtons = Array.CreateInstance(typeof(string), 40);
        public static Array likesOnButtons = Array.CreateInstance(typeof(int), 40);
        public static Array viewsOnButtons = Array.CreateInstance(typeof(int), 40);
        public static Array idOnButtons = Array.CreateInstance(typeof(int), 40);

        public static int number_of_rows_user_projects = 0;


        public static SqlConnection db = new SqlConnection(@"Data Source=EUGENE;Initial Catalog=simple;Integrated Security=True");

        public static void getElements()
        {
            for (int i = 0; i < likes.Length; i++)
            {
                likes.SetValue(-1000, i);
            }
            if (db.State == ConnectionState.Closed) db.Open();
            SqlCommand cmd_number_of_rows_users = new SqlCommand("SELECT MAX(ID) FROM projects", db);
            number_of_rows_user_projects = Convert.ToInt32(cmd_number_of_rows_users.ExecuteScalar());
            for (int entry = 1; entry < number_of_rows_user_projects + 1; entry++)
            {
                String str = @"select ID from projects where ID = @entry";
                SqlCommand cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                //var temp = (cmd.ExecuteScalar()).ToString();
                try { id.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry); }
                catch { topic.SetValue(String.Empty, entry); continue; }



                str = "select Author from projects where ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                //var temp = (cmd.ExecuteScalar()).ToString();
                author.SetValue(cmd.ExecuteScalar().ToString(), entry);

                str = "select Topic from projects where ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                //temp = (cmd.ExecuteScalar()).ToString();
                topic.SetValue(cmd.ExecuteScalar().ToString(), entry);

                str = "select Description from projects where ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                //temp = (cmd.ExecuteScalar()).ToString();
                description.SetValue(cmd.ExecuteScalar().ToString(), entry);

                str = "select Link_PDF from projects where ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                var temp = (cmd.ExecuteScalar()).ToString();
                try { Link_PDF.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry); }
                catch { }

                str = "select Link_pic from projects where ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                temp = (cmd.ExecuteScalar()).ToString();
                try { Link_pic.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry); }
                catch { }


                str = "select Likes_count from projects where ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                //temp = (cmd.ExecuteScalar()).ToString();
                likes.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry);

                str = "select Views from projects where ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                //temp = (cmd.ExecuteScalar()).ToString();
                views.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry);

                str = "select Username from projects where ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                temp = (cmd.ExecuteScalar()).ToString();
                try { Username.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry); }
                catch { }

            }
            db.Close();
        }

        public static void sort(String s)
        {
            int j = 0;


            if (s == "Alphabetical")
            {
                for (int i = 0; i < topic.Length; i++)
                {
                    topic_copy.SetValue(topic.GetValue(i), i);
                }

                Array.Sort(topic_copy);

                for (int i = 0; i < topic.Length; i++)
                {
                    for (int k = 0; k < topic_copy.Length; k++)
                    {
                        if (topic_copy.GetValue(k) != null && topic_copy.GetValue(k) != String.Empty && topic.GetValue(i) != null && topic.GetValue(i) != String.Empty && topic_copy.GetValue(k) == topic.GetValue(i))
                        {
                            author.SetValue(author.GetValue(i), k);
                            description.SetValue(description.GetValue(i), k);
                            Link_PDF.SetValue(Link_PDF.GetValue(i), k);
                            Link_pic.SetValue(Link_pic.GetValue(i), k);
                            likes.SetValue(likes.GetValue(i), k);
                            views.SetValue(views.GetValue(i), k);
                            Username.SetValue(Username.GetValue(i), k);
                            id.SetValue(id.GetValue(i), k);
                        }
                    }
                }
                Array.Sort(topic);
                for (int i = 1; i < topic.Length; i++)
                {
                    if (topic.GetValue(i) == null || topic.GetValue(i) == String.Empty)
                        continue;
                    topicOnButtons.SetValue(topic.GetValue(i), j);
                    authorOnButtons.SetValue(author.GetValue(i), j); ;
                    descriptionOnButtons.SetValue(description.GetValue(i), j);
                    Link_PDFOnButtons.SetValue(Link_PDF.GetValue(i), j);
                    Link_picOnButtons.SetValue(Link_pic.GetValue(i), j);
                    likesOnButtons.SetValue(likes.GetValue(i), j);
                    viewsOnButtons.SetValue(views.GetValue(i), j);
                    idOnButtons.SetValue(id.GetValue(i), j);
                    j++;
                }
                for (int i = 0; i < topic.Length; i++)
                {
                    topic.SetValue(null, i);
                }
            }

            if (s == "Rating")
            {
                for (int i = 0; i < likes.Length; i++)
                {
                    likes_copy.SetValue(likes.GetValue(i), i);
                }

                Array.Sort(likes_copy);
                Array.Reverse(likes_copy);

                for (int i = 0; i < likes.Length; i++)
                {
                    if (topic.GetValue(i) == "")
                        topic.SetValue(null, i);
                }

                for (int i = 0; i < likes_copy.Length; i++)
                {
                    if (Convert.ToInt32(likes_copy.GetValue(i)) == -1000) break;
                    for (int l = 0; l < likes.Length; l++)
                    {
                        if (Convert.ToInt32(likes_copy.GetValue(i)) == Convert.ToInt32(likes.GetValue(l)) && Convert.ToInt32(likes.GetValue(l)) != -1000 && Convert.ToInt32(likes_copy.GetValue(i)) != -1000)
                        {
                            likes.SetValue(-1000, l);
                            topicOnButtons.SetValue(topic.GetValue(l), i);
                            viewsOnButtons.SetValue(views.GetValue(l), i);
                            authorOnButtons.SetValue(author.GetValue(l), i); ;
                            descriptionOnButtons.SetValue(description.GetValue(l), i);
                            Link_PDFOnButtons.SetValue(Link_PDF.GetValue(l), i);
                            Link_picOnButtons.SetValue(Link_pic.GetValue(l), i);
                            likesOnButtons.SetValue(likes_copy.GetValue(i), i);
                            idOnButtons.SetValue(id.GetValue(l), i);
                            break;

                            //description.SetValue(description.GetValue(l), i);
                            //topic.SetValue(topic.GetValue(l), i);
                            //topic.SetValue(topic.GetValue(l), i);

                            //topic.SetValue(null, l);
                            //break;
                        }
                    }

                }
            }


        }
        public static void find(String text, String typeOfSorting)
        {
            for (int i = 0; i < author.Length; i++)
            {
                author.SetValue(null, i);
                topic.SetValue(null, i);
                description.SetValue(null, i);
                likes.SetValue(null, i);
                Link_PDF.SetValue(null, i);
                Link_pic.SetValue(null, i);
                views.SetValue(null, i);
            }

            if (db.State == ConnectionState.Closed) db.Open();
            SqlCommand cmd_number_of_rows_users = new SqlCommand("SELECT MAX(ID) FROM projects", db);
            number_of_rows_user_projects = Convert.ToInt32(cmd_number_of_rows_users.ExecuteScalar());
            for (int entry = 1; entry < number_of_rows_user_projects + 1; entry++)
            {
                bool b = false;
                String str = "select Author from projects where Author like '%" + text + "%' and ID = @entry";
                SqlCommand cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                if (author.GetValue(entry) != null) continue;
                try { author.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                catch { }
                if (author.GetValue(entry) != null) b = true;

                str = "select Topic from projects where Author like '%" + text + "%' and ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                try { topic.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                catch { }

                str = "select Description from projects where Author like '%" + text + "%' and ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                try { description.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                catch { }

                str = "select Likes_count from projects where Author like '%" + text + "%' and ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                try { likes.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry); }
                catch { }

                str = "select Link_PDF from projects where Author like '%" + text + "%' and ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                try { Link_PDF.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                catch { }

                str = "select Link_pic from projects where Author like '%" + text + "%' and ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                try { Link_pic.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                catch { }

                str = "select Views from projects where Author like '%" + text + "%' and ID = @entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("entry", entry));
                try { views.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry); }
                catch { }

                if (b == true)
                    continue;
                else
                {
                    b = false;
                    str = "select Author from projects where Topic like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { author.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }
                    if (author.GetValue(entry) != null) b = true;

                    str = "select Topic from projects where Topic like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { topic.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }

                    str = "select Description from projects where Topic like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { description.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }

                    str = "select Likes_count from projects where Topic like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { likes.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry); }
                    catch { }

                    str = "select Link_PDF from projects where Topic like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { Link_PDF.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }

                    str = "select Link_pic from projects where Topic like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { Link_pic.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }

                    str = "select Views from projects where Topic like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { views.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry); }
                    catch { }


                }

                if (b == true)
                    continue;
                else
                {
                    b = false;
                    str = "select Author from projects where Description like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { author.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }
                    b = true;

                    str = "select Topic from projects where Description like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { topic.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }

                    str = "select Description from projects where Description like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { description.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }

                    str = "select Likes_count from projects where Description like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { likes.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry); }
                    catch { }

                    str = "select Link_PDF from projects where  Description '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { Link_PDF.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }

                    str = "select Link_pic from projects where Description like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { Link_pic.SetValue(cmd.ExecuteScalar().ToString(), entry); }
                    catch { }

                    str = "select Views from projects where Description like '%" + text + "%' and ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    try { views.SetValue(Convert.ToInt32(cmd.ExecuteScalar().ToString()), entry); }
                    catch { }
                }
            }
            
            if (typeOfSorting == "Alphabetical")
            {
                for (int i = 0; i < topic.Length; i++)
                {
                    topic_copy.SetValue(topic.GetValue(i), i);
                }
                Array.Sort(topic_copy);

                Array.Reverse(topic_copy);
                int j = 0;
                for (int i = 0; i < topic.Length; i++)
                {
                    for (int k = 0; k < topic_copy.Length; k++)
                    {
                        if (topic_copy.GetValue(k) != null && topic_copy.GetValue(k) != String.Empty && topic.GetValue(i) != null && topic.GetValue(i) != String.Empty && topic_copy.GetValue(k) == topic.GetValue(i))
                        {
                            author.SetValue(author.GetValue(i), k);
                            description.SetValue(description.GetValue(i), k);
                            Link_PDF.SetValue(Link_PDF.GetValue(i), k);
                            Link_pic.SetValue(Link_pic.GetValue(i), k);
                            likes.SetValue(likes.GetValue(i), k);
                            views.SetValue(views.GetValue(i), k);
                            Username.SetValue(Username.GetValue(i), k);
                            id.SetValue(id.GetValue(i), k);
                        }
                    }
                }
                Array.Sort(topic);
                Array.Reverse(topic);
                int p = 0;
                for (int i = 0; i < topic.Length; i++)
                {
                    //if (topic.GetValue(i) == null || topic.GetValue(i) == String.Empty)
                    //    break;
                    
                    if (topicOnButtons.GetValue(i) != null) { j++; continue; }
                    topicOnButtons.SetValue(topic.GetValue(p), j);
                    authorOnButtons.SetValue(author.GetValue(p), j); ;
                    descriptionOnButtons.SetValue(description.GetValue(p), j);
                    Link_PDFOnButtons.SetValue(Link_PDF.GetValue(p), j);
                    Link_picOnButtons.SetValue(Link_pic.GetValue(p), j);
                    likesOnButtons.SetValue(likes.GetValue(p), j);
                    viewsOnButtons.SetValue(views.GetValue(p), j);
                    idOnButtons.SetValue(id.GetValue(p), j);
                    p++;
                    j++;
                }
            }

            else
            {
                Array likes_copy = Array.CreateInstance(typeof(int), 40);
                for (int i = 0; i < likes.Length; i++)
                {
                    likes_copy.SetValue(likes.GetValue(i), i);
                }

                Array.Sort(likes_copy);
                Array.Reverse(likes_copy);

                for (int i = 0; i < likes.Length; i++)
                {
                    if (topic.GetValue(i) == "")
                        topic.SetValue(null, i);
                }

                for (int i = 0; i < likes_copy.Length; i++)
                {
                    if (Convert.ToInt32(likes_copy.GetValue(i)) == -1000) break;
                    for (int l = 0; l < likes.Length; l++)
                    {
                        if (Convert.ToInt32(likes_copy.GetValue(i)) == Convert.ToInt32(likes.GetValue(l)) && Convert.ToInt32(likes.GetValue(l)) != -1000 && Convert.ToInt32(likes_copy.GetValue(i)) != -1000)
                        {
                            if (topicOnButtons.GetValue(i) != null) continue;
                            likes.SetValue(-1000, l);
                            topicOnButtons.SetValue(topic.GetValue(l), i);
                            viewsOnButtons.SetValue(views.GetValue(l), i);
                            authorOnButtons.SetValue(author.GetValue(l), i); ;
                            descriptionOnButtons.SetValue(description.GetValue(l), i);
                            Link_PDFOnButtons.SetValue(Link_PDF.GetValue(l), i);
                            Link_picOnButtons.SetValue(Link_pic.GetValue(l), i);
                            likesOnButtons.SetValue(likes_copy.GetValue(i), i);
                            idOnButtons.SetValue(id.GetValue(l), i);
                            break;
                        }
                    }
                }
            }

        }
    }
}
