using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public static class MGS
    {
        public static List<LineSection> MgSections()
        {
            return new List<LineSection>()
            {
                MessageId,
                InterfaceItemNr,
                PassengerType,
                CreationLocalDate,
                CreationLocalTime,
                CrsId,
                ValidatingCarrierCode,
                Spare1,
                AirlineCode,
                DocumentNr,
                CheckDigit1,
                ConjunctionTicketCount,
                AirlineNumCode,
                TicketNr,
                CheckDigit2,
                EmdTypeIndicator,
                EndorsableIndicator,
                CommissionableIndicator,
                RefundIndicator,
                Spare2,
                SegmentJourneyIndicator,
                FareCalcInVarArea,
                TravelOrigin,
                Spare3,
                TravelDestination,
                Spare4,
                RFIC,
                TourCode,
                TotFareCurrency,
                TotFareAmt,
                TotalTaxes,
                TotalCommision,
                EqAmtCurrency,
                EqAmtFareAmt,
                FormOfPayment,
                CcCpy,
                Spare5,
                CcNr,
                Spare6,
                CcExpNr,
                CcExtPay,
                CcAuth,
                CcAuthCode,
                Spare7,
                EndorsementIndicator,
                ExchengeDocNr,
                Spare8,
                CouponNr,
            };
        }

        public static LineSection MessageId = new LineSection() { start = 1, length = 2, iurLabel = "IUGMID", hrLabel = "MessageId" };
        public static LineSection InterfaceItemNr = new LineSection() { start = 3, length = 2, iurLabel = "IUGPNO", hrLabel = "InterfaceItemNr" };
        public static LineSection PassengerType = new LineSection() { start = 5, length = 3, iurLabel = "IUGPTY", hrLabel = "PassengerType" };
        public static LineSection CreationLocalDate = new LineSection() { start = 8, length = 9, iurLabel = "IUGDTE", hrLabel = "CreationDate" };
        public static LineSection CreationLocalTime = new LineSection() { start = 17, length = 4, iurLabel = "IUGTME", hrLabel = "CreationTime" };
        public static LineSection CrsId = new LineSection() { start = 21, length = 2, iurLabel = "IUGGDS", hrLabel = "CrsId" };
        public static LineSection ValidatingCarrierCode = new LineSection() { start = 23, length = 2, iurLabel = "IUGVAL", hrLabel = "ValidatingCarrierCode" };
        public static LineSection Spare1 = new LineSection() { start = 25, length = 1, iurLabel = "IUGVAS", hrLabel = "Spare1" };
        public static LineSection AirlineCode = new LineSection() { start = 26, length = 3, iurLabel = "IUEMA", hrLabel = "AirlineCode" };
        public static LineSection DocumentNr = new LineSection() { start = 29, length = 10, iurLabel = "IUGEMN", hrLabel = "DocumentNr" };
        public static LineSection CheckDigit1 = new LineSection() { start = 39, length = 1, iurLabel = "IUGEMC", hrLabel = "CheckDigit1" };
        public static LineSection ConjunctionTicketCount = new LineSection() { start = 40, length = 1, iurLabel = "IUGCJC", hrLabel = "ConjunctionTicketCount" };
        public static LineSection AirlineNumCode = new LineSection() { start = 41, length = 3, iurLabel = "IUGETA", hrLabel = "AirlineNumCode" };
        public static LineSection TicketNr = new LineSection() { start = 44, length = 10, iurLabel = "IUGETN", hrLabel = "TicketNr" };
        public static LineSection CheckDigit2 = new LineSection() { start = 54, length = 1, iurLabel = "IUGETC", hrLabel = "CheckDigit2" };

        public static LineSection EmdTypeIndicator = new LineSection() { start = 55, length = 1, iurLabel = "IUGTYP", hrLabel = "EmdType" };
        public static LineSection EndorsableIndicator = new LineSection() { start = 56, length = 1, iurLabel = "IUGEND", hrLabel = "Endorsable" };
        public static LineSection CommissionableIndicator = new LineSection() { start = 57, length = 1, iurLabel = "IUGCOI", hrLabel = "Commisionable" };
        public static LineSection RefundIndicator = new LineSection() { start = 58, length = 1, iurLabel = "IUGRFN", hrLabel = "Refundable" };
        public static LineSection Spare2 = new LineSection() { start = 59, length = 1, iurLabel = "IUGSJS", hrLabel = "Spare2" };
        public static LineSection SegmentJourneyIndicator = new LineSection() { start = 60, length = 1, iurLabel = "IUGSJC", hrLabel = "Segment/Journey/Portion" };
        public static LineSection FareCalcInVarArea = new LineSection() { start = 61, length = 1, iurLabel = "IUGFCO", hrLabel = "FareCalcInVarArea" };

        public static LineSection TravelOrigin = new LineSection() { start = 62, length = 3, iurLabel = "IUGOTV", hrLabel = "TravelOrigin" };
        public static LineSection Spare3 = new LineSection() { start = 65, length = 2, iurLabel = "IUGOTS", hrLabel = "Spare3" };
        public static LineSection TravelDestination = new LineSection() { start = 67, length = 3, iurLabel = "IUGDTV", hrLabel = "TravelDestinationn" };
        public static LineSection Spare4 = new LineSection() { start = 70, length = 2, iurLabel = "IUGDTS", hrLabel = "Spare4" };
        public static LineSection RFIC = new LineSection() { start = 72, length = 2, iurLabel = "IUGRFI", hrLabel = "RFIC" };
        public static LineSection TourCode = new LineSection() { start = 74, length = 15, iurLabel = "IUGTCO", hrLabel = "TourCode" };

        public static LineSection TotFareCurrency = new LineSection() { start = 89, length = 3, iurLabel = "IUGFCU", hrLabel = "TotalFareCurrency" };
        public static LineSection TotFareAmt = new LineSection() { start = 92, length = 18, iurLabel = "IUGFAM", hrLabel = "TotalFareAmt" };

        public static LineSection TotalTaxes = new LineSection() { start = 110, length = 18, iurLabel = "IUGTTX", hrLabel = "TotalTaxes" };
        public static LineSection TotalCommision = new LineSection() { start = 128, length = 18, iurLabel = "IUGTCM", hrLabel = "TotalCommision" };

        public static LineSection EqAmtCurrency = new LineSection() { start = 146, length = 3, iurLabel = "IUGECU", hrLabel = "EquivalentCurrency" };
        public static LineSection EqAmtFareAmt = new LineSection() { start = 149, length = 18, iurLabel = "IUGEAM", hrLabel = "TotalEquivalentAmt" };

        public static LineSection FormOfPayment = new LineSection() { start = 167, length = 2, iurLabel = "IUGFOP", hrLabel = "FormOfPayment" };

        public static LineSection CcCpy = new LineSection() { start = 169, length = 2, iurLabel = "IUGCCC", hrLabel = "CreditCardCompany" };
        public static LineSection Spare5 = new LineSection() { start = 171, length = 1, iurLabel = "IUGCCS", hrLabel = "Spare5" };
        public static LineSection CcNr = new LineSection() { start = 172, length = 18, iurLabel = "IUGCCN", hrLabel = "CreditCardNr" };
        public static LineSection Spare6 = new LineSection() { start = 190, length = 4, iurLabel = "IUGCNS", hrLabel = "Spare6" };
        public static LineSection CcExpNr = new LineSection() { start = 194, length = 4, iurLabel = "IUGCCD", hrLabel = "CreditCardExpNr" };
        public static LineSection CcExtPay = new LineSection() { start = 198, length = 4, iurLabel = "IUGCCE", hrLabel = "CreditCardExtPay" };
        public static LineSection CcAuth = new LineSection() { start = 202, length = 9, iurLabel = "IUGCCA", hrLabel = "CreditCardAuth" };
        public static LineSection CcAuthCode = new LineSection() { start = 211, length = 1, iurLabel = "IUGCCT", hrLabel = "CreditCardAuthCode" };
        public static LineSection Spare7 = new LineSection() { start = 212, length = 1, iurLabel = "IUGCNO", hrLabel = "Spare7" };

        public static LineSection EndorsementIndicator = new LineSection() { start = 213, length = 1, iurLabel = "IUGRES", hrLabel = "EndorsementIndicator" };
        public static LineSection ExchengeDocNr = new LineSection() { start = 214, length = 13, iurLabel = "IUGEXC", hrLabel = "ExchengeDocNr" };
        public static LineSection Spare8 = new LineSection() { start = 227, length = 10, iurLabel = "IUGSPR", hrLabel = "Spare8" };
        public static LineSection CouponNr = new LineSection() { start = 237, length = 2, iurLabel = "IUGCPN", hrLabel = "CouponNr" };
    }

}