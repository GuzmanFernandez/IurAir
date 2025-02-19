using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class M2Line : IurLine
    {
        public M2Line(string rawLine) : base(rawLine, "M2", 245, M2S.M2Sections()) { }

        public string FormOfPayment { get; set; }

        public override IurObject getObject()
        {
            return new M2Object(this);
        }
    }

    public class M2Object : IurObject
    {
        [DefaultValue("")]
        public string interfaceNameItemNr { get; set; }
        [DefaultValue("")]
        public string passengerType { get; set; }
        [DefaultValue("")]
        public string tcnNumber { get; set; }
        [DefaultValue("")]
        public string internationalItinIndicator { get; set; }
        [DefaultValue("")]
        public string formOfPayment { get; set; }
        [DefaultValue("")]
        public string ticketIndicator { get; set; }
        [DefaultValue("")]
        public string combinedTicketAndBp { get; set; }
        [DefaultValue("")]
        public string ticketOnly { get; set; }
        [DefaultValue("")]
        public string bpOnly { get; set; }
        [DefaultValue("")]
        public string remotePrintAuditorCoupon { get; set; }
        [DefaultValue("")]
        public string passengerReceiptOnly { get; set; }
        [DefaultValue("")]
        public string remPrintAgentsCoupon { get; set; }
        [DefaultValue("")]
        public string remPrintCreditCard { get; set; }
        [DefaultValue("")]
        public string invoiceDocument { get; set; }
        [DefaultValue("")]
        public string miniItinerary { get; set; }
        [DefaultValue("")]
        public string magneticEncoding { get; set; }
        [DefaultValue("")]
        public string usContractWholesaleBulk { get; set; }
        [DefaultValue("")]
        public string bspTicketSuppression { get; set; }
        [DefaultValue("")]
        public string fareSign { get; set; }
        [DefaultValue("")]
        public string fareCurrencyCode { get; set; }
        [DefaultValue("")]
        public string fareAmount { get; set; }
        [DefaultValue("")]
        public string tax1Sign { get; set; }
        [DefaultValue("")]
        public string tax1Amount { get; set; }
        [DefaultValue("")]
        public string tax1Id { get; set; }
        [DefaultValue("")]
        public string tax2Sign { get; set; }
        [DefaultValue("")]
        public string tax2Amount { get; set; }
        [DefaultValue("")]
        public string tax2Id { get; set; }
        [DefaultValue("")]
        public string tax3Sign { get; set; }
        [DefaultValue("")]
        public string tax3Amount { get; set; }
        [DefaultValue("")]
        public string tax3Id { get; set; }
        [DefaultValue("")]
        public string totalFareSign { get; set; }
        [DefaultValue("")]
        public string totalFareCurrCode { get; set; }
        [DefaultValue("")]
        public string totalFareAmount { get; set; }
        [DefaultValue("")]
        public string cancellationPenaltyAmount { get; set; }
        [DefaultValue("")]
        public string commissionOnPenalty { get; set; }
        [DefaultValue("")]
        public string obIndicator { get; set; }
        [DefaultValue("")]
        public string spareSpace { get; set; }
        [DefaultValue("")]
        public string equivalentPaidSign { get; set; }
        [DefaultValue("")]
        public string equivalentFareCurrCode { get; set; }
        [DefaultValue("")]
        public string equivalentFare { get; set; }
        [DefaultValue("")]
        public string commissionPercentage { get; set; }
        [DefaultValue("")]
        public string commissionSign { get; set; }
        [DefaultValue("")]
        public string commissionAmt { get; set; }
        [DefaultValue("")]
        public string netAmountSign { get; set; }
        [DefaultValue("")]
        public string netAmt { get; set; }
        [DefaultValue("")]
        public string canadianTickDesCode { get; set; }
        [DefaultValue("")]
        public string canadianTickTravelCode { get; set; }
        [DefaultValue("")]
        public string foreignTarifFlag { get; set; }
        [DefaultValue("")]
        public string travelAgencyTax { get; set; }
        [DefaultValue("")]
        public string inclusiveTourNr { get; set; }
        [DefaultValue("")]
        public string ticketingCity { get; set; }
        [DefaultValue("")]
        public string printMiniItinerary { get; set; }
        [DefaultValue("")]
        public string creditCardInfoPrintSuppression { get; set; }
        [DefaultValue("")]
        public string creditCardAuthSourceCode { get; set; }
        [DefaultValue("")]
        public string fareAgentPcc { get; set; }
        [DefaultValue("")]
        public string fareAgentDutyCode { get; set; }
        [DefaultValue("")]
        public string fareAgentSine { get; set; }
        [DefaultValue("")]
        public string printAgentPcc { get; set; }
        [DefaultValue("")]
        public string printAgentDutyCode { get; set; }
        [DefaultValue("")]
        public string printAgentSine { get; set; }
        [DefaultValue("")]
        public string spare2 { get; set; }
        [DefaultValue("")]
        public string creditCardAuth { get; set; }
        [DefaultValue("")]
        public string newFopIndicator { get; set; }
        [DefaultValue("")]
        public string privateFareIndicator { get; set; }
        [DefaultValue("")]
        public string spare3 { get; set; }
        [DefaultValue("")]
        public string ccExpDate { get; set; }
        [DefaultValue("")]
        public string ccExtPaymentMonths { get; set; }
        [DefaultValue("")]
        public string m4RecordCount { get; set; }
        [DefaultValue("")]
        public string m6RecordCount { get; set; }
        [DefaultValue("")]
        public string validatingCarrierCode { get; set; }
        [DefaultValue("")]
        public string ticketNumber { get; set; }
        [DefaultValue("")]
        public string conjunctionTktCount { get; set; }
        [DefaultValue("")]
        public string FormOfPayment { get; set; }
        public string[] M4ForPassenger { get; set; } = { };
        public string[] M6ForPassenger { get; set; } = { };


        public M2Object(M2Line line)
        {
            Dictionary<string, string> lineMap = line.parseLine();
            try
            {
                this.FormOfPayment = line.FormOfPayment;
            }
            catch (Exception e)
            {
                this.FormOfPayment = "";
            }
            this.interfaceNameItemNr = lineMap.ContainsKey("IU2PNO") ? lineMap["IU2PNO"].Trim() : "";
            this.passengerType = lineMap.ContainsKey("IU2PTY") ? lineMap["IU2PTY"].Trim() : "";
            this.tcnNumber = lineMap.ContainsKey("TCN") ? lineMap["TCN"].Trim() : "";
            this.internationalItinIndicator = lineMap.ContainsKey("IU2INT") ? lineMap["IU2INT"].Trim() : "";
            this.formOfPayment = lineMap.ContainsKey("IU2SSI") ? lineMap["IU2SSI"].Trim() : "";
            this.ticketIndicator = lineMap.ContainsKey("IU2IND") ? lineMap["IU2IND"].Trim() : "";
            this.combinedTicketAndBp = lineMap.ContainsKey("IU2AP1") ? lineMap["IU2AP1"].Trim() : "";
            this.ticketOnly = lineMap.ContainsKey("IU2AP2") ? lineMap["IU2AP2"].Trim() : "";
            this.bpOnly = lineMap.ContainsKey("IU2AP3") ? lineMap["IU2AP3"].Trim() : "";
            this.remotePrintAuditorCoupon = lineMap.ContainsKey("IU2AP4") ? lineMap["IU2AP4"].Trim() : "";
            this.passengerReceiptOnly = lineMap.ContainsKey("IU2AP5") ? lineMap["IU2AP5"].Trim() : "";
            this.remPrintAgentsCoupon = lineMap.ContainsKey("IU2AP6") ? lineMap["IU2AP6"].Trim() : "";
            this.remPrintCreditCard = lineMap.ContainsKey("IU2AP7") ? lineMap["IU2AP7"].Trim() : "";
            this.invoiceDocument = lineMap.ContainsKey("IU2AP8") ? lineMap["IU2AP8"].Trim() : "";
            this.miniItinerary = lineMap.ContainsKey("IU2AP9") ? lineMap["IU2AP9"].Trim() : "";
            this.magneticEncoding = lineMap.ContainsKey("IU2APA") ? lineMap["IU2APA"].Trim() : "";
            this.usContractWholesaleBulk = lineMap.ContainsKey("IU2APB") ? lineMap["IU2APB"].Trim() : "";
            this.bspTicketSuppression = lineMap.ContainsKey("IU2APZ") ? lineMap["IU2APZ"].Trim() : "";
            this.fareSign = lineMap.ContainsKey("IU2FSN") ? lineMap["IU2FSN"].Trim() : "";
            this.fareCurrencyCode = lineMap.ContainsKey("IU2FCC") ? lineMap["IU2FCC"].Trim() : "";
            this.fareAmount = lineMap.ContainsKey("IU2FAR") ? lineMap["IU2FAR"].Trim() : "";
            this.tax1Sign = lineMap.ContainsKey("IU2T1S") ? lineMap["IU2T1S"].Trim() : "";
            this.tax1Amount = lineMap.ContainsKey("IU2TX1") ? lineMap["IU2TX1"].Trim() : "";
            this.tax1Id = lineMap.ContainsKey("IU2ID1") ? lineMap["IU2ID1"].Trim() : "";
            this.tax2Sign = lineMap.ContainsKey("IU2T2S") ? lineMap["IU2T2S"].Trim() : "";
            this.tax2Amount = lineMap.ContainsKey("IU2TX2") ? lineMap["IU2TX2"].Trim() : "";
            this.tax2Id = lineMap.ContainsKey("IU2ID2") ? lineMap["IU2ID2"].Trim() : "";
            this.tax3Sign = lineMap.ContainsKey("IU2T3S") ? lineMap["IU2T3S"].Trim() : "";
            this.tax3Amount = lineMap.ContainsKey("IU2TX3") ? lineMap["IU2TX3"].Trim() : "";
            this.tax3Id = lineMap.ContainsKey("IU2ID3") ? lineMap["IU2ID3"].Trim() : "";
            this.totalFareSign = lineMap.ContainsKey("IU2TFS") ? lineMap["IU2TFS"].Trim() : "";
            this.totalFareCurrCode = lineMap.ContainsKey("IU2TFC") ? lineMap["IU2TFC"].Trim() : "";
            this.totalFareAmount = lineMap.ContainsKey("IU2TFR") ? lineMap["IU2TFR"].Trim() : "";
            this.cancellationPenaltyAmount = lineMap.ContainsKey("IU2PEN") ? lineMap["IU2PEN"].Trim() : "";
            this.commissionOnPenalty = lineMap.ContainsKey("IU2KXP") ? lineMap["IU2KXP"].Trim() : "";
            this.obIndicator = lineMap.ContainsKey("IU2OBF") ? lineMap["IU2OBF"].Trim() : "";
            this.spareSpace = lineMap.ContainsKey("IU2SPX") ? lineMap["IU2SPX"].Trim() : "";
            this.equivalentPaidSign = lineMap.ContainsKey("IU2EPS") ? lineMap["IU2EPS"].Trim() : "";
            this.equivalentFareCurrCode = lineMap.ContainsKey("IU2EFC") ? lineMap["IU2EFC"].Trim() : "";
            this.equivalentFare = lineMap.ContainsKey("IU2EFR") ? lineMap["IU2EFR"].Trim() : "";
            this.commissionPercentage = lineMap.ContainsKey("IU2PCT") ? lineMap["IU2PCT"].Trim() : "";
            this.commissionSign = lineMap.ContainsKey("IU2CSN") ? lineMap["IU2CSN"].Trim() : "";
            this.commissionAmt = lineMap.ContainsKey("IU2COM") ? lineMap["IU2COM"].Trim() : "";
            this.netAmountSign = lineMap.ContainsKey("IU2NAS") ? lineMap["IU2NAS"].Trim() : "";
            this.netAmt = lineMap.ContainsKey("IU2NET") ? lineMap["IU2NET"].Trim() : "";
            this.canadianTickDesCode = lineMap.ContainsKey("IU2CDC") ? lineMap["IU2CDC"].Trim() : "";
            this.canadianTickTravelCode = lineMap.ContainsKey("IU2CTT") ? lineMap["IU2CTT"].Trim() : "";
            this.foreignTarifFlag = lineMap.ContainsKey("IU2FTF") ? lineMap["IU2FTF"].Trim() : "";
            this.travelAgencyTax = lineMap.ContainsKey("IU2TAT") ? lineMap["IU2TAT"].Trim() : "";
            this.inclusiveTourNr = lineMap.ContainsKey("IU2TUR") ? lineMap["IU2TUR"].Trim() : "";
            this.ticketingCity = lineMap.ContainsKey("IU2TPC") ? lineMap["IU2TPC"].Trim() : "";
            this.printMiniItinerary = lineMap.ContainsKey("IU2MIT") ? lineMap["IU2MIT"].Trim() : "";
            this.creditCardInfoPrintSuppression = lineMap.ContainsKey("IU2CCB") ? lineMap["IU2CCB"].Trim() : "";
            this.creditCardAuthSourceCode = lineMap.ContainsKey("IU2APC") ? lineMap["IU2APC"].Trim() : "";
            this.fareAgentPcc = lineMap.ContainsKey("IU2SN4") ? lineMap["IU2SN4"].Trim() : "";
            this.fareAgentDutyCode = lineMap.ContainsKey("IU2SN3") ? lineMap["IU2SN3"].Trim() : "";
            this.fareAgentSine = lineMap.ContainsKey("IU2SN2") ? lineMap["IU2SN2"].Trim() : "";
            this.printAgentPcc = lineMap.ContainsKey("IU2PN4") ? lineMap["IU2PN4"].Trim() : "";
            this.printAgentDutyCode = lineMap.ContainsKey("IU2PN3") ? lineMap["IU2PN3"].Trim() : "";
            this.printAgentSine = lineMap.ContainsKey("IU2PN2") ? lineMap["IU2SN2"].Trim() : "";
            this.spare2 = lineMap.ContainsKey("IU2AVI") ? lineMap["IU2AVI"].Trim() : "";
            this.creditCardAuth = lineMap.ContainsKey("IU2ATH") ? lineMap["IU2ATH"].Trim() : "";
            this.newFopIndicator = lineMap.ContainsKey("IU2MFP") ? lineMap["IU2MFP"].Trim() : "";
            this.privateFareIndicator = lineMap.ContainsKey("IU2PVT") ? lineMap["IU2PVT"].Trim() : "";
            this.spare3 = lineMap.ContainsKey("IU2SPR") ? lineMap["IU2SPR"].Trim() : "";
            this.ccExpDate = lineMap.ContainsKey("IU2EXP") ? lineMap["IU2EXP"].Trim() : "";
            this.ccExtPaymentMonths = lineMap.ContainsKey("IU2EXT") ? lineMap["IU2EXT"].Trim() : "";
            this.m4RecordCount = lineMap.ContainsKey("IU2M4C") ? lineMap["IU2M4C"].Trim() : "";
            this.m6RecordCount = lineMap.ContainsKey("IU2M6C") ? lineMap["IU2M6C"].Trim() : "";
            this.validatingCarrierCode = lineMap.ContainsKey("IU2VAL") ? lineMap["IU2VAL"].Trim() : "";
            this.ticketNumber = lineMap.ContainsKey("IU2TNO") ? lineMap["IU2TNO"].Trim() : "";
            this.conjunctionTktCount = lineMap.ContainsKey("IU2DNO") ? lineMap["IU2DNO"].Trim() : "";
        }

        public M2AccessoryInfo GetAccessoryInfo()
        {
            return new M2AccessoryInfo()
            {
                ItineraryType = this.internationalItinIndicator == "X" ? ItineraryType.International.ToString() : ItineraryType.Domestic.ToString(),
                TicketingCity = this.ticketingCity,
                M4RecordsCount = int.Parse(this.m4RecordCount),
                M6RecordCount = int.Parse(this.m6RecordCount),
            };
        }

        public M2EconomicInfo GetEconomicInfo()
        {
            M2EconomicInfo eInfo =  new M2EconomicInfo()
            {
                BaseFare = new PriceData()
                {
                    Sign = this.fareSign == "-" ? "-" : "+",
                    Currency = this.fareCurrencyCode,
                    Amount = this.fareAmount,
                },
                Tax1 = new Tax()
                {
                    TaxCode = this.tax1Id,
                    Price = new PriceData()
                    {
                        Sign = this.tax1Sign == "-" ? "-" : "+",
                        Currency = this.fareCurrencyCode,
                        Amount = this.tax1Amount
                    }
                },
                Tax2 = new Tax()
                {
                    TaxCode = this.tax2Id,
                    Price = new PriceData()
                    {
                        Sign = this.tax2Sign == "-" ? "-" : "+",
                        Currency = this.fareCurrencyCode,
                        Amount = this.tax2Amount
                    }
                },
                Tax3 = new Tax()
                {
                    TaxCode = this.tax3Id,
                    Price = new PriceData()
                    {
                        Sign = this.tax3Sign == "-" ? "-" : "+",
                        Currency = this.fareCurrencyCode,
                        Amount = this.tax3Amount
                    }
                },
                TotalFare = new PriceData()
                {
                    Sign = this.totalFareSign == "-" ? "-" : "+",
                    Currency = this.totalFareCurrCode,
                    Amount = this.totalFareAmount
                },
                CommissionPercentage = this.commissionPercentage,
                Commission = new PriceData()
                {
                    Sign = this.commissionSign == "-" ? "-" : "+",
                    Currency = this.totalFareCurrCode,
                    Amount = this.commissionAmt
                },
                NetAmount = new PriceData()
                {
                    Sign = this.netAmountSign == "-" ? "-" : "+",
                    Currency = this.totalFareCurrCode,
                    Amount = this.netAmt
                },
                TariffType = this.foreignTarifFlag == "F" ? TariffType.Foreign : TariffType.Domestic,
                TravelAgencyTax = new PriceData()
                {
                    Sign = "+",
                    Currency = this.totalFareCurrCode,
                    Amount = this.travelAgencyTax
                },
            };
            if(String.IsNullOrEmpty(eInfo.Tax1.Price.Amount) && !String.IsNullOrEmpty(eInfo.TravelAgencyTax.Amount))
            {
                eInfo.Tax1.Price = eInfo.TravelAgencyTax;
            }

            return eInfo;
        }
    }

    public class M2EconomicInfo
    {
        public PriceData BaseFare { get; set; }
        public Tax Tax1 { get; set; }
        public Tax Tax2 { get; set; }
        public Tax Tax3 { get; set; }
        public PriceData TotalFare { get; set; }
        public string CommissionPercentage { get; set; }
        public PriceData Commission { get; set; }
        public PriceData NetAmount { get; set; }
        public TariffType TariffType { get; set; }
        public PriceData TravelAgencyTax { get; set; }

        public Tax getTotalTax()
        {
            Tax t = new Tax();
            t.TaxCode = "";
            t.Price = TravelAgencyTax;
            return t;
        }
    }

    

    public class M2AccessoryInfo
    {
        public string ItineraryType { get; set; }
        public string TicketingCity { get; set; }
        public int M4RecordsCount { get; set; }
        public int M6RecordCount { get; set; }
    }

    public class PriceData
    {
        public string Sign { get; set; }
        public string Currency { get; set; }
        private string _Amount;
        public string Amount
        {
            get { return _Amount; }
            set
            {
                value = value.Trim();
                if (value.StartsWith("."))
                {
                    value = $"0{value}";
                }
                _Amount = value;
            }
        }
    }

    public class Tax
    {
        public string TaxCode { get; set; }
        public PriceData Price { get; set; }
    }

    public enum ItineraryType
    {
        International,
        Domestic,
        Transborder,
        Undefined
    }

    public enum TariffType
    {
        Foreign,
        Domestic
    }
}
