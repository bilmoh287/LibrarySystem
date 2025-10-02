using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryBussinessLayer;

namespace LibraryPresentationLayer
{
    public partial class frmAuthors : Form
    {
        public frmAuthors()
        {
            InitializeComponent();
        }

        void _RefreshAuthors()
        {
            dataGridView1.DataSource = clsAuthors.GetAllAuthorsList();
        }

        private void frmAuthors_Load(object sender, EventArgs e)
        {
            _RefreshAuthors();
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            frmEditAddAuthors frm = new frmEditAddAuthors(-1);
            frm.OnBookSaved += _RefreshAuthors;
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmEditAddAuthors frm = new frmEditAddAuthors(ID);
            frm.OnBookSaved += _RefreshAuthors;
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AuthorID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want to delete Author with ID [" + AuthorID + "]", "Confirm Delete", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsAuthors.DeleteAuthor(AuthorID))
                {
                    MessageBox.Show("Book Deleted Successfully");
                    _RefreshAuthors();
                }
                else
                {
                    MessageBox.Show("Failed to Delete Book");
                }
            }
        }

        private void bookWroteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AuthorID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmBooksWrotedByAuthor frm = new frmBooksWrotedByAuthor(AuthorID);
            frm.ShowDialog();
        }
    }
}
