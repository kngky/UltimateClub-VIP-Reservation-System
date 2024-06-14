using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateClub_VIP_Reservation_System
{
    internal class DBHelper
    {
        public SqlConnection conn;
        private string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hi\Documents\UltimateClub_VIP_Reservation_System.mdf;Integrated Security=True;Connect Timeout=30";

        public DBHelper(bool notify = false)
        {
            conn = GetConnection(notify);
        }

        private SqlConnection GetConnection(bool notif)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(conString);
                sqlConnection.Open();
                if (notif)
                {
                    MessageBox.Show($"Database connected!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occured while connecting to database: {ex.Message}");
            }
            finally
            {
                sqlConnection.Close();   
            }
            return sqlConnection;
        }
    }
}
