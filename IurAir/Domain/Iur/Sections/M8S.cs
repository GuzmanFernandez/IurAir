using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public class M8S
    {
        public static Regex M8Regex = new Regex(@"M8(?<RemarkItemNr>\d{2})(?<PassengerRemark>.*)");

        public static Dictionary<string, string> parseLine(string line)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Regex toExclude = new Regex(@"[0-9]");
            if (M8Regex.IsMatch(line))
            {
                Match m = M8Regex.Match(line);
                var groupNames = M8Regex.GetGroupNames();
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
