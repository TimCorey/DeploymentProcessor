using Dapper;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace ProcessorLibrary
{
    public static class DataAccess
    {
        private static readonly string LogDatabase = "LoggingDb";
        private static readonly ConcurrentDictionary<string, DbProviderFactory> ProviderFactories = new ConcurrentDictionary<string, DbProviderFactory>();

        public static int InsertDeploymentAction(string deploymentType, string siteName)
        {
            var param = new
            {
                DeploymentType = deploymentType,
                LoggedInUser = string.Empty,
                SiteName = siteName
            };

            return RunProcedure(LogDatabase, "dbo.spDeploymentLog_InsertAction", param);
        }

        public static int InsertOutOfCycleFileChange(string fileName, string changeType)
        {
            var param = new
            {
                FileChange = fileName,
                ChangeType = changeType
            };

            return RunProcedure(LogDatabase, "dbo.spOutOfCycleFileChanges_Insert", param);
        }

        private static int RunProcedure(string connectionKey, string procedureName, object param = null)
        {
            using (IDbConnection connection = GetConnection(connectionKey))
            {
                return connection.Execute(procedureName, param, commandTimeout: connection.ConnectionTimeout, commandType: CommandType.StoredProcedure);
            }
        }

        private static Task<int> RunProcedureAsync(string connectionKey, string procedureName, object param = null)
        {
            using (IDbConnection connection = GetConnection(connectionKey))
            {
                return connection.ExecuteAsync(procedureName, param, commandTimeout: connection.ConnectionTimeout, commandType: CommandType.StoredProcedure);
            }
        }

        private static IDbConnection GetConnection(string connectionName)
        {
            ConnectionStringSettings settings = Configuration.GetConnectionSettings(connectionName);
            DbProviderFactory factory = GetProviderFactory(settings.ProviderName);

            try
            {
                IDbConnection connection = factory.CreateConnection();
                connection.ConnectionString = settings.ConnectionString;
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception($"{factory.GetType().Name} failed to create a connection.", ex);
            }
        }

        private static DbProviderFactory GetProviderFactory(string providerName = "System.Data.SqlClient")
        {
            DbProviderFactory providerFactory;
            if (!ProviderFactories.TryGetValue(providerName, out providerFactory))
            {
                providerFactory = DbProviderFactories.GetFactory(providerName);
                if (providerFactory == null)
                    throw new NotSupportedException($"{providerName} database provider is not available.");

                ProviderFactories[providerName] = providerFactory;
            }

            return providerFactory;
        }
    }
}
