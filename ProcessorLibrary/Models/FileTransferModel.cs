using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorLibrary.Models
{
    /// <summary>
    /// Information about the transfer of a single
    /// file from one location to another.
    /// </summary>
    public class FileTransferModel
    {
        /// <summary>
        /// The file that was transferred.
        /// </summary>
        public FileInfo File { get; set; }

        /// <summary>
        /// Identifies if the transfer was successful.
        /// </summary>
        public bool WasSuccessful { get; set; } = false;

        /// <summary>
        /// Where the file was going.
        /// </summary>
        public string Destination { get; set; } = "";

        /// <summary>
        /// The exception that was thrown, if any.
        /// </summary>
        public Exception TransferError { get; set; }
    }
}
