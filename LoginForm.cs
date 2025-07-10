using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace DatabaseConfiguration
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }


        //string filePath = "D://PRIYA.CHOTHANI//dbconfig2.txt";

        private void LoginForm_Load(object sender, EventArgs e)
        {
             
        }

        private void txtUnm_TextChanged(object sender, EventArgs e)
        {

        }

        //Login -Button
        private void btnLogin_Click(object sender, EventArgs e)
        
        {
            string connStr = File.ReadAllText("D://PRIYA.CHOTHANI//dbconfig2.txt");

            SqlConnection con = new SqlConnection(connStr);

            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed:\n" + ex.Message);
                return;
            }


            //string connStr = File.ReadAllText("D://PRIYA.CHOTHANI//dbconfig2.txt");
            //MessageBox.Show(connStr); // For debugging

            
            con.Open();

            SqlCommand cmd = new SqlCommand("LoginCredential", con);  //LoginCredential = stored procedure name
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", txtUnm.Text);
            cmd.Parameters.AddWithValue("@Password", txtPwd.Text);
            cmd.Parameters.AddWithValue("@status", "Select");
            cmd.ExecuteNonQuery();

            MessageBox.Show("Login Successful");


        }
    }
}
 
