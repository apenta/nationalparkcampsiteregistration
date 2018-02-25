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
    public class CampgroundSqlTests
    {

        const string connectionString = @"Server=.\SQLEXPRESS;Database=CampgroundDatabase;";

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void WrongCampground_ThrowsExceptionIfCampgroundDoesNotExist()
        {
            CampgroundSqlTests.ChooseFakeCampground(5, 1);
            CampgroundSqlDAL testClass = new CampgroundSqlDAL(connectionString);
            Campground newCampground = new Campground();
            Park newPark = new Park();
            newCampground.ParkId = 5;
            newPark.ParkId = 5;
            newCampground.CampgroundId = 1;

            testClass.GetCampgroundById(newPark, newCampground.CampgroundId);


        }
        public static void ChooseFakeCampground(int park_id, int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT into campground VALUES @ParkId, @Id", conn);
                cmd.Parameters.AddWithValue("@ParkId", park_id);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}



