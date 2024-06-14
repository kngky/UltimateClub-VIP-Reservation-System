using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UltimateClub_VIP_Reservation_System
{
    public partial class FormLogin : Form
    {
        DBHelper dbHelper = null;
        SqlConnection conn = null;
        SqlDataReader dr;
       public FormLogin()
        {
            InitializeComponent();
            dbHelper = new DBHelper();
            conn = dbHelper.conn;
        }

        private void pictureBoxMinimize_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxMinimize, "Minimize");
        }

        private void pictureBoxClose_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxClose, "Close");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        
        private void pictureBoxShow_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxShow, "Show Password");
        }

        private void pictureBoxHide_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxHide, "Hide Password");
        }

        private void pictureBoxShow_Click(object sender, EventArgs e)
        {
            pictureBoxShow.Hide();
            textBoxPassword.UseSystemPasswordChar = false;
            pictureBoxHide.Show();
        }

        private void pictureBoxHide_Click(object sender, EventArgs e)
        {
            pictureBoxHide.Hide();
            textBoxPassword.UseSystemPasswordChar = true;
            pictureBoxShow.Show();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM User_Table WHERE User_Name = '{textBoxUsername.Text}' AND " + $"User_Password = '{textBoxPassword.Text}'",conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    FormDashboard fd = new FormDashboard();
                    fd.Show();
                    this.Hide();
                    FormDashboard main = new FormDashboard();
                    main.lblUser.Text = textBoxUsername.Text;
                    //MessageBox.Show($"User exist");
                }
                else
                {
                    MessageBox.Show($"User not existing");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error occured: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
