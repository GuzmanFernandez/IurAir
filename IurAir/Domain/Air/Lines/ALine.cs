using IurAir.Domain.Air.Shared;
using IurAir.Domain.Common;
using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    public class ALine
    {
        public string AirlineName { get; set; } //24 truncate
        public string AirlineCode { get; set; } 
        public string AirlineNumericCode { get; set; } //padding left 0

        public ALine(M5NormOrExchangeObject m5)
        {
            Vectors v = VectorReader.GetVectorByIATA(m5.ValidatingCarrierCode);
            AirlineName = v.Name.Length > 24 ? v.Name.Substring(0, 24).ToUpper() : v.Name.ToUpper();
            AirlineCode = v.Designator.Length < 3 ? AirUtil.PadString(v.Designator, ' ', 3, false) : v.Designator;
            AirlineNumericCode = v.normalizeCode().Code + "0";
        }

        public ALine(string airlineNumCode)
        {
            airlineNumCode = int.Parse(airlineNumCode).ToString();
            Vectors v = VectorReader.GetVectorByCode(airlineNumCode);
            AirlineName = v.Name.Length > 24 ? v.Name.Substring(0, 24).ToUpper() : v.Name.ToUpper();
            AirlineCode = v.Designator.Length < 3 ? AirUtil.PadString(v.Designator, ' ', 3, false) : v.Designator;
            AirlineNumericCode = v.normalizeCode().Code + "0";
        }

        public string FormatString208()
        {
            return $"A-{AirlineName};{AirlineCode}{AirlineNumericCode}";
        }
        public string FormatString206()
        {
            return FormatString208();
        }
    }
}
