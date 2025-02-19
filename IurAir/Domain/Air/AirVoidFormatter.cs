using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air
{
    public class AirVoidFormatter
    {
        private M0VoidObject voidObject;
        private string filePath;

        public AirVoidFormatter(M0VoidObject voidObject, string filePath)
        {
            this.voidObject = voidObject;
            this.filePath = filePath;
        }



    }
}
