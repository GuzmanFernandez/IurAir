using IurAir.Domain.Air;
using IurAir.Domain.Iur;
using IurAir.Services.SettingsService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace IurAir.Domain.Common
{
    public class FileOperations
    {
        private PnrFileInfo _file;
        private List<AirRender> renders = new List<AirRender>();
        private DocumentParse currentParse;
        private DirectoryInfo _backup;
        private DirectoryInfo _errors;
        private DirectoryInfo _unparsable;
        private String CurrentFile;
        private string _IurFile = "";
        private string _AirFile = "";
        private string _Json = "";
        public string FilePath { get; set; }

        public string IurFile { get => _IurFile; set => _IurFile = value; }
        public string AirFile { get => _AirFile; set => _AirFile = value; }
        public string Json { get => _Json; set => _Json = value; }

        public FileOperations(string filePath)
        {
            FilePath = filePath;
            this._backup = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\I2A\\backup");
            this._errors = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\I2A\\errorFiles");
            this._unparsable = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\I2A\\unparsableFiles");
        }

        public void Execute()
        {
            FileCreated(FilePath);
        }

        private DirectoryInfo CreateDir(string pnrName)
        {
            DirectoryInfo docDir = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\I2A");
            return docDir.CreateSubdirectory(pnrName);
        }

        private DocumentParse CombineEmissionAndVoid(DocumentParse emission, DocumentParse voidParse)
        {
            VoidCombinedParse cp = new VoidCombinedParse(voidParse, emission);
            return cp.resultingParse;
        }

        private Boolean CanProcess(IurDocument document)
        {
            if (Properties.Settings.Default.DebugMode)
            {
                return true;
            }
            if (document.CheckType() != Iur.DocumentType.VoidTicket)
            {
                return document.AgencyIata == Properties.Settings.Default.AgencyIATA;
            }
            return true;
        }

        private void FileCreated(string file)
        {
            try
            {
                this.IurFile = File.ReadAllText(file);
                string[] lines = File.ReadAllLines(file);
                PnrFileInfo pnrInfo = AirAdvancedFormatter.ParsePnrFileInfo(new FileInfo(file));
                var dir = CreateDir(pnrInfo.PnrName);
                var fileToWrite = Path.Combine(dir.FullName, $"{pnrInfo.getCompleteFileName()}.PNR");
                using (StreamWriter writer = new StreamWriter(fileToWrite))
                {
                    writer.Write(IurFile);
                }
                PnrFileInfo writedFile = AirAdvancedFormatter.ParsePnrFileInfo(new FileInfo(fileToWrite));
                _file = writedFile;
                var bckFile = Path.Combine(_backup.FullName, $"{pnrInfo.getCompleteFileName()}.PNR");
                File.Copy(fileToWrite, bckFile, true);
                IurDocument doc = new IurDocument(lines, pnrInfo);
                var type = doc.CheckType();
                if (type == Iur.DocumentType.EMD || type == Iur.DocumentType.Refund || type == DocumentType.Undefined)
                {
                    File.Copy(bckFile, Path.Combine(_unparsable.FullName, $"{pnrInfo.getCompleteFileName()}.PNR"));
                    File.Delete(bckFile);
                    return;
                }
                if (!CanProcess(doc))
                {
                    MessageBox.Show("This software is not registered for your Agency, please contact your commercial at Sabre.");
                    throw new AgencyIataException("This software is not registered for your Agency, please contact your commercial at Sabre.");
                }
                IurSplitter splitter = new IurSplitter(doc);
                var split = splitter.splitByType();
                AirAdvancedFormatter aaf;
                DocumentParse parse = splitter.Parse;
                if (parse.AdvancedSplitter != null)
                {
                    aaf = new AirAdvancedFormatter(parse.AdvancedSplitter);
                }
                else
                {
                    aaf = new AirAdvancedFormatter(splitter);
                }

                if (parse.DocumentType == Domain.Iur.DocumentType.VoidTicket)
                {
                    var files = AirAdvancedFormatter.RecoverPreviousPnrFiles(new FileInfo(fileToWrite));
                    if (files != null && files.Count > 0)
                    {
                        foreach (var f in files)
                        {

                            PnrFileInfo fPnrInfo = f.Info;
                            string[] fLines = File.ReadAllLines(f.Info.FilePath);
                            IurDocument fDoc = new IurDocument(fLines, fPnrInfo);
                            if (!CanProcess(fDoc))
                            {
                                MessageBox.Show("This software is not registered for your Agency, please contact your commercial at Sabre.");
                                throw new AgencyIataException("This software is not registered for your Agency, please contact your commercial at Sabre.");
                            }
                            IurSplitter iSplit = new IurSplitter(fDoc);
                            if (iSplit.Parse.DocumentType == Domain.Iur.DocumentType.TicketEmission)
                            {
                                var cparse = CombineEmissionAndVoid(iSplit.Parse, parse);
                                if (cparse != null)
                                {
                                    parse = cparse;
                                    splitter.Parse = cparse;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (parse.HeadersData.m0VoidData != null)
                        {
                            var tkt = parse.HeadersData.m0VoidData.ticketNumber.Substring(3);
                            parse.HeadersData.IataNr = Properties.Settings.Default.AgencyIATA;
                            parse.HeadersData.BookingAgency = Properties.Settings.Default.AgencyIATA;
                        }
                    }
                }

                currentParse = parse;
                var serializerSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore };
                if (parse.AdvancedSplitter != null)
                {
                    Json = JsonConvert.SerializeObject(parse.AdvancedSplitter, Formatting.Indented);
                }
                else
                {
                    Json = JsonConvert.SerializeObject(parse, Formatting.Indented);
                }
                var combinedAir = aaf.getRenders(SettingService.GetPerPaxSplit());
                string cAir = "";
                if (combinedAir.Count() > 0)
                {
                    renders = combinedAir;
                }
                for (int i = 0; i < combinedAir.Count; i++)
                {
                    if (i == combinedAir.Count - 1)
                    {
                        cAir += combinedAir[i].Content;
                    }
                    else
                    {
                        cAir += combinedAir[i].Content + "\n\n###### NEW AIR FILE ######\n";
                    }
                }
                AirFile = cAir;
                this.CurrentFile = file;
                try
                {
                    Download();
                }
                catch (Exception exception)
                {
                    try
                    {
                        File.Copy(file, Path.Combine(_errors.FullName, $"{pnrInfo.getCompleteFileName()}.PNR"), true);
                    }
                    catch (Exception ex)
                    {
                        LogError(ex);
                    }
                    LogError(exception);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(file);
                    File.Copy(_errors.FullName, fileInfo.FullName, true);
                }
                catch (Exception exc)
                {
                    LogError(exc);
                }
                LogError(ex);
            }
            finally
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex) { LogError(ex); }

            }
        }

        private void Download()
        {

            if (currentParse != null)
            {
                string prep = "";
                String customFileName = String.Empty;
                switch (currentParse.DocumentType)
                {
                    case Domain.Iur.DocumentType.TicketEmission:
                        prep = "TKT_";
                        break;
                    case Domain.Iur.DocumentType.VoidTicket:
                        prep = "VOID_";
                        break;
                    case Domain.Iur.DocumentType.EMD:
                        prep = "EMD_";
                        break;
                    case Domain.Iur.DocumentType.Refund:
                        prep = "REF_";
                        break;
                    case Domain.Iur.DocumentType.EvenExchange:
                        prep = "EEX_";
                        break;
                    case Domain.Iur.DocumentType.Exchange:
                        prep = "NEX_";
                        break;
                    default:
                        prep = "NA_";
                        break;
                }
                if (SettingService.GetFileFormat() && SettingService.GetFileFormatPattern() != 0)
                {
                    int format = SettingService.GetFileFormatPattern();
                    if (format == 1)
                    {
                        string fileExt = DateTime.Now.ToString("yyMMdd-HHmmss");
                        customFileName = "air." + fileExt;
                    }
                }
                if (!string.IsNullOrEmpty(AirFile) && !string.IsNullOrEmpty(Json))
                {
                    var lastWrite = "";
                    for (int i = 0; i < renders.Count; i++)
                    {

                        FileInfo fi = new FileInfo(_file.FilePath);
                        string fileName = _file.getCompleteFileName();
                        if (fileName == lastWrite)
                        {
                            _file.FileNr++;
                            fileName = _file.getCompleteFileName();
                        }
                        try
                        {
                            long ticks = DateTime.Now.Ticks;
                            int randomThreeDigits = new Random((int)(ticks & 0xffffffff)).Next(100, 1000);
                            string prog = randomThreeDigits.ToString("000").PadLeft(3, '0');
                            string name = $"{fi.DirectoryName}\\AIR_{prep}{fileName}_{prog}.AIR";
                            using (StreamWriter writer = new StreamWriter(name))
                            {
                                writer.Write(renders[i].Content);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                        }
                        var bo = Properties.Settings.Default.BackOfficeFolder;
                        if (bo.Length > 1 && Directory.Exists(bo))
                        {

                            try
                            {
                                int num = Directory.GetFiles(bo).Count();
                                string numFiles = (num + 2).ToString("000");
                                string name = String.IsNullOrEmpty(customFileName) ?
                                    $"{bo}\\AIR_{prep}{fileName}_{numFiles}.AIR" :
                                    $"{bo}\\air.{numFiles}";
                                while (File.Exists(name))
                                {
                                    num += 1;
                                    numFiles = num.ToString("000");
                                    name = String.IsNullOrEmpty(customFileName) ?
                                    $"{bo}\\AIR_{prep}{fileName}_{numFiles}.AIR" :
                                    $"{bo}\\air.{numFiles}";
                                }
                                using (StreamWriter writer = new StreamWriter(name))
                                {
                                    writer.Write(renders[i].Content);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogError(ex);
                            }

                        }
                        lastWrite = fileName;
                    }
                    try
                    {
                        var prog = DateTime.Now.Millisecond.ToString("000");
                        using (StreamWriter writer = new StreamWriter(_file.FilePath + $"_{prog}.json"))
                        {
                            writer.Write(Json);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError(ex);
                    }

                }

            }
        }

        private void LogError(Exception ex)
        {
            string errorPath = Properties.Settings.Default.ErrorFolder;
            if (String.IsNullOrEmpty(errorPath))
            {
                errorPath = CreateDir("ErrorLogs").FullName;
            }
            try
            {
                using (StreamWriter sw = File.AppendText($"{errorPath}\\error.log"))
                {
                    sw.WriteLine(ex.ToString());
                }
            }
            catch (Exception exc)
            {

            }

            if (ex is AgencyIataException)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Application.Current.Shutdown();
                });
            }
        }
    }

    public class AgencyIataException : Exception
    {
        public AgencyIataException(string message) : base(message)
        {

        }
    }
}
