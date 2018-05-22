using System;
using System.Collections.Concurrent;
using System.Configuration;

namespace ProcessorLibrary
{
    public static class Configuration
    {
        private static readonly string DefaultProviderKey = "DefaultProvider";
        private static readonly string DefaultProvider = GetAppSetting(DefaultProviderKey);
        private static readonly ConcurrentDictionary<string, ConnectionStringSettings> ConnectionSettings = new ConcurrentDictionary<string, ConnectionStringSettings>();

        public static ConnectionStringSettings LoadConnectionSettings(string connectionKey)
        {
            if (string.IsNullOrEmpty(connectionKey))
                throw new ArgumentNullException(nameof(connectionKey));

            ConnectionStringSettings settings;
            if (!ConnectionSettings.TryGetValue(connectionKey, out settings))
            {
                string connectionName = GetAppSetting(connectionKey, true);
                if (!string.IsNullOrEmpty(connectionName))
                {
                    // A named appSetting was found, use the value as the connection name
                    settings = GetConnectionSettings(connectionName);
                }
                else
                {
                    // Use the connectionKey key as the connection name
                    settings = GetConnectionSettings(connectionKey);
                }

                ConnectionSettings[connectionKey] = settings;
            }

            return settings;
        }

        public static ConnectionStringSettings GetConnectionSettings(string connectionName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionName];
            if (settings == null)
                throw new SettingsPropertyNotFoundException($"{connectionName} connection not found.");

            if (string.IsNullOrEmpty(settings.ConnectionString))
                throw new ArgumentNullException($"{connectionName} connectionString value is blank.");

            // When no provider is specified in the ConnectionString use the default
            if (string.IsNullOrEmpty(settings.ProviderName))
            {
                settings.ProviderName = DefaultProvider;
            }

            return settings;
        }

        public static string GetAppSetting(string settingKey, bool allowNulls = false)
        {
            string settingValue = ConfigurationManager.AppSettings[settingKey];
            if (!allowNulls && settingValue == null)
                throw new SettingsPropertyNotFoundException($"{settingKey} appSetting not found.");

            return settingValue;
        }
    }
}
