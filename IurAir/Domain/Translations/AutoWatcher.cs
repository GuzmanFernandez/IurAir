using IurAir.Domain.Air;
using IurAir.Domain.Common;
using IurAir.Domain.Iur;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace IurAir.Domain.Translations
{
    public class AutoWatcher
    {
        private string AgencyLock = Properties.Settings.Default.AgencyIATA;

        private FileSystemWatcher _watcher;
        private string _pathToWatch="";
        private bool _isWatching;
        private PnrFileInfo _file;
        private List<AirRender> renders = new List<AirRender>();
        private string Json = "";
        private DocumentParse currentParse;
        private string IurFile = "";
        private bool documentLoaded = false;
        private String CurrentFile;
        private string AirFile = "";

        ConcurrentQueue<FileOperations> queue = new ConcurrentQueue<FileOperations> ();


        public AutoWatcher() {
            this._pathToWatch = Properties.Settings.Default.IurFolder;
        }

        public bool watching
        {
            get { return _isWatching; }
            set { _isWatching = value; }
        }

        public string DocumentType { get; private set; }

        public void watch()
        {
            if (!watching && _pathToWatch != string.Empty)
            {
                _watcher = new FileSystemWatcher(_pathToWatch);
                _watcher.EnableRaisingEvents = true;
                _watcher.IncludeSubdirectories = false;

                _watcher.Created += OnFileCreated;
                watching = true;
            }
        }

        public void stopWatch()
        {
            if (watching)
            {
                _watcher.Created -= OnFileCreated;
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
                watching = false;
            }
        }

        public void OnFileFound()
        {
            if(_pathToWatch != string.Empty)
            {
                DirectoryInfo di = new DirectoryInfo(_pathToWatch);
                foreach(FileInfo fi in di.GetFiles ())
                {
                    queue.Enqueue(new FileOperations(fi.FullName));
                    Thread thread = new Thread(() =>
                    {
                        while (queue.TryDequeue(out FileOperations operation))
                        {
                            operation.Execute();
                        }
                    });
                    thread.Start();
                }
            }
        }

        public void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            //this.FileCreated(e.FullPath);
            queue.Enqueue(new FileOperations(e.FullPath));
            Thread thread = new Thread(() =>
            {
                while (queue.TryDequeue(out FileOperations operation))
                {
                    operation.Execute();
                }
            });
            thread.Start();
        }

        public void EnableWatcher()
        {
            if (Properties.Settings.Default.IurFolder.Length < 1 || !Directory.Exists(Properties.Settings.Default.IurFolder))
            {
                MessageBox.Show("Please set input and output folders in Setup Window");
            }

            _pathToWatch = Properties.Settings.Default.IurFolder;
            watch();
        }

    }
}
