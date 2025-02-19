using IurAir.Domain.Air.Lines;
using IurAir.Domain.Common;
using IurAir.Domain.Iur;
using IurAir.Domain.Iur.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IurAir.Domain.TicketEmission
{

    public class Itinerary
    {
        public List<ItineraryTrait> Traits { get; set; }
        public Dictionary<string, List<string>> TranslatedItineraryLines { get; set; } = new Dictionary<string, List<string>>();
        private readonly IurDocument _document;

        public Itinerary(M1Object pax, IurDocument document)
        {
            _document = document;
            string[] m3LinesKey = pax
               .itineraryNumbersForPassenger
               .Select(itin => $"M3{itin}")
               .ToArray();
            Traits = document
                .compositeLines
                .Where(k => m3LinesKey.Contains(k.Key))
                .Select(k => new ItineraryTrait(k.Value as M3Object))
                .ToList();
            setM4Objects();
            List<HLine> lines = GetHlines();
            List<string> itinLines = new List<string>();
            foreach (HLine line in lines)
            {
                itinLines.Add(line.FormatString208());
            }
            TranslatedItineraryLines.Add("AIR", itinLines);
        }
        private void setM4Objects()
        {
            foreach(ItineraryTrait trait in Traits)
            {
                var m4 = _document
                    .compositeLines
                    .Where(cl => cl.Key.StartsWith($"M4{trait.TraitNumber}"))
                    .FirstOrDefault();
                if(m4.Value != null)
                {
                    trait.setM4(m4.Value as M4Object);
                }
            }
        }
        public List<HLine> GetHlines()
        {
            List<HLine> hLines = new List<HLine>();
            foreach (ItineraryTrait t in Traits)
            {
                try
                {
                    if(t.GetUnderlyingM4Object()!= null)
                    {
                        HLine line = new HLine(t.GetUnderlyingM3Object(), t.GetUnderlyingM4Object());
                        hLines.Add(line);
                    }
                    else
                    {
                        HLine line = new HLine(t.GetUnderlyingM3Object());
                        hLines.Add(line);
                    }
                    
                    
                }
                catch (Exception e)
                {

                }
            }
            return hLines;
        }
    }
    public class ItineraryTrait
    {
        public string TraitNumber { get; set; }
        public ItineraryDate DepartureDate { get; set; }
        public ItineraryDate ArrivalDate { get; set; }
        public ItineraryCity DepartureCity { get; set; }
        public ItineraryCity ArrivalCity { get; set; }
        public CarrierData Carrier { get; set; }
        public string FlightNumber { get; set; }
        public string AirlinePnrLocator { get; set; }
        private M3Object _undergroundM3 { get; set; }   
        public M3Object GetUnderlyingM3Object() 
        { return _undergroundM3; }
        private M4Object _undergroundM4;
        public M4Object GetUnderlyingM4Object()
        { return _undergroundM4; }

        public ItineraryTrait(
            string traitNumber,
            ItineraryDate departureDate,
            ItineraryDate arrivalDate,
            ItineraryCity departureCity,
            ItineraryCity arrivalCity,
            CarrierData carrier,
            string flightNumber,
            string airlinePnrLocator)
        {
            TraitNumber = traitNumber;
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            DepartureCity = departureCity;
            ArrivalCity = arrivalCity;
            FlightNumber = flightNumber;
            Carrier = carrier;
            AirlinePnrLocator = airlinePnrLocator;
            _undergroundM3 = null;
            _undergroundM4 = null;
        }

        public ItineraryTrait(M3Object m3)
        {
            TraitNumber = m3.itineraryItemNumber;
            DepartureDate = new ItineraryDate(m3.departureDate, m3.departureTime, m3.departureYear);
            DepartureCity = new ItineraryCity(m3.departureAirport, m3.departureCity);
            ArrivalDate = new ItineraryDate(DepartureDate.Date, m3.flightArrivalDateChangeInd, m3.arrivalTime);
            ArrivalCity = new ItineraryCity(m3.arrivalAirport, m3.arrivalCity);
            FlightNumber = m3.flightNumber;
            Carrier = new CarrierData(VectorReader.GetVectorByIATA(m3.carrierCode));
            AirlinePnrLocator = m3.airlinePnrLocator;
            _undergroundM3 = m3;
        }

        public void setM4(M4Object m4)
        {
            _undergroundM4 = m4;
        }
    }

    public class ItineraryDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
        public DateTime Date { get; set; }

        public ItineraryDate(string dateString, string timeString, string year)
        {
            int day, month, hour, minutes;
            DateUtilities.DateFromDocument(dateString, out day, out month);
            if (day == 0 || month == 0)
            {
                throw new ArgumentException("Date format not recognized");
            }
            DateUtilities.TimeFromDocument(timeString, out hour, out minutes);
            if (hour == -1 || minutes == -1)
            {
                throw new ArgumentException("Time format not recognized");
            }
            int Year;
            if (!int.TryParse(year, out Year))
            {
                throw new ArgumentException("Year format not recognized");
            }
            Day = day;
            Month = month;
            Hour = hour;
            Minutes = minutes;
            this.Year = Year;
            Date = new DateTime(year: Year, month: Month, day: Day, hour: Hour, minute: Minutes, second: 0);
        }

        public ItineraryDate(DateTime depDate, string dateIndicator, string timeString)
        {
            int year, month, day, hour, minutes;
            DateUtilities.TimeFromDocument(timeString, out hour, out minutes);
            if (hour == -1 || minutes == -1)
            {
                throw new ArgumentException("Time format not recognized");
            }
            switch (dateIndicator)
            {
                case "1":
                    DateTime calcDate1 = depDate.AddDays(1);
                    year = calcDate1.Year;
                    month = calcDate1.Month;
                    day = calcDate1.Day;
                    break;
                case "2":
                    DateTime calcDate2 = depDate.AddDays(2);
                    year = calcDate2.Year;
                    month = calcDate2.Month;
                    day = calcDate2.Day;
                    break;
                case "3":
                    DateTime calcDate3 = depDate.AddDays(3);
                    year = calcDate3.Year;
                    month = calcDate3.Month;
                    day = calcDate3.Day;
                    break;
                case "4":
                    DateTime calcDate4 = depDate - TimeSpan.FromDays(1);
                    year = calcDate4.Year;
                    month = calcDate4.Month;
                    day = calcDate4.Day;
                    break;
                case "5":
                    DateTime calcDate5 = depDate - TimeSpan.FromDays(2);
                    year = calcDate5.Year;
                    month = calcDate5.Month;
                    day = calcDate5.Day;
                    break;
                default:
                    year = depDate.Year;
                    month = depDate.Month;
                    day = depDate.Day;
                    break;
            }
            Day = day;
            Month = month;
            Hour = hour;
            Minutes = minutes;
            Year = year;
            Date = new DateTime(year: year, month: Month, day: Day, hour: Hour, minute: Minutes, second: 0);
        }
    }

    public class ItineraryCity
    {
        public string Country { get; set; }
        public string CityName { get; set; }
        public string AirportDesignator { get; set; }
        public string AirportCode { get; set; }

        public ItineraryCity(string airportCode, string city)
        {
            Airports airport = AirportReader.GetAirportByCode(airportCode);
            Country = airport.Country;
            AirportCode = airportCode;
            AirportDesignator = airport.Id;
            CityName = city;
        }
    }

    public class CarrierData
    {
        public string CarrierName { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierDesignator { get; set; }

        public CarrierData(Vectors vector)
        {
            CarrierName = vector.Name;
            CarrierCode = vector.Code;
            CarrierDesignator = vector.Designator;
        }
    }

    /*
     * [DefaultValue("")]
        public string itineraryItemNumber { get; }
        [DefaultValue("")]
        public string productCode { get; }
        [DefaultValue("")]
        public string linkCode { get; }
        [DefaultValue("")]
        public string controlData { get; }
        [DefaultValue("")]
        public string actionSdvSegStatCode { get; }
        [DefaultValue("")]
        public string departureDate { get; }
        [DefaultValue("")]
        public string secondaryProdCode { get; }
        [DefaultValue("")]
        public string issueBoardingPass { get; }
        [DefaultValue("")]
        public string departureAirport { get; }
        [DefaultValue("")]
        public string departureCity { get; }
        [DefaultValue("")]
        public string arrivalAirport { get; }
        [DefaultValue("")]
        public string arrivalCity { get; }
        [DefaultValue("")]
        public string carrierCode { get; }
        [DefaultValue("")]
        public string flightNumber { get; }
        [DefaultValue("")]
        public string classOfService { get; }
        [DefaultValue("")]
        public string departureTime { get; }
        [DefaultValue("")]
        public string arrivalTime { get; }
        [DefaultValue("")]
        public string elapsedFlyingTime { get; }
        [DefaultValue("")]
        public string mealServiceIndicator { get; }
        [DefaultValue("")]
        public string supplementalInformations { get; }
        [DefaultValue("")]
        public string flightArrivalDateChangeInd { get; }
        [DefaultValue("")]
        public string stops { get; }
        [DefaultValue("")]
        public string stopOverCityCodes { get; }
        [DefaultValue("")]
        public string carrierTypeCode { get; }
        [DefaultValue("")]
        public string equipmentTypeCode { get; }
        [DefaultValue("")]
        public string statuteMiles { get; }
        [DefaultValue("")]
        public string frequentTravelerMiles { get; }
        [DefaultValue("")]
        public string preReservedSeatCounter { get; }
        [DefaultValue("")]
        public string departureTerminal { get; }
        [DefaultValue("")]
        public string departureGate { get; }
        [DefaultValue("")]
        public string arrivalTerminal { get; }
        [DefaultValue("")]
        public string arrivalGate { get; }
        [DefaultValue("")]
        public string reportTime { get; }
        [DefaultValue("")]
        public string changeOfGaugeFunn { get; }
        [DefaultValue("")]
        public string commuterCarrierName { get; }
        [DefaultValue("")]
        public string itineraryItemTicketingIndicator { get; }
        [DefaultValue("")]
        public string specialMealReqCounter { get; }
        [DefaultValue("")]
        public string departureYear { get; }
        [DefaultValue("")]
        public string airlinePnrLocator { get; }*/
}
