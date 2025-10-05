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
    public partial class frmBookAdditionalInfo : Form
    {
        int BookID;
        public frmBookAdditionalInfo(int ID)
        {
            BookID = ID;
            InitializeComponent();
        }

        private void frmBookAdditionalInfo_Load(object sender, EventArgs e)
        {
            label1.Text = clsBorrowingLibrary.GetAddionalInfo(BookID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
