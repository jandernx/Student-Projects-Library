using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    class queries_users
    {
        public static Array username = Array.CreateInstance(typeof(string), 40);
        public static Array first_name = Array.CreateInstance(typeof(string), 40);
        public static Array last_name = Array.CreateInstance(typeof(string), 40);
        public static Array password = Array.CreateInstance(typeof(string), 40);
        public static Array mail = Array.CreateInstance(typeof(string), 40);
        public static Array sex = Array.CreateInstance(typeof(bool), 40);
        public static Array sex_str = Array.CreateInstance(typeof(string), 40);
        public static int number_of_rows_users = 0;
        public static SqlConnection db = new SqlConnection(@"Data Source=EUGENE;Initial Catalog=simple;Integrated Security=True");
        public static void get_info_users()
        {
            if (db.State == ConnectionState.Closed) db.Open();
            SqlCommand cmd_number_of_rows_users = new SqlCommand("select count(*) from Users", db);
            number_of_rows_users = Convert.ToInt32(cmd_number_of_rows_users.ExecuteScalar());
            for (int entry = 1; entry < number_of_rows_users + 1; entry++)
            {
                String str = "select Username from Users where ID=@Entry";
                SqlCommand cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                var temp = (cmd.ExecuteScalar()).ToString();
                username.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry);

                str = "select [First Name] from Users where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                temp = (cmd.ExecuteScalar()).ToString();
                first_name.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry);

                str = "select [Last Name] from Users where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                temp = (cmd.ExecuteScalar()).ToString();
                last_name.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry);

                str = "select Password from Users where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                temp = (cmd.ExecuteScalar()).ToString();
                password.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry);

                str = "select Mail from Users where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                temp = (cmd.ExecuteScalar()).ToString();
                mail.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry);

                str = "select Sex from Users where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                temp = (cmd.ExecuteScalar()).ToString();
                if (temp == "True")
                    sex.SetValue(true, entry);
                else
                    sex.SetValue(false, entry);
            }
        }
    }
}
