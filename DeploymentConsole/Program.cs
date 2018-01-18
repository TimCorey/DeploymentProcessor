using ProcessorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using ProcessorLibrary.Models;

namespace DeploymentConsole
{
    class Program
    {
        private static string currentTask = "";
        private static int fileSize = 0;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Deployment Processor");
            Console.WriteLine();

            Deployment deploy = new Deployment();
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>(ReportProgress);

            bool doneWithDeployer = false;

            do
            {
                Console.Write("Specify Process (1 - Deploy, 2 - Restore, 3 - Backup): ");
                string action = Console.ReadLine();
                Console.WriteLine();

                if (action == "1")
                {
                    //doneWithDeployer = await deploy.DeploySoftwareAsync(progress);
                }
                else if (action == "2")
                {
                    //doneWithDeployer = deploy.RollbackSoftware();
                }
                else if (action == "3")
                {
                    //doneWithDeployer = BackupSoftware();
                }
                else
                {
                    Console.WriteLine("Sorry, not an option.");
                } 
            } while (!doneWithDeployer);

            Console.WriteLine("Application complete. Hit enter to close.");

            Console.ReadLine();
        }

        private static void ReportProgress(ProgressReportModel value)
        {
            Console.WriteLine($"{ value.ActionName }: { value.ProgressPercentage }% done");
        }

        private static void ReportFileProgress(long value)
        {
            int size = (int)(value / 1024 / 1024);

            if (size > fileSize + 5)
            {
                Console.WriteLine($"{ currentTask }: { size }MB done");
                fileSize = size;
            }
        }

        

        

        private static bool CheckForDone()
        {
            Console.Write("Are you done with the deployment processor (yes/no): ");
            string doneDeploying = Console.ReadLine().ToLower();
            Console.WriteLine();

            return (doneDeploying == "yes");
        }

        
        
    }
}
