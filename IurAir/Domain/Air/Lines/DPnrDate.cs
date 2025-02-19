using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    public class DPnrDate
    {
        //YYMMDD
        public string LocalDate { get; set; }
        //YYMMDD
        public string ChangeLocalDate { get; set; }
        //YYMMDD
        public string CreationLocalDate { get; set; }

        public string Format208()
        {
            return $"D-{LocalDate};{ChangeLocalDate};{CreationLocalDate}";
        }

        public string Format206()
        {
            return Format208();
        }
    }
}
