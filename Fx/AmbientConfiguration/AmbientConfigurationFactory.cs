namespace XuanLibrary.Fx
{
    using System;
    using System.Configuration;
    using System.IO;

    using Microsoft.WindowsAzure.ServiceRuntime;

    public static class AmbientConfigurationFactory
    {
        public static AmbientConfiguration Create(string uriKey, string passwordKey, bool silent)
        {
            return new AmbientConfiguration(GetConfigurationUri(uriKey), GetConfigurationString(passwordKey), silent);
        }

        private static string GetConfigurationUri(string key)
        {
            // Get configuration uri
            string configurationUri = GetConfigurationString(key);

            if (string.IsNullOrEmpty(configurationUri))
            {
                // Failed to get configuration string, we cannot continue
                throw new ApplicationException("Fail to get configuration uri");
            }

            Uri uri;

            // To support relative file path
            if (Uri.TryCreate(configurationUri, UriKind.Relative, out uri))
            {
                configurationUri = Path.GetFullPath(configurationUri);
            }

            return configurationUri;
        }

        private static string GetConfigurationString(string configurationName)
        {
            // Get configuration value
            string configurationValue;

            try
            {
                // Try Azure first
                configurationValue = RoleEnvironment.GetConfigurationSettingValue(configurationName);
                Console.WriteLine($"Get configuration {configurationName}: {configurationValue} from Azure configuration.");
            }
            catch
            {
                // Fall back to config file
                configurationValue = ConfigurationManager.AppSettings[configurationName];
                Console.WriteLine($"Get configuration {configurationName}: {configurationValue} from local configuration file.");
            }

            return configurationValue;
        }
    }
}
