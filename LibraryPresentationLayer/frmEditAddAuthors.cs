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
    public partial class frmEditAddAuthors : Form
    {
        public delegate void BookSavedHandler();
        public event BookSavedHandler OnBookSaved;

        enum enMode { AddNewMode, UpdateMode };
        enMode _Mode;

        int _AuthorID;
        clsAuthors _Author;
        public frmEditAddAuthors(int ID)
        {
            InitializeComponent();

            _AuthorID = ID;
            if(_AuthorID == -1)
            {
                _Mode = enMode.AddNewMode;
            }
            else
            {
                _Mode = enMode.UpdateMode;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmEditAddAuthors_Load(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNewMode)
            {
                lblAddEditAuthors.Text = "Add New Author";
                _Author = new clsAuthors();
                return;
            }

            lblAddEditAuthors.Text = "Edit Author With ID =  " + _AuthorID.ToString();
            txtFullName.Text = _Author.FullName;
            dateTimePicker1.Value = _Author.DateOfBirth;
            txtNationality.Text = _Author.Nationality;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Author.FullName = txtFullName.Text;
            _Author.DateOfBirth = dateTimePicker1.Value;
            _Author.Nationality = txtNationality.Text;
            _Author.ContactInfo = txtContactInfo.Text;

            if (_Author.Save())
            {
                MessageBox.Show("Data Saved Successfully.");
                OnBookSaved?.Invoke();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.");
            }

            lblAddEditAuthors.Text = "Edit AUthor with ID = " + _Author.AuthorsID;
            lblAuthorsID.Text = _Author.AuthorsID.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
