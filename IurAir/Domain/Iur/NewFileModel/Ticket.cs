using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.NewFileModel
{
    public class Ticket
    {
        public string TicketNumber { get; set; }
        public string ItineraryType { get; set; }
        public Carrier Carrier { get; set; }
        public FareInfo Fare { get; set; }
        public AgencyCommission AgencyCommission { get; set; }
        public RawRemark[] Remarks { get; set; }
        public ResidentialFare ResidentialFare { get; set; } = null;
    }
}
