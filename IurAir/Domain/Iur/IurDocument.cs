using IurAir.Domain.Air;
using IurAir.Domain.Air.Shared;
using IurAir.Domain.Common;
using IurAir.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IurAir.Domain.Iur
{
    public class IurDocument
    {
        public string[] linesArray { get; set; }
        public List<M0Line> m0Lines { get; set; }
        public List<M0VoidLine> m0VoidLines { get; set; }
        public List<M1Line> m1Lines { get; set; }
        public List<M2Line> m2Lines { get; set; }
        public List<M3Line> m3Lines { get; set; }
        public List<M4Line> m4Lines { get; set; }
        public List<M5Line> m5Lines { get; set; }
        public List<M6Line> m6Lines { get; set; }
        public List<M7Line> m7Lines { get; set; }
        public List<M8Line> m8Lines { get; set; }
        public List<M9Line> m9Lines { get; set; }
        public List<MELine> mELines { get; set; }
        public List<MGLine> MGLines { get; set; }
        public List<Remark> Remarks { get; set; }
        public PnrFileInfo PnrFileInfo { get; }
        public VariableDataCollector variableData { get; set; }
        public Dictionary<string, IurObject> compositeLines = new Dictionary<string, IurObject>();
        public DocumentType DocumentType { get; set; } = DocumentType.Undefined;
        public DocumentDate DocumentDate { get; set; } = null;
        public string AgencyIata { get; set; }

        public IurDocument(string[] linesArray,PnrFileInfo pnrInfo)
        {
            
            this.PnrFileInfo = pnrInfo;
            m0Lines = new List<M0Line>();
            m0VoidLines = new List<M0VoidLine>();
            m1Lines = new List<M1Line>();
            m2Lines = new List<M2Line>();
            m3Lines = new List<M3Line>();
            m4Lines = new List<M4Line>();
            m5Lines = new List<M5Line>();
            m6Lines = new List<M6Line>();
            m7Lines = new List<M7Line>();
            m8Lines = new List<M8Line>();
            m9Lines = new List<M9Line>();
            mELines = new List<MELine>();
            MGLines = new List<MGLine>();
            Remarks = new List<Remark>();

            this.linesArray = linesArray;
            this.variableData = new VariableDataCollector(linesArray);
            string m0L = linesArray[0];
            if (variableData.IsVoid)
            {
                var voidLine = new M0VoidLine(m0L);
                m0VoidLines.Add(voidLine);
                compositeLines.Add("M0", voidLine.getObject());
                M0VoidObject m0v = voidLine.getObject() as M0VoidObject;
                DocumentDate = new DocumentDate( m0v.transmissionDay, m0v.transmissionMonth,  m0v.transmissionTime );
                DocumentType = DocumentType.VoidTicket;
            }
            else
            {
                var line = new M0Line(m0L);
                m0Lines.Add(line);
                compositeLines.Add("M0", line.getObject());
                M0Object m0o = line.getObject() as M0Object;
                AgencyIata = m0o.agencyArcIataNr.Replace(" ", "");
                DocumentDate = new DocumentDate(m0o.transmissionDay, m0o.transmissionMonth, m0o.transmissionTime);
            }
            var VarDataObj = variableData.getObject();
            compositeLines.Add("VarData", VarDataObj);

            int currentIndex = 0;
            foreach (string line in linesArray)
            {
                if (line.StartsWith("M1"))
                {
                    var mLine = new M1Line(line);
                    var mObject = new M1Object(mLine);
                    //var getted = mLine.getObject();
                    if (inRange(currentIndex + 1))
                    {
                        string itineraries = linesArray[currentIndex + 1];
                        if (!string.IsNullOrEmpty(itineraries))
                        {
                            mObject.itineraryNumbersForPassenger = (itineraries.SplitInParts(2)).ToArray();

                        }
                    }
                    if (inRange(currentIndex + 2))
                    {
                        string accLines = linesArray[currentIndex + 2];
                        if (!string.IsNullOrEmpty(accLines))
                        {
                            mObject.invoiceForPassenger = (accLines.SplitInParts(2)).ToArray();

                        }
                    }
                    if (inRange(currentIndex + 3))
                    {
                        string m7Remarks = linesArray[currentIndex + 3];
                        if (!string.IsNullOrEmpty(m7Remarks))
                        {
                            mObject.m7ForPassenger = (m7Remarks.SplitInParts(2)).ToArray();

                        }
                    }
                    if (inRange(currentIndex + 4))
                    {
                        string m8Remarks = linesArray[currentIndex + 4];
                        if (!string.IsNullOrEmpty(m8Remarks))
                        {
                            mObject.m8ForPassenger = (m8Remarks.SplitInParts(2)).ToArray();
                        }
                    }
                    if (inRange(currentIndex + 5))
                    {
                        string m9Remarks = linesArray[currentIndex + 5];
                        if (!string.IsNullOrEmpty(m9Remarks))
                        {
                            mObject.m9ForPassenger = (m9Remarks.SplitInParts(2)).ToArray();
                        }
                    }
                    this.m1Lines.Add(mLine);

                    this.compositeLines.Add($"M1{mObject.interfaceNameItemNumber}", mObject);
                }
                if (line.StartsWith("M2"))
                {
                    var mLine = new M2Line(line);
                    var mObject = new M2Object(mLine);
                    if(inRange(currentIndex + 1))
                    {
                        string entitlements = linesArray[currentIndex + 1];
                        if (!string.IsNullOrEmpty(entitlements))
                        {
                            mObject.M4ForPassenger = (entitlements.SplitInParts(2)).ToArray();
                        }
                    }
                    if (inRange(currentIndex + 2))
                    {
                        string fareCalc = linesArray[currentIndex + 2];
                        if (!string.IsNullOrEmpty(fareCalc))
                        {
                            mObject.M6ForPassenger = (fareCalc.SplitInParts(2)).ToArray();
                        }
                    }
                    string FOP = linesArray[currentIndex + 3];
                    mLine.FormOfPayment = FOP;
                    this.m2Lines.Add(mLine);
                    this.compositeLines.Add($"M2{mObject.interfaceNameItemNr}", mObject);
                }
                if (line.StartsWith("M3"))
                {
                    var mLine = new M3Line(line);
                    var mObject = new M3Object(mLine);
                    this.m3Lines.Add(mLine);
                    this.compositeLines.Add($"M3{mObject.itineraryItemNumber}", mObject);
                }
                if (line.StartsWith("M4"))
                {
                    var m4Line = new M4Line(line);
                    this.m4Lines.Add(m4Line);
                    var mObject = new M4Object((m4Line));
                    string key = $"M4{mObject.m3RecordItemNumber}{mObject.passengerTypeCode}";
                    if (compositeLines.ContainsKey(key))
                    {
                        key = $"{key}_{compositeLines.Count + 1}";
                    }
                    this.compositeLines.Add(key, mObject);
                }
                if (line.StartsWith("M5"))
                {
                    var m5Line = new M5Line(line);
                    this.m5Lines.Add(m5Line);
                    var mObject = m5Line.getObject();
                    if (mObject is M5NormOrExchangeObject)
                    {
                        M5NormOrExchangeObject normObject = mObject as M5NormOrExchangeObject;
                        if(normObject.GetAccountingType() != AccountingType.Normal)
                        {
                            string index = (this.m5Lines.Count + 1).ToString();
                            this.compositeLines.Add($"M5{normObject.AccountingNr}_{index}", mObject);
                        }
                        else
                        {
                            this.compositeLines.Add($"M5{normObject.AccountingNr}", mObject);
                        }
                        
                    }
                    else if (mObject is M5RefundObject)
                    {
                        this.DocumentType = DocumentType.Refund;
                        this.compositeLines.Add($"M5{(mObject as M5RefundObject).AccountingNr}", mObject);
                    }
                    else if(mObject is M5ServiceFeeLineObject)
                    {
                        this.compositeLines.Add($"M5{(mObject as M5ServiceFeeLineObject).AccountingNr}", mObject);
                    }

                }
                if (line.StartsWith("M6"))
                {
                    var m6Line = new M6Line(line);
                    this.m6Lines.Add(m6Line);
                    var mObject = new M6Object(m6Line);
                    this.compositeLines.Add($"M6{mObject.PassengerType}", mObject);
                }
                if (line.StartsWith("M7"))
                {
                    var m7Line = new M7Line(line);
                    this.m7Lines.Add(m7Line);
                    var mObject = new M7Object(m7Line);
                    this.compositeLines.Add($"M7{mObject.RemarkItemNr}", mObject);
                }
                if (line.StartsWith("M8"))
                {
                    var m8Line = new M8Line(line);
                    this.m8Lines.Add(m8Line);
                    var mObject = new M8Object(m8Line);
                    this.compositeLines.Add($"M8{mObject.RemarkItemNr}", mObject);
                }
                if (line.StartsWith("M9"))
                {
                    var m9Line = new M9Line(line);
                    this.m9Lines.Add(m9Line);
                    var mObject = new M9Object(m9Line);
                    this.compositeLines.Add($"M9{mObject.RemarkItemNr}", mObject);
                }
                if (line.StartsWith("ME"))
                {
                    var mELine = new MELine(line);
                    this.mELines.Add(mELine);
                    var mObject = new MEObject(mELine);
                    this.compositeLines.Add($"ME{mObject.AssociatedM3ItemNr}_{currentIndex}", mObject);
                }
                if (line.StartsWith("MG"))
                {
                    List<string> lines = new List<string>();
                    int mgIndex = currentIndex;
                    while (mgIndex < linesArray.Length - 1)
                    {
                        lines.Add(linesArray[mgIndex]);
                        mgIndex++;
                    }
                    var mgLine = new MGLine(lines.ToArray());
                    MGLines.Add(mgLine);
                    MgObject mgObj = mgLine.getObject() as MgObject;
                    this.compositeLines.Add($"{mgObj.InterfaceItemNr}", mgObj);
                }
                currentIndex++;
            }
            this.Remarks = getRemarks();
            this.DocumentType = CheckType();
        }
        private bool inRange(int index)
        {
            try
            {
                var i = linesArray[index];
                return true;
            }
            catch (Exception e) { return false; }
        }

        private List<Remark> getRemarks()
        {
            List<Remark> remarks = new List<Remark>();
            foreach (M7Line m7 in m7Lines)
            {
                var m7o = m7.getObject() as M7Object;
                string content = m7o.ItineraryRemark;
                if (!content.Contains("PT-SSR"))
                {
                    remarks.Add(new Remark()
                    {
                        Type = RemarkType.ItineraryRemark,
                        RemarkContent = content,
                        AssociatedSegment = m7o.RemarkItemNr
                    });
                }
            }
            foreach (M8Line m8 in m8Lines)
            {
                var m8o = m8.getObject() as M8Object;
                string content = m8o.PassengerRemark;
                if (!content.Contains("PT-SSR"))
                {
                    remarks.Add(new Remark()
                    {
                        Type = RemarkType.PassengerRemark,
                        RemarkContent = content,
                        AssociatedSegment = m8o.RemarkItemNr
                    });
                }
            }
            foreach (M9Line m9 in m9Lines)
            {
                var m9o = m9.getObject() as M9Object;
                string content = m9o.InterfaceRemark;
                if (!content.Contains("PT-SSR"))
                {
                    remarks.Add(new Remark()
                    {
                        Type = RemarkType.InterfaceRemark,
                        RemarkContent = content,
                        AssociatedSegment = m9o.RemarkItemNr
                    });
                }
            }
            return remarks;
        }
        public AirRequiredData ExtractAirData()
        {
            List<MgObject> objects = new List<MgObject>();
            List<Remark> remarks = new List<Remark>();
            M0VoidObject m0VO = CheckType() == DocumentType.VoidTicket ? m0VoidLines.FirstOrDefault().getObject() as M0VoidObject : null;
            M0Object m0O = CheckType() != DocumentType.VoidTicket ? m0Lines.FirstOrDefault().getObject() as M0Object : null;
            DocumentDate dd = new DocumentDate("10", "JUL", "0900");
           
            foreach (MGLine mgl in MGLines)
            {
                objects.Add(mgl.getObject() as MgObject);
            }
            foreach (M7Line m7 in m7Lines)
            {
                var m7o = m7.getObject() as M7Object;
                string content = m7o.ItineraryRemark;
                if (!content.Contains("PT-SSR"))
                {
                    remarks.Add(new Remark()
                    {
                        Type = RemarkType.ItineraryRemark,
                        RemarkContent = content,
                        AssociatedSegment = m7o.RemarkItemNr
                    });
                }
            }
            foreach (M8Line m8 in m8Lines)
            {
                var m8o = m8.getObject() as M8Object;
                string content = m8o.PassengerRemark;
                if (!content.Contains("PT-SSR"))
                {
                    remarks.Add(new Remark()
                    {
                        Type = RemarkType.PassengerRemark,
                        RemarkContent = content,
                        AssociatedSegment = m8o.RemarkItemNr
                    });
                }
            }
            foreach (M9Line m9 in m9Lines)
            {
                var m9o = m9.getObject() as M9Object;
                string content = m9o.InterfaceRemark;
                if (!content.Contains("PT-SSR"))
                {
                    remarks.Add(new Remark()
                    {
                        Type = RemarkType.InterfaceRemark,
                        RemarkContent = content,
                        AssociatedSegment= m9o.RemarkItemNr
                    });
                }
            }
            List<PassengerData> data = new List<PassengerData>();
            if (objects.Count > 0)
            {
                data = ChunkPerPax(objects);
            }
            else
            {
                data = ChunkPerPax();
            }
            return new AirRequiredData()
            {
                DocumentType = objects.Count > 0 ? DocumentType.EMD.ToString() : CheckType().ToString(),
                Passengers = data,
                PassengerTotalNumber = data.Count,
                M0VoidObject = m0VO,
                M0Object = m0O,
                MgObjects = objects,
                Remarks = remarks,
                BookingAgencyCityCode = m0O != null ? m0O.pseudoCityCode : "",
            };
        }

        /*
         * public static string EvenExch = "Even Exchange";
        public static string SecondFormOfPayment = "Second Form of Payment";
        public static string Refund = "Refund";
        public static string NormalTicket = "Normal Ticket";*/

        public DocumentType CheckType()
        {
            DocumentType dt = DocumentType.Undefined;
            if (this.variableData.IsVoid)
            {
                dt = DocumentType.VoidTicket;
            }
            foreach (var line in m5Lines)
            {
                var obj = line.getObject();
                if (obj is M5RefundObject)
                {
                    dt = DocumentType.Refund;
                }
                else if (obj is M5NormOrExchangeObject)
                {
                    var opType = ((M5NormOrExchangeObject)obj).OperationType;
                    switch (opType)
                    {
                        case "A":
                            dt = DocumentType.Exchange;
                            break;
                        case "Exchange with Additional Collection":
                            dt = DocumentType.Exchange;
                            break;
                        case "E":
                            dt = DocumentType.EvenExchange;
                            break;
                        case "Even Exchange":
                            dt = DocumentType.EvenExchange;
                            break;
                        default:
                            dt = DocumentType.TicketEmission;
                            break;
                    }
                }
            }
            if (MGLines.Count > 0)
            {
                return DocumentType.EMD;
            }

            return dt;
        }

        private List<PassengerData> ChunkPerPax(List<MgObject> mgObjects = null)
        {
            List<PassengerData> chunks = new List<PassengerData>();
            if (this.variableData.IsVoid)
            {
                return chunks;
            }
            int i = 0;
            int lastPaxIndex = m1Lines.Count - 1;
            while (i <= lastPaxIndex)
            {
                M1Object m1 = m1Lines[i].getObject() as M1Object;
                string id = m1.interfaceNameItemNumber;
                string paxType = "";
                M2Object m2Object = null;
                M6Object m6Object = null;
                M4Object m4Object = null;
                List<M3Object> passengerItineraryData = new List<M3Object>();
                List<M5NormOrExchangeObject> passengerAccountingData = new List<M5NormOrExchangeObject>();
                List<M5RefundObject> passengerRefoundingData = new List<M5RefundObject>();
                foreach (M2Line m2l in m2Lines)
                {
                    M2Object m2Obj = m2l.getObject() as M2Object;
                    if (m2Obj.interfaceNameItemNr == id)
                    {
                        m2Object = m2Obj;
                        paxType = m2Obj.passengerType;
                    }
                }
                foreach (M4Line m4l in m4Lines)
                {
                    M4Object m4Obj = m4l.getObject() as M4Object;
                    if (m4Obj.passengerTypeCode == paxType)
                    {
                        m4Object = m4Obj;
                    }
                }
                foreach (M3Line m3l in m3Lines)
                {
                    passengerItineraryData.Add(m3l.getObject() as M3Object);
                    if (mgObjects != null && mgObjects.Count > 0)
                    {
                        foreach (var mg in mgObjects)
                        {
                            mg.itineraryData.Add(m3l.getObject() as M3Object);

                        }
                    }
                }
                foreach (M5Line m5l in m5Lines)
                {
                    var m5Obj = m5l.getObject();
                    if (m5Obj is M5NormOrExchangeObject)
                    {
                        M5NormOrExchangeObject norm = m5Obj as M5NormOrExchangeObject;
                        if (mgObjects != null && mgObjects.Count > 0)
                        {
                            foreach (var mg in mgObjects)
                            {
                                mg.AccountingLines.Add(norm);
                            }
                        }
                        if (norm.InterfaceItemNr == id)
                        {
                            passengerAccountingData.Add(norm);
                        }
                    }
                    else if (m5Obj is M5RefundObject)
                    {
                        M5RefundObject refund = m5Obj as M5RefundObject;
                        if (mgObjects != null && mgObjects.Count > 0)
                        {
                            foreach (var mg in mgObjects)
                            {
                                mg.Refunds.Add(refund);
                            }
                        }
                        if (refund.InterfaceItemNr == id)
                        {
                            passengerRefoundingData.Add(refund);
                        }
                    }
                }
                foreach (M6Line m6l in m6Lines)
                {
                    M6Object m6Obj = m6l.getObject() as M6Object;
                    if (m6Obj.PassengerType == paxType)
                    {
                        m6Object = m6Obj;
                    }
                }
                if ((m2Object != null && m6Object != null) && mgObjects == null)
                {
                    PassengerData pd = new PassengerData()
                    {
                        PassengerType = paxType,
                        PassengerM1 = m1,
                        PassengerM2 = m2Object,
                        PassengerM6 = m6Object,
                        PassengerM4 = m4Object,
                        EconomicInfo = m2Object.GetEconomicInfo(),
                        AccessoryInfo = m2Object.GetAccessoryInfo(),
                        PassengerItineraryData = passengerItineraryData,
                        PassengerAccountingData = passengerAccountingData,
                        PassengerRefoundingData = passengerRefoundingData,
                        PassengerName = m1.passengerName,
                        ItineraryType = m2Object.internationalItinIndicator == "X" ? "International" : "Domestic",
                        MgObjects = null,
                    };
                    chunks.Add(pd);
                    i++;
                }
                else if (mgObjects != null && (m2Object == null && m6Object == null))
                {

                    PassengerData pd = new PassengerData()
                    {
                        PassengerType = paxType,
                        PassengerM1 = m1,
                        PassengerM2 = null,
                        PassengerM6 = null,
                        PassengerM4 = m4Object ?? null,
                        EconomicInfo = null,
                        AccessoryInfo = null,
                        PassengerItineraryData = passengerItineraryData,
                        PassengerAccountingData = passengerAccountingData,
                        PassengerRefoundingData = passengerRefoundingData,
                        PassengerName = m1.passengerName,
                        ItineraryType = "NA",
                        MgObjects = mgObjects ?? new List<MgObject>(),
                    };
                    chunks.Add(pd);
                    i++;
                }
                else
                {
                    i++;
                }
            }
            return chunks;
        }
    }



    public class AirRequiredData
    {
        public string DocumentType { get; set; }
        public DocumentDate EmissionData { get; set; }
        public int PassengerTotalNumber { get; set; }
        public M0Object M0Object { get; set; }
        public M0VoidObject M0VoidObject { get; set; }
        public List<MgObject> MgObjects { get; set; } = new List<MgObject>();
        public List<PassengerData> Passengers { get; set; }
        public string BookingAgencyCityCode { get; set; }
        public List<Remark> Remarks { get; set; } = new List<Remark>();
    }

    public class PassengerData
    {
        public string PassengerName { get; set; }
        public string PassengerType { get; set; }
        public string ItineraryType { get; set; }
        public M1Object PassengerM1 { get; set; }
        public M2Object PassengerM2 { get; set; }
        public M4Object PassengerM4 { get; set; }
        public M6Object PassengerM6 { get; set; }
        public M2EconomicInfo EconomicInfo { get; set; }
        public M2AccessoryInfo AccessoryInfo { get; set; }
        public List<M3Object> PassengerItineraryData { get; set; }
        public List<M5NormOrExchangeObject> PassengerAccountingData { get; set; }
        public List<M5RefundObject> PassengerRefoundingData { get; set; }
        public List<MgObject> MgObjects { get; set; }

        public Passenger toPassenger()
        {
            string Tnr = "Not Available";
            if (PassengerM2 != null)
            {
                if (string.IsNullOrEmpty(PassengerM2.ticketNumber))
                {
                    Tnr = "Not Available";
                }
                else
                {
                    Tnr = PassengerM2.ticketNumber;
                }
            }
            return new Passenger()
            {
                PassengerCategory = this.PassengerType,
                PassengerName = PassengerName.Replace('/', ' '),
                InterfaceNr = PassengerM1.interfaceNameItemNumber,
                TicketNumber = Tnr,
            };
        }
    }

    public class ResidentialFarePax
    {
        public bool isResident { get; set; } = false;
        public string docNumber { get; set; } = "";
        public string postalCode { get; set; } = "";

    }

    public enum DocumentType
    {
        TicketEmission,
        VoidTicket,
        EvenExchange,
        Exchange,
        Refund,
        EMD,
        Undefined
    }

    public class DocumentDate
    {
        public string Day { get; }
        public string Month { get; }
        public string Time { get; }

        public DocumentDate(string day, string month, string time)
        {
            Day = day;
            Month = month;
            Time = time;
        }

        public DateTime toDateTime()
        {
            return ParseDateTime($"{Day}{Month}{Time}");
        }

        public DateTime toDate()
        {
            return ParseDateOnly($"{Day}{Month}");
        }

        private DateTime ParseDateTime(string dt)
        {
            string Format24h = "ddMMMHHmm";
            string Format12h = "ddMMMhhmmt";
            CultureInfo ci = new CultureInfo("en-US");
            bool is24 = dt.Length == 9;
            DateTime outDate = DateTime.Now;
            if (is24)
            {
                DateTime.TryParseExact(dt, Format24h, ci, DateTimeStyles.None, out outDate);
            }
            else
            {
                DateTime.TryParseExact(dt, Format12h, ci, DateTimeStyles.None, out outDate);
            }
            return outDate;
        }

        private DateTime ParseDateOnly(string dt)
        {
            string Format = "ddMMM";
            DateTime outDate = DateTime.Now;
            CultureInfo ci = new CultureInfo("en-US");
            DateTime.TryParseExact(dt, Format, ci, DateTimeStyles.None, out outDate);
            return outDate;
        }
    }

}
