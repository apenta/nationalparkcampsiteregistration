using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;

namespace Capstone
{
    public class ProjectCLI
    {
        const string Command_Acadia = "1";
        const string Command_Arches = "2";
        const string Command_CuyahogaNationalValleyPark = "3";
        const string Command_Quit = "q"; 
        string DatabaseConnection = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;


        public void MainParkList()
        {
            PrintHeader();
            ParkMenu();

            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_Acadia:
                        GetAcadiaInfo();
                        break;

                    case Command_Arches:
                        GetArchesInfo();
                        break;

                    case Command_CuyahogaNationalValleyPark:
                        GetCVNPInfo();
                        break;

                    case Command_Quit:
                        Console.WriteLine("Thank you for using National Park Campsite Reservation!");
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }

                ParkMenu();
            }
        }

        const string Command_ViewCampgrounds = "1";
        const string Command_SearchForReservations = "2";
        const string Command_ReturnToPreviousScreen = "3";
       

        private void CampgroundList()
        {
            PrintHeader();
            CampgroundMenu();
            

            while (true)
            {
                string command = Console.ReadLine();
                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_ViewCampgrounds:
                        ViewAllCampgrounds();
                        break;

                    case Command_SearchForReservations:
                        SearchReservations();
                        break;

                    case Command_ReturnToPreviousScreen:
                        PreviousScreen();
                        return;

                  
                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }

                CampgroundMenu();
            }
        }
        //CAMPGROUND METHODS
        private void PreviousScreen()
        {
            throw new NotImplementedException();
        }

        private void SearchReservations()
        {
            throw new NotImplementedException();
        }

        private void ViewAllCampgrounds()
        {
            CampgroundSqlDAL dal = new CampgroundSqlDAL(DatabaseConnection);
            List<Campground> campgrounds = dal.ViewAllCampgrounds();

            if (campgrounds.Count > 0)
            {
                foreach (Campground campground in campgrounds)
                {
                    Console.WriteLine($"Name           Open             Close           Daily Fee");
                    Console.WriteLine($"{campground.CampgroundId}    {campground.CampName}    {campground.OpeningMonth});
                    Console.WriteLine("Location:" + "\t" + park.Location);
                    Console.WriteLine("Established:" + "\t" + park.EstDate.ToString("MM/dd/yyyy"));
                    Console.WriteLine("Area:" + "\t" + park.Area + " sq km");
                    Console.WriteLine("Annual Visitors:" + "\t" + park.Visitors);
                    Console.WriteLine();
                    Console.WriteLine(park.Description);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
                Console.WriteLine();
            }
        }


        //PARK METHODS
        private void GetCVNPInfo()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.GetCVNPInfo();

            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    Console.WriteLine(park.ParkName + " National Park");
                    Console.WriteLine("Location:" + "\t" + park.Location);
                    Console.WriteLine("Established:" + "\t" + park.EstDate.ToString("MM/dd/yyyy"));
                    Console.WriteLine("Area:" + "\t" + park.Area + " sq km");
                    Console.WriteLine("Annual Visitors:" + "\t" + park.Visitors);
                    Console.WriteLine();
                    Console.WriteLine(park.Description);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
                Console.WriteLine();
            }
        }

        private void GetArchesInfo()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.GetArchesInfo();

            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    Console.WriteLine(park.ParkName + " National Park");
                    Console.WriteLine("Location:" + "\t" + park.Location);
                    Console.WriteLine("Established:" + "\t" + park.EstDate.ToString("MM/dd/yyyy"));
                    Console.WriteLine("Area:" + "\t" + park.Area + " sq km");
                    Console.WriteLine("Annual Visitors:" + "\t" + park.Visitors);
                    Console.WriteLine();
                    Console.WriteLine(park.Description);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
                Console.WriteLine();
            }
        }


        private void GetAcadiaInfo()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.GetAcadiaInfo();

            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    Console.WriteLine(park.ParkName + " National Park");
                    Console.WriteLine("Location:" + "\t" + park.Location);
                    Console.WriteLine("Established:" + "\t" + park.EstDate.ToString("MM/dd/yyyy"));
                    Console.WriteLine("Area:" + "\t" + park.Area + " sq km");
                    Console.WriteLine("Annual Visitors:" + "\t" + park.Visitors);
                    Console.WriteLine();
                    Console.WriteLine(park.Description);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
                Console.WriteLine();
            }
        }



        private void PrintHeader()
        {
            Console.WriteLine(@"National Park Campground Reservation");
            Console.WriteLine();
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
            Console.WriteLine(" 1) - View Campgrounds");
            Console.WriteLine(" 2) Search for Reservation");
            Console.WriteLine(" 3) Return to Previous Screen");
            Console.WriteLine();
        }

    }
}
