using IurAir.Domain.Air.Lines;
using IurAir.Domain.Common;
using IurAir.Domain.Iur;
using IurAir.Domain.Translations;
using IurAir.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Annotations;

namespace IurAir.Domain.Air
{
    public class AirAdvancedFormatter
    {
        private IAirable airable;
        private int passengerNumber;

        public AirAdvancedFormatter(IAirable airable)
        {
            this.airable = airable;
        }

        public List<AirRender> getRenders(bool paxSplit = false)
        {
            if(airable != null)
            {
                return airable.getRenders(paxSplit);
            }
            return new List<AirRender>();
        }

        
        

        
       
        public List<String> RecoverSamePnrFiles(string pnrName, string directoryInfo, string toExclude = null)
        {
            List<String> res = new List<String>();
            DirectoryInfo di = new DirectoryInfo(directoryInfo);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.StartsWith(pnrName))
                {
                    if (toExclude != null && fi.Name.Split('.')[0] == toExclude)
                    {

                    }
                    else
                    {
                        using (StreamReader r = new StreamReader(fi.FullName))
                        {
                            res.Add(r.ReadToEnd());
                        }
                    }

                }
            }
            return res;
        }

        private static Mutex mutex = new Mutex();

        public static List<RelatedPnrFile> RecoverPreviousPnrFiles(FileInfo fi)
        {
            List<RelatedPnrFile> res = new List<RelatedPnrFile>();

            PnrFileInfo info = ParsePnrFileInfo(fi);
            if (info.FileNr < 0)
            {
                return res;
            }
            DirectoryInfo di = fi.Directory;

            mutex.WaitOne(); 
            try
            {
                foreach (FileInfo i in di.GetFiles())
                {
                    string ext = i.Extension;
                    if (ext == ".PNR")
                    {
                        PnrFileInfo pi = ParsePnrFileInfo(i);
                        if (pi.PnrName == info.PnrName && pi.FileNr >= 0 && pi.FileNr < info.FileNr)
                        {
                            using (StreamReader r = new StreamReader(i.FullName))
                            {
                                res.Add(new RelatedPnrFile(pi, r.ReadToEnd()));
                            }
                        }
                    }
                }
            }
            finally
            {
                mutex.ReleaseMutex(); 
            }
            return res;
        }

        public class  RelatedPnrFile
        {
            public PnrFileInfo Info { get; }
            public string Content { get; }

            public RelatedPnrFile(PnrFileInfo info, string content)
            {
                Info = info;
                Content = content;
            }
        }

        public static PnrFileInfo ParsePnrFileInfo(FileInfo file)
        {

            string FileName = file.Name.Split('.')[0];
            int fileNr;
            string fileName = FileName.Substring(0, FileName.Length - 2);
            try
            {
                fileNr = int.Parse(FileName.Substring(FileName.Length - 2));
            }
            catch
            {
                fileNr = -1;
            }
            return new PnrFileInfo(fileName, fileNr, file.FullName);
        }
    }

    public class PnrFileInfo
    {
        public string PnrName;
        public int FileNr;
        public string FilePath;

        public PnrFileInfo(string pnrName, int fileNr, string filePath)
        {
            PnrName = pnrName;
            FileNr = fileNr;
            FilePath = filePath;
        }

        public string getCompleteFileName()
        {
            string fNr = FileNr.ToString();
            if(fNr.Length < 2)
            {
                fNr = $"0{fNr}";
            }
            return $"{PnrName}{fNr}";
        }
    }
}
