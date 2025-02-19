using IurAir.Domain.Air.Shared;
using IurAir.Domain.Common;
using IurAir.Domain.Iur;
using IurAir.Domain.TicketEmission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    public class TLine
    {
        public string TktType { get; set; }
        public string NumericAirlineCode { get; set; }
        public string TktNr { get; set; }

        public TLine(string tktType, string numericAirlineCode, string tktNr)
        {
            TktType = tktType;
            NumericAirlineCode = numericAirlineCode;
            TktNr = tktNr;
        }

        public TLine(TicketType tktType, CarrierData carrier, string ticketNumber)
        {
            TktType = (tktType == TicketType.Electronic || tktType == TicketType.NonArcElectronic) ? "E" : "A";
            NumericAirlineCode = carrier.CarrierCode;
            TktNr = ticketNumber;
        }

        public TLine(M2Object m2)
        {
            switch (m2.ticketIndicator)
            {
                case "2":
                    TktType = "E";
                    break;
                default:
                    TktType = "A";
                    break;
            }
            Vectors v = VectorReader.GetVectorByIATA(m2.validatingCarrierCode);
            TktNr = m2.ticketNumber;
            NumericAirlineCode = v.Code.Length < 3 ? AirUtil.PadString(v.Code, ' ', 3, false) : v.Code;
        }

        public string FormatString208()
        {
            return $"T-{TktType}{NumericAirlineCode}-{TktNr}";
        }
        public string FormatString206()
        {
            return FormatString208();
        }
    }
}
