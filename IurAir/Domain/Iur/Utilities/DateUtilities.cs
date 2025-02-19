using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Utilities
{
    public static class DateUtilities
    {
        public static void DateFromDocument(string date, out int day, out int month)
        {
            DateTimeFormatInfo dtfi = new CultureInfo("en-US", false).DateTimeFormat;
            dtfi.ShortDatePattern = "ddMMM";
            DateTime parsed;
            if (DateTime.TryParseExact(date, "ddMMM", dtfi, DateTimeStyles.None, out parsed))
            {
                month = parsed.Month;
                day = parsed.Day;
            }
            else
            {
                month = 0;
                day = 0;
            }

        }

        public static void TimeFromDocument(string time, out int hours, out int minutes)
        {
            string[] formats = { "hhmmtt", "HHmm" };
            DateTime parsedTime;

            hours = -1;
            minutes = -1;

            if (DateTime.TryParseExact(time + "M", formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedTime))
            {
                hours = parsedTime.Hour;
                minutes = parsedTime.Minute;
            }
        }

    }
}
