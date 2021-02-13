
namespace BookDatabaseSearchProject
{
    partial class frmDatabase
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
            this.grdBooks = new System.Windows.Forms.DataGridView();
            this.btnAllRecords = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBooks
            // 
            this.grdBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBooks.Location = new System.Drawing.Point(11, 15);
            this.grdBooks.Name = "grdBooks";
            this.grdBooks.Size = new System.Drawing.Size(777, 275);
            this.grdBooks.TabIndex = 0;
            // 
            // btnAllRecords
            // 
            this.btnAllRecords.Location = new System.Drawing.Point(651, 415);
            this.btnAllRecords.Name = "btnAllRecords";
            this.btnAllRecords.Size = new System.Drawing.Size(137, 23);
            this.btnAllRecords.TabIndex = 1;
            this.btnAllRecords.Text = "Show All Records";
            this.btnAllRecords.UseVisualStyleBackColor = true;
            this.btnAllRecords.Click += new System.EventHandler(this.btnSQL_Click);
            // 
            // frmDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAllRecords);
            this.Controls.Add(this.grdBooks);
            this.Name = "frmDatabase";
            this.Text = "Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmForm_Closing);
            this.Load += new System.EventHandler(this.frmDatabase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdBooks;
        private System.Windows.Forms.Button btnAllRecords;
    }
}

