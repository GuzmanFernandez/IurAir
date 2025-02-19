using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public abstract class M2S
    {

        public static List<LineSection> M2Sections()
        {
            return new List<LineSection>() {
                InterfaceNameItemNr,
                PassengerType,
                TcnNumber,
                InternationalItinIndicator,
                FormOfPayment,
                TicketIndicator,
                CombinedTicketAndBp,
                TicketOnly,
                BpOnly,
                RemotePrintAuditorCoupon,
                PassengerReceiptOnly,
                RemPrintAgentsCoupon,
                RemPrintCreditCard,
                InvoiceDocument,
                MiniItinerary,
                MagneticEncoding,
                UsContractWholesaleBulk,
                BspTicketSuppression,
                FareSign,
                FareCurrencyCode,
                FareAmount,
                Tax1Sign,
                Tax1Amount,
                Tax1Id,
                Tax2Sign,
                Tax2Amount,
                Tax2Id,
                Tax3Sign,
                Tax3Amount,
                Tax3Id,
                TotalFareSign,
                TotalFareCurrCode,
                TotalFareAmount,
                CancellationPenaltyAmount,
                CommissionOnPenalty,
                ObIndicator,
                SpareSpace,
                EquivalentPaidSign,
                EquivalentFareCurrCode,
                EquivalentFare,
                CommissionPercentage,
                CommissionSign,
                CommissionAmt,
                NetAmountSign,
                NetAmt,
                CanadianTickDesCode,
                CanadianTickTravelCode,
                ForeignTarifFlag,
                TravelAgencyTax,
                InclusiveTourNr,
                TicketingCity,
                PrintMiniItinerary,
                CreditCardInfoPrintSuppression,
                CreditCardAuthSourceCode,
                FareAgentPcc,
                FareAgentDutyCode,
                FareAgentSine,
                PrintAgentPcc,
                PrintAgentDutyCode,
                PrintAgentSine,
                Spare2,
                CreditCardAuth,
                NewFopIndicator,
                PrivateFareIndicator,
                Spare3,
                CcExpDate,
                CcExtPaymentMonths,
                M4RecordCount,
                M6RecordCount,
                ValidatingCarrierCode,
                TicketNumber,
                ConjunctionTktCount,
            };
        }
        public static LineSection InterfaceNameItemNr = new LineSection() { start = 3, length = 2, iurLabel = "IU2PNO", hrLabel = "InterfaceNameItemNr" };
        public static LineSection PassengerType = new LineSection() { start = 5, length = 3, iurLabel = "IU2PTY", hrLabel = "PassengerType" };
        public static LineSection TcnNumber = new LineSection() { start = 8, length = 11, iurLabel = "IU2TCN", hrLabel = "TransactionControlNr" };
        public static LineSection InternationalItinIndicator = new LineSection() { start = 19, length = 1, iurLabel = "IU2INT", hrLabel = "InternationalItineraryIndicator" };
        public static LineSection FormOfPayment = new LineSection() { start = 20, length = 1, iurLabel = "IU2SSI", hrLabel = "FormOfPayment" };
        public static LineSection TicketIndicator = new LineSection() { start = 21, length = 1, iurLabel = "IU2IND", hrLabel = "TicketIndicator" };
        public static LineSection CombinedTicketAndBp = new LineSection() { start = 22, length = 1, iurLabel = "IU2AP1", hrLabel = "CombinedTicketAndBp" };
        public static LineSection TicketOnly = new LineSection() { start = 23, length = 1, iurLabel = "IU2AP2", hrLabel = "TicketOnly" };
        public static LineSection BpOnly = new LineSection() { start = 24, length = 1, iurLabel = "IU2AP3", hrLabel = "BoardingPassOnly" };
        public static LineSection RemotePrintAuditorCoupon = new LineSection() { start = 25, length = 1, iurLabel = "IU2AP4", hrLabel = "RemPrintAudCoupon" };
        public static LineSection PassengerReceiptOnly = new LineSection() { start = 26, length = 1, iurLabel = "IU2AP5", hrLabel = "PassengerReceiptOnly" };
        public static LineSection RemPrintAgentsCoupon = new LineSection() { start = 27, length = 1, iurLabel = "IU2AP6", hrLabel = "RemPrintAgCoupon" };
        public static LineSection RemPrintCreditCard = new LineSection() { start = 28, length = 1, iurLabel = "IU2AP7", hrLabel = "RemPrintCredCard" };
        public static LineSection InvoiceDocument = new LineSection() { start = 29, length = 1, iurLabel = "IU2AP8", hrLabel = "InvoiceDoc" };
        public static LineSection MiniItinerary = new LineSection() { start = 30, length = 1, iurLabel = "IU2AP9", hrLabel = "MiniItin" };
        public static LineSection MagneticEncoding = new LineSection() { start = 31, length = 1, iurLabel = "IU2APA", hrLabel = "MagneticEncoding" };
        public static LineSection UsContractWholesaleBulk = new LineSection() { start = 32, length = 1, iurLabel = "IU2APB", hrLabel = "UsContrWholesaleBulk" };
        public static LineSection BspTicketSuppression = new LineSection() { start = 33, length = 1, iurLabel = "IU2APZ", hrLabel = "BspTktSuppression" };
        public static LineSection FareSign = new LineSection() { start = 34, length = 1, iurLabel = "IU2FSN", hrLabel = "FareSign" };
        public static LineSection FareCurrencyCode = new LineSection() { start = 35, length = 3, iurLabel = "IU2FCC", hrLabel = "FareCurrencyCode" };
        public static LineSection FareAmount = new LineSection() { start = 38, length = 8, iurLabel = "IU2FAR", hrLabel = "FareAmt" };
        public static LineSection Tax1Sign = new LineSection() { start = 46, length = 1, iurLabel = "IU2T1S", hrLabel = "Tax1Sign" };
        public static LineSection Tax1Amount = new LineSection() { start = 47, length = 7, iurLabel = "IU2TX1", hrLabel = "Tax1Sign" };
        public static LineSection Tax1Id = new LineSection() { start = 54, length = 2, iurLabel = "IU2ID1", hrLabel = "Tax1Id" };
        public static LineSection Tax2Sign = new LineSection() { start = 56, length = 1, iurLabel = "IU2T2S", hrLabel = "Tax2Sign" };
        public static LineSection Tax2Amount = new LineSection() { start = 57, length = 7, iurLabel = "IU2TX2", hrLabel = "Tax2Sign" };
        public static LineSection Tax2Id = new LineSection() { start = 64, length = 2, iurLabel = "IU2ID2", hrLabel = "Tax2Id" };
        public static LineSection Tax3Sign = new LineSection() { start = 66, length = 1, iurLabel = "IU2T3S", hrLabel = "Tax3Sign" };
        public static LineSection Tax3Amount = new LineSection() { start = 67, length = 7, iurLabel = "IU2TX3", hrLabel = "Tax3Sign" };
        public static LineSection Tax3Id = new LineSection() { start = 74, length = 2, iurLabel = "IU2ID3", hrLabel = "Tax3Id" };
        public static LineSection TotalFareSign = new LineSection() { start = 76, length = 1, iurLabel = "IU2TFS", hrLabel = "TotalFareSign" };
        public static LineSection TotalFareCurrCode = new LineSection() { start = 77, length = 3, iurLabel = "IU2TFC", hrLabel = "TotalFareCurrCode" };
        public static LineSection TotalFareAmount = new LineSection() { start = 80, length = 8, iurLabel = "IU2TFR", hrLabel = "TotalFareAmount" };
        public static LineSection CancellationPenaltyAmount = new LineSection() { start = 88, length = 11, iurLabel = "IU2PEN", hrLabel = "CancellationPenaltyAmt" };
        public static LineSection CommissionOnPenalty = new LineSection() { start = 99, length = 11, iurLabel = "IU2KXP", hrLabel = "CommissionOnPenalty" };
        public static LineSection ObIndicator = new LineSection() { start = 110, length = 1, iurLabel = "IU2OBF", hrLabel = "ObIndicator" };
        public static LineSection SpareSpace = new LineSection() { start = 111, length = 5, iurLabel = "IU2SPX", hrLabel = "SpareSpace" };
        public static LineSection EquivalentPaidSign = new LineSection() { start = 116, length = 1, iurLabel = "IU2EPS", hrLabel = "EquivalentPaidSign" };
        public static LineSection EquivalentFareCurrCode = new LineSection() { start = 117, length = 3, iurLabel = "IU2EFC", hrLabel = "EquivalentFareCurrCode" };
        public static LineSection EquivalentFare = new LineSection() { start = 120, length = 8, iurLabel = "IU2EFR", hrLabel = "EquivalentFare" };
        public static LineSection CommissionPercentage = new LineSection() { start = 128, length = 8, iurLabel = "IU2PCT", hrLabel = "CommissionPercentage" };
        public static LineSection CommissionSign = new LineSection() { start = 136, length = 1, iurLabel = "IU2CSN", hrLabel = "CommissionSign" };
        public static LineSection CommissionAmt = new LineSection() { start = 137, length = 8, iurLabel = "IU2COM", hrLabel = "CommissionAmt" };
        public static LineSection NetAmountSign = new LineSection() { start = 145, length = 1, iurLabel = "IU2NAS", hrLabel = "NetAmtSign" };
        public static LineSection NetAmt = new LineSection() { start = 146, length = 8, iurLabel = "IU2NET", hrLabel = "NetAmt" };
        public static LineSection CanadianTickDesCode = new LineSection() { start = 154, length = 1, iurLabel = "IU2CDC", hrLabel = "CanadianTicketDesCode" };
        public static LineSection CanadianTickTravelCode = new LineSection() { start = 155, length = 1, iurLabel = "IU2CTT", hrLabel = "CanadianTicketTravCode" };
        public static LineSection ForeignTarifFlag = new LineSection() { start = 156, length = 1, iurLabel = "IU2FTF", hrLabel = "ForeignTarifFlag" };
        public static LineSection TravelAgencyTax = new LineSection() { start = 157, length = 8, iurLabel = "IU2TAT", hrLabel = "TravelAgTax" };
        public static LineSection InclusiveTourNr = new LineSection() { start = 165, length = 15, iurLabel = "IU2TUR", hrLabel = "InclusiveTourNr" };
        public static LineSection TicketingCity = new LineSection() { start = 180, length = 4, iurLabel = "IU2TPC", hrLabel = "TicketingCity" };
        public static LineSection PrintMiniItinerary = new LineSection() { start = 184, length = 1, iurLabel = "IU2MIT", hrLabel = "PrintMiniItin" };
        public static LineSection CreditCardInfoPrintSuppression = new LineSection() { start = 185, length = 1, iurLabel = "IU2CCB", hrLabel = "CreditCardInfoPrintSuppres" };
        public static LineSection CreditCardAuthSourceCode = new LineSection() { start = 186, length = 1, iurLabel = "IU2APC", hrLabel = "CreditCardAuthSourceCode" };
        public static LineSection FareAgentPcc = new LineSection() { start = 187, length = 5, iurLabel = "IU2SN4", hrLabel = "FareAgentPcc" };
        public static LineSection FareAgentDutyCode = new LineSection() { start = 192, length = 2, iurLabel = "IU2SN3", hrLabel = "FareAgentDutyCode" };
        public static LineSection FareAgentSine = new LineSection() { start = 194, length = 3, iurLabel = "IU2SN2", hrLabel = "FareAgentSine" };
        public static LineSection PrintAgentPcc = new LineSection() { start = 197, length = 5, iurLabel = "IU2PN4", hrLabel = "PrintAgentPcc" };
        public static LineSection PrintAgentDutyCode = new LineSection() { start = 202, length = 2, iurLabel = "IU2PN3", hrLabel = "PrintAgentDutyCode" };
        public static LineSection PrintAgentSine = new LineSection() { start = 204, length = 3, iurLabel = "IU2PN2", hrLabel = "PrintAgentSine" };
        public static LineSection Spare2 = new LineSection() { start = 207, length = 1, iurLabel = "IU2AVI", hrLabel = "Sparespace2" };
        public static LineSection CreditCardAuth = new LineSection() { start = 208, length = 9, iurLabel = "IU2ATH", hrLabel = "CreditCardAuth" };
        public static LineSection NewFopIndicator = new LineSection() { start = 217, length = 1, iurLabel = "IU2MFP", hrLabel = "NewFopIndicator" };
        public static LineSection PrivateFareIndicator = new LineSection() { start = 218, length = 1, iurLabel = "IU2PVT", hrLabel = "PrivateFareIndicator" };
        public static LineSection Spare3 = new LineSection() { start = 219, length = 3, iurLabel = "IU2SPR", hrLabel = "Spare3" };
        public static LineSection CcExpDate = new LineSection() { start = 222, length = 4, iurLabel = "IU2EXP", hrLabel = "CcExpDate" };
        public static LineSection CcExtPaymentMonths = new LineSection() { start = 226, length = 3, iurLabel = "IU2EXT", hrLabel = "CcExtPayMonths" };
        public static LineSection M4RecordCount = new LineSection() { start = 228, length = 2, iurLabel = "IU2M4C", hrLabel = "M4RecCount" };
        public static LineSection M6RecordCount = new LineSection() { start = 230, length = 2, iurLabel = "IU2M6C", hrLabel = "M6RecCount" };
        public static LineSection ValidatingCarrierCode = new LineSection() { start = 232, length = 2, iurLabel = "IU2VAL", hrLabel = "ValidatingCarrierCode" };
        public static LineSection TicketNumber = new LineSection() { start = 234, length = 10, iurLabel = "IU2TNO", hrLabel = "TicketNumber" };
        public static LineSection ConjunctionTktCount = new LineSection() { start = 244, length = 1, iurLabel = "IU2DNO", hrLabel = "ConjunctionTicketNr" };
    }
}
