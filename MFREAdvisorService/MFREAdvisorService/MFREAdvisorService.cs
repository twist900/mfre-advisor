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

        private static string[] exludedFiles = {"files_log.html","files_list.html", "MFlog.xml"};
        private static string xmlFileName = "files_log.xml";
        private static string htmlFileName = "files_log.html";
        private static string htmlFileListName = "files_list.html";
        private static string watchedDirPath = null;
        private static string assemblyPath = null;
        public MFREAdvisorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Debugger.Launch();
            FileSystemWatcher Watcher = new FileSystemWatcher();
            assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string filePath = Path.Combine(assemblyPath, "directory.xml");

            watchedDirPath = System.IO.File.ReadAllText(filePath);
            Watcher.Path = watchedDirPath;
            Watcher.IncludeSubdirectories = true;
            Watcher.Created += new FileSystemEventHandler(onChanged);
            Watcher.Deleted += new FileSystemEventHandler(onChanged);
            Watcher.Changed += new FileSystemEventHandler(onChanged);
            Watcher.Renamed += new RenamedEventHandler(onRenamed);
            Watcher.EnableRaisingEvents = true;

            enumAllFiles();
            
        }

        static void onChanged(object sender, FileSystemEventArgs e)
        {

            string shortPath = e.FullPath.Replace(watchedDirPath, "");
            if (shortPath.Equals("\\files_log.xml") ||          
                shortPath.Equals("\\files_log.html") ||     
                shortPath.Equals("\\files_list.html"))         
            {
                return;                                         
            }
           
            string filePath = System.IO.Path.Combine(watchedDirPath, xmlFileName);
            string htmlFile = System.IO.Path.Combine(watchedDirPath, htmlFileName);
            string htmlFileList = System.IO.Path.Combine(watchedDirPath, htmlFileListName);

            
            XDocument doc;
            if (!File.Exists(filePath))
            {
                doc = new XDocument( new XElement(
                    "Root",
                    new XAttribute("watchedDirectory", watchedDirPath)));
                doc.Save(filePath);
            }

            XDocument html;
            if (!File.Exists(htmlFile))
            {
            
                File.Copy(Path.Combine(assemblyPath, htmlFileName), htmlFile);
            }
            
            XDocument htmlList;
            if (!File.Exists(htmlFileList))
            {
                
                File.Copy(Path.Combine(assemblyPath, htmlFileListName), htmlFileList);
                htmlList = XDocument.Load(htmlFileList);
                var list = new DirectoryInfo(watchedDirPath).GetFiles("*.*", SearchOption.AllDirectories);
                string temp;
                XName tbody = "tbody";
                XElement addRow;
                foreach (var file in list)
                {
                    temp = file.FullName.Replace(watchedDirPath, "");
                    if (!temp.Equals("\\files_log.xml") && !temp.Equals("\\files_log.html") && !temp.Equals("\\files_list.html")) 
                    {
                        addRow = new XElement("tr",
                            new XElement("td", file.Name),
                            new XElement("td", temp.Replace("\\", "/"),
                            new XElement("td", file.CreationTime.ToString("G"))));
                        htmlList.Root.Descendants(tbody).First().Add(addRow); 
                    }
                }
                htmlList.Save(htmlFileList);
            }

            html = XDocument.Load(htmlFile);
            XElement changeRow = new XElement("tr", 
                            new XElement("td", e.Name),
                            new XElement("td", e.ChangeType),
                            new XElement("td", e.FullPath.Replace(watchedDirPath, "").Replace("\\","/")), 
                            new XElement("td", DateTime.Now.ToString("G")));
            XName thbody = "tbody";
           html.Root.Descendants(thbody).First().Add(changeRow);  
            html.Save(htmlFile);

            doc = XDocument.Load(filePath);
            XElement changeEl = new XElement("File",
                     new XAttribute("path", e.FullPath.Replace(watchedDirPath, "").Replace("\\","/")),
                    new XElement("Change",
                        new XAttribute("type", e.ChangeType),
                        new XAttribute("time", DateTime.Now.ToString("G"))));
            doc.Root.Add(changeEl);
            doc.Save(filePath);

             htmlList = XDocument.Load(htmlFileList);
            var list2 = new DirectoryInfo(watchedDirPath).GetFiles("*.*", SearchOption.AllDirectories);
            string temp2;
            XName tbody2 = "tbody";
            htmlList.Root.Descendants(tbody2).First().Descendants("tr").Remove();
            XElement addRow2;
            foreach (var file in list2)
            {
                temp2 = file.FullName.Replace(watchedDirPath, "");
                if (!temp2.Equals("\\files_log.xml") && !temp2.Equals("\\files_log.html") && !temp2.Equals("\\files_list.html"))  
                {
                    addRow2 = new XElement("tr",
                        new XElement("td", file.Name),
                         new XElement("td", temp2.Replace("\\", "/")),            
                        new XElement("td", file.CreationTime.ToString("G")));   
                    htmlList.Root.Descendants(tbody2).First().Add(addRow2);
                }
            }
            htmlList.Save(htmlFileList);         
    }
                 

        private static void onRenamed(object source, RenamedEventArgs e)
        {
           
             string shortPath = e.FullPath.Replace(watchedDirPath, "");
            if (shortPath.Equals("\\files_log.xml") ||          
                shortPath.Equals("\\files_log.html") ||         
                shortPath.Equals("\\files_list.html"))        
            {
                return;                                         
            }
            
            string htmlFile = System.IO.Path.Combine(watchedDirPath, htmlFileName);
            string htmlFileList = System.IO.Path.Combine(watchedDirPath, htmlFileListName);
           
            string filePath = System.IO.Path.Combine(watchedDirPath, xmlFileName);

            XDocument doc;
            if (!File.Exists(filePath))
            {
                doc = new XDocument(new XElement(
                    "Root",
                    new XAttribute("watchedDirectory", watchedDirPath)));
                doc.Save(filePath);
            }

            XDocument html;
            if (!File.Exists(htmlFile))
            {
                File.Copy(Path.Combine(assemblyPath, htmlFileName), htmlFile);
            }

            XDocument htmlList;
            if (!File.Exists(htmlFileList))
            {
                File.Copy(Path.Combine(assemblyPath, htmlFileListName), htmlFileList);
                htmlList = XDocument.Load(htmlFileList);
                var list = new DirectoryInfo(watchedDirPath).GetFiles("*.*", SearchOption.AllDirectories);
                string temp;
                XName tbody = "tbody";
                XElement addRow;
                foreach (var file in list)
                {
                    temp = file.FullName.Replace(watchedDirPath, "");
                    if (!temp.Equals("\\files_log.xml") && !temp.Equals("\\files_log.html") && !temp.Equals("\\files_list.html")) 
                    {
                        addRow = new XElement("tr",
                            new XElement("td", file.Name),
                            new XElement("td", temp.Replace("\\","/")), 
                            new XElement("td", file.CreationTime.ToString("G")));
                       htmlList.Root.Descendants(tbody).First().Add(addRow);
                    }
                }
                htmlList.Save(htmlFileList);
            }

            html = XDocument.Load(htmlFile);
            XElement changeRow = new XElement("tr",
                            new XElement("td", e.Name),
                            new XElement("td", e.ChangeType),
                            new XElement("td", e.FullPath.Replace(watchedDirPath, "").Replace("\\", "/")                        
                                                + " from " + e.OldFullPath.Replace(watchedDirPath, "").Replace("\\", "/")), 
                            new XElement("td", DateTime.Now.ToString("G")));
            XName thbody = "tbody";
            html.Root.Descendants(thbody).First().Add(changeRow);          
            html.Save(htmlFile);


            htmlList = XDocument.Load(htmlFileList);
            var list2 = new DirectoryInfo(watchedDirPath).GetFiles("*.*", SearchOption.AllDirectories);
            string temp2;
            XName tbody2 = "tbody";
            htmlList.Root.Descendants(tbody2).First().Descendants("tr").Remove();
            XElement addRow2;
            foreach (var file in list2)
            {
                temp2 = file.FullName.Replace(watchedDirPath, "");
                if (!temp2.Equals("\\files_log.xml") && !temp2.Equals("\\files_log.html") && !temp2.Equals("\\files_list.html")) 
                {
                    addRow2 = new XElement("tr",
                        new XElement("td", file.Name),
                         new XElement("td", temp2.Replace("\\", "/")),            
                        new XElement("td", file.CreationTime.ToString("G")));  
                    htmlList.Root.Descendants(tbody2).First().Add(addRow2);
                }
            }
            htmlList.Save(htmlFileList);

        }

        protected override void OnStop()
        {
        }


        private void enumAllFiles() {
            
            string filePath = System.IO.Path.Combine(watchedDirPath, xmlFileName);

            XDocument doc;
            if (!File.Exists(filePath))
            {
                doc = new XDocument(new XElement(
                    "Root",
                    new XAttribute("watchedDirectory", watchedDirPath)));
                doc.Save(filePath);
            }
                doc = XDocument.Load(filePath);

            DirectoryInfo diTop = new DirectoryInfo(watchedDirPath);
            try
            {
                foreach (var fi in diTop.EnumerateFiles())
                {
                    try
                    {
                        XElement el = new XElement("File",
                            new XAttribute("path", fi.FullName),
                            new XElement("Change",
                                 new XAttribute("type", "register-file"),
                                 new XAttribute("time", DateTime.Now.ToString("G"))));
                        doc.Root.Add(el);   
                    }
                    catch (UnauthorizedAccessException UnAuthTop)
                    {
                        
                    }
                }

                foreach (var di in diTop.EnumerateDirectories("*"))
                {
                    try
                    {
                        foreach (var fi in di.EnumerateFiles("*", SearchOption.AllDirectories))
                        {
                            try
                            {
                                XElement el = new XElement("File",
                                    new XAttribute("path", fi.FullName),
                                    new XElement("Change",
                                      new XAttribute("type", "register-file"),
                                      new XAttribute("time", DateTime.Now.ToString("G"))));
                                doc.Root.Add(el);   
                            }
                            catch (UnauthorizedAccessException UnAuthFile)
                            {
                                Console.WriteLine("UnAuthFile: {0}", UnAuthFile.Message);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException UnAuthSubDir)
                    {
                        Console.WriteLine("UnAuthSubDir: {0}", UnAuthSubDir.Message);
                    }
                }
            }
            catch (DirectoryNotFoundException DirNotFound)
            {
                Console.WriteLine("{0}", DirNotFound.Message);
            }
            catch (UnauthorizedAccessException UnAuthDir)
            {
                Console.WriteLine("UnAuthDir: {0}", UnAuthDir.Message);
            }
            catch (PathTooLongException LongPath)
            {
                Console.WriteLine("{0}", LongPath.Message);
            }

            doc.Save(filePath);
        }
    }
}