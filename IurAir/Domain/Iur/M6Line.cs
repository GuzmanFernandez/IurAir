using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class M6Line : IurLine
    {
        public string line { get; }
        public M6Line(string rawLine) : base(rawLine, "M6", 0, new List<LineSection>())
        {
            this.line = rawLine;
        }

        public override Dictionary<string, string> parseLine()
        {
            return M6S.parseLine(line);
        }

        public override IurObject getObject()
        {
            return new M6Object(this);
        }
    }

    public class M6Object : IurObject
    {
        [DefaultValue("")]
        public string PassengerType { get; set; }
        [DefaultValue("")]
        public string FareCalculationType { get; set; }
        [DefaultValue("")]
        public string CompressPrint { get; set; }
        [DefaultValue("")]
        public string CouponBooklet { get; set; }
        [DefaultValue("")]
        public string Data { get; set; }

        public M6Object(M6Line line)
        {
            Dictionary<string, string> parsed = line.parseLine();
            PassengerType = parsed.ContainsKey("PassengerType") ? parsed["PassengerType"].Trim() : "";
            FareCalculationType = parsed.ContainsKey("FareCalculationType") ? parsed["FareCalculationType"].Trim() : "";
            CompressPrint = parsed.ContainsKey("CompressPrint") ? parsed["CompressPrint"].Trim() : "";
            CouponBooklet = parsed.ContainsKey("CouponBooklet") ? parsed["CouponBooklet"].Trim() : "";
            Data = parsed.ContainsKey("Data") ? parsed["Data"].Trim() : "";
        }
    }
}
