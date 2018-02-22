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

        public List<Park> GetAcadiaInfo()
        {
            List<Park> output = new List<Park>();
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * from park WHERE name = 'Acadia'", conn);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Park park = new Park();
                            park.ParkName = Convert.ToString(reader["name"]);
                            park.Location = Convert.ToString(reader["location"]);
                            park.EstDate = Convert.ToDateTime(reader["establish_date"]);
                            park.Area = Convert.ToInt32(reader["area"]);
                            park.Visitors = Convert.ToInt32(reader["visitors"]);
                            park.Description = Convert.ToString(reader["description"]);

                            output.Add(park);
                        }
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine("An error occured reading the database: " + ex.Message);
                }
                return output;
            }
        }
        public List<Park> GetArchesInfo()
        {
            List<Park> output = new List<Park>();
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * from park WHERE name = 'Arches'", conn);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Park park = new Park();
                            park.ParkName = Convert.ToString(reader["name"]);
                            park.Location = Convert.ToString(reader["location"]);
                            park.EstDate = Convert.ToDateTime(reader["establish_date"]);
                            park.Area = Convert.ToInt32(reader["area"]);
                            park.Visitors = Convert.ToInt32(reader["visitors"]);
                            park.Description = Convert.ToString(reader["description"]);

                            output.Add(park);
                        }
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine("An error occured reading the database: " + ex.Message);
                }
                return output;
            }
        }
        public List<Park> GetCVNPInfo()
        {
            List<Park> output = new List<Park>();
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("SELECT * from park WHERE name = 'Cuyahoga Valley'", conn);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Park park = new Park();
                            park.ParkName = Convert.ToString(reader["name"]);
                            park.Location = Convert.ToString(reader["location"]);
                            park.EstDate = Convert.ToDateTime(reader["establish_date"]);
                            park.Area = Convert.ToInt32(reader["area"]);
                            park.Visitors = Convert.ToInt32(reader["visitors"]);
                            park.Description = Convert.ToString(reader["description"]);

                            output.Add(park);
                        }
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine("An error occured reading the database: " + ex.Message);
                }
                return output;
            }
        }


    }

}
