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
        public static void ZipFolder(string folderPath, string zipPath)
        {
            ZipFile.CreateFromDirectory(folderPath, zipPath);
        }

        /// <summary>
        /// Unzips a file to the given folder location.
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="restoreLocation"></param>
        public static void UnzipFile(string zipPath, string restoreLocation)
        {
            restoreLocation.ClearDirectory();
            ZipFile.ExtractToDirectory(zipPath, restoreLocation);
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

        public static void ClearDirectory(this string directory)
        {
            DirectoryInfo di = new DirectoryInfo(directory);
            foreach (FileInfo file in di.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in di.GetDirectories()) subDirectory.Delete(true);

        }
    }
}
