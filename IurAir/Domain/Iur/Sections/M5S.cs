using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IurAir.Domain.Iur.Sections
{
    public class M5S
    {
        public static string ALL = "All";
        public static string PerPerson = "Per Person";
        public static string ExchWithColl = "Exchange with Additional Collection";
        public static string EvenExch = "Even Exchange";
        public static string SecondFormOfPayment = "Second Form of Payment";
        public static string Refund = "Refund";
        public static string NormalTicket = "Normal Ticket";


        //private static Regex NormOrExchange = new Regex(@"M5(?<AccountingNr>\d\d)(?<InterfaceItemNr>(  )?(\d\d)?(AA)?(PP)?) (?<OperationType>[AEM ]{1})(?<ValidatingCarrierCode>[A-z0-9]{2})#(?<TicketNumber>\d{10})/(?<Commission>[0-9 .P]{7})/(?<fare>(?<FareCurrencyCode>[A-z. ]{3})?(?<FareAmount>[0-9. ]{8}))/(?<Tax1>[0-9. ]{7})/(?<Tax2>[0-9. ]{7}/)?(?<Tax3>[0-9. ]{7}/)?(?<PriceIndication>(ONE)?(PER)?(ALL)?)/(?<FormOfPayment>([A-z ]{2})[^/]*)/(?<ConjunctionDocsNr>\d)/(?<RoutingIndicator>[DFT])(?<OptionalTextData>[^/E])?(?<ETicketIndicator>/E)?(?<ProductNr>P-.*)?(?<VendorNr>V-.*)?(?<exhangeGrp>(-@)(?<ExchValidatingCarrier_Ticket>[0-9/]{14})?(?<ExchangedCoupons>\d*)?)?");
        private static Regex NormOrExchange = new Regex(@"M5(?<AccountingNr>\d\d)(?<InterfaceItemNr>(  )?(\d\d)?(AA)?(PP)?) (?<OperationType>[AEM ]{1})(?<ValidatingCarrierCode>[A-z0-9]{2})#(?<TicketNumber>\d{10})/(?<Commission>[0-9 .P]*)/(?<fare>(?<FareCurrencyCode>[A-z. ]{3})?(?<FareAmount>[0-9. ]*))/(?<Tax1>[0-9. ]*)/(?<Tax2>[0-9. ]*/)?(?<Tax3>[0-9. ]*/)?(?<PriceIndication>(ONE)?(PER)?(ALL)?)/(?<FormOfPayment>([A-z ]{2})[^/]*)/(?<ConjunctionDocsNr>\d)/(?<RoutingIndicator>[DFT])(?<OptionalTextData>[^/E])?(?<ETicketIndicator>/E)?(?<ProductNr>P-.*)?(?<VendorNr>V-.*)?(?<exhangeGrp>(-@)(?<ExchValidatingCarrier_Ticket>[0-9/]{14})?(?<ExchangedCoupons>\d*)?)?");
        private static Regex RefundRegex = new Regex(@"M5(?<AccountingNr>\d\d)(?<InterfaceItemNr>(  )?(\d\d)?(AA)?(PP)?) (?<OperationType>R)(?<ValidatingCarrierCode>[A-z0-9]{2})([#/]{1})(?<TicketNumber>\d{10})/(?<Commission>[P0-9 .]{7})/(?<fare>(?<FareCurrencyCode>[A-z. ]{3})?(?<FareAmount>[0-9. ]*))/(?<Tax1>[0-9. ]*/)(?<Tax2>[0-9. ]{7}/)?(?<Tax3>[0-9. ]{7}/)?(?<PriceIndication>(ONE)?(PER)?(ALL)?)/(?<FormOfPayment>([A-z ]{2})[^/]*)/(?<ConjunctionDocsNr>\d)/(?<CouponsNrIndicator>\d)/(?<RefundType>[FP])/(?<RoutingIndicator>[DFT])/(?<OptionalTextData>.*)?");

        public static Dictionary<String,String> parseLine(string line)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Regex toExclude = new Regex(@"\b[0-9]\b");
            if (NormOrExchange.IsMatch(line))
            {
                Match m = NormOrExchange.Match(line);
                var groupNames = NormOrExchange.GetGroupNames();
                foreach (string g in groupNames)
                {
                    if (!toExclude.IsMatch(g))
                    {
                        switch (g)
                        {
                            case "InterfaceItemNr":
                                result.Add(g, parseInterfaceItemNr(m.Groups[g].Value));
                                break;
                            case "OperationType":
                                result.Add(g, parseOpType(m.Groups[g].Value));
                                break;
                            default:
                                result.Add(g, m.Groups[g].Value);
                                break;
                        }
                    }
                }
            }
            if (RefundRegex.IsMatch(line))
            {
                Match m = RefundRegex.Match(line);
                var groupNames = RefundRegex.GetGroupNames();
                foreach (string g in groupNames)
                {
                    if (!toExclude.IsMatch(g))
                    {
                        switch (g)
                        {
                            case "InterfaceItemNr":
                                result.Add(g, parseInterfaceItemNr(m.Groups[g].Value));
                                break;
                            case "OperationType":
                                result.Add(g, parseOpType(m.Groups[g].Value));
                                break;
                            default:
                                result.Add(g, m.Groups[g].Value);
                                break;
                        }
                    }
                }
            }
            return result;
        }

        private static string parseInterfaceItemNr(string interfaceItNr) { 
            if(interfaceItNr == "AA")
            {
                return ALL;
            }
            if(interfaceItNr == "PP")
            {
                return PerPerson;
            }
            else
            {
                return interfaceItNr;
            }
        }

        private static string parseOpType(string opType) 
        { 
            if(opType == "A")
            {
                return ExchWithColl;
            }
            if(opType == "E")
            {
                return EvenExch;
            }
            if(opType == "M")
            {
                return SecondFormOfPayment;
            }
            if(opType == "R")
            {
                return Refund;
            }
            else
            {
                return NormalTicket;
            }
        }
    }
}
