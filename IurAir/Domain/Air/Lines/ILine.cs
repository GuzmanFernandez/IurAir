using IurAir.Domain.Air.Shared;
using IurAir.Domain.Iur;
using IurAir.Services.SettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IurAir.Domain.Air.Lines
{
    public class ILine
    {

        public string ItemNumber { get; set; }
        public string PnrPassengerNumber { get; set; }
        public string PassengerName { get; set; }
        public string PassengerType { get; set; }

        public ILine(M1Object m1, M4Object m4)
        {
            ItemNumber = m1.interfaceNameItemNumber;
            PnrPassengerNumber = m1.interfaceNameItemNumber;
            PassengerName = m1.passengerName.Trim();
            PassengerType = m4.passengerTypeCode;
        }

        public ILine(M1Object m1, MgObject mg)
        {
            ItemNumber = m1.interfaceNameItemNumber;
            PnrPassengerNumber = m1.interfaceNameItemNumber;
            PassengerName = m1.passengerName.Trim();
            PassengerType = mg.PassengerType;
        }

        public ILine(string itemNumber, string pnrPassengerNumber, string passengerName, string passengerType)
        {
            string pattern = @"C([N0-9]{2})";
            string substitution = @"CHD";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(passengerType, substitution);
            ItemNumber = AirUtil.PadString(itemNumber, '0', 3, true);
            PnrPassengerNumber = pnrPassengerNumber;
            PassengerName = passengerName;
            PassengerType = passengerType;
        }

        public string FormatString208(bool isPerPAX = false)
        {
            if (isPerPAX)
            {
                ItemNumber = "001";
                PnrPassengerNumber = "01";
            }
            return $"I-{ItemNumber};{PnrPassengerNumber}{PassengerName}({PassengerType});;;;";
        }

        public string FormatString206()
        {
            return FormatString208();
        }
    }
}


//I-001;01SPINETTI/MARCO;;APPSA 0584 32991 - GLAMOUR TOUR OPERATOR - A//MARCO PER PROVA SABRE//E-MARCO.SPINETTI@GLAMOURVIAGGI.IT;;