using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class CampSite
    {
        public int SiteId { get; set; }
        public int CampgroundId { get; set; }
        public int CampsiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public bool Accessible { get; set; }
        public int MaxRvLength { get; set; }
        public bool Utilities { get; set; }
        
    }
}
