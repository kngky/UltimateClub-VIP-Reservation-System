using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateClub_VIP_Reservation_System.User_Control
{
    public partial class Reservation : Form
    {
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBHelper dbHelper = null;
        SqlConnection conn = null;
        public Reservation()
        {
            InitializeComponent();
            dbHelper = new DBHelper();
            conn = dbHelper.conn;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtClientContactNo_TextChanged(object sender, EventArgs e)
        {
            conn.Open();
            cm = new SqlCommand("SELECT Firstname, Lastname FROM ClientTabe Where ContactNo = '" + txtClientContactNo.Text + "'", conn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                string firstname = dr["Firstname"].ToString();
                string lastname = dr["Lastname"].ToString();
                
                lblFirstname.Text = firstname;
                lblLastname.Text = lastname;
            }
            dr.Close();
            conn.Close();
        }

        public void Clear()
        {
            lblLastname.Text = string.Empty;
            lblLastname.Text = string.Empty;
            txtCategory.Clear();
            txtClientContactNo.Clear();
            txtRateFee.Clear();
            dtpDATE.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to add this reservation?", "Save reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("INSERT INTO ReservationTable( RClientFirstname, RClientLastName, Category, RClientContactNo, RateFee, Date, Status) " +
                        "VALUES ( @RClientFirstname, @RClientLastName, @Category, @RClientContactNo, @RateFee, @Date, @Status)", conn);
                    cm.Parameters.AddWithValue("@RClientFirstname", lblLastname.Text);
                    cm.Parameters.AddWithValue("@RClientLastName", lblFirstname.Text);
                    cm.Parameters.AddWithValue("@Category", txtCategory.Text);
                    cm.Parameters.AddWithValue("@RClientContactNo", txtClientContactNo.Text);
                    cm.Parameters.AddWithValue("@RateFee", int.Parse(txtRateFee.Text));
                    cm.Parameters.AddWithValue("@Date", dtpDATE.Value.Date);
                    cm.Parameters.AddWithValue("@Status", "UNPAID");
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Reservation has been successfully added!");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchReservation_Click(object sender, EventArgs e)
        {
            int i = 0;
            dgvReservation.Rows.Clear();
            conn.Open();
            cm = new SqlCommand("SELECT * FROM ReservationTable WHERE RClientContactNo = '" + textBoxSearchContactNoReservation.Text + "'", conn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvReservation.Rows.Add(dr["ReservationID"].ToString(), dr["RClientFirstname"].ToString(), dr["RClientLastname"].ToString(), 
                    dr["Category"].ToString(), dr["RClientContactNo"].ToString(), dr["RateFee"].ToString(), dr["Date"].ToString(), dr["Status"].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void dataGridViewClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvReservation.Rows[e.RowIndex];
                lblRReservationID_.Text = row.Cells[0].Value.ToString();
                lblClientFirstname.Text = row.Cells[1].Value.ToString();
                lblClientLastName.Text = row.Cells[2].Value.ToString();
                textCategory.Text = row.Cells[3].Value.ToString();
                textClientContactNo.Text = row.Cells[4].Value.ToString();
                textRateFee.Text = row.Cells[5].Value.ToString();
                dtpDDate.Text = row.Cells[6].Value.ToString();
                cboStatus.Text = row.Cells[7].Value.ToString();
                conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to update this Reservation?", "Update Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE ReservationTable SET RClientFirstname = @RClientFirstname, RClientLastName = @RClientLastName, Category = @Category, " +
                        "RClientContactNo = @RClientContactNo, RateFee = @RateFee, Date = @Date, Status = @Status WHERE ReservationID = @ReservationID", conn);
                    cm.Parameters.AddWithValue("@RClientFirstname", lblClientFirstname.Text);
                    cm.Parameters.AddWithValue("@RClientLastName", lblClientLastName.Text);
                    cm.Parameters.AddWithValue("@RClientContactNo", textClientContactNo.Text);
                    cm.Parameters.AddWithValue("@Category", textCategory.Text);
                    cm.Parameters.AddWithValue("@RateFee", textRateFee.Text);
                    cm.Parameters.AddWithValue("@Date", dtpDDate.Value.Date);
                    cm.Parameters.AddWithValue("@Status", cboStatus.SelectedItem.ToString());
                    cm.Parameters.AddWithValue("@ReservationID", lblRReservationID_.Text);
                    conn.Open();
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Reservation has been successfully updated.");
                    Clear();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure want to remove this reservation?", "Delete Reservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cm = new SqlCommand("INSERT INTO ArchivedReservationTable(AReservationID, ARClientFirstname, ARClientLastName, ARClientContactNo, ACategory," +
                        "ARateFee, ADate, AStatus) VALUES (@AReservationID, @ARClientFirstname, @ARClientLastName, @ARClientContactNo, @ACategory," +
                        "@ARateFee, @ADate, @AStatus)", conn);
                    cm.Parameters.AddWithValue("@ARClientFirstname", lblClientFirstname.Text);
                    cm.Parameters.AddWithValue("@ARClientLastName", lblClientLastName.Text);
                    cm.Parameters.AddWithValue("@ARClientContactNo", textClientContactNo.Text);
                    cm.Parameters.AddWithValue("@ACategory", textCategory.Text);
                    cm.Parameters.AddWithValue("@ARateFee", textRateFee.Text);
                    cm.Parameters.AddWithValue("@ADate", dtpDDate.Value.Date);
                    cm.Parameters.AddWithValue("@AStatus", cboStatus.SelectedItem.ToString());
                    cm.Parameters.AddWithValue("@AReservationID", lblRReservationID_.Text);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    Clear();

                    conn.Open();
                    cm = new SqlCommand("DELETE FROM ReservationTable WHERE ReservationID = @AReservationID", conn);
                    cm.Parameters.AddWithValue("@AReservationID", lblRReservationID_.Text);
                    cm.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Reservation has been successfully deleted!");
                    Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
