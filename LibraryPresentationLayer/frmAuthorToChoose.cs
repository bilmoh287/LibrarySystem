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
    public partial class frmAuthorToChoose : Form
    {
        int _BookID;
        public frmAuthorToChoose(int BookID)
        {
            _BookID = BookID;
            InitializeComponent();
        }

        private void frmAuthorToChoose_Load(object sender, EventArgs e)
        {
            DataTable dtAuthors = clsAuthors.GetAllAuthorsList();
            cklbAuthors.DataSource = dtAuthors;
            cklbAuthors.DisplayMember = "FullName";
            cklbAuthors.ValueMember = "AuthorID";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataRowView item in cklbAuthors.CheckedItems)
            {
                int authorID = (int)item["AuthorID"];
                clsBookAuthor.AddRelation(_BookID, authorID);
            }

            MessageBox.Show("Authors linked successfully!");
            this.Close();
        }
    }
}
