using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorLibrary
{
    public static class DataAccess
    {
        private static string LoadConnectionString(string id = "FitIndex")
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void InsertOutOfCycleFileChange(string fileName, string changeType)
        {
            using (IDbConnection cnn = new SqlConnection(LoadConnectionString("LoggingDb")))
            {
                var p = new DynamicParameters();
                p.Add("@FileChanged", fileName);
                p.Add("@ChangeType", changeType);

                cnn.Execute("dbo.spOutOfCycleFileChanges_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        public static void InsertDeploymentAction(string deploymentType, string siteName)
        {
            using (IDbConnection cnn = new SqlConnection(LoadConnectionString("LoggingDb")))
            {
                var p = new DynamicParameters();
                p.Add("@DeploymentType", deploymentType);
                p.Add("@LoggedInUser", "");
                p.Add("@SiteName", siteName);

                cnn.Execute("dbo.spDeploymentLog_InsertAction", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
