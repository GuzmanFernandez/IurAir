using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public static class M3S
    {
        public static List<LineSection> M3Sections()
        {
            return new List<LineSection>()
            {
                ItineraryItemNumber,
                ProductCode,
                LinkCode,
                ControlData,
                ActionSdvSegStatCode,
                DepartureDate,
                SecondaryProdCode,
                IssueBoardingPass,
                DepartureAirport,
                DepartureCity,
                ArrivalAirport,
                ArrivalCity,
                CarrierCode,
                FlightNumber,
                ClassOfService,
                DepartureTime,
                ArrivalTime,
                ElapsedFlyingTime,
                MealServiceIndicator,
                SupplementalInformations,
                FlightArrivalDateChangeInd,
                Stops,
                StopOverCityCodes,
                CarrierTypeCode,
                EquipmentTypeCode,
                StatuteMiles,
                FrequentTravelerMiles,
                PreReservedSeatCounter,
                DepartureTerminal,
                DepartureGate,
                ArrivalTerminal,
                ArrivalGate,
                ReportTime,
                ChangeOfGaugeFunn,
                CommuterCarrierName,
                ItineraryItemTicketingIndicator,
                SpecialMealReqCounter,
                DepartureYear,
                AirlinePnrLocator,
            };
        }

        public static LineSection ItineraryItemNumber = new LineSection() { start = 3, length = 2, iurLabel = "IU3ITN", hrLabel = "itinerary_item_nr" };
        public static LineSection ProductCode = new LineSection() { start = 5, length = 1, iurLabel = "IU3PRC", hrLabel = "product_code" };
        public static LineSection LinkCode = new LineSection() { start = 6, length = 1, iurLabel = "IU3LNK", hrLabel = "link_code" };
        public static LineSection ControlData = new LineSection() { start = 7, length = 1, iurLabel = "IU3CRL", hrLabel = "control_data" };
        public static LineSection ActionSdvSegStatCode = new LineSection() { start = 8, length = 2, iurLabel = "IU3AAC", hrLabel = "action_advice_seg_status_code" };
        public static LineSection DepartureDate = new LineSection() { start = 10, length = 5, iurLabel = "IU3DDT", hrLabel = "departure_date" };
        public static LineSection SecondaryProdCode = new LineSection() { start = 15, length = 3, iurLabel = "IU3PC2", hrLabel = "sec_product_code" };
        public static LineSection IssueBoardingPass = new LineSection() { start = 18, length = 1, iurLabel = "IU3BPI", hrLabel = "issue_boarding_pass" };
        public static LineSection DepartureAirport = new LineSection() { start = 19, length = 3, iurLabel = "IU3DCC", hrLabel = "departure_airport" };
        public static LineSection DepartureCity = new LineSection() { start = 22, length = 17, iurLabel = "IU3DCY", hrLabel = "departure_city" };
        public static LineSection ArrivalAirport = new LineSection() { start = 39, length = 3, iurLabel = "IU3ACC", hrLabel = "arrival_airport" };
        public static LineSection ArrivalCity = new LineSection() { start = 42, length = 17, iurLabel = "IU3ACY", hrLabel = "arrival_city" };
        public static LineSection CarrierCode = new LineSection() { start = 59, length = 2, iurLabel = "IU3CAR", hrLabel = "airline_code" };
        public static LineSection FlightNumber = new LineSection() { start = 61, length = 5, iurLabel = "IU3FLT", hrLabel = "flight_nr" };
        public static LineSection ClassOfService = new LineSection() { start = 66, length = 2, iurLabel = "IU3CLS", hrLabel = "service_class" };
        public static LineSection DepartureTime = new LineSection() { start = 68, length = 5, iurLabel = "IU3DTM", hrLabel = "departure_time" };
        public static LineSection ArrivalTime = new LineSection() { start = 73, length = 5, iurLabel = "IU3ATM", hrLabel = "arrival_time" };
        public static LineSection ElapsedFlyingTime = new LineSection() { start = 78, length = 8, iurLabel = "IU3ELT", hrLabel = "flight_duration" };
        public static LineSection MealServiceIndicator = new LineSection() { start = 86, length = 4, iurLabel = "IU3MLI", hrLabel = "meal_service_indicator" };
        public static LineSection SupplementalInformations = new LineSection() { start = 90, length = 1, iurLabel = "IU3SUP", hrLabel = "supplemental_info" };
        public static LineSection FlightArrivalDateChangeInd = new LineSection() { start = 91, length = 1, iurLabel = "IU3DCH", hrLabel = "arrival_date_change" };
        public static LineSection Stops = new LineSection() { start = 92, length = 1, iurLabel = "IU3NOS", hrLabel = "stop_nr" };
        public static LineSection StopOverCityCodes = new LineSection() { start = 93, length = 18, iurLabel = "IU3SCC", hrLabel = "stop_over_city_codes" };
        public static LineSection CarrierTypeCode = new LineSection() { start = 111, length = 2, iurLabel = "IU3CRT", hrLabel = "carrier_type_code" };
        public static LineSection EquipmentTypeCode = new LineSection() { start = 113, length = 3, iurLabel = "IU3EQP", hrLabel = "equipment" };
        public static LineSection StatuteMiles = new LineSection() { start = 116, length = 6, iurLabel = "IU3ARM", hrLabel = "geo_mileage" };
        public static LineSection FrequentTravelerMiles = new LineSection() { start = 122, length = 6, iurLabel = "IU3AVM", hrLabel = "ft_miles" };
        public static LineSection PreReservedSeatCounter = new LineSection() { start = 128, length = 2, iurLabel = "IU3SCT", hrLabel = "pre_res_seat" };
        public static LineSection DepartureTerminal = new LineSection() { start = 130, length = 26, iurLabel = "IU3TRM", hrLabel = "check_in_terminal" };
        public static LineSection DepartureGate = new LineSection() { start = 156, length = 4, iurLabel = "IU3GAT", hrLabel = "check_in_gate" };
        public static LineSection ArrivalTerminal = new LineSection() { start = 160, length = 26, iurLabel = "IU3TMA", hrLabel = "arrival_terminal" };
        public static LineSection ArrivalGate = new LineSection() { start = 186, length = 4, iurLabel = "IU3GAR", hrLabel = "arrival_gate" };
        public static LineSection ReportTime = new LineSection() { start = 190, length = 5, iurLabel = "IU3RTM", hrLabel = "report_time" };
        public static LineSection ChangeOfGaugeFunn = new LineSection() { start = 195, length = 1, iurLabel = "IU3COG", hrLabel = "change_of_gauge_funn" };
        public static LineSection CommuterCarrierName = new LineSection() { start = 196, length = 37, iurLabel = "IU3CRN", hrLabel = "shared_des_commuter" };
        public static LineSection ItineraryItemTicketingIndicator = new LineSection() { start = 233, length = 1, iurLabel = "IU3TKT", hrLabel = "tktt" };
        public static LineSection SpecialMealReqCounter = new LineSection() { start = 234, length = 2, iurLabel = "IU3MCT", hrLabel = "special_meal_counter" };
        public static LineSection DepartureYear = new LineSection() { start = 236, length = 4, iurLabel = "IU3YER", hrLabel = "dept_year" };
        public static LineSection AirlinePnrLocator = new LineSection() { start = 240, length = 8, iurLabel = "IU3OAL", hrLabel = "pnr_locator" };
    }
}
