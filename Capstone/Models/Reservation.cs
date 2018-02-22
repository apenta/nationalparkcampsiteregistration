using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int SiteId { get; set; }
        public string ReservationName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
