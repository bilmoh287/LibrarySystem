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
    }
}
