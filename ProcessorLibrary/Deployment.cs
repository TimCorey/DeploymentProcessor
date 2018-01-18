using ProcessorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorLibrary
{
    public class Deployment
    {
        public string CurrentTask { get; set; }

        private string rootPath = ConfigurationManager.AppSettings["RootFolderPath"];
        private string backupPath = $"{ ConfigurationManager.AppSettings["RootFolderPath"] }\\Backups";

        public List<string> GetDeploymentDirectoryOptions()
        {
            return GetDeployDirectories();
        }

        private void StartMonitorService()
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = "DeploymentMonitorService";

            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                // Start the service if the current status is stopped.
                try
                {
                    // Start the service, and wait until its status is "Running".
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                }
                catch (InvalidOperationException ex)
                {
                    // TODO - Do something with the error here
                }
            }
        }

        private void StopMonitorService()
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = "DeploymentMonitorService";

            if (sc.Status == ServiceControllerStatus.Running)
            {
                // Start the service if the current status is stopped.
                try
                {
                    // Start the service, and wait until its status is "Running".
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                catch (InvalidOperationException ex)
                {
                    // TODO - Do something with the error here
                }
            }
        }

        public async Task<bool> DeploySoftwareAsync(List<string> directories, IProgress<ProgressReportModel> progressIndicator)
        {
            string rootPath = ConfigurationManager.AppSettings["RootFolderPath"];
            string backupPath = $"{ rootPath }\\Backups";

            await BackupSoftwareAsync(directories, progressIndicator);

            // Temporarily turns off the file system monitor
            StopMonitorService();

            try
            {
                // Copy Files over to new location
                for (int i = 0; i < directories.Count; i++)
                {
                    string path = directories[i];
                    CurrentTask = $"Copying Files - { path }";

                    // Records that a restore has been kicked off
                    DataAccess.InsertDeploymentAction("Deploy", path);

                    await Files.CopyFilesAsync(path + "_Staging", path, progressIndicator);
                }
            }
            catch (Exception ex)
            {
                // TODO - Do something with this error
            }
            finally
            {
                StartMonitorService();
            }

            // TODO - Make a better return value
            return true;
        }

        public async Task<bool> RollbackSoftware(List<string> directories, IProgress<ProgressReportModel> progressIndicator)
        {
            string rootPath = ConfigurationManager.AppSettings["RootFolderPath"];
            string backupPath = $"{ rootPath }\\Backups";

            // Temporarily turns off the file system monitor
            StopMonitorService();

            try
            {
                DirectoryInfo di = new DirectoryInfo(backupPath);
                FileInfo[] zipFiles = di.GetFiles("*.zip");

                // Extract zip files to given folders
                for (int i = 0; i < directories.Count; i++)
                {
                    string path = directories[i];
                    string folder = new DirectoryInfo(path).Name;

                    // Records that a restore has been kicked off
                    DataAccess.InsertDeploymentAction("Restore", path);

                    try
                    {
                        FileInfo zipFile = zipFiles.Where(x => x.Name.Contains(folder)).OrderByDescending(x => x.LastWriteTimeUtc).First();
                        await Files.UnzipFileAsync(zipFile.FullName, path, progressIndicator);
                    }
                    catch (Exception ex)
                    {
                        // TODO - put an actual error statement here
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO - Do something with this error
            }
            finally
            {
                StartMonitorService();
            }

            // TODO - Make a better return value
            return true;
        }

        public async Task<bool> BackupSoftwareAsync(List<string> directories, IProgress<ProgressReportModel> progressIndicator)
        {
            // Temporarily turns off the file system monitor
            StopMonitorService();

            try
            {
                // Back up regular folders
                for (int i = 0; i < directories.Count; i++)
                {
                    string path = directories[i];
                    string folder = new DirectoryInfo(path).Name;

                    // Records that a restore has been kicked off
                    DataAccess.InsertDeploymentAction("Backup", path);

                    CurrentTask = $"Zipping Backup - { path }";
                    await Files.ZipFolderAsync(directories[i], backupPath + $"\\{ folder } Backup { DateTime.Now.ToString("MM-dd-yyyy-HHmmss") }.zip", progressIndicator);
                }
            }
            catch (Exception ex)
            {
                // TODO - Do something with this exception
            }
            finally
            {
                StartMonitorService();
            }

            return true;
        }

        private List<string> GetDeployDirectories()
        {
            List<string> allDirectories = Files.GetTopDirectories(rootPath);
            List<string> output = new List<string>();

            foreach (var dir in allDirectories)
            {
                if (dir.Contains("_Staging"))
                {
                    output.Add(dir.Substring(0, dir.Length - "_Staging".Length));
                }
            }

            return output;
        }
    }
}
