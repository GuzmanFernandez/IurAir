using CommunityToolkit.Mvvm.ComponentModel;
using IurAir.Domain.Iur;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IurAir.Models.ObservablePassenger;

namespace IurAir.Models
{


    public class Passenger
    {
        public string PassengerName { get; set; }
        public string PassengerCategory { get; set; }
        public string InterfaceNr { get; set; }
        public string TicketNumber { get; set; }
    }

    public class ObservablePassenger : ObservableObject
    {
        private readonly Passenger _Passenger;

        public ObservablePassenger(Passenger passenger)
        {
            _Passenger = passenger;
        }

        public string Name
        {
            get => _Passenger.PassengerName;
            set => SetProperty(_Passenger.PassengerName, value, _Passenger, (p, n) => p.PassengerName = n);
        }


        public string Category
        {
            get => _Passenger.PassengerCategory;
            set => SetProperty(_Passenger.PassengerCategory, value, _Passenger, (p, c) => p.PassengerCategory = c);
        }

        public string InterfaceNr
        {
            get => _Passenger.InterfaceNr;
            set => SetProperty(_Passenger.InterfaceNr, value, _Passenger, (p, c) => p.InterfaceNr = c);
        }

        public string TicketNr
        {
            get => _Passenger.TicketNumber;
            set => SetProperty(_Passenger.TicketNumber, value, _Passenger, (p, c) => p.TicketNumber = c);
        }
    }

    public class Itinerary
    {
        public string InterfaceNr { get; set; }
        public string FlightNumber { get; set; }
        public string Carrier { get; set; }
        public string DepartureCity { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureCountry { get; set; }
        public DateTime DepartureTime { get; set; }

        public string ArrivalCity { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalCountry { get; set; }
        public DateTime ArrivalTime { get; set; }
    }

    public class ObservableItinerary : ObservableObject
    {
        private readonly Itinerary _Itinerary;

        public ObservableItinerary(Itinerary itinerary) => this._Itinerary = itinerary;

        public string InterfaceNr
        {
            get => _Itinerary.InterfaceNr;
            set => SetProperty(_Itinerary.InterfaceNr, value, _Itinerary, (i, v) => i.InterfaceNr = v);
        }

        public string Carrier
        {
            get => _Itinerary.Carrier;
            set => SetProperty(_Itinerary.Carrier, value, _Itinerary, (i, v) => i.Carrier = v);
        }

        public string FlightNumber
        {
            get => _Itinerary.FlightNumber;
            set => SetProperty(_Itinerary.FlightNumber, value, _Itinerary, (i, v) => i.FlightNumber = v);
        }

        public string DepartureCity
        {
            get => _Itinerary.DepartureCity;
            set => SetProperty(_Itinerary.DepartureCity, value, _Itinerary, (i, v) => i.DepartureCity = v);
        }
        public string DepartureAirport
        {
            get => _Itinerary.DepartureAirport;
            set => SetProperty(_Itinerary.DepartureAirport, value, _Itinerary, (i, v) => i.DepartureAirport = v);
        }
        public string DepartureCountry
        {
            get => _Itinerary.DepartureCountry;
            set => SetProperty(_Itinerary.DepartureCountry, value, _Itinerary, (i, v) => i.DepartureCountry = v);
        }
        public DateTime DepartureTime
        {
            get => _Itinerary.DepartureTime;
            set => SetProperty(_Itinerary.DepartureTime, value, _Itinerary, (i, v) => i.DepartureTime = v);
        }
        public string ArrivalCity
        {
            get => _Itinerary.ArrivalCity;
            set => SetProperty(_Itinerary.ArrivalCity, value, _Itinerary, (i, v) => i.ArrivalCity = v);
        }
        public string ArrivalAirport
        {
            get => _Itinerary.ArrivalAirport;
            set => SetProperty(_Itinerary.ArrivalAirport, value, _Itinerary, (i, v) => i.ArrivalAirport = v);
        }
        public string ArrivalCountry
        {
            get => _Itinerary.ArrivalCountry;
            set => SetProperty(_Itinerary.ArrivalCountry, value, _Itinerary, (i, v) => i.ArrivalCountry = v);
        }
        public DateTime ArrivalTime
        {
            get => _Itinerary.ArrivalTime;
            set => SetProperty(_Itinerary.ArrivalTime, value, _Itinerary, (i, v) => i.ArrivalTime = v);
        }
    }

    public class Fares
    {
        public String PassengerType { get; set; }
        public PriceData BaseFare { get; set; }
        public PriceData TotalFare { get; set; }
        public PriceData TotalTaxes { get; set; }
        public string TicketNr { get; set; }
    }

    public class ObservableFares : ObservableObject
    {
        public readonly Fares _Fares;
        public ObservableFares(Fares f) => _Fares = f;

        public String PassengerType
        {
            get => _Fares.PassengerType;
            set => SetProperty(_Fares.PassengerType, value, _Fares, (f, v) => f.PassengerType = v);
        }
        public string TotalFareString
        {
            get => $"{_Fares.TotalFare.Currency} {_Fares.TotalFare.Amount}";
        }
        public string BaseFareString
        {
            get => $"{_Fares.BaseFare.Currency} {_Fares.BaseFare.Amount}";
        }
        public ObservablePriceData BaseFare
        {
            get => new ObservablePriceData(_Fares.BaseFare);
            set => SetProperty(new ObservablePriceData(_Fares.BaseFare), value, _Fares, (d, v) => d.BaseFare = new PriceData() { Sign = v.Sign, Currency = v.Currency, Amount = v.Amount });
        }
        public ObservablePriceData TotalFare
        {
            get => new ObservablePriceData(_Fares.TotalFare);
            set => SetProperty(new ObservablePriceData(_Fares.TotalFare), value, _Fares, (d, v) => d.TotalFare = new PriceData() { Sign = v.Sign, Currency = v.Currency, Amount = v.Amount });
        }
        public ObservablePriceData TotalTaxes
        {
            get => new ObservablePriceData(_Fares.TotalTaxes);
            set => SetProperty(new ObservablePriceData(_Fares.TotalTaxes), value, _Fares, (d, v) => d.TotalTaxes = new PriceData() { Sign = v.Sign, Currency = v.Currency, Amount = v.Amount });
        }

        public string TicketNr
        {
            get => _Fares.TicketNr;
            set => SetProperty(_Fares.TicketNr, value, _Fares, (f, v) => f.TicketNr = v);
        }
    }

    public class ObservablePriceData : ObservableObject
    {
        private readonly PriceData _PD;
        public ObservablePriceData(PriceData PD) => this._PD = PD;
        public string Sign
        {
            get => _PD.Sign;
            set => SetProperty(_PD.Sign, value, _PD, (p, v) => p.Sign = v);
        }
        public string Currency
        {
            get => _PD.Currency;
            set => SetProperty(_PD.Currency, value, _PD, (p, v) => p.Currency = v);
        }
        public string Amount
        {
            get => _PD.Amount;
            set => SetProperty(_PD.Amount, value, _PD, (p, v) => p.Amount = v);
        }
    }

    public class ObservableTax : ObservableObject
    {
        private readonly Tax _Tax;
        public ObservableTax(Tax tax)
        {
            _Tax = tax;
        }

        public string TaxCode
        {
            get => _Tax.TaxCode;
            set => SetProperty(_Tax.TaxCode, value, _Tax, (t, v) => t.TaxCode = v);
        }
        public ObservablePriceData Price
        {
            get => new ObservablePriceData(_Tax.Price);
            set => SetProperty(new ObservablePriceData(_Tax.Price), value, _Tax, (t, v) => t.Price = new PriceData() { Sign = v.Sign, Amount = v.Amount, Currency = v.Currency });
        }
    }

    public class Remark
    {
        public RemarkType Type { get; set; }
        public string RemarkContent { get; set; }
        public string AssociatedSegment { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Remark remark &&
                   Type == remark.Type &&
                   RemarkContent == remark.RemarkContent;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, RemarkContent);
        }
    }

    public class ObservableRemark : ObservableObject
    {
        private readonly Remark _Remark;
        public ObservableRemark(Remark r) => _Remark = r;
        public RemarkType Type
        {
            get => _Remark.Type;
            set => SetProperty(_Remark.Type, value, _Remark, (r, v) => r.Type = v);
        }
        public string RemarkContent
        {
            get => _Remark.RemarkContent;
            set => SetProperty(_Remark.RemarkContent, value, _Remark, (r, v) => r.RemarkContent = v);
        }
        public string AssociatedSegment
        {
            get => _Remark.AssociatedSegment;
            set => SetProperty(_Remark.AssociatedSegment, value, _Remark, (r, v) => r.AssociatedSegment = v);
        }
        public string RemarkType
        {
            get => _Remark.Type.ToString();
        }
    }

    public enum RemarkType
    {
        ItineraryRemark,
        PassengerRemark,
        InterfaceRemark
    }
}
