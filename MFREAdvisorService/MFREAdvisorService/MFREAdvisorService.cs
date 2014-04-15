using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MFREAdvisorService
{
    public partial class MFREAdvisorService : ServiceBase
    {
        private static string watchedDirPath = null;
        public MFREAdvisorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Debugger.Launch();
            FileSystemWatcher Watcher = new FileSystemWatcher();
            /*String localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fileName = "directory.txt";
            string filePath = System.IO.Path.Combine(localAppDataPath, fileName);
            //string currentDirectory = Directory.GetCurrentDirectory();
            //string fileName = "directory.txt";
            //string filePath = System.IO.Path.Combine(currentDirectory, fileName);
            //Watcher.Path = Environment.SystemDirectory;
*/
            string filePath = @"C:\Users\Georgy\AppData\Local\Directory.txt";
            watchedDirPath = System.IO.File.ReadAllText(filePath);
            Watcher.Path = watchedDirPath;
            Watcher.IncludeSubdirectories = true;
            Watcher.Created += new FileSystemEventHandler(onChanged);
            Watcher.Deleted += new FileSystemEventHandler(onChanged);
            Watcher.Changed += new FileSystemEventHandler(onChanged);
            Watcher.Renamed += new RenamedEventHandler(onRenamed);
            Watcher.EnableRaisingEvents = true;
            
        }

        static void onChanged(object sender, FileSystemEventArgs e)
        {     
            string file = @"C:\Users\Georgy\AppData\Local";
            string fileName = "MFRElog.xml";
            //path to file which stores all the changes
            string filePath = System.IO.Path.Combine(file, fileName);
            
            XDocument doc;
            if (!File.Exists(filePath))
            {
                doc = new XDocument( new XElement(
                    "Root",
                    new XAttribute("watchedDirectory", watchedDirPath)));
                doc.Save(filePath);
            }
            else
            {
                doc = XDocument.Load(filePath);
                XElement changeEl = new XElement("File",
                    new XAttribute("path", e.FullPath),
                    new XElement("Change",
                        new XAttribute("type", e.ChangeType),
                        new XAttribute("time", DateTime.Now.ToString("G"))));
                doc.Root.Add(changeEl);
                doc.Save(filePath);
            }

            
            
        }

        private static void onRenamed(object source, RenamedEventArgs e)
        {
            string file = @"C:\Users\Georgy\AppData\Local";
            string fileName = "MFRElog.xml";
            //path to file which stores all the changes
            string filePath = System.IO.Path.Combine(file, fileName);

            XDocument doc;
            if (!File.Exists(filePath))
            {
                doc = new XDocument(new XElement(
                    "Root",
                    new XAttribute("watchedDirectory", watchedDirPath)));
                doc.Save(filePath);
            }
            else
            {
                doc = XDocument.Load(filePath);
                XElement changeEl = new XElement("File",
                    new XAttribute("path", e.OldFullPath),
                    new XElement("Change",
                        new XAttribute("type", e.ChangeType),
                        new XAttribute("time", "10:00")),
                    new XElement("NewPath", e.FullPath));
                doc.Root.Add(changeEl);
                doc.Save(filePath);
            }

        }

        protected override void OnStop()
        {
        }
    }
}
