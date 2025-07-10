using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace DatabaseConfiguration
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

            CommClass.filePath = "D://PRIYA.CHOTHANI//dbconfig3.txt";

            if (!File.Exists(CommClass.filePath))
            {
                Form1 f1 = new Form1();
                f1.ShowDialog();
                if (f1.DialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                CommClass.Connection = File.ReadAllText(CommClass.filePath);
                SqlConnection con = new SqlConnection(CommClass.Connection);
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    Form1 f1 = new Form1();
                    f1.ShowDialog();
                    if (f1.DialogResult == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                finally
                {
                    con.Close();
                }
            }
            
            Application.Run(new LoginForm());
        }

    }
}

