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
            //_Book = clsBooks.Find(txtSearch.Text);
            //if(_Book != null)
            //{
            //    dgvListBooks.DataSource = _Book;
            //}

            _Book = clsBooks.Find(txtSearch.Text);
            if (_Book != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("BookID", typeof(int));
                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("ISBN", typeof(string));
                dt.Columns.Add("PublicationDate", typeof(DateTime));
                dt.Columns.Add("Genre", typeof(string));
                dt.Columns.Add("AdditionalInfo", typeof(string));

                dt.Rows.Add(_Book.ID, _Book.Title, _Book.ISBN,
                            _Book.PublicationDate, _Book.Genre, _Book.AdditionalInfo);

                dgvListBooks.DataSource = dt;
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int BookID = (int)dgvListBooks.CurrentRow.Cells[0].Value;
            if(MessageBox.Show("Are you sure you want to delete Book [" + BookID + "]", "Confirm Delete", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if(clsBooks.DeleteBook(BookID))
                {
                    MessageBox.Show("Book Deleted Successfully");
                    _RefreshBooks();
                }
                else
                {
                    MessageBox.Show("Failed to Delete Book");
                }
            }

        }
    }
}
