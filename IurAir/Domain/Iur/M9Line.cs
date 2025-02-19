using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class M9Line : IurLine
    {
        public string line { get; }
        public M9Line(string rawLine) : base(rawLine, "M9", 0, new List<LineSection>())
        {
            this.line = rawLine;
        }

        public override Dictionary<string, string> parseLine()
        {
            return M9S.parseLine(line);
        }

        public override IurObject getObject()
        {
            return new M9Object(this);
        }
    }

    public class M9Object : IurObject
    {
        public string RemarkItemNr { get; set; }
        public string InterfaceRemark { get; set; }


        public M9Object(M9Line line)
        {
            Dictionary<string, string> parsed = line.parseLine();
            RemarkItemNr = parsed["RemarkItemNr"].Trim();
            InterfaceRemark = parsed["InterfaceRemark"].Trim();

        }
    }
}
