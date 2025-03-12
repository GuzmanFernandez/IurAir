using IurAir.Domain.Iur;
using IurAir.Domain.Mir.Lines;
using IurAir.Domain.TicketEmission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IurAir.Domain.Mir.Lines
{
    public class A02PassengerDataSection
    {
        #region Mandatory Fields (Up to A02C01)
        public string SectionLabel { get; set; } = "A02"; // A02SEC, 3 bytes, "A02"
        public string PassengerName { get; set; } // A02NME, 33 bytes, Passenger name
        public string TransactionNumber { get; set; } // A02TRN, 11 bytes, TCN Number

        // Ticket/Invoice Numbers (A02TIN, 22 bytes total)
        public string YearIndicator { get; set; } // A02YIN, 1 byte, Last digit of year
        public string TicketNumber { get; set; } // A02TKT, 10 bytes, Ticket number
        public string NumberOfTicketBooks { get; set; } // A02NBK, 2 bytes, Number of ticket books
        public string InvoiceNumber { get; set; } // A02INV, 9 bytes, Invoice number

        public string PassengerIdentificationCode { get; set; } // A02PIC, 6 bytes, *PIC or *PD
        public string AssociatedFareItemNumber { get; set; } // A02FIN, 2 bytes, Fare item number
        public string AssociatedExchangeItemNumber { get; set; } // A02EIN, 2 bytes, Exchange item number
        public char MultipleFiledFaresIndicator { get; set; } // A02FFN, 1 byte, Y/N (Travelport+ only)
        public char CarriageReturn1 { get; set; } = '\r'; // A02C01, 1 byte, First carriage return
        #endregion

        #region Optional Fields (After A02C01, if data exists)
        public string NameFieldRemarksIdentifier { get; set; } // A02PN1, 3 bytes, "NR:"
        public string NameFieldRemarks { get; set; } // A02PNR, 33 bytes, Name remarks

        public string PassengerTitleIdentifier { get; set; } // A02PT1, 2 bytes, "T:"
        public string PassengerTitle { get; set; } // A02PTL, 13 bytes, Title

        public string PassengerAgeIdentifier { get; set; } // A02PA1, 2 bytes, "A:"
        public int? PassengerAge { get; set; } // A02PA2, 3 bytes, Age

        public string PassengerGenderIdentifier { get; set; } // A02PG1, 2 bytes, "G:"
        public char? PassengerGender { get; set; } // A02PG2, 1 byte, 'F' or 'M'

        public string PassengerSmokingPreferenceIdentifier { get; set; } // A02PS1, 2 bytes, "S:"
        public char? PassengerSmokingPreference { get; set; } // A02PSP, 1 byte, 'Y' or 'N'

        public string PassengerCountryCodeIdentifier { get; set; } // A02PC1, 2 bytes, "C:"
        public string PassengerCountryCode { get; set; } // A02PCC, 2 bytes, Country code

        public string PassengerPassportNumberIdentifier { get; set; } // A02PP1, 2 bytes, "P:"
        public string PassengerPassportNumber { get; set; } // A02PPN, 33 bytes, Passport number

        public string PassengerPassportExpirationDateIdentifier { get; set; } // A02PD1, 2 bytes, "D:"
        public string PassengerPassportExpirationDate { get; set; } // A02PDE, 7 bytes, DDMMMYY

        public string StockControlNumberIdentifier { get; set; } // A02SCI, 3 bytes, "SC:"
        public string NumberOfDocumentsIssued { get; set; } // A02SCD, 2 bytes, Number of documents
        public string StockControlNumber { get; set; } // A02SCN, 11 bytes, Stock control number
        public string NumberOfAccountableDocuments { get; set; } // A02SCA, 4 bytes, Number of accountable documents

        public string FareQuoteStatusIdentifier { get; set; } // A02FQI, 3 bytes, "SI:"
        public char? FareQuoteStatusCode { get; set; } // A02FQS, 1 byte, Fare quote status

        public string ExpandedNameIdentifier { get; set; } // A02ENI, 3 bytes, "EN:"
        public string ExpandedName { get; set; } // A02EXN, 55 bytes, Expanded name

        public string PricingModifierIdentifier { get; set; } // A02PMI, 3 bytes, "PM:"
        public string PricingModifier { get; set; } // A02PRM, 10 bytes, Pricing modifier

        public string LastDateToPurchaseTicketIdentifier { get; set; } // A02TLI, 3 bytes, "TL:"
        public string LastDateToPurchaseTicket { get; set; } // A02TLD, 8 bytes, DDMMMYY

        public string Category35Identifier { get; set; } // A02C35, 4 bytes, "C35:"
        public char? Category35Indicator { get; set; } // A02I35, 1 byte, Category 35 indicator

        public string DateOfTicketIssueIdentifier { get; set; } // A02DTI, 3 bytes, "TD"
        public string DateOfTicketIssue { get; set; } // A02DTE, 7 bytes, DDMMMYY

        public string PassengerSpecificCommissionRateIdentifier { get; set; } // A02PRI, 4 bytes, "PCR:"
        public string PassengerSpecificCommissionRate { get; set; } // A02PCR, 4 bytes, Commission rate

        public string PassengerSpecificCommissionAmountIdentifier { get; set; } // A02PAI, 4 bytes, "PCA:"
        public string PassengerSpecificCommissionAmount { get; set; } // A02PCA, 11 bytes, Commission amount
        #endregion

        #region Carriage Returns
        public char FloatingCarriageReturn { get; set; } = '\r'; // A02C02, 1 byte, After last optional field
        public char EndOfSectionCarriageReturn { get; set; } = '\r'; // A02C03, 1 byte, End of section
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public A02PassengerDataSection()
        {
        }

        /// <summary>
        /// Maps values from an AdvIurSplitter object to the A02 section properties
        /// </summary>
        public void MapFrom(AdvIurSplitter splitter)
        {
            if (splitter == null) throw new ArgumentNullException(nameof(splitter));

            // Assuming one A02 section per passenger in TicketedPassengers; we'll use the first one as an example
            var passenger = splitter.TicketedPassengers.FirstOrDefault();
            if (passenger == null) return;

            #region Mandatory Fields
            PassengerName = passenger.PassengerInformations?.PassengerName?.Substring(0, Math.Min(passenger.PassengerInformations?.PassengerName?.Length ?? 0, 33));
            TransactionNumber = passenger.PassengerInformations?.TicketNumber?.PadLeft(11, '0'); // Assuming TicketNumber could be used for TCN
            YearIndicator = passenger.AccountingInformations?.ExtTicketRecord?.IssueDate?.Year.ToString().Last().ToString() ?? DateTime.Now.Year.ToString().Last().ToString();
            TicketNumber = passenger.PassengerInformations?.TicketNumber?.PadLeft(10, '0');
            NumberOfTicketBooks = "01"; // Default, as AdvIurSplitter doesn’t provide this directly
            InvoiceNumber = passenger.AccountingInformations?.InvoiceNumber?.Substring(0, Math.Min(passenger.AccountingInformations?.InvoiceNumber?.Length ?? 0, 9));
            PassengerIdentificationCode = passenger.PassengerInformations?.PassengerType != null ? $"*PIC" : null; // Simplified mapping
            AssociatedFareItemNumber = "01"; // Default, as no direct fare item mapping
            AssociatedExchangeItemNumber = passenger.AccountingInformations?.IsExchangeTicket ?? false ? "01" : "  ";
            MultipleFiledFaresIndicator = splitter.TicketedPassengers.Count > 1 ? 'Y' : 'N';
            #endregion

            #region Optional Fields
            // Name Field Remarks
            var remarks = passenger.PassengerInformations?.Remarks?.FirstOrDefault();
            if (!string.IsNullOrEmpty(remarks))
            {
                NameFieldRemarksIdentifier = "NR:";
                NameFieldRemarks = remarks.Substring(0, Math.Min(remarks.Length, 33));
            }

            // Passenger Title (not directly available, could be parsed from Name if format allows)
            if (!string.IsNullOrEmpty(passenger.PassengerInformations?.PassengerName) && passenger.PassengerInformations.PassengerName.Contains("/"))
            {
                var parts = passenger.PassengerInformations.PassengerName.Split('/');
                if (parts.Length > 1 && parts[1].Contains(" "))
                {
                    var title = parts[1].Split(' ')[1];
                    PassengerTitleIdentifier = "T:";
                    PassengerTitle = title.Substring(0, Math.Min(title.Length, 13));
                }
            }

            // Passenger Age (not directly available in AdvIurSplitter)
            // Assuming a hypothetical Age property could be added
            // PassengerAgeIdentifier = "A:";
            // PassengerAge = passenger.Age;

            // Passenger Gender (not directly available)
            // Assuming a hypothetical Gender property
            // PassengerGenderIdentifier = "G:";
            // PassengerGender = passenger.Gender;

            // Passenger Smoking Preference (not available)
            // PassengerSmokingPreferenceIdentifier = "S:";
            // PassengerSmokingPreference = passenger.SmokingPreference;

            // Country Code (could use AgencyCountryCode from Headers)
            if (!string.IsNullOrEmpty(splitter.Headers?.AgencyCountryCode))
            {
                PassengerCountryCodeIdentifier = "C:";
                PassengerCountryCode = splitter.Headers.AgencyCountryCode.Substring(0, Math.Min(splitter.Headers.AgencyCountryCode.Length, 2));
            }

            // Passport Number (not directly available)
            // Assuming a hypothetical PassportNumber property
            // PassengerPassportNumberIdentifier = "P:";
            // PassengerPassportNumber = passenger.PassengerInformations.PassportNumber;

            // Passport Expiration Date (not available)
            // PassengerPassportExpirationDateIdentifier = "D:";
            // PassengerPassportExpirationDate = passenger.PassengerInformations.PassportExpirationDate?.ToString("ddMMMyy").ToUpper();

            // Stock Control Information (not directly available, could use TicketNumber)
            if (!string.IsNullOrEmpty(passenger.PassengerInformations?.TicketNumber))
            {
                StockControlNumberIdentifier = "SC:";
                NumberOfDocumentsIssued = "01"; // Default assumption
                StockControlNumber = passenger.PassengerInformations?.TicketNumber.PadLeft(11, '0');
                NumberOfAccountableDocuments = "0001"; // Default assumption
            }

            // Fare Quote Status (simplified mapping from AccountingInformations)
            if (passenger.AccountingInformations?.ExtTicketRecord?.IsManualPricing ?? false)
            {
                FareQuoteStatusIdentifier = "SI:";
                FareQuoteStatusCode = 'B'; // Manually built fare
            }
            else
            {
                FareQuoteStatusIdentifier = "SI:";
                FareQuoteStatusCode = 'G'; // Autopriced by Travelport+
            }

            // Expanded Name (not directly available, could concatenate Name and Remarks)
            if (!string.IsNullOrEmpty(passenger.PassengerInformations?.PassengerName))
            {
                ExpandedNameIdentifier = "EN:";
                ExpandedName = $"{passenger.PassengerInformations.PassengerName} {remarks ?? ""}".Substring(0, Math.Min(55, passenger.PassengerInformations.PassengerName.Length + (remarks?.Length ?? 0) + 1));
            }

            // Pricing Modifier (not directly available)
            // PricingModifierIdentifier = "PM:";
            // PricingModifier = passenger.PricingModifier;

            // Last Date to Purchase Ticket (not directly available)
            // LastDateToPurchaseTicketIdentifier = "TL:";
            // LastDateToPurchaseTicket = passenger.LastPurchaseDate?.ToString("ddMMMyy").ToUpper();

            // Category 35 (not directly available)
            // Category35Identifier = "C35:";
            // Category35Indicator = passenger.Category35Indicator;

            // Date of Ticket Issue
            if (passenger.AccountingInformations?.ExtTicketRecord?.IssueDate.HasValue ?? false)
            {
                DateOfTicketIssueIdentifier = "TD";
                DateOfTicketIssue = passenger.AccountingInformations.ExtTicketRecord.IssueDate.Value.ToString("ddMMMyy").ToUpper();
            }

            // Commission Rate and Amount
            if (!string.IsNullOrEmpty(passenger.AccountingInformations?.ExtTicketRecord?.CommissionPercent))
            {
                PassengerSpecificCommissionRateIdentifier = "PCR:";
                PassengerSpecificCommissionRate = passenger.AccountingInformations.ExtTicketRecord.CommissionPercent.PadLeft(4, '0');
            }

            if (!string.IsNullOrEmpty(passenger.AccountingInformations?.ExtTicketRecord?.Commission.StringAmount))
            {
                PassengerSpecificCommissionAmountIdentifier = "PCA:";
                PassengerSpecificCommissionAmount = decimal.Parse(passenger.AccountingInformations.ExtTicketRecord.Commission.StringAmount).ToString("F2").Replace(".", "").PadLeft(11, '0');
            }
            #endregion
        }

        /// <summary>
        /// Generates the formatted A02 section string for a single passenger
        /// </summary>
        public string GenerateSection()
        {
            ValidateRequiredFields();

            var builder = new StringBuilder();

            #region Mandatory Fields
            builder.Append(SectionLabel.PadRight(3));
            builder.Append(PassengerName?.PadRight(33, ' ') ?? new string(' ', 33));
            builder.Append(TransactionNumber?.PadLeft(11, '0') ?? "00000000000");
            builder.Append(YearIndicator?.PadRight(1, ' ') ?? " ");
            builder.Append(TicketNumber?.PadLeft(10, '0') ?? "0000000000");
            builder.Append(NumberOfTicketBooks?.PadLeft(2, '0') ?? "00");
            builder.Append(InvoiceNumber?.PadRight(9, ' ') ?? new string(' ', 9));
            builder.Append(PassengerIdentificationCode?.PadRight(6, ' ') ?? new string(' ', 6));
            builder.Append(AssociatedFareItemNumber?.PadLeft(2, '0') ?? "  ");
            builder.Append(AssociatedExchangeItemNumber?.PadLeft(2, '0') ?? "  ");
            builder.Append(MultipleFiledFaresIndicator != '\0' ? MultipleFiledFaresIndicator : ' ');
            builder.Append(CarriageReturn1);
            #endregion

            #region Optional Fields
            if (!string.IsNullOrEmpty(NameFieldRemarksIdentifier) && !string.IsNullOrEmpty(NameFieldRemarks))
            {
                builder.Append(NameFieldRemarksIdentifier.PadRight(3));
                builder.Append(NameFieldRemarks.PadRight(33, ' '));
            }

            if (!string.IsNullOrEmpty(PassengerTitleIdentifier) && !string.IsNullOrEmpty(PassengerTitle))
            {
                builder.Append(PassengerTitleIdentifier.PadRight(2));
                builder.Append(PassengerTitle.PadRight(13, ' '));
            }

            if (!string.IsNullOrEmpty(PassengerAgeIdentifier) && PassengerAge.HasValue)
            {
                builder.Append(PassengerAgeIdentifier.PadRight(2));
                builder.Append(PassengerAge.Value.ToString().PadLeft(3, '0'));
            }

            if (!string.IsNullOrEmpty(PassengerGenderIdentifier) && PassengerGender.HasValue)
            {
                builder.Append(PassengerGenderIdentifier.PadRight(2));
                builder.Append(PassengerGender.Value);
            }

            if (!string.IsNullOrEmpty(PassengerSmokingPreferenceIdentifier) && PassengerSmokingPreference.HasValue)
            {
                builder.Append(PassengerSmokingPreferenceIdentifier.PadRight(2));
                builder.Append(PassengerSmokingPreference.Value);
            }

            if (!string.IsNullOrEmpty(PassengerCountryCodeIdentifier) && !string.IsNullOrEmpty(PassengerCountryCode))
            {
                builder.Append(PassengerCountryCodeIdentifier.PadRight(2));
                builder.Append(PassengerCountryCode.PadRight(2, ' '));
            }

            if (!string.IsNullOrEmpty(PassengerPassportNumberIdentifier) && !string.IsNullOrEmpty(PassengerPassportNumber))
            {
                builder.Append(PassengerPassportNumberIdentifier.PadRight(2));
                builder.Append(PassengerPassportNumber.PadRight(33, ' '));
            }

            if (!string.IsNullOrEmpty(PassengerPassportExpirationDateIdentifier) && !string.IsNullOrEmpty(PassengerPassportExpirationDate))
            {
                builder.Append(PassengerPassportExpirationDateIdentifier.PadRight(2));
                builder.Append(PassengerPassportExpirationDate.PadRight(7, ' '));
            }

            if (!string.IsNullOrEmpty(StockControlNumberIdentifier))
            {
                builder.Append(StockControlNumberIdentifier.PadRight(3));
                builder.Append(NumberOfDocumentsIssued?.PadLeft(2, '0') ?? "  ");
                builder.Append(StockControlNumber?.PadLeft(11, '0') ?? "00000000000");
                builder.Append(NumberOfAccountableDocuments?.PadLeft(4, '0') ?? "0000");
            }

            if (!string.IsNullOrEmpty(FareQuoteStatusIdentifier) && FareQuoteStatusCode.HasValue)
            {
                builder.Append(FareQuoteStatusIdentifier.PadRight(3));
                builder.Append(FareQuoteStatusCode.Value);
            }

            if (!string.IsNullOrEmpty(ExpandedNameIdentifier) && !string.IsNullOrEmpty(ExpandedName))
            {
                builder.Append(ExpandedNameIdentifier.PadRight(3));
                builder.Append(ExpandedName.PadRight(55, ' '));
            }

            if (!string.IsNullOrEmpty(PricingModifierIdentifier) && !string.IsNullOrEmpty(PricingModifier))
            {
                builder.Append(PricingModifierIdentifier.PadRight(3));
                builder.Append(PricingModifier.PadRight(10, ' '));
            }

            if (!string.IsNullOrEmpty(LastDateToPurchaseTicketIdentifier) && !string.IsNullOrEmpty(LastDateToPurchaseTicket))
            {
                builder.Append(LastDateToPurchaseTicketIdentifier.PadRight(3));
                builder.Append(LastDateToPurchaseTicket.PadRight(8, ' '));
            }

            if (!string.IsNullOrEmpty(Category35Identifier) && Category35Indicator.HasValue)
            {
                builder.Append(Category35Identifier.PadRight(4));
                builder.Append(Category35Indicator.Value);
            }

            if (!string.IsNullOrEmpty(DateOfTicketIssueIdentifier) && !string.IsNullOrEmpty(DateOfTicketIssue))
            {
                builder.Append(DateOfTicketIssueIdentifier.PadRight(3));
                builder.Append(DateOfTicketIssue.PadRight(7, ' '));
            }

            if (!string.IsNullOrEmpty(PassengerSpecificCommissionRateIdentifier) && !string.IsNullOrEmpty(PassengerSpecificCommissionRate))
            {
                builder.Append(PassengerSpecificCommissionRateIdentifier.PadRight(4));
                builder.Append(PassengerSpecificCommissionRate.PadLeft(4, '0'));
            }

            if (!string.IsNullOrEmpty(PassengerSpecificCommissionAmountIdentifier) && !string.IsNullOrEmpty(PassengerSpecificCommissionAmount))
            {
                builder.Append(PassengerSpecificCommissionAmountIdentifier.PadRight(4));
                builder.Append(PassengerSpecificCommissionAmount.PadLeft(11, '0'));
            }

            // Add floating carriage return if any optional data exists
            if (HasOptionalData())
            {
                builder.Append(FloatingCarriageReturn);
            }
            #endregion

            return builder.ToString();
        }

        /// <summary>
        /// Generates the full A02 section for multiple passengers, including end-of-section CR
        /// </summary>
        public static string GenerateFullSection(List<A02PassengerDataSection> passengers)
        {
            var builder = new StringBuilder();
            foreach (var passenger in passengers)
            {
                builder.Append(passenger.GenerateSection());
            }
            builder.Append('\r'); // A02C03 - End of section
            return builder.ToString();
        }

        private bool HasOptionalData()
        {
            return !string.IsNullOrEmpty(NameFieldRemarksIdentifier) ||
                   !string.IsNullOrEmpty(PassengerTitleIdentifier) ||
                   !string.IsNullOrEmpty(PassengerAgeIdentifier) ||
                   !string.IsNullOrEmpty(PassengerGenderIdentifier) ||
                   !string.IsNullOrEmpty(PassengerSmokingPreferenceIdentifier) ||
                   !string.IsNullOrEmpty(PassengerCountryCodeIdentifier) ||
                   !string.IsNullOrEmpty(PassengerPassportNumberIdentifier) ||
                   !string.IsNullOrEmpty(PassengerPassportExpirationDateIdentifier) ||
                   !string.IsNullOrEmpty(StockControlNumberIdentifier) ||
                   !string.IsNullOrEmpty(FareQuoteStatusIdentifier) ||
                   !string.IsNullOrEmpty(ExpandedNameIdentifier) ||
                   !string.IsNullOrEmpty(PricingModifierIdentifier) ||
                   !string.IsNullOrEmpty(LastDateToPurchaseTicketIdentifier) ||
                   !string.IsNullOrEmpty(Category35Identifier) ||
                   !string.IsNullOrEmpty(DateOfTicketIssueIdentifier) ||
                   !string.IsNullOrEmpty(PassengerSpecificCommissionRateIdentifier) ||
                   !string.IsNullOrEmpty(PassengerSpecificCommissionAmountIdentifier);
        }

        private void ValidateRequiredFields()
        {
            if (SectionLabel != "A02")
                throw new ArgumentException("SectionLabel must be 'A02'");
            if (string.IsNullOrEmpty(PassengerName))
                throw new ArgumentException("PassengerName is required");
        }
    }
}

// Example usage:
//public class Program
//{
//    public static void Main()
//    {
//        // Assuming an AdvIurSplitter instance from your domain
//        var document = new IurDocument(); // Placeholder, replace with actual initialization
//        var splitter = new AdvIurSplitter(document);

//        // Add a sample passenger for demonstration
//        splitter.TicketedPassengers.Add(new TicketDataRecord(new M1Object("M1..."), document)
//        {
//            PassengerInformations = new PassengerInfo { Name = "SMITH/JOHN MR", PassengerType = "ADT" },
//            TicketNumber = "1234567890",
//            AccountingInformations = new AccountingInfo
//            {
//                ExtTicketRecord = new ExtendedTicketRecord
//                {
//                    IssueDate = DateTime.Now,
//                    CommissionPercent = "5",
//                    Commission = new PriceData { StringAmount = "25.00" }
//                }
//            }
//        });

//        var a02Sections = new List<A02PassengerDataSection>();
//        foreach (var passenger in splitter.TicketedPassengers)
//        {
//            var a02 = new A02PassengerDataSection();
//            a02.MapFrom(splitter);
//            a02Sections.Add(a02);
//        }

//        string fullSection = A02PassengerDataSection.GenerateFullSection(a02Sections);
//        Console.WriteLine(fullSection);
//    }
//}