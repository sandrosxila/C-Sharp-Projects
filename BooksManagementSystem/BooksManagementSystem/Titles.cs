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
    public partial class frmTitles : Form
    {
        public frmTitles()
        {
            InitializeComponent();
        }

        private OleDbConnection booksConn;
        private OleDbCommand titlesComm;
        private OleDbDataAdapter titlesAdapter;
        private DataTable titlesTable;
        private CurrencyManager titlesManager;
        private OleDbCommandBuilder builderComm;
        private OleDbCommand authorsComm;
        private OleDbDataAdapter authorsAdapter;
        private DataTable[] authorsTable = new DataTable[4];
        private ComboBox[] authorsCombo = new ComboBox[4];
        private OleDbCommand publishersComm;
        private OleDbDataAdapter publishersAdapter;
        private DataTable publishersTable;
        private OleDbCommand ISBNAuthorsComm;
        private OleDbDataAdapter ISBNAuthorsAdapter;
        private DataTable ISBNAuthorsTable;
        private bool dbError = false;
        private State appState;
        private int CurrentPosition { get; set; }

        private enum State
        {
            View,
            Edit,
            Add,
            Delete
        }

        private void frmTitles_Load(object sender, EventArgs e)
        {
            try
            {
                string dataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                                  "\\DB\\Books.accdb";
                var connString =
                    $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dataPath};Persist Security Info=False;";
                booksConn = new OleDbConnection(connString);
                booksConn.Open();

                titlesComm = new OleDbCommand("SELECT * FROM Titles ORDER BY Title", booksConn);
                titlesAdapter = new OleDbDataAdapter(titlesComm);
                titlesTable = new DataTable();
                titlesAdapter.Fill(titlesTable);
                txtTitle.DataBindings.Add("Text", titlesTable, "Title");
                txtYearPublished.DataBindings.Add("Text", titlesTable, "Year_Published");
                txtISBN.DataBindings.Add("Text", titlesTable, "ISBN");
                txtDescription.DataBindings.Add("Text", titlesTable, "Description");
                txtNotes.DataBindings.Add("Text", titlesTable, "Notes");
                txtSubject.DataBindings.Add("Text", titlesTable, "Subject");
                txtComments.DataBindings.Add("Text", titlesTable, "Comments");
                titlesManager = (CurrencyManager)BindingContext[titlesTable];
                authorsCombo[0] = cboAuthor1;
                authorsCombo[1] = cboAuthor2;
                authorsCombo[2] = cboAuthor3;
                authorsCombo[3] = cboAuthor4;

                authorsComm = new OleDbCommand("SELECT * FROM Authors ORDER BY Author", booksConn);
                authorsAdapter = new OleDbDataAdapter(authorsComm);

                for (int i = 0; i < 4; i++)
                {
                    authorsTable[i] = new DataTable();
                    authorsAdapter.Fill(authorsTable[i]);
                    authorsCombo[i].DataSource = authorsTable[i];
                    authorsCombo[i].DisplayMember = "Author";
                    authorsCombo[i].ValueMember = "AU_ID";
                    authorsCombo[i].SelectedIndex = -1;
                }

                publishersComm = new OleDbCommand("SELECT * FROM Publishers ORDER BY Name", booksConn);
                publishersAdapter = new OleDbDataAdapter(publishersComm);
                publishersTable = new DataTable();
                publishersAdapter.Fill(publishersTable);
                cboPublisher.DataSource = publishersTable;
                cboPublisher.DisplayMember = "Name";
                cboPublisher.ValueMember = "PubID";
                cboPublisher.DataBindings.Add("SelectedValue", titlesTable, "PubID");

                setAppState(State.View);
                GetAuthors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTitles_Closing(object sender, FormClosingEventArgs e)
        {
            booksConn.Close();
            booksConn.Dispose();
            titlesComm.Dispose();
            titlesAdapter.Dispose();
            titlesTable.Dispose();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            titlesManager.Position = 0;
            GetAuthors();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            titlesManager.Position--;
            GetAuthors();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            titlesManager.Position++;
            GetAuthors();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            titlesManager.Position = titlesManager.Count - 1;
            GetAuthors();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtFind.Text.Equals("") || txtFind.Text.Length < 3)
            {
                MessageBox.Show("Invalid Input", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                titlesTable.DefaultView.Sort = "Title";
                var foundedRows = titlesTable.Select($"Title Like '*{txtFind.Text}*'");
                if (foundedRows.Length == 0)
                {
                    MessageBox.Show("Record not found", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    titlesManager.Position = titlesTable.DefaultView.Find(foundedRows[0]["Title"]);
                    GetAuthors();
                }
            }
        }

        private void setAppState(State appState)
        {
            this.appState = appState;
            switch (appState)
            {
                case State.View:
                    txtTitle.ReadOnly = true;
                    txtYearPublished.ReadOnly = true;
                    txtISBN.ReadOnly = true;
                    txtDescription.ReadOnly = true;
                    txtNotes.ReadOnly = true;
                    txtSubject.ReadOnly = true;
                    txtComments.ReadOnly = true;
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnAddNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDone.Enabled = true;
                    btnFind.Enabled = true;
                    btnAuthors.Enabled = true;
                    btnPublishers.Enabled = true;
                    cboAuthor1.Enabled = false;
                    cboAuthor2.Enabled = false;
                    cboAuthor3.Enabled = false;
                    cboAuthor4.Enabled = false;
                    cboPublisher.Enabled = false;
                    btnXAuthor1.Visible = false;
                    btnXAuthor2.Visible = false;
                    btnXAuthor3.Visible = false;
                    btnXAuthor4.Visible = false;
                    break;
                default:
                    txtTitle.ReadOnly = false;
                    txtYearPublished.ReadOnly = false;
                    txtISBN.ReadOnly = appState != State.Add;
                    txtDescription.ReadOnly = false;
                    txtNotes.ReadOnly = false;
                    txtSubject.ReadOnly = false;
                    txtComments.ReadOnly = false;
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnAddNew.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDone.Enabled = false;
                    btnFind.Enabled = false;
                    btnAuthors.Enabled = false;
                    btnPublishers.Enabled = false;
                    cboAuthor1.Enabled = true;
                    cboAuthor2.Enabled = true;
                    cboAuthor3.Enabled = true;
                    cboAuthor4.Enabled = true;
                    cboPublisher.Enabled = true;
                    btnXAuthor1.Visible = true;
                    btnXAuthor2.Visible = true;
                    btnXAuthor3.Visible = true;
                    btnXAuthor4.Visible = true;
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtYearPublished.DataBindings.Clear();
            setAppState(State.Edit);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            CurrentPosition = titlesManager.Position;
            setAppState(State.Add);
            titlesManager.AddNew();
            for (int i = 0; i < 4; i++)
            {
                authorsCombo[i].SelectedIndex = -1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            titlesManager.CancelCurrentEdit();
            if (appState == State.View)
            {
                txtYearPublished.DataBindings.Add("Text", titlesTable, "Year_Published");
            }
            else if (appState == State.Add)
            {
                titlesManager.Position = CurrentPosition;
            }
            GetAuthors();
            setAppState(State.View);
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
                titlesManager.RemoveAt(titlesManager.Position);
                builderComm = new OleDbCommandBuilder(titlesAdapter);
                titlesAdapter.Update(titlesTable);
                setAppState(State.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Record Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool ValidateInput()
        {
            string message = "";
            int inputYear, currentYear;
            bool allOK = true;
            if (!txtYearPublished.Text.Trim().Equals(""))
            {
                inputYear = Convert.ToInt32(txtYearPublished.Text);
                currentYear = DateTime.Now.Year;
                if (inputYear > currentYear)
                {
                    message += "Year published cannot be greater than current year \r\n";
                    allOK = false;
                }
            }

            if (txtISBN.Text.Length != 13)
            {
                message += "Incomplete ISBN\r\n";
                allOK = false;
            }

            if (!allOK)
            {
                MessageBox.Show(message, "Invalid input", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            return allOK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    var savedRecord = txtISBN.Text;
                    titlesManager.EndCurrentEdit();
                    builderComm = new OleDbCommandBuilder(titlesAdapter);
                    if (appState == State.Edit)
                    {
                        var titleRow = titlesTable.Select($"ISBN = '{savedRecord}'");

                        if (string.IsNullOrEmpty(txtYearPublished.Text))
                            titleRow[0]["Year_Published"] = DBNull.Value;
                        else titleRow[0]["Year_Published"] = txtYearPublished.Text;
                        titlesTable.DefaultView.Sort = "Title";
                        titlesManager.Position = titlesTable.DefaultView.Find(savedRecord);
                        titlesAdapter.Update(titlesTable);
                        txtYearPublished.DataBindings.Add("Text", titlesTable, "Year_Published");
                    }
                    else if (appState == State.Add)
                    {
                        titlesAdapter.Update(titlesTable);
                        titlesTable.DefaultView.Sort = "Title";
                        var foundRecord = titlesTable.Select($"ISBN = '{savedRecord}'");
                        titlesManager.Position = titlesTable.DefaultView.Find(foundRecord[0]["Title"]);
                        ISBNAuthorsComm = new OleDbCommand($"SELECT * FROM Title_Author WHERE ISBN = '{txtISBN.Text}'", booksConn);
                        ISBNAuthorsAdapter = new OleDbDataAdapter(ISBNAuthorsComm);
                        ISBNAuthorsTable = new DataTable();
                        ISBNAuthorsAdapter.Fill(ISBNAuthorsTable);
                    }

                    builderComm = new OleDbCommandBuilder(ISBNAuthorsAdapter);
                    if (ISBNAuthorsTable.Rows.Count > 0 && appState == State.Edit)
                    {
                        for (int i = 0; i < ISBNAuthorsTable.Rows.Count; i++)
                        {
                            ISBNAuthorsTable.Rows[i].Delete();
                        }

                        ISBNAuthorsAdapter.Update(ISBNAuthorsTable);
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        if (authorsCombo[i].SelectedIndex != -1)
                        {
                            ISBNAuthorsTable.Rows.Add();
                            ISBNAuthorsTable.Rows[ISBNAuthorsTable.Rows.Count - 1]["ISBN"] = txtISBN.Text;
                            ISBNAuthorsTable.Rows[ISBNAuthorsTable.Rows.Count - 1]["AU_ID"] =
                                authorsCombo[i].SelectedValue;
                        }
                    }
                    ISBNAuthorsAdapter.Update(ISBNAuthorsTable);

                    MessageBox.Show("Record is saved successfully", "Saving Record",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setAppState(State.View);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Record Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtYearPublished_KeyPress(object sender, KeyPressEventArgs e)
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

        private void GetAuthors()
        {
            for (int i = 0; i < 4; i++)
            {
                authorsCombo[i].SelectedIndex = -1;
            }
            ISBNAuthorsComm = new OleDbCommand($"SELECT * FROM Title_Author WHERE ISBN = '{txtISBN.Text}'", booksConn);
            ISBNAuthorsAdapter = new OleDbDataAdapter(ISBNAuthorsComm);
            ISBNAuthorsTable = new DataTable();
            ISBNAuthorsAdapter.Fill(ISBNAuthorsTable);
            if (ISBNAuthorsTable.Rows.Count == 0)
                return;

            for (int i = 0; i < ISBNAuthorsTable.Rows.Count; i++)
            {
                authorsCombo[i].SelectedValue = ISBNAuthorsTable.Rows[i]["AU_ID"].ToString();
            }
        }


        private void btnXAuthor_Click(object sender, EventArgs e)
        {
            Button btnClicked = (Button)sender;
            switch (btnClicked.Name)
            {
                case "btnXAuthor1":
                    cboAuthor1.SelectedIndex = -1;
                    break;
                case "btnXAuthor2":
                    cboAuthor2.SelectedIndex = -1;
                    break;
                case "btnXAuthor3":
                    cboAuthor3.SelectedIndex = -1;
                    break;
                case "btnXAuthor4":
                    cboAuthor4.SelectedIndex = -1;
                    break;
                default:
                    break;
            }
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            var authorForm = new frmAuthors();
            authorForm.ShowDialog();
            authorForm.Dispose();
            booksConn.Close();
            string dataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                              "\\DB\\Books.accdb";
            var connString =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dataPath};Persist Security Info=False;";
            booksConn = new OleDbConnection(connString);
            booksConn.Open();
            authorsAdapter.SelectCommand = authorsComm;

            for (int i = 0; i < 4; i++)
            {
                authorsTable[i] = new DataTable();
                authorsAdapter.Fill(authorsTable[i]);
                authorsCombo[i].DataSource = authorsTable[i];
            }
            GetAuthors();
        }

        private void btnPublishers_Click(object sender, EventArgs e)
        {
            var publishersForm = new frmPublishers();
            publishersForm.ShowDialog();
            publishersForm.Dispose();
            booksConn.Close();
            string dataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                              "\\DB\\Books.accdb";
            var connString =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dataPath};Persist Security Info=False;";
            booksConn = new OleDbConnection(connString);
            booksConn.Open();
            cboPublisher.DataBindings.Clear();
            publishersAdapter.SelectCommand = publishersComm;
            publishersTable = new DataTable();
            publishersAdapter.Fill(publishersTable);
            cboPublisher.DataSource = publishersTable;
            cboPublisher.DisplayMember = "Name";
            cboPublisher.ValueMember = "PubID";
            cboPublisher.DataBindings.Add("SelectedValue", titlesTable, "PubID");
        }
    }
}
