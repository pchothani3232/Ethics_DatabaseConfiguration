using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DatabaseConfiguration
{
    public partial class ListPage : Form
    {
        SqlConnection con = new SqlConnection(CommClass.Connection);
       
        public ListPage()
        {
            InitializeComponent();
        }

        private void ListPage_Load(object sender, EventArgs e)
        {
            //fillgrid();
        }

        public void fillgrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Datalist", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Status", "Select");
            //cmd.Parameters.AddWithValue("@Fromdate", dtpForm.Value.Date);
            //cmd.Parameters.AddWithValue("@Todate", dtpTo.Value.Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //Search form date....To date
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Datalist",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", "DateBySearchData");
            cmd.Parameters.AddWithValue("@Fromdate", dtpForm.Value.Date);
            cmd.Parameters.AddWithValue("@Todate", dtpTo.Value.Date);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

       
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillgrid();
        }

        //Search Name
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Datalist", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Status", "SearchCustomerName");
            cmd.Parameters.AddWithValue("@CustomerName", txtNameSearch.Text);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            
        }

      
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //this.Hide();
            SalesInvoice s1 = new SalesInvoice();
            s1.ShowDialog();

        }
    }
}
