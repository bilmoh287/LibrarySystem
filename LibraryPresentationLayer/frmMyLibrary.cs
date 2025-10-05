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
    public partial class frmMyLibrary : Form
    {
        public frmMyLibrary()
        {
            InitializeComponent();
        }

        private void _RefreshList()
        {
            dgvListBorrowedBooks.DataSource = clsBorrowingLibrary.GetAllBorrowedBooksList(clsGlobalUser.CurrentUser.UserID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMyLibrary_Load(object sender, EventArgs e)
        {
            _RefreshList();
        }

        private void returnBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int BookID = (int)dgvListBorrowedBooks.CurrentRow.Cells[0].Value;
            if(clsBorrowingLibrary.ReturnBook(BookID))
            {
                MessageBox.Show("Payment Method will be here soon😁");
                _RefreshList();
            }

        }
    }
}
