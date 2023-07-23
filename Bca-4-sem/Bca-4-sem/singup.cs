using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;

namespace Bca_4_sem
{
    public partial class singup : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";


        public singup()
        {
            InitializeComponent();
        }

        private void txtname_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtname.Text) == true)
            {
                txtname.Focus();
                errorProvider1.SetError(this.txtname, "please enter name");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtsurname_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtsurname_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtsurname.Text) == true)
            {
                txtsurname.Focus();
                errorProvider2.SetError(this.txtsurname, "please enter name");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void txtgender_Leave(object sender, EventArgs e)
        {
            if (txtgender.SelectedItem == null)
            {
                txtgender.Focus();
                errorProvider3.SetError(this.txtgender, "please eslect gender");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
            {
                numericUpDown1.Focus();
                errorProvider4.SetError(this.numericUpDown1, "please select a class");
            }
            else
            {
                errorProvider4.Clear();
            }
        }

        private void txtaddress_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtaddress.Text) == true)
            {
                txtaddress.Focus();
                errorProvider5.SetError(this.txtaddress, "please enter name");
            }
            else
            {
                errorProvider5.Clear();
            }
        }

        private void txtaddress_KeyPress(object sender, KeyPressEventArgs e)
        {
           char ch = e.KeyChar;
            if (char.IsLetterOrDigit(ch) == true)
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

        private void txtemail_Leave(object sender, EventArgs e)
        {
            if(Regex.IsMatch(txtemail.Text,pattern)==false)
            {
                txtemail.Focus();
                errorProvider6.SetError(this.txtemail, "invalid email");
            }
            else
            {
                errorProvider6.Clear();
            }
        }

        private void txtpassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpassword.Text) == true)
            {
                txtpassword.Focus();
                errorProvider7.SetError(this.txtpassword, "please enter name");
            }
            else
            {
                errorProvider7.Clear();
            }
        }

        private void txtconform_Leave(object sender, EventArgs e)
        {
            if (txtpassword .Text!= txtconform.Text)
            {
                txtconform.Focus();
                errorProvider8.SetError(this.txtconform, "password is not match");
            }
            else
            {
                errorProvider8.Clear();
            }
        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsurname.Focus();
            }
        }

        private void txtsurname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtgender.Focus();
            }
        }

        private void txtgender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                numericUpDown1.Focus();
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtaddress.Focus();
            }
        }

        private void txtaddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpassword.Focus();
            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtconform.Focus();
            }
        }

        private void singupbutton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtname.Text) == true)
            {
                txtname.Focus();
                errorProvider1.SetError(this.txtname, "please enter name");
            }
            else if (string.IsNullOrEmpty(txtsurname.Text) == true)
            {
                txtsurname.Focus();
                errorProvider2.SetError(this.txtsurname, "please enter name");
            }
            else if (txtgender.SelectedItem == null)
            {
                txtgender.Focus();
                errorProvider3.SetError(this.txtgender, "please eslect gender");
            }
            else if (numericUpDown1.Value == 0)
            {
                numericUpDown1.Focus();
                errorProvider4.SetError(this.numericUpDown1, "please select a class");
            }
            else if (string.IsNullOrEmpty(txtaddress.Text) == true)
            {
                txtaddress.Focus();
                errorProvider5.SetError(this.txtaddress, "please enter name");
            }
            else if (Regex.IsMatch(txtemail.Text, pattern) == false)
            {
                txtemail.Focus();
                errorProvider6.SetError(this.txtemail, "invalid email");
            }
            else if (string.IsNullOrEmpty(txtpassword.Text) == true)
            {
                txtpassword.Focus();
                errorProvider7.SetError(this.txtpassword, "please enter name");
            }
            else if (txtpassword.Text != txtconform.Text)
            {
                txtconform.Focus();
                errorProvider8.SetError(this.txtconform, "password is not match");
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "insert into singup values(@name,@surname,@gender,@age,@address,@email,@pass)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@surname", txtsurname.Text);
                cmd.Parameters.AddWithValue("@gender", txtgender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@age", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@pass", txtpassword.Text);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("singup successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Username is:" + txtname.Text + "\n \n" + "password is: " + txtpassword.Text, "success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    Login Loginform = new Login();
                    Loginform.ShowDialog();
                }
                else
                {
                    MessageBox.Show("singup failed", "fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
        }
    }
}
