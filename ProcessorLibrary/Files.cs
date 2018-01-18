using ProcessorLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorLibrary
{
    public static class Files
    {

        /// <summary>
        /// Zips the contents of a given folder.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="zipPath"></param>
        public static void ZipFolder(string folderPath, string zipPath, IProgress<ProgressReportModel> progress)
        {
            bool stillZipping = true;
            long targetFolderSize = Directory.GetFiles(folderPath).Select(x => new FileInfo(x).Length).Aggregate((a, b) => a + b);

            Task.Run(() => {
                ZipFile.CreateFromDirectory(folderPath, zipPath, CompressionLevel.NoCompression, false);
                stillZipping = false;
            });

            while (stillZipping)
            {
                if (File.Exists(zipPath))
                {
                    var fileInfo = new FileInfo(zipPath);
                    //if ((int)((fileInfo.Length * 100) / targetFolderSize) > 30)
                    //{
                    //    throw new FileLoadException("Zip file just too massive.");
                    //}
                    progress.Report(new ProgressReportModel { ActionName = $"Zipping Folder - { folderPath }", ProgressPercentage = (int)((fileInfo.Length * 100) / targetFolderSize) });
                }
            }
        }

        public async static Task ZipFolderAsync(string folderPath, string zipPath, IProgress<ProgressReportModel> progress)
        {
            await Task.Run(() => ZipFolder(folderPath, zipPath, progress));
        }

        /// <summary>
        /// Unzips a file to the given folder location.
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="restoreLocation"></param>
        public static void UnzipFile(string zipPath, string restoreLocation, IProgress<ProgressReportModel> progress)
        {
            bool stillZipping = true;
            var totalSize = new FileInfo(zipPath).Length;

            restoreLocation.ClearDirectory();

            Task.Run(() => {
                ZipFile.ExtractToDirectory(zipPath, restoreLocation);
                stillZipping = false;
            });
            
            while (stillZipping)
            {
                long targetFolderSize = 0;
                var files = Directory.GetFiles(restoreLocation);

                if (files.Length > 0)
                {
                    targetFolderSize = files.Select(x => new FileInfo(x).Length).Aggregate((a, b) => a + b);
                }

                progress.Report(new ProgressReportModel { ActionName = $"Restoring Folder - { restoreLocation }", ProgressPercentage = (int)((targetFolderSize * 100) / totalSize) });
            }
        }

        public async static Task UnzipFileAsync(string zipPath, string restoreLocation, IProgress<ProgressReportModel> progress)
        {
            await Task.Run(() => UnzipFile(zipPath, restoreLocation, progress));
        }

        /// <summary>
        /// Gets a list of the sub-directories (only the top-level ones) in a directory
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static List<string> GetTopDirectories(string rootPath)
        {
            return Directory.GetDirectories(rootPath).ToList();
        }

        /// <summary>
        /// Deletes all of the files and folders in a directory.
        /// </summary>
        /// <param name="directory">The directory to clear.</param>
        public static void ClearDirectory(this string directory)
        {
            DirectoryInfo di = new DirectoryInfo(directory);
            
            di.GetFiles().ToList().ForEach(x => x.Delete());
            di.GetDirectories().ToList().ForEach(x => x.Delete(true));

        }

        /// <summary>
        /// Gets a list of every file with its complete path in the given folder.
        /// </summary>
        /// <param name="fullPath">The root path to look in.</param>
        /// <param name="fileInfoList">A list of all of the files found.</param>
        public static void EnumerateFiles(string fullPath, List<FileInfo> fileInfoList)
        {
            DirectoryInfo di = new DirectoryInfo(fullPath);
            FileInfo[] files = di.GetFiles();

            files.ToList().ForEach(x => fileInfoList.Add(x));

            //Scan recursively
            DirectoryInfo[] dirs = di.GetDirectories();
            if (dirs == null || dirs.Length < 1)
            {
                return;
            }

            dirs.ToList().ForEach(x => EnumerateFiles(x.FullName, fileInfoList));
        }

        public static async Task<int> CopyFilesAsync(string fromDir, string toDir, IProgress<ProgressReportModel> progress)
        {
            List<FileInfo> files = new List<FileInfo>();
            List<string> errors = new List<string>();
            int totalFiles = 0;

            Files.EnumerateFiles(fromDir, files);

            int filesCopied = await Task.Run<int>(async () =>
            {
                totalFiles = files.Count;
                int filesProcessed = 0;

                foreach (var file in files)
                {
                    try
                    {
                        string destPath = GetDestinationPath(file, fromDir, toDir);
                        bool result = await CopyFileAsync(file, destPath);
                        if (result)
                        {
                            filesProcessed += 1;
                            progress.Report(new ProgressReportModel { ProgressPercentage = (filesProcessed * 100) / totalFiles, ActionName = $"Copy Files - { fromDir }" });
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"{ file }: { ex.Message }");
                        Console.WriteLine($"{ file }: { ex.Message }");
                    }
                }

                return filesProcessed;
            });

            return filesCopied;
        }

        private static async Task<bool> CopyFileAsync(FileInfo file, string destination)
        {
            try
            {
                await Task.Run(() => File.Copy(file.FullName, destination, true));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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
    }
}
