using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public class M6S
    {
        public static Regex M6Regex = new Regex(@"M6(?<PassengerType>.{3})(?<FareCalculationType>\d{1})(?<CompressPrint>[0-9 ]{1})(?<CouponBooklet>[0-9 ]{2})(?<Data>.*)");

        public static Dictionary<string, string> parseLine(string line)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Regex toExclude = new Regex(@"[0-9]");
            if (M6Regex.IsMatch(line))
            {
                Match m = M6Regex.Match(line);
                var groupNames = M6Regex.GetGroupNames();
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
