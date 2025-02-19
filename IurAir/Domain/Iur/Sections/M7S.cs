using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public class M7S
    {
        public static Regex M7Regex = new Regex(@"M7(?<RemarkItemNr>\d{2})(?<ItineraryRemark>.*)");

        public static Dictionary<string, string> parseLine(string line)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Regex toExclude = new Regex(@"[0-9]");
            if (M7Regex.IsMatch(line))
            {
                Match m = M7Regex.Match(line);
                var groupNames = M7Regex.GetGroupNames();
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
