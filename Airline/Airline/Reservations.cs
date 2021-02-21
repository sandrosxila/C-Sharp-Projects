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
    public partial class frmReservations : Form
    {
        private Passenger passenger;
        Seat seat;

        public static List<Seat> seats;

        private OleDbCommand command;
        private OleDbDataReader reader;

        public frmReservations()
        {
            InitializeComponent();
            command = new OleDbCommand();
            seats = new List<Seat>();
        }

        private void frmReservations_Load(object sender, EventArgs e)
        {
            PopulateSeatRows();
            PopulateAirplane();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            passenger = new Passenger();
            seat = new Seat();
            var checkedButton = gbRadios.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (!passenger.IsValidPassenger(txtName.Text) || cmbSeatRow.SelectedIndex == -1 || checkedButton == null)
            {
                MessageBox.Show("Please Enter Valid Name and Seat", "Invalid Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (seat.IsPlaneFull())
            {
                var msg = MessageBox.Show("Plane is Full.\r\nAdd Passenger on waiting list?",
                    "Plane is Full", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (msg == DialogResult.No)
                {
                    return;
                }
                else
                {
                    using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
                    {
                        conn.Open();
                        command = new OleDbCommand("INSERT INTO Passengers (PassengerName, PassengerOnWaitingList)" +
                                                   " Values (@passengerName,true)", conn);
                        command.Parameters.Add(new OleDbParameter("passengerName", txtName.Text));
                        command.ExecuteNonQuery();

                        command = new OleDbCommand("INSERT INTO PassengerSeats (PassengerID, SeatID)" + "" +
                                                   "SELECT MAX(PassengerID),0 FROM Passengers", conn);
                        command.ExecuteNonQuery();

                        MessageBox.Show($"Passenger {txtName.Text} was added to the waiting list",
                            "Waiting List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    return;
                }
            }

            if (seat.IsSeatAlreadyTaken(cmbSeatRow.SelectedItem.ToString(), checkedButton.Text))
            {
                MessageBox.Show($"The seat is already taken. Please select different seat.",
                    "Seat Taken", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;  
            }

            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                command = new OleDbCommand("INSERT INTO Passengers (PassengerName)" +
                                           " Values (@passengerName)", conn);
                command.Parameters.Add(new OleDbParameter("passengerName", txtName.Text));
                command.ExecuteNonQuery();

                command = new OleDbCommand("UPDATE Seats SET IsTaken = true WHERE SeatRow = @seatRow AND SeatColumn = @seatColumn", conn);
                command.Parameters.Add(new OleDbParameter("seatRow", cmbSeatRow.Text));
                command.Parameters.Add(new OleDbParameter("seatColumn", checkedButton.Text));
                command.ExecuteNonQuery();

                command = new OleDbCommand("INSERT INTO PassengerSeats (SeatID,PassengerID) " +
                                           "SELECT Seats.SeatID, (SELECT MAX(PassengerID) FROM Passengers) " +
                                           "FROM Seats WHERE Seats.SeatRow = @seatRow AND Seats.SeatColumn = @seatColumn"
                    , conn);
                command.Parameters.Add(new OleDbParameter("seatRow", cmbSeatRow.Text));
                command.Parameters.Add(new OleDbParameter("seatColumn", checkedButton.Text)); 
                command.ExecuteNonQuery();

                MessageBox.Show($"Passenger has been added.",
                    "Seat Taken", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PopulateAirplane();
            }
        }

        private void btnShowPassengers_Click(object sender, EventArgs e)
        {
            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                command = new OleDbCommand(@"
                                            SELECT P.PassengerID AS ID, P.PassengerName AS Name, S.SeatRow, S.SeatColumn,  P.PassengerOnWaitingList AS OnWaitingList
                                            FROM (Passengers as P
                                            INNER JOIN  PassengerSeats as PS 
                                            ON PS.PassengerID = P.PassengerID)
                                            INNER JOIN Seats as S
                                            ON S.SeatID = PS.SeatID
                                            UNION
                                            SELECT P.PassengerID, P.PassengerName, NULL, NULL,  P.PassengerOnWaitingList
                                            FROM Passengers as P
                                            WHERE P.PassengerOnWaitingList = TRUE
                                            ORDER BY S.SeatRow, S.SeatColumn",conn);

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                PassengerLookup form = new PassengerLookup(dt);
                form.ShowDialog();
                PopulateAirplane();
            }
        }

        private void btnSearchPassenger_Click(object sender, EventArgs e)
        {
            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                if (!txtName.Text.Trim().Equals(""))
                {
                    command = new OleDbCommand(@"
                                            SELECT P.PassengerID AS ID, P.PassengerName AS Name, S.SeatRow, S.SeatColumn,  P.PassengerOnWaitingList AS OnWaitingList
                                            FROM (Passengers as P
                                            INNER JOIN  PassengerSeats as PS 
                                            ON PS.PassengerID = P.PassengerID)
                                            INNER JOIN Seats as S
                                            ON S.SeatID = PS.SeatID
                                            WHERE P.PassengerName LIKE @passengerName
                                            UNION
                                            SELECT P.PassengerID, P.PassengerName, NULL, NULL,  P.PassengerOnWaitingList
                                            FROM Passengers as P
                                            WHERE P.PassengerOnWaitingList = TRUE AND P.PassengerName LIKE @passengerName
                                            ORDER BY S.SeatRow, S.SeatColumn", conn);
                    command.Parameters.Add(new OleDbParameter("passengerName", "%" + txtName.Text + "%"));

                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    PassengerLookup form = new PassengerLookup(dt);
                    form.ShowDialog();
                    PopulateAirplane();
                }
                else
                {
                    MessageBox.Show("Please Enter a Valid Name", "Invalid Input", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Display the seats in list box
        private void PopulateAirplane()
        {
            lstOutput.Items.Clear();
            seats.Clear();

            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                command = new OleDbCommand("SELECT * FROM Seats ORDER BY SeatRow, SeatColumn", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    seat = new Seat();
                    seat.SeatID = Convert.ToInt32(reader["SeatID"]);
                    seat.SeatRow = Convert.ToInt32(reader["SeatRow"]);
                    seat.SeatColumn = reader["SeatColumn"].ToString();
                    seat.IsSeatTaken = Convert.ToBoolean(reader["IsTaken"]);
                    seats.Add(seat);
                }

                var msg = "";
                var counter = 0;

                for (int i = 0; i < seats.Count; i++)
                {
                    counter++;
                    if (seats[i].IsSeatTaken)
                    {
                        msg += "  " + "XX" + "  ";
                    }
                    else
                    {
                        msg += "  " + seats[i].SeatRow + seats[i].SeatColumn + "  ";
                    }

                    if (counter % 4 == 0)
                    {
                        lstOutput.Items.Add(msg);
                        msg = "";
                    }
                    else if (counter % 2 == 0)
                    {
                        msg += "       ";
                    }
                }
            }
        }

        //Populate dropdown with seat rows
        private void PopulateSeatRows()
        {
            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                command = new OleDbCommand("SELECT DISTINCT SeatRow from Seats ORDER BY SeatRow", conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cmbSeatRow.Items.Add(reader["SeatRow"]);
                }
            }
        }
    }
}
