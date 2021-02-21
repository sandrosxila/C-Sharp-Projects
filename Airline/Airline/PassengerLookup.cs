using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Airline
{
    public partial class PassengerLookup : Form
    {
        private OleDbCommand command;
        private DataTable table;
        public PassengerLookup(DataTable table)
        {
            InitializeComponent();
            this.table = table;
        }

        private void PassengerLookup_Load(object sender, EventArgs e)
        {
            dgvOutput.DataSource = table;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvOutput_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            int selectedID = Convert.ToInt32(dgvOutput.Rows[index].Cells[0].Value);
            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                command = new OleDbCommand(@"
                                            SELECT P.PassengerID AS ID, S.SeatID, P.PassengerName AS Name, S.SeatRow, S.SeatColumn, P.PassengerOnWaitingList AS OnWaitingList
                                            FROM (Passengers as P
                                            INNER JOIN  PassengerSeats as PS 
                                            ON PS.PassengerID = P.PassengerID)
                                            INNER JOIN Seats as S
                                            ON S.SeatID = PS.SeatID
                                            WHERE P.PassengerID LIKE @passengerID
                                            UNION
                                            SELECT P.PassengerID, NULL, P.PassengerName, NULL, NULL, P.PassengerOnWaitingList
                                            FROM Passengers as P
                                            WHERE P.PassengerOnWaitingList = TRUE AND P.PassengerID LIKE @passengerID
                                            ORDER BY S.SeatRow, S.SeatColumn", conn);
                command.Parameters.Add(new OleDbParameter("passengerID", selectedID));

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                EditDelete form = new EditDelete(dt);
                form.ShowDialog();
                Close();
            }
            
        }
    }
}
