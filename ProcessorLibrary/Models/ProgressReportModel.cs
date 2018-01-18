using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorLibrary.Models
{
    public class ProgressReportModel
    {
        public string ActionName { get; set; } = "";
        public int ProgressPercentage { get; set; } = 0;
    }
}
