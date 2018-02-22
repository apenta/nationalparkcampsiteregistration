﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Campground
    {
        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string CampName { get; set; }
        public int OpeningMonth { get; set; }
        public int ClosingMonth { get; set; }
        public decimal DailyFee { get; set; }
    }
}
