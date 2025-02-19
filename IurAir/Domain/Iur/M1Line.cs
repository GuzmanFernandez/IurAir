using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class M1Line : IurLine
    {

        public M1Line(string rawLine) : base(rawLine, "M1", 139, M1S.M1Sections())
        {

        }

        public override IurObject getObject()
        {
            return new M1Object(this);
        }
    }

    public class M1Object : IurObject
    {
        [DefaultValue("")]
        public string interfaceNameItemNumber { get; }
        [DefaultValue("")]
        public string passengerName { get; }
        [DefaultValue("")]
        public string passengerManNumber { get; }
        [DefaultValue("")]
        public string advantageAirPass { get; }
        [DefaultValue("")]
        public string frequentTravelerNr { get; }
        [DefaultValue("")]
        public string frequentTravelerLevel { get; }
        [DefaultValue("")]
        public string itinerariesNumber { get; }
        [DefaultValue("")]
        public string nameSelectedForTicketing { get; }
        [DefaultValue("")]
        public string spareSpace1 { get; }
        [DefaultValue("")]
        public string acctgLines { get; }
        [DefaultValue("")]
        public string itineraryRemarksNr { get; }
        [DefaultValue("")]
        public string invoiceRemarksNr { get; }
        [DefaultValue("")]
        public string interfaceRemarksNr { get; }
        [DefaultValue("")]
        public string futureUseField { get; }
        [DefaultValue("")]
        public string[] itineraryNumbersForPassenger { get; set; } = { };
        [DefaultValue("")]
        public string[] invoiceForPassenger { get; set; } = { };
        [DefaultValue("")]
        public string[] m7ForPassenger { get; set; } = { };
        [DefaultValue("")]
        public string[] m8ForPassenger { get; set; } = { };
        [DefaultValue("")]
        public string[] m9ForPassenger { get; set; } = { };

        public M1Object(M1Line line)
        {
            Dictionary<string, string> lineMap = line.parseLine();
            this.interfaceNameItemNumber = lineMap.ContainsKey("IU1PNO") ? lineMap["IU1PNO"].Trim() : "";
            this.passengerName = lineMap.ContainsKey("IU1PNM") ? lineMap["IU1PNM"].Trim() : "";
            this.passengerManNumber = lineMap.ContainsKey("IU1PRK") ? lineMap["IU1PRK"].Trim() : "";
            this.advantageAirPass = lineMap.ContainsKey("IU1IND") ? lineMap["IU1IND"].Trim() : "";
            this.frequentTravelerNr = lineMap.ContainsKey("IU1AVN") ? lineMap["IU1AVN"].Trim() : "";
            this.frequentTravelerLevel = lineMap.ContainsKey("IU1MLV") ? lineMap["IU1MLV"].Trim() : "";
            this.itinerariesNumber = lineMap.ContainsKey("IU1NM3") ? lineMap["IU1NM3"].Trim() : "";
            this.nameSelectedForTicketing = lineMap.ContainsKey("IU1TKT") ? lineMap["IU1TKT"].Trim() : "";
            this.spareSpace1 = lineMap.ContainsKey("IU1SSM") ? lineMap["IU1SSM"].Trim() : "";
            this.acctgLines = lineMap.ContainsKey("IU1NM5") ? lineMap["IU1NM5"].Trim() : "";
            this.itineraryRemarksNr = lineMap.ContainsKey("IU1NM7") ? lineMap["IU1NM7"].Trim() : "";
            this.invoiceRemarksNr = lineMap.ContainsKey("IU1NM8") ? lineMap["IU1NM8"].Trim() : "";
            this.interfaceRemarksNr = lineMap.ContainsKey("IU1NM9") ? lineMap["IU1NM9"].Trim() : "";
            this.futureUseField = lineMap.ContainsKey("IU1NMA") ? lineMap["IU1NMA"].Trim() : "";
        }

        public override bool Equals(object obj)
        {
            return obj is M1Object @object &&
                   interfaceNameItemNumber == @object.interfaceNameItemNumber &&
                   passengerName == @object.passengerName &&
                   passengerManNumber == @object.passengerManNumber &&
                   advantageAirPass == @object.advantageAirPass &&
                   frequentTravelerNr == @object.frequentTravelerNr &&
                   frequentTravelerLevel == @object.frequentTravelerLevel &&
                   itinerariesNumber == @object.itinerariesNumber &&
                   nameSelectedForTicketing == @object.nameSelectedForTicketing &&
                   spareSpace1 == @object.spareSpace1 &&
                   acctgLines == @object.acctgLines &&
                   itineraryRemarksNr == @object.itineraryRemarksNr &&
                   invoiceRemarksNr == @object.invoiceRemarksNr &&
                   interfaceRemarksNr == @object.interfaceRemarksNr &&
                   futureUseField == @object.futureUseField &&
                   EqualityComparer<string[]>.Default.Equals(itineraryNumbersForPassenger, @object.itineraryNumbersForPassenger) &&
                   EqualityComparer<string[]>.Default.Equals(invoiceForPassenger, @object.invoiceForPassenger) &&
                   EqualityComparer<string[]>.Default.Equals(m7ForPassenger, @object.m7ForPassenger) &&
                   EqualityComparer<string[]>.Default.Equals(m8ForPassenger, @object.m8ForPassenger) &&
                   EqualityComparer<string[]>.Default.Equals(m9ForPassenger, @object.m9ForPassenger);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(interfaceNameItemNumber);
            hash.Add(passengerName);
            hash.Add(passengerManNumber);
            hash.Add(advantageAirPass);
            hash.Add(frequentTravelerNr);
            hash.Add(frequentTravelerLevel);
            hash.Add(itinerariesNumber);
            hash.Add(nameSelectedForTicketing);
            hash.Add(spareSpace1);
            hash.Add(acctgLines);
            hash.Add(itineraryRemarksNr);
            hash.Add(invoiceRemarksNr);
            hash.Add(interfaceRemarksNr);
            hash.Add(futureUseField);
            hash.Add(itineraryNumbersForPassenger);
            hash.Add(invoiceForPassenger);
            hash.Add(m7ForPassenger);
            hash.Add(m8ForPassenger);
            hash.Add(m9ForPassenger);
            return hash.ToHashCode();
        }
    }
}
