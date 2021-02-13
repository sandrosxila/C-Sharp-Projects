using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEntryFrom
{
    public partial class frmDataEntry : Form
    {
        private TimeSpan elapsedTime;
        private DateTime lastElapsed;
        public frmDataEntry()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtZip.Clear();
            txtName.Focus();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            timTimer.Enabled = true;
            grbDataEntry.Enabled = true;
            txtName.Focus();
            lastElapsed = DateTime.Now;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            timTimer.Enabled = false;
            grbDataEntry.Enabled = false;
        }

        private void timTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime += DateTime.Now - lastElapsed;
            lastElapsed = DateTime.Now;
            txtTimer.Text = Convert.ToString(
                    new TimeSpan(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds)
                );
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string dataEntry;

            if (
                txtName.Text.Equals("") ||
                txtAddress.Text.Equals("") ||
                txtCity.Text.Equals("") ||
                txtState.Text.Equals("") ||
                txtZip.Text.Equals("")
            )
            {
                MessageBox.Show("Each text box must be filled", "Error Message", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }

            dataEntry =
                txtName.Text + "\r\n" +
                txtAddress.Text + "\r\n" +
                txtCity.Text + "\r\n" +
                txtState.Text + "\r\n" +
                txtZip.Text;

            MessageBox.Show(dataEntry, "Data Entry", MessageBoxButtons.OK);
            btnClear.PerformClick();
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            string textBoxSender = ((TextBox) sender).Name;

            if (e.KeyChar == 13)
            {
                switch (textBoxSender)
                {
                    case "txtName":
                        txtAddress.Focus();
                        break;
                    case "txtAddress":
                        txtCity.Focus();
                        break;
                    case "txtCity":
                        txtState.Focus();
                        break;
                    case "txtState":
                        txtZip.Focus();
                        break;
                    case "txtZip":
                        btnAccept.Focus();
                        break;
                }
            }

            if (textBoxSender == "txtZip")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void btnAccept_Hover(object sender, EventArgs e)
        {
            btnAccept.BackColor = Color.CornflowerBlue;
        }

        private void btnAccept_Leave(object sender, EventArgs e)
        {
            btnAccept.BackColor = SystemColors.Control;
        }

        private void btnClear_Hover(object sender, EventArgs e)
        {
            btnClear.BackColor = Color.Crimson;
            btnClear.ForeColor = Color.WhiteSmoke;
        }

        private void btnClear_Leave(object sender, EventArgs e)
        {
            btnClear.BackColor = SystemColors.Control;
            btnClear.ForeColor = SystemColors.ControlText;
        }
    }
}
