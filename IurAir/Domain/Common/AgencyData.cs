using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IurAir.Domain.Common
{
    public class AgencyData
    {
        public string Version { get; set; }
        public string OfficeId { get; set; }
        public string AgentDutyCode { get; set; }
        public string AgentInitials { get; set; }
        public string NetworkIdentifier1 { get; set; }
        public string TerminalIdentifier1 { get; set; }
        public string NetworkIdentifier2 { get; set; }
        public string TerminalIdentifier2 { get; set; }
        public string AgencyIATA { get; set; }
        public string DefaultCurrency { get; set; }
        
    }
}
