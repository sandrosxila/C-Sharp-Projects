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

namespace Books
{
    public partial class frmTitles : Form
    {
        private OleDbConnection conn;
        private OleDbCommand titlesCommand;
        private OleDbDataAdapter titlesAdapter;
        private DataTable titlesTable;
        private CurrencyManager titlesManager;

        public frmTitles()
        {
            InitializeComponent();
        }

        private void frmTitles_Load(object sender, EventArgs e)
        {
            string dataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                              "\\DB\\Books.accdb";
            var connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dataPath};Persist Security Info=False;";
            conn = new OleDbConnection(connString);
            conn.Open();
            titlesCommand = new OleDbCommand("Select * from Titles", conn);
            titlesAdapter = new OleDbDataAdapter();
            titlesAdapter.SelectCommand = titlesCommand;
            titlesTable = new DataTable();
            titlesAdapter.Fill(titlesTable);

            //bind controls
            txtTitle.DataBindings.Add("Text", titlesTable, "Title");
            txtYear.DataBindings.Add("Text", titlesTable, "Year_Published");
            txtISBN.DataBindings.Add("Text", titlesTable, "ISBN");
            txtPubID.DataBindings.Add("Text", titlesTable, "PubId");

            // establish currency manager
            titlesManager = (CurrencyManager)BindingContext[titlesTable];

            conn.Close();
            conn.Dispose();
            titlesAdapter.Dispose();
            titlesTable.Dispose();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            titlesManager.Position = 0;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            titlesManager.Position = Math.Max(titlesManager.Position - 1, 0);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            titlesManager.Position = Math.Min(titlesManager.Position + 1, titlesManager.Count - 1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            titlesManager.Position = titlesManager.Count - 1;
        }
    }
}
