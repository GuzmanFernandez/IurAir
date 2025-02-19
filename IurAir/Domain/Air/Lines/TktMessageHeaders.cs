using IurAir.Domain.Air.Shared;
using IurAir.Domain.Iur;
using IurAir.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{

    public class AirMessageHeader
    {
        public TktMessageHeaders Headers { get; } = null;
        
        public AirMessageHeader(TktMessageHeaders headers, DocumentType dt)
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

    public class TktMessageHeaders
    {
        private string FirstLine;
        private string SecondLine = $"AMD 111111111;1/1;              {Environment.NewLine}";
        private string ThirdLine = $"{Properties.Settings.Default.NetworkIdentifier1}{Properties.Settings.Default.TerminalIdentifier1};{Properties.Settings.Default.NetworkIdentifier2}{Properties.Settings.Default.TerminalIdentifier2}{Environment.NewLine}";
        string MucLine { get; }

        public string AirlinePnrLoc { get; set; }
        public string TotalPassengers { get; set; }
        public string PnrLocator { get; set; }
        public string CarrierCode { get; set; }
        public string BookingAgency { get; set; }
        public string IataNr { get; set; }
        public DocumentType DocumentType { get; set; }
        public string VoidDate { get; set; }

        public TktMessageHeaders(
            string AirlinePnrLoc, 
            string totalPassengers,
            string pnrLocator, 
            string carrierCode, 
            string bookingAgency, 
            string iataNr, 
            DocumentType documentType,
            string VoidDate = "")
        {
            this.AirlinePnrLoc = AirlinePnrLoc;
            TotalPassengers = totalPassengers;
            PnrLocator = pnrLocator;
            CarrierCode = carrierCode;
            BookingAgency = bookingAgency;
            IataNr = iataNr;
            DocumentType = documentType;
            this.VoidDate = VoidDate;
            MucLine = new MucFormatter(AirlinePnrLoc, totalPassengers, pnrLocator, carrierCode, bookingAgency, iataNr).Format();
            formatFirstLine();
        }

       private string getSecondLine()
        {
            if(DocumentType == DocumentType.VoidTicket)
            {
                return $"AMD 111111111;1/1;VOID{VoidDate};{Properties.Settings.Default.AgentInitials}{Properties.Settings.Default.AgentDutyCode}{Environment.NewLine}";
            }
            else
            {
                return SecondLine;
            }
        }

        private void formatFirstLine()
        {
            string flTemplate = $"AIR-BLK206;7A;;230;0000000000;1A1137490;001001{Environment.NewLine}";
            string charL = (flTemplate.Length + getSecondLine().Length+ ThirdLine.Length+ MucLine.Length).ToString();
            FirstLine = $"AIR-BLK{Properties.Settings.Default.Version};7A;;{charL};0000000000;1A1137490;001001{Environment.NewLine}";
        }

        public string Format208()
        {
            return $"{FirstLine}{getSecondLine()}{ThirdLine}{MucLine}";
        }

        public string Format206()
        {
            return Format208();
        }
    }

    internal class MucFormatter
    {
        public string AirlinePnrLoc { get; set; }
        public string TotalPassengers { get; set; }
        public string PnrLocator { get; set; }
        public string CarrierCode { get; set; }
        public string BookingAgency { get; set; }
        public string IataNr { get; set; }

        public MucFormatter(string AirlinePnrLoc, string totalPassengers, string pnrLocator, string carrierCode, string bookingAgency, string iataNr)
        {
            this.AirlinePnrLoc = AirlinePnrLoc;
            TotalPassengers = $"{AirUtil.PadString(totalPassengers, '0', 2, true)}";
            PnrLocator = pnrLocator;
            CarrierCode = AirUtil.PadString(carrierCode, ' ', 3, false); 
            BookingAgency = AirUtil.PadString(bookingAgency, '0', 9, true);
            IataNr = iataNr;
        }

        

        public string Format()
        {
            string OfficeId = BookingAgency;
            string AgencyIATA = IataNr;
            string AgencyIdentif = $"{OfficeId};{AgencyIATA};{OfficeId};{AgencyIATA};{OfficeId};{AgencyIATA};{OfficeId};{AgencyIATA}";
            return $"MUC1A {PnrLocator}000;{TotalPassengers}{TotalPassengers};{AgencyIdentif};;;;;;;;;;;;;;;;;;;;;{CarrierCode}{AirlinePnrLoc}{Environment.NewLine}";

        }

    }

}

/*
 *  public TktMessageHeaders(int Passengers, M0Object m0, M2Object m2, string airlinePnrLoc)
        {
            MucLine = new MucFormatter(Passengers, m0, m2, airlinePnrLoc).Format();

        }

        public TktMessageHeaders(M0VoidObject m0)
        {
            MucLine = new MucFormatter(1, m0).Format();
        }

        public TktMessageHeaders(int Passengers, M0Object m0, MgObject mg, string airlinePnrLoc)
        {
            MucLine = new MucFormatter(Passengers, m0, mg, airlinePnrLoc).Format();
        }


public MucFormatter(int Passengers, M0Object m0, M2Object m2, string airlinePnrLocator)
        {
            TotalPassengers = $"{AirUtil.PadString(Passengers.ToString(), '0', 2, true)}";
            PnrLocator = m0.pnrLocator;
            CarrierCode = AirUtil.PadString(m2.validatingCarrierCode, ' ', 3, false);
            AirlinePnrLoc = airlinePnrLocator;
            BookingAgency = AirUtil.PadString(m0.pseudoCityCode, '0', 9, true);
            var IataNrArray = m0.agencyArcIataNr.Split(' ');
            string iata = "";
            foreach (var seg in IataNrArray)
            {
                iata += seg;
            }
            IataNr = iata;
        }

        public MucFormatter(int Passengers, M0VoidObject m0)
        {
            TotalPassengers = $"{AirUtil.PadString(Passengers.ToString(), '0', 2, true)}";
            CarrierCode = AirUtil.PadString("XX", ' ', 3, false);
            PnrLocator = m0.voidPnrLocator;
            BookingAgency = Properties.Settings.Default.OfficeId;
            IataNr = Properties.Settings.Default.AgencyIATA;
        }

        public MucFormatter(int Passengers, M0Object m0, MgObject mg, string airlinePnrLoc)
        {
            TotalPassengers = $"{AirUtil.PadString(Passengers.ToString(), '0', 2, true)}";
            CarrierCode = $"{AirUtil.PadString(mg.AirlineNumCode, '0', 3, true)}";
            PnrLocator = m0.pnrLocator;
            AirlinePnrLoc = airlinePnrLoc;
            BookingAgency = AirUtil.PadString(m0.pseudoCityCode, '0', 9, true);
            var IataNrArray = m0.agencyArcIataNr.Split(' ');
            string iata = "";
            foreach (var seg in IataNrArray)
            {
                iata += seg;
            }
            IataNr = iata;
        }*/