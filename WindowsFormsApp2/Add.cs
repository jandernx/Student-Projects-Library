using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    class Add
    {
        public static SqlConnection db = new SqlConnection(@"Data Source=EUGENE;Initial Catalog=simple;Integrated Security=True");
        public static void add_project(int i, String author, String topic, String description, String link_PDF, String link_pic, String username)
        {
            if (db.State == ConnectionState.Closed) db.Open();
            String str = @"insert into projects values(@id, @author, @topic, @description, @Link_PDF, @Link_pic, 0, @username, 0);";
            SqlCommand cmd = new SqlCommand(str, db);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@id", SqlDbType.Int) {Value = i},
                new SqlParameter("@author", SqlDbType.Text) {Value = author},
                new SqlParameter("@topic", SqlDbType.Text) {Value = topic},
                new SqlParameter("@description", SqlDbType.Text) {Value = description},
                new SqlParameter("@Link_PDF", SqlDbType.Text) {Value = link_PDF},
                new SqlParameter("@Link_pic", SqlDbType.Text) {Value = link_pic},
                new SqlParameter("@username", SqlDbType.Text) {Value = username},
            };
            cmd.Parameters.AddRange(prm.ToArray());
            cmd.ExecuteNonQuery();

            if (topic.IndexOf(' ') == -1)
            {

                str = "ALTER TABLE [user_project] ADD [Liked" + topic + "] int default 0 NOT NULL ;";
                cmd = new SqlCommand(str, db);

                cmd.ExecuteNonQuery();

                str = "ALTER TABLE [user_project] ADD [Viewed" + topic + "] int default 0 NOT NULL ;";
                cmd = new SqlCommand(str, db);
                cmd.ExecuteNonQuery();

            }
            else
            {
                var topicc = topic.Remove(topic.IndexOf(' '), topic.Length - topic.IndexOf(' '));
                str = "ALTER TABLE [user_project] ADD [Liked" + topicc + "] int default 0 NOT NULL ;";
                cmd = new SqlCommand(str, db);

                cmd.ExecuteNonQuery();

                str = "ALTER TABLE [user_project] ADD [Viewed" + topicc + "] int default 0 NOT NULL ;";
                cmd = new SqlCommand(str, db);
                cmd.ExecuteNonQuery();
            }


        }
    }
}
