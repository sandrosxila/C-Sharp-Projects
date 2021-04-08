namespace QueryBeautifier
{
    partial class Form1
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
            this.txtLeft = new System.Windows.Forms.RichTextBox();
            this.txtRight = new System.Windows.Forms.RichTextBox();
            this.btnFormat = new System.Windows.Forms.Button();
            this.chkMatches = new System.Windows.Forms.CheckBox();
            this.txtDiff = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSort = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtLeft
            // 
            this.txtLeft.Font = new System.Drawing.Font("Calibri", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLeft.Location = new System.Drawing.Point(16, 44);
            this.txtLeft.Margin = new System.Windows.Forms.Padding(4);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new System.Drawing.Size(294, 616);
            this.txtLeft.TabIndex = 0;
            this.txtLeft.Text = "";
            // 
            // txtRight
            // 
            this.txtRight.Font = new System.Drawing.Font("Calibri", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRight.Location = new System.Drawing.Point(318, 44);
            this.txtRight.Margin = new System.Windows.Forms.Padding(4);
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new System.Drawing.Size(294, 616);
            this.txtRight.TabIndex = 1;
            this.txtRight.Text = "";
            this.txtRight.TextChanged += new System.EventHandler(this.txtRight_Changed);
            // 
            // btnFormat
            // 
            this.btnFormat.Location = new System.Drawing.Point(195, 3);
            this.btnFormat.Margin = new System.Windows.Forms.Padding(4);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(245, 36);
            this.btnFormat.TabIndex = 2;
            this.btnFormat.Text = "Format";
            this.btnFormat.UseVisualStyleBackColor = true;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // chkMatches
            // 
            this.chkMatches.AutoSize = true;
            this.chkMatches.Checked = true;
            this.chkMatches.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMatches.Location = new System.Drawing.Point(466, 19);
            this.chkMatches.Name = "chkMatches";
            this.chkMatches.Size = new System.Drawing.Size(119, 20);
            this.chkMatches.TabIndex = 3;
            this.chkMatches.Text = "Check Matches";
            this.chkMatches.UseVisualStyleBackColor = true;
            // 
            // txtDiff
            // 
            this.txtDiff.Font = new System.Drawing.Font("Calibri", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiff.Location = new System.Drawing.Point(620, 44);
            this.txtDiff.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiff.Name = "txtDiff";
            this.txtDiff.Size = new System.Drawing.Size(483, 616);
            this.txtDiff.TabIndex = 1;
            this.txtDiff.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(617, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Difference:";
            // 
            // chkSort
            // 
            this.chkSort.AutoSize = true;
            this.chkSort.Checked = true;
            this.chkSort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSort.Location = new System.Drawing.Point(466, 3);
            this.chkSort.Name = "chkSort";
            this.chkSort.Size = new System.Drawing.Size(51, 20);
            this.chkSort.TabIndex = 3;
            this.chkSort.Text = "Sort";
            this.chkSort.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 668);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSort);
            this.Controls.Add(this.chkMatches);
            this.Controls.Add(this.btnFormat);
            this.Controls.Add(this.txtDiff);
            this.Controls.Add(this.txtRight);
            this.Controls.Add(this.txtLeft);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Query String Formatter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLeft;
        private System.Windows.Forms.RichTextBox txtRight;
        private System.Windows.Forms.Button btnFormat;
        private System.Windows.Forms.CheckBox chkMatches;
        private System.Windows.Forms.RichTextBox txtDiff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSort;
    }
}

