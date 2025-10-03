namespace LibraryPresentationLayer
{
    partial class frmMyLibrary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvListBorrowedBooks = new System.Windows.Forms.DataGridView();
            this.cmdReturn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.returnBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListBorrowedBooks)).BeginInit();
            this.cmdReturn.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvListBorrowedBooks
            // 
            this.dgvListBorrowedBooks.AllowUserToAddRows = false;
            this.dgvListBorrowedBooks.AllowUserToDeleteRows = false;
            this.dgvListBorrowedBooks.AllowUserToOrderColumns = true;
            this.dgvListBorrowedBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListBorrowedBooks.ContextMenuStrip = this.cmdReturn;
            this.dgvListBorrowedBooks.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvListBorrowedBooks.Location = new System.Drawing.Point(0, 0);
            this.dgvListBorrowedBooks.Name = "dgvListBorrowedBooks";
            this.dgvListBorrowedBooks.ReadOnly = true;
            this.dgvListBorrowedBooks.RowHeadersWidth = 51;
            this.dgvListBorrowedBooks.RowTemplate.Height = 24;
            this.dgvListBorrowedBooks.Size = new System.Drawing.Size(968, 194);
            this.dgvListBorrowedBooks.TabIndex = 0;
            // 
            // cmdReturn
            // 
            this.cmdReturn.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmdReturn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.returnBookToolStripMenuItem});
            this.cmdReturn.Name = "cmdReturn";
            this.cmdReturn.Size = new System.Drawing.Size(211, 56);
            // 
            // returnBookToolStripMenuItem
            // 
            this.returnBookToolStripMenuItem.Name = "returnBookToolStripMenuItem";
            this.returnBookToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.returnBookToolStripMenuItem.Text = "Return Book";
            this.returnBookToolStripMenuItem.Click += new System.EventHandler(this.returnBookToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(427, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 44);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMyLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 292);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvListBorrowedBooks);
            this.Name = "frmMyLibrary";
            this.Text = "frmBorrowed";
            this.Load += new System.EventHandler(this.frmMyLibrary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListBorrowedBooks)).EndInit();
            this.cmdReturn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListBorrowedBooks;
        private System.Windows.Forms.ContextMenuStrip cmdReturn;
        private System.Windows.Forms.ToolStripMenuItem returnBookToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}