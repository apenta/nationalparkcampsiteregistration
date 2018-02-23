using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone;



namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;

        public ParkSqlDAL(string DatabaseConnection)
        {
            connectionString = DatabaseConnection;
        }


        public Park GetParkInfo(string parkName)
        {
            Park park = new Park();
            
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * from park WHERE name = @parkName", conn);
                        cmd.Parameters.AddWithValue("@parkName", parkName);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            park.ParkId = Convert.ToInt32(reader["park_id"]);
                            park.ParkName = Convert.ToString(reader["name"]);
                            park.Location = Convert.ToString(reader["location"]);
                            park.EstDate = Convert.ToDateTime(reader["establish_date"]);
                            park.Area = Convert.ToInt32(reader["area"]);
                            park.Visitors = Convert.ToInt32(reader["visitors"]);
                            park.Description = Convert.ToString(reader["description"]);

                       
                        }
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine("An error occured reading the database: " + ex.Message);
                }
                return park;
            }
        }

      


    }

}
