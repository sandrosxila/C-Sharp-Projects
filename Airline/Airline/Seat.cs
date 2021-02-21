using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline
{
    public class Seat
    {
        public int SeatID { get; set; }
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
        public bool IsSeatTaken { get; set; }
        private OleDbCommand command;
        private OleDbDataReader reader;

        //Check if a plane is already full (it is used to place passenger on waiting list)
        public bool IsPlaneFull()
        {
            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                command  = new OleDbCommand($"SELECT * FROM Seats WHERE IsTaken = false",conn);
                reader = command.ExecuteReader();
                return !reader.HasRows;
            }
        }

        //Check if seat is already taken
        public bool IsSeatAlreadyTaken(string seatRow, string seatColumn)
        {
            using (var conn = new OleDbConnection(DatabaseObjects.connectionString))
            {
                conn.Open();
                command = new OleDbCommand(
                    "SELECT * FROM Seats WHERE SeatRow = @SeatRow AND SeatColumn = @SeatColumn AND IsTaken = false",
                    conn);
                command.Parameters.Add(new OleDbParameter("SeatRow", seatRow));
                command.Parameters.Add(new OleDbParameter("SeatColumn", seatColumn));
                reader = command.ExecuteReader();
                return !reader.HasRows;
            }
        }
    }
}
