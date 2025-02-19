using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public abstract class M1S
    {

        public static LineSection InterfaceNameItemNumber = new LineSection() { start = 3, length = 2, iurLabel = "IU1PNO", hrLabel = "InterfaceNameItemNr" };
        public static LineSection PassengerName = new LineSection() { start = 5, length = 64, iurLabel = "IU1PNM", hrLabel = "PassengerName" };
        public static LineSection PassengerManNumber = new LineSection() { start = 69, length = 30, iurLabel = "IU1PRK", hrLabel = "PassengerManNr" };
        public static LineSection AdvantageAirPass = new LineSection() { start = 99, length = 1, iurLabel = "IU1IND", hrLabel = "AdvantageAirPass" };
        public static LineSection FrequentTravelerNr = new LineSection() { start = 100, length = 20, iurLabel = "IU1AVN", hrLabel = "FrequentTravelerNr" };
        public static LineSection FrequentTravelerLevel = new LineSection() { start = 120, length = 5, iurLabel = "IU1MLV", hrLabel = "FrequentTravelerMembershipLevel" };
        public static LineSection ItinerariesNumber = new LineSection() { start = 125, length = 2, iurLabel = "IU1NM3", hrLabel = "ItinerariesNr" };
        public static LineSection NameSelectedForTicketing = new LineSection() { start = 127, length = 1, iurLabel = "IU1TKT", hrLabel = "NameSelectedForTicketing" };
        public static LineSection SpareSpace1 = new LineSection() { start = 128, length = 1, iurLabel = "IU1SSM", hrLabel = "Spare1" };
        public static LineSection AcctgLines = new LineSection() { start = 129, length = 2, iurLabel = "IU1NM5", hrLabel = "AcctgLinesNr" };
        public static LineSection ItineraryRemarksNr = new LineSection() { start = 131, length = 2, iurLabel = "IU1NM7", hrLabel = "ItineraryRemarksNr" };
        public static LineSection InvoiceRemarksNr = new LineSection() { start = 133, length = 2, iurLabel = "IU1NM8", hrLabel = "InvoiceRemarksNr" };
        public static LineSection InterfaceRemarksNr = new LineSection() { start = 135, length = 2, iurLabel = "IU1NM9", hrLabel = "InterfaceRemarksNr" };
        public static LineSection FutureUseField = new LineSection() { start = 137, length = 2, iurLabel = "IU1NMA", hrLabel = "FutureUseField" };

        public static List<LineSection> M1Sections()
        {
            return new List<LineSection>()
            {
                InterfaceNameItemNumber,
                PassengerName,
                PassengerManNumber,
                AdvantageAirPass,
                FrequentTravelerNr,
                FrequentTravelerLevel,
                ItinerariesNumber,
                NameSelectedForTicketing,
                SpareSpace1,
                AcctgLines,
                ItineraryRemarksNr,
                InvoiceRemarksNr,
                InterfaceRemarksNr,
                FutureUseField,
            };
        }
    }
}
