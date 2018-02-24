using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;
using Capstone.DAL;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationSqlTests
    {
        const string connectionString = @"Server=.\SQLEXPRESS;Database=CampgroundDatabase;";

        [ExpectedException(typeof(SqlException))]
        [TestMethod]
        public void CreateReservation_ThrowsExceptionIfRowExists()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                ReservationSqlTests.InsertFakeReservation(2, "Penta", Convert.ToDateTime("08/01/2018"), Convert.ToDateTime( "08/04/2018"));
                ReservationSqlDAL testClass = new ReservationSqlDAL(connectionString);
                Reservation newReservation = new Reservation();
                newReservation.ReservationId = 59;
                newReservation.SiteId = 2;
                newReservation.ReservationName = "Penta";
                newReservation.FromDate = Convert.ToDateTime("08/01/2018");
                newReservation.ToDate = Convert.ToDateTime("08/04/2018");
            }
        }

        public static void InsertFakeReservation(int reservedSiteId, string reservationName, DateTime arrivalDate, DateTime departureDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT reservation(site_id, name, from_date, to_date, create_date)  VALUES (@reservedSiteId, @reservationName, @arrivalDate, @departureDate, @createDate)");
                cmd.Parameters.AddWithValue("@reservedSiteId", reservedSiteId);
                cmd.Parameters.AddWithValue("@reservationName", reservationName);
                cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                cmd.Parameters.AddWithValue("@departureDate", departureDate);
                cmd.Parameters.AddWithValue("@createDate", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }
    }

}
