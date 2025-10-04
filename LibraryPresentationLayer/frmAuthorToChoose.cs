using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryBussinessLayer;

namespace LibraryPresentationLayer
{
    public partial class frmAuthorToChoose : Form
    {
        int _BookID;
        public frmAuthorToChoose(int BookID)
        {
            _BookID = BookID;
            InitializeComponent();
        }

        private void _RefreshAuthorToChoose()
        {
            DataTable dtAuthors = clsAuthors.GetAllAuthorsList();
            cklbAuthors.DataSource = dtAuthors;
            cklbAuthors.DisplayMember = "FullName";
            cklbAuthors.ValueMember = "AuthorID";
        }

        private void frmAuthorToChoose_Load(object sender, EventArgs e)
        {
            _RefreshAuthorToChoose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Check if no authors are selected
            if (cklbAuthors.CheckedItems.Count == 0)
            {
                DialogResult result = MessageBox.Show(
                    "No author selected. Do you want to save this book without an author?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return; // stay on the form until they pick or accept
            }

            // Save the selected authors
            foreach (DataRowView item in cklbAuthors.CheckedItems)
            {
                int authorID = (int)item["AuthorID"];
                clsBookAuthor.AddRelation(_BookID, authorID);
            }

            // mark as successful
            this.DialogResult = DialogResult.OK;
            MessageBox.Show("Authors linked successfully!");
            this.Close();
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            frmAddEditAuthors frm = new frmAddEditAuthors(-1);
            frm.OnBookSaved += _RefreshAuthorToChoose;
            frm.ShowDialog();
        }
    }
}
