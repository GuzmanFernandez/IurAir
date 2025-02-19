using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    public class FMLine
    {
        public string tag = "FM*C*";
        public string percentage;
        public string segments;
        public string passengers;
        public string Line { get; }

        public FMLine(string percentage, string segments, string passengers)
        {
            this.percentage = percentage;
            this.segments = segments;
            this.passengers = passengers;
            this.Line = $"{tag}{percentage};{segments};{passengers}";
        }
    }
}
