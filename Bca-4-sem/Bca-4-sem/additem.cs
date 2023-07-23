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
    public partial class additem : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public additem()
        {
            InitializeComponent();
        }

        private void txtitemname_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtitemname.Text) == true)
            {
                txtitemname.Focus();
                errorProvider1.SetError(this.txtitemname, "please enter name0f item");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtitemname_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsLetter(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else if (ch == 32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtprice_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtprice.Text) == true)
            {
                txtprice.Focus();
                errorProvider2.SetError(this.txtprice, "please enter name");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else if (ch == 32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtdiscount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtdiscount.Text) == true)
            {
                txtdiscount.Focus();
                errorProvider3.SetError(this.txtdiscount, "please enter name");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void txtdiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else if (ch == 32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtitemname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtprice.Focus();
            }
        }

        private void txtprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdiscount.Focus();
            }
        }

        private void insertbutton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into items_tbl values(@name,@price,@discount)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", txtitemname.Text);
            cmd.Parameters.AddWithValue("@price", txtprice.Text);
            cmd.Parameters.AddWithValue("@discount", txtdiscount.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Items added successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtitemname.Clear();
                txtprice.Clear();
                txtdiscount.Clear();
                txtitemname.Focus();
            }
            else
            {
                MessageBox.Show("items cant add", "fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtitemname.Clear();
                txtprice.Clear();
                txtdiscount.Clear();
                txtitemname.Focus();
            }
            con.Close();
        }
    }
}
