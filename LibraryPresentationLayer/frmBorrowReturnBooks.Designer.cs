namespace LibraryPresentationLayer
{
    partial class frmBorrowReturnBooks
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
            this.dgvListOfBooks = new System.Windows.Forms.DataGridView();
            this.cmsBorrow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.borrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnMyLibrary = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListOfBooks)).BeginInit();
            this.cmsBorrow.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvListOfBooks
            // 
            this.dgvListOfBooks.AllowUserToAddRows = false;
            this.dgvListOfBooks.AllowUserToDeleteRows = false;
            this.dgvListOfBooks.AllowUserToOrderColumns = true;
            this.dgvListOfBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListOfBooks.ContextMenuStrip = this.cmsBorrow;
            this.dgvListOfBooks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvListOfBooks.Location = new System.Drawing.Point(0, 100);
            this.dgvListOfBooks.Name = "dgvListOfBooks";
            this.dgvListOfBooks.ReadOnly = true;
            this.dgvListOfBooks.RowHeadersWidth = 51;
            this.dgvListOfBooks.RowTemplate.Height = 24;
            this.dgvListOfBooks.Size = new System.Drawing.Size(987, 376);
            this.dgvListOfBooks.TabIndex = 0;
            // 
            // cmsBorrow
            // 
            this.cmsBorrow.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsBorrow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.borrowToolStripMenuItem});
            this.cmsBorrow.Name = "cmsBorrow";
            this.cmsBorrow.Size = new System.Drawing.Size(211, 56);
            // 
            // borrowToolStripMenuItem
            // 
            this.borrowToolStripMenuItem.Name = "borrowToolStripMenuItem";
            this.borrowToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.borrowToolStripMenuItem.Text = "Borrow";
            this.borrowToolStripMenuItem.Click += new System.EventHandler(this.borrowToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(86, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(227, 28);
            this.textBox1.TabIndex = 3;
            // 
            // btnMyLibrary
            // 
            this.btnMyLibrary.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMyLibrary.Location = new System.Drawing.Point(713, 31);
            this.btnMyLibrary.Name = "btnMyLibrary";
            this.btnMyLibrary.Size = new System.Drawing.Size(155, 48);
            this.btnMyLibrary.TabIndex = 4;
            this.btnMyLibrary.Text = "My Library";
            this.btnMyLibrary.UseVisualStyleBackColor = true;
            // 
            // frmBorrowReturnBooks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 476);
            this.Controls.Add(this.btnMyLibrary);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvListOfBooks);
            this.Name = "frmBorrowReturnBooks";
            this.Text = "frmBorrowReturnBooks";
            this.Load += new System.EventHandler(this.frmBorrowReturnBooks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListOfBooks)).EndInit();
            this.cmsBorrow.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListOfBooks;
        private System.Windows.Forms.ContextMenuStrip cmsBorrow;
        private System.Windows.Forms.ToolStripMenuItem borrowToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnMyLibrary;
    }
}