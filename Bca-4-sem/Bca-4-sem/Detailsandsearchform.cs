using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Bca_4_sem
{
    public partial class Detailsandsearchform : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Detailsandsearchform()
        {
            InitializeComponent();
            BindGridView();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "getdataforinnerjoinfinal";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

        }

        private void searchinvoicebutton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "getdataforinnerjoinfinal_byinvoice";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@invoiceId", txtsearchbyinvoice.Text);
            SqlDataAdapter sda = new SqlDataAdapter();
            //sda.SelectCommand.Parameters.AddWithValue(" @invoiceId",txtsearchbyinvoice.Text);
            sda.SelectCommand = cmd;
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            txtsearchbyinvoice.Focus();
        }

        private void searchdatetimebutton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "datetimes";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstdate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@seconddate", dateTimePicker2.Value);
            SqlDataAdapter dd = new SqlDataAdapter();
            dd.SelectCommand = cmd;
            DataTable data = new DataTable();
            dd.Fill(data);
            dataGridView1.DataSource = data;
           
        }

        private void resetbutton1_Click(object sender, EventArgs e)
        {
            BindGridView(); 
        }
    }
}
