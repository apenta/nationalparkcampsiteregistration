using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone;


namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;

        public ReservationSqlDAL(string DatabaseConnection)
        {
            connectionString = DatabaseConnection;
        }

        public Reservation GetReservationInfo(int reservedSiteId, string reservationName, string arrivalDate, string departureDate)
        {
            Reservation reservation = new Reservation();

            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("INSERT reservation(site_id, name, from_date, to_date, create_date)  VALUES (@reservedSiteId, @reservationName, @arrivalDate, @departureDate, @createDate)", conn);
                        cmd.Parameters.AddWithValue("@reservedSiteId", reservedSiteId);
                        cmd.Parameters.AddWithValue("@reservationName", reservationName);
                        cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                        cmd.Parameters.AddWithValue("@departureDate", departureDate);
                        cmd.Parameters.AddWithValue("@createDate", DateTime.Now);

                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("SELECT * from reservation WHERE reservation_id = (SELECT MAX(reservation_id) FROM reservation);", conn);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            reservation.ReservationId = Convert.ToInt32(reader["reservation_id"]);
                            reservation.SiteId = Convert.ToInt32(reader["site_id"]);
                            reservation.ReservationName = Convert.ToString(reader["name"]);
                            reservation.FromDate = Convert.ToDateTime(reader["from_date"]);
                            reservation.ToDate = Convert.ToDateTime(reader["to_date"]);
                            reservation.DateCreated = Convert.ToDateTime(reader["create_date"]);
                        }
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine("An error occured reading the database: " + ex.Message);
                }
                return reservation;
            }
        }

    }
}
