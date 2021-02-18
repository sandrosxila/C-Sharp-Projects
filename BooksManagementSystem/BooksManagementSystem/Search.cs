using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksManagementSystem
{
    public partial class frmSearch : Form
    {

        private DataRow[] _dataRows;
        private string _formUsed;
        public int Index { get; set; }
        public frmSearch()
        {
            InitializeComponent();
        }

        public frmSearch(DataRow[] dataRows, string formUsed)
        {
            _dataRows = dataRows;
            _formUsed = formUsed;
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            foreach (var result in _dataRows)
            {
                lstSearchResults.Items.Add(result[1]);
            }
        }

        private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            Index = lstSearchResults.SelectedIndex;
            Close();
        }
    }
}
