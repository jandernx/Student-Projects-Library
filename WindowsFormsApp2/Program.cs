using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Queries_my_projects.user_projects();
            //Queries_my_projects.get_projects("janderr97");
            //Update.update_project(5, @"dsdasdasd", @"sadasd", @"4646465", @"21312dsad");
            //Sort.getElements();
            //Sort.sort("Rating");
            //User_projectTable.getElements("me", Sort.topicOnButtons);
        }
    }
}
