using IurAir.Domain.Air;
using IurAir.Domain.Iur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IurAir.Domain.Common
{
    public class FileopsV2
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

        public FileopsV2(string filePath)
        {
            FilePath = filePath;
            this._backup = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\I2A\\backup");
            this._errors = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\I2A\\errorFiles");
            this._unparsable = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\I2A\\unparsableFiles");
        }
    }
}
