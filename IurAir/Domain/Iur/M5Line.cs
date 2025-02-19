using IurAir.Domain.Iur.Sections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static IurAir.Domain.Iur.M5RefundObject;
using static System.Windows.Forms.Design.AxImporter;

namespace IurAir.Domain.Iur
{
    public class M5Line : IurLine
    {
        public string line { get; }
        public M5Line(string rawLine) : base(rawLine, "M5", 0, new List<LineSection>())
        {
            this.line = rawLine;
        }

        public override Dictionary<string, string> parseLine()
        {
            return M5S.parseLine(line);
        }

        public override IurObject getObject()
        {
            bool isRefund = line[7] == 'R';
            bool isServiceFee = line[10] == '@';
            if (isRefund)
            {
                return new M5RefundObject(this);
            }
            else if (isServiceFee)
            {
                return new M5ServiceFeeLineObject(this.line);
            }
            else
            {
                return new M5NormOrExchangeObject(this);
            }
        }
    }

    public class M5ServiceFeeLineObject : IurObject
    {
        //M501AA XB@0050672756/10.00/   10.00/   0.00/ALL/CA PEREZ SERRA SALVADOR MR/1/D
        public string AccountingNr { get; set; } = "";
        public string AccountingType { get; set; } = "";
        public string OpType { get; set; } = "";
        public string FeeEmitterCode { get; set; } = "";
        public string FeeNumber { get; set; } = "";
        public string FeeAmount { get; set; } = "";
        public string PriceIndication { get; set; } = "";
        public string FormOfPayment { get; set; } = "";
        private readonly string pattern = @"M5(?<InterfaceNr>\d+)(?<AccountingType>[A-z0-9]{2})(?<OpType>[ AEM]{1}) ?(?<FeeEmitter>[A-Z]{2})@(?<FeeNumber>[A-z0-9]*)\/(?<Commision>[0-9. ]*)\/(?<CurrCode>[A-z ]{3})(?<FareAmt>[0-9.]*)\/(?<Taxes>(?<CurrCode>(?!(ONE|PER|ALL))[A-z]{3})?(?<Amt>[0-9. ]*)\/){1,3}(?<PriceType>ONE|PER|ALL)\/(?<PaymentData>(?<FormOfPayment>[A-z0-9]*)? ?(?<PaxData>[A-z0-9 ]*)?)\/(?<ConjDocs>\d*)\/(?<RoutingInd>D|F|T)?";
        public M5ServiceFeeLineObject(string line)
        {
            Match match = Regex.Match(line, pattern);
            if (match.Success)
            {
                foreach(Group group in match.Groups)
                {
                    if (group.Success)
                    {
                        if(group.Name == "InterfaceNr")
                        {
                            AccountingNr = group.Value;
                        }
                        if (group.Name == "AccountingType")
                        {
                            AccountingType = group.Value;
                        }
                        if(group.Name == "OpType")
                        {
                            OpType = group.Value;
                        }
                        if(group.Name == "FeeEmitter")
                        {
                            FeeEmitterCode = group.Value;
                        }
                        if(group.Name == "FeeNumber")
                        {
                            FeeNumber = group.Value;
                        }
                        if(group.Name == "FareAmt")
                        {
                            FeeAmount = group.Value;
                        }
                        if(group.Name == "PriceType")
                        {
                            PriceIndication = group.Value;
                        }
                        if(group.Name == "FormOfPayment")
                        {
                            FormOfPayment = group.Value;
                        }
                    }
                }
            }
        }
    }

    public class M5NormOrExchangeObject : IurObject
    {
        public string AccountingNr { get; set; }
        public string InterfaceItemNr { get; set; }
        public string OperationType { get; set; }
        public string ValidatingCarrierCode { get; set; }
        public string TicketNumber { get; set; }
        public string CommissionPercentage { get; set; }
        public string FareCurrencyCode { get; set; }
        public string FareAmount { get; set; }
        public string Tax1 { get; set; }
        public string Tax2 { get; set; }
        public string Tax3 { get; set; }
        public string PriceIndication { get; set; }
        public string FormOfPayment { get; set; }
        public string ConjunctionDocsNr { get; set; }
        public string RoutingIndicator { get; set; }
        public string OptionalTextData { get; set; }
        public string ElectronicTicketIndicator { get; set; }
        public string VendorNumber { get; set; }
        public string ProductType { get; set; }
        public string ExchValidatingCarrier_Ticket { get; set; }
        public string ExchangedCoupons { get; set; }

        public M5NormOrExchangeObject(M5Line line)
        {
            Dictionary<string, string> parsed = line.parseLine();
            string cp = "";
            if (parsed.ContainsKey("Commission"))
            {
                var comm = parsed["Commission"];
                if(comm != null)
                {
                    comm = comm.Trim();
                    if (comm.StartsWith("."))
                    {
                        cp = $"0{comm}".Trim();
                    }
                    else
                    {
                        cp = comm.Trim();
                    }
                }
            }
            AccountingNr = parsed.ContainsKey("AccountingNr") ? parsed["AccountingNr"].Trim() : "";
            InterfaceItemNr = parsed.ContainsKey("InterfaceItemNr") ? parsed["InterfaceItemNr"].Trim() : "";
            OperationType = parsed.ContainsKey("OperationType") ? parsed["OperationType"].Trim() : "";
            ValidatingCarrierCode = parsed.ContainsKey("ValidatingCarrierCode") ? parsed["ValidatingCarrierCode"].Trim() : "";
            TicketNumber = parsed.ContainsKey("TicketNumber") ? parsed["TicketNumber"].Trim() : "";
            CommissionPercentage = cp;
            FareCurrencyCode = parsed.ContainsKey("FareCurrencyCode") ? parsed["FareCurrencyCode"].Trim() : "";
            FareAmount = parsed.ContainsKey("FareAmount") ? parsed["FareAmount"].Trim() : "";
            Tax1 = parsed.ContainsKey("Tax1") ? parsed["Tax1"].Trim() : "";
            Tax2 = parsed.ContainsKey("Tax2") ? parsed["Tax2"].Trim() : "";
            Tax3 = parsed.ContainsKey("Tax3") ? parsed["Tax3"].Trim() : "";
            PriceIndication = parsed.ContainsKey("PriceIndication") ? parsed["PriceIndication"].Trim() : "";
            FormOfPayment = parsed.ContainsKey("FormOfPayment") ? parsed["FormOfPayment"].Trim() : "";
            ConjunctionDocsNr = parsed.ContainsKey("ConjunctionDocsNr") ? parsed["ConjunctionDocsNr"].Trim() : "";
            RoutingIndicator = parsed.ContainsKey("RoutingIndicator") ? parsed["RoutingIndicator"].Trim() : "";
            OptionalTextData = parsed.ContainsKey("OptionalTextData") ? parsed["OptionalTextData"].Trim() : "";
            ElectronicTicketIndicator = parsed.ContainsKey("ElectronicTicketIndicator") ? parsed["ElectronicTicketIndicator"].Trim() : "";
            VendorNumber = parsed.ContainsKey("VendorNumber") ? parsed["VendorNumber"].Trim() : "";
            ProductType = parsed.ContainsKey("ProductType") ? parsed["ProductType"].Trim() : "";
            ExchValidatingCarrier_Ticket = parsed.ContainsKey("ExchValidatingCarrier_Ticket") ? parsed["ExchValidatingCarrier_Ticket"].Trim() : "";
            ExchangedCoupons = parsed.ContainsKey("ExchangedCoupons") ? parsed["ExchangedCoupons"].Trim() : "";
        }

        public string LineExplanation()
        {
            string refersTo =
                GetAccountingType() ==
                AccountingType.Normal ? $"It refers to passenger identified by ID '{InterfaceItemNr}'" :
                GetAccountingType() ==
                AccountingType.PerPerson ? $"It's intended to be Per Person " :
                GetAccountingType() ==
                AccountingType.All ? $"It reports the value of All invoices" :
                "Could not be correctly parsed";
            string pricing = getInvoiceIndicator() ==
                InvoiceIndicator.ONE ? "It's the price of this single item tax excluded" :
                getInvoiceIndicator() ==
                InvoiceIndicator.PER ? "It's the price PER item tax excluded (needs to be multiplied for passenger number)" :
                getInvoiceIndicator() ==
                InvoiceIndicator.ALL ? "It's the total price of ALL items tax excluded" :
                "Could not be correctly parsed";
            return $"This line is a {getOperationType().ToString()}. {refersTo}. {FareCurrencyCode} {FareAmount} {pricing}.";
        }

        public M5OperationType getOperationType()
        {
            if (this.OperationType.Equals(M5S.NormalTicket))
            {
                return M5OperationType.NormalTicket;
            }
            if (this.OperationType.Equals(M5S.EvenExch))
            {
                return M5OperationType.EvenExchange;
            }
            if (this.OperationType.Equals(M5S.ExchWithColl))
            {
                return M5OperationType.ExchangeWithCollection;
            }
            if (this.OperationType.Equals(M5S.SecondFormOfPayment))
            {
                return M5OperationType.SecondFOP;
            }
            if (this.OperationType.Equals(M5S.Refund))
            {
                return M5OperationType.Refund;
            }
            return M5OperationType.Undefined;
        }

        public AccountingType GetAccountingType()
        {
            if (this.InterfaceItemNr.Equals(M5S.ALL))
            {
                return AccountingType.All;
            }
            if (this.InterfaceItemNr.Equals(M5S.PerPerson))
            {
                return AccountingType.PerPerson;
            }
            return AccountingType.Normal;
        }

        public InvoiceIndicator getInvoiceIndicator()
        {
            if (this.PriceIndication.Equals("ONE"))
            {
                return InvoiceIndicator.ONE;
            }
            if (this.PriceIndication.Equals("PER"))
            {
                return InvoiceIndicator.PER;
            }
            if (this.PriceIndication.Equals("ALL"))
            {
                return InvoiceIndicator.ALL;
            }
            return InvoiceIndicator.UNDEFINED;

        }

        public PriceData BaseFare()
        {
            return new PriceData()
            {
                Amount = FareAmount,
                Currency = FareCurrencyCode,
                Sign = "+"
            };
        }

        public PriceData TotalFare()
        {
            return new PriceData()
            {
                Amount = (float.Parse(FareAmount) + float.Parse(Tax1)).ToString("0.00"),
                Currency = FareCurrencyCode,
                Sign = "+"
            };
        }
    }

    class FormOfPayment
    {
        public FormOfPayment(string m5FormOfPayment)
        {

        }
    }

    public class M5RefundObject : IurObject
    {
        public string AccountingNr { get; set; }
        public string InterfaceItemNr { get; set; }
        public string RefundIndicator { get; set; }
        public string ValidatingCarrierCode { get; set; }
        public string TicketNumber { get; set; }
        public string CommissionPercentage { get; set; }
        public string FareCurrencyCode { get; set; }
        public string FareAmount { get; set; }
        public string Tax1 { get; set; }
        public string Tax2 { get; set; }
        public string Tax3 { get; set; }
        public string PriceIndication { get; set; }
        public string FormOfPayment { get; set; }
        public string ConjunctionDocsNr { get; set; }
        public string CouponsNrIndicator { get; set; }
        public string RefundType { get; set; }
        public string FormerInvoiceNr { get; set; }
        public string RoutingIndicator { get; set; }
        public string OptionalTextData { get; set; }

        public M5RefundObject(M5Line line)
        {
            Dictionary<string, string> parsed = line.parseLine();
            string cp = "";
            if (parsed.ContainsKey("Commission"))
            {
                var comm = parsed["Commission"];
                if (comm != null)
                {
                    comm = comm.Trim();
                    if (comm.StartsWith("."))
                    {
                        cp = $"0{comm}".Trim();
                    }
                    else
                    {
                        cp = comm.Trim();
                    }
                }
            }

            AccountingNr = parsed.ContainsKey("AccountingNr") ? parsed["AccountingNr"].Trim() : "";
            InterfaceItemNr = parsed.ContainsKey("InterfaceItemNr") ? parsed["InterfaceItemNr"].Trim() : "";
            ValidatingCarrierCode = parsed.ContainsKey("ValidatingCarrierCode") ? parsed["ValidatingCarrierCode"].Trim() : "";
            TicketNumber = parsed.ContainsKey("TicketNumber") ? parsed["TicketNumber"].Trim() : "";
            CommissionPercentage = cp;
            FareCurrencyCode = parsed.ContainsKey("FareCurrencyCode") ? parsed["FareCurrencyCode"].Trim() : "";
            FareAmount = parsed.ContainsKey("FareAmount") ? parsed["FareAmount"].Trim() : "";
            Tax1 = parsed.ContainsKey("Tax1") ? parsed["Tax1"].Trim() : "";
            Tax2 = parsed.ContainsKey("Tax2") ? parsed["Tax2"].Trim() : "";
            Tax3 = parsed.ContainsKey("Tax3") ? parsed["Tax3"].Trim() : "";
            PriceIndication = parsed.ContainsKey("PriceIndication") ? parsed["PriceIndication"].Trim() : "";
            FormOfPayment = parsed.ContainsKey("FormOfPayment") ? parsed["FormOfPayment"].Trim() : "";
            ConjunctionDocsNr = parsed.ContainsKey("ConjunctionDocsNr") ? parsed["ConjunctionDocsNr"].Trim() : "";
            RoutingIndicator = parsed.ContainsKey("RoutingIndicator") ? parsed["RoutingIndicator"].Trim() : "";
            OptionalTextData = parsed.ContainsKey("OptionalTextData") ? parsed["OptionalTextData"].Trim() : "";
        }

            public string LineExplanation()
        {
            return "";
        }
    }

    //public static string ALL = "All";
    //public static string PerPerson = "Per Person";
    //public static string ExchWithColl = "Exchange with Additional Collection";
    //public static string EvenExch = "Even Exchange";
    //public static string SecondFormOfPayment = "Second Form of Payment";
    //public static string Refund = "Refund";
    //public static string NormalTicket = "Normal Ticket";

    public enum M5OperationType
    {
        NormalTicket,
        EvenExchange,
        ExchangeWithCollection,
        Refund,
        SecondFOP,
        Undefined
    }

    public enum AccountingType
    {
        Normal,
        All,
        PerPerson,
        Undefined
    }

    public enum InvoiceIndicator
    {
        ONE,
        PER,
        ALL,
        UNDEFINED
    }
}
