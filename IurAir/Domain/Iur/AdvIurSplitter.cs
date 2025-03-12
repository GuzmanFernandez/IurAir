using IurAir.Domain.Air;
using IurAir.Domain.Air.Lines;
using IurAir.Domain.Air.Shared;
using IurAir.Domain.Common;
using IurAir.Domain.Iur.NewFileModel;
using IurAir.Domain.Iur.Utilities;
using IurAir.Domain.Mir.Lines.Header;
using IurAir.Domain.TicketEmission;
using IurAir.Domain.Translations;
using IurAir.Models;
using IurAir.Services.SettingsService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IurAir.Domain.Iur
{
    public class AdvIurSplitter : IAirable
    {
        public IurDocument _document;
        private List<M1Object> _paxes;
        public List<TicketDataRecord> TicketedPassengers { get; } = new List<TicketDataRecord>();
        public List<UnparsablePassenger> UnparsablePassengers { get; } = new List<UnparsablePassenger> { };
        [JsonIgnore]
        public HeadersData Headers { get; set; }
        [JsonIgnore]
        public AirMessageHeader AirHeaders { get; set; }
        public MirMessageHeader MirHeaders { get; set; }
        public Dictionary<string, string> HeadersTranslations { get; set; } = new Dictionary<string, string>();
        public DocumentType DocumentType { get; set; }
        public PnrFileInfo PnrFileInfo { get; set; }
        public Dictionary<string, List<string>> RemarkTranslations { get; set; } = new Dictionary<string, List<string>>();


        public AdvIurSplitter(IurDocument document)
        {
            _document = document;
            DocumentType = _document.DocumentType;
            PnrFileInfo = _document.PnrFileInfo;
            Headers = getHeaders();
            if (Headers == null)
            {
                throw new Exception("File could not be parsed");
            }
            if (document.DocumentType != DocumentType.VoidTicket)
            {
                List<string> m7Remarks = _document.compositeLines
                    .Where(x => x.Key.StartsWith("M7"))
                    .Select(x => (x.Value as M7Object).ItineraryRemark)
                    .ToList();
                List<string> m8Remarks = _document.compositeLines
                    .Where(x => x.Key.StartsWith("M8"))
                    .Select(x => (x.Value as M8Object).PassengerRemark)
                    .ToList();
                List<string> m9Remarks = _document.compositeLines
                    .Where(x => x.Key.StartsWith("M9"))
                    .Select(x => (x.Value as M9Object).InterfaceRemark)
                    .ToList();
                foreach (string rm in m7Remarks)
                {
                    if (RemarkTranslations.ContainsKey("AIR"))
                    {
                        RemarkTranslations["AIR"].Add($"RM*{rm}");
                    }
                    else
                    {
                        RemarkTranslations.Add("AIR", new List<string>() { $"RM*{rm}" });
                    }
                }
                foreach (string rm in m8Remarks)
                {
                    if (RemarkTranslations.ContainsKey("AIR"))
                    {
                        RemarkTranslations["AIR"].Add($"RM*{rm}");
                    }
                    else
                    {
                        RemarkTranslations.Add("AIR", new List<string>() { $"RM*{rm}" });
                    }
                }
                foreach (string rm in m9Remarks)
                {
                    if (RemarkTranslations.ContainsKey("AIR"))
                    {
                        RemarkTranslations["AIR"].Add($"RM*{rm}");
                    }
                    else
                    {
                        RemarkTranslations.Add("AIR", new List<string>() { $"RM*{rm}" });
                    }
                }
                this._paxes = _document.compositeLines
                    .Where(k => k.Key.StartsWith("M1"))
                    .Select(k => k.Value as M1Object)
                    .ToList();
                foreach (M1Object p in _paxes)
                {
                    try
                    {
                        TicketDataRecord tdr = new TicketDataRecord(p, _document);
                        TicketedPassengers.Add(tdr);
                    }
                    catch (Exception ex)
                    {
                        var st = ex.StackTrace;
                        UnparsablePassengers.Add(new UnparsablePassenger(p, ex.Message));
                    }
                }
            }
            AirHeaders = getTktHeaders();
            //MirHeaders = getMIRTktHeaders();
            MirFileHeader mfh = new MirFileHeader(Headers.IataNr, 001);
            mfh.MapFrom(this);
            string MIRHeader = mfh.GenerateHeader();
            HeadersTranslations.Add("AIR", AirHeaders.format());
            //HeadersTranslations.Add("MIR", MirHeaders.format());
            getPerPaxParts(SettingService.GetPerPaxSplit());
        }

        private HeadersData getHeaders()
        {
            var m0o = _document.compositeLines.FirstOrDefault().Value;
            if (m0o == null)
            {
                return null;
            }
            else
            {
                HeadersData hd = null;
                if (m0o is M0Object)
                {
                    return new HeadersData(m0o as M0Object, null);
                }
                else if (m0o is M0VoidObject)
                {
                    return new HeadersData(null, m0o as M0VoidObject);
                }
                if (hd == null)
                {
                    return null;
                }
            }
            return null;
        }

        private MirMessageHeader getMIRTktHeaders()
        {
            CarrierData carrier = new CarrierData(Vectors.defaultVector());
            string pnrLocator = "";
            string airlinePnrLocator = "";
            string iataNr = "";
            string bookingAgency = "";
            DateTime firstDepDate = new DateTime();
            ItineraryTrait trait = null;
            string documentDate = "";
            Double totalBaseFare = 0.00;
            string baseFareCurrency = "";
            string totalTaxCurrency = "";
            string commissionAmount = "";
            string commissionRate = "";


            if (Headers.m0Data != null)
            {
                string depDate = Headers.m0Data.departureDate;
                int day, month;
                DateUtilities.DateFromDocument(depDate, out day, out month);
                trait = TicketedPassengers
                        .Select(x => x.Itinerary)
                        .SelectMany(it => it.Traits)
                        .Where(tr => tr.DepartureDate.Month == month && tr.DepartureDate.Day == day)
                        .FirstOrDefault();
                if (trait == null)
                {
                    throw new Exception("Unparsable File");
                }
                carrier = trait.Carrier;
                airlinePnrLocator = trait.AirlinePnrLocator;
                pnrLocator = Headers.m0Data.pnrLocator;
                iataNr = Headers.IataNr;
                bookingAgency = Headers.BookingAgency;
                firstDepDate = TicketedPassengers
                        .Select(x => x.Itinerary)
                        .SelectMany(it => it.Traits)
                        .Where(tr => tr.DepartureDate.Month == month && tr.DepartureDate.Day == day)
                        .FirstOrDefault().DepartureDate.Date;
                documentDate = _document.DocumentDate.toDate().ToString("ddMMMyy");

                foreach(TicketDataRecord tdr in TicketedPassengers)
                {
                    totalBaseFare += Double.Parse(tdr.AccountingInformations.ExtTicketRecord.BaseFare.StringAmount);
                    baseFareCurrency = tdr.AccountingInformations.ExtTicketRecord.BaseFare.Currency;
                    totalTaxCurrency = tdr.AccountingInformations.ExtTicketRecord.TotalTaxes.Tax.Currency;
                    commissionAmount = tdr.AccountingInformations.ExtTicketRecord.Commission.StringAmount;
                    commissionRate = tdr.AccountingInformations.ExtTicketRecord.CommissionPercent;
                }
            }
            else if (Headers.m0VoidData != null)
            {
                pnrLocator = Headers.m0VoidData.voidPnrLocator;
                airlinePnrLocator = "";
                iataNr = Headers.IataNr;
                bookingAgency = Headers.BookingAgency;
            }
            var passengers = TicketedPassengers.Count().ToString();
            MirMessageHeader header = null;
            if (DocumentType == DocumentType.VoidTicket)
            {
                string voidS = $"{Headers.m0VoidData.transmissionDay}{Headers.m0VoidData.transmissionMonth}";
                string voidDate = AirUtil.PadString(voidS, ' ', 5, false);
                //TODO: Add correct date time for header!!! mirar depDate definicion
                header = null;// new MirMessageHeader(new MIR_TktMessageHeaders(carrier.CarrierDesignator, carrier.CarrierName, carrier.CarrierCode, new DateTime(), DocumentType, bookingAgency, iataNr, pnrLocator, airlinePnrLocator, voidDate), DocumentType);
            }
            else
            {
                //TODO: Add correct date time for header!!! mirar depDate definicion
                header = new MirMessageHeader(new MIR_TktMessageHeaders(
                    carrier.CarrierDesignator, carrier.CarrierName, carrier.CarrierCode, 
                    firstDepDate, DocumentType, bookingAgency, iataNr, pnrLocator, airlinePnrLocator, 
                    documentDate, totalBaseFare, baseFareCurrency, totalTaxCurrency, commissionAmount,
                    commissionRate), DocumentType);
            }
            return header;
        }
        private AirMessageHeader getTktHeaders()
        {
            CarrierData carrier = new CarrierData(Vectors.defaultVector());
            string pnrLocator = "";
            string lnIata = "";
            string iataNr = "";
            string bookingAgency = "";

            if (Headers.m0Data != null)
            {
                string depDate = Headers.m0Data.departureDate;
                int day, month;
                DateUtilities.DateFromDocument(depDate, out day, out month);
                ItineraryTrait trait = TicketedPassengers
                        .Select(x => x.Itinerary)
                        .SelectMany(it => it.Traits)
                        .Where(tr => tr.DepartureDate.Month == month && tr.DepartureDate.Day == day)
                        .FirstOrDefault();
                if (trait == null)
                {
                    throw new Exception("Unparsable File");
                }
                carrier = trait.Carrier;
                lnIata = trait.AirlinePnrLocator;
                pnrLocator = Headers.m0Data.pnrLocator;
                iataNr = Headers.IataNr;
                bookingAgency = Headers.BookingAgency;
            }
            else if (Headers.m0VoidData != null)
            {
                pnrLocator = Headers.m0VoidData.voidPnrLocator;
                lnIata = "";
                iataNr = Headers.IataNr;
                bookingAgency = Headers.BookingAgency;
            }
            var passengers = TicketedPassengers.Count().ToString();
            AirMessageHeader header = null;
            if (DocumentType == DocumentType.VoidTicket)
            {
                string voidS = $"{Headers.m0VoidData.transmissionDay}{Headers.m0VoidData.transmissionMonth}";
                string voidDate = AirUtil.PadString(voidS, ' ', 5, false);
                header = new AirMessageHeader(new TktMessageHeaders(lnIata, passengers, pnrLocator, carrier.CarrierDesignator, bookingAgency, iataNr, DocumentType, voidDate), DocumentType);
            }
            else
            {
                header = new AirMessageHeader(new TktMessageHeaders(lnIata, passengers, pnrLocator, carrier.CarrierDesignator, bookingAgency, iataNr, DocumentType), DocumentType);
            }
            return header;
        }

        public string getCommonParts()
        {
            string head = Headers != null ? HeadersTranslations["AIR"] : "";
            return $"{head}";
        }

        public Dictionary<string, string> getPerPaxParts(bool splitPerPax = false)
        {
            Dictionary<string, List<TicketDataRecord>> split = new Dictionary<string, List<TicketDataRecord>>();

            foreach (TicketDataRecord tdr in TicketedPassengers)
            {
                var keyToAdd = "";
                if (!splitPerPax)
                {
                    keyToAdd = tdr.PassengerInformations.PassengerType;
                }
                else
                {
                    keyToAdd = tdr.PassengerInformations.PassengerPnrNumber;
                }
                if (!split.ContainsKey(keyToAdd))
                {
                    split.Add(keyToAdd, new List<TicketDataRecord>() { tdr });
                }
                else
                {
                    split[keyToAdd].Add(tdr);
                }
            }
            Dictionary<string, string> passengers = new Dictionary<string, string>();

            foreach (var kvp in split)
            {
                bool sameItinerary = true;
                string itineraryString = null;
                bool sameFareData = true;
                string fareString = null;
                string passengersString = "";
                string carrierString = null;
                bool sameCarrier = true;
                foreach (TicketDataRecord ticket in kvp.Value)
                {
                    if (itineraryString == null)
                    {
                        itineraryString = string.Join(Environment.NewLine, ticket.Itinerary.TranslatedItineraryLines["AIR"]);
                    }
                    else
                    {
                        string newItinString = string.Join(Environment.NewLine, ticket.Itinerary.TranslatedItineraryLines["AIR"]);
                        if (itineraryString != newItinString)
                        {
                            sameItinerary = false;
                        }
                    }
                    if (fareString == null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(ticket.AccountingInformations.TotalFareTranslations["AIR"]);
                        sb.AppendLine(ticket.AccountingInformations.TaxTranslations["AIR"]);
                        fareString = sb.ToString();
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(ticket.AccountingInformations.TotalFareTranslations["AIR"]);
                        sb.AppendLine(ticket.AccountingInformations.TaxTranslations["AIR"]);
                        string newFareString = sb.ToString();
                        if (fareString != newFareString)
                        {
                            sameFareData = false;
                        }
                    }
                    if (carrierString == null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(ticket.AccountingInformations.ValidatingCarrierTranslations["AIR"]);
                        carrierString = sb.ToString();
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(ticket.AccountingInformations.ValidatingCarrierTranslations["AIR"]);
                        string newCarrierString = sb.ToString();
                        if (carrierString != newCarrierString)
                        {
                            sameCarrier = false;
                        }
                    }
                    passengersString = passengersString + $"{ticket.ForPaxTranslation["AIR"]}";
                }
                DateTime dt = _document.DocumentDate.toDateTime();
                string ld = dt.ToString("yyMMdd");
                DPnrDate pnrDate = new DPnrDate()
                {
                    LocalDate = ld,
                    ChangeLocalDate = ld,
                    CreationLocalDate = ld,
                };

                passengers.Add(kvp.Key, $"{carrierString}\r\n{pnrDate.Format208()}\r\n{itineraryString}\r\n{fareString}\r\n{passengersString}");
            }
            return passengers;
        }

        public List<AirRender> getRenders(bool paxSplit = false)
        {
            var cp = getCommonParts();
            var ppp = getPerPaxParts(paxSplit);
            List<AirRender> renders = new List<AirRender>();
            string remarks = "";
            if (RemarkTranslations.Count > 0 && RemarkTranslations.ContainsKey("AIR"))
            {
                foreach (string r in RemarkTranslations["AIR"])
                {
                    remarks += $"{r}\r\n";
                }
            }

            if (ppp.Count < 1)
            {
                AirRender r = new AirRender()
                {
                    FileName = PnrFileInfo.PnrName,
                    Content = $"{cp}{remarks}ENDX",
                    Type = "RM_"
                };
                renders.Add(r);
            }

            foreach (var p in ppp)
            {

                AirRender render = new AirRender()
                {
                    FileName = PnrFileInfo.PnrName,
                    Content = $"{cp}{p.Value}{remarks}ENDX"
                };
                renders.Add(render);
            }

            foreach (var r in renders)
            {
                if (r.Content.Contains(Environment.NewLine + Environment.NewLine))
                {
                    r.Content = r.Content.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
                }
            }
            return renders;
        }

        public int getPassengerTypeCount()
        {
            int paxTypes = 0;
            string lastPaxType = null;
            foreach (TicketDataRecord tdr in TicketedPassengers)
            {
                if (lastPaxType == null)
                {
                    lastPaxType = tdr.PassengerInformations.PassengerType;
                    paxTypes += 1;
                }
                if (lastPaxType != tdr.PassengerInformations.PassengerType)
                {
                    paxTypes += 1;
                }
            }
            return paxTypes;
        }

        public DocumentParse getOriginal()
        {
            throw new NotImplementedException();
        }

        public class UnparsablePassenger
        {
            M1Object _passenger;
            string _exception;

            public UnparsablePassenger(M1Object passenger, string exception)
            {
                Passenger = passenger;
                Exception = exception;
            }

            public M1Object Passenger { get => _passenger; set => _passenger = value; }
            public string Exception { get => _exception; set => _exception = value; }
        }


    }
}
