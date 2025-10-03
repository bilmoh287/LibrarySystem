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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsers()
        {
            dgvUsers.DataSource = clsUsers.GetAllUsersList();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsers();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmAddEditUser frm = new frmAddEditUser(ID);
            frm.OnBookSaved += _RefreshUsers; // subscribe to event
            frm.ShowDialog();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser(-1);
            frm.OnBookSaved += _RefreshUsers; // subscribe to event
            frm.ShowDialog();
        }
    
    }
}
