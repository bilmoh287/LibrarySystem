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
    public partial class frmBooksWrotedByAuthor : Form
    {
        int _AuthorID;
        public frmBooksWrotedByAuthor(int AuthorID)
        {
            _AuthorID = AuthorID;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBooksWrotedByAuthor_Load(object sender, EventArgs e)
        {
            dgvBooks.DataSource = clsBookAuthor.GetAllBooksWrittenByAuthor(_AuthorID);
        }
    }
}
