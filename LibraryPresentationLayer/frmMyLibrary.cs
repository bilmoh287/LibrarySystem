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
            dgvListBorrowedBooks.DataSource = clsBorrowingLibrary.GetAllBorrowedBooksList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMyLibrary_Load(object sender, EventArgs e)
        {
            _RefreshList();
        }
    }
}
