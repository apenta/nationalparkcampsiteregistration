using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using System.Data.SqlClient;
using System.Globalization;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL
    {
        private string connectionString;

        public CampgroundSqlDAL(string DatabaseConnection)
        {
            connectionString = DatabaseConnection;
        }

        public List<Campground> ViewAllCampgrounds(Park park)
        {
            List<Campground> output2 = new List<Campground>();
            {

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * from campground WHERE park_id = @ParkId", conn);
                        cmd.Parameters.AddWithValue("@ParkId", park.ParkId);


                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Campground campground = new Campground();
                            campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                            campground.OpeningMonth = Convert.ToInt32(reader["open_from_mm"]);
                            campground.ClosingMonth = Convert.ToInt32(reader["open_to_mm"]);
                            campground.CampName = Convert.ToString(reader["name"]);
                            campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);



                            output2.Add(campground);
                        }
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine("An error occured reading the database: " + ex.Message);
                }
                return output2;

            }


        }
        public Campground GetCampgroundById(int id)
        {
            Campground campground = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * from campground WHERE campground_id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", id);


                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        campground = new Campground();
                        campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        campground.OpeningMonth = Convert.ToInt32(reader["open_from_mm"]);
                        campground.ClosingMonth = Convert.ToInt32(reader["open_to_mm"]);
                        campground.CampName = Convert.ToString(reader["name"]);
                        campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);

                    }
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine("An error occured reading the database: " + ex.Message);
            }
            return campground;
        }

    }

}

