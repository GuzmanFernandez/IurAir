using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur
{
    public class M4Line : IurLine
    {
        public M4Line(string rawLine) : base(rawLine, "M4", 86, M4S.M4Sections())
        {
        }

        public override IurObject getObject()
        {
            return new M4Object(this);
        }
    }

    public class M4Object : IurObject
    {
        [DefaultValue("")]
        public string m3RecordItemNumber { get; }
        [DefaultValue("")]
        public string passengerTypeCode { get; }
        [DefaultValue("")]
        public string connectionIndicator { get; }
        [DefaultValue("")]
        public string entitlementType { get; }
        [DefaultValue("")]
        public string fareNotValidBeforeDate { get; }
        [DefaultValue("")]
        public string fareNotValidAfterDate { get; }
        [DefaultValue("")]
        public string m4Status { get; }
        [DefaultValue("")]
        public string baggageAllow { get; }
        [DefaultValue("")]
        public string fareBasisCode { get; }
        [DefaultValue("")]
        public string amtrakClassOfService { get; }
        [DefaultValue("")]
        public string fareByLegDollarAmt { get; }
        [DefaultValue("")]
        public string electronicTicketIndicator { get; }
        [DefaultValue("")]
        public string fareBasisCodeExpanded { get; }
        [DefaultValue("")]
        public string ticketDesignatorExpanded { get; }
        [DefaultValue("")]
        public string currencyCode { get; }
        [DefaultValue("")]
        public string spareSpace { get; }

        public M4Object(M4Line line)
        {
            var lineMap = line.parseLine();
            this.m3RecordItemNumber = lineMap.ContainsKey("IU4SEG") ? lineMap["IU4SEG"].Trim() : "";
            this.passengerTypeCode = lineMap.ContainsKey("IU4TYP") ? lineMap["IU4TYP"].Trim() : "";
            this.connectionIndicator = lineMap.ContainsKey("IU4CNI") ? lineMap["IU4CNI"].Trim() : "";
            this.entitlementType = lineMap.ContainsKey("IU4ETP") ? lineMap["IU4ETP"].Trim() : "";
            this.fareNotValidBeforeDate = lineMap.ContainsKey("IU4NVB") ? lineMap["IU4NVB"].Trim() : "";
            this.fareNotValidAfterDate = lineMap.ContainsKey("IU4NVA") ? lineMap["IU4NVA"].Trim() : "";
            this.m4Status = lineMap.ContainsKey("IU4AAC") ? lineMap["IU4AAC"].Trim() : "";
            this.baggageAllow = lineMap.ContainsKey("IU4AWL") ? lineMap["IU4AWL"].Trim() : "";
            this.fareBasisCode = lineMap.ContainsKey("IU4FBS") ? lineMap["IU4FBS"].Trim() : "";
            this.amtrakClassOfService = lineMap.ContainsKey("IU4ACL") ? lineMap["IU4ACL"].Trim() : "";
            this.fareByLegDollarAmt = lineMap.ContainsKey("IU4AMT") ? lineMap["IU4AMT"].Trim() : "";
            this.electronicTicketIndicator = lineMap.ContainsKey("IU4ETK") ? lineMap["IU4ETK"].Trim() : "";
            this.fareBasisCodeExpanded = lineMap.ContainsKey("IU4FB2") ? lineMap["IU4FB2"].Trim() : "";
            this.ticketDesignatorExpanded = lineMap.ContainsKey("IU4TD2") ? lineMap["IU4TD2"].Trim() : "";
            this.currencyCode = lineMap.ContainsKey("IU4CUR") ? lineMap["IU4CUR"].Trim() : "";
            this.spareSpace = lineMap.ContainsKey("IU4SP2") ? lineMap["IU4SP2"].Trim() : "";
        }
    }
}
