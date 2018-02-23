using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampSiteSqlDAL
    {
        private string connectionString;

        public CampSiteSqlDAL(string DatabaseConnection)
        {
            connectionString = DatabaseConnection;
        }

        public List<CampSite> Search(int campgroundId, string arrivalDate, string departureDate)
        {
            List<CampSite> output = new List<CampSite>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    
                    SqlCommand cmd = new SqlCommand("SELECT site_number, max_occupancy, accessible, max_rv_length, utilities, daily_fee from site JOIN campground ON campground.campground_id = site.campground_id WHERE campground.campground_id = (@campgroundId);", conn);
                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                    cmd.Parameters.AddWithValue("@departureDate", departureDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CampSite campSite = new CampSite();
                        Campground campground = new Campground();
                        
                        campSite.CampsiteNumber = Convert.ToInt32(reader["site_number"]);
                        campSite.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        campSite.Accessible = Convert.ToBoolean(reader["accessible"]);
                        campSite.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                        campSite.Utilities = Convert.ToBoolean(reader["utilities"]);
                        campSite.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
                     

                        output.Add(campSite);
                        //output.Add(campground);
                    
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error reading the database: " + ex.Message);
            }
            return output;
        }
    }

    
}
