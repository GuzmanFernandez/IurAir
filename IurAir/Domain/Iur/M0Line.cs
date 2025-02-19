using IurAir.Domain.Iur;
using IurAir.Domain.Iur.Sections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace IurAir.Domain.Iur
{
    public class M0Line : IurLine
    {
        public M0Line(string rawLine) : base(rawLine, "M0", 231, M0S.M0Sections())
        {

        }

        public override IurObject getObject()
        {
            return new M0Object(this);
        }
    }

    public class M0Object : IurObject
    {

        public M0Object(M0Line line)
        {
            Dictionary<string, string> lineMap = line.parseLine();
            this.systemOriginationCode = lineMap.ContainsKey("HEAD") ? lineMap["HEAD"].Trim() : "";
            this.transmissionDay = lineMap.ContainsKey("TRDAY") ? lineMap["TRDAY"].Trim() : "";
            this.transmissionMonth = lineMap.ContainsKey("TRMON") ? lineMap["TRMON"].Trim() : "";
            this.transmissionTime = lineMap.ContainsKey("TRTIM") ? lineMap["TRTIM"].Trim() : "";
            this.recordIdentifier = lineMap.ContainsKey("IU0MID") ? lineMap["IU0MID"].Trim() : "";
            this.transactionType = lineMap.ContainsKey("IU0TYP") ? lineMap["IU0TYP"].Trim() : "";
            this.interfaceVersion = lineMap.ContainsKey("IU0VER") ? lineMap["IU0VER"].Trim() : "";
            this.customerNumber = lineMap.ContainsKey("IU0DKN") ? lineMap["IU0DKN"].Trim() : "";
            this.spareSpace = lineMap.ContainsKey("IU0CTJ") ? lineMap["IU0CTJ"].Trim() : "";
            this.previouslyInvoicedIndicator = lineMap.ContainsKey("IU0PIV") ? lineMap["IU0PIV"].Trim() : "";
            this.pnrRecordControlCheck = lineMap.ContainsKey("IU0RRC") ? lineMap["IU0RRC"].Trim() : "";
            this.queueType = lineMap.ContainsKey("IU0QUE") ? lineMap["IU0QUE"].Trim() : "";
            this.spareSpace2 = lineMap.ContainsKey("IU0CDK") ? lineMap["IU0CDK"].Trim() : "";
            this.invoiceNumber = lineMap.ContainsKey("IU0IVN") ? lineMap["IU0IVN"].Trim() : "";
            this.agencyArcIataNr = lineMap.ContainsKey("IU0ATC") ? lineMap["IU0ATC"].Trim() : "";
            this.pnrLocator = lineMap.ContainsKey("IU0PNR") ? lineMap["IU0PNR"].Trim() : "";
            this.linkPnrLocator = lineMap.ContainsKey("IU0PNL") ? lineMap["IU0PNL"].Trim() : "";
            this.subscriberInterfaceOptionIndicator = lineMap.ContainsKey("IU0OPT") ? lineMap["IU0OPT"].Trim() : "";
            this.phoneIndicator = lineMap.ContainsKey("IU0F00") ? lineMap["IU0F00"].Trim() : "";
            this.entitlements = lineMap.ContainsKey("IU0F01") ? lineMap["IU0F01"].Trim() : "";
            this.fareCalculations = lineMap.ContainsKey("IU0F02") ? lineMap["IU0F02"].Trim() : "";
            this.invoiceItineraryRemarks = lineMap.ContainsKey("IU0F03") ? lineMap["IU0F03"].Trim() : "";
            this.interfaceRemarks = lineMap.ContainsKey("IU0F04") ? lineMap["IU0F04"].Trim() : "";
            this.statuteMiles = lineMap.ContainsKey("IU0F05") ? lineMap["IU0F05"].Trim() : "";
            this.itineraryToPosQ = lineMap.ContainsKey("IU0F06") ? lineMap["IU0F06"].Trim() : "";
            this.customerProfitabilityRecord = lineMap.ContainsKey("IU0F07") ? lineMap["IU0F07"].Trim() : "";
            this.miscChargeOrderRecords = lineMap.ContainsKey("IU0F08") ? lineMap["IU0F08"].Trim() : "";
            this.prePaidTicketRecords = lineMap.ContainsKey("IU0F09") ? lineMap["IU0F09"].Trim() : "";
            this.tourOrderRecords = lineMap.ContainsKey("IU0F0A") ? lineMap["IU0F0A"].Trim() : "";
            this.segmentAssociatedRemarksRecords = lineMap.ContainsKey("IU0F0B") ? lineMap["IU0F0B"].Trim() : "";
            this.spareSpace3 = lineMap.ContainsKey("IU0F0C") ? lineMap["IU0F0C"].Trim() : "";
            this.nameAssociatedRemarks = lineMap.ContainsKey("IU0F0D") ? lineMap["IU0F0D"].Trim() : "";
            this.amountMovedToMX = lineMap.ContainsKey("IU0F0E") ? lineMap["IU0F0E"].Trim() : "";
            this.spareSpace4 = lineMap.ContainsKey("IU0F0F") ? lineMap["IU0F0F"].Trim() : "";
            this.atbIndicators = lineMap.ContainsKey("IU0ATB") ? lineMap["IU0ATB"].Trim() : "";
            this.duplicateInterfaceIndicator = lineMap.ContainsKey("IU0DUP") ? lineMap["IU0DUP"].Trim() : "";
            this.pseudoCityCode = lineMap.ContainsKey("IU0PCC") ? lineMap["IU0PCC"].Trim() : "";
            this.bookingAgentDutyCode = lineMap.ContainsKey("IU0IDC") ? lineMap["IU0IDC"].Trim() : "";
            this.bookingAgentSine = lineMap.ContainsKey("IU0IAG") ? lineMap["IU0IAG"].Trim() : "";
            this.lnIata = lineMap.ContainsKey("IU0LIN") ? lineMap["IU0LIN"].Trim() : "";
            this.remotePrinter = lineMap.ContainsKey("IU0RPR") ? lineMap["IU0RPR"].Trim() : "";
            this.pnrCreationDate = lineMap.ContainsKey("IU0PDT") ? lineMap["IU0PDT"].Trim() : "";
            this.pnrCreationTime = lineMap.ContainsKey("IU0TIM") ? lineMap["IU0TIM"].Trim() : "";
            this.invoicingAgencyCityCode = lineMap.ContainsKey("IU0IS4") ? lineMap["IU0IS4"].Trim() : "";
            this.invoicingAgentDutyCode = lineMap.ContainsKey("IU0IS1") ? lineMap["IU0IS1"].Trim() : "";
            this.invoicingAgentCode = lineMap.ContainsKey("IU0IS3") ? lineMap["IU0IS3"].Trim() : "";
            this.spare4 = lineMap.ContainsKey("IU0TCO") ? lineMap["IU0TCO"].Trim() : "";
            this.branchId = lineMap.ContainsKey("IU0IDB") ? lineMap["IU0IDB"].Trim() : "";
            this.departureDate = lineMap.ContainsKey("IU0DEP") ? lineMap["IU0DEP"].Trim() : "";
            this.originCityCode = lineMap.ContainsKey("IU0ORG") ? lineMap["IU0ORG"].Trim() : "";
            this.originCityName = lineMap.ContainsKey("IU0ONM") ? lineMap["IU0ONM"].Trim() : "";
            this.destinationCityCode = lineMap.ContainsKey("IU0DST") ? lineMap["IU0DST"].Trim() : "";
            this.destinationCityName = lineMap.ContainsKey("IU0DNM") ? lineMap["IU0DNM"].Trim() : "";
            this.passengerNumber = lineMap.ContainsKey("IU0NM1") ? lineMap["IU0NM1"].Trim() : "";
            this.ticketedPassengerNumber = lineMap.ContainsKey("IU0NM2") ? lineMap["IU0NM2"].Trim() : "";
            this.itinerarySegmentNumber = lineMap.ContainsKey("IU0NM3") ? lineMap["IU0NM3"].Trim() : "";
            this.entitlementNumber = lineMap.ContainsKey("IU0NM4") ? lineMap["IU0NM4"].Trim() : "";
            this.accountingLinesNumber = lineMap.ContainsKey("IU0NM5") ? lineMap["IU0NM5"].Trim() : "";
            this.fareCalcLinesNumber = lineMap.ContainsKey("IU0NM6") ? lineMap["IU0NM6"].Trim() : "";
            this.itineraryRemarksNumber = lineMap.ContainsKey("IU0NM7") ? lineMap["IU0NM7"].Trim() : "";
            this.invoiceRemarkLinesNumber = lineMap.ContainsKey("IU0NM8") ? lineMap["IU0NM8"].Trim() : "";
            this.interfaceRemarksNumber = lineMap.ContainsKey("IU0NM9") ? lineMap["IU0NM9"].Trim() : "";
            this.customerProfitabilityItemsNumber = lineMap.ContainsKey("IU0NMA") ? lineMap["IU0NMA"].Trim() : "";
            this.spare5 = lineMap.ContainsKey("IU0ADC") ? lineMap["IU0ADC"].Trim() : "";
            this.phoneFieldsNumber = lineMap.ContainsKey("IU0PHC") ? lineMap["IU0PHC"].Trim() : "";
            this.iURTA = lineMap.ContainsKey("IU0TYM") ? lineMap["IU0TYM"].Trim() : "";
            this.recordCreationDate = lineMap.ContainsKey("IU0MDT") ? lineMap["IU0MDT"].Trim() : "";
        }
        [DefaultValue("")]
        public string systemOriginationCode { get; set; }
        [DefaultValue("")]
        public string transmissionDay { get; set; }
        [DefaultValue("")]
        public string transmissionMonth { get; set; }
        [DefaultValue("")]
        public string transmissionTime { get; set; }
        [DefaultValue("")]
        public string recordIdentifier { get; set; }
        [DefaultValue("")]
        public string transactionType { get; set; }
        [DefaultValue("")]
        public string interfaceVersion { get; set; }
        [DefaultValue("")]
        public string customerNumber { get; set; }
        [DefaultValue("")]
        public string spareSpace { get; set; }
        [DefaultValue("")]
        public string previouslyInvoicedIndicator { get; set; }
        [DefaultValue("")]
        public string pnrRecordControlCheck { get; set; }
        [DefaultValue("")]
        public string queueType { get; set; }
        [DefaultValue("")]
        public string spareSpace2 { get; set; }
        [DefaultValue("")]
        public string invoiceNumber { get; set; }
        [DefaultValue("")]
        public string agencyArcIataNr { get; set; }
        [DefaultValue("")]
        public string pnrLocator { get; set; }
        [DefaultValue("")]
        public string linkPnrLocator { get; set; }
        [DefaultValue("")]
        public string subscriberInterfaceOptionIndicator { get; set; }
        [DefaultValue("")]
        public string phoneIndicator { get; set; }
        [DefaultValue("")]
        public string entitlements { get; set; }
        [DefaultValue("")]
        public string fareCalculations { get; set; }
        [DefaultValue("")]
        public string invoiceItineraryRemarks { get; set; }
        [DefaultValue("")]
        public string interfaceRemarks { get; set; }
        [DefaultValue("")]
        public string statuteMiles { get; set; }
        [DefaultValue("")]
        public string itineraryToPosQ { get; set; }
        [DefaultValue("")]
        public string customerProfitabilityRecord { get; set; }
        [DefaultValue("")]
        public string miscChargeOrderRecords { get; set; }
        [DefaultValue("")]
        public string prePaidTicketRecords { get; set; }
        [DefaultValue("")]
        public string tourOrderRecords { get; set; }
        [DefaultValue("")]
        public string segmentAssociatedRemarksRecords { get; set; }
        [DefaultValue("")]
        public string spareSpace3 { get; set; }
        [DefaultValue("")]
        public string nameAssociatedRemarks { get; set; }
        [DefaultValue("")]
        public string amountMovedToMX { get; set; }
        [DefaultValue("")]
        public string spareSpace4 { get; set; }
        [DefaultValue("")]
        public string atbIndicators { get; set; }
        [DefaultValue("")]
        public string duplicateInterfaceIndicator { get; set; }
        [DefaultValue("")]
        public string pseudoCityCode { get; set; }
        [DefaultValue("")]
        public string bookingAgentDutyCode { get; set; }
        [DefaultValue("")]
        public string bookingAgentSine { get; set; }
        [DefaultValue("")]
        public string lnIata { get; set; }
        [DefaultValue("")]
        public string remotePrinter { get; set; }
        [DefaultValue("")]
        public string pnrCreationDate { get; set; }
        [DefaultValue("")]
        public string pnrCreationTime { get; set; }
        [DefaultValue("")]
        public string invoicingAgencyCityCode { get; set; }
        [DefaultValue("")]
        public string invoicingAgentDutyCode { get; set; }
        [DefaultValue("")]
        public string invoicingAgentCode { get; set; }
        [DefaultValue("")]
        public string spare4 { get; set; }
        [DefaultValue("")]
        public string branchId { get; set; }
        [DefaultValue("")]
        public string departureDate { get; set; }
        [DefaultValue("")]
        public string originCityCode { get; set; }
        [DefaultValue("")]
        public string originCityName { get; set; }
        [DefaultValue("")]
        public string destinationCityCode { get; set; }
        [DefaultValue("")]
        public string destinationCityName { get; set; }
        [DefaultValue("")]
        public string passengerNumber { get; set; }
        [DefaultValue("")]
        public string ticketedPassengerNumber { get; set; }
        [DefaultValue("")]
        public string itinerarySegmentNumber { get; set; }
        [DefaultValue("")]
        public string entitlementNumber { get; set; }
        [DefaultValue("")]
        public string accountingLinesNumber { get; set; }
        [DefaultValue("")]
        public string fareCalcLinesNumber { get; set; }
        [DefaultValue("")]
        public string itineraryRemarksNumber { get; set; }
        [DefaultValue("")]
        public string invoiceRemarkLinesNumber { get; set; }
        [DefaultValue("")]
        public string interfaceRemarksNumber { get; set; }
        [DefaultValue("")]
        public string customerProfitabilityItemsNumber { get; set; }
        [DefaultValue("")]
        public string spare5 { get; set; }
        [DefaultValue("")]
        public string phoneFieldsNumber { get; set; }
        [DefaultValue("")]
        public string iURTA { get; set; }
        [DefaultValue("")]
        public string recordCreationDate { get; set; }
    }

    public class M0VoidLine : IurLine
    {
        public M0VoidLine(string rawLine) : base(rawLine, "M0", 67, M0S.M0VoidSections()) { }

        public override IurObject getObject()
        {
            return new M0VoidObject(this);
        }
    }

    public class M0VoidObject : IurObject
    {
        [DefaultValue("")]
        public string systemOriginationCode;
        [DefaultValue("")]
        public string transmissionDay;
        [DefaultValue("")]
        public string transmissionMonth;
        [DefaultValue("")]
        public string transmissionTime;
        [DefaultValue("")]
        public string recordIdentifier;
        [DefaultValue("")]
        public string transactionType;
        [DefaultValue("")]
        public string interfaceVersion;
        [DefaultValue("")]
        public string customerNumber;
        [DefaultValue("")]
        public string ticketNumber;
        [DefaultValue("")]
        public string numberOfTickets;
        [DefaultValue("")]
        public string voidPnrLocator;
        [DefaultValue("")]
        public string duplicateVoidIndicator;
        [DefaultValue("")]
        public string voidAuthCodes;

        public M0VoidObject(M0VoidLine line)
        {
            Dictionary<string, string> lineMap = line.parseLine();

            this.systemOriginationCode = lineMap.ContainsKey("HEAD") ? lineMap["HEAD"].Trim() : "";
            this.transmissionDay = lineMap.ContainsKey("TRDAY") ? lineMap["TRDAY"].Trim() : "";
            this.transmissionMonth = lineMap.ContainsKey("TRMON") ? lineMap["TRMON"].Trim() : "";
            this.transmissionTime = lineMap.ContainsKey("TRTIM") ? lineMap["TRTIM"].Trim() : "";
            this.recordIdentifier = lineMap.ContainsKey("IU0MID") ? lineMap["IU0MID"].Trim() : "";
            this.transactionType = lineMap.ContainsKey("IU0TYP") ? lineMap["IU0TYP"].Trim() : "";
            this.interfaceVersion = lineMap.ContainsKey("IU0VER") ? lineMap["IU0VER"].Trim() : "";
            this.customerNumber = lineMap.ContainsKey("IU0DKN") ? lineMap["IU0DKN"].Trim() : "";
            this.ticketNumber = lineMap.ContainsKey("IU0TKI") ? lineMap["IU0TKI"].Trim() : "";
            this.numberOfTickets = lineMap.ContainsKey("IU0CNT") ? lineMap["IU0CNT"].Trim() : "";
            this.voidPnrLocator = lineMap.ContainsKey("IU0PR1") ? lineMap["IU0PR1"].Trim() : "";
            this.duplicateVoidIndicator = lineMap.ContainsKey("IU0DUP") ? lineMap["IU0DUP"].Trim() : "";
            this.voidAuthCodes = lineMap.ContainsKey("IU0SAC") ? lineMap["IU0SAC"].Trim() : "";
        }
    }
}
