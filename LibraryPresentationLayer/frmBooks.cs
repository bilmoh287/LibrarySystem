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
        
        private void _RefreshBooks()
        {
            dgvListBooks.DataSource = clsBooks.GetAllBooks();
        }
        private void frmBooks_Load(object sender, EventArgs e)
        {
            _RefreshBooks();
        }
    }
}
