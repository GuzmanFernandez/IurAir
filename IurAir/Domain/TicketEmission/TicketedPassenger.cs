using IurAir.Domain.Air.Lines;
using IurAir.Domain.Air.Shared;
using IurAir.Domain.Iur;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace IurAir.Domain.TicketEmission
{
    public class PassengerInformations
    {
        public string PassengerPnrNumber { get; set; }
        public string PassengerName { get; set; }
        public string PassengerType { get; set; }
        public string TicketNumber { get; set; }
        public bool ResidentialFareForSpain { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ResidentialFare ResidentialFare { get; set; }
        [JsonIgnore]
        public ILine iLine { get; set; }
        public Dictionary<string, string> PassengerInfoTranslation { get; set; } = new Dictionary<string, string>();
        [JsonIgnore]
        public TLine tLine { get; set; }
        public Dictionary<string, string> TicketNumberTranslations { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> TicketingCarrierTranslations { get; set; } = new Dictionary<string, string>();
    }

    public class ResidentialFare
    {
        public ResidentType ResidentialType { get; set; } = ResidentType.NonResident;
        public DocumentType DocType { get; set; } = DocumentType.NonResident;
        public string Fee { get; set; } = null;
        public string DocumentNumber { get; set; } = null;
        public string BirthDate { get; set; } = null;
        

        //private readonly string ResidentRegex = @"/(?:RCDN|RCTR|RCMR|DCDN|DCTR|DCMR|BPDN|BPTR|BPMR|BIDN|BITR|BIMR|CEDN|CETR|CEMR|RMDN|RMTR|RMMR)/(?<doc>[^/]+)/(?<zip>[^/]+)/";
        private readonly string ResidentRegex = @"(?<residentType>(RC|DC|BP|BI|CE|RM)?)(?<docType>(?:DN|TR|MR))\/(?<doc>[^\/]+)\/(?<ddn>[^\/]+)\/(?:[A-Z]*)?(?<fee>([0-9.]*))";

        public ResidentialFare(M6Object m6)
        {
            Match match = Regex.Match(m6.Data, ResidentRegex);
            if (match.Success)
            {
                GroupCollection grp = match.Groups;
                foreach (Group g in grp)
                {
                    if (g.Name == "doc")
                    {
                        DocumentNumber = g.Value;
                    }
                    if (g.Name == "ddn")
                    {
                        BirthDate = g.Value;
                    }
                    if(g.Name == "residentType")
                    {
                        var rt = EnumExtensions.ParseEnumValue<ResidentType>(g.Value);
                        ResidentialType = rt;
                    }
                    if (g.Name == "docType")
                    {
                        DocType = EnumExtensions.ParseEnumValue<DocumentType>(g.Value);
                    }
                    if (g.Name == "fee")
                    {
                        Fee = g.Value;
                    }
                }
            }
        }

        public enum ResidentType
        {
            [EnumMember(Value="RC")]
            ResidenteDeCanaria_Islas_Peninsula,
            [EnumMember(Value = "DC")]
            ResidenteDeCanarias_Interinsular,
            [EnumMember(Value = "BP")]
            ResidenteDeBaleares_Islas_Peninsula,
            [EnumMember(Value = "BI")] 
            ResidenteDeBaleares_Peninsular,
            [EnumMember(Value = "CE")]
            ResidenteDeCeuta,
            [EnumMember(Value = "RM")] 
            ResidenteDeMelilla,
            NonResident
        }

        public enum DocumentType
        {
            [EnumMember(Value = "DN")]
            TarjetaDeIdentidad,
            [EnumMember(Value = "TR")]
            NacionalEuropeo,
            [EnumMember(Value = "MR")]
            MenoreSinDNI,
            NonResident
        }
    }

}
