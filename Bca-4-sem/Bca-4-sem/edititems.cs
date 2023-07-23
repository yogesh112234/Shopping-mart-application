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
    public partial class edititems : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public edititems()
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

        private void edititems_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtitemname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtprice.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtdiscount.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void updatebutton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Update  items_tbl set item_name = @name, item_price = @price, item_discount = @discount where item_id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.Parameters.AddWithValue("@name", txtitemname.Text);
            cmd.Parameters.AddWithValue("@price", txtprice.Text);
            cmd.Parameters.AddWithValue("@discount", txtdiscount.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Items update successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                txtid.Clear();
                txtitemname.Clear();
                txtprice.Clear();
                txtdiscount.Clear();
                txtitemname.Focus();
            }
            else
            {
                MessageBox.Show("items cant update", "fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid.Clear();
                txtitemname.Clear();
                txtprice.Clear();
                txtdiscount.Clear();
                txtitemname.Focus();
            }
            con.Close();
        }

        private void deletebutton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from items_tbl where item_id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Items deleted successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
                txtid.Clear();
                txtitemname.Clear();
                txtprice.Clear();
                txtdiscount.Clear();
                txtitemname.Focus();
            }
            else
            {
                MessageBox.Show("items cant deleted", "fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtid.Clear();
                txtitemname.Clear();
                txtprice.Clear();
                txtdiscount.Clear();
                txtitemname.Focus();
            }
            con.Close();
        }
    }
}
