using IurAir.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Services.SettingsService
{
    public static class SettingService
    {
        
        public static AgencyData getStoredAgencyData()
        {
            return new AgencyData()
            {
                Version = Properties.Settings.Default.Version,
                OfficeId = Properties.Settings.Default.OfficeId,
                AgentDutyCode = Properties.Settings.Default.AgentDutyCode,
                AgentInitials = Properties.Settings.Default.AgentInitials,
                NetworkIdentifier1 = Properties.Settings.Default.NetworkIdentifier1,
                TerminalIdentifier1 = Properties.Settings.Default.TerminalIdentifier1,
                NetworkIdentifier2 = Properties.Settings.Default.NetworkIdentifier2,
                TerminalIdentifier2 = Properties.Settings.Default.TerminalIdentifier2,
                AgencyIATA = Properties.Settings.Default.AgencyIATA,
                DefaultCurrency = Properties.Settings.Default.DefaultCurrency,
            };
        }

        public static AgencyData storeValue(AgencyData agencyData)
        {
            AgencyData stored = getStoredAgencyData();
            AgencyData toStore = new AgencyData()
            {
                Version = agencyData.Version ?? Properties.Settings.Default.Version,
                OfficeId = agencyData.OfficeId ?? Properties.Settings.Default.OfficeId,
                AgentDutyCode = agencyData.AgentDutyCode ?? Properties.Settings.Default.AgentDutyCode,
                AgentInitials = agencyData.AgentInitials ?? Properties.Settings.Default.AgentInitials,
                NetworkIdentifier1 = agencyData.NetworkIdentifier1 ?? Properties.Settings.Default.NetworkIdentifier1,
                TerminalIdentifier1 = agencyData.TerminalIdentifier1 ?? Properties.Settings.Default.TerminalIdentifier1,
                NetworkIdentifier2 = agencyData.NetworkIdentifier2 ?? Properties.Settings.Default.NetworkIdentifier2,
                TerminalIdentifier2 = agencyData.TerminalIdentifier2 ?? Properties.Settings.Default.TerminalIdentifier2,
                AgencyIATA = agencyData.AgencyIATA ?? Properties.Settings.Default.AgencyIATA,
                DefaultCurrency = agencyData.DefaultCurrency ?? Properties.Settings.Default.DefaultCurrency,
            };
            return saveAgencyData(toStore);
        }

        private static AgencyData saveAgencyData(AgencyData agencyData)
        {
            Properties.Settings.Default.Version = agencyData.Version;
            Properties.Settings.Default.OfficeId = agencyData.OfficeId;
            Properties.Settings.Default.AgentDutyCode = agencyData.AgentDutyCode;
            Properties.Settings.Default.AgentInitials = agencyData.AgentInitials;
            Properties.Settings.Default.NetworkIdentifier1 = agencyData.NetworkIdentifier1;
            Properties.Settings.Default.TerminalIdentifier1 = agencyData.TerminalIdentifier1;
            Properties.Settings.Default.NetworkIdentifier2 = agencyData.NetworkIdentifier2;
            Properties.Settings.Default.TerminalIdentifier2 = agencyData.TerminalIdentifier2;
            Properties.Settings.Default.AgencyIATA = agencyData.AgencyIATA;
            Properties.Settings.Default.DefaultCurrency = agencyData.DefaultCurrency;
            Properties.Settings.Default.Save();
            return getStoredAgencyData();
        }

        public static bool GetPerPaxSplit()
        {
            return Properties.Settings.Default.SplitPerPax;
        }

        public static void SetPerPaxSplit(bool splitPerPax)
        {
            Properties.Settings.Default.SplitPerPax = splitPerPax;
            Properties.Settings.Default.Save();
        }

        public static string GetIurFolder()
        {
            return Properties.Settings.Default.IurFolder;
        }

        public static string GetAmaFolder()
        {
            return Properties.Settings.Default.BackOfficeFolder;
        }
        public static string GetErrFolder()
        {
            return Properties.Settings.Default.ErrorFolder;
        }

        public static void SetIurFolder(string folder)
        {
            Properties.Settings.Default.IurFolder = folder;
            Properties.Settings.Default.Save();
        }

        public static void SetAmaFolder(string folder)
        {
            Properties.Settings.Default.BackOfficeFolder = folder;
            Properties.Settings.Default.Save();
        }
        public static void SetErrFolder(string folder)
        {
            Properties.Settings.Default.ErrorFolder = folder;
            Properties.Settings.Default.Save();
        }

        public static bool GetStartupStart()
        {
            return Properties.Settings.Default.Startup;
        }

        public static bool GetAutoConvert()
        {
            return Properties.Settings.Default.AutoConvert;
        }

        public static void SetStartupStart(bool value)
        {
            Properties.Settings.Default.Startup = value;
            Properties.Settings.Default.Save();
        }

        public static void SetAutoConvert(bool value)
        {
            Properties.Settings.Default.AutoConvert = value;
            Properties.Settings.Default.Save();
        }

        public static bool GetFileFormat()
        {
            return Properties.Settings.Default.CustomFileFormat;
        }

        public static void SetFileFormat(bool value)
        {
            Properties.Settings.Default.CustomFileFormat = value;
            Properties.Settings.Default.Save();
        }

        public static int GetFileFormatPattern()
        {
            return Properties.Settings.Default.CustomFormatPattern;
        }

        public static void SetFileFormatPattern(int value)
        {
            Properties.Settings.Default.CustomFormatPattern = value;
            Properties.Settings.Default.Save();
        }
    }
}
