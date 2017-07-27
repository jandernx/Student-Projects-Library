using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{

    class User_projectTable
    {
        private static List<int> id = new List<int>();
        public static int[] project = new int[Sort.topicOnButtons.Length];
        public static int[] projectw = new int[Sort.topicOnButtons.Length];

        public int[,] liked = new int[20, 20];
        public static Array topicO = Array.CreateInstance(typeof(string), 40);
        private int[,] viewed = new int[20, 20];
        private static SqlConnection db = new SqlConnection(@"Data Source=EUGENE;Initial Catalog=simple;Integrated Security=True");
        public static int number_of_rows_user_projects = 0;


        public static void getElements(Array topicOnButtons, String s)
        {
            for (int i = 0; i < topicOnButtons.Length; i++)
            {
                topicO.SetValue(topicOnButtons.GetValue(i), i);
            }
            for (int i = 0; i < topicOnButtons.Length; i++)
            {
                try
                {
                    var temp = topicOnButtons.GetValue(i).ToString();
                    topicO.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), i);
                }
                catch { break; }
            }
            for (int i = 0; i < project.Length; i++)
            {
                project[i] = 0;
                projectw[i] = 0;
            }
            if (db.State == ConnectionState.Closed) db.Open();
            SqlCommand cmd_number_of_rows_users = new SqlCommand("SELECT MAX(ID) FROM user_project", db);
            number_of_rows_user_projects = Convert.ToInt32(cmd_number_of_rows_users.ExecuteScalar());
            //for (int m = 1; m < number_of_rows_user_projects + 1; m++)
            //{
                for (int i = 0; i < topicOnButtons.Length; i++)
                {
                    String str = @"select Liked" + topicO.GetValue(i) + " from user_project where Username like '%"+s+"%';";
                    SqlCommand cmd = new SqlCommand(str, db);
                    try { project[i] = Convert.ToInt32(cmd.ExecuteScalar()); }
                    catch { }

                    str = @"select Viewed" + topicO.GetValue(i) + " from user_project where Username like '%" + s + "%';";
                    cmd = new SqlCommand(str, db);
                    try { projectw[i] = Convert.ToInt32(cmd.ExecuteScalar()); }
                    catch { }
                }
            //}
        }

        public static void sendNew(String s)
        {
            if (db.State == ConnectionState.Closed) db.Open();
            SqlCommand cmd_number_of_rows_users = new SqlCommand("SELECT MAX(ID) FROM projects", db);
            number_of_rows_user_projects = Convert.ToInt32(cmd_number_of_rows_users.ExecuteScalar());
            //for (int m = 1; m < number_of_rows_user_projects + 1; m++)
            //{
                for (int i = 0; i < Sort.topicOnButtons.Length; i++)
                {
                    try
                    {
                        String str = "update user_project set Liked" + @topicO.GetValue(i) + " = " + project[i] + " where Username like '%" + s + "%';";
                    SqlCommand cmd = new SqlCommand(str, db);
                        cmd.ExecuteNonQuery();
                    }
                    catch { break; }

                    try
                    {
                        String str = "update user_project set Viewed" + @topicO.GetValue(i) + " = " + projectw[i] + " where Username like '%" + s + "%';";
                    SqlCommand cmd = new SqlCommand(str, db);
                        cmd.ExecuteNonQuery();
                    }
                    catch { break; }
                }

            //}
        }
    }
}
