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
        public delegate void BookSavedHandler();
        public event BookSavedHandler OnBookSaved;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Book.Title = txtName.Text;
            _Book.ISBN = txtISBN.Text;
            _Book.PublicationDate = dateTimePicker1.Value;
            _Book.Genre = txtGenre.Text;
            _Book.AdditionalInfo = txtAddInfo.Text;

            if(_Book.Save())
            {
                if(_Mode == enMode.AddNewMode)
                {
                    frmAuthorToChoose frm = new frmAuthorToChoose(_Book.);
                    frm.ShowDialog();
                }
                MessageBox.Show("Data Saved Successfully.");
                OnBookSaved?.Invoke();
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
            if(_Book == null)
            {
                MessageBox.Show("Book Not Found");
                this.Close();
                return;
            }

            lblAddEditBooks.Text = "Edit Book With ID =  " + _BookID.ToString();
            lblBookID.Text = _BookID.ToString();
            txtName.Text = _Book.Title;
            txtISBN.Text = _Book.ISBN;
            dateTimePicker1.Value = _Book.PublicationDate;
            txtGenre.Text = _Book.Genre;
            txtAddInfo.Text = _Book.AdditionalInfo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
