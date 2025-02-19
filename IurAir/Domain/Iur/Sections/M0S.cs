using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public static class M0S
    {
        public static List<LineSection> M0Sections()
        {
            return new List<LineSection>()
            {
                SystemOriginationCode,
                TransmissionDay,
                TransmissionMonth,
                TransmissionTime,
                RecordIdentifier,
                TransactionType,
                InterfaceVersion,
                CustomerNumber,
                SpareSpace,
                PreviouslyInvoicedIndicator,
                PnrRecordControlCheck,
                QueueType,
                SpareSpace2,
                InvoiceNumber,
                AgencyArcIataNr,
                PnrLocator,
                LinkPnrLocator,
                SubscriberInterfaceOptionIndicator,
                PhoneIndicator,
                Entitlements,
                FareCalculations,
                InvoiceItineraryRemarks,
                InterfaceRemarks,
                StatuteMiles,
                ItineraryToPosQ,
                CustomerProfitabilityRecord,
                MiscChargeOrderRecords,
                PrePaidTicketRecords,
                TourOrderRecords,
                SegmentAssociatedRemarksRecords,
                SpareSpace3,
                NameAssociatedRemarks,
                AmountMovedToMX,
                SpareSpace4,
                AtbIndicators,
                DuplicateInterfaceIndicator,
                PseudoCityCode,
                BookingAgentDutyCode,
                BookingAgentSine,
                LnIata,
                RemotePrinter,
                PnrCreationDate,
                PnrCreationTime,
                InvoicingAgencyCityCode,
                InvoicingAgentDutyCode,
                InvoicingAgentCode,
                Spare4,
                BranchId,
                DepartureDate,
                OriginCityCode,
                OriginCityName,
                DestinationCityCode,
                DestinationCityName,
                PassengerNumber,
                TicketedPassengerNumber,
                ItinerarySegmentNumber,
                EntitlementNumber,
                AccountingLinesNumber,
                FareCalcLinesNumber,
                ItineraryRemarksNumber,
                InvoiceRemarkLinesNumber,
                InterfaceRemarksNumber,
                CustomerProfitabilityItemsNumber,
                Spare5,
                PhoneFieldsNumber,
                IURTA,
                RecordCreationDate,
            };
        }

        public static List<LineSection> M0VoidSections()
        {
            return new List<LineSection>()
            {
                SystemOriginationCode,
                TransmissionDay,
                TransmissionMonth,
                TransmissionTime,
                RecordIdentifier,
                TransactionType,
                InterfaceVersion,
                CustomerNumber,
                TicketNumber,
                NumberOfTickets,
                VoidPnrLocator,
                DuplicateVoidIndicator,
                VoidAuthCodes
            };
        }
        //AA08APR1113M0117                    003979696 18811 6  BTNHFS        611111110000001001 AK47 * LM 62A6FC            08APR 1047AK47 9 QC   00020MAYFCOROME FIUMICINO   FCOROME FIUMICINO   001001002002001001000000000000  01 181308APR
        public static LineSection SystemOriginationCode = new LineSection() { start = 1, length = 2, iurLabel = "HEAD", hrLabel = "OriginationCode" };
        public static LineSection TransmissionDay = new LineSection() { start = 3, length = 2, iurLabel = "TRDAY", hrLabel = "TransmissionDay" };
        public static LineSection TransmissionMonth = new LineSection() { start = 5, length = 3, iurLabel = "TRMON", hrLabel = "TransmissionMonth" };
        public static LineSection TransmissionTime = new LineSection() { start = 8, length = 4, iurLabel = "TRTIM", hrLabel = "TransmissionTime" };
        public static LineSection RecordIdentifier = new LineSection() { start = 12, length = 2, iurLabel = "IU0MID", hrLabel = "LineIdentifier" };
        public static LineSection TransactionType = new LineSection() { start = 14, length = 1, iurLabel = "IU0TYP", hrLabel = "TransactionType" };
        public static LineSection InterfaceVersion = new LineSection() { start = 15, length = 2, iurLabel = "IU0VER", hrLabel = "InterfaceVersion" };
        public static LineSection CustomerNumber = new LineSection() { start = 17, length = 10, iurLabel = "IU0DKN", hrLabel = "CustomerNumber" };
        public static LineSection SpareSpace = new LineSection() { start = 27, length = 5, iurLabel = "IU0CTJ", hrLabel = "SpareSpace" };
        public static LineSection PreviouslyInvoicedIndicator = new LineSection() { start = 32, length = 1, iurLabel = "IU0PIV", hrLabel = "PreviouslyInvoiced" };
        public static LineSection PnrRecordControlCheck = new LineSection() { start = 33, length = 2, iurLabel = "IU0RRC", hrLabel = "PnrRecordControlCheck" };
        public static LineSection QueueType = new LineSection() { start = 35, length = 1, iurLabel = "IU0QUE", hrLabel = "QueueType" };
        public static LineSection SpareSpace2 = new LineSection() { start = 36, length = 1, iurLabel = "IU0CDK", hrLabel = "SpareSpace2" };
        public static LineSection InvoiceNumber = new LineSection() { start = 37, length = 7, iurLabel = "IU0IVN", hrLabel = "InvoiceNumber" };
        public static LineSection AgencyArcIataNr = new LineSection() { start = 44, length = 10, iurLabel = "IU0ATC", hrLabel = "AgencyArcIataNr" };
        public static LineSection PnrLocator = new LineSection() { start = 54, length = 8, iurLabel = "IU0PNR", hrLabel = "PnrLocator" };
        public static LineSection LinkPnrLocator = new LineSection() { start = 62, length = 8, iurLabel = "IU0PNL", hrLabel = "LinkPnrLocator" };
        public static LineSection SubscriberInterfaceOptionIndicator = new LineSection() { start = 70, length = 1, iurLabel = "IU0OPT", hrLabel = "SubscInterfOptionInd" };
        public static LineSection PhoneIndicator = new LineSection() { start = 71, length = 1, iurLabel = "IU0F00", hrLabel = "PhoneIndicator" };
        public static LineSection Entitlements = new LineSection() { start = 72, length = 1, iurLabel = "IU0F01", hrLabel = "Entitlements" };
        public static LineSection FareCalculations = new LineSection() { start = 73, length = 1, iurLabel = "IU0F02", hrLabel = "FareCalculations" };
        public static LineSection InvoiceItineraryRemarks = new LineSection() { start = 74, length = 1, iurLabel = "IU0F03", hrLabel = "InvoiceItineraryRemarks" };
        public static LineSection InterfaceRemarks = new LineSection() { start = 75, length = 1, iurLabel = "IU0F04", hrLabel = "InterfaceRemarks" };
        public static LineSection StatuteMiles = new LineSection() { start = 76, length = 1, iurLabel = "IU0F05", hrLabel = "StatuteMiles" };
        public static LineSection ItineraryToPosQ = new LineSection() { start = 77, length = 1, iurLabel = "IU0F06", hrLabel = "ItineraryToPosQ" };
        public static LineSection CustomerProfitabilityRecord = new LineSection() { start = 78, length = 1, iurLabel = "IU0F07", hrLabel = "CustomerProfitabilityRecords" };
        public static LineSection MiscChargeOrderRecords = new LineSection() { start = 79, length = 1, iurLabel = "IU0F08", hrLabel = "MiscChargeOrderRecords" };
        public static LineSection PrePaidTicketRecords = new LineSection() { start = 80, length = 1, iurLabel = "IU0F09", hrLabel = "PrePaidTicketRecords" };
        public static LineSection TourOrderRecords = new LineSection() { start = 81, length = 1, iurLabel = "IU0F0A", hrLabel = "TourOrderRecords" };
        public static LineSection SegmentAssociatedRemarksRecords = new LineSection() { start = 82, length = 1, iurLabel = "IU0F0B", hrLabel = "SegmentAssociatedRemarkRecords" };
        public static LineSection SpareSpace3 = new LineSection() { start = 83, length = 1, iurLabel = "IU0F0C", hrLabel = "SpareSpace3" };
        public static LineSection NameAssociatedRemarks = new LineSection() { start = 84, length = 1, iurLabel = "IU0F0D", hrLabel = "NameAssociatedRemarks" };
        public static LineSection AmountMovedToMX = new LineSection() { start = 85, length = 1, iurLabel = "IU0F0E", hrLabel = "AmountMovedToMX" };
        public static LineSection SpareSpace4 = new LineSection() { start = 86, length = 1, iurLabel = "IU0F0F", hrLabel = "SpareSpace4" };
        public static LineSection AtbIndicators = new LineSection() { start = 87, length = 1, iurLabel = "IU0ATB", hrLabel = "AtbIndicators" };
        public static LineSection DuplicateInterfaceIndicator = new LineSection() { start = 88, length = 1, iurLabel = "IU0DUP", hrLabel = "DuplicateInterfaceIndicator" };
        public static LineSection PseudoCityCode = new LineSection() { start = 89, length = 5, iurLabel = "IU0PCC", hrLabel = "PseudoCityCode" };
        public static LineSection BookingAgentDutyCode = new LineSection() { start = 94, length = 2, iurLabel = "IU0IDC", hrLabel = "BookingAgDutyCode" };
        public static LineSection BookingAgentSine = new LineSection() { start = 96, length = 3, iurLabel = "IU0IAG", hrLabel = "BookingAgSine" };
        public static LineSection LnIata = new LineSection() { start = 99, length = 8, iurLabel = "IU0LIN", hrLabel = "LnIata" };
        public static LineSection RemotePrinter = new LineSection() { start = 107, length = 10, iurLabel = "IU0RPR", hrLabel = "RemotePrinter" };
        public static LineSection PnrCreationDate = new LineSection() { start = 117, length = 5, iurLabel = "IU0PDT", hrLabel = "PnrCreationDate" };
        public static LineSection PnrCreationTime = new LineSection() { start = 122, length = 5, iurLabel = "IU0TIM", hrLabel = "PnrCreationTime" };
        public static LineSection InvoicingAgencyCityCode = new LineSection() { start = 127, length = 5, iurLabel = "IU0IS4", hrLabel = "InvoiceAgencyCityCode" };
        public static LineSection InvoicingAgentDutyCode = new LineSection() { start = 132, length = 2, iurLabel = "IU0IS1", hrLabel = "InvoiceAgentDutyCode" };
        public static LineSection InvoicingAgentCode = new LineSection() { start = 134, length = 3, iurLabel = "IU0IS3", hrLabel = "InvoiceAgentCode" };
        public static LineSection Spare4 = new LineSection() { start = 137, length = 2, iurLabel = "IU0TCO", hrLabel = "Spare4" };
        public static LineSection BranchId = new LineSection() { start = 139, length = 3, iurLabel = "IU0IDB", hrLabel = "BranchId" };
        public static LineSection DepartureDate = new LineSection() { start = 142, length = 5, iurLabel = "IU0DEP", hrLabel = "DepartureDate" };
        public static LineSection OriginCityCode = new LineSection() { start = 147, length = 3, iurLabel = "IU0ORG", hrLabel = "OriginCityCode" };
        public static LineSection OriginCityName = new LineSection() { start = 150, length = 17, iurLabel = "IU0ONM", hrLabel = "OriginCityName" };
        public static LineSection DestinationCityCode = new LineSection() { start = 167, length = 3, iurLabel = "IU0DST", hrLabel = "DestinationCityCode" };
        public static LineSection DestinationCityName = new LineSection() { start = 170, length = 17, iurLabel = "IU0DNM", hrLabel = "DestinationCityName" };
        public static LineSection PassengerNumber = new LineSection() { start = 187, length = 3, iurLabel = "IU0NM1", hrLabel = "PassengerNumber" };
        public static LineSection TicketedPassengerNumber = new LineSection() { start = 190, length = 3, iurLabel = "IU0NM2", hrLabel = "TicketedPassengerNumber" };
        public static LineSection ItinerarySegmentNumber = new LineSection() { start = 193, length = 3, iurLabel = "IU0NM3", hrLabel = "ItinerarySegNumber" };
        public static LineSection EntitlementNumber = new LineSection() { start = 196, length = 3, iurLabel = "IU0NM4", hrLabel = "EntitlementsNumber" };
        public static LineSection AccountingLinesNumber = new LineSection() { start = 199, length = 3, iurLabel = "IU0NM5", hrLabel = "AccountingLinesNumber" };
        public static LineSection FareCalcLinesNumber = new LineSection() { start = 202, length = 3, iurLabel = "IU0NM6", hrLabel = "FareCalcLinesPassengerNumber" };
        public static LineSection ItineraryRemarksNumber = new LineSection() { start = 205, length = 3, iurLabel = "IU0NM7", hrLabel = "ItineraryRemarksNumber" };
        public static LineSection InvoiceRemarkLinesNumber = new LineSection() { start = 208, length = 3, iurLabel = "IU0NM8", hrLabel = "InvoiceRemarksLinesNumber" };
        public static LineSection InterfaceRemarksNumber = new LineSection() { start = 211, length = 3, iurLabel = "IU0NM9", hrLabel = "InterfaceRemarksLinesNumber" };
        public static LineSection CustomerProfitabilityItemsNumber = new LineSection() { start = 214, length = 3, iurLabel = "IU0NMA", hrLabel = "CustomerProfitabilityItems" };
        public static LineSection Spare5 = new LineSection() { start = 217, length = 2, iurLabel = "IU0ADC", hrLabel = "SpareSpace5" };
        public static LineSection PhoneFieldsNumber = new LineSection() { start = 219, length = 2, iurLabel = "IU0PHC", hrLabel = "PhoneFieldsNumber" };
        public static LineSection IURTA = new LineSection() { start = 221, length = 5, iurLabel = "IU0TYM", hrLabel = "IURTA" };
        public static LineSection RecordCreationDate = new LineSection() { start = 226, length = 5, iurLabel = "IU0MDT", hrLabel = "RecordCreationDate" };
        public static LineSection TicketNumber = new LineSection() { start = 27, length = 14, iurLabel = "IU0TKI", hrLabel = "TicketNumber" };
        public static LineSection NumberOfTickets = new LineSection() { start = 41, length = 2, iurLabel = "IU0CNT", hrLabel = "NumberOfTicket" };
        public static LineSection VoidPnrLocator = new LineSection() { start = 43, length = 8, iurLabel = "IU0PR1", hrLabel = "PnrLocator" };
        public static LineSection DuplicateVoidIndicator = new LineSection() { start = 51, length = 1, iurLabel = "IU0DUP", hrLabel = "DuplicateVoidIndicator" };
        public static LineSection VoidAuthCodes = new LineSection() { start = 52, length = 15, iurLabel = "IU0SAC", hrLabel = "VoidAuthCodes" };
    }
}
