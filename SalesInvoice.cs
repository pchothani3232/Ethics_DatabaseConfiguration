using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseConfiguration
{
    public partial class SalesInvoice : Form
    {
        SqlConnection con = new SqlConnection(CommClass.Connection);
        public SalesInvoice()
        {
            InitializeComponent();

        }

        private void SalesInvoice_Load(object sender, EventArgs e)
        {
            fillCombobox();
            AutogenerateCode();
        }


        void fillCombobox()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("StateMaster_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", "select");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbState.DataSource = dt;
            cmbState.DisplayMember = "StateName";  
            //cmbState.ValueMember = "Id";
            cmbState.SelectedIndex = -1; // optional, to clear selection
            con.Close();
        }

        void AutogenerateCode()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SalesInvoice_Sp", con);  //LoginCredential = stored procedure name
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", "SalesInvoiceNumber");
            SqlParameter p = new SqlParameter("@SalesInvoiceNumber", SqlDbType.Decimal);
            p.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p);
            cmd.ExecuteNonQuery(); // Run the stored procedure

            // Show result in textbox
            txtSalesInvoiceNo.Text = p.Value.ToString();
        }


        //Search by CustomerName and MobileNumber
        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtCustomerName.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                SqlCommand cmd = new SqlCommand("SalesInvoice_Sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Status", "SearchByNameAndMobileNo");
                cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@MobileNumber", txtCustomerName.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.Visible = true;
                con.Close();
            }
            else
            {              
                dataGridView1.Visible = false;               
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                txtCustomerName.TextChanged -= txtCustomerName_TextChanged;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtCustomerName.Text = row.Cells["Customer_Name"].Value?.ToString() ?? string.Empty;
                txtCustomerMobileNo.Text = row.Cells["Customer_PhoneNumber"].Value?.ToString() ?? string.Empty;
                txtCustomerAddress.Text = row.Cells["Customer_Address"].Value?.ToString() ?? string.Empty;
                cmbState.Text = row.Cells["Customer_State"].Value?.ToString() ?? string.Empty;
                txtCustomerName.TextChanged += txtCustomerName_TextChanged;
                dataGridView1.Visible = false;
            }
            

            //if (dataGridView1.CurrentRow.Index != -1)
            //{
            //    DataGridViewRow row1 = dataGridView1.Rows[e.RowIndex];


            //    txtCustomerName.Text = row1.Cells["Customer_Name"].Value.ToString();
            //    txtCustomerAddress.Text = row1.Cells["Customer_Address"].Value.ToString();
            //    txtCustomerMobileNo.Text = row1.Cells["Customer_PhoneNumber"].Value.ToString();
            //    cmbState.Text = row1.Cells["Customer_State"].Value.ToString();

            //    dataGridView1.Visible = false;

            //    //txtCustomerName.Text = dataGridView1.CurrentRow.Cells["Customer_Name"].Value.ToString();
            //    //txtCustomerAddress.Text = dataGridView1.CurrentRow.Cells["Customer_Address"].Value.ToString();
            //    //txtCustomerMobileNo.Text = dataGridView1.CurrentRow.Cells["Customer_PhoneNumber"].Value.ToString();
            //    //cmbState.Text = dataGridView1.CurrentRow.Cells["Customer_State"].Value.ToString();

            //}
        }
    }
}
