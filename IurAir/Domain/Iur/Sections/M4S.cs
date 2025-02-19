using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public static class M4S
    {

        public static List<LineSection> M4Sections()
        {
            return new List<LineSection>()
            {
                M3RecordItemNumber,
                PassengerTypeCode,
                ConnectionIndicator,
                EntitlementType,
                FareNotValidBeforeDate,
                FareNotValidAfterDate,
                M4Status,
                BaggageAllow,
                FareBasisCode,
                AmtrakClassOfService,
                FareByLegDollarAmt,
                ElectronicTicketIndicator,
                FareBasisCodeExpanded,
                TicketDesignatorExpanded,
                CurrencyCode,
                SpareSpace,
            };
        }

        public static LineSection M3RecordItemNumber = new LineSection() { start = 3, length = 2, iurLabel = "IU4SEG", hrLabel = "itinerary_record_item_nr" };
        public static LineSection PassengerTypeCode = new LineSection() { start = 5, length = 3, iurLabel = "IU4TYP", hrLabel = "passenger_type_code" };
        public static LineSection ConnectionIndicator = new LineSection() { start = 8, length = 1, iurLabel = "IU4CNI", hrLabel = "connection_indicator" };
        public static LineSection EntitlementType = new LineSection() { start = 9, length = 1, iurLabel = "IU4ETP", hrLabel = "entitlement_type" };
        public static LineSection FareNotValidBeforeDate = new LineSection() { start = 10, length = 5, iurLabel = "IU4NVB", hrLabel = "fare_not_valid_before" };
        public static LineSection FareNotValidAfterDate = new LineSection() { start = 15, length = 5, iurLabel = "IU4NVA", hrLabel = "fare_not_valid_after" };
        public static LineSection M4Status = new LineSection() { start = 20, length = 2, iurLabel = "IU4AAC", hrLabel = "m4_status" };
        public static LineSection BaggageAllow = new LineSection() { start = 22, length = 3, iurLabel = "IU4AWL", hrLabel = "baggage_allow" };
        public static LineSection FareBasisCode = new LineSection() { start = 25, length = 13, iurLabel = "IU4FBS", hrLabel = "fare_basis_code" };
        public static LineSection AmtrakClassOfService = new LineSection() { start = 38, length = 2, iurLabel = "IU4ACL", hrLabel = "amtrak_service_class" };
        public static LineSection FareByLegDollarAmt = new LineSection() { start = 40, length = 8, iurLabel = "IU4AMT", hrLabel = "fare_byleg_dollar_amt" };
        public static LineSection ElectronicTicketIndicator = new LineSection() { start = 48, length = 1, iurLabel = "IU4ETK", hrLabel = "eticket_indicator" };
        public static LineSection FareBasisCodeExpanded = new LineSection() { start = 49, length = 12, iurLabel = "IU4FB2", hrLabel = "fare_basis_expanded" };
        public static LineSection TicketDesignatorExpanded = new LineSection() { start = 61, length = 10, iurLabel = "IU4TD2", hrLabel = "ticket_designator_expanded" };
        public static LineSection CurrencyCode = new LineSection() { start = 71, length = 3, iurLabel = "IU4CUR", hrLabel = "currency_code" };
        public static LineSection SpareSpace = new LineSection() { start = 74, length = 13, iurLabel = "IU4SP2", hrLabel = "spare_space" };
    }
}
