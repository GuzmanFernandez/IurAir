using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.NewFileModel
{
    public class FareInfo
    {
        public PriceData BaseFare { get; set; }
        public Tax Tax1 { get; set; }
        public Tax Tax2 { get; set; }
        public Tax Tax3 { get; set; }
        public PriceData TotalFare { get; set; }
        public PriceData NetAmount { get; set; }
        public TariffType TariffType { get; set; }
        public string FormOfPayment { get; set; }

        public FareInfo() { }
        public FareInfo(M2EconomicInfo info, string fop)
        {
            this.BaseFare = info.BaseFare;
            this.Tax1 = info.Tax1;
            this.Tax2 = info.Tax2; 
            this.Tax3 = info.Tax3;
            this.TotalFare = info.TotalFare;
            this.NetAmount = info.NetAmount;
            this.TariffType = info.TariffType;
            this.FormOfPayment = fop;
        }
        
    }
}
