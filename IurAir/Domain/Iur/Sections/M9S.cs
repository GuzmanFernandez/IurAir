using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public class M9S
    {
        public static Regex M9Regex = new Regex(@"M9(?<RemarkItemNr>\d{2})(?<InterfaceRemark>.*)");

        public static Dictionary<string, string> parseLine(string line)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Regex toExclude = new Regex(@"[0-9]");
            if (M9Regex.IsMatch(line))
            {
                Match m = M9Regex.Match(line);
                var groupNames = M9Regex.GetGroupNames();
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
