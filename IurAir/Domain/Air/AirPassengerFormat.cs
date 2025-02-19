using IurAir.Domain.Air.Lines;
using IurAir.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air
{
    public class AirPassengerFormat
    {
        ILine iLine = null;
        TLine tLine = null;
        string fmLine= "";
        FpLine fpLine= null;
        Vectors ticketingCarrier = Vectors.defaultVector();

        public AirPassengerFormat(ILine iLine, TLine tLine, FpLine fpLine, Vectors ticketingCarrier, string commission = "")
        {
            this.iLine = iLine;
            this.tLine = tLine;
            this.fmLine = commission;
            this.fpLine = fpLine;
            this.ticketingCarrier = ticketingCarrier;
        }

        public string Format()
        {
            string i = iLine != null ? iLine.FormatString208() + "\r\n" : "";
            string t = tLine != null ? tLine.FormatString208() + "\r\n" : "";
            string fp = fpLine != null ? fpLine.FormatString208() + "\r\n" : "FPCASH";
            string fv = ticketingCarrier.Equals(Vectors.defaultVector()) ? "" : $"FV{ticketingCarrier.Designator}\r\n";
            string fm = string.IsNullOrEmpty(fmLine) ? "" : fmLine+"\r\n";
            return $"{i}{t}{fm}{fp}{fv}";
        }


        //I Passenger name
        //SSR Special services --NO
        //T Ticket Number
        //FE Endorsement restrictions --NO
        //FM Fare Commission
        //FP Form of Payment
        //FV Ticketing Carrier
        //TKOK Ticketing arrangement --NO

        /*
            I-001;01COLLAZOS MORALES/JAIME FERNEY MR;;APROCIO//VIAJES NORESTE;;
            SSR ADTK 1A  /TO VY ON/BEFORE 12MAY 1240Z OTHERWISE WILL BE XLD
            SSR OTHS 1A  /PLS ADD PAX EMAIL SR CTCE FOR IRREG COMM
            SSR CTCR VY  HK2/NON CONSENT
            T-K030-6949967721
            FENON END/NON REF/CHGS RESTR;S3;P1-2
            FM*C*0.00;S3;P1-2
            FPCASH
            FVVY;S3;P1-2
            TKOK11MAY/AGPI12862//ETVY
        */
    }
}
