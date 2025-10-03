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
using static LibraryBussinessLayer.clsUsers;

namespace LibraryPresentationLayer
{
    public partial class frmAddEditUser : Form
    {
        public delegate void BookSavedHandler();
        public event BookSavedHandler OnBookSaved;

        enum enMode { AddNewMode, UpdateMode };
        enMode _Mode;

        clsUsers _User;
        int _UserID;
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
            _Mode = (_UserID == -1) ? enMode.AddNewMode : enMode.UpdateMode;

        }

        private void LoadUserPermissions()
        {
            for (int i = 0; i < cklbPermission.Items.Count; i++)
            {
                string itemText = cklbPermission.Items[i].ToString()
                                  .Replace(" ", "")      // remove spaces
                                  .Replace("/", "");     // remove slash

                if (Enum.TryParse(itemText, out clsUsers.Permissions perm))
                {
                    if ((_User.Permission & (int)perm) == (int)perm)
                    {
                        cklbPermission.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNewMode)
            {
                lblAddEditUser.Text = "Add New USer";
                _User = new clsUsers();
                return;
            }

            _User = clsUsers.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("Author Not Found");
                this.Close();
                return;
            }

            lblAddEditUser.Text = "Edit Author With ID =  " + _UserID.ToString();
            lblUserID.Text = _UserID.ToString();
            txtFullName.Text = _User.FullName;
            dateTimePicker1.Value = _User.DateOfBirth;
            txtContactInfo.Text = _User.ContactInfo;
            lblLibraryCard.Text = _User.LibraryCard;
            txtUsername.Text = _User.Username;
            txtPassword.Text = _User.Password;
            LoadUserPermissions();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _User.FullName = txtFullName.Text;
            _User.DateOfBirth = dateTimePicker1.Value;
            _User.ContactInfo = txtContactInfo.Text;
            _User.ContactInfo = txtContactInfo.Text;
            _User.Username = txtUsername.Text;
            _User.Password = txtPassword.Text;

            int selectedPermissions = 0;

            foreach (var item in cklbPermission.CheckedItems)
            {
                string itemText = item.ToString()
                                      .Replace(" ", "")
                                      .Replace("/", "");

                if (Enum.TryParse(itemText, out clsUsers.Permissions perm))
                {
                    selectedPermissions |= (int)perm;
                }
            }

            _User.Permission = selectedPermissions;


            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully.");
                OnBookSaved?.Invoke();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.");
            }

            lblAddEditUser.Text = "Edit User with ID = " + _User.UserID;
            lblUserID.Text = _User.UserID.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
