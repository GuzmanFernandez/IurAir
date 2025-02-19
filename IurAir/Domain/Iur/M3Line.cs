using IurAir.Domain.Common;
using IurAir.Domain.Iur.Sections;
using IurAir.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class M3Line : IurLine
    {
        public M3Line(string rawLine) : base(rawLine, "M3", 248, M3S.M3Sections())
        {
        }

        public override IurObject getObject()
        {
            return new M3Object(this);
        }

    }

    public class M3Object : IurObject
    {
        [DefaultValue("")]
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
        public string airlinePnrLocator { get; }

        public M3Object(M3Line line)
        {
            Dictionary<string, string> lineMap = line.parseLine();
            this.itineraryItemNumber = lineMap.ContainsKey("IU3ITN") ? lineMap["IU3ITN"].Trim() : "";
            this.productCode = lineMap.ContainsKey("IU3PRC") ? lineMap["IU3PRC"].Trim() : "";
            this.linkCode = lineMap.ContainsKey("IU3LNK") ? lineMap["IU3LNK"].Trim() : "";
            this.controlData = lineMap.ContainsKey("IU3CRL") ? lineMap["IU3CRL"].Trim() : "";
            this.actionSdvSegStatCode = lineMap.ContainsKey("IU3AAC") ? lineMap["IU3AAC"].Trim() : "";
            this.departureDate = lineMap.ContainsKey("IU3DDT") ? lineMap["IU3DDT"].Trim() : "";
            this.secondaryProdCode = lineMap.ContainsKey("IU3PC2") ? lineMap["IU3PC2"].Trim() : "";
            this.issueBoardingPass = lineMap.ContainsKey("IU3BPI") ? lineMap["IU3BPI"].Trim() : "";
            this.departureAirport = lineMap.ContainsKey("IU3DCC") ? lineMap["IU3DCC"].Trim() : "";
            this.departureCity = lineMap.ContainsKey("IU3DCY") ? lineMap["IU3DCY"].Trim() : "";
            this.arrivalAirport = lineMap.ContainsKey("IU3ACC") ? lineMap["IU3ACC"].Trim() : "";
            this.arrivalCity = lineMap.ContainsKey("IU3ACY") ? lineMap["IU3ACY"].Trim() : "";
            this.carrierCode = lineMap.ContainsKey("IU3CAR") ? lineMap["IU3CAR"].Trim() : "";
            this.flightNumber = lineMap.ContainsKey("IU3FLT") ? lineMap["IU3FLT"].Trim() : "";
            this.classOfService = lineMap.ContainsKey("IU3CLS") ? lineMap["IU3CLS"].Trim() : "";
            this.departureTime = lineMap.ContainsKey("IU3DTM") ? lineMap["IU3DTM"].Trim() : "";
            this.arrivalTime = lineMap.ContainsKey("IU3ATM") ? lineMap["IU3ATM"].Trim() : "";
            this.elapsedFlyingTime = lineMap.ContainsKey("IU3ELT") ? lineMap["IU3ELT"].Trim() : "";
            this.mealServiceIndicator = lineMap.ContainsKey("IU3MLI") ? lineMap["IU3MLI"].Trim() : "";
            this.supplementalInformations = lineMap.ContainsKey("IU3SUP") ? lineMap["IU3SUP"].Trim() : "";
            this.flightArrivalDateChangeInd = lineMap.ContainsKey("IU3DCH") ? lineMap["IU3DCH"].Trim() : "";
            this.stops = lineMap.ContainsKey("IU3NOS") ? lineMap["IU3NOS"].Trim() : "";
            this.stopOverCityCodes = lineMap.ContainsKey("IU3SCC") ? lineMap["IU3SCC"].Trim() : "";
            this.carrierTypeCode = lineMap.ContainsKey("IU3CRT") ? lineMap["IU3CRT"].Trim() : "";
            this.equipmentTypeCode = lineMap.ContainsKey("IU3EQP") ? lineMap["IU3EQP"].Trim() : "";
            this.statuteMiles = lineMap.ContainsKey("IU3ARM") ? lineMap["IU3ARM"].Trim() : "";
            this.frequentTravelerMiles = lineMap.ContainsKey("IU3AVM") ? lineMap["IU3AVM"].Trim() : "";
            this.preReservedSeatCounter = lineMap.ContainsKey("IU3SCT") ? lineMap["IU3SCT"].Trim() : "";
            this.departureTerminal = lineMap.ContainsKey("IU3TRM") ? lineMap["IU3TRM"].Trim() : "";
            this.departureGate = lineMap.ContainsKey("IU3GAT") ? lineMap["IU3GAT"].Trim() : "";
            this.arrivalTerminal = lineMap.ContainsKey("IU3TMA") ? lineMap["IU3TMA"].Trim() : "";
            this.arrivalGate = lineMap.ContainsKey("IU3GAR") ? lineMap["IU3GAR"].Trim() : "";
            this.reportTime = lineMap.ContainsKey("IU3RTM") ? lineMap["IU3RTM"].Trim() : "";
            this.changeOfGaugeFunn = lineMap.ContainsKey("IU3COG") ? lineMap["IU3COG"].Trim() : "";
            this.commuterCarrierName = lineMap.ContainsKey("IU3CRN") ? lineMap["IU3CRN"].Trim() : "";
            this.itineraryItemTicketingIndicator = lineMap.ContainsKey("IU3TKT") ? lineMap["IU3TKT"].Trim() : "";
            this.specialMealReqCounter = lineMap.ContainsKey("IU3MCT") ? lineMap["IU3MCT"].Trim() : "";
            this.departureYear = lineMap.ContainsKey("IU3YER") ? lineMap["IU3YER"].Trim() : "";
            this.airlinePnrLocator = lineMap.ContainsKey("IU3OAL") ? lineMap["IU3OAL"].Trim() : "";
        }

        public Itinerary toItinerary()
        {
            string dts = $"{departureDate}{departureYear}{departureTime}";
            string dtP = "ddMMMyyyyHHmm";
            return new Itinerary()
            {
                DepartureAirport = this.departureAirport,
                DepartureCity = this.departureCity,
                DepartureCountry = AirportReader.GetAirportByCode(this.departureAirport).Country,

                ArrivalAirport = this.arrivalAirport,
                ArrivalCity = this.arrivalCity,
                ArrivalCountry = AirportReader.GetAirportByCode(this.arrivalAirport).Country,
            };
        }
    }
}
