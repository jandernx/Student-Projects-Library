using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    class Updatee
    {
        public static SqlConnection db = new SqlConnection(@"Data Source=EUGENE;Initial Catalog=simple;Integrated Security=True");
        public static void update_project(int i, String topic, String description, String link_PDF, String link_pic)
        {
            if (db.State == ConnectionState.Closed) db.Open();
            String str = @"UPDATE projects SET topic = @topic, Description = @description, Link_PDF = @Link_PDF , Link_pic = @Link_pic WHERE id = @id;";
            SqlCommand cmd = new SqlCommand(str, db);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@topic", SqlDbType.Text) {Value = topic},
                new SqlParameter("@description", SqlDbType.Text) {Value = description},
                new SqlParameter("@Link_PDF", SqlDbType.Text) {Value = link_PDF},
                new SqlParameter("@Link_pic", SqlDbType.Text) {Value = link_pic},
                new SqlParameter("@id", SqlDbType.Int) {Value = i},
            };
            cmd.Parameters.AddRange(prm.ToArray());
            cmd.ExecuteNonQuery();
        }
    }
}
