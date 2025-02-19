using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public class MES
    {
        public static Regex MERegex = new Regex(@"ME(?<AssociatedM3ItemNr>\d{2})(?<SegmentRemark>.*)");

        public static Dictionary<string, string> parseLine(string line)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Regex toExclude = new Regex(@"[0-9]");
            if (MERegex.IsMatch(line))
            {
                Match m = MERegex.Match(line);
                var groupNames = MERegex.GetGroupNames();
                foreach (string g in groupNames)
                {
                    if (!toExclude.IsMatch(g))
                    {
                        result.Add(g, m.Groups[g].Value);
                    }
                }
            }
            return result;
        }
    }
}
