using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class M8Line : IurLine
    {
        public string line { get; }
        public M8Line(string rawLine) : base(rawLine, "M8", 0, new List<LineSection>())
        {
            this.line = rawLine;
        }

        public override Dictionary<string, string> parseLine()
        {
            return M8S.parseLine(line);
        }

        public override IurObject getObject()
        {
            return new M8Object(this);
        }
    }

    public class M8Object : IurObject
    {
        public string RemarkItemNr { get; set; }
        public string PassengerRemark { get; set; }


        public M8Object(M8Line line)
        {
            Dictionary<string, string> parsed = line.parseLine();
            RemarkItemNr = parsed["RemarkItemNr"].Trim();
            PassengerRemark = parsed["PassengerRemark"].Trim();

        }
    }
}
