using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone;


//namespace Capstone.DAL
//{
//    public class ReservationSqDAL
//    {
//        private string connectionString;

//        public ReservationSqlDAL(string DatabaseConnection)
//        {
//            connectionString = DatabaseConnection;
//        }

//        public Reservation ()
//        {
//            Reservation reservation = new Reservation();

//            {
//                try
//                {
//                    using (SqlConnection conn = new SqlConnection(connectionString))
//                    {
//                        conn.Open();

//                        SqlCommand cmd = new SqlCommand("SELECT site_id, from_date, to_date from reservation WHERE site_id = @siteID AND ", conn);
//        //                cmd.Parameters.AddWithValue("@parkName", parkName);

//        //                SqlDataReader reader = cmd.ExecuteReader();

//        //                if (reader.Read())
//        //                {
//        //                    park.ParkId = Convert.ToInt32(reader["park_id"]);
//        //                    park.ParkName = Convert.ToString(reader["name"]);
//        //                    park.Location = Convert.ToString(reader["location"]);
//        //                    park.EstDate = Convert.ToDateTime(reader["establish_date"]);
//        //                    park.Area = Convert.ToInt32(reader["area"]);
//        //                    park.Visitors = Convert.ToInt32(reader["visitors"]);
//        //                    park.Description = Convert.ToString(reader["description"]);


//        //                }
//        //            }
//        //        }

//        //        catch (SqlException ex)
//        //        {
//        //            Console.WriteLine("An error occured reading the database: " + ex.Message);
//        //        }
//        //        return park;
//            }
//        }
//    }

//}
