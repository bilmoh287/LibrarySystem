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
    public partial class frmBorrowReturnBooks : Form
    {
        clsBorrowingLibrary _MyLibray;
        public frmBorrowReturnBooks()
        {
            InitializeComponent();
        }
        private void _RefreshBooks()
        {
            {
                dgvListOfBooks.DataSource = clsBooks.GetAllBooks();
            }
        }

        private void frmBorrowReturnBooks_Load(object sender, EventArgs e)
        {
            _RefreshBooks();
        }

        private void borrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int BookID = (int)dgvListOfBooks.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want to Borrow this Book ", "Confirm", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information) == DialogResult.OK)
            {
                _MyLibray = new clsBorrowingLibrary(BookID, clsGlobalUser.CurrentUser.UserID);
                if (_MyLibray.Save())
                {
                    MessageBox.Show("You Successfully Borrowed this Book");
                    frmMyLibrary frm = new frmMyLibrary();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("You have already borrowed this book and haven't returned it yet.");
                    //MessageBox.Show("Failed to Borrow this Book");
                }
            }
        }

        private void btnMyLibrary_Click(object sender, EventArgs e)
        {
            frmMyLibrary frm = new frmMyLibrary();
            frm.ShowDialog();
        }
    }
}
