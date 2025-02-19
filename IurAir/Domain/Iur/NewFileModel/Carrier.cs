using IurAir.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.NewFileModel
{
    public class Carrier
    {
        public readonly Vectors vector;

        public Carrier(Vectors vector)
        {
            this.vector = vector;
        }
    }
}
