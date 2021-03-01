namespace AsyncAwaitPractice
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.btnClickMe = new System.Windows.Forms.Button();
            this.lblText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClickMe
            // 
            this.btnClickMe.Location = new System.Drawing.Point(12, 83);
            this.btnClickMe.Name = "btnClickMe";
            this.btnClickMe.Size = new System.Drawing.Size(160, 23);
            this.btnClickMe.TabIndex = 0;
            this.btnClickMe.Text = "Click Me";
            this.btnClickMe.UseVisualStyleBackColor = true;
            this.btnClickMe.Click += new System.EventHandler(this.btnClickMe_Click);
            // 
            // lblText
            // 
            this.lblText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblText.Location = new System.Drawing.Point(12, 27);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(160, 28);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "Click To Update (5s)";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 118);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnClickMe);
            this.Name = "Form1";
            this.Text = "AsyncAwaitPractice";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblText;

        private System.Windows.Forms.Button btnClickMe;

        #endregion
    }
}