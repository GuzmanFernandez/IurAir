using System.Text;
using System;
using IurAir.Domain.Iur;
using IurAir.Domain.Common;
using IurAir.Domain.TicketEmission;
using System.Linq;
using IurAir.Domain.Iur.Utilities;
using IurAir.Domain.Air.Shared;
using System.Globalization;
using System.Collections.Generic;

namespace IurAir.Domain.Mir.Lines.Header
{
    public class MirFileHeader
    {
        #region Before First Carriage Return (Bytes 00-4F)
        public string BASIC_ID { get; set; } = "T5"; // 2 bytes
        public string TRANSMITTING_SYSTEM { get; set; } // 2 bytes (1V=Apollo, 1G=Travelport+)
        public string IATA_ASSIGNED_NUMERIC_ACCOUNTING_CODE_AND_CHECK_DIGIT { get; set; } // 4 bytes
        public string MIR_TYPE_INDICATOR { get; set; } = "92"; // 2 bytes
        public int TOTAL_RECORD_SIZE { get; set; } // 5 bytes
        public int RECORD_MESSAGE_SEQUENCE_NUMBER { get; set; } // 5 bytes
        public string DATE_OF_MIR_CREATION { get; set; } // 7 bytes (DDMMMYY)
        public string TIME_OF_MIR_CREATION { get; set; } // 5 bytes
        public string AIRLINE_CODE { get; set; } // 2 bytes
        public string AIRLINE_NUMBER { get; set; } // 3 bytes
        public string OFFICIAL_AIRLINE_NAME { get; set; } // 24 bytes
        public string DATE_OF_FIRST_TRAVEL { get; set; } // 7 bytes (DDMMMYY)
        public string INPUT_CRT_LNIATA_GTID { get; set; } // 6 bytes
        public string OUTPUT_DEVICE_GTID { get; set; } // 6 bytes
        #endregion

        #region First Carriage Return (Byte 50)
        public char CARRIAGE_RETURN_1 { get; set; } = '\r'; // 1 byte
        #endregion

        #region Between First and Second Carriage Return (Bytes 51-8F)
        public string BOOKING_AGENCY_ACCOUNT_CODE { get; set; } // 4 bytes
        public string ISSUING_TICKETING_AGENCY_ACCOUNT_CODE { get; set; } // 4 bytes
        public string AGENCY_ACCOUNT_NUMBER { get; set; } // 9 bytes
        public string RECORD_LOCATOR { get; set; } // 6 bytes
        public string RECORD_LOCATOR_FROM_ORIGINATING_CRS_AIRLINE { get; set; } // 6 bytes
        public string OTHER_CRS_AIRLINE_CODE { get; set; } // 2 bytes
        public char AUTOMATIC_MANUAL_INDICATOR { get; set; } // 1 byte
        public string BOOKING_AGENT_SIGN { get; set; } // 6 bytes
        public char SERVICE_BUREAU_INDICATOR { get; set; } // 1 byte
        public string TICKETING_AGENT_SIGN { get; set; } // 2 bytes
        public string AGENT_DUTY_CODE { get; set; } // 2 bytes
        public string PNR_BOOKING_FILE_CREATION_DATE { get; set; } // 7 bytes
        public int ELAPSED_PNR_BOOKING_FILE_HANDLING_TIME { get; set; } // 3 bytes
        public string DATE_OF_LAST_CHANGE_TO_PNR_BOOKING_FILE { get; set; } // 7 bytes
        public int NUMBER_OF_CHANGES_TO_PNR_BOOKING_FILE { get; set; } // 3 bytes
        #endregion

        #region Second Carriage Return (Byte 90)
        public char CARRIAGE_RETURN_2 { get; set; } = '\r'; // 1 byte
        #endregion

        #region Between Second and Third Carriage Return (Bytes 91-F0)
        public string CURRENCY_CODE { get; set; } // 3 bytes
        public decimal PARTY_FARE { get; set; } // 12 bytes
        public int NUMBER_OF_DECIMAL_PLACES_IN_CURRENCY { get; set; } // 1 byte
        public string CURRENCY_CODE_TAXES_COMMISSION { get; set; } // 3 bytes
        public decimal FIRST_TAX_AMOUNT { get; set; } // 8 bytes
        public string FIRST_TAX_CODE { get; set; } // 2 bytes
        public decimal SECOND_TAX_AMOUNT { get; set; } // 8 bytes
        public string SECOND_TAX_CODE { get; set; } // 2 bytes
        public decimal THIRD_TAX_AMOUNT { get; set; } // 8 bytes
        public string THIRD_TAX_CODE { get; set; } // 2 bytes
        public decimal FOURTH_TAX_AMOUNT { get; set; } // 8 bytes
        public string FOURTH_TAX_CODE { get; set; } // 2 bytes
        public decimal FIFTH_TAX_AMOUNT { get; set; } // 8 bytes
        public string FIFTH_TAX_CODE { get; set; } // 2 bytes
        public decimal COMMISSION_AMOUNT { get; set; } // 8 bytes
        public int COMMISSION_RATE { get; set; } // 4 bytes
        public string TOUR_CODE { get; set; } // 15 bytes
        #endregion

        #region Third Carriage Return (Byte F1)
        public char CARRIAGE_RETURN_3 { get; set; } = '\r'; // 1 byte
        #endregion

        #region Between Third and Fourth Carriage Return (Bytes F2-124)
        public char RETRANSMISSION { get; set; } // 1 byte
        public char MANUAL_PRICING { get; set; } // 1 byte
        public char FARES_PRICING_360 { get; set; } // 1 byte
        public char SAME_FARE_FOR_ALL_PASSENGERS { get; set; } // 1 byte
        public char STP_TICKET_ISSUED { get; set; } // 1 byte
        public char ATB_INDICATOR { get; set; } // 1 byte
        public char EXCHANGE_TICKET_INFORMATION { get; set; } // 1 byte
        public char CONJUNCTION_NUMBER_REQUIRED { get; set; } // 1 byte
        public char FARE_CONSTRUCTION_DATA_PRESENT { get; set; } // 1 byte
        public char SEAT_DATA_INFORMATION { get; set; } // 1 byte
        public char TINS_DATA_PRESENT { get; set; } // 1 byte
        public char TICKETING_COMMAND_USED { get; set; } // 1 byte
        public char SITI_SOTO_INDICATOR { get; set; } // 1 byte
        public char TRAVEL_ADVISORY_INDICATOR { get; set; } // 1 byte
        public char GROUP_MANAGER_DATA_PRESENT { get; set; } // 1 byte
        public char DIRECT_BOOKED_INDICATOR { get; set; } // 1 byte
        public char DOMESTIC_INTERNATIONAL_INDICATOR { get; set; } // 1 byte
        public string PLATING_CARRIER_CODE { get; set; } // 3 bytes
        public string ISO_COUNTRY_CODE_OF_AGENCY { get; set; } // 3 bytes
        public string DUAL_MIR_IDENTIFIER { get; set; } // 2 bytes
        public char SENDER_TARGET_INDICATOR { get; set; } // 1 byte
        public string PSEUDO_CITY_CODE_DUAL { get; set; } // 4 bytes
        public int DUAL_MIR_SEQUENCE_NUMBER { get; set; } // 5 bytes
        public string DUAL_MIR_GTID { get; set; } // 6 bytes
        public string STP_IDENTIFIER { get; set; } // 2 bytes
        public string HOST_PSEUDO_CITY_CODE { get; set; } // 4 bytes
        public string HOME_PSEUDO_CITY_CODE { get; set; } // 4 bytes
        #endregion

        #region Fourth Carriage Return (Byte 125)
        public char CARRIAGE_RETURN_4 { get; set; } = '\r'; // 1 byte
        #endregion

        #region Between Fourth and Sixth Carriage Return (Bytes 126-15A)
        public int NUMBER_OF_CUSTOMER_REMARKS { get; set; } // 3 bytes
        public int NUMBER_OF_CORPORATE_NAMES { get; set; } // 3 bytes
        public int NUMBER_OF_PASSENGER_ITEMS { get; set; } // 3 bytes
        public int NUMBER_OF_FREQUENT_FLYER_ITEMS { get; set; } // 3 bytes
        public int NUMBER_OF_TICKETED_PRICED_AIRLINE_SEGMENTS { get; set; } // 3 bytes
        public int NUMBER_OF_WAITLISTED_NON_PRICED_TICKETED_SEGMENTS { get; set; } // 3 bytes
        public int NUMBER_OF_SEAT_DATA_ITEMS { get; set; } // 3 bytes
        public int NUMBER_OF_FARE_SECTIONS { get; set; } // 3 bytes
        public int NUMBER_OF_TICKET_EXCHANGE_ITEMS { get; set; } // 3 bytes
        public int NUMBER_OF_FORM_OF_PAYMENT_ITEMS { get; set; } // 3 bytes
        public int NUMBER_OF_PHONE_ITEMS { get; set; } // 3 bytes
        public int NUMBER_OF_PASSENGER_ADDRESS_ITEMS { get; set; } // 3 bytes
        public int NUMBER_OF_BACKOFFICE_TICKET_REMARKS { get; set; } // 3 bytes
        public int NUMBER_OF_ASSOCIATED_UNASSOCIATED_REMARKS { get; set; } // 3 bytes
        public int NUMBER_OF_AUXILIARY_SEGMENTS { get; set; } // 3 bytes
        public int NUMBER_OF_LEISURESHOPPER_ITEMS { get; set; } // 3 bytes
        public string NDC_IDENTIFIER { get; set; } // 4 bytes
        public char NDC_INDICATOR { get; set; } // 1 byte
        #endregion

        #region Sixth Carriage Return (Byte 15B)
        public char CARRIAGE_RETURN_6 { get; set; } = '\r'; // 1 byte
        #endregion

        /// <summary>
        /// Constructor with required parameters
        /// </summary>
        public MirFileHeader(string iataCode, int sequenceNumber)
        {
            TRANSMITTING_SYSTEM = "1G";
            IATA_ASSIGNED_NUMERIC_ACCOUNTING_CODE_AND_CHECK_DIGIT = iataCode;
            RECORD_MESSAGE_SEQUENCE_NUMBER = sequenceNumber;
            DATE_OF_MIR_CREATION = DateTime.Now.ToString("ddMMMyy").ToUpper();
            TIME_OF_MIR_CREATION = DateTime.Now.ToString("HHmm");
        }

        /// <summary>
        /// Maps values from an AdvIurSplitter object to the header properties
        /// </summary>
        public void MapFrom(AdvIurSplitter splitter)
        {
            if (splitter == null) throw new ArgumentNullException(nameof(splitter));

            bool isVoid = splitter.Headers.m0VoidData != null || new VariableDataCollector(splitter._document.RawLines.ToArray()).IsVoid;
            CarrierData carrier = new CarrierData(Vectors.defaultVector());
            DateTime firstDepDate;

            // Determine first travel date and carrier
            string depDate = splitter.Headers.m0Data?.departureDate ?? "01JAN25";
            int day, month;
            DateUtilities.DateFromDocument(depDate, out day, out month);
            ItineraryTrait trait = splitter.TicketedPassengers
                .Select(x => x.Itinerary)
                .SelectMany(it => it.Traits)
                .FirstOrDefault(tr => tr.DepartureDate.Month == month && tr.DepartureDate.Day == day);

            if (trait == null)
            {
                var m3Lines = splitter._document.m3Lines;
                if (m3Lines.Any())
                {
                    firstDepDate = m3Lines
                        .Select(m =>
                        {
                            var parsed = m.parseLine();
                            return parsed.ContainsKey("DepartureDate")
                                ? DateTime.ParseExact(parsed["DepartureDate"], "ddMMMyy", CultureInfo.InvariantCulture)
                                : DateTime.MaxValue; // Fallback if key is missing
                        })
                        .OrderBy(d => d)
                        .FirstOrDefault();
                    if (firstDepDate == DateTime.MaxValue) firstDepDate = DateTime.Now; // No valid dates found
                }
                else
                {
                    firstDepDate = DateTime.Now;
                }

                carrier.CarrierDesignator = splitter._document.m0Lines.FirstOrDefault()?.parseLine()?["ValidatingCarrierCode"] ?? "XX";
                carrier.CarrierCode = "000";
                carrier.CarrierName = "UNKNOWN AIRLINE";
            }
            else
            {
                firstDepDate = trait.DepartureDate.Date;
                carrier = trait.Carrier;
            }

            #region Before First Carriage Return
            TOTAL_RECORD_SIZE = CalculateTotalRecordSize(splitter);
            AIRLINE_CODE = carrier.CarrierDesignator;
            AIRLINE_NUMBER = carrier.CarrierCode;
            OFFICIAL_AIRLINE_NAME = AirUtil.PadString(carrier.CarrierName.ToUpper(), ' ', 24, false);
            DATE_OF_FIRST_TRAVEL = firstDepDate.ToString("ddMMMyy").ToUpper();
            INPUT_CRT_LNIATA_GTID = splitter.Headers.m0Data?.lnIata ?? "000000";
            OUTPUT_DEVICE_GTID = Properties.Settings.Default.OutputDeviceGtid ?? "999999";
            #endregion

            #region Between First and Second Carriage Return
            BOOKING_AGENCY_ACCOUNT_CODE = AirUtil.PadString(splitter.Headers.BookingAgency?.ToString().Substring(5), ' ', 4, false);
            ISSUING_TICKETING_AGENCY_ACCOUNT_CODE = AirUtil.PadString(splitter.Headers.BookingAgency?.ToString().Substring(5), ' ', 4, false);
            AGENCY_ACCOUNT_NUMBER = AirUtil.PadString(splitter.Headers.IataNr?.ToString(), ' ', 9, false);
            RECORD_LOCATOR = splitter.PnrFileInfo?.PnrName ?? "XXXXXX";
            RECORD_LOCATOR_FROM_ORIGINATING_CRS_AIRLINE = trait?.AirlinePnrLocator ?? "      ";
            OTHER_CRS_AIRLINE_CODE = "  ";
            AUTOMATIC_MANUAL_INDICATOR = ' ';
            BOOKING_AGENT_SIGN = AirUtil.PadString(splitter.Headers.BookingAgency?.ToString().Substring(5) + Properties.Settings.Default.AgentInitials, ' ', 6, false);
            SERVICE_BUREAU_INDICATOR = 'N';
            TICKETING_AGENT_SIGN = Properties.Settings.Default.AgentInitials ?? "XX";
            AGENT_DUTY_CODE = "AG";
            PNR_BOOKING_FILE_CREATION_DATE = splitter._document.DocumentDate?.toDate().ToString("ddMMMyy").ToUpper() ?? DateTime.Now.ToString("ddMMMyy").ToUpper();
            ELAPSED_PNR_BOOKING_FILE_HANDLING_TIME = 0;
            DATE_OF_LAST_CHANGE_TO_PNR_BOOKING_FILE = DateTime.Now.ToString("ddMMMyy").ToUpper();
            NUMBER_OF_CHANGES_TO_PNR_BOOKING_FILE = 1;
            #endregion

            #region Between Second and Third Carriage Return
            CURRENCY_CODE = isVoid ? "USD" : (Properties.Settings.Default.DefaultCurrency ?? "USD");
            PARTY_FARE = isVoid ? 0m : CalculateTotalFareAmount(splitter._document.m2Lines, splitter._document.M5Lines);
            NUMBER_OF_DECIMAL_PLACES_IN_CURRENCY = 2;
            CURRENCY_CODE_TAXES_COMMISSION = CURRENCY_CODE;
            MapTaxesAndCommission(splitter);
            TOUR_CODE = splitter._document.MGLines.FirstOrDefault()?.GetObject()?.TourCode ?? "";
            #endregion

            #region Between Third and Fourth Carriage Return
            RETRANSMISSION = 'N';
            MANUAL_PRICING = 'Y';
            FARES_PRICING_360 = 'N';
            SAME_FARE_FOR_ALL_PASSENGERS = HasSameFareForAllPassengers(splitter) ? 'Y' : 'N';
            STP_TICKET_ISSUED = 'N';
            ATB_INDICATOR = MapAtbIndicator("ELECTRONIC");
            EXCHANGE_TICKET_INFORMATION = isVoid ? 'V' : (splitter.DocumentType.Equals("Exchange") || splitter.DocumentType.Equals("EvenExchange") ? 'Y' : 'N');
            CONJUNCTION_NUMBER_REQUIRED = splitter.SegmentCount > 4 ? 'Y' : 'N';
            FARE_CONSTRUCTION_DATA_PRESENT = splitter._document.MGLines.Any(mg => mg.HasVarFareCalc) ? 'Y' : 'N';
            SEAT_DATA_INFORMATION = splitter.HasSeatData ? 'D' : 'A';
            TINS_DATA_PRESENT = splitter.IsTinsAgency ? 'Y' : 'N';
            TICKETING_COMMAND_USED = MapTicketingCommand(splitter.TicketingCommand);
            SITI_SOTO_INDICATOR = ' ';
            TRAVEL_ADVISORY_INDICATOR = splitter.HasTravelAdvisory ? 'Y' : 'N';
            GROUP_MANAGER_DATA_PRESENT = splitter.IsGroupBooking ? 'Y' : 'N';
            DIRECT_BOOKED_INDICATOR = splitter.IsDirectBooked ? 'Y' : 'N';
            DOMESTIC_INTERNATIONAL_INDICATOR = MapDomIntIndicator(splitter.IsDomestic, splitter.IsTransborder);
            PLATING_CARRIER_CODE = splitter.PlatingCarrierCode;
            ISO_COUNTRY_CODE_OF_AGENCY = splitter.AgencyCountryCode;
            DUAL_MIR_IDENTIFIER = splitter.IsDualMir ? "D:" : null;
            SENDER_TARGET_INDICATOR = splitter.IsDualMirSender ? 'S' : splitter.IsDualMirTarget ? 'T' : '\0';
            PSEUDO_CITY_CODE_DUAL = splitter.DualMirPseudoCityCode;
            DUAL_MIR_SEQUENCE_NUMBER = splitter.DualMirSequenceNumber ?? 0;
            DUAL_MIR_GTID = splitter.DualMirGtid;
            STP_IDENTIFIER = splitter.IsStpTicket ? "S:" : null;
            HOST_PSEUDO_CITY_CODE = splitter.HostPseudoCityCode;
            HOME_PSEUDO_CITY_CODE = splitter.HomePseudoCityCode;
            #endregion

            #region Between Fourth and Sixth Carriage Return
            NUMBER_OF_CUSTOMER_REMARKS = splitter._document.M7Lines.Count + splitter._document.M8Lines.Count;
            NUMBER_OF_CORPORATE_NAMES = 0;
            NUMBER_OF_PASSENGER_ITEMS = splitter._document.M1Lines.Count;
            NUMBER_OF_FREQUENT_FLYER_ITEMS = splitter._document.M1Lines.Count(m => !string.IsNullOrEmpty(m.parseLine()["FrequentFlyerNumber"]));
            NUMBER_OF_TICKETED_PRICED_AIRLINE_SEGMENTS = isVoid ? 0 : splitter._document.MGLines.Sum(mg => mg.Coupons.Count);
            NUMBER_OF_WAITLISTED_NON_PRICED_TICKETED_SEGMENTS = 0;
            NUMBER_OF_SEAT_DATA_ITEMS = splitter.HasSeatData ? splitter._document.M3Lines.Count : 0;
            NUMBER_OF_FARE_SECTIONS = splitter._document.M5Lines.Count;
            NUMBER_OF_TICKET_EXCHANGE_ITEMS = splitter._document.M5Lines.Count(m => m.GetObject() is M5NormOrExchangeObject mo && !string.IsNullOrEmpty(mo.ExchValidatingCarrier_Ticket));
            NUMBER_OF_FORM_OF_PAYMENT_ITEMS = splitter._document.M5Lines.Select(m => m.GetObject().FormOfPayment).Distinct().Count();
            NUMBER_OF_PHONE_ITEMS = new VariableDataCollector(splitter._document.RawLines.ToArray()).M0VarData.Count(kv => kv.Key.StartsWith("Phone") && !string.IsNullOrEmpty(kv.Value));
            NUMBER_OF_PASSENGER_ADDRESS_ITEMS = new VariableDataCollector(splitter._document.RawLines.ToArray()).M0VarData.Count(kv => kv.Key.StartsWith("Address") && !string.IsNullOrEmpty(kv.Value));
            NUMBER_OF_BACKOFFICE_TICKET_REMARKS = splitter._document.M9Lines.Count;
            NUMBER_OF_ASSOCIATED_UNASSOCIATED_REMARKS = splitter._document.MELines.Count;
            NUMBER_OF_AUXILIARY_SEGMENTS = 0;
            NUMBER_OF_LEISURESHOPPER_ITEMS = 0;
            NDC_IDENTIFIER = splitter._document.RawLines.Any(l => l.Contains("NDC")) ? "NDC:" : null;
            NDC_INDICATOR = NDC_IDENTIFIER != null ? 'Y' : '\0';
            #endregion
        }

        #region Helper Methods for Mapping
        private int CalculateTotalRecordSize(AdvIurSplitter splitter)
        {
            int headerSize = 348;
            int bodyEstimate = (splitter.TicketedPassengers.Count * 100) +
                               (splitter._document.MGLines.Sum(mg => mg.Coupons.Count) * 50) +
                               (splitter._document.M5Lines.Count * 60) +
                               (splitter._document.M7Lines.Count + splitter._document.M8Lines.Count + splitter._document.M9Lines.Count + splitter._document.MELines.Count) * 40;
            return headerSize + bodyEstimate;
        }

        public static decimal CalculateTotalFareAmount(List<M2Line> m2Lines, List<M5Line> m5Lines)
        {
            decimal total = m2Lines?.Select(line => line.GetObject() as M2Object)
                                   .Where(obj => obj != null && !string.IsNullOrEmpty(obj.totalFareAmount))
                                   .Sum(obj => ParseTotalFareAmount(obj.totalFareAmount)) ?? 0m;

            var refunds = m5Lines?.Select(line => line.GetObject())
                                 .OfType<M5RefundObject>()
                                 .Sum(r => decimal.TryParse(r.FareAmount, out decimal amt) ? amt : 0m) ?? 0m;
            total -= refunds;

            var fees = m5Lines?.Select(line => line.GetObject())
                              .OfType<M5ServiceFeeLineObject>()
                              .Sum(f => decimal.TryParse(f.FeeAmount, out decimal amt) ? amt : 0m) ?? 0m;
            total += fees;

            return total;
        }

        private void MapTaxesAndCommission(AdvIurSplitter splitter)
        {
            var m5Lines = splitter._document.M5Lines.Select(l => l.GetObject()).OfType<M5NormOrExchangeObject>().ToList();
            if (m5Lines.Any())
            {
                var taxes = m5Lines.SelectMany(m => new[] { m.Tax1, m.Tax2, m.Tax3 })
                                   .Where(t => !string.IsNullOrEmpty(t))
                                   .Select(t => decimal.TryParse(t, out decimal amt) ? amt : 0m)
                                   .ToList();
                if (taxes.Count > 0) { FIRST_TAX_AMOUNT = taxes[0]; FIRST_TAX_CODE = "TX"; }
                if (taxes.Count > 1) { SECOND_TAX_AMOUNT = taxes[1]; SECOND_TAX_CODE = "TX"; }
                if (taxes.Count > 2) { THIRD_TAX_AMOUNT = taxes[2]; THIRD_TAX_CODE = "TX"; }
                if (taxes.Count > 3) { FOURTH_TAX_AMOUNT = taxes[3]; FOURTH_TAX_CODE = "TX"; }
                if (taxes.Count > 4) { FIFTH_TAX_AMOUNT = taxes[4]; FIFTH_TAX_CODE = "TX"; }

                var commission = m5Lines.FirstOrDefault(m => !string.IsNullOrEmpty(m.CommissionPercentage));
                if (commission != null && decimal.TryParse(commission.CommissionPercentage, out decimal rate) && decimal.TryParse(commission.FareAmount, out decimal fare))
                {
                    COMMISSION_AMOUNT = fare * (rate / 100);
                    COMMISSION_RATE = (int)(rate * 100);
                }
            }
        }

        public static bool HasSameFareForAllPassengers(AdvIurSplitter splitter, bool useBaseFare = false)
        {
            if (splitter == null || !splitter.TicketedPassengers.Any())
                return true;

            var m2Objects = splitter.TicketedPassengers
                .Select(p => p._passenger.GetObject() as M2Object)
                .Where(m2 => m2 != null && (useBaseFare ? !string.IsNullOrEmpty(m2.fareAmount) : !string.IsNullOrEmpty(m2.totalFareAmount)))
                .ToList();

            if (m2Objects.Count <= 1)
                return true;

            var fares = m2Objects.Select(m2 => ParseFare(useBaseFare ? m2.fareAmount : m2.totalFareAmount)).ToList();
            decimal referenceFare = fares[0];
            return fares.All(fare => fare == referenceFare);
        }

        private static decimal ParseFare(string fare)
        {
            if (string.IsNullOrEmpty(fare))
                return 0m;

            string cleanedFare = new string(fare.Where(c => char.IsDigit(c) || c == '.').ToArray());
            return decimal.TryParse(cleanedFare, out decimal result) ? result : throw new FormatException($"Unable to parse fare: '{fare}'");
        }

        private static decimal ParseTotalFareAmount(string amount)
        {
            if (string.IsNullOrEmpty(amount))
                return 0m;

            amount = amount.Trim();
            return decimal.TryParse(amount, out decimal result) ? result : throw new FormatException($"Unable to parse totalFareAmount: '{amount}'");
        }

        private char MapAtbIndicator(string ticketType)
        {
            switch (ticketType?.ToUpper())
            {
                case "ELECTRONIC": return '5';
                case "ATB": return '1';
                case "NON_ATB": return '0';
                default: return '0';
            }
        }

        private char MapTicketingCommand(string command)
        {
            switch (command?.ToUpper())
            {
                case "HB":
                case "TKP": return 'H';
                case "DXD": return 'X';
                case "DND": return 'N';
                default: return 'H';
            }
        }

        private char MapDomIntIndicator(bool isDomestic, bool isTransborder)
        {
            if (isTransborder) return 'T';
            return isDomestic ? ' ' : 'X';
        }
        #endregion

        /// <summary>
        /// Generates the formatted header string
        /// </summary>
        public string GenerateHeader()
        {
            ValidateRequiredFields();

            var builder = new StringBuilder();

            #region Before First Carriage Return
            builder.Append(BASIC_ID.PadRight(2));
            builder.Append(TRANSMITTING_SYSTEM.PadRight(2));
            builder.Append(IATA_ASSIGNED_NUMERIC_ACCOUNTING_CODE_AND_CHECK_DIGIT.PadLeft(4, '0'));
            builder.Append(MIR_TYPE_INDICATOR.PadLeft(2, '0'));
            builder.Append(TOTAL_RECORD_SIZE.ToString().PadLeft(5, '0'));
            builder.Append(RECORD_MESSAGE_SEQUENCE_NUMBER.ToString().PadLeft(5, '0'));
            builder.Append(DATE_OF_MIR_CREATION?.PadRight(7, ' ') ?? new string(' ', 7));
            builder.Append(TIME_OF_MIR_CREATION?.PadRight(5, ' ') ?? new string(' ', 5));
            builder.Append(AIRLINE_CODE?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(AIRLINE_NUMBER?.PadLeft(3, '0') ?? "000");
            builder.Append(OFFICIAL_AIRLINE_NAME?.PadRight(24, ' ') ?? new string(' ', 24));
            builder.Append(DATE_OF_FIRST_TRAVEL?.PadRight(7, ' ') ?? new string(' ', 7));
            builder.Append(INPUT_CRT_LNIATA_GTID?.PadRight(6, ' ') ?? new string(' ', 6));
            builder.Append(OUTPUT_DEVICE_GTID?.PadRight(6, ' ') ?? new string(' ', 6));
            #endregion

            #region First Carriage Return
            builder.Append(CARRIAGE_RETURN_1);
            #endregion

            #region Between First and Second Carriage Return
            builder.Append(BOOKING_AGENCY_ACCOUNT_CODE?.PadRight(4, ' ') ?? new string(' ', 4));
            builder.Append(ISSUING_TICKETING_AGENCY_ACCOUNT_CODE?.PadRight(4, ' ') ?? new string(' ', 4));
            builder.Append(AGENCY_ACCOUNT_NUMBER?.PadRight(9, ' ') ?? new string(' ', 9));
            builder.Append(RECORD_LOCATOR?.PadRight(6, ' ') ?? new string(' ', 6));
            builder.Append(RECORD_LOCATOR_FROM_ORIGINATING_CRS_AIRLINE?.PadRight(6, ' ') ?? new string(' ', 6));
            builder.Append(OTHER_CRS_AIRLINE_CODE?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(AUTOMATIC_MANUAL_INDICATOR != '\0' ? AUTOMATIC_MANUAL_INDICATOR : ' ');
            builder.Append(BOOKING_AGENT_SIGN?.PadRight(6, ' ') ?? new string(' ', 6));
            builder.Append(SERVICE_BUREAU_INDICATOR != '\0' ? SERVICE_BUREAU_INDICATOR : ' ');
            builder.Append(TICKETING_AGENT_SIGN?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(AGENT_DUTY_CODE?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(PNR_BOOKING_FILE_CREATION_DATE?.PadRight(7, ' ') ?? new string(' ', 7));
            builder.Append(ELAPSED_PNR_BOOKING_FILE_HANDLING_TIME.ToString().PadLeft(3, '0'));
            builder.Append(DATE_OF_LAST_CHANGE_TO_PNR_BOOKING_FILE?.PadRight(7, ' ') ?? new string(' ', 7));
            builder.Append(NUMBER_OF_CHANGES_TO_PNR_BOOKING_FILE.ToString().PadLeft(3, '0'));
            #endregion

            #region Second Carriage Return
            builder.Append(CARRIAGE_RETURN_2);
            #endregion

            #region Between Second and Third Carriage Return
            builder.Append(CURRENCY_CODE?.PadRight(3, ' ') ?? new string(' ', 3));
            builder.Append(PARTY_FARE.ToString("F" + NUMBER_OF_DECIMAL_PLACES_IN_CURRENCY).Replace(".", "").PadLeft(12, '0'));
            builder.Append(NUMBER_OF_DECIMAL_PLACES_IN_CURRENCY.ToString().PadLeft(1, '0'));
            builder.Append(CURRENCY_CODE_TAXES_COMMISSION?.PadRight(3, ' ') ?? new string(' ', 3));
            builder.Append(FIRST_TAX_AMOUNT.ToString("F2").Replace(".", "").PadLeft(8, '0'));
            builder.Append(FIRST_TAX_CODE?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(SECOND_TAX_AMOUNT.ToString("F2").Replace(".", "").PadLeft(8, '0'));
            builder.Append(SECOND_TAX_CODE?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(THIRD_TAX_AMOUNT.ToString("F2").Replace(".", "").PadLeft(8, '0'));
            builder.Append(THIRD_TAX_CODE?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(FOURTH_TAX_AMOUNT.ToString("F2").Replace(".", "").PadLeft(8, '0'));
            builder.Append(FOURTH_TAX_CODE?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(FIFTH_TAX_AMOUNT.ToString("F2").Replace(".", "").PadLeft(8, '0'));
            builder.Append(FIFTH_TAX_CODE?.PadRight(2, ' ') ?? new string(' ', 2));
            builder.Append(COMMISSION_AMOUNT.ToString("F2").Replace(".", "").PadLeft(8, '0'));
            builder.Append(COMMISSION_RATE.ToString().PadLeft(4, '0'));
            builder.Append(TOUR_CODE?.PadRight(15, ' ') ?? new string(' ', 15));
            #endregion

            #region Third Carriage Return
            builder.Append(CARRIAGE_RETURN_3);
            #endregion

            #region Between Third and Fourth Carriage Return
            builder.Append(RETRANSMISSION != '\0' ? RETRANSMISSION : ' ');
            builder.Append(MANUAL_PRICING != '\0' ? MANUAL_PRICING : ' ');
            builder.Append(FARES_PRICING_360 != '\0' ? FARES_PRICING_360 : ' ');
            builder.Append(SAME_FARE_FOR_ALL_PASSENGERS != '\0' ? SAME_FARE_FOR_ALL_PASSENGERS : ' ');
            builder.Append(STP_TICKET_ISSUED != '\0' ? STP_TICKET_ISSUED : ' ');
            builder.Append(ATB_INDICATOR != '\0' ? ATB_INDICATOR : ' ');
            builder.Append(EXCHANGE_TICKET_INFORMATION != '\0' ? EXCHANGE_TICKET_INFORMATION : ' ');
            builder.Append(CONJUNCTION_NUMBER_REQUIRED != '\0' ? CONJUNCTION_NUMBER_REQUIRED : ' ');
            builder.Append(FARE_CONSTRUCTION_DATA_PRESENT != '\0' ? FARE_CONSTRUCTION_DATA_PRESENT : ' ');
            builder.Append(SEAT_DATA_INFORMATION != '\0' ? SEAT_DATA_INFORMATION : ' ');
            builder.Append(TINS_DATA_PRESENT != '\0' ? TINS_DATA_PRESENT : ' ');
            builder.Append(TICKETING_COMMAND_USED != '\0' ? TICKETING_COMMAND_USED : ' ');
            builder.Append(SITI_SOTO_INDICATOR != '\0' ? SITI_SOTO_INDICATOR : ' ');
            builder.Append(TRAVEL_ADVISORY_INDICATOR != '\0' ? TRAVEL_ADVISORY_INDICATOR : ' ');
            builder.Append(GROUP_MANAGER_DATA_PRESENT != '\0' ? GROUP_MANAGER_DATA_PRESENT : ' ');
            builder.Append(DIRECT_BOOKED_INDICATOR != '\0' ? DIRECT_BOOKED_INDICATOR : ' ');
            builder.Append(DOMESTIC_INTERNATIONAL_INDICATOR != '\0' ? DOMESTIC_INTERNATIONAL_INDICATOR : ' ');
            builder.Append(PLATING_CARRIER_CODE?.PadRight(3, ' ') ?? new string(' ', 3));
            builder.Append(ISO_COUNTRY_CODE_OF_AGENCY?.PadRight(3, ' ') ?? new string(' ', 3));
            if (!string.IsNullOrEmpty(DUAL_MIR_IDENTIFIER))
            {
                builder.Append(DUAL_MIR_IDENTIFIER.PadRight(2));
                builder.Append(SENDER_TARGET_INDICATOR != '\0' ? SENDER_TARGET_INDICATOR : ' ');
                builder.Append(PSEUDO_CITY_CODE_DUAL?.PadRight(4, ' ') ?? new string(' ', 4));
                builder.Append(DUAL_MIR_SEQUENCE_NUMBER.ToString().PadLeft(5, '0'));
                builder.Append(DUAL_MIR_GTID?.PadRight(6, ' ') ?? new string(' ', 6));
            }
            else
            {
                builder.Append(new string(' ', 18));
            }
            if (!string.IsNullOrEmpty(STP_IDENTIFIER))
            {
                builder.Append(STP_IDENTIFIER.PadRight(2));
                builder.Append(HOST_PSEUDO_CITY_CODE?.PadRight(4, ' ') ?? new string(' ', 4));
                builder.Append(HOME_PSEUDO_CITY_CODE?.PadRight(4, ' ') ?? new string(' ', 4));
            }
            else
            {
                builder.Append(new string(' ', 10));
            }
            #endregion

            #region Fourth Carriage Return
            builder.Append(CARRIAGE_RETURN_4);
            #endregion

            #region Between Fourth and Sixth Carriage Return
            builder.Append(NUMBER_OF_CUSTOMER_REMARKS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_CORPORATE_NAMES.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_PASSENGER_ITEMS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_FREQUENT_FLYER_ITEMS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_TICKETED_PRICED_AIRLINE_SEGMENTS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_WAITLISTED_NON_PRICED_TICKETED_SEGMENTS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_SEAT_DATA_ITEMS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_FARE_SECTIONS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_TICKET_EXCHANGE_ITEMS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_FORM_OF_PAYMENT_ITEMS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_PHONE_ITEMS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_PASSENGER_ADDRESS_ITEMS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_BACKOFFICE_TICKET_REMARKS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_ASSOCIATED_UNASSOCIATED_REMARKS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_AUXILIARY_SEGMENTS.ToString().PadLeft(3, '0'));
            builder.Append(NUMBER_OF_LEISURESHOPPER_ITEMS.ToString().PadLeft(3, '0'));
            builder.Append(NDC_IDENTIFIER?.PadRight(4, ' ') ?? new string(' ', 4));
            builder.Append(NDC_INDICATOR != '\0' ? NDC_INDICATOR : ' ');
            #endregion

            #region Sixth Carriage Return
            builder.Append(CARRIAGE_RETURN_6);
            #endregion

            return builder.ToString();
        }

        private void ValidateRequiredFields()
        {
            if (string.IsNullOrEmpty(TRANSMITTING_SYSTEM) || (TRANSMITTING_SYSTEM != "1V" && TRANSMITTING_SYSTEM != "1G"))
                throw new ArgumentException("TRANSMITTING_SYSTEM must be '1V' or '1G'");
            if (string.IsNullOrEmpty(IATA_ASSIGNED_NUMERIC_ACCOUNTING_CODE_AND_CHECK_DIGIT))
                throw new ArgumentException("IATA_ASSIGNED_NUMERIC_ACCOUNTING_CODE_AND_CHECK_DIGIT is required");
            if (TOTAL_RECORD_SIZE > 50000)
                throw new ArgumentException("TOTAL_RECORD_SIZE cannot exceed 50,000");
        }
    }
}