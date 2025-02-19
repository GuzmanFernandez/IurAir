using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace IurAir.Domain.Iur
{
    public class MGLine : IurLine
    {
        private Regex CouponRegex = new Regex(@"^(?<CpnNr>\d{2})(?<FareAmt>.{18})(?<TaxCode>.{3})(?<TaxAmt>.{18})(?<AdditionalTaxes>.{2})(?<ServiceDate>.{9})(?<NVBDate>.{9})(?<NVADate>.{9})(?<RFICCode>.{3})(?<RFICName>.{50})(?<FlightNr>.{5})(?<DepCityCode>.{3})(?<Spare1>.{2})(?<DestCityCode>.{3})(?<Spare2>.{2})(?<DepartureDate>.{9})(?<PresentTo>.{1})(?<PresentAt>.{1})(?<Spare3>[ ]{14})");
        private Regex AdditionalTaxRegex = new Regex(@"^(?<TaxCode>.{3})(?<TaxAmt>.{18})");
        public MGLine(string[] rawLines)
            : base(rawLines[0], "MG", 239, MGS.MgSections())
        {
            MgLines = rawLines;
            initAdditionalLinesData();
        }

        string[] MgLines { get; set; }

        public bool HasVarFareCalc { get; set; }
        public bool HasEndorsementResctrictions { get; set; }
        public bool HasAdditionalExchangeInfo { get; set; }
        public int CouponsNr { get; set; }
        public string VarFarCalcLine { get; set; } = "";
        public string EndorsementRestrLine { get; set; } = "";
        public Dictionary<string, CouponObject> Coupons { get; } = new Dictionary<string, CouponObject>();
        public void initAdditionalLinesData()
        {
            MgObject obj = new MgObject(this) as MgObject;
            this.CouponsNr = int.Parse(obj.CouponNr);
            this.HasVarFareCalc = obj.FareCalcInVarArea == "Y";
            this.HasEndorsementResctrictions = obj.EndorsementIndicator == "Y";
            VarFarCalcLine = IsInRange(1) ? MgLines[1] : "";
            EndorsementRestrLine = IsInRange(2) ? MgLines[2] : "";
            int lastCouponIndex = 0;
            for (int i = 0; i < MgLines.Length; i++)
            {
                string toMatch = MgLines[i];
                if (CouponRegex.IsMatch(toMatch))
                {
                    lastCouponIndex = i;
                    Match m = CouponRegex.Match(toMatch);
                    CouponObject coupon = new CouponObject(m);
                    int expectedTaxes = int.Parse(string.IsNullOrWhiteSpace(coupon.AdditionalTaxesNr) ? "0" : coupon.AdditionalTaxesNr);
                    int lastTaxIndex = 0;
                    bool hasPresentTo = coupon.PresentTo == "Y";
                    bool hasPresentAt = coupon.PresentAt == "Y";
                    if(expectedTaxes > 0 && IsInRange(lastTaxIndex))
                    {
                        int nextTax = lastCouponIndex + 1;
                        int lastTax = lastCouponIndex + expectedTaxes;
                        List<TaxObject> taxes = new List<TaxObject>();
                        while(nextTax <= lastTax)
                        {
                            if (AdditionalTaxRegex.IsMatch(MgLines[nextTax]))
                            {
                                Match tm = AdditionalTaxRegex.Match(MgLines[nextTax]);
                                taxes.Add(new TaxObject(tm.Groups["TaxCode"].Value.Trim(), tm.Groups["TaxAmt"].Value.Trim()));
                                nextTax++;
                            }
                        }
                        lastTaxIndex = nextTax;
                        coupon.Taxes = taxes;
                    }
                    else
                    {
                        lastTaxIndex = lastCouponIndex + 1;
                    }
                    if (hasPresentTo && IsInRange(lastTaxIndex +1))
                    {
                        coupon.PresentToContent = MgLines[lastTaxIndex + 1];
                    }
                    if(hasPresentAt && IsInRange(lastTaxIndex + 2))
                    {
                        coupon.PresentAtContent = MgLines[lastTaxIndex+2];
                    }
                    this.Coupons.Add(coupon.CouponNr, coupon);
                }
            }
        }


        public override Dictionary<string, string> parseLine()
        {

            Dictionary<string, string> resDictionary = new Dictionary<string, string>();
            foreach (var section in sections)
            {
                string label = section.hrLabel;
                int ssStart = section.start - 1;
                int ssEnd = section.length;
                string value = rawLine.Substring(ssStart, ssEnd);
                resDictionary.Add(label, value);
            }
            return resDictionary;

        }

        private bool IsInRange(int index)
        {
            return index < this.MgLines.Length - 1;
        }

        public override IurObject getObject()
        {
            return new MgObject(this);
        }
    }

    public class MgObject : IurObject
    {
        public string MessageId { get; set; }
        public string InterfaceItemNr { get; set; }
        public string PassengerType { get; set; }
        public string CreationLocalDate { get; set; }
        public string CreationLocalTime { get; set; }
        public string CrsId { get; set; }
        public string ValidatingCarrierCode { get; set; }
        public string AirlineCode { get; set; }
        public string DocumentNr { get; set; }
        public string CheckDigit1 { get; set; }
        public string ConjunctionTicketCount { get; set; }
        public string AirlineNumCode { get; set; }
        public string TicketNr { get; set; }
        public string CheckDigit2 { get; set; }
        public string EmdTypeIndicator { get; set; }
        public string EndorsableIndicator { get; set; }
        public string CommissionableIndicator { get; set; }
        public string RefundIndicator { get; set; }
        public string SegmentJourneyIndicator { get; set; }
        public string FareCalcInVarArea { get; set; }
        public string TravelOrigin { get; set; }
        public string TravelDestination { get; set; }
        public string RFIC { get; set; }
        public string TourCode { get; set; }
        public string TotFareCurrency { get; set; }
        public string TotFareAmt { get; set; }
        public string TotalTaxes { get; set; }
        public string TotalCommision { get; set; }
        public string EqAmtCurrency { get; set; }
        public string EqAmtFareAmt { get; set; }
        public string FormOfPayment { get; set; }
        public string CcCpy { get; set; }
        public string CcNr { get; set; }
        public string CcExpNr { get; set; }
        public string CcExtPay { get; set; }
        public string CcAuth { get; set; }
        public string CcAuthCode { get; set; }
        public string EndorsementIndicator { get; set; }
        public string ExchangeDocNr { get; set; }
        public string CouponNr { get; set; }
        public string EndorsementRestrictionStr { get; set; }
        public string FareCalcData { get; set; }
        public Dictionary<string, CouponObject> Coupons { get; set; }
        public List<M3Object> itineraryData { get; set; } = new List<M3Object>();
        public List<M5NormOrExchangeObject> AccountingLines { get; set; } = new List<M5NormOrExchangeObject>();
        public List<M5RefundObject> Refunds { get; set; } = new List<M5RefundObject>();


        public MgObject(MGLine line)
        {
            Dictionary<string, string> lineMap = line.parseLine();
            this.MessageId = lineMap.ContainsKey("MessageId") ? lineMap["MessageId"].Trim() : "";
            this.InterfaceItemNr = lineMap.ContainsKey("InterfaceItemNr") ? lineMap["InterfaceItemNr"].Trim() : "";
            this.PassengerType = lineMap.ContainsKey("PassengerType") ? lineMap["PassengerType"].Trim() : "";
            this.CreationLocalDate = lineMap.ContainsKey("CreationLocalDate") ? lineMap["CreationLocalDate"].Trim() : "";
            this.CreationLocalTime = lineMap.ContainsKey("CreationLocalTime") ? lineMap["CreationLocalTime"].Trim() : "";
            this.CrsId = lineMap.ContainsKey("CrsId") ? lineMap["CrsId"].Trim() : "";
            this.ValidatingCarrierCode = lineMap.ContainsKey("ValidatingCarrierCode") ? lineMap["ValidatingCarrierCode"].Trim() : "";
            this.AirlineCode = lineMap.ContainsKey("AirlineCode") ? lineMap["AirlineCode"].Trim() : "";
            this.DocumentNr = lineMap.ContainsKey("DocumentNr") ? lineMap["DocumentNr"].Trim() : "";
            this.CheckDigit1 = lineMap.ContainsKey("CheckDigit1") ? lineMap["CheckDigit1"].Trim() : "";
            this.ConjunctionTicketCount = lineMap.ContainsKey("ConjunctionTicketCount") ? lineMap["ConjunctionTicketCount"].Trim() : "";
            this.AirlineNumCode = lineMap.ContainsKey("AirlineNumCode") ? lineMap["AirlineNumCode"].Trim() : "";
            this.TicketNr = lineMap.ContainsKey("TicketNr") ? lineMap["TicketNr"].Trim() : "";
            this.CheckDigit2 = lineMap.ContainsKey("CheckDigit2") ? lineMap["CheckDigit2"].Trim() : "";
            this.EmdTypeIndicator = lineMap.ContainsKey("EmdTypeIndicator") ? lineMap["EmdTypeIndicator"].Trim() : "";
            this.EndorsableIndicator = lineMap.ContainsKey("EndorsableIndicator") ? lineMap["EndorsableIndicator"].Trim() : "";
            this.CommissionableIndicator = lineMap.ContainsKey("CommissionableIndicator") ? lineMap["CommissionableIndicator"].Trim() : "";
            this.RefundIndicator = lineMap.ContainsKey("RefundIndicator") ? lineMap["RefundIndicator"].Trim() : "";
            this.SegmentJourneyIndicator = lineMap.ContainsKey("SegmentJourneyIndicator") ? lineMap["SegmentJourneyIndicator"].Trim() : "";
            this.FareCalcInVarArea = lineMap.ContainsKey("FareCalcInVarArea") ? lineMap["FareCalcInVarArea"].Trim() : "";
            this.TravelOrigin = lineMap.ContainsKey("TravelOrigin") ? lineMap["TravelOrigin"].Trim() : "";
            this.TravelDestination = lineMap.ContainsKey("TravelDestination") ? lineMap["TravelDestination"].Trim() : "";
            this.RFIC = lineMap.ContainsKey("RFIC") ? lineMap["RFIC"].Trim() : "";
            this.TourCode = lineMap.ContainsKey("TourCode") ? lineMap["TourCode"].Trim() : "";
            this.TotFareCurrency = lineMap.ContainsKey("TotFareCurrency") ? lineMap["TotFareCurrency"].Trim() : "";
            this.TotFareAmt = lineMap.ContainsKey("TotFareAmt") ? lineMap["TotFareAmt"].Trim() : "";
            this.TotalTaxes = lineMap.ContainsKey("TotalTaxes") ? lineMap["TotalTaxes"].Trim() : "";
            this.TotalCommision = lineMap.ContainsKey("TotalCommision") ? lineMap["TotalCommision"].Trim() : "";
            this.EqAmtCurrency = lineMap.ContainsKey("EqAmtCurrency") ? lineMap["EqAmtCurrency"].Trim() : "";
            this.EqAmtFareAmt = lineMap.ContainsKey("EqAmtFareAmt") ? lineMap["EqAmtFareAmt"].Trim() : "";
            this.FormOfPayment = lineMap.ContainsKey("FormOfPayment") ? lineMap["FormOfPayment"].Trim() : "";
            this.CcCpy = lineMap.ContainsKey("CcCpy") ? lineMap["CcCpy"].Trim() : "";
            this.CcNr = lineMap.ContainsKey("CcNr") ? lineMap["CcNr"].Trim() : "";
            this.CcExpNr = lineMap.ContainsKey("CcExpNr") ? lineMap["CcExpNr"].Trim() : "";
            this.CcExtPay = lineMap.ContainsKey("CcExtPay") ? lineMap["CcExtPay"].Trim() : "";
            this.CcAuth = lineMap.ContainsKey("CcAuth") ? lineMap["CcAuth"].Trim() : "";
            this.CcAuthCode = lineMap.ContainsKey("CcAuthCode") ? lineMap["CcAuthCode"].Trim() : "";
            this.EndorsementIndicator = lineMap.ContainsKey("EndorsementIndicator") ? lineMap["EndorsementIndicator"].Trim() : "";
            this.ExchangeDocNr = lineMap.ContainsKey("ExchangeDocNr") ? lineMap["ExchangeDocNr"].Trim() : "";
            this.CouponNr = lineMap.ContainsKey("CouponNr") ? lineMap["CouponNr"].Trim() : "";
            this.EndorsementRestrictionStr = line.EndorsementRestrLine;
            this.FareCalcData = line.VarFarCalcLine;
            this.Coupons = line.Coupons;
        }
    }

    public class CouponObject
    {
        public string CouponNr { get; set; }
        public string FareAmt { get; set; }
        public string TaxCode { get; set; }
        public string TaxAmt { get; set; }
        public string AdditionalTaxesNr { get; set; }
        public string ServiceDate { get; set; }
        public string NotValidBefore { get; set; }
        public string NotValidAfter { get; set; }
        public string RFICSubCode { get; set; }
        public string RFICCodeName { get; set; }
        public string FlightNr { get; set; }
        public string DepartureCityCode { get; set; }
        public string DestinationCityCode { get; set; }
        public string DepartureDate { get; set; }
        public string PresentTo { get; set; }
        public string PresentAt { get; set; }
        public List<TaxObject> Taxes { get; set; } = new List<TaxObject>();
        public string PresentAtContent { get; set; } = "";
        public string PresentToContent { get; set; } = "";

        public CouponObject(Match m)
        {
            this.CouponNr = m.Groups["CpnNr"].Value.Trim() ?? "";
            this.FareAmt = m.Groups["FareAmt"].Value.Trim() ?? "";
            this.TaxCode = m.Groups["TaxCode"].Value.Trim() ?? "";
            this.TaxAmt = m.Groups["TaxAmt"].Value.Trim() ?? "";
            this.AdditionalTaxesNr = m.Groups["AdditionalTaxes"].Value.Trim() ?? "";
            this.ServiceDate = m.Groups["ServiceDate"].Value.Trim() ?? "";
            this.NotValidBefore = m.Groups["NVBDate"].Value.Trim() ?? "";
            this.NotValidAfter = m.Groups["NVADate"].Value.Trim() ?? "";
            this.RFICSubCode = m.Groups["RFICCode"].Value.Trim() ?? "";
            this.RFICCodeName = m.Groups["RFICName"].Value.Trim() ?? "";
            this.FlightNr = m.Groups["FlightNr"].Value.Trim() ?? "";
            this.DepartureCityCode = m.Groups["DepCityCode"].Value.Trim() ?? "";
            this.DestinationCityCode = m.Groups["DestCityCode"].Value.Trim() ?? "";
            this.DepartureDate = m.Groups["DepartureDate"].Value.Trim() ?? "";
            this.PresentTo = m.Groups["PresentTo"].Value.Trim() ?? "";
            this.PresentAt = m.Groups["PresentAt"].Value.Trim() ?? "";
        }
    }

    public class TaxObject 
    {
        public string TaxCode { get; }
        public string TaxAmount { get;}
        public TaxObject(string TaxCode, string TaxAmt)
        {
            this.TaxCode = TaxCode;
            this.TaxAmount= TaxAmt;
        }
    }
}
