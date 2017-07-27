using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    class Delete
    {
        public static SqlConnection db = new SqlConnection(@"Data Source=EUGENE;Initial Catalog=simple;Integrated Security=True");
        public static void delete_project(int i)
        {
            if (db.State == ConnectionState.Closed) db.Open();
            String str = @"DELETE FROM projects WHERE id = @entry";
            SqlCommand cmd = new SqlCommand(str, db);
            cmd.Parameters.Add(new SqlParameter("entry", i));
            cmd.ExecuteNonQuery();
        }
    }
}
