using IurAir.Domain.Air;
using IurAir.Domain.Air.Lines;
using IurAir.Domain.Air.Shared;
using IurAir.Domain.Common;
using IurAir.Domain.Iur.Utilities;
using IurAir.Domain.TicketEmission;
using IurAir.Domain.Translations;
using IurAir.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IurAir.Domain.Iur
{
    public class IurSplitter : IAirable
    {
        IurDocument _document;
        public DocumentParse Parse { get; set; }

        public IurSplitter(IurDocument document)
        {
            _document = document;
            _document.DocumentType = _document.CheckType();
            Parse = ParseFile();
        }

        private DocumentParse ParseFile()
        {
            var m0o = _document.compositeLines.FirstOrDefault().Value;
            if (m0o == null)
            {
                throw new Exception("File could not be parsed");
            }
            else
            {
                AdvIurSplitter advSplitter = null;
                try
                {
                    advSplitter = new AdvIurSplitter(_document);
                }
                catch (Exception ex)
                {
                    string err = ex.ToString();
                }
                HeadersData hd = null;
                if (m0o is M0Object)
                {
                    hd = new HeadersData(m0o as M0Object, null);
                }
                else if (m0o is M0VoidObject)
                {
                    hd = new HeadersData(null, m0o as M0VoidObject);
                }
                if (hd == null)
                {
                    throw new Exception("File could not be parsed");
                }
                else
                {
                    return new DocumentParse(
                        GenerateItinerary(),
                        _document.PnrFileInfo,
                        _document.DocumentDate.toDateTime(),
                        _document.CheckType(),
                        hd,
                        _document.Remarks,
                        getPassengerChunks(),
                        advSplitter);
                }
            }
        }


        private List<PassengerChunk> getPassengerChunks()
        {
            List<PassengerChunk> passengers = new List<PassengerChunk>();
            List<M1Object> pax = _document.compositeLines
                .Where(k => k.Key.StartsWith("M1"))
                .Select(k => k.Value as M1Object)
                .ToList();
            if (pax.Count > 0)
            {
                foreach (M1Object o in pax)
                {

                    InformationList il = retrieveInfoFromInterfaceNr(o);
                    if (il != null)
                    {
                        var it = GenerateItinerary();
                        il.totSeg = it != null ? it.Count : 1;
                        il.totPax = pax.Count;
                        passengers.Add(new PassengerChunk(o, il, il.PassengerEntitlementData != null ? il.PassengerEntitlementData.passengerTypeCode : "Unavailable"));
                    }
                }
                return passengers;
            }
            return null;
        }

        public Dictionary<string, List<PassengerChunk>> splitByType()
        {
            Dictionary<string, List<PassengerChunk>> retVal = new Dictionary<string, List<PassengerChunk>>();
            foreach (PassengerChunk pc in Parse.Passengers)
            {
                if (!retVal.ContainsKey(pc.PassengerType))
                {
                    retVal.Add(pc.PassengerType, new List<PassengerChunk> { pc });
                }
                else
                {
                    retVal[pc.PassengerType].Add(pc);
                }
            }
            return retVal;
        }

        private List<M3Object> GenerateItinerary()
        {
            List<M3Object> itinerary = new List<M3Object>();

            var voyage = _document.compositeLines
                .Where(v => v.Key.StartsWith("M3"))
                .Select(x => x.Value as M3Object)
                .ToList();
            if (voyage.Count > 0)
            {
                itinerary.AddRange(voyage);
            }

            return itinerary.Count > 0 ? itinerary : null;
        }


        private InformationList retrieveInfoFromInterfaceNr(M1Object m1)
        {
            Dictionary<M1Object, InformationList> retDict = new Dictionary<M1Object, InformationList>();
            List<ParsingError> errors = new List<ParsingError>();
            string interfaceNr = m1.interfaceNameItemNumber;
            string paxTypeCode = "";
            InformationList il = new InformationList();
            M2Object m2 = null;
            try
            {
                m2 = _document.compositeLines
                  .Where(k => k.Key.StartsWith($"M2{m1.interfaceNameItemNumber}"))
                  .Select(k => k.Value as M2Object)
                  .FirstOrDefault();
            }
            catch (Exception ex)
            {
                errors.Add(new ParsingError() { Exception = ex });
            }
            if (m2 != null)
            {
                il.PassengerTicketData = m2;
                paxTypeCode = m2.passengerType;
                if (paxTypeCode.Length > 0)
                {
                    il.PassengerTypeCode = paxTypeCode;
                }
                try
                {
                    M4Object m4 = _document.compositeLines
                        .Where(k => k.Key.Contains($"{m2.passengerType}"))
                        .Select(k => k.Value as M4Object)
                        .FirstOrDefault();
                    if (m4 != null)
                    {
                        il.PassengerEntitlementData = m4;
                    }
                }
                catch (Exception e)
                {
                    errors.Add(new ParsingError());
                }
            }
            DocumentType dt = _document.DocumentType;
            List<IurObject> m5ls = _document.compositeLines
                .Where(k => k.Key.StartsWith("M5"))
                .Select(k => k.Value)
                .ToList();
            List<InvoicingData> invoices = new List<InvoicingData>();
            foreach (IurObject m in m5ls)
            {
                InvoicingData id = new InvoicingData(dt, m);
                if (id != null && id.AccountingType == AccountingType.Normal)
                {
                    if (id.InterfaceItemNumber == interfaceNr)
                    {
                        invoices.Add(id);
                    }
                }
            }
            if (invoices.Count > 0)
            {
                il.InvoicingData = invoices;
            }
            Dictionary<string, List<InvoicingData>> perTicket = new Dictionary<string, List<InvoicingData>>();
            bool isExchange = false;
            List<string> toRemove = new List<string>();
            foreach (InvoicingData invoice in invoices)
            {
                if (invoice.OperationType == M5OperationType.EvenExchange || invoice.OperationType == M5OperationType.ExchangeWithCollection)
                {
                    isExchange = true;
                }
            }
            if (isExchange)
            {
                foreach (InvoicingData id in invoices)
                {
                    if (id.OperationType != M5OperationType.NormalTicket)
                    {
                        if (!perTicket.ContainsKey(id.AccountingType.ToString()))
                        {
                            perTicket.Add(id.TicketNumber, new List<InvoicingData>() { id });
                        }
                    }
                    else
                    {
                        toRemove.Add(id.TicketNumber);
                    }
                }
            }
            if (toRemove.Count > 0)
            {
                foreach (string id in toRemove)
                {
                    invoices.RemoveAll(n => n.TicketNumber == id);
                }
            }
            else
            {
                foreach (InvoicingData id in invoices)
                {
                    if (!perTicket.ContainsKey(id.TicketNumber))
                    {
                        perTicket.Add(id.TicketNumber, new List<InvoicingData> { id });
                    }
                    else
                    {
                        perTicket[id.TicketNumber].Add(id);
                    }
                }

            }
            if (perTicket.Count > 0)
            {
                il.PerTicketInvoices = perTicket;
                List<FareList> list = new List<FareList>();
                foreach (var tkt in perTicket)
                {
                    list.Add(new FareList(tkt.Value, paxTypeCode, tkt.Key));
                }
                if (list.Count > 0)
                {
                    il.FareList = list;
                }
            }
            if (errors.Count > 0)
            {
                il.errors.AddRange(errors);
            }
            return il;
        }


        private List<HLine> getHLines()
        {
            List<HLine> hlines = new List<HLine>();
            if (Parse.Itinerary != null && Parse.Itinerary.Count > 0)
            {
                Parse.Itinerary.ForEach((m3) => hlines.Add(new HLine(m3)));
            }
            return hlines;
        }

        private ALine getALine()
        {
            foreach (PassengerChunk chunk in Parse.Passengers)
            {
                if (chunk.Data != null && chunk.Data.InvoicingData != null && chunk.Data.InvoicingData.Count > 0)
                {

                    var carrier = chunk.Data.InvoicingData[0].ValidatingCarrier;
                    if (carrier != null && carrier != Vectors.defaultVector())
                    {
                        return new ALine(carrier.Code);
                    }

                }
                else if (chunk.Data.PassengerTicketData != null)
                {
                    var carrier = VectorReader.GetVectorByIATA(chunk.Data.PassengerTicketData.validatingCarrierCode);
                    if (carrier != null && carrier != Vectors.defaultVector())
                    {
                        return new ALine(carrier.Code);
                    }
                }
            }
            return null;
        }

        private Dictionary<string, KLine> getKLines()
        {
            var kLines = new Dictionary<string, KLine>();
            var chunked = splitByType();
            foreach (var line in chunked)
            {
                if (!kLines.ContainsKey(line.Key))
                {
                    if (line.Value != null && line.Value.Count > 0)
                    {
                        var data = line.Value[0];
                        if (data.Data.FareList != null)
                        {
                            var fare = data.Data.FareList[0];
                            var inv = fare.InvoiceSum;
                            if (inv != null)
                            {
                                var kline = new KLine(inv.BaseFare.Currency, inv.BaseFare.Amount, inv.BaseFare.Currency, inv.TotalFare.Currency, inv.TotalFare.Amount);
                                if (kline != null)
                                {
                                    kLines.Add(line.Key, kline);
                                }
                            }


                        }
                    }
                }
            }

            return kLines.Count > 0 ? kLines : null;
        }

        private Dictionary<string, TaxLine> getTaxLines()
        {
            var taxlines = new Dictionary<string, TaxLine>();
            var chunked = splitByType();
            foreach (var line in chunked)
            {
                if (!taxlines.ContainsKey(line.Key)) { }
                {
                    if (line.Value[0].Data.PassengerTicketData != null)
                    {
                        var info = line.Value[0].Data.PassengerTicketData.GetEconomicInfo();
                        if (info != null)
                        {
                            taxlines.Add(line.Key, new TaxLine(info));
                        }
                    }else if (line.Value[0].Data.InvoicingData[0].Taxes[0] != null)
                    {
                        taxlines.Add(line.Key, new TaxLine(line.Value[0].Data.InvoicingData[0].Taxes[0].Amount));
                    }
                }
            }
            return taxlines.Count > 0 ? taxlines : null;
        }

        private List<AirPassengerFormat> getPassengerLines(string type)
        {
            List<AirPassengerFormat> airPassengers = new List<AirPassengerFormat>();
            foreach (var paxType in splitByType())
            {
                if (paxType.Key == type)
                {
                    foreach (var item in paxType.Value)
                    {
                        airPassengers.Add(item.Data.getAirLines(item.Passenger));
                    }
                }
            }
            return airPassengers;
        }

        public AirMessageHeader getTktHeaders()
        {
            Vectors carrier = Vectors.defaultVector();
            string pnrLocator = "";
            string lnIata = "";
            string iataNr = "";
            string bookingAgency = "";
            if (Parse.HeadersData.m0Data != null)
            {
                var ALPnr = "";
                if (Parse.Itinerary != null && Parse.Itinerary.Count > 0)
                {
                    var alpnr = Parse.Itinerary[0].airlinePnrLocator;
                    if (!string.IsNullOrEmpty(alpnr))
                    {
                        ALPnr = alpnr;
                    }
                }
                pnrLocator = Parse.HeadersData.m0Data.pnrLocator;
                lnIata = ALPnr;
                iataNr = Parse.HeadersData.IataNr;
                bookingAgency = Parse.HeadersData.BookingAgency;
            }
            else if (Parse.HeadersData.m0VoidData != null)
            {
                pnrLocator = Parse.HeadersData.m0VoidData.voidPnrLocator;
                lnIata = "";
                iataNr = Parse.HeadersData.IataNr;
                bookingAgency = Parse.HeadersData.BookingAgency;
            }
            var splitted = splitByType();
            AirMessageHeader header = null;

            if (Parse.Itinerary != null && Parse.Itinerary.Count > 0)
            {
                carrier = VectorReader.GetVectorByIATA(Parse.Itinerary[0].carrierCode);
            }
            var passengers = Parse.Passengers.Count.ToString();
            if (Parse.DocumentType == DocumentType.VoidTicket)
            {
                string voidS = $"{Parse.HeadersData.m0VoidData.transmissionDay}{Parse.HeadersData.m0VoidData.transmissionMonth}";
                string voidDate = AirUtil.PadString(voidS, ' ', 5, false);
                header = new AirMessageHeader(new TktMessageHeaders(lnIata, passengers, pnrLocator, carrier.Designator, bookingAgency, iataNr, Parse.DocumentType, voidDate), Parse.DocumentType);
            }
            else
            {
                header = new AirMessageHeader(new TktMessageHeaders(lnIata, passengers, pnrLocator, carrier.Designator, bookingAgency, iataNr, Parse.DocumentType), Parse.DocumentType);
            }
            return header;
        }

        public string getCommonParts()
        {
            var headers = getTktHeaders();
            var voyage = getHLines();
            var carrierLine = getALine();
            string ld = Parse.DocumentDate.ToString("yyMMdd");

            DPnrDate pnrDate = new DPnrDate()
            {
                LocalDate = ld,
                ChangeLocalDate = ld,
                CreationLocalDate = ld,
            };
            string head = headers != null ? headers.format() : "";
            string trip = "";
            string carrier = carrierLine != null ? carrierLine.FormatString208() : "";
            if (voyage != null && voyage.Count > 0)
            {
                foreach (var segment in voyage)
                {
                    trip += segment.FormatString208() + Environment.NewLine;
                }
            }
            return $"{head}{pnrDate.Format208()}{Environment.NewLine}{carrier}{Environment.NewLine}{trip}";
        }

        public Dictionary<string, string> getPerPaxParts(bool paxSplit = false)
        {
            Dictionary<string, string> passengers = new Dictionary<string, string>();
            var split = splitByType();
            var kLines = getKLines();
            var taxes = getTaxLines();
            Dictionary<string, List<AirPassengerFormat>> paxRenders = new Dictionary<string, List<AirPassengerFormat>>();
            if (split != null)
            {
                foreach (var type in split)
                {
                    var paxList = getPassengerLines(type.Key);
                    if (paxList != null)
                    {
                        paxRenders.Add(type.Key, paxList);
                    }
                }
            }
            if (kLines != null)
            {
                foreach (var Kline in kLines)
                {
                    string k = Kline.Value.FormatString208();
                    passengers.Add(Kline.Key, $"{k}{Environment.NewLine}");
                }
            }
            if (taxes != null)
            {
                foreach (var Tline in taxes)
                {
                    if (passengers.ContainsKey(Tline.Key))
                    {
                        passengers[Tline.Key] += Tline.Value.FormatString208() + Environment.NewLine;
                    }
                }
            }
            if (paxRenders != null)
            {
                foreach (var pax in paxRenders)
                {
                    if (passengers.ContainsKey(pax.Key))
                    {
                        foreach (var p in pax.Value)
                        {
                            passengers[pax.Key] += p.Format();
                        }
                    }
                    else
                    {
                        passengers.Add("NDA", "");
                        foreach (var p in pax.Value)
                        {
                            passengers["NDA"] += p.Format();
                        }
                    }
                }
            }
            return passengers;
        }



        public List<AirRender> getRenders(bool paxSplit = false)
        {
            var cp = getCommonParts();
            var ppp = getPerPaxParts();
            List<AirRender> renders = new List<AirRender>();
            string remarks = "";

            if (Parse.RemarkList != null && Parse.RemarkList.Count > 0)
            {
                foreach (Remark r in Parse.RemarkList)
                {
                    if (!r.RemarkContent.Contains("SSR"))
                    {
                        remarks += $"RM*{r.RemarkContent}{Environment.NewLine}";
                    }
                }
            }
            if (ppp.Count < 1)
            {
                AirRender r = new AirRender()
                {
                    FileName = Parse.PnrFileInfo.PnrName,
                    Content = $"{cp}{remarks}ENDX",
                    Type = "RM_"
                };
                renders.Add(r);
            }
            foreach (var p in ppp)
            {

                AirRender render = new AirRender()
                {
                    FileName = Parse.PnrFileInfo.PnrName,
                    Content = $"{cp}{p.Value}{remarks}ENDX"
                };
                renders.Add(render);
            }
            return renders;
        }

        public int getPassengerTypeCount()
        {
            return this.splitByType().Count();
        }

        public DocumentParse getOriginal()
        {
            return Parse;
        }
    }

    public class DocumentParse
    {
        public List<M3Object> Itinerary { get; } = null;
        public PnrFileInfo PnrFileInfo { get; } = null;
        public DateTime DocumentDate { get; } = DateTime.Now;
        public DocumentType DocumentType { get; } = DocumentType.Undefined;
        public HeadersData HeadersData { get; } = null;
        public List<Remark> RemarkList { get; } = null;
        public List<PassengerChunk> Passengers { get; } = null;
        public bool HasError { get; set; } = false;
        public List<ParsingError> ErrorBag { get; } = new List<ParsingError>();
        public AdvIurSplitter AdvancedSplitter { get; set; } = null;

        public DocumentParse(
            List<M3Object> itinerary,
            PnrFileInfo pnrInfo,
            DateTime documentDate,
            DocumentType documentType,
            HeadersData headersData,
            List<Remark> remarkList,
            List<PassengerChunk> paxes,
            AdvIurSplitter ais = null)
        {
            Itinerary = itinerary;
            PnrFileInfo = pnrInfo;
            DocumentDate = documentDate;
            DocumentType = documentType;
            HeadersData = headersData;
            RemarkList = remarkList;
            Passengers = paxes ?? new List<PassengerChunk>();
            if (ais != null)
            {
                AdvancedSplitter = ais;
            }
            foreach (var item in Passengers)
            {
                InformationList il = item.Data;
                if (il.errors.Count > 0)
                {
                    HasError = true;
                    ErrorBag.AddRange(il.errors);
                }
            }
        }
    }

    public class VoidCombinedParse
    {
        DocumentParse voidParse;
        DocumentParse emission;
        public DocumentParse resultingParse { get; }

        public VoidCombinedParse(DocumentParse voidParse, DocumentParse emission)
        {
            this.voidParse = voidParse;
            this.emission = emission;
            string tkt = null;
            var paxChunk = new List<PassengerChunk>();
            if (voidParse.HeadersData.m0VoidData != null)
            {
                tkt = voidParse.HeadersData.m0VoidData.ticketNumber.Substring(3);
                voidParse.HeadersData.IataNr = emission.HeadersData.IataNr;
                voidParse.HeadersData.BookingAgency = emission.HeadersData.BookingAgency;
            }
            if (emission.Passengers != null && tkt != null)
            {
                foreach (var pax in emission.Passengers)
                {
                    if (pax.Data != null && pax.Data.PassengerTicketData != null)
                    {
                        if (pax.Data.PassengerTicketData.ticketNumber == tkt)
                        {
                            paxChunk.Add(pax);
                        }
                    }
                }
            }


            this.resultingParse = new DocumentParse(emission.Itinerary, voidParse.PnrFileInfo, voidParse.DocumentDate, DocumentType.VoidTicket, voidParse.HeadersData, emission.RemarkList, paxChunk);
        }
    }


    public class InformationList
    {
        public string PassengerTypeCode { get; set; } = null;
        public M2Object PassengerTicketData { get; set; } = null;
        public M4Object PassengerEntitlementData { get; set; } = null;
        public List<InvoicingData> InvoicingData { get; set; } = null;
        public Dictionary<string, List<InvoicingData>> PerTicketInvoices { get; set; } = null;
        public List<FareList> FareList { get; set; } = null;
        public List<string> TripSegments { get; } = new List<string>();
        public List<string> PersonalRemarks { get; } = new List<string>();
        public List<ParsingError> errors { get; } = new List<ParsingError>();
        public int totPax { get; set; } = 1;
        public int totSeg { get; set; } = 1;

        private ILine getILine(M1Object anagraph)
        {
            string passengerType = PassengerTypeCode != null ? PassengerTypeCode : "";
            return new ILine(anagraph.interfaceNameItemNumber, anagraph.interfaceNameItemNumber, anagraph.passengerName, passengerType);
        }

        private TLine getTLine()
        {
            if (PassengerTicketData != null)
            {
                return new TLine(PassengerTicketData);
            }
            else if (InvoicingData != null && InvoicingData.Count > 0)
            {
                foreach (InvoicingData data in InvoicingData)
                {
                    if (!string.IsNullOrEmpty(data.TicketNumber))
                    {
                        return new TLine("E", data.ValidatingCarrier.Code, data.TicketNumber);
                    }
                }
            }
            return null;
        }

        private FpLine GetFpLine()
        {
            if (InvoicingData != null && InvoicingData.Count > 0)
            {
                string fop = InvoicingData[0].FormOfPayment;
                if (fop.StartsWith("CA"))
                {
                    fop = "CASH";
                }
                else if (fop.StartsWith("CC"))
                {
                    fop = "CC";
                }
                else
                {
                    fop = fop.Substring(0, 2);
                }
                return new FpLine(fop);
            }
            return null;
        }

        private string GetCommission(int totPax = 1, int totSeg = 1)
        {
            if (InvoicingData != null && InvoicingData.Count > 0)
            {
                var percentage = InvoicingData[0].CommissionPercentage;
                var segments = totSeg == 1 ? $"S{totSeg}" : $"S1-{totSeg}";
                var pax = totPax == 1 ? $"P{totPax}" : $"P1-{totPax}";
                var fm = new FMLine(percentage, segments, pax);
                return fm.Line;
            }
            return "";
        }

        private Vectors getVector()
        {
            if (InvoicingData != null && InvoicingData.Count > 0)
            {
                return InvoicingData[0].ValidatingCarrier;
            }
            else if (PassengerTicketData != null)
            {
                return VectorReader.GetVectorByIATA(PassengerTicketData.validatingCarrierCode);
            }
            return Vectors.defaultVector();
        }

        public AirPassengerFormat getAirLines(M1Object m1)
        {
            return new AirPassengerFormat(getILine(m1), getTLine(), GetFpLine(), getVector(), GetCommission(totPax, totSeg));
        }
    }


    public class FareList
    {
        public string PassengerType { get; }
        public string TicketNumber { get; }
        public List<InvoicingData> Invoices { get; set; } = null;
        public Fares InvoiceSum { get; } = null;

        public FareList(List<InvoicingData> invoices, string passengerType, string ticketNumber)
        {
            Invoices = invoices;
            List<PriceData> totalBase = new List<PriceData>();
            List<PriceData> totalTaxes = new List<PriceData>();
            foreach (InvoicingData id in invoices)
            {
                if (id.OperationType == M5OperationType.NormalTicket ||
                    id.OperationType == M5OperationType.SecondFOP ||
                    id.OperationType == M5OperationType.EvenExchange ||
                    id.OperationType == M5OperationType.ExchangeWithCollection)
                {
                    totalBase.Add(id.BaseFare);
                    totalTaxes.Add(InvoiceHelper.sumPriceDataList(id.Taxes));
                }
            }
            PassengerType = passengerType;
            TicketNumber = ticketNumber;
            PriceData Base = InvoiceHelper.sumPriceDataList(totalBase);
            PriceData Taxes = InvoiceHelper.sumPriceDataList(totalTaxes);
            InvoiceSum = new Fares()
            {
                BaseFare = Base,
                TotalTaxes = Taxes,
                TotalFare = InvoiceHelper.sumPriceData(Base, Taxes),
                PassengerType = PassengerType,
                TicketNr = TicketNumber
            };

        }

    }

    public class PassengerChunk
    {
        public M1Object Passenger { get; }
        public InformationList Data { get; }
        public string PassengerType { get; }

        public PassengerChunk(M1Object passenger, InformationList data, string passengerType)
        {
            Passenger = passenger;
            Data = data;
            PassengerType = passengerType;
        }

        public Passenger toPassenger()
        {
            string ticket = "";
            if (Data != null)
            {
                if (Data.PassengerTicketData != null)
                {
                    ticket = Data.PassengerTicketData.ticketNumber;
                }
            }
            return new Passenger()
            {
                PassengerName = Passenger.passengerName.Replace("/", " "),
                PassengerCategory = PassengerType,
                InterfaceNr = Passenger.interfaceNameItemNumber,
                TicketNumber = ticket
            };
        }
    }

    public class ParsingError
    {
        public Exception Exception { get; set; }
    }

    public class InvoicingData
    {
        public InvoicingData(DocumentType dt, IurObject m5O)
        {
            this.InvoiceType = dt;
            if (m5O is M5NormOrExchangeObject)
            {
                M5NormOrExchangeObject o = m5O as M5NormOrExchangeObject;
                this.Explanation = o.LineExplanation();
                AccountingNr = o.AccountingNr;
                InterfaceItemNumber = !string.IsNullOrEmpty(o.InterfaceItemNr) ? o.InterfaceItemNr : null;
                AccountingType = AccountingNr == "AA" ? AccountingType.All : AccountingNr == "PP" ? AccountingType.PerPerson : AccountingType.Normal;
                switch (o.RoutingIndicator)
                {
                    case "D":
                        this.RoutingIndicator = ItineraryType.Domestic;
                        break;
                    case "F":
                        this.RoutingIndicator = ItineraryType.International;
                        break;
                    case "T":
                        this.RoutingIndicator = ItineraryType.Transborder;
                        break;
                }
                this.ValidatingCarrier = VectorReader.GetVectorByIATA(o.ValidatingCarrierCode);
                this.TicketNumber = o.TicketNumber;
                this.CommissionPercentage = o.CommissionPercentage;
                this.BaseFare = new PriceData()
                {
                    Sign = "+",
                    Amount = o.FareAmount,
                    Currency = string.IsNullOrEmpty(o.FareCurrencyCode) ? "" : o.FareCurrencyCode,
                };
                List<PriceData> taxes = new List<PriceData>();
                PriceData tax1 = new PriceData()
                {
                    Sign = "+",
                    Amount = o.Tax1,
                    Currency = string.IsNullOrEmpty(o.FareCurrencyCode) ? "" : o.FareCurrencyCode,
                };
                PriceData tax2 = new PriceData()
                {
                    Sign = "+",
                    Amount = o.Tax2,
                    Currency = string.IsNullOrEmpty(o.FareCurrencyCode) ? "" : o.FareCurrencyCode,
                };
                PriceData tax3 = new PriceData()
                {
                    Sign = "+",
                    Amount = o.Tax3,
                    Currency = string.IsNullOrEmpty(o.FareCurrencyCode) ? "" : o.FareCurrencyCode,
                };
                if (!string.IsNullOrEmpty(tax1.Amount)) { taxes.Add(tax1); }
                if (!string.IsNullOrEmpty(tax2.Amount)) { taxes.Add(tax2); }
                if (!string.IsNullOrEmpty(tax3.Amount)) { taxes.Add(tax3); }
                if (taxes.Count > 0)
                {
                    this.Taxes = taxes;
                }

                this.PriceIndication = o.getInvoiceIndicator();
                this.OperationType = o.getOperationType();
                this.AccountingType = o.GetAccountingType();

                this.FormOfPayment = o.FormOfPayment.StartsWith("CC") ? "CC" : "CA";
                this.ConjunctionDocsNr = o.ConjunctionDocsNr;
                this.ElectronicTicketIndicator = !string.IsNullOrEmpty(o.ElectronicTicketIndicator);
                string exchangeString = o.ExchValidatingCarrier_Ticket;
                if (!string.IsNullOrEmpty(exchangeString))
                {
                    if (exchangeString.EndsWith("/"))
                    {
                        var intermediateData = exchangeString.Split(new char[] { '/' })[0];
                        var vectorString = intermediateData.Substring(0, 3);
                        this.ExchValidatingCarrier = VectorReader.GetVectorByCode(vectorString);
                        this.ExchTicket = intermediateData.Substring(3);
                    }
                    else
                    {
                        var data = exchangeString.Split(new char[] { '/' });
                        if (data.Length > 1)
                        {
                            this.ExchValidatingCarrier = VectorReader.GetVectorByCode(data[0]);
                            this.ExchTicket = data[1];
                        }
                        else
                        {
                            this.ExchTicket = exchangeString;
                        }
                    }

                }
                if (!string.IsNullOrEmpty(o.ExchangedCoupons))
                {
                    this.ExchangedCoupons = o.ExchangedCoupons;
                }

            }
            else if (m5O is M5RefundObject)
            {
                this.Explanation = "This is a REFUND";
                M5RefundObject o = m5O as M5RefundObject;
                switch (o.RoutingIndicator)
                {
                    case "D":
                        this.RoutingIndicator = ItineraryType.Domestic;
                        break;
                    case "F":
                        this.RoutingIndicator = ItineraryType.International;
                        break;
                    case "T":
                        this.RoutingIndicator = ItineraryType.Transborder;
                        break;
                }
                this.ValidatingCarrier = VectorReader.GetVectorByIATA(o.ValidatingCarrierCode);
                this.TicketNumber = o.TicketNumber;
                this.CommissionPercentage = o.CommissionPercentage;
                this.BaseFare = new PriceData()
                {
                    Sign = "+",
                    Amount = o.FareAmount.StartsWith(".") ? $"0{o.FareAmount}" : o.FareAmount,
                    Currency = string.IsNullOrEmpty(o.FareCurrencyCode) ? "" : o.FareCurrencyCode,
                };
                List<PriceData> taxes = new List<PriceData>();
                PriceData tax1 = new PriceData()
                {
                    Sign = "+",
                    Amount = o.Tax1.StartsWith(".") ? $"0{o.Tax1}" : o.Tax1,
                    Currency = string.IsNullOrEmpty(o.FareCurrencyCode) ? "" : o.FareCurrencyCode,
                };
                PriceData tax2 = new PriceData()
                {
                    Sign = "+",
                    Amount = o.Tax2.StartsWith(".") ? $"0{o.Tax2}" : o.Tax2,
                    Currency = string.IsNullOrEmpty(o.FareCurrencyCode) ? "" : o.FareCurrencyCode,
                };
                PriceData tax3 = new PriceData()
                {
                    Sign = "+",
                    Amount = o.Tax3.StartsWith(".") ? $"0{o.Tax3}" : o.Tax3,
                    Currency = string.IsNullOrEmpty(o.FareCurrencyCode) ? "" : o.FareCurrencyCode,
                };
                if (!string.IsNullOrEmpty(tax1.Amount)) { taxes.Add(tax1); }
                if (!string.IsNullOrEmpty(tax2.Amount)) { taxes.Add(tax2); }
                if (!string.IsNullOrEmpty(tax3.Amount)) { taxes.Add(tax3); }
                if (taxes.Count > 0)
                {
                    this.Taxes = taxes;
                }
                switch (o.PriceIndication)
                {
                    case "ONE":
                        this.PriceIndication = InvoiceIndicator.ONE;
                        break;
                    case "PER":
                        this.PriceIndication = InvoiceIndicator.PER;
                        break;
                    case "ALL":
                        this.PriceIndication = InvoiceIndicator.ALL;
                        break;
                }
                this.FormOfPayment = o.FormOfPayment;
                this.ConjunctionDocsNr = o.ConjunctionDocsNr;
                this.FormerInvoiceNr = o.FormerInvoiceNr;
                this.RefundType = o.RefundType;
            }
        }

        public DocumentType InvoiceType { get; }
        public string InterfaceItemNumber { get; } = null;
        public Vectors ValidatingCarrier { get; } = null;
        public string TicketNumber { get; } = null;
        public string CommissionPercentage { get; } = null;
        public PriceData BaseFare { get; } = null;
        public List<PriceData> Taxes { get; } = null;
        public InvoiceIndicator PriceIndication { get; } = InvoiceIndicator.UNDEFINED;
        public string FormOfPayment { get; } = null;
        public string ConjunctionDocsNr { get; } = null;
        public ItineraryType RoutingIndicator { get; } = ItineraryType.Undefined;
        public bool ElectronicTicketIndicator { get; } = true;
        public Vectors ExchValidatingCarrier { get; } = null;
        public string ExchTicket { get; } = null;
        public string ExchangedCoupons { get; } = null;
        public string CouponsNrIndicator { get; } = null;
        public string RefundType { get; } = null;
        public string FormerInvoiceNr { get; } = null;
        public string AccountingNr { get; } = null;
        public string Explanation { get; }
        public AccountingType AccountingType { get; } = AccountingType.Undefined;
        public M5OperationType OperationType { get; } = M5OperationType.Undefined;

        public Fares getFares(string PassengerType)
        {
            PriceData taxes = InvoiceHelper.sumPriceDataList(Taxes);
            return new Fares()
            {
                PassengerType = PassengerType,
                BaseFare = BaseFare,
                TotalFare = InvoiceHelper.sumPriceData(BaseFare, taxes),
                TotalTaxes = taxes,
                TicketNr = TicketNumber,
            };
        }
    }

    public class HeadersData
    {
        public M0Object m0Data { get; } = null;
        public M0VoidObject m0VoidData { get; } = null;
        public string IataNr { get; set; }
        public string BookingAgency { get; set; }


        public HeadersData(M0Object m0Data, M0VoidObject m0VoidData)
        {
            this.m0Data = m0Data;
            this.m0VoidData = m0VoidData;
            if (m0Data != null)
            {
                this.IataNr = getIataNr(m0Data.agencyArcIataNr);
                //Properties.Settings.Default.AgencyIATA = this.IataNr;
                this.BookingAgency = AirUtil.PadString(m0Data.pseudoCityCode, '0', 9, true);
                Properties.Settings.Default.OfficeId = this.BookingAgency;
                Properties.Settings.Default.Save();
            }
            if (m0VoidData != null)
            {
                this.IataNr = Properties.Settings.Default.AgencyIATA;
                this.BookingAgency = Properties.Settings.Default.OfficeId;
            }
        }
        private string getIataNr(string iurIata)
        {
            var iata = iurIata.Split(' ');
            string retIata = "";
            foreach (var i in iata)
            {
                retIata += i;
            }
            return retIata;
        }
    }
}
