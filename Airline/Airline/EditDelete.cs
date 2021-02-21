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
    public partial class EditDelete : Form
    {
        private DataTable table;
        private OleDbCommand command;
        private OleDbDataReader reader;

        public EditDelete(DataTable table)
        {
            InitializeComponent();
            command = new OleDbCommand();
            this.table = table;

        }
        private void EditDelete_Load(object sender, EventArgs e)
        {
            txtPassengerID.DataBindings.Add("Text", table, "ID");
            txtName.DataBindings.Add("Text", table, "Name");
            txtSeatID.DataBindings.Add("Text", table, "SeatID");

            var r = table.Rows[0]["SeatRow"].ToString();
            var row = r.Equals("") ? 0 : Convert.ToInt32(r);
            var c = table.Rows[0]["SeatColumn"].ToString();
            var column = c.Equals("") ? "None" : c;
            cmbRow.SelectedIndex = row;
            cmbColumn.SelectedItem = column;

            chbWaiting.Checked = Convert.ToBoolean(table.Rows[0]["OnWaitingList"]);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Passenger Name is Required.", "Invalid Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (chbWaiting.Checked && (cmbRow.SelectedIndex > 0 || cmbColumn.SelectedIndex > 0))
            {
                MessageBox.Show("Passenger Cannot be on Waiting List and have a seat assigned.", "Bad Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (!chbWaiting.Checked && (cmbRow.SelectedIndex <= 0 || cmbColumn.SelectedIndex <= 0))
            {
                MessageBox.Show("Passenger Should be in waiting list or have a seat.", "Bad Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                command = new OleDbCommand("SELECT SeatID, IsTaken FROM Seats WHERE SeatRow = @seatRow AND SeatColumn = @seatColumn", conn);
                command.Parameters.Add(new OleDbParameter("seatRow", cmbRow.SelectedItem));
                command.Parameters.Add(new OleDbParameter("seatColumn", cmbColumn.SelectedItem));
                reader = command.ExecuteReader();

                var newSeatID = 0;
                bool newIsTaken = false;
                while (reader.Read())
                {
                    newSeatID = Convert.ToInt32(reader["SeatID"]);
                    newIsTaken = Convert.ToBoolean(reader["IsTaken"]);
                }

                if (newSeatID != Convert.ToInt32("0"+txtSeatID.Text) && newIsTaken)
                {
                    MessageBox.Show("Seat is already taken", "Seat Taken", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                command = new OleDbCommand("UPDATE Passengers SET PassengerName = @passengerName, PassengerOnWaitingList = @onWaitingList "
                                            + "WHERE PassengerID = @passengerID", conn);
                command.Parameters.Add(new OleDbParameter("passengerName", txtName.Text));
                command.Parameters.Add(new OleDbParameter("onWaitingList", chbWaiting.Checked));
                command.Parameters.Add(new OleDbParameter("passengerID", txtPassengerID.Text));
                command.ExecuteNonQuery();

                command = new OleDbCommand("UPDATE Seats SET IsTaken = false WHERE SeatID = @seatID", conn);
                command.Parameters.Add(new OleDbParameter("seatID", txtSeatID.Text));
                command.ExecuteNonQuery();

                command = new OleDbCommand("UPDATE Seats SET IsTaken = true WHERE SeatID = @seatID", conn);
                command.Parameters.Add(new OleDbParameter("seatID", newSeatID));
                command.ExecuteNonQuery();

                command = new OleDbCommand("UPDATE PassengerSeats SET SeatID = @seatID WHERE PassengerID = @passengerID", conn);
                command.Parameters.Add(new OleDbParameter("seatID", newSeatID));
                command.Parameters.Add(new OleDbParameter("passengerID", txtPassengerID.Text));
                command.ExecuteNonQuery();

                MessageBox.Show($"Passenger has been updated.",
                    "Seat Taken", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Are you sure you want to delete {txtName.Text} from the Database?",
                "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No)
                return;
            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                

                command = new OleDbCommand("DELETE FROM Passengers WHERE PassengerID = @passengerID", conn);
                command.Parameters.Add(new OleDbParameter("passengerID", txtPassengerID.Text));
                command.ExecuteNonQuery();

                command = new OleDbCommand("DELETE FROM PassengerSeats WHERE PassengerID = @passengerID", conn);
                command.Parameters.Add(new OleDbParameter("passengerID", txtPassengerID.Text));
                command.ExecuteNonQuery();


                if (!txtSeatID.Text.Equals(""))
                {
                    command = new OleDbCommand("UPDATE Seats SET IsTaken = false WHERE SeatID = @seatID", conn);
                    command.Parameters.Add(new OleDbParameter("seatID", txtSeatID.Text));
                    command.ExecuteNonQuery();
                }


                MessageBox.Show("Record has been deleted.",
                    "Deletion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
