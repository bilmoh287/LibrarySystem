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
    public partial class frmBooks : Form
    {
        public frmBooks()
        {
            InitializeComponent();
        }

        clsBooks _Book;
        
        private void _RefreshBooks()
        {
            dgvListBooks.DataSource = clsBooks.GetAllBooks();
        }
        private void frmBooks_Load(object sender, EventArgs e)
        {
            _RefreshBooks();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _Book = clsBooks.Find(txtSearch.Text);
            if(_Book != null)
            {
                dgvListBooks.DataSource = _Book;
            }
        }

        private void btnAddBooks_Click(object sender, EventArgs e)
        {
            frmAddEditBooks frm = new frmAddEditBooks(-1);
            frm.OnBookSaved += _RefreshBooks; // subscribe to event
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvListBooks.CurrentRow.Cells["BookID"].Value;
            frmAddEditBooks frm = new frmAddEditBooks(ID);
            frm.OnBookSaved += _RefreshBooks; // subscribe to event
            frm.ShowDialog();
        }
    }
}
