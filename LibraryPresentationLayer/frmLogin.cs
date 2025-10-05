using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryBussinessLayer;

namespace LibraryPresentationLayer
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void llSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser(-1);
            frm.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.",
                    "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Logging In
            var user = clsUsers.Login(username, password);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password.",
                    "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save user globally
            clsGlobalUser.CurrentUser = user;

            MessageBox.Show($"Welcome, {user.FullName}!",
                "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

 

            // Open MainMenu
            frmMainMenu mainForm = new frmMainMenu();
            mainForm.Show();

            // Hide login form
            this.Hide();
        }
    }
}
