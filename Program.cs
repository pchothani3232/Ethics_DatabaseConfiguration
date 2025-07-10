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

            string filePath = "D://PRIYA.CHOTHANI//dbconfig3.txt";

            CommClass CC = new CommClass();

            if (!File.Exists(filePath))
            {
                Form1 f1 = new Form1();
                f1.ShowDialog();
            }
            CC.Connection = File.ReadAllText(filePath);
            SqlConnection con = new SqlConnection(CC.Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("LoginCredential", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@status", "test");
            int test = cmd.ExecuteNonQuery();
            con.Close();
            if (test != 1)
            {
                Form1 f1 = new Form1();
                f1.ShowDialog();
            }

            Application.Run(new LoginForm());

        }
    }
}

