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

        int _BookID;
        clsBooks _Book;
        public frmAddEditBooks(int ID)
        {
            InitializeComponent();
            _BookID = ID;
            if(_BookID ==  -1)
                _Mode = enMode.AddNewMode;
            else
                _Mode = enMode.UpdateMode;

        }

        private void lblAddEditBooks_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Book.Name = txtName.Text;
            _Book.ISBN = txtISBN.Text;
            _Book.PublicationDate = dateTimePicker1.Value;
            _Book.Genre = txtGenre.Text;
            _Book.AdditionalInfo = txtAddInfo.Text;

            if(_Book.Save())
            {
                MessageBox.Show("Data Saved Successfully.");
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.");
            }

            lblAddEditBooks.Text = "Edit BookID = " + _Book.ID;
            lblBookID.Text = _Book.ID.ToString();
        }

        private void frmAddEditBooks_Load(object sender, EventArgs e)
        {
            if(_Mode  == enMode.AddNewMode)
            {
                lblAddEditBooks.Text = "Add New Book";
                _Book = new clsBooks();
                return;
            }

            _Book = clsBooks.Find(_BookID);
            lblAddEditBooks.Text = "Edit Book " + _BookID.ToString();
        }
    }
}
