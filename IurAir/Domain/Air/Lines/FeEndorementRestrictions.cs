using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    public class FeEndorementRestrictions
    {
        public string Endorsement { get; set; }

        public FeEndorementRestrictions(MgObject mgObj)
        {
            this.Endorsement = mgObj.EndorsementRestrictionStr;
        }

        public string FormatString208()
        {
            return $"FE{Endorsement}";
        }
        public string FormatString206()
        {
            return FormatString208();
        }
    }
}
