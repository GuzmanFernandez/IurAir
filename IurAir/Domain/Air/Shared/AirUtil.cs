using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Shared
{
    public static class AirUtil
    {
        public static string PadString(string ToPad, char PadChar, int DesiredLength, bool ToStart)
        {
            if(ToPad.Length > DesiredLength)
            {
                return ToPad.Substring(0, DesiredLength);
            }
            string retString = ToPad;
            int charToApply = DesiredLength - ToPad.Length;
            while(charToApply > 0)
            {
                if(ToStart)
                {
                    retString = $"{PadChar}{retString}";    
                }
                else
                {
                    retString = $"{retString}{PadChar}";
                }
                charToApply--;
            }

            return retString;
        }

        public static string MonthConvert(string Month)
        {
            int month = int.Parse(Month);
            switch(month) {
                case 1:
                    return "JAN";
                case 2:
                    return "FEB";
                case 3:
                    return "MAR";
                case 4:
                    return "APR";
                case 5:
                    return "MAY";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AUG";
                case 9:
                    return "SEP";
                case 10:
                    return "OCT";
                case 11:
                    return "NOV";
                default:
                    return "DEC";
            }
        }

        public static string CalculateArrivalDate(string DepartureDate, string indicator)
        {
            DateTime dt = DateTime.Parse(DepartureDate);
            switch (indicator)
            {
                case "1":
                    dt = dt.AddDays(1);
                    break;
                case "2":
                    dt = dt.AddDays(2);
                    break;
                case "3":
                    dt = dt.AddDays(3);
                    break;
                case "4":
                    dt = dt.AddDays(-1);
                    break;
                case "5":
                    dt = dt.AddDays(-2);
                    break;
                default:
                    return DepartureDate;
            }
            return dt.ToString("ddMMM", new CultureInfo("en_US")).ToUpper();
        }
        public static string ConvertElapsed(string elapsed)
        {
            string ToConvert = elapsed.Trim();
            var splitted = ToConvert.Split('.');
            string h = splitted[0];
            string m = splitted[1];
            if (h.Length < 2)
            {
                h = "0" + h;
            }
            if(h.Length == 1)
            {
                h = "0" + h;
            }
            if (m.Length < 2)
            {
                m = "0" + m;
            }
            return $"{h}{m}";
        }
    }
}
