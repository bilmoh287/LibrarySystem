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
    public partial class frmAuthorWhoWroteTheBook : Form
    {
        int _BookID;
        public frmAuthorWhoWroteTheBook(int BookID)
        {
            _BookID = BookID;
            InitializeComponent();
        }

        private void frmAuthorWhoWroteTheBook_Load(object sender, EventArgs e)
        {
            dgvAuthors.DataSource = clsBookAuthor.GetAllAuthorWhoWroteTheBook(_BookID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
