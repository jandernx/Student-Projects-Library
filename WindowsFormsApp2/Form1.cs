using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PQScan.PDFToImage;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WindowsFormsApp2
{


    public partial class Form1 : Form
    {
        private bool reg = false;
        private bool dragging = false;
        private bool logged = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Array description = Array.CreateInstance(typeof(string), 40);
        private Array id = Array.CreateInstance(typeof(int), 40);
        private Array author = Array.CreateInstance(typeof(string), 40);
        private Array topic = Array.CreateInstance(typeof(string), 40);
        private Array likes_count = Array.CreateInstance(typeof(int), 40);
        private Array user = Array.CreateInstance(typeof(string), 40);
        private Array mail = Array.CreateInstance(typeof(string), 40);
        private Array password = Array.CreateInstance(typeof(string), 40);
        private String restore_topic = String.Empty;
        private String restore_description = String.Empty;
        private String restore_link_pdf = String.Empty;
        private String restore_link_pic = String.Empty;
        private int index = 1;
        private int max = 0;
        private int ind = -1;
        private int current_id = 0;
        private int switcher = 0;
        private String current_username = String.Empty;
        private int lable_value = 0;
        bool wasSerached = false;
        SqlConnection db = new SqlConnection(@"Data Source=EUGENE;Initial Catalog=simple;Integrated Security=True");


        private List<String> recover = new List<string>();

        public Form1()
        {
            InitializeComponent();


        }

        private void logopanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
            bunifuFlatButton3_Click(sender, e);

            clickMain();
            User_projectTable.getElements(Sort.topicOnButtons, current_username);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {

        }



        private void login_textbox_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'simpleDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.simpleDataSet.Users);
            password_textbox.isPassword = true;
            //if (login_textbox.Focus() == false && login_textbox.Text == "") login_textbox.Text = "Логін або пошта";
        }

        private void login_textbox_Click(object sender, EventArgs e)
        {
            if (login_textbox.Text == "Логін або пошта") login_textbox.Text = "";
        }

        private void password_textbox_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            clickLogin();
        }

        private void show_registration()
        {
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            bunifuMetroTextbox1.Visible = true;
            bunifuMetroTextbox1.Visible = true;
            bunifuMetroTextbox2.Visible = true;
            bunifuMetroTextbox3.Visible = true;
            bunifuMetroTextbox4.Visible = true;
            bunifuMetroTextbox5.Visible = true;
            bunifuMetroTextbox6.Visible = true;
            bunifuCheckbox2.Visible = true;
            label10.Visible = true;
            linkLabel1.Visible = true;
            bunifuThinButton21.Visible = true;
            label9.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
        }

        private void hide_registration()
        {
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = true;
            bunifuMetroTextbox1.Visible = true;
            bunifuMetroTextbox1.Visible = true;
            bunifuMetroTextbox2.Visible = true;
            bunifuMetroTextbox3.Visible = true;
            bunifuMetroTextbox4.Visible = true;
            bunifuMetroTextbox5.Visible = true;
            bunifuMetroTextbox6.Visible = true;
            bunifuCheckbox2.Visible = true;
            label10.Visible = true;
            linkLabel1.Visible = true;
            bunifuThinButton21.Visible = true;
            label9.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            clickRegister();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            clickAbout();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Sort.getElements();
            Sort.sort("Alphabetical");
            List<double> power = new List<double>();
            List<double> power_copy = new List<double>();
            for (int i = 0; i < 5; i++)
            {
                if (Sort.topicOnButtons.GetValue(i) == null) break;
                else
                {
                    try { power.Add(Convert.ToDouble(Sort.likesOnButtons.GetValue(i)) / Convert.ToDouble(Sort.viewsOnButtons.GetValue(i))); }
                    catch { continue; }
                }
            }

            for (int i = 0; i < power.Count; i++)
            {
                power_copy.Add(power[i]);
            }

            power_copy.Sort();
            power_copy.Reverse();

            for (int i = 0; i < power_copy.Count; i++)
            {
                for (int j = 0; j < power.Count; j++)
                {
                    if (power_copy[i] == power[j] && power_copy[i] != 0 && power[j] != 0 && power[i] < power[j])
                    {
                        String temp = Convert.ToString(Sort.topicOnButtons.GetValue(j));
                        Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i), j);
                        Sort.topicOnButtons.SetValue(temp, i);
                        temp = Convert.ToString(Sort.authorOnButtons.GetValue(j));
                        Sort.authorOnButtons.SetValue(Sort.authorOnButtons.GetValue(i), j);
                        Sort.authorOnButtons.SetValue(temp, i);
                        temp = Convert.ToString(Sort.descriptionOnButtons.GetValue(j));
                        Sort.descriptionOnButtons.SetValue(Sort.descriptionOnButtons.GetValue(i), j);
                        Sort.descriptionOnButtons.SetValue(temp, i);
                        temp = Convert.ToString(Sort.likesOnButtons.GetValue(j));
                        Sort.likesOnButtons.SetValue(Sort.likesOnButtons.GetValue(i), j);
                        Sort.likesOnButtons.SetValue(Convert.ToInt32(temp), i);
                        temp = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(j));
                        Sort.Link_PDFOnButtons.SetValue(Sort.Link_PDFOnButtons.GetValue(i), j);
                        Sort.Link_PDFOnButtons.SetValue(temp, i);
                        temp = Convert.ToString(Sort.Link_picOnButtons.GetValue(j));
                        Sort.Link_picOnButtons.SetValue(Sort.Link_picOnButtons.GetValue(i), j);
                        Sort.Link_picOnButtons.SetValue(temp, i);
                        temp = Convert.ToString(Sort.viewsOnButtons.GetValue(j));
                        Sort.viewsOnButtons.SetValue(Sort.viewsOnButtons.GetValue(i), j);
                        Sort.viewsOnButtons.SetValue(Convert.ToInt32(temp), i);
                        double tempp = power[j];
                        power[j] = power[i];
                        power[i] = tempp;
                        break;
                    }
                }
            }
            int q = 0;
            for (int i = 0; i < power.Count; i++)
            {
                if (Double.IsInfinity(power[i]))
                    q++;
            }
            int n = power.Count - q;

            if (n == 5)
            {

                label39.Text = Convert.ToString(Sort.topicOnButtons.GetValue(0));
                label38.Text = Convert.ToString(Sort.authorOnButtons.GetValue(0));
                label37.Text = "Лайки/Перегляди:  " + Convert.ToString(power[0]).Remove(4, 11);

                label25.Text = Convert.ToString(Sort.topicOnButtons.GetValue(1));
                label26.Text = Convert.ToString(Sort.authorOnButtons.GetValue(1));
                label27.Text = "Лайки/Перегляди:  " + Convert.ToString(power[1]).Remove(4, 11);

                label36.Text = Convert.ToString(Sort.topicOnButtons.GetValue(2));
                label35.Text = Convert.ToString(Sort.authorOnButtons.GetValue(2));
                label34.Text = "Лайки/Перегляди:  " + Convert.ToString(power[2]).Remove(4, 11);

                label28.Text = Convert.ToString(Sort.topicOnButtons.GetValue(3));
                label29.Text = Convert.ToString(Sort.authorOnButtons.GetValue(3));
                label30.Text = "Лайки/Перегляди:  " + Convert.ToString(power[3]).Remove(4, 11);

                label33.Text = Convert.ToString(Sort.topicOnButtons.GetValue(4));
                label32.Text = Convert.ToString(Sort.authorOnButtons.GetValue(4));
                label31.Text = "Лайки/Перегляди:  " + Convert.ToString(power[4]).Remove(4, 11);

                label39.Visible = true;
                label38.Visible = true;
                label37.Visible = true;

                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;

                label36.Visible = true;
                label35.Visible = true;
                label34.Visible = true;

                label28.Visible = true;
                label29.Visible = true;
                label30.Visible = true;

                label33.Visible = true;
                label32.Visible = true;
                label31.Visible = true;
            }

            if (n == 4)
            {
                label25.Text = Convert.ToString(Sort.topicOnButtons.GetValue(1));
                label26.Text = Convert.ToString(Sort.authorOnButtons.GetValue(1));
                label27.Text = "Лайки/Перегляди:  " + Convert.ToString(power[1]).Remove(4, 11);

                label36.Text = Convert.ToString(Sort.topicOnButtons.GetValue(2));
                label35.Text = Convert.ToString(Sort.authorOnButtons.GetValue(2));
                label34.Text = "Лайки/Перегляди:  " + Convert.ToString(power[2]).Remove(4, 11);

                label28.Text = Convert.ToString(Sort.topicOnButtons.GetValue(3));
                label29.Text = Convert.ToString(Sort.authorOnButtons.GetValue(3));
                label30.Text = "Лайки/Перегляди:  " + Convert.ToString(power[3]).Remove(4, 11);

                label33.Text = Convert.ToString(Sort.topicOnButtons.GetValue(4));
                label32.Text = Convert.ToString(Sort.authorOnButtons.GetValue(4));
                label31.Text = "Лайки/Перегляди:  " + Convert.ToString(power[4]).Remove(4, 11);

                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;

                label36.Visible = true;
                label35.Visible = true;
                label34.Visible = true;

                label28.Visible = true;
                label29.Visible = true;
                label30.Visible = true;

                label33.Visible = true;
                label32.Visible = true;
                label31.Visible = true;
            }

            if (n == 3)
            {
                label39.Text = Convert.ToString(Sort.topicOnButtons.GetValue(2));
                label38.Text = Convert.ToString(Sort.authorOnButtons.GetValue(2));
                label37.Text = "Лайки/Перегляди:  " + Convert.ToString(power[2]).Remove(4, 11);

                label25.Text = Convert.ToString(Sort.topicOnButtons.GetValue(3));
                label26.Text = Convert.ToString(Sort.authorOnButtons.GetValue(3));
                label27.Text = "Лайки/Перегляди:  " + Convert.ToString(power[3]).Remove(4, 11);

                label36.Text = Convert.ToString(Sort.topicOnButtons.GetValue(4));
                label35.Text = Convert.ToString(Sort.authorOnButtons.GetValue(4));
                label34.Text = "Лайки/Перегляди:  " + Convert.ToString(power[4]).Remove(4, 11);

                label39.Visible = true;
                label38.Visible = true;
                label37.Visible = true;

                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;

                label36.Visible = true;
                label35.Visible = true;
                label34.Visible = true;
            }

            if (n == 2)
            {
                label25.Text = Convert.ToString(Sort.topicOnButtons.GetValue(3));
                label26.Text = Convert.ToString(Sort.authorOnButtons.GetValue(3));
                label27.Text = "Лайки/Перегляди:  " + Convert.ToString(power[3]).Remove(4, 11);

                label36.Text = Convert.ToString(Sort.topicOnButtons.GetValue(4));
                label35.Text = Convert.ToString(Sort.authorOnButtons.GetValue(4));
                label34.Text = "Лайки/Перегляди:  " + Convert.ToString(power[4]).Remove(4, 11);

                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;

                label36.Visible = true;
                label35.Visible = true;
                label34.Visible = true;
            }

            if (n == 1)
            {
                label39.Text = Convert.ToString(Sort.topicOnButtons.GetValue(4));
                label38.Text = Convert.ToString(Sort.authorOnButtons.GetValue(4));
                label37.Text = "Лайки/Перегляди:  " + Convert.ToString(power[4]).Remove(4, 11);

                label39.Visible = true;
                label38.Visible = true;
                label37.Visible = true;
            }

            max = n;

            linkLabel9.Visible = false;
            linkLabel10.Visible = false;
            pictureBox12.Visible = false;
            label24.Visible = false;
            this.Size = new Size(937, 545);
            if(current_username != "")if(current_username != "")User_projectTable.sendNew(current_username);
            if (db.State == ConnectionState.Closed) db.Open();
            for (int j = 0; j < Sort.topicOnButtons.Length; j++)
            {
                if (Convert.ToString(Sort.topicOnButtons.GetValue(j)) == null || Convert.ToString(Sort.topicOnButtons.GetValue(j)) == "")
                    break;
                for (int i = 0; i < Sort.topic.Length; i++)
                {
                    if (Convert.ToString(Sort.topic.GetValue(i)) == Convert.ToString(Sort.topicOnButtons.GetValue(j)))
                    {
                        String str = "update projects set Views = " + Sort.viewsOnButtons.GetValue(j) + " where ID = " + i;
                        SqlCommand cmd = new SqlCommand(str, db);
                        cmd.ExecuteNonQuery();
                        break;
                    }
                }
            }
            clickStatisitcs();
        }

        public void queries()
        {
            if (db.State == ConnectionState.Closed) db.Open();
            SqlCommand cmd_legthofdb = new SqlCommand("select count(*) from projects", db);
            int number_of_rows = Convert.ToInt32(cmd_legthofdb.ExecuteScalar());
            for (int entry = 1; entry < number_of_rows + 1; entry++)
            {
                String str = "select Description from projects where ID=@Entry";
                SqlCommand cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                try { description.SetValue((cmd.ExecuteScalar()).ToString(), entry); }
                catch { continue;  }

                str = "select Author from projects where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                author.SetValue((cmd.ExecuteScalar()).ToString(), entry);

                str = "select Topic from projects where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                topic.SetValue((cmd.ExecuteScalar()).ToString(), entry);

                str = "select Likes_count from projects where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                likes_count.SetValue(Convert.ToInt32((cmd.ExecuteScalar()).ToString()), entry);

                str = "select ID from projects where ID=@Entry";
                cmd = new SqlCommand(str, db);
                cmd.Parameters.Add(new SqlParameter("Entry", entry));
                id.SetValue(Convert.ToInt32((cmd.ExecuteScalar()).ToString()), entry);
                //if (myArr.GetValue(entry) == null) break;                                 

            }
            db.Close();
        }
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Sort.topic.Length; i++)
            {
                Sort.topic.SetValue(null, i);
            }
            for (int i = 0; i < Sort.topicOnButtons.Length; i++)
            {
                Sort.topicOnButtons.SetValue(null, i);
            }
            Sort.getElements();
            Sort.sort("Rating");
            comboBox1.SelectedItem = "За рейтингом";
            label24.Visible = true;
            pictureBox12.Visible = true;
            User_projectTable.getElements(Sort.topicOnButtons, current_username);

            clickProjects();
            if (Sort.topicOnButtons.GetValue(0) != null)
            {
                bunifuFlatButton4.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(0));
                bunifuFlatButton4.Visible = true;
            }
            else
                bunifuFlatButton4.Visible = false;
            if (Sort.topicOnButtons.GetValue(1) != null)
            {
                bunifuFlatButton11.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(1));
                bunifuFlatButton11.Visible = true;
            }
            else
                bunifuFlatButton11.Visible = false;
            if (Sort.topicOnButtons.GetValue(2) != null)
            {
                bunifuFlatButton12.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(2));
                bunifuFlatButton12.Visible = true;
            }
            else
                bunifuFlatButton12.Visible = false;
            if (Sort.topicOnButtons.GetValue(3) != null)
            {
                bunifuFlatButton13.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(3));
                bunifuFlatButton13.Visible = true;
            }
            else
                bunifuFlatButton13.Visible = false;
            if (Sort.topicOnButtons.GetValue(4) != null)
            {
                bunifuFlatButton14.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(4));
                bunifuFlatButton14.Visible = true;
            }
            else
                bunifuFlatButton14.Visible = false;
            if (Sort.topicOnButtons.GetValue(5) != null)
            {
                bunifuFlatButton15.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(5));
                bunifuFlatButton15.Visible = true;
            }
            else
                bunifuFlatButton15.Visible = false;
            if (Sort.topicOnButtons.GetValue(6) != null)
            {
                bunifuFlatButton16.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(6));
                bunifuFlatButton16.Visible = true;
            }
            else
                bunifuFlatButton16.Visible = false;
            if (Sort.topicOnButtons.GetValue(7) != null)
            {
                bunifuFlatButton17.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(7));
                bunifuFlatButton17.Visible = false;
            }
            else
                bunifuFlatButton17.Visible = false;
            if (Sort.topicOnButtons.GetValue(8) != null)
            {
                bunifuFlatButton18.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(8));
                bunifuFlatButton18.Visible = true;
            }
            else
                bunifuFlatButton18.Visible = false;
            if (Sort.topicOnButtons.GetValue(9) != null)
            {
                bunifuFlatButton19.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(9));
                bunifuFlatButton19.Visible = true;
            }
            else
                bunifuFlatButton19.Visible = false;
            if (Sort.topicOnButtons.GetValue(10) != null)
            {
                bunifuFlatButton20.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(10));
                bunifuFlatButton20.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(11) != null)
            {
                bunifuFlatButton21.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(11));
                bunifuFlatButton21.Visible = true;
            }
            this.Size = new Size(1222, 545);
            linkLabel10.Location = linkLabel9.Location;
            linkLabel10.Visible = true;
            linkLabel9.Visible = false;
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }


        private void headerpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
        {

        }

        private void headerpanel_MouseMove(object sender, MouseEventArgs e)
        {

            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void headerpanel_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void headerpanel_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void bunifuCustomLabel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void bunifuCustomLabel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void bunifuCustomLabel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            label13.Focus();
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.Visible = false;
            pictureBox7.Visible = true;
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
        }
        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {

            pictureBox7.Visible = false;
            pictureBox6.Visible = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            like();
            send_like();
        }

        private void pictureBox9_MouseHover(object sender, EventArgs e)
        {
            pictureBox9.Visible = false;
            pictureBox8.Visible = true;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Visible = false;
            pictureBox9.Visible = true;
        }

        private void label14_Click(object sender, EventArgs e)
        {
            label14.Focus();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            like();
            send_like();
        }

        public static class Globals
        {
            public static Boolean liked = false;
            public static Boolean disliked = false;
        }
        private void send_like()
        {
            lable_value = Convert.ToInt32(label16.Text);
            if (db.State == ConnectionState.Closed) db.Open();
            String str = "UPDATE projects SET Likes_count = @lable_value WHERE ID = @id; ";
            SqlCommand cmd = new SqlCommand(str, db);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@lable_value", SqlDbType.Int) {Value = lable_value},
                new SqlParameter("@id", SqlDbType.Int) {Value = Sort.idOnButtons.GetValue(switcher-1)},
            };
            cmd.Parameters.AddRange(prm.ToArray());
            cmd.ExecuteNonQuery();
            db.Close();

        }

        private void send_view()
        {
            lable_value = Convert.ToInt32(Sort.viewsOnButtons.GetValue(switcher)) + 1;
            if (db.State == ConnectionState.Closed) db.Open();
            String str = "UPDATE projects SET Views = @lable_value WHERE ID = @id; ";
            SqlCommand cmd = new SqlCommand(str, db);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@lable_value", SqlDbType.Int) {Value = lable_value},
                new SqlParameter("@id", SqlDbType.Int) {Value = Sort.idOnButtons.GetValue(switcher-1)},
            };
            cmd.Parameters.AddRange(prm.ToArray());
            cmd.ExecuteNonQuery();
            db.Close();

        }
        private void like()
        {
            if (Globals.liked && !Globals.disliked)
            {
                label16.Text = Convert.ToString(int.Parse(label16.Text) - 1);
                Globals.liked = false;
                User_projectTable.project[switcher - 1] = 0;
                Sort.likesOnButtons.SetValue(Convert.ToInt32(label16.Text), switcher - 1);
                pictureBox6.Image = pictureBoxTemporary.Image;
            }
            else if (Globals.disliked)
            {
                label16.Text = Convert.ToString(int.Parse(label16.Text) + 2);
                Globals.liked = true;
                Globals.disliked = false;
                User_projectTable.project[switcher - 1] = 1;
                Sort.likesOnButtons.SetValue(Convert.ToInt32(label16.Text), switcher - 1);
                //pictureBoxTemporary.Image = pictureBox6.Image;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;

            }
            else
            {
                label16.Text = Convert.ToString(int.Parse(label16.Text) + 1);
                Globals.liked = true;
                try 
                {
                    User_projectTable.project[switcher - 1] = 1;
                    Sort.likesOnButtons.SetValue(Convert.ToInt32(label16.Text), switcher - 1);
                    //pictureBoxTemporary.Image = pictureBox6.Image;
                    pictureBox6.Image = pictureBox7.Image;
                }
                catch { }
                
            }
        }
        private void dislike()
        {
            if (Globals.disliked && !Globals.liked)
            {
                label16.Text = Convert.ToString(int.Parse(label16.Text) + 1);
                Globals.disliked = false;
                User_projectTable.project[switcher - 1] = 0;
                Sort.likesOnButtons.SetValue(Convert.ToInt32(label16.Text), switcher - 1);
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (Globals.liked)
            {
                label16.Text = Convert.ToString(int.Parse(label16.Text) - 2);
                Globals.disliked = true;
                Globals.liked = false;
                User_projectTable.project[switcher - 1] = -1;
                Sort.likesOnButtons.SetValue(Convert.ToInt32(label16.Text), switcher-1);
                //pictureBoxTemporary2.Image = pictureBox9.Image;
                pictureBox9.Image = pictureBox8.Image;
                pictureBox6.Image = pictureBoxTemporary.Image;
            }
            else
            {
                label16.Text = Convert.ToString(int.Parse(label16.Text) - 1);
                Globals.disliked = true;
                User_projectTable.project[switcher - 1] = -1;
                Sort.likesOnButtons.SetValue(Convert.ToInt32(label16.Text), switcher - 1);
                //pictureBoxTemporary2.Image = pictureBox9.Image;
                pictureBox9.Image = pictureBox8.Image;
            }
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            dislike();
            send_like();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            dislike();
            send_like();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Globals.liked = false;
            Globals.disliked = false;
            pictureBox6.Image = pictureBoxTemporary.Image;
            pictureBox9.Image = pictureBoxTemporary2.Image;
            if (switcher < Sort.topicOnButtons.Length)
            switchProject();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.liked = false;
            Globals.disliked = false;
            pictureBox6.Image = pictureBoxTemporary.Image;
            pictureBox9.Image = pictureBoxTemporary2.Image;
            switcher -= 2;
            if(switcher>=0)
                switchProject();
        }

        private int check_login_queries()
        {
            int number_of_rows_users = 0;

            if (db.State == ConnectionState.Closed) db.Open();
            {
                SqlCommand cmd_number_of_rows_users = new SqlCommand("select MAX(ID) from Users", db);
                number_of_rows_users = Convert.ToInt32(cmd_number_of_rows_users.ExecuteScalar());
                for (int entry = 1; entry < number_of_rows_users + 1; entry++)
                {
                    String str = "select Username from Users where ID=@Entry";
                    SqlCommand cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("Entry", entry));
                    var temp = (cmd.ExecuteScalar()).ToString();
                    user.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry);

                    str = "select Mail from Users where ID=@Entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("Entry", entry));
                    temp = (cmd.ExecuteScalar()).ToString();
                    mail.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry);

                    str = "select [Password] from Users where ID=@Entry";
                    cmd = new SqlCommand(str, db);
                    cmd.Parameters.Add(new SqlParameter("Entry", entry));
                    temp = (cmd.ExecuteScalar()).ToString();
                    password.SetValue(temp.Remove(temp.IndexOf(' '), temp.Length - temp.IndexOf(' ')), entry);
                }
            }
            return number_of_rows_users;
        }
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            int number_of_rows_users = check_login_queries();

            if (login_textbox.Text.Length != 0 && password_textbox.Text.Length != 0)
                if (login_textbox.Text.Length > 7)
                    if (password_textbox.Text.Length > 7)
                        for (int i = 1; i < number_of_rows_users + 1; i++)
                        {
                            if (login_textbox.Text == Convert.ToString(user.GetValue(i)) || login_textbox.Text == Convert.ToString(mail.GetValue(i)))
                                if (password_textbox.Text == Convert.ToString(password.GetValue(i)))
                                {
                                    logged = true;
                                    //MessageBox.Show("you have logged");
                                    current_id = i;
                                    current_username = login_textbox.Text;
                                    password_textbox.Text = "";
                                    login_textbox.Text = "";
                                    break;
                                }
                                else
                                { MessageBox.Show("Неправильний пароль"); break; }
                            else
                            { if (i == number_of_rows_users) { MessageBox.Show("Неправильний логін або пошта"); break; } }
                        }
                    else
                    { MessageBox.Show("Пароль має бути не менше 8 символів"); }
                else
                { MessageBox.Show("Логін або пошта мають бути не менше 8 символів"); }
            else if (login_textbox.Text.Length == 0 && password_textbox.Text.Length == 0)
            { MessageBox.Show("ВВедіть логін або пошту та пароль"); }
            else if (login_textbox.Text.Length == 0 && password_textbox.Text.Length != 0)
            { MessageBox.Show("ВВедіть логін або пошту"); }
            else if (login_textbox.Text.Length != 0 && password_textbox.Text.Length == 0)
            { MessageBox.Show("Введіть пароль"); }
            if (logged)
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                linkLabel2.Visible = true;
                linkLabel3.Visible = true;
                label17.Visible = true;
                queries_users.get_info_users();
                label17.Text = queries_users.first_name.GetValue(current_id) + " " + queries_users.last_name.GetValue(current_id);
                bunifuFlatButton1_Click(sender, e);

            }
            //{ MessageBox.Show("ВВедіть логін або пошту"); }

            //if (login_textbox.Text == Convert.ToString(user.GetValue(i)) || login_textbox.Text == Convert.ToString(mail.GetValue(i)))
            //    if (password_textbox.Text == Convert.ToString(password.GetValue(i)))
            //    {
            //        logged = true;
            //        MessageBox.Show("you have logged");
            //        break;
            //    }
            //    else if (password_textbox.Text.Length == 0) MessageBox.Show("Введіть пароль");
            //    else if (password_textbox.Text.Length < 7) MessageBox.Show("Пароль має бути не менше 8 символів");
            //    else MessageBox.Show("Неправильний пароль");
            //else if (login_textbox.Text.Length == 0 && password_textbox.Text.Length == 0) MessageBox.Show("ВВедіть логін або пошту та пароль");
            //else if (login_textbox.Text.Length == 0) MessageBox.Show("ВВедіть логін або пошту");
            //else if (login_textbox.Text.Length < 7) MessageBox.Show("Логін або пошта мають бути не менше 8 символів");
            //else if (i == number_of_rows_users) MessageBox.Show("uncorrect user");

        }
        private int male_of_female()
        {
            if (radioButton1.Checked)
                return 1;
            else
                return 0;
        }

        private void registration()
        {
            
            if (db.State == ConnectionState.Closed) db.Open();
            SqlCommand cmd_legthofdb = new SqlCommand("select count(*) from Users", db);
            int id_for_next = Convert.ToInt32(cmd_legthofdb.ExecuteScalar()) + 1;

            String str = "insert into Users (ID, Username, [First Name], [Last Name], Password, Mail, Sex ) VALUES(@id_for_next, @username, @first_name, @last_name, @password, @mail, @sex); ";
            SqlCommand cmd = new SqlCommand(str, db);
            List<SqlParameter> prm = new List<SqlParameter>()
            {
                new SqlParameter("@id_for_next", SqlDbType.Int) {Value = id_for_next},
                new SqlParameter("@username", SqlDbType.Char) {Value = bunifuMetroTextbox3.Text},
                new SqlParameter("@first_name", SqlDbType.Char) {Value = bunifuMetroTextbox1.Text},
                new SqlParameter("@last_name", SqlDbType.Char) {Value = bunifuMetroTextbox2.Text},
                new SqlParameter("@password", SqlDbType.Char) {Value = bunifuMetroTextbox5.Text},
                new SqlParameter("@mail", SqlDbType.Char) {Value = bunifuMetroTextbox4.Text},
                new SqlParameter("@sex", SqlDbType.Bit) {Value = Convert.ToBoolean(male_of_female())},
            };
            cmd.Parameters.AddRange(prm.ToArray());
            cmd.ExecuteNonQuery();

            str = "insert into user_project (ID, Username) VALUES(@id_for_next, @username); ";
            cmd = new SqlCommand(str, db);
            List<SqlParameter> prmm = new List<SqlParameter>()
            {
                new SqlParameter("@id_for_next", SqlDbType.Int) {Value = User_projectTable.number_of_rows_user_projects+1},
                new SqlParameter("@username", SqlDbType.Char) {Value = bunifuMetroTextbox3.Text},
            };
            cmd.Parameters.AddRange(prmm.ToArray());
            cmd.ExecuteNonQuery();
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            //if (bunifuMetroTextbox1.Text != String.Empty && bunifuMetroTextbox2.Text != String.Empty && bunifuMetroTextbox3.Text != String.Empty && bunifuMetroTextbox4.Text != String.Empty && bunifuMetroTextbox5.Text != String.Empty && bunifuMetroTextbox6.Text != String.Empty && radioButton1.Checked == true || radioButton2.Checked == false && bunifuCheckbox2.Checked == true)
            //    if (bunifuMetroTextbox3.Text.Length > 7)
            //        bunifuMetroTextbox3.Text = "Логін має бути не менше 8 символів";
            //    if (bunifuMetroTextbox4.Text.Length > 7)
            //        bunifuMetroTextbox4.Text = "Пошта має бути не менше 8 символів";
            //    if (bunifuMetroTextbox5.Text.Length > 7)
            //        bunifuMetroTextbox5.Text = "Пароль має бути не менше 8 символів";
            //    if (bunifuMetroTextbox6.Text.Length > 7)
            //        bunifuMetroTextbox6.Text = "Логін має бути не менше 8 символів";
            bunifuMetroTextbox1_Leave(sender, e);
            bunifuMetroTextbox2_Leave_1(sender, e);
            bunifuMetroTextbox3_Leave_1(sender, e);
            bunifuMetroTextbox4_Leave(sender, e);
            bunifuMetroTextbox5_Leave(sender, e);
            bunifuMetroTextbox6_Leave(sender, e);
            if (bunifuMetroTextbox6.Text == String.Empty || bunifuMetroTextbox6.Text == "Паролі не співпадають")
                reg = false;
            if (bunifuCheckbox2.Checked == false)
            {
                label10.ForeColor = Color.Red;
                linkLabel1.ForeColor = Color.Red;
                reg = false;
            }
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                radioButton1.ForeColor = Color.Red;
                radioButton2.ForeColor = Color.Red;
                reg = false;
            }

            int number_of_rows_users = 0;
            check_login_queries();
            SqlCommand cmd_number_of_rows_users = new SqlCommand("select MAX(ID) from Users", db);
            number_of_rows_users = Convert.ToInt32(cmd_number_of_rows_users.ExecuteScalar());
            for (int i = 1; i < number_of_rows_users + 1; i++)
            {
                if (bunifuMetroTextbox3.Text == Convert.ToString(user.GetValue(i)))
                {
                    bunifuMetroTextbox3.ForeColor = Color.Red;
                    bunifuMetroTextbox3.Text = "Такий логін вже існує";
                    reg = false;
                    break; 
                }

                else if (bunifuMetroTextbox4.Text == Convert.ToString(mail.GetValue(i)))
                {
                    bunifuMetroTextbox4.ForeColor = Color.Red;
                    bunifuMetroTextbox4.Text = "Така пошта вже існує";
                    reg = false;
                    break;
                }
            }
            if (reg == true)
            {
                registration();
                MessageBox.Show("Ви зареєстровані");
                bunifuMetroTextbox1.Text = "";
                bunifuMetroTextbox2.Text = "";
                bunifuMetroTextbox3.Text = "";
                bunifuMetroTextbox4.Text = "";
                bunifuMetroTextbox5.Text = "";
                bunifuMetroTextbox6.Text = "";
                bunifuCheckbox2.Checked = false;
                radioButton1.Checked = false;
                radioButton2.Checked = false;

            }
        }


        private void bunifuMetroTextbox2_Leave_1(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox1.ForeColor != Color.Red && bunifuMetroTextbox2.Text.Length == 0)
            {
                bunifuMetroTextbox2.ForeColor = Color.Red;
                bunifuMetroTextbox2.Text = "Введіть фамілію";
                reg = false;
            }
            else if (bunifuMetroTextbox2.ForeColor == Color.Black && bunifuMetroTextbox2.Text.Length < 4 || bunifuMetroTextbox2.Text.Any(c => char.IsDigit(c)) && bunifuMetroTextbox2.ForeColor == Color.Black)
            {
                bunifuMetroTextbox2.ForeColor = Color.Red;
                bunifuMetroTextbox2.Text = "Введіть справжню фамілію";
                reg = false;
            }
        }

        private void bunifuMetroTextbox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (bunifuMetroTextbox2.Text == "Введіть фамілію" || bunifuMetroTextbox2.Text == "Введіть справжню фамілію")
            {
                bunifuMetroTextbox2.ForeColor = Color.Black;
                bunifuMetroTextbox2.Text = String.Empty;
            }
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox3.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            bunifuMetroTextbox2.ForeColor = Color.Black;
            reg = true;
        }

        private void bunifuMetroTextbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (bunifuMetroTextbox1.Text == "Введіть ім'я" || bunifuMetroTextbox1.Text == "Введіть справжнє ім'я")
            {
                bunifuMetroTextbox1.ForeColor = Color.Black;
                bunifuMetroTextbox1.Text = String.Empty;
            }
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox2.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            bunifuMetroTextbox1.ForeColor = Color.Black;
            reg = true;
        }

        private void bunifuMetroTextbox1_Leave(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox2.Text.Length != 0 && bunifuMetroTextbox1.Text.Length == 0)
            {
                bunifuMetroTextbox1.ForeColor = Color.Red;
                bunifuMetroTextbox1.Text = "Введіть ім'я";
                reg = false;
            }
            else if (bunifuMetroTextbox1.ForeColor == Color.Black && bunifuMetroTextbox1.Text.Length < 4 || bunifuMetroTextbox1.Text.Any(c => char.IsDigit(c)) && bunifuMetroTextbox1.ForeColor == Color.Black)
            {
                bunifuMetroTextbox1.ForeColor = Color.Red;
                bunifuMetroTextbox1.Text = "Введіть справжнє ім'я";
                reg = false;
            }
        }

        private void bunifuMetroTextbox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false && bunifuMetroTextbox2.ForeColor != Color.Red)
            {
                radioButton1.ForeColor = Color.Red;
                radioButton2.ForeColor = Color.Red;
            }
            if (bunifuMetroTextbox3.Text == "Введіть логін" || bunifuMetroTextbox3.Text == "Логін має бути не менше 8 символів" || bunifuMetroTextbox3.Text == "Такий логін вже існує")
            {
                bunifuMetroTextbox3.ForeColor = Color.Black;
                bunifuMetroTextbox3.Text = String.Empty;
            }
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox4.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            bunifuMetroTextbox3.ForeColor = Color.Black;
            reg = true;
        }

        private void bunifuMetroTextbox3_Leave_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false && bunifuMetroTextbox2.ForeColor != Color.Red)
            {
                radioButton1.ForeColor = Color.Red;
                radioButton2.ForeColor = Color.Red;
            }
            else if (bunifuMetroTextbox2.ForeColor != Color.Red && bunifuMetroTextbox3.Text.Length == 0)
            {
                bunifuMetroTextbox3.ForeColor = Color.Red;
                bunifuMetroTextbox3.Text = "Введіть логін";
                reg = false;
            }
            else if (bunifuMetroTextbox3.Text.Length < 8 && bunifuMetroTextbox3.ForeColor == Color.Black)
            {
                bunifuMetroTextbox3.ForeColor = Color.Red;
                bunifuMetroTextbox3.Text = "Логін має бути не менше 8 символів";
                reg = false;
            }
        }

        private void bunifuMetroTextbox4_Leave(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox3.ForeColor != Color.Red && bunifuMetroTextbox4.Text.Length == 0)
            {
                bunifuMetroTextbox4.ForeColor = Color.Red;
                bunifuMetroTextbox4.Text = "Введіть пошту";
                reg = false;
            }
            else if (bunifuMetroTextbox4.ForeColor != Color.Red && bunifuMetroTextbox4.Text.Length < 7 && bunifuMetroTextbox4.Text.Length > 0 || !Regex.IsMatch(bunifuMetroTextbox4.Text, "@") && bunifuMetroTextbox4.ForeColor != Color.Red || bunifuMetroTextbox4.Text.IndexOf(".") == -1 && bunifuMetroTextbox4.ForeColor != Color.Red || Regex.Matches(bunifuMetroTextbox4.Text, @"[a-zA-Z]").Count < 3 && bunifuMetroTextbox4.ForeColor != Color.Red)
            {
                bunifuMetroTextbox4.ForeColor = Color.Red;
                bunifuMetroTextbox4.Text = "Неправильна пошта";
                reg = false;
            }
        }

        private void bunifuMetroTextbox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (bunifuMetroTextbox4.Text == "Введіть пошту" || bunifuMetroTextbox4.Text == "Неправильна пошта" || bunifuMetroTextbox4.Text == "Така пошта вже існує")
            {
                bunifuMetroTextbox4.ForeColor = Color.Black;
                bunifuMetroTextbox4.Text = String.Empty;
            }
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox5.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            bunifuMetroTextbox4.ForeColor = Color.Black;
            reg = true;
        }

        private void bunifuMetroTextbox5_KeyDown(object sender, KeyEventArgs e)
        {
            bunifuMetroTextbox5.isPassword = true;
            if (bunifuMetroTextbox5.Text == "Введіть пароль" || bunifuMetroTextbox5.Text == "Слабкий пароль")
            {
                bunifuMetroTextbox5.ForeColor = Color.Black;
                try
                {
                    bunifuMetroTextbox5.Text = String.Empty + Convert.ToString(e)[0];
                    bunifuMetroTextbox5.Text = bunifuMetroTextbox5.Text.Remove(bunifuMetroTextbox5.Text.LastIndexOf('S'));
                }
                catch { }
            }
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox6.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            bunifuMetroTextbox5.ForeColor = Color.Black;
            reg = true;
        }

        private void bunifuMetroTextbox5_Leave(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox4.ForeColor != Color.Red && bunifuMetroTextbox5.Text.Length == 0)
            {
                bunifuMetroTextbox5.isPassword = false;
                bunifuMetroTextbox5.ForeColor = Color.Red;
                bunifuMetroTextbox5.Text = "Введіть пароль";
                reg = false;
            }
            else if (Regex.Matches(bunifuMetroTextbox5.Text, @"[a-zA-Z]").Count < 4 && bunifuMetroTextbox5.ForeColor == Color.Black || !bunifuMetroTextbox5.Text.Any(c => char.IsDigit(c)) && bunifuMetroTextbox5.ForeColor == Color.Black || bunifuMetroTextbox5.Text.Length < 8 && bunifuMetroTextbox5.ForeColor == Color.Black)
            {
                bunifuMetroTextbox5.isPassword = false;
                bunifuMetroTextbox5.ForeColor = Color.Red;
                bunifuMetroTextbox5.Text = "Слабкий пароль";
                reg = false;
            }
        }

        private void bunifuMetroTextbox6_Leave(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox6.Text != bunifuMetroTextbox5.Text && bunifuMetroTextbox5.ForeColor != Color.Red)
            {
                bunifuMetroTextbox6.isPassword = false;
                bunifuMetroTextbox6.ForeColor = Color.Red;
                bunifuMetroTextbox6.Text = "Паролі не співпадають";
                reg = false;
            }
        }

        private void bunifuMetroTextbox6_KeyDown(object sender, KeyEventArgs e)
        {
            bunifuMetroTextbox6.ForeColor = Color.Black;
            if (bunifuMetroTextbox6.Text == "Паролі не співпадають")
            {
                bunifuMetroTextbox6.ForeColor = Color.Black;
                try
                {
                    bunifuMetroTextbox6.Text = String.Empty + Convert.ToString(e)[0];
                    bunifuMetroTextbox6.Text = bunifuMetroTextbox6.Text.Remove(bunifuMetroTextbox6.Text.LastIndexOf('S'));
                }
                catch { }
            }
            if (e.KeyCode == Keys.Enter)
            {
                bunifuThinButton21_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            reg = true;
        }

        private void radioButton1_MouseDown(object sender, MouseEventArgs e)
        {
            reg = true;
            radioButton1.ForeColor = Color.Black;
            radioButton2.ForeColor = Color.Black;
        }

        private void radioButton2_MouseDown(object sender, MouseEventArgs e)
        {
            reg = true;
            radioButton1.ForeColor = Color.Black;
            radioButton2.ForeColor = Color.Black;
        }

        private void bunifuCheckbox2_OnChange(object sender, EventArgs e)
        {
            reg = true;
            label10.ForeColor = Color.Black;
            linkLabel1.ForeColor = Color.Black;
        }

        private void bunifuMetroTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false && bunifuMetroTextbox2.ForeColor != Color.Red)
            {
                radioButton1.ForeColor = Color.Red;
                radioButton2.ForeColor = Color.Red;
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bunifuMetroTextbox5.Text = "";
            bunifuThinButton29.Visible = false;
            hide_edit_buttons();
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            linkLabel2.Visible = false;
            linkLabel3.Visible = false;
            label17.Visible = false;
            logged = false;
            if(current_username != "")if(current_username != "")User_projectTable.sendNew(current_username);
            if (db.State == ConnectionState.Closed) db.Open();
            for (int j = 0; j < Sort.topicOnButtons.Length; j++)
            {
                if (Convert.ToString(Sort.topicOnButtons.GetValue(j)) == null || Convert.ToString(Sort.topicOnButtons.GetValue(j)) == "")
                    break;
                for (int i = 0; i < Sort.topic.Length; i++)
                {
                    if (Convert.ToString(Sort.topic.GetValue(i)) == Convert.ToString(Sort.topicOnButtons.GetValue(j)))
                    {
                        String str = "update projects set Views = " + Sort.viewsOnButtons.GetValue(j) + " where ID = " + i;
                        SqlCommand cmd = new SqlCommand(str, db);
                        cmd.ExecuteNonQuery();
                        break;
                    }
                }
            }
            Application.Restart();
            Environment.Exit(0);
        }

        private void bunifuMetroTextbox6_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox4_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void password_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuThinButton22_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            axAcroPDF2.Visible = true;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label12.Visible = false;
            linkLabel4.Visible = false;
            linkLabel5.Visible = true;
            pictureBox10.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox5.Visible = false;
            label16.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bunifuFlatButton2_Click(sender, e);
        }
        private void hide()
        {
            axAcroPDF2.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            linkLabel4.Visible = false;
            linkLabel5.Visible = false;
            pictureBox10.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            pictureBox5.Visible = false;
            label16.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
        }

        private void hide_edit()
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false; pictureBox11.Visible = false;
            textBox4.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
        }
        private void show_edit()
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true; pictureBox11.Visible = true;
            textBox4.Visible = true;
            label19.Visible = true;
            label20.Visible = true;
            label21.Visible = true;
            label22.Visible = true;
            label23.Visible = true;
        }
        private void show_edit_buttons()
        {
            bunifuThinButton23.Visible = true;
            bunifuThinButton24.Visible = true;
        }
        private void hide_edit_buttons()
        {
            bunifuThinButton23.Visible = false;
            bunifuThinButton24.Visible = false;
            bunifuThinButton25.Visible = false;
            bunifuThinButton26.Visible = false;
            bunifuThinButton27.Visible = false;
            bunifuThinButton28.Visible = false;
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clickMyProjects();
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            Queries_my_projects.user_projects();
            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(0));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(0));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(0));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(0));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(0) == null || Queries_my_projects.Link_pic_prjct.GetValue(0) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(0));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 0;

        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(1));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(1));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(1));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(1));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(1) == null || Queries_my_projects.Link_pic_prjct.GetValue(1) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(1));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 1;
        }

        private void bunifuFlatButton3_Click_1(object sender, EventArgs e)
        {
            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(2));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(2));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(2));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(2));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(2) == null || Queries_my_projects.Link_pic_prjct.GetValue(2) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(2));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 2;
        }

        private void bunifuFlatButton6_Click_1(object sender, EventArgs e)
        {
            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(3));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(3));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(3));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(3));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(3) == null || Queries_my_projects.Link_pic_prjct.GetValue(3) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(3));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 3;
        }

        private void bunifuFlatButton7_Click_1(object sender, EventArgs e)
        {
            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(4));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(4));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(4));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(4));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(4) == null || Queries_my_projects.Link_pic_prjct.GetValue(4) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(4));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 4;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2_LinkClicked(sender, e);
            axAcroPDF2.Visible = false;
            linkLabel6.Visible = false;
            linkLabel7.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = false;
            bunifuThinButton29.Visible = true;
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            label18.Visible = false;
            bunifuThinButton212.Visible = false;
            bunifuThinButton26.Visible = false;
            linkLabel6.Visible = false;
            linkLabel8.Location = linkLabel6.Location;
            linkLabel8.Visible = true;
            linkLabel7.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            axAcroPDF2.Visible = true;
            pictureBox10.Visible = false;
            hide_main_buttons();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            bunifuThinButton23.Visible = false;
            bunifuThinButton26.Visible = true;
            bunifuThinButton212.Visible = true;
            label18.Visible = true;
            bunifuThinButton212.Location = bunifuThinButton23.Location;
            bunifuThinButton26.Location = bunifuThinButton24.Location;
            linkLabel6.Visible = false;
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < Queries_my_projects.number_of_rows_user_projects + 1; i++)
            {
                if (label13.Text == Convert.ToString(Queries_my_projects.topic.GetValue(i)))
                {
                    Delete.delete_project(i);
                    axAcroPDF2.Visible = false;
                    linkLabel6.Visible = false;
                    Queries_my_projects.user_projects();
                    Queries_my_projects.get_projects(current_username);
                    if (Queries_my_projects.topic_prjct.GetValue(0) != null)
                    {
                        bunifuFlatButton1.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(0));
                        bunifuFlatButton1.Visible = true;
                    }
                    if (Queries_my_projects.topic_prjct.GetValue(1) != null)
                    {
                        bunifuFlatButton2.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(1));
                        bunifuFlatButton2.Visible = true;
                    }
                    if (Queries_my_projects.topic_prjct.GetValue(2) != null)
                    {
                        bunifuFlatButton3.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(2));
                        bunifuFlatButton3.Visible = true;
                    }
                    if (Queries_my_projects.topic_prjct.GetValue(3) != null)
                    {
                        bunifuFlatButton6.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(3));
                        bunifuFlatButton6.Visible = true;
                    }
                    if (Queries_my_projects.topic_prjct.GetValue(4) != null)
                    {
                        bunifuFlatButton7.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(4));
                        bunifuFlatButton7.Visible = true;
                    }
                    if (Queries_my_projects.topic_prjct.GetValue(5) != null)
                    {
                        bunifuFlatButton8.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(5));
                        bunifuFlatButton8.Visible = true;
                    }
                    if (Queries_my_projects.topic_prjct.GetValue(6) != null)
                    {
                        bunifuFlatButton9.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(6));
                        bunifuFlatButton9.Visible = true;
                    }
                    if (Queries_my_projects.topic_prjct.GetValue(7) != null)
                    {
                        bunifuFlatButton10.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(7));
                        bunifuFlatButton10.Visible = true;
                    }
                    if (Queries_my_projects.topic_prjct.GetValue(8) != null)
                    {
                        bunifuFlatButton5.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(8));
                        bunifuFlatButton5.Visible = true;
                    }
                }
            }
            clickMyProjects();
            //if (ind == 1)
            //    bunifuFlatButton1.Visible = false;
            //else if (ind == 2)
            //    bunifuFlatButton2.Visible = false;
            //else if (ind == 3)
            //    bunifuFlatButton3.Visible = false;
            //else if (ind == 4)
            //    bunifuFlatButton6.Visible = false;
            //else if (ind == 5)
            //    bunifuFlatButton7.Visible = false;
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            show_edit();
            linkLabel6.Visible = false;
            linkLabel7.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = false;



            bunifuThinButton27.Visible = true;
            bunifuThinButton28.Visible = true;
            bunifuThinButton28.Location = bunifuThinButton23.Location;
            bunifuThinButton27.Location = bunifuThinButton24.Location;



            for (int i = 1; i < Queries_my_projects.number_of_rows_user_projects + 1; i++)
            {
                //if (label13.Text == Convert.ToString(Queries_my_projects.topic.GetValue(i)))
                //{
                //    restore_topic = Convert.ToString(Queries_my_projects.topic.GetValue(i));
                //    restore_description = Convert.ToString(Queries_my_projects.description.GetValue(i));
                //    restore_link_pdf = Convert.ToString(Queries_my_projects.Link_PDF.GetValue(i));
                //    restore_link_pic = Convert.ToString(Queries_my_projects.Link_pic.GetValue(i));
                //}
            }
            textBox2.Text = label13.Text;
            textBox1.Text = label15.Text;
            textBox5.Text = label17.Text;
            for (int i = 1; i < Queries_my_projects.number_of_rows_user_projects + 1; i++)
            {
                if (label13.Text == Convert.ToString(Queries_my_projects.topic.GetValue(i)))
                {
                    textBox3.Text = Convert.ToString(Queries_my_projects.Link_pic.GetValue(i));
                    textBox4.Text = Convert.ToString(Queries_my_projects.Link_PDF.GetValue(i));
                }
            }

            if (recover.Count == 0)
            {
                recover.Add(textBox2.Text);
                recover.Add(textBox1.Text);
                recover.Add(textBox3.Text);
                recover.Add(textBox4.Text);
            }
            else
            {
                recover[0] = textBox2.Text;
                recover[1] = textBox1.Text;
                recover[2] = textBox3.Text;
                recover[3] = textBox4.Text;
            }

        }
        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            bunifuThinButton26.Visible = false;
            bunifuThinButton25.Visible = false;
            label18.Visible = false;
            linkLabel6.Visible = true;

            
        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            bunifuThinButton27.Visible = false;
            bunifuThinButton28.Visible = false;
            hide_edit();
            label13.Text = recover[0];
            label14.Text = label17.Text;
            label15.Text = recover[1];
            String s = recover[3];
            axAcroPDF2.src = @s;
            if (recover[2] == null || recover[2] == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = recover[2];
            }
            linkLabel7.Visible = true;
            linkLabel6.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();


        }

        private void save_button(String name, String description, String author, String link_pdf, String link_pic)
        {
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = name;
            label14.Text = author;
            label15.Text = description;
            String s = link_pdf;
            axAcroPDF2.src = @s;


            if (link_pic == null || link_pic == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = link_pic;
            }


            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            Queries_my_projects.user_projects();
            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(5));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(5));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(5));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(5));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(5) == null || Queries_my_projects.Link_pic_prjct.GetValue(5) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(5));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 5;
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {

            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(6));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(6));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(6));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(6));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(6) == null || Queries_my_projects.Link_pic_prjct.GetValue(6) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(6));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 6;
        }

        private void hide_main_buttons()
        {
            bunifuThinButton23.Visible = false;
            bunifuThinButton24.Visible = false;
        }

        private void show_main_buttons()
        {
            bunifuThinButton23.Visible = true;
            bunifuThinButton24.Visible = true;
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            hide_edit();
            hide_edit_buttons();
            for (int i = 1; i < Queries_my_projects.number_of_rows_user_projects + 1; i++)
            {
                if (label13.Text == Convert.ToString(Queries_my_projects.topic.GetValue(i)))
                {
                    //restore_topic = Convert.ToString(Queries_my_projects.topic.GetValue(i));
                    //restore_description = Convert.ToString(Queries_my_projects.description.GetValue(i));
                    //restore_link_pdf = Convert.ToString(Queries_my_projects.Link_PDF.GetValue(i));
                    //restore_link_pic = Convert.ToString(Queries_my_projects.Link_pic.GetValue(i));
                    Updatee.update_project(i, textBox2.Text, textBox1.Text, textBox4.Text, textBox3.Text);
                }
            }
            save_button(textBox2.Text, textBox1.Text, textBox5.Text, textBox4.Text, textBox3.Text);
            Queries_my_projects.user_projects();
        }

        private void hide_projects()
        {
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuThinButton29.Visible = false;
        }
        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
            hide_projects();
            bunifuThinButton210.Visible = true;
            bunifuThinButton211.Visible = true;
            bunifuThinButton211.Location = bunifuThinButton23.Location;
            bunifuThinButton210.Location = bunifuThinButton24.Location;
            show_edit();
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = Convert.ToString(author.GetValue(current_id));
        }

        private void bunifuThinButton211_Click(object sender, EventArgs e)
        {
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            bunifuThinButton29.Visible = true;
            hide_edit_buttons();
            hide_edit();
            Queries_my_projects.user_projects();
            Queries_my_projects.get_projects(current_username);
            if (Queries_my_projects.topic_prjct.GetValue(0) != null)
            {
                bunifuFlatButton1.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(0));
                bunifuFlatButton1.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(1) != null)
            {
                bunifuFlatButton2.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(1));
                bunifuFlatButton2.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(2) != null)
            {
                bunifuFlatButton3.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(2));
                bunifuFlatButton3.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(3) != null)
            {
                bunifuFlatButton6.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(3));
                bunifuFlatButton6.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(4) != null)
            {
                bunifuFlatButton7.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(4));
                bunifuFlatButton7.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(5) != null)
            {
                bunifuFlatButton8.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(5));
                bunifuFlatButton8.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(6) != null)
            {
                bunifuFlatButton9.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(6));
                bunifuFlatButton9.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(7) != null)
            {
                bunifuFlatButton10.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(7));
                bunifuFlatButton10.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(8) != null)
            {
                bunifuFlatButton5.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(8));
                bunifuFlatButton5.Visible = true;
            }
        }

        private void bunifuThinButton210_Click(object sender, EventArgs e)
        {
            Add.add_project(Queries_my_projects.number_of_rows_user_projects + 1, label17.Text, textBox2.Text, textBox1.Text, textBox4.Text, textBox3.Text, current_username);
            button1.Visible = false;
            button2.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label14.Visible = false;
            label16.Visible = false;
            label13.Visible = false;
            linkLabel4.Visible = false;
            axAcroPDF2.Visible = false;
            axAcroPDF2.Visible = false;
            label11.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            login_textbox.Visible = false;
            password_textbox.Visible = false;
            bunifuThinButton22.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox2.Visible = false;
            bunifuMetroTextbox3.Visible = false;
            bunifuMetroTextbox4.Visible = false;
            bunifuMetroTextbox5.Visible = false;
            bunifuMetroTextbox6.Visible = false;
            bunifuCheckbox2.Visible = false;
            label10.Visible = false;
            linkLabel1.Visible = false;
            bunifuThinButton21.Visible = false;
            label9.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            axAcroPDF2.Visible = false;
            linkLabel6.Visible = false;
            linkLabel7.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = false;
            hide_edit_buttons();
            hide_edit();
            bunifuThinButton29.Visible = true;
            Queries_my_projects.user_projects();
            Queries_my_projects.get_projects(current_username);
            if (Queries_my_projects.topic_prjct.GetValue(0) != null)
            {
                bunifuFlatButton1.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(0));
                bunifuFlatButton1.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(1) != null)
            {
                bunifuFlatButton2.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(1));
                bunifuFlatButton2.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(2) != null)
            {
                bunifuFlatButton3.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(2));
                bunifuFlatButton3.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(3) != null)
            {
                bunifuFlatButton6.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(3));
                bunifuFlatButton6.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(4) != null)
            {
                bunifuFlatButton7.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(4));
                bunifuFlatButton7.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(5) != null)
            {
                bunifuFlatButton8.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(5));
                bunifuFlatButton8.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(6) != null)
            {
                bunifuFlatButton9.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(6));
                bunifuFlatButton9.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(7) != null)
            {
                bunifuFlatButton10.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(7));
                bunifuFlatButton10.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(8) != null)
            {
                bunifuFlatButton5.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(8));
                bunifuFlatButton5.Visible = true;
            }

        }

        private void textBox4_MouseDown(object sender, MouseEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox4.Text = openFileDialog.InitialDirectory + openFileDialog.FileName;
            }
        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {

            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(7));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(7));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(7));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(7));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(7) == null || Queries_my_projects.Link_pic_prjct.GetValue(7) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(7));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 7;
        }

        private void bunifuFlatButton5_Click_1(object sender, EventArgs e)
        {

            bunifuThinButton29.Visible = false;
            linkLabel6.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            bunifuFlatButton6.Visible = false;
            bunifuFlatButton7.Visible = false;
            bunifuFlatButton8.Visible = false;
            bunifuFlatButton9.Visible = false;
            bunifuFlatButton10.Visible = false;
            bunifuFlatButton5.Visible = false;
            label13.Text = Convert.ToString(Queries_my_projects.topic_prjct.GetValue(8));
            label14.Text = Convert.ToString(Queries_my_projects.author_prjct.GetValue(8));
            label15.Text = Convert.ToString(Queries_my_projects.description_prjct.GetValue(8));
            String s = Convert.ToString(Queries_my_projects.Link_PDF_prjct.GetValue(8));
            axAcroPDF2.src = @s;
            if (Queries_my_projects.Link_pic_prjct.GetValue(8) == null || Queries_my_projects.Link_pic_prjct.GetValue(8) == String.Empty)
            {
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg";
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Queries_my_projects.Link_pic_prjct.GetValue(8));
            }
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_edit_buttons();
            ind = 8;
        }

        private void textBox3_MouseDown(object sender, MouseEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Filter = "Image Files | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox3.Text = openFileDialog.InitialDirectory + openFileDialog.FileName;
            }
        }

        private void clickMyProjects()
        {

            label40.Visible = false;
            label39.Visible = false;
            label38.Visible = false;
            label37.Visible = false;

            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;

            label36.Visible = false;
            label35.Visible = false;
            label34.Visible = false;

            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            label33.Visible = false;
            label32.Visible = false;
            label31.Visible = false;


            if(current_username != "")if(current_username != "")User_projectTable.sendNew(current_username);
            if (db.State == ConnectionState.Closed) db.Open();
            for (int j = 0; j < Sort.topicOnButtons.Length; j++)
            {
                if (Convert.ToString(Sort.topicOnButtons.GetValue(j)) == null || Convert.ToString(Sort.topicOnButtons.GetValue(j)) == "")
                    break;
                for (int i = 0; i < Sort.topic.Length; i++)
                {
                    if (Convert.ToString(Sort.topic.GetValue(i)) == Convert.ToString(Sort.topicOnButtons.GetValue(j)))
                    {
                        String str = "update projects set Views = " + Sort.viewsOnButtons.GetValue(j) + " where ID = " + i;
                        SqlCommand cmd = new SqlCommand(str, db);
                        cmd.ExecuteNonQuery();
                        break;
                    }
                }
            }

            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            bunifuMetroTextbox11.Visible = false;
            bunifuFlatButton23.Visible = false;
            bunifuFlatButton22.Visible = false;

            linkLabel9.Visible = false;
            linkLabel10.Visible = false;
            pictureBox12.Visible = false;
            label24.Visible = false;
            this.Size = new Size(937, 545);
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            label18.Visible = false;
            bunifuThinButton212.Visible = false;
            bunifuThinButton26.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label14.Visible = false;
            label16.Visible = false;
            label13.Visible = false;
            linkLabel4.Visible = false;
            axAcroPDF2.Visible = false;
            label12.Visible = false;
            axAcroPDF2.Visible = false;
            label11.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            login_textbox.Visible = false;
            password_textbox.Visible = false;
            bunifuThinButton22.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox2.Visible = false;
            bunifuMetroTextbox3.Visible = false;
            bunifuMetroTextbox4.Visible = false;
            bunifuMetroTextbox5.Visible = false;
            bunifuMetroTextbox6.Visible = false;
            bunifuCheckbox2.Visible = false;
            label10.Visible = false;
            linkLabel1.Visible = false;
            bunifuThinButton21.Visible = false;
            label9.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;


            axAcroPDF2.Visible = false;
            linkLabel6.Visible = false;
            linkLabel7.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = false;
            hide_edit_buttons();
            hide_edit();
            bunifuThinButton29.Visible = true;
            Queries_my_projects.user_projects();
            Queries_my_projects.get_projects(current_username);
            if (Queries_my_projects.topic_prjct.GetValue(0) != null)
            {
                bunifuFlatButton1.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(0));
                bunifuFlatButton1.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(1) != null)
            {
                bunifuFlatButton2.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(1));
                bunifuFlatButton2.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(2) != null)
            {
                bunifuFlatButton3.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(2));
                bunifuFlatButton3.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(3) != null)
            {
                bunifuFlatButton6.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(3));
                bunifuFlatButton6.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(4) != null)
            {
                bunifuFlatButton7.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(4));
                bunifuFlatButton7.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(5) != null)
            {
                bunifuFlatButton8.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(5));
                bunifuFlatButton8.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(6) != null)
            {
                bunifuFlatButton9.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(6));
                bunifuFlatButton9.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(7) != null)
            {
                bunifuFlatButton10.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(7));
                bunifuFlatButton10.Visible = true;
            }
            if (Queries_my_projects.topic_prjct.GetValue(8) != null)
            {
                bunifuFlatButton5.Text = "   " + Convert.ToString(Queries_my_projects.topic_prjct.GetValue(8));
                bunifuFlatButton5.Visible = true;
            }
        }

        private void clickProjects()
        {

            label40.Visible = false;
            label39.Visible = false;
            label38.Visible = false;
            label37.Visible = false;

            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;

            label36.Visible = false;
            label35.Visible = false;
            label34.Visible = false;

            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            label33.Visible = false;
            label32.Visible = false;
            label31.Visible = false;


            pictureBox13.Visible = true;
            pictureBox14.Visible = true;
            bunifuMetroTextbox11.Visible = true;
            bunifuFlatButton23.Visible = true;
            bunifuFlatButton22.Visible = true;

            switcher = 0;
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            label18.Visible = false;
            bunifuThinButton212.Visible = false;
            bunifuThinButton26.Visible = false;
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            linkLabel6.Visible = false;
            linkLabel7.Visible = false;
            hide_projects();
            hide_edit_buttons();
            hide_edit();
            if (switcher>=0 && switcher<Sort.topicOnButtons.Length)
                switchProject();
            
            linkLabel5.Visible = false;
            linkLabel4.Visible = true;
            button1.Visible = true;
            button2.Visible = true;

            if (logged)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
            }

            label11.Visible = false;
            label12.Visible = true;
            label14.Visible = true;
            label16.Visible = true;
            label13.Visible = true;
            pictureBox10.Visible = true;
            axAcroPDF2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            login_textbox.Visible = false;
            password_textbox.Visible = false;
            bunifuThinButton22.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox2.Visible = false;
            bunifuMetroTextbox3.Visible = false;
            bunifuMetroTextbox4.Visible = false;
            bunifuMetroTextbox5.Visible = false;
            bunifuMetroTextbox6.Visible = false;
            bunifuCheckbox2.Visible = false;
            label10.Visible = false;
            linkLabel1.Visible = false;
            bunifuThinButton21.Visible = false;
            label9.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }



        private void switchProject()
        {
            if (User_projectTable.project[switcher] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[switcher] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[switcher] == null || User_projectTable.projectw[switcher] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(switcher))+1, switcher);
                User_projectTable.projectw[switcher] = 1;
            }

            label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(switcher));
            label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(switcher));
            label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(switcher));
            label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(switcher));
            label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(switcher));
            String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(switcher));

            if (Sort.Link_picOnButtons.GetValue(switcher) == null || Sort.Link_picOnButtons.GetValue(switcher) == String.Empty)
            {
                axAcroPDF2.src = @s;
                PDFDocument pdfDoc = new PDFDocument();
                pdfDoc.LoadPDF(@s);
                try { Bitmap jpgImage = pdfDoc.ToImage(0);
                jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                pictureBox10.ImageLocation = "img.jpg"; }
                catch { MessageBox.Show("Нічого не знайдено"); };
            }
            else
            {
                pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(switcher));
            }
            linkLabel7.Visible = false;

            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            switcher++;
        }
        private void clickMain()
        {
            
            label39.Visible = false;
            label38.Visible = false;
            label37.Visible = false;

            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;

            label36.Visible = false;
            label35.Visible = false;
            label34.Visible = false;

            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            label33.Visible = false;
            label32.Visible = false;
            label31.Visible = false;

            if (logged)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
            }
            pictureBox5.Visible = true;
            label40.Visible = true;
            label40.Text = "TOP-1";
            linkLabel7.Visible = true;
            pictureBox10.Visible = true;
            pictureBox12.Visible = true;
            label24.Visible = true;
            label11.Visible = false;
            label12.Visible = true;
            label14.Visible = true;
            label16.Visible = true;
            label13.Visible = true;
            label24.Visible = true;
            if (max == 5)
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(0));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(0));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(0));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(0));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(0));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(0));

                if (Sort.Link_picOnButtons.GetValue(0) == null || Sort.Link_picOnButtons.GetValue(0) == String.Empty)
                {
                    axAcroPDF2.src = @s;
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    try
                    {
                        Bitmap jpgImage = pdfDoc.ToImage(0);
                        jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                        pictureBox10.ImageLocation = "img.jpg";
                    }
                    catch { MessageBox.Show("Нічого не знайдено"); };
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(0));
                }

            }

            if (max == 4)
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(1));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(1));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(1));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(1));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(1));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(1));

                if (Sort.Link_picOnButtons.GetValue(1) == null || Sort.Link_picOnButtons.GetValue(1) == String.Empty)
                {
                    axAcroPDF2.src = @s;
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    try
                    {
                        Bitmap jpgImage = pdfDoc.ToImage(0);
                        jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                        pictureBox10.ImageLocation = "img.jpg";
                    }
                    catch { MessageBox.Show("Нічого не знайдено"); };
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(1));
                }

            }

            if (max == 3)
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(2));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(2));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(2));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(2));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(2));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(2));

                if (Sort.Link_picOnButtons.GetValue(2) == null || Sort.Link_picOnButtons.GetValue(2) == String.Empty)
                {
                    axAcroPDF2.src = @s;
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    try
                    {
                        Bitmap jpgImage = pdfDoc.ToImage(0);
                        jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                        pictureBox10.ImageLocation = "img.jpg";
                    }
                    catch { MessageBox.Show("Нічого не знайдено"); };
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(2));
                }

            }
            if(current_username != "")if(current_username != "")User_projectTable.sendNew(current_username);
            if (db.State == ConnectionState.Closed) db.Open();
            for (int j = 0; j < Sort.topicOnButtons.Length; j++)
            {
                if (Convert.ToString(Sort.topicOnButtons.GetValue(j)) == null || Convert.ToString(Sort.topicOnButtons.GetValue(j)) == "")
                    break;
                for (int i = 0; i < Sort.topic.Length; i++)
                {
                    if (Convert.ToString(Sort.topic.GetValue(i)) == Convert.ToString(Sort.topicOnButtons.GetValue(j)))
                    {
                        String str = "update projects set Views = " + Sort.viewsOnButtons.GetValue(j) + " where ID = " + i;
                        SqlCommand cmd = new SqlCommand(str, db);
                        cmd.ExecuteNonQuery();
                        break;
                    }
                }
            }


            linkLabel9.Visible = false;
            linkLabel10.Visible = false;
            this.Size = new Size(937, 545);
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            label18.Visible = false;
            bunifuThinButton212.Visible = false;
            bunifuThinButton26.Visible = false;
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            linkLabel6.Visible = false;
            //hide_projects();
            hide_edit_buttons();
            //hide();
            hide_edit();
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            login_textbox.Visible = false;
            password_textbox.Visible = false;
            bunifuThinButton22.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox2.Visible = false;
            bunifuMetroTextbox3.Visible = false;
            bunifuMetroTextbox4.Visible = false;
            bunifuMetroTextbox5.Visible = false;
            bunifuMetroTextbox6.Visible = false;
            bunifuCheckbox2.Visible = false;
            label10.Visible = false;
            linkLabel1.Visible = false;
            bunifuThinButton21.Visible = false;
            label9.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }

        private void clickAbout()
        {
            label40.Visible = false;
            label39.Visible = false;
            label38.Visible = false;
            label37.Visible = false;

            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;

            label36.Visible = false;
            label35.Visible = false;
            label34.Visible = false;

            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            label33.Visible = false;
            label32.Visible = false;
            label31.Visible = false;

            if(current_username != "")if(current_username != "")User_projectTable.sendNew(current_username);
            if (db.State == ConnectionState.Closed) db.Open();
            for (int j = 0; j < Sort.topicOnButtons.Length; j++)
            {
                if (Convert.ToString(Sort.topicOnButtons.GetValue(j)) == null || Convert.ToString(Sort.topicOnButtons.GetValue(j)) == "")
                    break;
                for (int i = 0; i < Sort.topic.Length; i++)
                {
                    if (Convert.ToString(Sort.topic.GetValue(i)) == Convert.ToString(Sort.topicOnButtons.GetValue(j)))
                    {
                        String str = "update projects set Views = " + Sort.viewsOnButtons.GetValue(j) + " where ID = " + i;
                        SqlCommand cmd = new SqlCommand(str, db);
                        cmd.ExecuteNonQuery();
                        break;
                    }
                }
            }

            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            bunifuMetroTextbox11.Visible = false;
            bunifuFlatButton23.Visible = false;
            bunifuFlatButton22.Visible = false;

            linkLabel9.Visible = false;
            linkLabel10.Visible = false;
            pictureBox12.Visible = false;
            label24.Visible = false;
            this.Size = new Size(937, 545);
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            label18.Visible = false;
            bunifuThinButton212.Visible = false;
            bunifuThinButton26.Visible = false;
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            linkLabel6.Visible = false;
            linkLabel7.Visible = false;
            hide_projects();
            hide_edit_buttons();
            hide();
            hide_edit();
            button1.Visible = false;
            button2.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            label11.Visible = true;
            label12.Visible = false;
            label14.Visible = false;
            label16.Visible = false;
            label13.Visible = false;
            axAcroPDF2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            login_textbox.Visible = false;
            password_textbox.Visible = false;
            bunifuThinButton22.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox2.Visible = false;
            bunifuMetroTextbox3.Visible = false;
            bunifuMetroTextbox4.Visible = false;
            bunifuMetroTextbox5.Visible = false;
            bunifuMetroTextbox6.Visible = false;
            bunifuCheckbox2.Visible = false;
            label10.Visible = false;
            linkLabel1.Visible = false;
            bunifuThinButton21.Visible = false;
            label9.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }

        private void clickStatisitcs()
        {
           
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            bunifuMetroTextbox11.Visible = false;
            bunifuFlatButton23.Visible = false;
            bunifuFlatButton22.Visible = false;

            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            label18.Visible = false;
            bunifuThinButton212.Visible = false;
            bunifuThinButton26.Visible = false;
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            linkLabel6.Visible = false;
            linkLabel7.Visible = false;
            hide_projects();
            hide_edit_buttons();
            hide();
            hide_edit();
            button1.Visible = false;
            button2.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            label12.Visible = false;
            label14.Visible = false;
            label16.Visible = false;
            label13.Visible = false;
            axAcroPDF2.Visible = false;
            label11.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            login_textbox.Visible = false;
            password_textbox.Visible = false;
            bunifuThinButton22.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox2.Visible = false;
            bunifuMetroTextbox3.Visible = false;
            bunifuMetroTextbox4.Visible = false;
            bunifuMetroTextbox5.Visible = false;
            bunifuMetroTextbox6.Visible = false;
            bunifuCheckbox2.Visible = false;
            label10.Visible = false;
            linkLabel1.Visible = false;
            bunifuThinButton21.Visible = false;
            label9.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            label40.Text = "TOP-" + max;
            label40.Visible = true;
        }

        private void clickLogin()
        {
            linkLabel7.Visible = false;
            label40.Visible = false;
            label39.Visible = false;
            label38.Visible = false;
            label37.Visible = false;

            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;

            label36.Visible = false;
            label35.Visible = false;
            label34.Visible = false;

            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            label33.Visible = false;
            label32.Visible = false;
            label31.Visible = false;

            pictureBox12.Visible = false;
            pictureBox14.Visible = false;
            bunifuMetroTextbox11.Visible = false;
            bunifuFlatButton23.Visible = false;
            bunifuFlatButton22.Visible = false;

            linkLabel9.Visible = false;
            linkLabel10.Visible = false;
            pictureBox12.Visible = false;
            label24.Visible = false;
            this.Size = new Size(937, 545);
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            label18.Visible = false;
            bunifuThinButton212.Visible = false;
            bunifuThinButton26.Visible = false;
            hide();
            hide_edit_buttons();
            hide_edit();
            button1.Visible = false;
            button2.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label14.Visible = false;
            label16.Visible = false;
            label13.Visible = false;
            axAcroPDF2.Visible = false;
            label1.Location = new Point(349, 217);
            label2.Location = new Point(412, 298);
            login_textbox.Location = new Point(502, 205);
            password_textbox.Location = new Point(502, 286);
            label1.Visible = true;
            label2.Visible = true;
            login_textbox.Visible = true;
            password_textbox.Visible = true;
            bunifuThinButton22.Visible = true;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox1.Visible = false;
            bunifuMetroTextbox2.Visible = false;
            bunifuMetroTextbox3.Visible = false;
            bunifuMetroTextbox4.Visible = false;
            bunifuMetroTextbox5.Visible = false;
            bunifuMetroTextbox6.Visible = false;
            bunifuCheckbox2.Visible = false;
            label10.Visible = false;
            linkLabel1.Visible = false;
            bunifuThinButton21.Visible = false;
            label9.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }

        private void clickRegister()
        {
            linkLabel7.Visible = false;
            label40.Visible = false;
            label39.Visible = false;
            label38.Visible = false;
            label37.Visible = false;

            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;

            label36.Visible = false;
            label35.Visible = false;
            label34.Visible = false;

            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            label33.Visible = false;
            label32.Visible = false;
            label31.Visible = false;


            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            bunifuMetroTextbox11.Visible = false;
            bunifuFlatButton23.Visible = false;
            bunifuFlatButton22.Visible = false;

            linkLabel9.Visible = false;
            linkLabel10.Visible = false;
            pictureBox12.Visible = false;
            label24.Visible = false;
            this.Size = new Size(937, 545);
            bunifuThinButton210.Visible = false;
            bunifuThinButton211.Visible = false;
            label18.Visible = false;
            bunifuThinButton212.Visible = false;
            bunifuThinButton26.Visible = false;
            hide();
            hide_edit_buttons();
            hide_edit();
            button1.Visible = false;
            button2.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label14.Visible = false;
            label16.Visible = false;
            label13.Visible = false;
            axAcroPDF2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            login_textbox.Visible = false;
            password_textbox.Visible = false;
            bunifuThinButton22.Visible = false;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            bunifuMetroTextbox1.Visible = true;
            bunifuMetroTextbox1.Visible = true;
            bunifuMetroTextbox2.Visible = true;
            bunifuMetroTextbox3.Visible = true;
            bunifuMetroTextbox4.Visible = true;
            bunifuMetroTextbox5.Visible = true;
            bunifuMetroTextbox6.Visible = true;
            bunifuCheckbox2.Visible = true;
            label10.Visible = true;
            linkLabel1.Visible = true;
            bunifuThinButton21.Visible = true;
            label9.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
        }

        private void bunifuThinButton212_Click(object sender, EventArgs e)
        {
            bunifuThinButton23.Visible = true;
            bunifuThinButton26.Visible = false;
            bunifuThinButton212.Visible = false;
            label18.Visible = false;
            linkLabel6.Visible = true;
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel6.Visible = true;
            linkLabel8.Visible = false;
            linkLabel7.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            axAcroPDF2.Visible = false;
            pictureBox10.Visible = true;
            show_main_buttons();
        }

        private void pictureBox11_MouseDown(object sender, MouseEventArgs e)
        {
            textBox3.Text = String.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sort.getElements();
            Sort.sort("Alphabetical");
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Size = new Size(937, 545);
            linkLabel10.Visible = false;
            linkLabel9.Visible = true;
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Size = new Size(1222, 545);
            linkLabel10.Location = linkLabel9.Location;
            linkLabel9.Visible = false;
            linkLabel10.Visible = true;
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[0] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[0] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[0] == null || User_projectTable.projectw[0] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(0)) + 1, 0);
                User_projectTable.projectw[0] = 1;
            }
            String ss = bunifuFlatButton4.Text;
            ss = ss.Remove(0, 1);
                if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(0)))
                {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(0));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(0));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(0));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(0));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(0));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(0));
                    axAcroPDF2.src = @s;
                    if (Sort.Link_picOnButtons.GetValue(0) == null || Sort.Link_picOnButtons.GetValue(0) == String.Empty)
                    {
                        PDFDocument pdfDoc = new PDFDocument();
                        pdfDoc.LoadPDF(@s);
                        Bitmap jpgImage = pdfDoc.ToImage(0);
                        jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                        pictureBox10.ImageLocation = "img.jpg";
                    }
                    else
                    {
                        pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(0));
                    }
                    linkLabel7.Visible = false;

                    label13.Visible = true;
                    label14.Visible = true;
                    label15.Visible = true;
                    axAcroPDF2.Visible = false;
                    pictureBox10.Visible = true;
                switcher = 1;
                }
            
        }

        private void bunifuFlatButton11_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[1] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[1] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[1] == null || User_projectTable.projectw[1] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(1)) + 1, 1);
                User_projectTable.projectw[1] = 1;
            }
            String ss = bunifuFlatButton11.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(1)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(1));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(1));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(1));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(1));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(1));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(1));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(1) == null || Sort.Link_picOnButtons.GetValue(1) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(1));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 2;
            }
        }

        private void bunifuFlatButton12_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[2] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[2] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[2] == null || User_projectTable.projectw[2] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(2)) + 1, switcher);
                User_projectTable.projectw[2] = 1;
            }
            String ss = bunifuFlatButton12.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(2)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(2));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(2));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(2));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(2));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(2));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(2));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(2) == null || Sort.Link_picOnButtons.GetValue(2) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(2));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 3;
            }
        }

        private void bunifuFlatButton13_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[3] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[3] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[3] == null || User_projectTable.projectw[3] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(3)) + 1, 3);
                User_projectTable.projectw[3] = 1;
            }
            String ss = bunifuFlatButton13.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(3)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(3));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(3));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(3));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(3));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(3));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(3));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(3) == null || Sort.Link_picOnButtons.GetValue(3) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(3));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 4;
            }
        }

        private void bunifuFlatButton14_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[4] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[4] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[4] == null || User_projectTable.projectw[4] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(switcher)) + 1, 4);
                User_projectTable.projectw[4] = 1;
            }
            String ss = bunifuFlatButton14.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(4)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(4));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(4));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(4));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(4));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(4));

                if (Sort.Link_picOnButtons.GetValue(4) == null || Sort.Link_picOnButtons.GetValue(4) == String.Empty)
                {
                    String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(4));
                    axAcroPDF2.src = @s;
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(4));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 5;
            }
        }

        private void bunifuFlatButton15_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[5] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[5] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[5] == null || User_projectTable.projectw[5] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(5)) + 1, 5);
                User_projectTable.projectw[5] = 1;
            }
            String ss = bunifuFlatButton15.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(5)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(5));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(5));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(5));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(5));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(5));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(5));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(5) == null || Sort.Link_picOnButtons.GetValue(5) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(5));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 6;
            }
        }

        private void bunifuFlatButton16_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[6] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[6] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[6] == null || User_projectTable.projectw[6] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(6)) + 1, 6);
                User_projectTable.projectw[6] = 1;
            }
            String ss = bunifuFlatButton16.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(6)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(6));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(6));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(6));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(6));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(6));
                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(6));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(6) == null || Sort.Link_picOnButtons.GetValue(6) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(6));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 7;
            }
        }

        private void bunifuFlatButton17_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[7] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[7] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[7] == null || User_projectTable.projectw[7] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(7)) + 1, 7);
                User_projectTable.projectw[7] = 1;
            }
            String ss = bunifuFlatButton17.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(7)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(7));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(7));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(7));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(7));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(7));

                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(7));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(7) == null || Sort.Link_picOnButtons.GetValue(7) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(7));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 8;
            }
        }

        private void bunifuFlatButton18_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[8] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[8] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[8] == null || User_projectTable.projectw[8] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(8)) + 1, 8);
                User_projectTable.projectw[8] = 1;
            }
            String ss = bunifuFlatButton4.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(8)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(8));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(8));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(8));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(8));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(8));

                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(8));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(8) == null || Sort.Link_picOnButtons.GetValue(8) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(8));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 9;
            }
        }

        private void bunifuFlatButton19_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[9] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[9] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[9] == null || User_projectTable.projectw[9] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(9)) + 1, 9);
                User_projectTable.projectw[9] = 1;
            }
            String ss = bunifuFlatButton19.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(9)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(9));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(9));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(9));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(9));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(9));

                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(9));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(9) == null || Sort.Link_picOnButtons.GetValue(9) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(9));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 10;
            }
        }

        private void bunifuFlatButton20_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[10] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[10] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[10] == null || User_projectTable.projectw[10] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(10)) + 1, 10);
                User_projectTable.projectw[10] = 1;
            }
            String ss = bunifuFlatButton20.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(10)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(10));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(10));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(10));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(10));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(10));

                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(10));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(10) == null || Sort.Link_picOnButtons.GetValue(10) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(10));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 11;
            }
        }

        private void bunifuFlatButton21_Click(object sender, EventArgs e)
        {
            if (User_projectTable.project[11] == 1)
            {
                Globals.liked = true;
                Globals.disliked = false;
                pictureBox6.Image = pictureBox7.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else if (User_projectTable.project[11] == 0)
            {
                Globals.liked = false;
                Globals.disliked = false;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBoxTemporary2.Image;
            }
            else
            {
                Globals.liked = false;
                Globals.disliked = true;
                pictureBox6.Image = pictureBoxTemporary.Image;
                pictureBox9.Image = pictureBox8.Image;
            }

            if (User_projectTable.projectw[11] == null || User_projectTable.projectw[11] == 0)
            {
                Sort.viewsOnButtons.SetValue(Convert.ToInt32(Sort.viewsOnButtons.GetValue(11)) + 1, 11);
                User_projectTable.projectw[11] = 1;
            }
            String ss = bunifuFlatButton21.Text;
            ss = ss.Remove(0, 1);
            if (ss == Convert.ToString(Sort.topicOnButtons.GetValue(11)))
            {
                label13.Text = Convert.ToString(Sort.topicOnButtons.GetValue(11));
                label14.Text = Convert.ToString(Sort.authorOnButtons.GetValue(11));
                label12.Text = Convert.ToString(Sort.descriptionOnButtons.GetValue(11));
                label16.Text = Convert.ToString(Sort.likesOnButtons.GetValue(11));
                label24.Text = Convert.ToString(Sort.viewsOnButtons.GetValue(11));

                String s = Convert.ToString(Sort.Link_PDFOnButtons.GetValue(11));
                axAcroPDF2.src = @s;
                if (Sort.Link_picOnButtons.GetValue(11) == null || Sort.Link_picOnButtons.GetValue(11) == String.Empty)
                {
                    PDFDocument pdfDoc = new PDFDocument();
                    pdfDoc.LoadPDF(@s);
                    Bitmap jpgImage = pdfDoc.ToImage(0);
                    jpgImage.Save("img.jpg", ImageFormat.Jpeg);
                    pictureBox10.ImageLocation = "img.jpg";
                }
                else
                {
                    pictureBox10.ImageLocation = Convert.ToString(Sort.Link_picOnButtons.GetValue(11));
                }
                linkLabel7.Visible = false;

                label13.Visible = true;
                label14.Visible = true;
                label12.Visible = true;
                axAcroPDF2.Visible = false;
                pictureBox10.Visible = true;
                switcher = 12;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(current_username != "")if(current_username != "")User_projectTable.sendNew(current_username);
            if (db.State == ConnectionState.Closed) db.Open();
            for (int j = 0; j < Sort.topicOnButtons.Length; j++)
            {
                if (Convert.ToString(Sort.topicOnButtons.GetValue(j)) == null || Convert.ToString(Sort.topicOnButtons.GetValue(j)) == "")
                    break;
                for (int i = 0; i < Sort.topic.Length; i++)
                {
                    if (Convert.ToString(Sort.topic.GetValue(i)) == Convert.ToString(Sort.topicOnButtons.GetValue(j)))
                    {
                        String str = "update projects set Views = " + Sort.viewsOnButtons.GetValue(j) + " where ID = " + i;
                        SqlCommand cmd = new SqlCommand(str, db);
                        cmd.ExecuteNonQuery();
                        break;
                    }
                }
            }
            if (comboBox1.SelectedItem == "За алфавітом")
            {
                Sort.getElements();
                Sort.sort("Alphabetical");
                switcher = 0;
                switchProject();
            }

            else if (comboBox1.SelectedItem == "За рейтингом")
            {
                Sort.getElements();
                Sort.sort("Rating");
                switcher = 0;
                switchProject();
            }
            User_projectTable.getElements(Sort.topicOnButtons, current_username);
            for (int i = 0; i < Sort.topicOnButtons.Length; i++)
            {
                if (Convert.ToString(Sort.topicOnButtons.GetValue(i)) == Convert.ToString(Sort.topicOnButtons.GetValue(i+1)))
                {
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 2), i + 1);
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 3), i + 2);
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 4), i + 3);
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 5), i + 4);
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 6), i + 5);
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 7), i + 6);
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 8), i + 7);
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 9), i + 8);
                    break;
                }
            }
            if (Sort.topicOnButtons.GetValue(0) != null)
            {
                bunifuFlatButton4.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(0));
                bunifuFlatButton4.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(1) != null )
            {
                bunifuFlatButton11.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(1));
                bunifuFlatButton11.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(2) != null )
            {
                bunifuFlatButton12.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(2));
                bunifuFlatButton12.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(3) != null )
            {
                bunifuFlatButton13.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(3));
                bunifuFlatButton13.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(4) != null )
            {
                bunifuFlatButton14.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(4));
                bunifuFlatButton14.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(5) != null )
            {
                bunifuFlatButton15.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(5));
                bunifuFlatButton15.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(6) != null )
            {
                bunifuFlatButton16.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(6));
                bunifuFlatButton16.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(7) != null)
            {
                bunifuFlatButton17.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(7));
                bunifuFlatButton17.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(8) != null)
            {
                bunifuFlatButton18.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(8));
                bunifuFlatButton18.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(9) != null)
            {
                bunifuFlatButton19.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(9));
                bunifuFlatButton19.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(10) != null)
            {
                bunifuFlatButton20.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(10));
                bunifuFlatButton20.Visible = true;
            }
            if (Sort.topicOnButtons.GetValue(11) != null)
            {
                bunifuFlatButton21.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(11));
                bunifuFlatButton21.Visible = true;
            }
            bunifuFlatButton4_Click(sender,e);
        }

        private void bunifuFlatButton22_MouseDown(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            bunifuFlatButton22.Focus();
        }

        private void bunifuFlatButton23_MouseDown(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            bunifuFlatButton23.Focus();
        }

        private void bunifuFlatButton23_Click(object sender, EventArgs e)
        {
        }

        private void bunifuFlatButton22_Click_1(object sender, EventArgs e)
        {
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            String[] words = bunifuMetroTextbox11.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //for (int i = 0; i < wor; i++)
            //{
            //int g = 0;
            //for (int i = 0; i < words.Length; i++)
            //{
            //    words[words.Length + g] = ;
            //    g++
            //}
            //}
            for (int i = 0; i < Sort.topicOnButtons.Length; i++)
            {
                Sort.topicOnButtons.SetValue(null, i);
                Sort.authorOnButtons.SetValue(null, i);
                Sort.descriptionOnButtons.SetValue(null, i);
                Sort.likesOnButtons.SetValue(null, i);
                Sort.Link_PDFOnButtons.SetValue(null, i);
                Sort.Link_pic.SetValue(null, i);
                Sort.viewsOnButtons.SetValue(null, i);
            }

            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    Sort.find(words[i], "Alphabetical");
                }
            }
            catch
            {
                MessageBox.Show("Нічого не знайдено");
            }

            for (int i = 0; i < Sort.topicOnButtons.Length; i++)
            {
                if (Convert.ToString(Sort.topicOnButtons.GetValue(i)) == Convert.ToString(Sort.topicOnButtons.GetValue(i + 1)))
                {
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 2), i + 1);
                    Sort.topicOnButtons.SetValue(Sort.topicOnButtons.GetValue(i + 3), i + 2);
                    break;
                }
            }

            if (Sort.topicOnButtons.GetValue(0) != null)
            {
                bunifuFlatButton4.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(0));
                bunifuFlatButton4.Visible = true;
            }
            else
                bunifuFlatButton4.Visible = false;

            if (Sort.topicOnButtons.GetValue(1) != null)
            {
                bunifuFlatButton11.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(1));
                bunifuFlatButton11.Visible = true;
            }
            else
                bunifuFlatButton11.Visible = false;

            if (Sort.topicOnButtons.GetValue(2) != null)
            {
                bunifuFlatButton12.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(2));
                bunifuFlatButton12.Visible = true;
            }
            else
                bunifuFlatButton12.Visible = false;

            if (Sort.topicOnButtons.GetValue(3) != null)
            {
                bunifuFlatButton13.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(3));
                bunifuFlatButton13.Visible = true;
            }
            else
                bunifuFlatButton13.Visible = false;

            if (Sort.topicOnButtons.GetValue(4) != null)
            {
                bunifuFlatButton14.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(4));
                bunifuFlatButton14.Visible = true;
            }
            else
                bunifuFlatButton14.Visible = false;

            if (Sort.topicOnButtons.GetValue(5) != null)
            {
                bunifuFlatButton15.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(5));
                bunifuFlatButton15.Visible = true;
            }
            else
                bunifuFlatButton15.Visible = false;

            if (Sort.topicOnButtons.GetValue(6) != null)
            {
                bunifuFlatButton16.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(6));
                bunifuFlatButton16.Visible = true;
            }
            else
                bunifuFlatButton16.Visible = false;

            if (Sort.topicOnButtons.GetValue(7) != null)
            {
                bunifuFlatButton17.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(7));
                bunifuFlatButton17.Visible = true;
            }
            else
                bunifuFlatButton17.Visible = false;

            if (Sort.topicOnButtons.GetValue(8) != null)
            {
                bunifuFlatButton18.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(8));
                bunifuFlatButton18.Visible = true;
            }
            else
                bunifuFlatButton18.Visible = false;

            if (Sort.topicOnButtons.GetValue(9) != null)
            {
                bunifuFlatButton19.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(9));
                bunifuFlatButton19.Visible = true;
            }
            else
                bunifuFlatButton19.Visible = false;

            if (Sort.topicOnButtons.GetValue(10) != null)
            {
                bunifuFlatButton20.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(10));
                bunifuFlatButton20.Visible = true;
            }
            else
                bunifuFlatButton20.Visible = false;

            if (Sort.topicOnButtons.GetValue(11) != null)
            {
                bunifuFlatButton21.Text = " " + Convert.ToString(Sort.topicOnButtons.GetValue(11));
                bunifuFlatButton21.Visible = true;
            }
            else
                bunifuFlatButton21.Visible = false ;

            switcher = 0;
            switchProject();
            wasSerached = true;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (wasSerached)
            {
                bunifuMetroTextbox11.Text = "Введіть запит";
                label14.Focus();
                
                comboBox1.Visible = true;
                comboBox1.SelectedIndex = 0;
                comboBox1.SelectedIndex = 1;
                
            }
            else
            {
                bunifuMetroTextbox11.Text = "Введіть запит";
                label14.Focus();
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {
            label12.Focus();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pictureBox10.Focus();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Focus();
        }

        private void bunifuMetroTextbox11_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void bunifuMetroTextbox11_OnValueChanged(object sender, EventArgs e)
        {
           
        }

        private void bunifuMetroTextbox11_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (bunifuMetroTextbox11.Text == "Введіть запит")
            {
                bunifuMetroTextbox11.Text = String.Empty + Convert.ToString(e)[0];
                bunifuMetroTextbox11.Text = bunifuMetroTextbox11.Text.Remove(bunifuMetroTextbox11.Text.LastIndexOf('S'));

            }
            else
                if (e.KeyCode == Keys.Enter)
            {
                pictureBox14_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}

