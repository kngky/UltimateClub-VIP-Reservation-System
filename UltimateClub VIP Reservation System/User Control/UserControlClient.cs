using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateClub_VIP_Reservation_System.User_Control
{
    public partial class UserControlClient : Form
    {
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBHelper dbHelper = null;
        SqlConnection conn = null;
        public UserControlClient()
        {
            InitializeComponent();
            dbHelper = new DBHelper();
            conn = dbHelper.conn;
            LoadClient();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        public void Clear()
        {
            textBoxFirstname.Clear();
            textBoxLastname.Clear();
            textBoxContactNumber.Clear();
            textBoxEmailAddress.Clear();

            /*btnSave.Enabled = true;
            btnUpdate.Enabled = false;*/
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to save this client?", "Save product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("INSERT INTO ClientTabe( Firstname, Lastname, ContactNo, EmailAddress) VALUES ( @Firstname, @Lastname, @ContactNo, @EmailAddress)", conn);
                    cm.Parameters.AddWithValue("@Firstname", textBoxFirstname.Text);
                    cm.Parameters.AddWithValue("@Lastname", textBoxLastname.Text);
                    cm.Parameters.AddWithValue("@ContactNo", int.Parse(textBoxContactNumber.Text));
                    cm.Parameters.AddWithValue("@EmailAddress", textBoxEmailAddress.Text);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Client has been successfully added!");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadClient()
        {
            int i = 0;
            dataGridViewClient.Rows.Clear();
            conn.Open();
            cm = new SqlCommand("SELECT * FROM ClientTabe WHERE ClientID = '" + textBoxReservationSearchContactNo.Text + "'", conn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridViewClient.Rows.Add(i, dr["ClientID"].ToString(),i, dr["Firstname"].ToString(),i, dr["Lastname"].ToString(),i, dr["ContactNo"].ToString(),i, dr["EmailAddress"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int i = 0;
            dataGridViewClient.Rows.Clear();
            conn.Open();
            cm = new SqlCommand("SELECT * FROM ClientTabe WHERE ContactNo = '" + textBoxReservationSearchContactNo.Text + "'", conn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridViewClient.Rows.Add(dr["ClientID"].ToString(), dr["Firstname"].ToString(), dr["Lastname"].ToString(), dr["ContactNo"].ToString(), dr["EmailAddress"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to update this Client?", "Update Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE ClientTabe SET Firstname = @Firstname, Lastname = @Lastname, ContactNo = @ContactNo, EmailAddress = @EmailAddress WHERE ClientID = @ClientID", conn);
                    cm.Parameters.AddWithValue("@ClientiD", lblClientID_.Text);
                    cm.Parameters.AddWithValue("@Firstname", txtFirstName.Text);
                    cm.Parameters.AddWithValue("@Lastname", txtLastName.Text);
                    cm.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                    cm.Parameters.AddWithValue("@EmailAddress", txtEmailAddress.Text);
                    conn.Open();
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Client has been successfully updated.");
                    Clear();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to remove this client?", "Delete Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("INSERT INTO ArchivedClients( AFirstname, ALastname, AContactNo, AEmailAddress) VALUES ( @AFirstname, @ALastname, @AContactNo, @AEmailAddress)", conn);
                    cm.Parameters.AddWithValue("@AFirstname", txtFirstName.Text);
                    cm.Parameters.AddWithValue("@ALastname", txtLastName.Text);
                    cm.Parameters.AddWithValue("@AContactNo", int.Parse(txtContactNo.Text));
                    cm.Parameters.AddWithValue("@AEmailAddress", txtEmailAddress.Text);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    Clear();

                    conn.Open();
                    cm = new SqlCommand("DELETE FROM ClientTabe WHERE ClientID = @ClientID", conn);
                    cm.Parameters.AddWithValue("@ClientID", lblClientID_.Text);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Client has been successfully deleted!");
                    Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewClient.Rows[e.RowIndex];
                lblClientID_.Text = row.Cells[0].Value.ToString();
                txtFirstName.Text = row.Cells[1].Value.ToString();
                txtLastName.Text = row.Cells[2].Value.ToString();
                txtContactNo.Text = row.Cells[3].Value.ToString();
                txtEmailAddress.Text = row.Cells[4].Value.ToString();
                conn.Close();
            }
        }

        private void btnSearchReservation_Click(object sender, EventArgs e)
        {

        }
    }
}
