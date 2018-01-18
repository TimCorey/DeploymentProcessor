using ProcessorLibrary;
using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;

namespace DeploymentMonitor
{
    public partial class DeploymentMonitorService : ServiceBase
    {
        FileSystemWatcher watcher = new FileSystemWatcher();
        private string rootPath = ConfigurationManager.AppSettings["RootFolderPath"];

        public DeploymentMonitorService()
        {
            InitializeComponent();
            WatchRootSite();
        }

        protected override void OnStart(string[] args)
        {
            watcher.EnableRaisingEvents = true;
        }

        protected override void OnStop()
        {
            watcher.EnableRaisingEvents = false;
        }

        protected override void OnPause()
        {
            watcher.EnableRaisingEvents = false;
        }

        protected override void OnContinue()
        {
            watcher.EnableRaisingEvents = true;
        }

        private void WatchRootSite()
        {
            watcher.Path = rootPath;
            watcher.NotifyFilter = NotifyFilters.Size | NotifyFilters.FileName;
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.Changed += RootSite_OnChanged;
            watcher.Created += RootSite_OnChanged;
            watcher.Deleted += RootSite_OnChanged;
            watcher.Renamed += Watcher_Renamed;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            SaveUnauthorizedChange(e.ChangeType.ToString(), e.FullPath);
        }

        private void RootSite_OnChanged(object sender, FileSystemEventArgs e)
        {
            SaveUnauthorizedChange(e.ChangeType.ToString(), e.FullPath);
        }

        private void SaveUnauthorizedChange(string changeType, string fileName)
        {
            try
            {
                DataAccess.InsertOutOfCycleFileChange(fileName, changeType);
            }
            catch
            {
                // For now, just eat the error so we don't crash
            }
        }
    }
}
