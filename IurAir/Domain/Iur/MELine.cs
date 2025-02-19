using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class MELine : IurLine
    {
        public string line { get; }
        public MELine(string rawLine) : base(rawLine, "ME", 0, new List<LineSection>())
        {
            this.line = rawLine;
        }

        public override Dictionary<string, string> parseLine()
        {
            return M6S.parseLine(line);
        }

        public override IurObject getObject()
        {
            return new MEObject(this);
        }
    }

    public class MEObject : IurObject
    {
        public string AssociatedM3ItemNr { get; set; }
        public string SegmentRemark { get; set; }


        public MEObject(MELine line)
        {
            Dictionary<string, string> parsed = line.parseLine();
            AssociatedM3ItemNr = parsed.ContainsKey("AssociatedM3ItemNr") ? parsed["AssociatedM3ItemNr"].Trim() : "";
            SegmentRemark = parsed.ContainsKey("SegmentRemark") ? parsed["SegmentRemark"].Trim() : "";

        }
    }
}
