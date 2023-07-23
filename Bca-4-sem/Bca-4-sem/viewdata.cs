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
    public partial class viewdata : Form
    {
      string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public viewdata()
        {
            InitializeComponent();
            BindGridView();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from items_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            additem adf = new additem();
            adf.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            edititems edf = new edititems();
            edf.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            edititems edf = new edititems();
            edf.ShowDialog();
        }

        private void viewdata_Activated(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
}
