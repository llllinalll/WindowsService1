//using System;
//using System.Diagnostics;
//using System.IO;
//using System.ServiceProcess;
//using WindowsService1;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data;
//using System.Collections.Generic;

//namespace FileMonitorService
//{
//    public partial class Service1 : ServiceBase
//    {
//        private FileSystemWatcher watcher;
//        private readonly string logFilePath = @"C:\Users\Student406-11\source\repos\WindowsService1\bin\Debug\log.txt";
//        private readonly string monitorPath = @"C:\Users\Student406-11\source\repos\WindowsService1\bin\Debug\Monitor";

//        public Service1()
//        {
//            InitializeComponent();

//            // Создание экземпляра FileSystemWatcher
//            watcher = new FileSystemWatcher()
//            {
//                Path = monitorPath,
//                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
//                Filter = "*.*"  
//            };

//            // Подписка на события
//            watcher.Created += OnChanged;
//            watcher.Changed += OnChanged;
//            watcher.Deleted += OnChanged;
//        }

//        protected override void OnStart(string[] args)
//        {
//            watcher.EnableRaisingEvents = true;
//            LogEvent("Service started.");
//        }

//        protected override void OnStop()
//        {
//            watcher.EnableRaisingEvents = false;
//            LogEvent("Service stopped.");
//        }

//        private void OnChanged(object sender, FileSystemEventArgs e)
//        {
//            string message = $"{e.ChangeType}: {e.FullPath} at {DateTime.Now}";
//            LogEvent(message);
//        }

//        private void LogEvent(string message)
//        {
//            try
//            {
//                using (StreamWriter sw = new StreamWriter(logFilePath, true))
//                {
//                    sw.WriteLine(message);
//                }
//            }
//            catch (Exception ex)
//            {
//                EventLog.WriteEntry("FileMonitorService", $"Error writing log: {ex.Message}", EventLogEntryType.Error);
//            }
//        }
//    }
//}


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

namespace VersionControl
{
    public partial class Service1 : ServiceBase
    {
        private FileSystemWatcher watcher;
        private string logFilePath = @"C:\Users\Student406-11\source\repos\WindowsService1\bin\Debug\log.txt";

        public Service1()
        {
            InitializeComponent();


            watcher = new FileSystemWatcher();
            watcher.Path = @"C:\Users\Student406-11\source\repos\WindowsService1\bin\Debug\Monitor";
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            watcher.Filter = "*.*";

            watcher.Created += OnChanged;
            watcher.Changed += OnChanged;
            watcher.Deleted += OnChanged;
        }

        protected override void OnStart(string[] args)
        {
            watcher.EnableRaisingEvents = true;
            WriteLog("Служба запущена.");
        }

        protected override void OnStop()
        {
            watcher.EnableRaisingEvents = false;
            WriteLog("Служба остановлена.");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            string message = $"{e.ChangeType}: {e.FullPath} at {DateTime.Now}";
            WriteLog(message);
        }

        private void WriteLog(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
            }
        }
    }
}