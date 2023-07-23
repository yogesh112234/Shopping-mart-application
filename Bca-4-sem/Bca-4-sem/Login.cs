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
    public partial class Login : Form
    {
        public static string username = "";
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Login()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBox1.Checked;
            switch (check)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox1.Text) == true)
           // {
              //  textBox1.Focus();
              //  errorProvider1.SetError(this.textBox1, "please fill these field");
          //  }
           // else
          //  {
               // errorProvider1.Clear();
           // }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          /*  if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "please fill these field");
            }
            else
            {
                errorProvider1.Clear();
            }*/
        }

        private void Login_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
             {
              textBox2.Focus();
              errorProvider2.SetError(this.textBox2, "please fill these field");
             }
             else
             {
             errorProvider2.Clear();
             }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "please fill these field");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "please fill these field");
            }

            else if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "please fill these field");
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from singup where name = @user and password = @pass";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    MessageBox.Show("Login successful", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    username = textBox1.Text;
                    this.Hide();
                    Form1 Mainform = new Form1();
                    Mainform.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Login failed", "unsuccess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            singup su = new singup();
            this.Hide();
            su.ShowDialog();
        }
    }
}
