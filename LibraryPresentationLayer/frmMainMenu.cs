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
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void btnManageBooks_Click(object sender, EventArgs e)
        {
            if (!clsGlobalUser.CurrentUser.HasPermission(clsUsers.Permissions.ManageAuthors))
            {
                MessageBox.Show("You don’t have permission to Manage Authors.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmBooks frm = new frmBooks();
            frm.ShowDialog();
        }

        private void btnManageAuthors_Click(object sender, EventArgs e)
        {
            if (!clsGlobalUser.CurrentUser.HasPermission(clsUsers.Permissions.ManageAuthors))
            {
                MessageBox.Show("You don’t have permission to Manage Authors.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmAuthors frm = new frmAuthors();
            frm.ShowDialog();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            if (!clsGlobalUser.CurrentUser.HasPermission(clsUsers.Permissions.ManageUsers))
            {
                MessageBox.Show("You don’t have permission to Manage Users.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmUsers frm = new frmUsers();
            frm.ShowDialog();
        }

        private void btnBorrowReturn_Click(object sender, EventArgs e)
        {
            if (!clsGlobalUser.CurrentUser.HasPermission(clsUsers.Permissions.BorrowReturn))
            {
                MessageBox.Show("You don’t have permission to Borrow Books.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmBorrowReturnBooks frm = new frmBorrowReturnBooks();
            frm.ShowDialog();
        }
    }
}
