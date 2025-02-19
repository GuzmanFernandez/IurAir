using IurAir.Domain.Air.Shared;
using IurAir.Domain.Iur;
using IurAir.Domain.TicketEmission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    public class TaxLine
    {
        public Tax tax1 { get; set; }
        public Tax tax2 { get; set; }
        public Tax tax3 { get; set; }
        public Tax totalTax { get; set; }

        public TaxLine(M2EconomicInfo info)
        {
            tax1 = info.Tax1;
            tax2 = info.Tax2;
            tax3 = info.Tax3;
            try
            {
                totalTax = info.getTotalTax();
            }catch(Exception) { }
        }

        public TaxLine(string amount)
        {
            totalTax = new Tax() { Price = new PriceData() { Amount = amount, Currency = "EUR", Sign = "+" }, TaxCode = "" };
        }

        public static string GetFromETR(ExtendedTicketRecord record)
        {
            //KFTF; EUR3.10     JD AE;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            //var tax = $"TAX-{record.TotalTaxes.Tax.Currency}{AirUtil.PadString(record.TotalTaxes.Tax.StringAmount, ' ', 9, false)}{AirUtil.PadString("", ' ', 3, false)};;;";
            var kft = $"KFTF; {record.TotalTaxes.Tax.Currency}{AirUtil.PadString(record.TotalTaxes.Tax.StringAmount, ' ', 9, false)}XX XX; EUR0.00     XX XX; EUR0.00     XX XX;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;";
            //                                                                                                                                     KFTF; EUR3.10     JD AE; ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
            return $"{kft}";
        }

        //TAX-EUR24.00    YQ ;EUR18.00    YR ;EUR61.27    XT ;
        //TAX-EUR240.00   YR ;EUR2.32     EX ;EUR98.38    XT ;
        //TAX-EUR3.77     JD ;EUR1.02     QV ;EUR0.38     OG ;
        //TAX-EUR30.00    YQ ;EUR1.00     YR ;EUR41.96    XT ;
        //KFTY;OEUR4.00     YR   ;OEUR209.00   YR   ;OEUR5.74     JD   ;OEUR0.62     OG   ;OEUR3.38     QV   ;OEUR7.40     CJ   ;OEUR8.19     RN   ;OEUR9.90     AY   ;OEUR17.41    US   ;OEUR17.41    US   ;OEUR3.50     XA   ;OEUR6.19     XY   ;OEUR5.40     YC   ;OEUR4.68     FR   ;OEUR11.09    QX   ;OEUR3.98     XF   ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        //;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        //KFTF; EUR5.00     YR   ; EUR20.84    JD ; EUR7.55     XT ;
        public string FormatString208()
        {
            if (totalTax == null)
            {
                var tax = $"TAX-{tax1.Price.Currency}{AirUtil.PadString(tax1.Price.Amount, ' ', 9, false)}{AirUtil.PadString(tax1.TaxCode, ' ', 3, false)};{tax2.Price.Currency}{AirUtil.PadString(tax2.Price.Amount, ' ', 9, false)}{AirUtil.PadString(tax2.TaxCode, ' ', 3, false)};{tax3.Price.Currency}{AirUtil.PadString(tax3.Price.Amount, ' ', 9, false)}{AirUtil.PadString(tax3.TaxCode, ' ', 3, false)};";
                var kft = $"KFTF; {tax1.Price.Currency}{AirUtil.PadString(tax1.Price.Amount, ' ', 9, false)}{AirUtil.PadString(tax1.TaxCode, ' ', 5, false)}; {tax2.Price.Currency}{AirUtil.PadString(tax2.Price.Amount, ' ', 9, false)}{AirUtil.PadString(tax2.TaxCode, ' ', 5, false)}; {tax3.Price.Currency}{AirUtil.PadString(tax3.Price.Amount, ' ', 9, false)}{AirUtil.PadString(tax3.TaxCode, ' ', 5, false)};;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;";
                return $"{kft}\r\n{tax}";
            }
            else
            {
                //var tax = $"TAX-{totalTax.Price.Currency}{AirUtil.PadString(totalTax.Price.Amount, ' ', 9, false)}{AirUtil.PadString(totalTax.TaxCode, ' ', 3, false)};{tax2.Price.Currency}{AirUtil.PadString(tax2.Price.Amount, ' ', 9, false)}{AirUtil.PadString(tax2.TaxCode, ' ', 3, false)};{tax3.Price.Currency}{AirUtil.PadString(tax3.Price.Amount, ' ', 9, false)}{AirUtil.PadString(tax3.TaxCode, ' ', 3, false)};";
                var kft = $"KFTF; {totalTax.Price.Currency}{AirUtil.PadString(totalTax.Price.Amount, ' ', 9, false)}XX XX; EUR0.00     XX XX; EUR0.00     XX XX;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;";
                return $"{kft}";
            }
        }
        public string FormatString206()
        {
            return FormatString208();
        }
    }
}
