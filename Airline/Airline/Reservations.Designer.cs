
namespace Airline
{
    partial class frmReservations
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSeatRow = new System.Windows.Forms.ComboBox();
            this.gbRadios = new System.Windows.Forms.GroupBox();
            this.radD = new System.Windows.Forms.RadioButton();
            this.radA = new System.Windows.Forms.RadioButton();
            this.radC = new System.Windows.Forms.RadioButton();
            this.radB = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnShowPassengers = new System.Windows.Forms.Button();
            this.btnSearchPassenger = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbRadios.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(53, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(115, 20);
            this.txtName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Seat:";
            // 
            // cmbSeatRow
            // 
            this.cmbSeatRow.FormattingEnabled = true;
            this.cmbSeatRow.Location = new System.Drawing.Point(53, 38);
            this.cmbSeatRow.Name = "cmbSeatRow";
            this.cmbSeatRow.Size = new System.Drawing.Size(83, 21);
            this.cmbSeatRow.TabIndex = 3;
            // 
            // gbRadios
            // 
            this.gbRadios.Controls.Add(this.radD);
            this.gbRadios.Controls.Add(this.radA);
            this.gbRadios.Controls.Add(this.radC);
            this.gbRadios.Controls.Add(this.radB);
            this.gbRadios.Location = new System.Drawing.Point(53, 65);
            this.gbRadios.Name = "gbRadios";
            this.gbRadios.Size = new System.Drawing.Size(83, 57);
            this.gbRadios.TabIndex = 4;
            this.gbRadios.TabStop = false;
            // 
            // radD
            // 
            this.radD.AutoSize = true;
            this.radD.Location = new System.Drawing.Point(44, 34);
            this.radD.Name = "radD";
            this.radD.Size = new System.Drawing.Size(33, 17);
            this.radD.TabIndex = 8;
            this.radD.TabStop = true;
            this.radD.Text = "D";
            this.radD.UseVisualStyleBackColor = true;
            // 
            // radA
            // 
            this.radA.AutoSize = true;
            this.radA.Location = new System.Drawing.Point(6, 10);
            this.radA.Name = "radA";
            this.radA.Size = new System.Drawing.Size(32, 17);
            this.radA.TabIndex = 5;
            this.radA.TabStop = true;
            this.radA.Text = "A";
            this.radA.UseVisualStyleBackColor = true;
            // 
            // radC
            // 
            this.radC.AutoSize = true;
            this.radC.Location = new System.Drawing.Point(6, 34);
            this.radC.Name = "radC";
            this.radC.Size = new System.Drawing.Size(32, 17);
            this.radC.TabIndex = 7;
            this.radC.TabStop = true;
            this.radC.Text = "C";
            this.radC.UseVisualStyleBackColor = true;
            // 
            // radB
            // 
            this.radB.AutoSize = true;
            this.radB.Location = new System.Drawing.Point(44, 10);
            this.radB.Name = "radB";
            this.radB.Size = new System.Drawing.Size(32, 17);
            this.radB.TabIndex = 6;
            this.radB.TabStop = true;
            this.radB.Text = "B";
            this.radB.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(53, 132);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(115, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add Passenger";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnShowPassengers
            // 
            this.btnShowPassengers.Location = new System.Drawing.Point(53, 174);
            this.btnShowPassengers.Name = "btnShowPassengers";
            this.btnShowPassengers.Size = new System.Drawing.Size(115, 23);
            this.btnShowPassengers.TabIndex = 6;
            this.btnShowPassengers.Text = "Show Passengers";
            this.btnShowPassengers.UseVisualStyleBackColor = true;
            this.btnShowPassengers.Click += new System.EventHandler(this.btnShowPassengers_Click);
            // 
            // btnSearchPassenger
            // 
            this.btnSearchPassenger.Location = new System.Drawing.Point(53, 216);
            this.btnSearchPassenger.Name = "btnSearchPassenger";
            this.btnSearchPassenger.Size = new System.Drawing.Size(115, 23);
            this.btnSearchPassenger.TabIndex = 7;
            this.btnSearchPassenger.Text = "Search Passenger";
            this.btnSearchPassenger.UseVisualStyleBackColor = true;
            this.btnSearchPassenger.Click += new System.EventHandler(this.btnSearchPassenger_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(53, 255);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(115, 23);
            this.btnQuit.TabIndex = 8;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.Location = new System.Drawing.Point(188, 27);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(136, 251);
            this.lstOutput.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "(1A, 1B, 1C, 1D, ...10D)";
            // 
            // frmReservations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 290);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnSearchPassenger);
            this.Controls.Add(this.btnShowPassengers);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gbRadios);
            this.Controls.Add(this.cmbSeatRow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Name = "frmReservations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reservations";
            this.Load += new System.EventHandler(this.frmReservations_Load);
            this.gbRadios.ResumeLayout(false);
            this.gbRadios.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSeatRow;
        private System.Windows.Forms.RadioButton radD;
        private System.Windows.Forms.RadioButton radA;
        private System.Windows.Forms.RadioButton radC;
        private System.Windows.Forms.RadioButton radB;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnShowPassengers;
        private System.Windows.Forms.Button btnSearchPassenger;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbRadios;
    }
}

