using IurAir.Domain.Air.Shared;
using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    //K-YEUR765.00     ;;;;;;;;;;;;EUR0.00       ;;;
    //K-FEUR333.00     ;;;;;;;;;;;;EUR534.43     ;;;
    //K-YEUR1413.00    ;;;;;;;;;;;;EUR1753.70    ;;;
    //K-RUSD1441.00    ;EUR           ;;;;;;;;;;;EUR59.90      ;0.949102   ;;
    public class KLine
    {
        public string CurrencyType { get; set; }
        public string BaseFare { get; set; } //11 char .00
        public string CurrencyCode { get; set; }
        public string EquivalentAmount { get; set; }
        public string CollectedCurrencyCode { get; set; }
        public string CollectedAmount { get; set; }
        public string Comment { get; set; }

        public KLine() { }

        public KLine(M2EconomicInfo economicInfo)
        {
            CurrencyType = economicInfo.BaseFare.Currency.Length == 3 ? economicInfo.BaseFare.Currency : "EUR";
            BaseFare = AirUtil.PadString(economicInfo.BaseFare.Amount,' ', 11, false);
            CollectedCurrencyCode = economicInfo.TotalFare.Currency.Length == 3 ? economicInfo.TotalFare.Currency : "EUR";
            CollectedAmount = AirUtil.PadString(economicInfo.TotalFare.Amount, ' ', 11, false);
        }

        public KLine(M5NormOrExchangeObject m5)
        {
            var baseFare = m5.BaseFare();
            var totFare = m5.TotalFare();
            CurrencyType = baseFare.Currency.Length == 3 ? baseFare.Currency : "EUR";
            BaseFare = AirUtil.PadString(baseFare.Amount, ' ', 11, false);
            CollectedCurrencyCode = totFare.Currency.Length == 3 ? totFare.Currency : "EUR";
            CollectedAmount = AirUtil.PadString(totFare.Amount, ' ', 11, false);
            this.Comment = m5.LineExplanation();
        }

        public KLine(string currencyType, string baseFare, string currencyCode, string collectedCurrencyCode, string collectedAmount, string Comment = "")
        {
            this.Comment = Comment;
            CurrencyType = currencyType.Length == 3 ? currencyType : "EUR";
            BaseFare = AirUtil.PadString(baseFare, ' ', 11, false);
            CurrencyCode = currencyCode;
            CollectedCurrencyCode = collectedCurrencyCode.Length == 3 ? collectedCurrencyCode : "EUR";
            CollectedAmount = AirUtil.PadString(collectedAmount, ' ', 11, false);
        }

        public string FormatString208(bool perPax = false)
        {
            if(Comment != null && Comment.Length > 0)
            {
                Comment = perPax ? $"##PerPax line, multiple k lines will be rendered##\n##{Comment}##\n" : Comment;
                return $"##{Comment}##\nK-Y{CurrencyType}{BaseFare};;;;;;;;;;;;{CollectedCurrencyCode}{CollectedAmount};;;";
            }
            Comment = perPax ? $"##PerPax line, multiple k lines will be rendered##\n" : "";
            return $"{Comment}K-Y{CurrencyType}{BaseFare};;;;;;;;;;;;{CollectedCurrencyCode}{CollectedAmount};;;";
            
        }
        public string FormatString206(bool perPax = false)
        {
            return FormatString208(perPax);
        }
    }
}
