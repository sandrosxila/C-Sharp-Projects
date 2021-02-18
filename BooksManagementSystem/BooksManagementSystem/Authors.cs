using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BooksManagementSystem
{
    public partial class frmAuthors : Form
    {
        public frmAuthors()
        {
            InitializeComponent();
        }

        private OleDbConnection booksConn;
        private OleDbCommand authorsComm;
        private OleDbDataAdapter authorsAdapter;
        private DataTable authorsTable;
        private CurrencyManager authorsManager;
        private OleDbCommandBuilder builderComm;
        private bool dbError = false;
        private int CurrentPosition { get; set; }
        private enum State
        {
            View,
            Edit,
            Add,
            Delete
        }

        private State appState = State.View;

        private void frmAuthors_Load(object sender, EventArgs e)
        {
            try
            {
                string dataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                                  "\\DB\\Books.accdb";
                var connString =
                    $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dataPath};Persist Security Info=False;";
                booksConn = new OleDbConnection(connString);
                booksConn.Open();

                authorsComm = new OleDbCommand("SELECT * FROM Authors ORDER BY Author", booksConn);
                authorsAdapter = new OleDbDataAdapter(authorsComm);
                authorsTable = new DataTable();
                authorsAdapter.Fill(authorsTable);
                txtAuthorID.DataBindings.Add("Text", authorsTable, "AU_ID");
                txtAuthorName.DataBindings.Add("Text", authorsTable, "Author");
                txtAuthorBorn.DataBindings.Add("Text", authorsTable, "Year_Born");
                authorsManager = (CurrencyManager)BindingContext[authorsTable];
                setAppState(State.View);
            }
            catch (Exception ex)
            {
                dbError = true;
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            authorsManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            authorsManager.Position++;
        }

        private void frmAuthors_Closing(object sender, FormClosingEventArgs e)
        {
            if (!dbError)
            {
                booksConn.Close();
                authorsTable.Dispose();
                authorsComm.Dispose();
                authorsAdapter.Dispose();
                booksConn.Dispose();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    var savedRecord = txtAuthorName.Text;
                    authorsManager.EndCurrentEdit();
                    builderComm = new OleDbCommandBuilder(authorsAdapter);
                    if (appState == State.Edit)
                    {
                        var authRow = authorsTable.Select("AU_ID = " + txtAuthorID.Text);
                        if (String.IsNullOrEmpty(txtAuthorBorn.Text))
                            authRow[0]["Year_Born"] = DBNull.Value;
                        else
                            authRow[0]["Year_Born"] = txtAuthorBorn.Text;
                        authorsTable.DefaultView.Sort = "Author";
                        authorsManager.Position = authorsTable.DefaultView.Find(savedRecord);
                        authorsAdapter.Update(authorsTable);
                        txtAuthorBorn.DataBindings.Add("Text", authorsTable, "Year_Born");
                    }
                    else if (appState == State.Add)
                    {
                        authorsTable.DefaultView.Sort = "Author";
                        authorsManager.Position = authorsTable.DefaultView.Find(savedRecord);
                        authorsAdapter.Update(authorsTable);
                    }

                    MessageBox.Show("Record Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setAppState(State.View);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Record Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure that you want to delete?", "Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (response == DialogResult.No)
                return;

            try
            {
                setAppState(State.Delete);
                authorsManager.RemoveAt(authorsManager.Position);
                builderComm = new OleDbCommandBuilder(authorsAdapter);
                authorsAdapter.Update(authorsTable);
                setAppState(State.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setAppState(State appState)
        {
            this.appState = appState;
            switch (appState)
            {
                case State.View:
                    txtAuthorID.BackColor = Color.White;
                    txtAuthorID.ForeColor = Color.Black;
                    txtAuthorName.ReadOnly = true;
                    txtAuthorBorn.ReadOnly = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    txtAuthorName.TabStop = false;
                    txtAuthorBorn.TabStop = false;
                    btnFirst.Enabled = true;
                    btnLast.Enabled = true;
                    btnSearch.Enabled = true;
                    txtSearch.Enabled = true;
                    btnEdit.Enabled = true;
                    break;
                default:
                    txtAuthorID.BackColor = Color.Red;
                    txtAuthorID.ForeColor = Color.White;
                    txtAuthorName.ReadOnly = false;
                    txtAuthorBorn.ReadOnly = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = false;
                    btnFirst.Enabled = false;
                    btnLast.Enabled = false;
                    btnSearch.Enabled = false;
                    txtSearch.Enabled = false;
                    btnEdit.Enabled = false;
                    txtAuthorName.Focus();
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtAuthorBorn.DataBindings.Clear();
            setAppState(State.Edit);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentPosition = authorsManager.Position;
                authorsManager.AddNew();
                setAppState(State.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record Addition Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            authorsManager.CancelCurrentEdit();
            if (appState == State.View)
                txtAuthorBorn.DataBindings.Add("Text", authorsTable, "Year_Born");
            else if (appState == State.Add)
            {
                authorsManager.Position = CurrentPosition;
            }
            setAppState(State.View);
        }

        private void txtAuthorBorn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8 || e.KeyChar == 13)
            {
                e.Handled = false;
                if (e.KeyChar == 13)
                    btnSave.Focus();
                lblWrongInput.Visible = false;
            }
            else
            {
                e.Handled = true;
                lblWrongInput.Visible = true;
            }
        }

        private bool ValidateInput()
        {
            string message = "";
            int inputYear, currentYear;
            bool allOK = true;

            if (txtAuthorName.Text.Trim().Equals(""))
            {
                message = "Author's Name is Required" + "\r\n";
                txtAuthorName.Focus();
                allOK = false;
            }

            if (!txtAuthorBorn.Text.Trim().Equals(""))
            {
                inputYear = Convert.ToInt32(txtAuthorBorn.Text);
                currentYear = DateTime.Now.Year;
                if (inputYear >= currentYear)
                {
                    message += "Invalid Year";
                    txtAuthorBorn.Focus();
                    allOK = false;
                }
            }

            if (!allOK)
            {
                MessageBox.Show(message, "Invalid input", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return allOK;
        }

        private void txtAuthorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtAuthorBorn.Focus();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            authorsManager.Position = 0;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            authorsManager.Position = authorsManager.Count - 1;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Equals("") || txtSearch.Text.Length < 3)
            {
                MessageBox.Show("Invalid Search", "Invalid Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                authorsTable.DefaultView.Sort = "Author";
                var foundRows = authorsTable.Select($"Author LIKE '*{txtSearch.Text}*'");
                if (foundRows.Length == 0)
                {
                    MessageBox.Show("Couldn't find any record", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    authorsManager.Position = authorsTable.DefaultView.Find(foundRows[0]["Author"]);
                }
            }
        }
    }
}
