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

namespace NorthWindDB
{
    public partial class frmTitles : Form
    {
        private OleDbConnection conn;
        private OleDbCommand customersCommand;
        private OleDbDataAdapter customersAdapter;
        private DataTable customersTable;
        private CurrencyManager customersManager;

        public frmTitles()
        {
            InitializeComponent();
        }

        private void frmTitles_Load(object sender, EventArgs e)
        {
            string sourcePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName
                + @"\DB\NorthWind.accdb";
            string connString =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={sourcePath};Persist Security Info=False;";
            conn = new OleDbConnection(connString);
            conn.Open();
            customersCommand = new OleDbCommand("Select * from Customers", conn);
            customersAdapter = new OleDbDataAdapter(customersCommand);
            customersTable = new DataTable();
            customersAdapter.Fill(customersTable);
            txtCustomerId.DataBindings.Add("Text", customersTable, "CustomerID");
            txtCompanyName.DataBindings.Add("Text", customersTable, "CompanyName");
            txtContactName.DataBindings.Add("Text", customersTable, "ContactName");
            txtContactTitle.DataBindings.Add("Text", customersTable, "ContactTitle");
            customersManager = (CurrencyManager)BindingContext[customersTable];
            conn.Close();
            conn.Dispose();
            customersAdapter.Dispose();
            customersTable.Dispose();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            customersManager.Position = 0;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            customersManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            customersManager.Position++;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            customersManager.Position = customersManager.Count - 1;
        }
    }
}
