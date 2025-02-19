using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.NewFileModel
{
    public class AgencyCommission
    {
        public string CommissionPercentage { get; set; }
        public PriceData Commission { get; set; }
        public PriceData TravelAgencyTax { get; set; }

        public AgencyCommission() { }
        public AgencyCommission(M2EconomicInfo info)
        {
            CommissionPercentage = info.CommissionPercentage;
            Commission = info.Commission;
            TravelAgencyTax = info.TravelAgencyTax;
        }
    }
}
