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
    public class ParkSqlTests
    {
        const string connectionString = @"Server=.\SQLEXPRESS;Database=CampgroundDatabase;";

        [TestMethod]
        public void ViewAllParks_Successful()
        {
            ParkSqlDAL testClass = new ParkSqlDAL(connectionString);
            Park newPark = new Park();
            newPark.ParkName = "Acadia";

            testClass.GetParkInfo(newPark.ParkName);


        }
    }
}
