using IurAir.Domain.Air.Lines;
using IurAir.Domain.Air.Shared;
using IurAir.Domain.Iur;
using IurAir.Models;
using IurAir.Services.SettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static IurAir.Domain.TicketEmission.ResidentialFare;

namespace IurAir.Domain.TicketEmission
{
    public class TicketDataRecord
    {
        public PassengerInformations PassengerInformations { get; set; }
        public Itinerary Itinerary { get; set; }
        public TicketAccounting AccountingInformations { get; set; }
        public Dictionary<string, string> ResidentialFareTranslation { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> ForPaxTranslation { get; set; } = new Dictionary<string, string>();
        public TicketDataRecord(M1Object passenger, IurDocument document)
        {
            string totPax = document
                .compositeLines
                .Where(x => x.Key.StartsWith("M1"))
                .Count()
                .ToString();
            Itinerary = new Itinerary(passenger, document);
            AccountingInformations = new TicketAccounting(passenger, document);
            if (AccountingInformations.ExtTicketRecord != null)
            {
                string commission = AccountingInformations.ExtTicketRecord.Commission != null ? AccountingInformations.ExtTicketRecord.Commission.StringAmount : "0.00";
                string totSeg = Itinerary.Traits.Count().ToString();
                FMLine fmL = new FMLine(commission, totSeg, totPax);
                AccountingInformations.CommissionTranslations.Add("AIR", fmL.Line);
            }
            PassengerInformations = new PassengerInformations();
            PassengerInformations.PassengerPnrNumber = passenger.interfaceNameItemNumber;
            PassengerInformations.PassengerName = passenger.passengerName;
            if (AccountingInformations.ExtTicketRecord != null)
            {

                PassengerInformations.PassengerType = AccountingInformations.ExtTicketRecord.PassengerType;
                PassengerInformations.TicketNumber = AccountingInformations.ExtTicketRecord.TicketNumber;
            }
            ILine iline = new ILine(
                PassengerInformations.PassengerPnrNumber,
                PassengerInformations.PassengerPnrNumber,
                PassengerInformations.PassengerName,
                PassengerInformations.PassengerType);
            PassengerInformations.iLine = iline;
            PassengerInformations.PassengerInfoTranslation.Add("AIR", iline.FormatString208(SettingService.GetPerPaxSplit()));
            if (AccountingInformations.ExtTicketRecord != null)
            {
                TLine tline = new TLine(
                    AccountingInformations.ExtTicketRecord.TicketType,
                    AccountingInformations.ExtTicketRecord.ValidatingCarrier,
                    PassengerInformations.TicketNumber
                    );

                PassengerInformations.TicketNumberTranslations.Add("AIR", tline.FormatString208());
                PassengerInformations.TicketingCarrierTranslations.Add("AIR", $"FV{AccountingInformations.ExtTicketRecord.ValidatingCarrier.CarrierDesignator}");
            }
            if (AccountingInformations.ExtTicketRecord != null && AccountingInformations.ExtTicketRecord.RelatedM6Lines.Count > 0)
            {
                if (AccountingInformations.ExtTicketRecord.RelatedM6Lines.Count == 1)
                {
                    string line = AccountingInformations.ExtTicketRecord.RelatedM6Lines[0];
                    M6Object m6 = document
                        .compositeLines
                        .Where(l => l.Key == line)
                        .Select(l => l.Value as M6Object)
                        .FirstOrDefault();
                    if (m6 == null)
                    {
                        m6 = document
                            .compositeLines
                            .Where(l => l.Key == $"M6{PassengerInformations.PassengerType}")
                            .Select(l => l.Value as M6Object)
                        .FirstOrDefault();
                    }
                    if (m6 != null)
                    {
                        ResidentialFare rf = new ResidentialFare(m6);
                        if (rf.DocumentNumber != null)
                        {
                            PassengerInformations.ResidentialFare = rf;
                            string rt = EnumExtensions.GetEnumMemberValue(rf.ResidentialType);
                            string dt = EnumExtensions.GetEnumMemberValue(rf.DocType);
                            PassengerInformations.ResidentialFareForSpain = true;
                            ResidentialFareTranslation.Add("AIR", $"FD{rt}{dt}/{rf.DocumentNumber}/{rf.BirthDate}");
                            AccountingInformations.fee.Add(new FeeInformations(PassengerInformations.PassengerPnrNumber, PassengerInformations.PassengerName, rf.Fee));
                            AccountingInformations.FormOfPaymentTranslations["AIR"] += $"+SFCA/{AccountingInformations.ExtTicketRecord.TotalFare.Currency}{rf.Fee}";
                        }
                        else
                        {
                            PassengerInformations.ResidentialFare = null;
                            PassengerInformations.ResidentialFareForSpain = false;
                        }
                    }
                    else
                    {
                        PassengerInformations.ResidentialFare = null;
                        PassengerInformations.ResidentialFareForSpain = false;
                    }
                }
                else
                {
                    List<string> lines = AccountingInformations.ExtTicketRecord.RelatedM6Lines;
                    List<M6Object> m6Objects = document
                        .compositeLines
                        .Where(k => lines.Contains(k.Key))
                        .Select(k => k.Value as M6Object)
                        .ToList();
                    bool hasResidentialFare = false;
                    ResidentialFare rf = null;
                    int i = m6Objects.Count;
                    while (i >= 0 || rf == null)
                    {
                        foreach (M6Object m6Object in m6Objects)
                        {
                            ResidentialFare rft = new ResidentialFare(m6Object);
                            if (rft.DocumentNumber != null)
                            {
                                rf = rft;
                                hasResidentialFare = true;
                            }
                            i--;
                        }
                    }
                    PassengerInformations.ResidentialFare = rf;
                    PassengerInformations.ResidentialFareForSpain = hasResidentialFare;
                }
            }
            else
            {
                PassengerInformations.ResidentialFare = null;
                PassengerInformations.ResidentialFareForSpain = false;
            }
            ForPaxTranslation.Add("AIR", getForPaxAirTranslation());
        }

        private string getForPaxAirTranslation()
        {
            string iLine = PassengerInformations.PassengerInfoTranslation["AIR"];
            string tLine = PassengerInformations.TicketNumberTranslations["AIR"];
            string resident = ResidentialFareTranslation.Count > 0 ? ResidentialFareTranslation["AIR"] : null;
            string fmLine = AccountingInformations.CommissionTranslations["AIR"];
            string fpLine = AccountingInformations.FormOfPaymentTranslations["AIR"];
            string fvLine = PassengerInformations.TicketingCarrierTranslations["AIR"];
            string[] paxStrings = resident != null ?
                new List<string>() { iLine, tLine, resident, fmLine, fpLine, fvLine }.ToArray() :
                new List<string>() { iLine, tLine, fmLine, fpLine, fvLine }.ToArray();
            return string.Join("\r\n", paxStrings);
        }

    }

}
