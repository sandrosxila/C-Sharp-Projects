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

namespace SQLTester
{
    public partial class frmTester : Form
    {
        private OleDbConnection conn;
        public frmTester()
        {
            InitializeComponent();
        }

        private void frmTester_Load(object sender, EventArgs e)
        {
            string dataPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                              "\\DB\\Books.accdb";
            var connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dataPath};Persist Security Info=False;";
            conn = new OleDbConnection(connString);
            conn.Open();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            OleDbCommand command = null;
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            DataTable table = new DataTable();

            try
            {
                command = new OleDbCommand(txtCommand.Text,conn);
                adapter.SelectCommand = command;
                adapter.Fill(table);

                grdRecords.DataSource = table;

                lblCount.Text = table.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            command.Dispose();
            adapter.Dispose();
            table.Dispose();
        }

        private void frmTester_Closing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
            conn.Dispose();
        }
    }
}
