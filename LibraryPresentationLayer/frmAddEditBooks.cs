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
    public partial class frmAddEditBooks : Form
    {
        enum enMode {AddNewMode, UpdateMode};
        enMode _Mode;

        clsBooks _Books;
        public frmAddEditBooks(int ID)
        {
            InitializeComponent();
            if(ID ==  -1)
                _Mode = enMode.AddNewMode;
            else
                _Mode = enMode.UpdateMode;

        }

        private void lblAddEditBooks_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Books.Name = txtName.Text;
            _Books.ISBN = txtISBN.Text;
            _Books.PublicationDate = dateTimePicker1.Value;
            _Books.Genre = txtGenre.Text;
            _Books.AdditionalInfo = txtAddInfo.Text;
        }
    }
}
