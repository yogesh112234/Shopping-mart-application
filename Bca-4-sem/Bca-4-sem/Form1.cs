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
    public partial class Form1 : Form
    {
        int FinalCost = 0;
        int SrNo = 0;
        int tax = 0;
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            GetInvoiceId();
            txtuser.Text = Login.username;
            GetItems();
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "SR NO";
            dataGridView1.Columns[1].Name = "ITEM NAME";
            dataGridView1.Columns[2].Name = "UNIT PRICE";
            dataGridView1.Columns[3].Name = "DISCOUNT PER ITEM";
            dataGridView1.Columns[4].Name = "QUANTITY";
            dataGridView1.Columns[5].Name = "SUB TOTAL";
            dataGridView1.Columns[6].Name = "TAX";
            dataGridView1.Columns[7].Name = "TOTAL COST";
        }
        void GetItems()
        {
            txtselectitem.Items.Clear();
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from items_tbl";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string item_names = dr.GetString(1);
                txtselectitem.Items.Add(item_names);

            }
            txtselectitem.Sorted = true;
            con.Close();
        }
        void getprice()
        {
            if (txtselectitem.SelectedItem == null)
            {

            }
            else
            {
                int price = 0;
                SqlConnection con = new SqlConnection(cs);
                string query = "select item_price from items_tbl where item_name = @name";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@name", txtselectitem.SelectedItem.ToString());
                DataTable data = new DataTable();
                sda.Fill(data);
                if (data.Rows.Count > 0)
                {
                    price = Convert.ToInt32(data.Rows[0]["item_price"]);
                }
                txtunitprice.Text = price.ToString();
            }
        }
        void getDiscount()
        {
            if (txtselectitem.SelectedItem == null)
            {

            }
            else
            {
                int discount = 0;
                SqlConnection con = new SqlConnection(cs);
                string query = "select item_discount from items_tbl where item_name = @name";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@name", txtselectitem.SelectedItem.ToString());
                DataTable data = new DataTable();
                sda.Fill(data);
                if (data.Rows.Count > 0)
                {
                    discount = Convert.ToInt32(data.Rows[0]["item_discount"]);
                }
                txtdiscountperitem.Text = discount.ToString();
            }
        }

        private void txtselectitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            getprice();
            getDiscount();
            txtquantity.Enabled = true;
        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtquantity.Text) == true)
            {

            }
            else
            {
                int price = Convert.ToInt32(txtunitprice.Text);
                int discount = Convert.ToInt32(txtdiscountperitem.Text);
                int quantity = Convert.ToInt32(txtquantity.Text);
                int subtotal = price * quantity;
                subtotal = subtotal - discount * quantity;
                txtsubtotal.Text = subtotal.ToString();

            }
        }

        private void txtsubtotal_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtsubtotal.Text) == true)
            {

            }
            else
            {
                int subtotal = Convert.ToInt32(txtsubtotal.Text);
                if (subtotal >= 10000)
                {
                    tax = (int)(subtotal * 0.15);
                    txttax.Text = tax.ToString();
                }
                else if (subtotal >= 6000)
                {
                    tax = (int)(subtotal * 0.10);
                    txttax.Text = tax.ToString();
                }
                else if (subtotal >= 3000)
                {
                    tax = (int)(subtotal * 0.07);
                    txttax.Text = tax.ToString();
                }
                else if (subtotal >= 1000)
                {
                    tax = (int)(subtotal * 0.05);
                    txttax.Text = tax.ToString();
                }
                else
                {
                    tax = (int)(subtotal * 0.03);
                    txttax.Text = tax.ToString();
                }
            }
        }

        private void txttax_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txttax.Text) == true)
            {

            }
            else
            {
                int subtotal = Convert.ToInt32(txtsubtotal.Text);
                int tax = Convert.ToInt32(txttax.Text);
                int totalcost = subtotal + tax;
                txttotalcost.Text = totalcost.ToString();
            }
        }
        void AddDataToGridView(string Sr_No,string item_name,string unit_price,string discount,string quantity,string sub_total,string tax,string total_cost)
        {
            string[] row = { Sr_No, item_name, unit_price, discount, quantity, sub_total, tax, total_cost };
            dataGridView1.Rows.Add(row);
        }

        private void addbutton1_Click(object sender, EventArgs e)
        {
            if (txtselectitem.SelectedItem != null)
            {
                AddDataToGridView((++SrNo).ToString(), txtselectitem.SelectedItem.ToString(), txtunitprice.Text, txtdiscountperitem.Text, txtquantity.Text, txtsubtotal.Text, txttax.Text, txttotalcost.Text);
                ResetControls();
                CalculateFinalCost();
            }
            else
            {
                MessageBox.Show("please select items");
            }
        }
        void ResetControls()
        {
            txtselectitem.SelectedItem = null;
            txtunitprice.Clear();
            txtdiscountperitem.Clear();
            txtquantity.Clear();
            txtsubtotal.Clear();
            txttax.Clear();
            txttotalcost.Clear();
            txtfinalcost.Clear();
            txtamountpaid.Clear();
            txtchange.Clear();
            txtquantity.Enabled = false;
        }
        void CalculateFinalCost()
        {
            FinalCost = 0;
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                FinalCost = FinalCost +Convert.ToInt32(dataGridView1.Rows[i].Cells[7].Value);
            }
            txtfinalcost.Text = FinalCost.ToString();
        }

        private void txtamountpaid_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtamountpaid.Text)==true)
            {

            }
            else
            {
                int AmountPaid = Convert.ToInt32(txtamountpaid.Text);
                int FCost = Convert.ToInt32(txtfinalcost.Text);
                int change = AmountPaid - FCost;
                txtchange.Text = change.ToString();
            }
        }

        private void resetbutton2_Click(object sender, EventArgs e)
        {
           
        }

        private void cleargridviewbutton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            SrNo = 0;
        }
        void GetInvoiceId()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select invoice_id from order_master";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            if(data.Rows.Count < 1)
            {
                txtinvoice.Text = "1";
            }
            else
            {
                string query2 = "select max(invoice_id) from order_master";
                SqlCommand cmd = new SqlCommand(query2, con);
                con.Open();
                int a=Convert.ToInt32( cmd.ExecuteScalar());
                a = a + 1;
                txtinvoice.Text = a.ToString();
                con.Close();
            }
        }

        private void insertbutton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into order_master values(@id,@user,@datetime,@finalcost)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtinvoice.Text);
            cmd.Parameters.AddWithValue("@user", txtuser.Text);
            cmd.Parameters.AddWithValue("@datetime",DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@finalcost", txtfinalcost.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("insert successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetInvoiceId();
                ResetControls();
            }
            else
            {
                MessageBox.Show("insert fail", "fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            con.Close();
            InsertIntoOrderDetails();
        }
        int getLastInsertedInvoiceId()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select max(invoice_id) from order_master";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
           int MaxInvoiceId = Convert.ToInt32( cmd.ExecuteScalar());
            con.Close();
            return MaxInvoiceId;
        }
        void InsertIntoOrderDetails()
        {
            int a = 0;
            SqlConnection con = new SqlConnection(cs);
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string query = "insert into order_details values(@invoice_id,@name,@price,@discount,@quantity,@subtotal,@tax,@finalcost)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@invoice_id", getLastInsertedInvoiceId());
                    cmd.Parameters.AddWithValue("@name", dataGridView1.Rows[i].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@price", dataGridView1.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@discount", dataGridView1.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@quantity", dataGridView1.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@subtotal", dataGridView1.Rows[i].Cells[5].Value);
                    cmd.Parameters.AddWithValue("@tax", dataGridView1.Rows[i].Cells[6].Value);
                    cmd.Parameters.AddWithValue("@finalcost", dataGridView1.Rows[i].Cells[7].Value);
                    con.Open();
                    a = a + cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch
            {

            }
            if (a > 0)
            {
                MessageBox.Show("data added in order_details");
            }
            else
            {
                MessageBox.Show("data  not added in order_details");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtquantity_KeyPress(object sender, KeyPressEventArgs e)
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
            else
            {
                e.Handled = true;
            }
        }

        private void txtamountpaid_KeyPress(object sender, KeyPressEventArgs e)
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
            else
            {
                e.Handled = true;
            }
        }

        private void billbutton1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.images_for_project205611;
            Image img = bmp;
            e.Graphics.DrawImage(img, 30, 5,800,250);
            e.Graphics.DrawString("Invoice Id: " + txtinvoice.Text, new Font("Arial",15,FontStyle.Bold),Brushes.Black, new Point(30,300));
            e.Graphics.DrawString("User Name: " + txtuser.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 330));
            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 360));
            e.Graphics.DrawString("Time: " + DateTime.Now.ToLongTimeString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 390));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 420));
            e.Graphics.DrawString("ITEM", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 450));
            e.Graphics.DrawString("UNIT PRICE", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(240, 450));
            e.Graphics.DrawString("DISCOUNT", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(390, 450));
            e.Graphics.DrawString("QUANTITY", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(590, 450));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 480));
            //item name print
            int gap = 510;
            if (dataGridView1.Rows.Count > 0)
            {
                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, gap));
                        gap = gap + 30;
                    }
                    catch
                    {

                    }
                }
            }
            //item price
            int gap1 = 510;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(260, gap1));
                        gap1 = gap1 + 30;
                    }
                    catch
                    {

                    }
                }
            }
            //discount
            int gap2 = 510;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(410, gap2));
                        gap2 = gap2 + 30;
                    }
                    catch
                    {

                    }
                }
            }
            //quantity
            int gap3 = 510;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[4].Value.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(610, gap3));
                        gap3 = gap3 + 30;
                    }
                    catch
                    {

                    }
                }
            }
            //sub total
                int subtotalprint = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    subtotalprint = subtotalprint + Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                }
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 850));
            e.Graphics.DrawString("SUB TOTAL::  " + subtotalprint.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 880));
            //tax

            int taxprint = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                taxprint = taxprint + Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            e.Graphics.DrawString("TAX::  " + taxprint.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 910));
            e.Graphics.DrawString("FINAL AMOUNT::  " + txtfinalcost.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 940));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 970));
            e.Graphics.DrawString("AMOUNT PAID::  " + txtamountpaid.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 1000));
            e.Graphics.DrawString("CHANGE::  " + txtchange.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 1030));
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            additem adf = new additem();
            adf.ShowDialog();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            GetItems();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edititems edf = new edititems();
            edf.ShowDialog();
        }

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewdata vdf = new viewdata();
            vdf.ShowDialog();
        }

        private void detailsAndSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Detailsandsearchform das = new Detailsandsearchform();
            das.ShowDialog();
        }
    }
}
