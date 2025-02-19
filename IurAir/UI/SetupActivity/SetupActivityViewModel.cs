using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IurAir.Domain.Common;
using IurAir.Services.SettingsService;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace IurAir.UI.SetupActivity
{
    
    public class SetupActivityViewModel : ObservableObject
    {

        private const string password = "!Cambiami2023";

        public ICommand LoadFromFile { get; }
        public ICommand SetFolder { get; }
        public ICommand HandleRadio { get; }

        public ICommand OpenPasswordDialog { get; }
        public ICommand HandlePerPax { get; }
        
        public SetupActivityViewModel()
        {
            LoadFromFile = new RelayCommand(_LoadFromFile);
            SetFolder = new RelayCommand<string>(_SetFolder);
            HandleRadio = new RelayCommand<string>(_HandleRadio);
            OpenPasswordDialog = new RelayCommand(_OpenPasswordDialog);
        }

        
        private void _OpenPasswordDialog()
        {
            DialogWindow dw = new DialogWindow();
            if(dw.ShowDialog() == true)
            {
                string pass = dw.Password;
                string agencyIata = dw.AgencyIata;
                Boolean debug = dw.DebugMode;
                Boolean persistentDebug = dw.PersistentDebug;
                if(pass == password)
                {
                    AgencyIATA = agencyIata;
                    Properties.Settings.Default.DebugMode = debug;
                    Properties.Settings.Default.AgencyIATA = agencyIata;
                    Properties.Settings.Default.PersistentDebug = persistentDebug;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Wrong password");
                }
            }
        }

        private void _LoadFromFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "IUR";
            dialog.DefaultExt = ".pnr";

            bool? res = dialog.ShowDialog();
            if (res == true)
            {
                using (StreamReader r = new StreamReader(dialog.FileName))
                {
                    string airFile = r.ReadToEnd();
                    LoadAgencyDataFromAir(airFile.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
                }
            }
        }

        private void _SetFolder(string type)
        {
            var ookii = new VistaFolderBrowserDialog();
            if (ookii.ShowDialog() == true)
            {
                switch (int.Parse(type))
                {
                    case 0:
                        IurFolder = ookii.SelectedPath;
                        SettingService.SetIurFolder(IurFolder);
                        break;
                    case 1:
                        AmaFolder = ookii.SelectedPath;
                        SettingService.SetAmaFolder(AmaFolder);
                        break;
                    default:
                        ErrFolder = ookii.SelectedPath;
                        SettingService.SetErrFolder(ErrFolder);
                        break;
                }

            }

        }

        private void _HandleRadio(string radio)
        {
            switch (radio)
            {
                case "StartupRadio_Y":
                    StartupStart= true;
                    SettingService.SetStartupStart(true);
                    break;
                case "StartupRadio_N":
                    StartupStart= false;
                    SettingService.SetStartupStart(false);
                    break;
                case "AutoRadio_Y":
                    AutoConvert = true;
                    SettingService.SetAutoConvert(true);
                    break;
                case "AutoRadio_N":
                    AutoConvert = false;
                    SettingService.SetAutoConvert(false);
                    break;
                case "StandardFormat":
                    CustomFormat = false;
                    SettingService.SetFileFormat(false);
                    break;
                case "CustomFormat":
                    CustomFormat = true;
                    SettingService.SetFileFormat(true);
                    break;
                case "OrionFormat":
                    CustomFilenameFormat = 1;
                    SettingService.SetFileFormatPattern(1);
                    break;
            }
        }

        
       

        private string _Version = "";
        private string _OfficeId = "";
        private string _AgentDutyCode = "";
        private string _AgentInitials = "";
        private string _NetworkIdentifier1 = "";
        private string _TerminalIdentifier1 = "";
        private string _NetworkIdentifier2 = "";
        private string _TerminalIdentifier2 = "";
        private string _AgencyIATA = "";
        private string _DefaultCurrency = "";

        public string Version
        {
            get => _Version;
            set
            {
                SetProperty(ref _Version, value);
            }
        }
        public string OfficeId
        {
            get => _OfficeId;
            set
            {
                SetProperty(ref _OfficeId, value);
            }
        }
        public string AgentDutyCode
        {
            get => _AgentDutyCode;
            set
            {
                SetProperty(ref _AgentDutyCode, value);
            }
        }
        public string AgentInitials
        {
            get => _AgentInitials;
            set
            {
                SetProperty(ref _AgentInitials, value);
            }
        }
        public string NetworkIdentifier1
        {
            get => _NetworkIdentifier1;
            set
            {
                SetProperty(ref _NetworkIdentifier1, value);
            }
        }
        public string TerminalIdentifier1
        {
            get => _TerminalIdentifier1;
            set
            {
                SetProperty(ref _TerminalIdentifier1, value);
            }
        }
        public string NetworkIdentifier2
        {
            get => _NetworkIdentifier2;
            set
            {
                SetProperty(ref _NetworkIdentifier2, value);
            }
        }
        public string TerminalIdentifier2
        {
            get => _TerminalIdentifier2;
            set
            {
                SetProperty(ref _TerminalIdentifier2, value);
            }
        }
        public string AgencyIATA
        {
            get => _AgencyIATA;
            set
            {
                
                SetProperty(ref _AgencyIATA, value);
            }
        }

        public string DefaultCurrency
        {
            get => _DefaultCurrency;
            set
            {
                SetProperty(ref _DefaultCurrency, value);
            }
        }

        private string _IurFolder;
        private string _AmaFolder;
        private string _ErrFolder;

        public string IurFolder
        {
            get => _IurFolder;
            set
            {
                SetProperty(ref _IurFolder, value);
            }
        }
        public string AmaFolder
        {
            get => _AmaFolder;
            set
            {
                SetProperty(ref _AmaFolder, value);
            }
        }
        public string ErrFolder
        {
            get => _ErrFolder;
            set
            {
                SetProperty(ref _ErrFolder, value);
            }
        }

        private bool _StartupStart;
        private bool _AutoConvert;
        private bool _CustomFormat;

        public bool StartupStart
        {
            get => _StartupStart;
            set 
            { 
                SetProperty(ref _StartupStart, value); 
            }
        }
        public bool AutoConvert
        {
            get => _AutoConvert;
            set 
            { 
                SetProperty(ref _AutoConvert, value); 
            }
        }
        public bool CustomFormat
        {
            get => _CustomFormat;
            set
            {
                SetProperty(ref _CustomFormat, value);
            }
        }

        private int _CustomFilenameFormat;
        public int CustomFilenameFormat
        {
            get => _CustomFilenameFormat;
            set
            {
                SetProperty(ref _CustomFilenameFormat, value);
            }
        }

        private bool _splitCHBChecked;
        public bool SplitCHBChecked
        {
            get => _splitCHBChecked;
            set
            {
                SetProperty(ref _splitCHBChecked, value);
                SettingService.SetPerPaxSplit(value);
            }
        }

        public void Init()
        {
            AgencyData ad = SettingService.getStoredAgencyData();
            Version = ad.Version;
            OfficeId = ad.OfficeId;
            AgentDutyCode = ad.AgentDutyCode;
            AgentInitials = ad.AgentInitials;
            NetworkIdentifier1 = ad.NetworkIdentifier1;
            TerminalIdentifier1 = ad.TerminalIdentifier1;
            NetworkIdentifier2 = ad.NetworkIdentifier2;
            TerminalIdentifier2 = ad.TerminalIdentifier2;
            AgencyIATA = ad.AgencyIATA;
            IurFolder = SettingService.GetIurFolder();
            AmaFolder = SettingService.GetAmaFolder();
            ErrFolder = SettingService.GetErrFolder();
            StartupStart = SettingService.GetStartupStart();
            AutoConvert = SettingService.GetAutoConvert();
            CustomFormat = SettingService.GetFileFormat();
            if(CustomFormat)
            {
                int pattern = SettingService.GetFileFormatPattern();
                CustomFilenameFormat = pattern;
            }
            SplitCHBChecked = SettingService.GetPerPaxSplit();
        }


        private void LoadAgencyDataFromAir(string[] airFile)
        {
            int terminalLineIndex = -1;
            for (int i = 0; i < airFile.Length; i++)
            {
                string line = airFile[i];
                if (line.StartsWith("AIR"))
                {
                    Version = line.Substring(7, 3);
                }
                if (line.StartsWith("AMD"))
                {
                    if (InRange(i + 1, airFile))
                    {
                        terminalLineIndex = i + 1;
                    }
                }
                if (line.StartsWith("MUC1A"))
                {
                    string[] splittedLine = line.Split(';');
                    if (InRange(2, splittedLine))
                    {
                        OfficeId = splittedLine[2];
                    }
                    if (InRange(3, splittedLine))
                    {
                        AgencyIATA = splittedLine[3];
                    }
                }
            }
            if (terminalLineIndex != -1)
            {
                string[] terminalLine = airFile[terminalLineIndex].Split(';');
                if (InRange(0, terminalLine))
                {
                    NetworkIdentifier1 = terminalLine[0].Substring(0, 2);
                    TerminalIdentifier1 = terminalLine[0].Substring(3);
                }
                if (InRange(1, terminalLine))
                {
                    NetworkIdentifier2 = terminalLine[1].Substring(0, 2);
                    TerminalIdentifier2 = terminalLine[1].Substring(3);
                }
            }
        }

        public void Save()
        {
            SettingService.storeValue(collect());
            SettingService.SetIurFolder(IurFolder);
            SettingService.SetAmaFolder(AmaFolder);
            SettingService.SetErrFolder(ErrFolder);
            SettingService.SetStartupStart(StartupStart);
            SettingService.SetAutoConvert(AutoConvert);
            SettingService.SetFileFormat(CustomFormat);
            SettingService.SetFileFormatPattern(CustomFilenameFormat);
            SettingService.SetPerPaxSplit(SplitCHBChecked);
        }

        private AgencyData collect()
        {
            return new AgencyData
            {
                Version = string.IsNullOrEmpty(_Version) ? null : Version,
                OfficeId = string.IsNullOrEmpty(_OfficeId) ? null : OfficeId,
                AgentDutyCode = string.IsNullOrEmpty(_AgentDutyCode) ? null : AgentDutyCode,
                AgentInitials = string.IsNullOrEmpty(_AgentInitials) ? null : AgentInitials,
                NetworkIdentifier1 = string.IsNullOrEmpty(_NetworkIdentifier1) ? null : NetworkIdentifier1,
                TerminalIdentifier1 = string.IsNullOrEmpty(_TerminalIdentifier1) ? null : TerminalIdentifier1,
                NetworkIdentifier2 = string.IsNullOrEmpty(_NetworkIdentifier2) ? null : NetworkIdentifier2,
                TerminalIdentifier2 = string.IsNullOrEmpty(_TerminalIdentifier2) ? null : TerminalIdentifier2,
                AgencyIATA = string.IsNullOrEmpty(_AgencyIATA) ? null : AgencyIATA,
            };
        }

        private bool InRange(int range, string[] toCheck)
        {
            return range <= toCheck.Length - 1;
        }
    }

    public class BoolInversionConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is bool)
            {
                return !(bool)value;
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }
    }

    public class CustomFilenamePatternConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int rbValue = 0;

            int.TryParse((string)parameter, out rbValue);

            if(value is int intValue && rbValue != 0)
            {
                return intValue == rbValue;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                return parameter;
            }
            return Binding.DoNothing;
        }
    }
}
