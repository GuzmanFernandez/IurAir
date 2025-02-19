using IurAir.Domain.Air.Shared;
using IurAir.Domain.Common;
using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    /*
     H-001;002OMDE;MEDELLIN         ;MAD;MADRID A.SUAREZ  ;UX    0198 M M 09JUN2025 1255 10JUN;OK01;HK01;M ;0;788;M;;1PC;;;ET;0930 ;N;4993;1 
     H-002;003XMAD;MADRID A.SUAREZ  ;ALC;ALICANTE         ;UX    4049 M M 10JUN1505 1610 10JUN;OK01;HK01;G ;0;E90;;AIR EUROPA EXPRESS;1PC;2 ;;ET;0105 ;N;222;
     H-003;004OALC;ALICANTE         ;MAD;MADRID A.SUAREZ  ;UX    4042 Q Q 03SEP1215 1320 03SEP;OK01;HK01;G ;0;E90;;AIR EUROPA EXPRESS;1PC;;;ET;0105 ;N;222;2 
     H-004;005XMAD;MADRID A.SUAREZ  ;MDE;MEDELLIN         ;UX    0199 Q Q 03SEP1515 1825 03SEP;OK01;HK01;M ;0;788;M;;1PC;1 ;;ET;1010 ;N;4993;
     * */
    public class HLine
    {
        public string ProgressiveLineNumber { get; set; }
        public string SegmentNumber { get; set; }
        public string StopOverPermitted { get; set; }
        public string OriginAirportCode { get; set; }
        public string OriginCityName { get; set; }
        public string DestinationAirportCode { get; set; }
        public string DestinationCityName { get; set; }
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string FlightNumberSuffix { get; set; }
        public string ClassOfService { get; set; }
        public string ClassOfBooking { get; set; }
        public string DepartureDate { get; set; } //(DDMMM 01JUN)
        public string DepartureTime { get; set; } //(HHMM + A-P-N-M if 12)
        public string ArrivalTime { get; set; } //(HHMM + A-P-N-M if 12)
        public string ArrivalDate { get; set; } //(DDMMM 01JUN)
        public string StatusCode { get; set; }
        public string PnrStatusCode { get; set; }
        public string MealCode { get; set; }
        public string NumberOfStops { get; set; }
        public string EquipmentType { get; set; }
        public string BaggageAllowance { get; set; }
        public string ElectronicTicket { get; set; }
        public string FlightDuration { get; set; }
        public string OriginCountryCode { get; set; }
        public string DestinationCountryCode { get; set; }
        public string Miles { get; set; }
        public string ArrivalTerminal { get; set; }
        public HLine(M3Object itinerary, M4Object entitlements)
        {
            ProgressiveLineNumber = $"0{itinerary.itineraryItemNumber}";
            SegmentNumber =AirUtil.PadString(ProgressiveLineNumber, '0', 3, true);
            StopOverPermitted = string.IsNullOrEmpty(entitlements.connectionIndicator) ? "X" : entitlements.connectionIndicator;
            OriginAirportCode = itinerary.departureAirport;
            OriginCityName = AirUtil.PadString(itinerary.departureCity, ' ', 17, false);
            DestinationAirportCode = itinerary.arrivalAirport;
            DestinationCityName = AirUtil.PadString(itinerary.arrivalCity, ' ', 17, false);
            AirlineCode = AirUtil.PadString(itinerary.carrierCode, ' ', 6, false);
            FlightNumber= AirUtil.PadString(itinerary.flightNumber, '0', 4, true);
            FlightNumberSuffix = " ";
            ClassOfService = AirUtil.PadString(itinerary.classOfService, ' ', 2, false);
            ClassOfBooking = AirUtil.PadString(itinerary.classOfService, ' ', 2, false);
            DepartureDate = itinerary.departureDate;
            DepartureTime = AirUtil.PadString(itinerary.departureTime, ' ', 5, false);
            ArrivalTime = AirUtil.PadString(itinerary.arrivalTime, ' ', 5, false);
            ArrivalDate = AirUtil.CalculateArrivalDate(DepartureDate, itinerary.flightArrivalDateChangeInd);
            StatusCode = "OK01";
            PnrStatusCode = "HK01";
            MealCode = itinerary.mealServiceIndicator;
            NumberOfStops = string.IsNullOrWhiteSpace(itinerary.stops) ? "0" : itinerary.stops;
            EquipmentType = itinerary.equipmentTypeCode;
            BaggageAllowance = entitlements.baggageAllow;
            ElectronicTicket = string.IsNullOrWhiteSpace(entitlements.electronicTicketIndicator) ? "" : "ET";
            FlightDuration = AirUtil.PadString(AirUtil.ConvertElapsed(itinerary.elapsedFlyingTime), ' ', 5, false);
            OriginCountryCode = AirportReader.GetAirportByCode(itinerary.departureAirport).Country;
            DestinationCountryCode = AirportReader.GetAirportByCode(itinerary.arrivalAirport).Country;
            Miles = string.IsNullOrEmpty(itinerary.statuteMiles) ?itinerary.statuteMiles : int.Parse(itinerary.statuteMiles).ToString();
            ArrivalTerminal = "1";
        }

        

        public HLine(M3Object itinerary)
        {
            ProgressiveLineNumber = $"0{itinerary.itineraryItemNumber}";
            SegmentNumber = AirUtil.PadString(ProgressiveLineNumber, '0', 3, true);
            StopOverPermitted = "X";
            OriginAirportCode = itinerary.departureAirport;
            OriginCityName = AirUtil.PadString(itinerary.departureCity, ' ', 17, false);
            DestinationAirportCode = itinerary.arrivalAirport;
            DestinationCityName = AirUtil.PadString(itinerary.arrivalCity, ' ', 17, false);
            AirlineCode = AirUtil.PadString(itinerary.carrierCode, ' ', 6, false);
            FlightNumber = AirUtil.PadString(itinerary.flightNumber, '0', 4, true);
            FlightNumberSuffix = " ";
            ClassOfService = AirUtil.PadString(itinerary.classOfService, ' ', 2, false);
            ClassOfBooking = AirUtil.PadString(itinerary.classOfService, ' ', 2, false);
            DepartureDate = itinerary.departureDate;
            DepartureTime = AirUtil.PadString(itinerary.departureTime, ' ', 5, false);
            ArrivalTime = AirUtil.PadString(itinerary.arrivalTime, ' ', 5, false);
            ArrivalDate = AirUtil.CalculateArrivalDate(DepartureDate, itinerary.flightArrivalDateChangeInd);
            StatusCode = "OK01";
            PnrStatusCode = "HK01";
            MealCode = AirUtil.PadString(itinerary.mealServiceIndicator, ' ', 2, false);
            NumberOfStops = string.IsNullOrWhiteSpace(itinerary.stops) ? "0" : itinerary.stops;
            EquipmentType = itinerary.equipmentTypeCode;
            BaggageAllowance = "1PC";
            ElectronicTicket = "ET";
            
            FlightDuration = AirUtil.PadString(AirUtil.ConvertElapsed(itinerary.elapsedFlyingTime), ' ', 5, false);
            OriginCountryCode = AirportReader.GetAirportByCode(itinerary.departureAirport).Country;
            DestinationCountryCode = AirportReader.GetAirportByCode(itinerary.arrivalAirport).Country;
            Miles = string.IsNullOrEmpty(itinerary.statuteMiles) ? itinerary.statuteMiles : int.Parse(itinerary.statuteMiles).ToString();
        }

        public HLine()
        {
            //H-001;002OMDE;MEDELLIN         ;MAD;MADRID A.SUAREZ  ;UX    0198 M M 09JUN2025 1255 10JUN;OK01;HK01;M ;0;788;M;;1PC;;;ET;0930 ;N;4993;1
            //H-001;001OMDE;MEDELLIN         ;MAD;MADRID A.SUAREZ  ;UX    0198 M M 09JUN2025 1255 10JUN;OK01;HK01;M ;0;788;M;;1PC;;;ET;0930 ;N;4993;1
            ProgressiveLineNumber = $"001";
            SegmentNumber = AirUtil.PadString(ProgressiveLineNumber, '0', 3, true);
            OriginAirportCode = "MDE";
            OriginCityName = AirUtil.PadString("MEDELLIN", ' ', 17, false);
            DestinationAirportCode = "MAD";
            DestinationCityName = AirUtil.PadString("MADRID A.SUAREZ", ' ', 17, false);
            AirlineCode = AirUtil.PadString("UX", ' ', 6, false);
            FlightNumber = AirUtil.PadString("198", '0', 4, true);
            FlightNumberSuffix = " ";
            ClassOfService = AirUtil.PadString("M", ' ', 2, false);
            ClassOfBooking = AirUtil.PadString("M", ' ', 2, false);
            DepartureDate = "09JUN";
            DepartureTime = AirUtil.PadString("2025", ' ', 5, false);
            ArrivalTime = AirUtil.PadString("1255", ' ', 5, false);
            ArrivalDate = "10JUN";
            StatusCode = "OK01";
            PnrStatusCode = "HK01";
            MealCode = "M";
            NumberOfStops = "0";
            EquipmentType = "788";
            BaggageAllowance = "1PC";
            ElectronicTicket = "ET";
            FlightDuration = AirUtil.PadString("0930", ' ', 5, false);
            Miles = "4993";
            OriginCountryCode = "XX";
            DestinationCountryCode = "XX";
            ArrivalTerminal = "1";
        }
        public string FormatString208()
        {
            return $"H-{ProgressiveLineNumber};{SegmentNumber}{StopOverPermitted}{OriginAirportCode};{OriginCityName};{DestinationAirportCode};{DestinationCityName};{AirlineCode}{FlightNumber} {ClassOfService}{ClassOfBooking}{DepartureDate}{DepartureTime}{ArrivalTime}{ArrivalDate};{StatusCode};{PnrStatusCode};{MealCode};{NumberOfStops};{EquipmentType};;;{BaggageAllowance};;;{ElectronicTicket};{FlightDuration};N;{Miles};{OriginCountryCode};{DestinationCountryCode};";
        }
        public string FormatString206()
        {
            return $"H-{ProgressiveLineNumber};{SegmentNumber}{StopOverPermitted}{OriginAirportCode};{OriginCityName};{DestinationAirportCode};{DestinationCityName};{AirlineCode}{FlightNumber} {ClassOfService}{ClassOfBooking}{DepartureDate}{DepartureTime}{ArrivalTime}{ArrivalDate};{StatusCode};{PnrStatusCode};{MealCode};{NumberOfStops};{EquipmentType};;;{BaggageAllowance};;;{ElectronicTicket};{FlightDuration};N;{Miles};{ArrivalTerminal}";
        }
    }

}

//206
//H-001;002OMDE;MEDELLIN         ;MAD;MADRID A.SUAREZ  ;UX    0198 M M 09JUN2025 1255 10JUN;OK01;HK01;M ;0;788;M;;1PC;;;ET;0930 ;N;4993;1

//207
//H-003;002OLIN;MILAN LINATE     ;FCO;ROME FIUMICINO   ;AZ    2013 O O 10JUN0700 0810 10JUN;OK01;HK01;R ;0;32S;;;0PC;;;ET;0110 ;N;293;IT;IT;1 
//H-001;001FCO;ROME FIUMICINO   ;JFK;NEW YORK JFK     ;AA    0235 Y Y 20APR1030 1359 20APR;OK01;HK01;;0;772;;;1PC;;;ET;0929 ;N;4274;XX;XX;1

//IUR
//M3011 0HK16DECAIRNBCNBARCELONA        MADMADRID           IB 3003Y 0700 0825  1.25   G   000                    32A000300      00TERMINAL 1                    TERMINAL 4                         0                                     1002022N2AJLF


//H-001;001FCO;ROME FIUMICINO   ;JFK;NEW YORK JFK     ;AA    0235 Y Y 20APR1030 1359 20APR;OK01;HK01;;0;772;;;1PC;;;ET;0929 ;N;4274;XX;XX;1
