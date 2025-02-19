using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    public class FpLine
    {
        public string FormOfPayment { get; set; }

        public FpLine(M2Object m2)
        {
            FormOfPayment = m2.FormOfPayment;
        }

       public FpLine(MgObject mg)
        {
            FormOfPayment = mg.FormOfPayment;
        }

        public FpLine(string formOfPayment)
        {
            FormOfPayment = formOfPayment;
        }

        public string FormatString208()
        {
            return $"FP{FormOfPayment}";
        }
        public string FormatString206()
        {
            return FormatString208();
        }
    }
}
