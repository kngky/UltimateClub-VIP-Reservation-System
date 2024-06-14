using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UltimateClub_VIP_Reservation_System.User_Control;

namespace UltimateClub_VIP_Reservation_System
{
    public partial class FormDashboard : Form
    {   
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void buttonDashboard_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            openSubForm(new Dashboard());
        }


        public void openSubForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDashboard.Controls.Add(childForm);
            panelDashboard.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }


        private Form activeForm = null;


        private void buttonClient_Click(object sender, EventArgs e)
        {
            UserControlClient usc = new UserControlClient();
            openSubForm(new UserControlClient());
        }

        private void buttonReservation_Click(object sender, EventArgs e)
        {
            Reservation r = new Reservation();
            openSubForm(new Reservation());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            FormDashboard fd = new FormDashboard();
            FormLogin fl = new FormLogin();
            this.Dispose();
            fl.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        public void inputUser()
        {
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
