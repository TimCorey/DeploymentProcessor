using ProcessorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace DeploymentConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Deployment Processor");
            Console.WriteLine();

            bool doneWithDeployer = false;

            do
            {
                Console.Write("Specify Process (1 - Deploy, 2 - Restore, 3 - Backup): ");
                string action = Console.ReadLine();
                Console.WriteLine();

                if (action == "1")
                {
                    doneWithDeployer = DeploySoftware();
                }
                else if (action == "2")
                {
                    doneWithDeployer = RollbackSoftware();
                }
                else if (action == "3")
                {
                    doneWithDeployer = BackupSoftware();
                }
                else
                {
                    Console.WriteLine("Sorry, not an option.");
                } 
            } while (!doneWithDeployer);

            Console.WriteLine("Application complete. Hit enter to close.");

            Console.ReadLine();
        }

        private static bool BackupSoftware()
        {
            List<string> directories = GetDeployDirectories();

            string rootPath = ConfigurationManager.AppSettings["RootFolderPath"];
            string backupPath = $"{ rootPath }\\Backups";

            Console.WriteLine("Which directories, in which order, do you want to back up (comma-separated): ");

            for (int i = 0; i < directories.Count; i++)
            {
                Console.WriteLine($"{ i } - { directories[i] }");
            }

            Console.WriteLine();
            string[] deploy = Console.ReadLine().Split(',');

            // Back up regular folders
            for (int i = 0; i < deploy.Length; i++)
            {
                string path = directories[int.Parse(deploy[i])];
                string folder = new DirectoryInfo(path).Name;

                Files.ZipFolder(directories[int.Parse(deploy[i])], backupPath + $"\\{ folder } Backup { DateTime.Now.ToString("MM-dd-yyyy-HHmmss") }.zip");
            }

            Console.WriteLine("Backups Complete");
            Console.WriteLine();

            return CheckForDone();
        }

        private static bool DeploySoftware()
        {
            List<string> directories = GetDeployDirectories();

            string rootPath = ConfigurationManager.AppSettings["RootFolderPath"];
            string backupPath = $"{ rootPath }\\Backups";

            Console.WriteLine("Which directories, in which order, do you want to deploy (comma-separated): ");

            for (int i = 0; i < directories.Count; i++)
            {
                Console.WriteLine($"{ i } - { directories[i] }");
            }

            Console.WriteLine();
            string[] deploy = Console.ReadLine().Split(',');

            // Back up regular folders
            for (int i = 0; i < deploy.Length; i++)
            {
                string path = directories[int.Parse(deploy[i])];
                string folder = new DirectoryInfo(path).Name;

                Files.ZipFolder(directories[int.Parse(deploy[i])], backupPath + $"\\{ folder } Backup { DateTime.Now.ToString("MM-dd-yyyy-HHmmss") }.zip");
            }

            Console.WriteLine("Backups Complete");
            Console.WriteLine();

            // Copy Files over to new location
            for (int i = 0; i < deploy.Length; i++)
            {
                string path = directories[int.Parse(deploy[i])];
                CopyFiles(path + "_Staging", path);
            }

            return CheckForDone();
        }

        private static bool RollbackSoftware()
        {
            List<string> directories = GetDeployDirectories();

            string rootPath = ConfigurationManager.AppSettings["RootFolderPath"];
            string backupPath = $"{ rootPath }\\Backups";

            DirectoryInfo di = new DirectoryInfo(backupPath);
            FileInfo[] zipFiles = di.GetFiles("*.zip");

            Console.WriteLine("Which directories, in which order, do you want to restore (comma-separated): ");

            for (int i = 0; i < directories.Count; i++)
            {
                Console.WriteLine($"{ i } - { directories[i] }");
            }

            Console.WriteLine();
            string[] deploy = Console.ReadLine().Split(',');

            // Extract zip files to given folders
            for (int i = 0; i < deploy.Length; i++)
            {
                string path = directories[int.Parse(deploy[i])];
                string folder = new DirectoryInfo(path).Name;

                try
                {
                    FileInfo zipFile = zipFiles.Where(x => x.Name.Contains(folder)).OrderByDescending(x => x.LastWriteTimeUtc).First();
                    Files.UnzipFile(zipFile.FullName, path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was an error trying to restore { folder }: { ex.Message }");
                }
            }

            Console.WriteLine("Rollback Complete");
            Console.WriteLine();
            return CheckForDone();
        }

        private static bool CheckForDone()
        {
            Console.Write("Are you done with the deployment processor (yes/no): ");
            string doneDeploying = Console.ReadLine();
            Console.WriteLine();

            return (doneDeploying == "yes");
        }

        private static void CopyFiles(string fromDir, string toDir)
        {
            List<FileInfo> files = new List<FileInfo>();
            List<string> errors = new List<string>();

            EnumerateFiles(fromDir, files);

            foreach (var file in files)
            {
                try
                {
                    string destPath = GetDestinationPath(file, fromDir, toDir);
                    File.Copy(file.FullName, destPath, true);
                }
                catch (Exception ex)
                {
                    errors.Add($"{ file }: { ex.Message }");
                    Console.WriteLine($"{ file }: { ex.Message }");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"{ files.Count - errors.Count } files copied; { errors.Count } errors.");
            Console.WriteLine();
        }

        private static string GetDestinationPath(FileInfo file, string baseDir, string destDir)
        {
            string output = file.FullName.Substring(baseDir.Length);

            output = $"{ destDir }{ output }";

            // Makes sure the path exists
            string path = output.Substring(0, output.Length - file.Name.Length);
            Directory.CreateDirectory(path);

            return output;
        }

        private static List<string> GetDeployDirectories()
        {
            List<string> allDirectories = Files.GetTopDirectories(ConfigurationManager.AppSettings["RootFolderPath"]);
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

        private static void EnumerateFiles(string fullPath, List<FileInfo> fileInfoList)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(fullPath);
                FileInfo[] files = di.GetFiles();

                foreach (FileInfo file in files)
                    fileInfoList.Add(file);

                //Scan recursively
                DirectoryInfo[] dirs = di.GetDirectories();
                if (dirs == null || dirs.Length < 1)
                    return;
                foreach (DirectoryInfo dir in dirs)
                    EnumerateFiles(dir.FullName, fileInfoList);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"File access exception: { ex.Message }");
            }
        }
    }
}
