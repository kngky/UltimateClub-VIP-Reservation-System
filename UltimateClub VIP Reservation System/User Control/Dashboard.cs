using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace UltimateClub_VIP_Reservation_System.User_Control
{
    public partial class Dashboard : Form
    {
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBHelper dbHelper = null;
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        public Dashboard()
        {
            InitializeComponent();
            dbHelper = new DBHelper();
            conn = dbHelper.conn;
            LOADALL();
        }

        public void LOADALL()
        {
            LoadOB();
            LoadClients();
            LoadVIP();
            LoadVVIP();
            PaidClients();
            UnpaidClients();
        }

        public void LoadOB()
        {
            int count = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReservationTable", conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            lblObNumber.Text = count.ToString();
            conn.Close();
        }

        public void LoadClients()
        {
            int count = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(ClientID) FROM ClientTabe", conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            lblClientNumber.Text = count.ToString();
            conn.Close();
        }

        public void LoadVIP()
        {
            int count = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReservationTable where Category = 'VIP'", conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            lblVipNumber.Text = count.ToString();
            conn.Close();
        }

        public void LoadVVIP()
        {
            int count = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReservationTable where Category = 'VVIP'", conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            lblVVIPnumber.Text = count.ToString();
            conn.Close();
        }

        public void PaidClients()
        {
            int count = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReservationTable where Status = 'Paid'", conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            lblPaidClients.Text = count.ToString();
            conn.Close();
        }

        public void UnpaidClients()
        {
            int count = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReservationTable where Status = 'Unpaid'", conn);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            lblUnPaidClients.Text = count.ToString();
            conn.Close();
        }

        public void LOADClients()
        {

        }

        public void VIPnumber()
        {

        }

        public void VVIPNumber()
        {

        }

        private void btnREF_Click(object sender, EventArgs e)
        {
        }
    }
}
