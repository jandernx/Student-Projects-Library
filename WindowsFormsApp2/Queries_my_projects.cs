using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    class Queries_my_projects
    {
        public static Array id = Array.CreateInstance(typeof(int), 40);
        public static Array author = Array.CreateInstance(typeof(string), 40);
        public static Array topic = Array.CreateInstance(typeof(string), 40);
        public static Array description = Array.CreateInstance(typeof(string), 40);
        public static Array Link_PDF = Array.CreateInstance(typeof(string), 40);
        public static Array Link_pic = Array.CreateInstance(typeof(string), 40);
        public static Array likes = Array.CreateInstance(typeof(int), 40);
        public static Array id_prjct = Array.CreateInstance(typeof(int), 40);
        public static Array author_prjct = Array.CreateInstance(typeof(string), 40);
        public static Array topic_prjct = Array.CreateInstance(typeof(string), 40);
        public static Array description_prjct = Array.CreateInstance(typeof(string), 40);
        public static Array Link_PDF_prjct = Array.CreateInstance(typeof(string), 40);
        public static Array Link_pic_prjct = Array.CreateInstance(typeof(string), 40);
        public static Array likes_prjct = Array.CreateInstance(typeof(int), 40);
        public static int number_of_rows_user_projects = 0;
        public static Array Username = Array.CreateInstance(typeof(string), 40);
        public static int last_row = 0;

        public static SqlConnection db = new SqlConnection(@"Data Source=EUGENE;Initial Catalog=simple;Integrated Security=True");
        public static void user_projects()
        {
            if (db.State == ConnectionState.Closed) db.Open();
            SqlCommand cmd_number_of_rows_users = new SqlCommand("SELECT MAX(ID) FROM projects", db);
            number_of_rows_user_projects = Convert.ToInt32(cmd_number_of_rows_users.ExecuteScalar());
            for (int entry = 1; entry < number_of_rows_user_projects+1; entry++)
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

                    str = "select Username from projects where ID = @entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("entry", entry));
                    temp = (cmd.ExecuteScalar()).ToString();
                    try { Username.SetValue(temp.Remove(temp.IndexOf(' '),temp.Length - temp.IndexOf(' ')), entry); }
                    catch { }
            }
        }

        public static void dannye()
        {
            for (int i = 1; i < number_of_rows_user_projects + 1; i++)
            {
                Console.WriteLine(id.GetValue(i));
                Console.WriteLine(author.GetValue(i));
                Console.WriteLine(topic.GetValue(i));
                Console.WriteLine(description.GetValue(i));
                Console.WriteLine(Link_PDF.GetValue(i));
                Console.WriteLine(likes.GetValue(i));
                Console.WriteLine(Username.GetValue(i));
            }
            
        }
        public static void get_projects(String username)
        {
            int end = number_of_rows_user_projects + 1;
            int j = -1;
            for (int i = 1; i < end; i++)
            {
                topic_prjct.SetValue(null, i);
            }
            for (int i = 1; i < end ; i++)
            {
                if (username == Convert.ToString(Username.GetValue(i)))
                {
                    j++;
                    id_prjct.SetValue(id.GetValue(i), j);
                    author_prjct.SetValue(author.GetValue(i), j);
                    topic_prjct.SetValue(topic.GetValue(i), j);
                    if (topic_prjct.GetValue(j) == String.Empty)
                    {
                        j--;
                        continue;
                    }
                    description_prjct.SetValue(description.GetValue(i), j);
                    Link_PDF_prjct.SetValue(Link_PDF.GetValue(i), j);
                    Link_pic_prjct.SetValue(Link_pic.GetValue(i), j);
                    likes_prjct.SetValue(likes.GetValue(i), j);
                    last_row = j;
                }
                
            }
        }
    }
}
