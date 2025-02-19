using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Shared
{
    public static class InvoiceHelper
    {
     
        public static PriceData sumPriceData(PriceData a, PriceData b)
        {
            CultureInfo culture = new CultureInfo("en-US");
            string currency = a.Currency;
            if(a.Currency != b.Currency)
            {
                if (!a.Currency.Contains(b.Currency))
                {
                    currency = $"{a.Currency}_{b.Currency}";
                }
            }
            try
            {
                float amount1 = float.Parse(a.Amount, culture);
                float amount2 = float.Parse(b.Amount, culture);
                return new PriceData()
                {
                    Sign = "+",
                    Amount = (amount1 + amount2).ToString("0.00", culture),
                    Currency = a.Currency,
                };
            }catch(Exception e)
            {
                return null;
            }
        }
        
        public static PriceData sumPriceDataList(List<PriceData> prices)
        {
            PriceData total = new PriceData() { 
                Amount = "0.00",
                Currency= prices[0].Currency,
                Sign = "+", 
            };
            foreach(PriceData priceData in prices) 
            {
                total = InvoiceHelper.sumPriceData(priceData, total);
            }
            return total;
        }
    }
}
