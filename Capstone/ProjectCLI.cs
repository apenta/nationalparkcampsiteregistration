using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using System.Globalization;


namespace Capstone
{
    public class ProjectCLI
    {
        const string Command_Acadia = "1";
        const string Command_Arches = "2";
        const string Command_CuyahogaNationalValleyPark = "3";
        const string Command_Quit = "q";

        static string DatabaseConnection = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        CampgroundSqlDAL campgroundSqlDAL = null;
        static ReservationSqlDAL reservationsqlDAL = new ReservationSqlDAL(DatabaseConnection);


        public void MainParkList()
        {
            campgroundSqlDAL = new CampgroundSqlDAL(DatabaseConnection);
            PrintHeader();
            ParkMenu();

            Park park = new Park();

            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_Acadia:
                        park = GetParkInfo("Acadia");
                        break;

                    case Command_Arches:
                        park = GetParkInfo("Arches");
                        break;

                    case Command_CuyahogaNationalValleyPark:
                        park = GetParkInfo("Cuyahoga Valley");
                        break;

                    case Command_Quit:
                        Console.Clear();
                        Console.WriteLine("Thank you for using National Park Campsite Reservation!");
                        MainParkList();
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        Console.WriteLine();
                        MainParkList();
                        break;
                }

                CampgroundList(park);
            }
        }
        //Campground List
        const string Command_ViewCampgrounds = "1";
        const string Command_SearchForReservations = "2";
        const string Command_ReturnToPreviousScreen = "3";


        private void CampgroundList(Park park)
        {
//            PrintHeader();
            CampgroundMenu();


            while (true)
            {
                string command = Console.ReadLine();
                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_ViewCampgrounds:
                        ViewAllCampgrounds(park);
                        CampgroundMenu();
                        break;

                    case Command_SearchForReservations:
                        ReservationSearch(park);
                        break;

                    case Command_ReturnToPreviousScreen:
                        Console.Clear();
                        MainParkList();
                        break;


                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        CampgroundMenu();
                        break;
                }


            }
        }

        public void ReservationSearch(Park park)
        {
            ViewAllCampgrounds(park);

            int campgroundId = CLIHelper.GetInteger("Which campground (enter 0 to cancel)? ");

            if (campgroundId == 0)
            {
                MainParkList();
            }

            else
            {
//                List<Campground> campgrounds = campgroundSqlDAL.ViewAllCampgrounds(park);
                Campground campground = campgroundSqlDAL.GetCampgroundById(park, campgroundId);

                if (campgroundId != campground.CampgroundId)
                {
                    Console.WriteLine("Sorry, that was an invalid input! Please start over!");
                    return;
                }
                else
                {
                    string arrivalDate = CLIHelper.GetString("What is the arrival date? MM/DD/YYYY");
                    string departureDate = CLIHelper.GetString("What is the departure date? MM/DD/YYYY");

                    int arrivalMonth = Int32.Parse(arrivalDate.Substring(0, 2));
                    int departureMonth = Int32.Parse(departureDate.Substring(0, 2));

                    int totalStay = (DateTime.Parse(departureDate) - DateTime.Parse(arrivalDate)).Days;

                    if (((arrivalMonth >= campground.OpeningMonth) && (arrivalMonth <= campground.ClosingMonth))
                        && ((departureMonth >= campground.OpeningMonth) && (departureMonth <= campground.ClosingMonth)))
                    {
                        CampSiteSqlDAL dal = new CampSiteSqlDAL(DatabaseConnection);
                        List<CampSite> campSites = dal.Search(campgroundId, arrivalDate, departureDate);

                        if (campSites.Count > 0)
                        {
                            Console.WriteLine("Site No.    Max Occup.    Accessible?    Max RV Length   Utility   Cost");

                            foreach (CampSite site in campSites)
                            {
                                Console.WriteLine();
                                Console.WriteLine($"{site.CampsiteNumber}    {site.MaxOccupancy}     {TranslateBoolAccessible(site.Accessible)}      {site.MaxRvLength}     {TranslateBoolUtilities(site.Utilities)}   {site.DailyFee.ToString("C")}");

                            }
                            Console.WriteLine();
                            Console.WriteLine($"Total cost for stay: ${totalStay * campground.DailyFee:00.00}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("**** BOOKED OUT!! ****");
                            Console.WriteLine("Please try another date or another campground.");
                            Console.WriteLine();
                            ReservationSearch(park);
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Sorry, campground is closed, LOSER!");
                        Console.WriteLine();
                        MainParkList();
                    }

                    int reservedSiteId = CLIHelper.GetInteger("Which site should be reserved (enter 0 to cancel)?");
                    if (reservedSiteId == 0)
                    {
                        Console.Clear();
                        MainParkList();
                    }
                    else
                    {
                        string reservationName = CLIHelper.GetString("What name should the reservation be made under?");
                        Reservation r = reservationsqlDAL.GetReservationInfo(reservedSiteId, reservationName, arrivalDate, departureDate);
                        GetReservationId(r);
                    }

                }

            }
        }

        private void GetReservationId(Reservation reservation)
        {
            Console.WriteLine();
            Console.WriteLine($"Congratulations, {reservation.ReservationName}, your reservation has been made for {reservation.FromDate.ToString("MM/dd/yyyy")} to {reservation.ToDate.ToString("MM/dd/yyyy")}."); //at {campground.CampName} 
            Console.WriteLine($"Your confirmation id is {reservation.ReservationId}.");
            Console.WriteLine();
            Console.WriteLine($"Enjoy your stay!");
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine();
            Console.WriteLine();
            MainParkList();
        }

        private string TranslateBoolAccessible(bool accessible)
        {
            string result = "";

            if (accessible == true)
            {
                result = "Yes";
            }
            else if (accessible == false)
            {
                result = "No";
            }
            return result;
        }

        private string TranslateBoolUtilities(bool utilities)
        {
            string result = "";

            if (utilities == true)
            {
                result = "Yes";
            }
            else if (utilities == false)
            {
                result = "N/A";
            }
            return result;
        }

        private string TranslateMonth(int month)
        {
            string result = "";

            if (month == 1)
            {
                result = "January";
            }
            else if (month == 2)
            {
                result = "February";
            }
            else if (month == 3)
            {
                result = "March";
            }
            else if (month == 4)
            {
                result = "April";
            }
            else if (month == 5)
            {
                result = "May";
            }
            else if (month == 6)
            {
                result = "June";
            }
            else if (month == 7)
            {
                result = "July";
            }
            else if (month == 8)
            {
                result = "August";
            }
            else if (month == 9)
            {
                result = "September";
            }
            else if (month == 10)
            {
                result = "October";
            }
            else if (month == 11)
            {
                result = "November";
            }
            else if (month == 12)
            {
                result = "December";
            }
            return result;
        }

        private void ViewAllCampgrounds(Park park)
        {
            //            CampgroundSqlDAL dal = new CampgroundSqlDAL(DatabaseConnection);
            List<Campground> campgrounds = campgroundSqlDAL.ViewAllCampgrounds(park);

            if (campgrounds.Count > 0)
            {
                Console.WriteLine("Number".PadRight(11) + "Name".PadRight(35) + "Open".PadRight(20) + "Close".PadRight(20) + "Daily Fee".PadRight(0));

                foreach (Campground campground in campgrounds)
                {
                    Console.WriteLine("#".PadRight(0) +  (campground.CampgroundId).ToString().PadRight(10) + (campground.CampName).PadRight(35) + TranslateMonth(campground.OpeningMonth).PadRight(20) + TranslateMonth(campground.ClosingMonth).PadRight(20) + campground.DailyFee.ToString("C").PadRight(20));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
                Console.WriteLine();
            }
            return;
        }




        private Park GetParkInfo(string parkName)
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            Park park = dal.GetParkInfo(parkName);

            if (park != null)
            {
                Console.WriteLine(park.ParkName + " National Park");
                Console.WriteLine("Location:" + "\t" + park.Location);
                Console.WriteLine("Established:" + "\t" + park.EstDate.ToString("MM/dd/yyyy"));
                Console.WriteLine($"Area: {park.Area:###,###,##0} sq km");
                Console.WriteLine($"Annual Visitors: {park.Visitors:###,###,##0}");
                Console.WriteLine();
                Console.WriteLine(park.Description);
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
                Console.WriteLine();
            }
            return park;
        }

        private void PrintHeader()
        {
            Console.WriteLine(" __    _  _______  _______  ___   _______  __    _  _______  ___        _______  _______  ______    ___   _  _______");
            Console.WriteLine("|  |  | ||   _   ||       ||   | |       ||  |  | ||   _   ||   |      |       ||   _   ||    _ |  |   | | ||       |");
            Console.WriteLine("|   |_| ||  |_|  ||_     _||   | |   _   ||   |_| ||  |_|  ||   |      |    _  ||  |_|  ||   | ||  |   |_| ||  _____|");
            Console.WriteLine("|       ||       |  |   |  |   | |  | |  ||       ||       ||   |      |   |_| ||       ||   |_||_ |      _|| |_____");
            Console.WriteLine("|  _    ||       |  |   |  |   | |  |_|  ||  _    ||       ||   |___   |    ___||       ||    __  ||     |_ |_____  |");
            Console.WriteLine("| | |   ||   _   |  |   |  |   | |       || | |   ||   _   ||       |  |   |    |   _   ||   |  | ||    _  | _____| |");
            Console.WriteLine("|_|  |__||__| |__|  |___|  |___| |_______||_|  |__||__| |__||_______|  |___|    |__| |__||___|  |_||___| |_||_______|");
            Console.WriteLine();
        }
        private void ParkMenu()
        {
            Console.WriteLine("Please select park you would like to view: ");
            Console.WriteLine(" 1) - Acadia");
            Console.WriteLine(" 2) - Arches");
            Console.WriteLine(" 3) - Cuyahoga National Valley Park");
            Console.WriteLine(" Q) - Quit");
            Console.WriteLine();

        }

        private void CampgroundMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine(" 1) View Campgrounds");
            Console.WriteLine(" 2) Search for Reservation");
            Console.WriteLine(" 3) Return to Previous Screen");
            Console.WriteLine();
        }

        private void SearchMenu()
        {
            Console.WriteLine("Search for Campground Reservation");
            Console.WriteLine();
            Console.WriteLine("Which campground (enter 0 to cancel)? ");
            Console.WriteLine("What is the arrival date? __/__/___");
            Console.WriteLine("What is the departure date? __/__/___");
            Console.WriteLine();
        }

        private void MakeReservationMenu()
        {
            Console.WriteLine("Which site should be reserved (enter 0 to cancel)?");
            Console.WriteLine("What name should the reservation be made under?");

        }
   
        //public class StringHelpers
        //{
        //    /// <summary>
        //    /// Convert boolean value to "Yes" or "No"
        //    /// </summary>
        //    /// <param name="b"></param>
        //    /// <returns></returns>
        //    public static string ConvertBoolToYesNo(bool Accessible)
        //    {
        //        if (Accessible == true) { return "Yes"; }

        //        else return "No";
        //    }
        //}

    }
}

