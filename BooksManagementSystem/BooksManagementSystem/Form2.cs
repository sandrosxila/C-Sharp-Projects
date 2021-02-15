using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksManagementSystem
{
    public partial class frmPublishers : Form
    {
        public frmPublishers()
        {
            InitializeComponent();
        }

        private OleDbConnection booksConn;
        private OleDbCommand publishersComm;
        private OleDbDataAdapter publishersAdapter;
        private DataTable publishersTable;
        private CurrencyManager publishersManager;
        private bool dbError = false;

        enum State
        {
            Edit, Add, View
        }
        private void frmPublishers_Load(object sender, EventArgs e)
        {
            try
            {
                string dataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                                  "\\DB\\Books.accdb";
                var connString =
                    $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dataPath};Persist Security Info=False;";
                booksConn = new OleDbConnection(connString);
                booksConn.Open();
                publishersComm = new OleDbCommand("SELECT * FROM Publishers ORDER BY Name", booksConn);
                publishersTable = new DataTable();
                publishersAdapter = new OleDbDataAdapter(publishersComm);
                publishersAdapter.Fill(publishersTable);
                txtPublisherID.DataBindings.Add("Text", publishersTable, "PubID");
                txtName.DataBindings.Add("Text", publishersTable, "Name");
                txtCompanyName.DataBindings.Add("Text", publishersTable, "Company_Name");
                txtAddress.DataBindings.Add("Text", publishersTable, "Address");
                txtCity.DataBindings.Add("Text", publishersTable, "City");
                txtState.DataBindings.Add("Text", publishersTable, "State");
                txtZip.DataBindings.Add("Text", publishersTable, "Zip");
                txtTelephone.DataBindings.Add("Text", publishersTable, "Telephone");
                txtFax.DataBindings.Add("Text", publishersTable, "FAX");
                txtComments.DataBindings.Add("Text", publishersTable, "Comments");
                publishersManager = (CurrencyManager)BindingContext[publishersTable];
                setAppState(State.View);
            }
            catch (Exception ex)
            {
                dbError = true;
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPublishers_Closing(object sender, FormClosingEventArgs e)
        {
            if (!dbError)
            {
                booksConn.Close();
                booksConn.Dispose();
                publishersAdapter.Dispose();
                publishersTable.Dispose();
                publishersComm.Dispose();
                publishersTable.Dispose();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            publishersManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            publishersManager.Position++;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                setAppState(State.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record Editing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                setAppState(State.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record Addition Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setAppState(State appState)
        {
            switch (appState)
            {
                case State.View:
                    txtPublisherID.ReadOnly = true;
                    txtName.ReadOnly = true;
                    txtCompanyName.ReadOnly = true;
                    txtAddress.ReadOnly = true;
                    txtCity.ReadOnly = true;
                    txtState.ReadOnly = true;
                    txtZip.ReadOnly = true;
                    txtTelephone.ReadOnly = true;
                    txtFax.ReadOnly = true;
                    txtComments.ReadOnly = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = true;
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    break;
                default:
                    txtPublisherID.BackColor = Color.Red;
                    txtPublisherID.ForeColor = Color.White;
                    txtPublisherID.ReadOnly = true;
                    txtName.ReadOnly = false;
                    txtCompanyName.ReadOnly = false;
                    txtAddress.ReadOnly = false;
                    txtCity.ReadOnly = false;
                    txtState.ReadOnly = false;
                    txtZip.ReadOnly = false;
                    txtTelephone.ReadOnly = false;
                    txtFax.ReadOnly = false;
                    txtComments.ReadOnly = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = false;
                    txtName.Focus();
                    break;
            }
        }

        private bool ValidateInput()
        {
            string message = "";
            bool allOK = false;

            if (txtName.Text.Trim().Equals(""))
            {
                message += "Publisher Name must be filled" + "\r\n";
                txtName.Focus();
                allOK = false;
            }

            if (!allOK)
            {
                MessageBox.Show(message, "Invalid input", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return allOK;
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = (TextBox)sender;
            switch (textBox.Name)
            {
                case "txtZip":
                    if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    if (e.KeyChar == 13)
                        txtTelephone.Focus();
                    break;
                case "txtPublisherID":
                    if (e.KeyChar == 13)
                        txtName.Focus();
                    break;
                case "txtName":
                    if (e.KeyChar == 13)
                        txtCompanyName.Focus();
                    break;
                case "txtCompanyName":
                    if (e.KeyChar == 13)
                        txtAddress.Focus();
                    break;
                case "txtAddress":
                    if (e.KeyChar == 13)
                        txtCity.Focus();
                    break;
                case "txtCity":
                    if (e.KeyChar == 13)
                        txtState.Focus();
                    break;
                case "txtState":
                    if (e.KeyChar == 13)
                        txtZip.Focus();
                    break;
                case "txtTelephone":
                    if (e.KeyChar == 13)
                        txtFax.Focus();
                    break;
                case "txtFax":
                    if (e.KeyChar == 13)
                        txtComments.Focus();
                    break;
                case "txtComments":
                    if (e.KeyChar == 13)
                        btnSave.Focus();
                    break;
                default:
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setAppState(State.View);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    MessageBox.Show("Record is saved successfully", "Error Saving Record",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setAppState(State.Add);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Saving Record Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure about the record deletion?", "Record Deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (response == DialogResult.No)
                return;
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Saving Record Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
