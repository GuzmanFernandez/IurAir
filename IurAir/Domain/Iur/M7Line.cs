using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class M7Line : IurLine
    {
        public string line { get; }
        public M7Line(string rawLine) : base(rawLine, "M7", 0, new List<LineSection>())
        {
            this.line = rawLine;
        }

        public override Dictionary<string, string> parseLine()
        {
            return M7S.parseLine(line);
        }

        public override IurObject getObject()
        {
            return new M7Object(this);
        }
    }

    public class M7Object : IurObject
    {
        public string RemarkItemNr { get; set; }
        public string ItineraryRemark { get; set; }
       

        public M7Object(M7Line line)
        {
            Dictionary<string, string> parsed = line.parseLine();
            RemarkItemNr = parsed["RemarkItemNr"].Trim();
            ItineraryRemark = parsed["ItineraryRemark"].Trim();
            
        }
    }
}
