using IurAir.Domain.Air.Shared;
using IurAir.Domain.Iur;
using IurAir.Domain.Iur.NewFileModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Mir.Lines.Header
{
    public class MirMessageHeader
    {
        public MIR_TktMessageHeaders Headers { get; } = null;

        public MirMessageHeader(MIR_TktMessageHeaders headers, DocumentType dt)
        {
            Headers = headers;
        }

        public string format()
        {
            if (Headers != null)
            {
                return Headers.Format208();
            }
            else return "";
        }
    }

    public class MIR_TktMessageHeaders
    {
        public string CarrierAlphaCode { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierName { get; set; }
        public DateTime DateOfFirstTravel { get; set; }
        public DocumentType DocumentType { get; set; }
        public string VoidDate { get; set; }
        public string BookingAgency { get; set; }
        public string IataNr { get; set; }
        public string PnrLocator { get; set; }
        public string AirLinePnrLocator { get; set; }
        public string DocumentDate { get; set; }
        public Double TotalBaseFare { get; set; }
        public string BaseFareCurrency { get; set; }
        public string TotalTaxCurrency { get; set; }
        public string CommissionAmount { get; set; }
        public string CommissionRate { get; set; }

        private string _firstLine;
        private string SecondLine;
        private string ThirdLine;
        private string FourthLine;
        private string FifthLine;
        string FirstLine { get; }

        public MIR_TktMessageHeaders(
            string carrierAlphaCode,
            string carrierName,
            string carrierCode,
            DateTime dateOfFirstTravel,
            DocumentType documentType,
            string bookingAgency,
            string iataNr,
            string pnrLocator,
            string airlinePnrLocator,
            string documentDate,
            Double totalBaseFare,
            string baseFareCurrency,
            string totalTaxCurrency,
            string commissionAmount,
            string commissionRate,
            string voidDate = "")
        {
            CarrierAlphaCode = carrierAlphaCode;
            CarrierName = carrierName;
            CarrierCode = carrierCode;
            DateOfFirstTravel = dateOfFirstTravel;
            DocumentType = documentType;
            VoidDate = voidDate;
            BookingAgency = bookingAgency;  
            IataNr = iataNr;  
            PnrLocator = pnrLocator;
            AirLinePnrLocator = airlinePnrLocator;
            DocumentDate = documentDate;
            TotalBaseFare = totalBaseFare;
            BaseFareCurrency = baseFareCurrency;
            TotalTaxCurrency = totalTaxCurrency;
            CommissionAmount = commissionAmount;
            CommissionRate = commissionRate;
            FirstLine = new FirstLineFormatter(carrierAlphaCode, carrierName, carrierCode, dateOfFirstTravel).Format();
            SecondLine = new SecondLineFormatter(bookingAgency, iataNr, pnrLocator, airlinePnrLocator, documentDate).Format();
            ThirdLine = new ThirdLineFormatter(totalBaseFare, baseFareCurrency, totalTaxCurrency, commissionAmount, commissionRate).Format();
            FourthLine = new FourthLineFormatter(DocumentType).Format();
        }

        public string Format208()
        {
            return $"{FirstLine}{SecondLine}{ThirdLine}";
        }

        public string Format206()
        {
            return Format208();
        }
    }

    internal class FirstLineFormatter
    {
        public string CarrierAlphaCode { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierName { get; set; }
        public DateTime DateOfFirstTravel { get; set; }

        public FirstLineFormatter(string carrierAlphaCode, string carrierName, string carrierCode, DateTime dateOfFirstTravel)
        {
            CarrierAlphaCode = carrierAlphaCode;
            CarrierCode = carrierCode;
            CarrierName = carrierName; 
            DateOfFirstTravel = dateOfFirstTravel;
        }

        public string Format()
        {
            return $"T51G7733920000000000{DateTime.Now.ToString("dd")}" +
                   $"{DateTime.Now.ToString("MMM").ToUpper()}{DateTime.Now.ToString("yy")}" +
                   $"{DateTime.Now.ToString("HH")}{DateTime.Now.ToString("mm")} {CarrierAlphaCode}{CarrierCode}{AirUtil.PadString(CarrierName.ToUpper(), ' ', 24, false)}" +
                   $"{DateOfFirstTravel.ToString("ddMMMyy").ToUpper()}" +
                   $"999999999999{Environment.NewLine}";

        }

    }

    internal class SecondLineFormatter
    {
        public string BookingAgency { get; set; }
        public string IataNr { get; set; }
        public string PnrLocator { get; set; }
        public string AirLinePnrLocator { get; set; }
        public string DocumentDate { get; set; }



        public SecondLineFormatter(string bookingAgency, string iataNr, string pnrLocator, string airLinePnrLocator, string documentDate)
        {
            BookingAgency = bookingAgency.Substring(5);
            IataNr = iataNr;
            PnrLocator = pnrLocator;
            AirLinePnrLocator = airLinePnrLocator;
            DocumentDate = documentDate.ToUpper();

        }

        public string Format()
        {
            return $"{BookingAgency}{BookingAgency}{AirUtil.PadString(IataNr, ' ', 9, false)}{PnrLocator}{AirLinePnrLocator}   {BookingAgency}{Properties.Settings.Default.AgentInitials}" +
                   $"N{Properties.Settings.Default.AgentInitials}AG{DocumentDate}000{DateTime.Now.ToString("ddMMMyy").ToUpper()}001{Environment.NewLine}";
        }

    }

    internal class ThirdLineFormatter
    {
        public Double TotalBaseFare { get; set; }
        public string BaseFareCurrency { get; set; }
        public string TotalTaxCurrency { get; set; }
        public string CommissionAmount { get; set; }
        public string CommissionRate { get; set; }



        public ThirdLineFormatter(Double totalBaseFare, string baseFareCurrency, string totalTaxCurrency, string commissionAmount, string commissionRate)
        {
            TotalBaseFare = totalBaseFare;
            BaseFareCurrency = baseFareCurrency;
            TotalTaxCurrency = totalTaxCurrency;
            CommissionAmount = commissionAmount;
            CommissionRate = commissionRate;    
        }

        public string Format()
        {
            bool isInt = TotalBaseFare % 1 == 0;
            int restDigits = 0;
            string TotalBaseFareStr = "";
            if (isInt)
            {
                TotalBaseFareStr = ((int)double.Parse(TotalBaseFare.ToString())).ToString();
            }
            else 
            {
                restDigits = (TotalBaseFare % 1).ToString().Length;
                TotalBaseFareStr = TotalBaseFare.ToString().Replace(",", "");
            }

            return $"{BaseFareCurrency}{AirUtil.PadString(TotalBaseFareStr.ToUpper(), '0', 12, true)}{restDigits}{TotalTaxCurrency}" +
                   $"00000000  00000000  00000000  00000000  00000000  " +
                   $"{AirUtil.PadString(CommissionAmount, '0', 8, true)}{AirUtil.PadString(CommissionRate, '0', 4, true)}" +
                   $"               {Environment.NewLine}";
        }

    }

    internal class FourthLineFormatter
    {
        public string CarrierAlphaCode { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierName { get; set; }
        public DateTime DateOfFirstTravel { get; set; }

        public FourthLineFormatter(DocumentType docType)
        {
            CarrierAlphaCode = carrierAlphaCode;
            CarrierCode = carrierCode;
            CarrierName = carrierName;
            DateOfFirstTravel = dateOfFirstTravel;
        }

        public string Format()
        {
            return $"NN " +
                   $"{DateTime.Now.ToString("MMM").ToUpper()}{DateTime.Now.ToString("yy")}" +
                   $"{DateTime.Now.ToString("HH")}{DateTime.Now.ToString("mm")} {CarrierAlphaCode}{CarrierCode}{AirUtil.PadString(CarrierName.ToUpper(), ' ', 24, false)}" +
                   $"{DateOfFirstTravel.ToString("ddMMMyy").ToUpper()}" +
                   $"999999999999{Environment.NewLine}";

        }

    }
}
