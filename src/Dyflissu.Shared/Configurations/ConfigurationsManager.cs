using System.IO;
using Newtonsoft.Json;

namespace Dyflissu.Shared.Configurations
{
    /// <summary>
    /// Class for managing configurations. I didn't use default microsoft configuration classes, because didn't manage
    /// how to set them up properly not in ASP.NET application.
    /// </summary>
    public static class ConfigurationsManager
    {
        public const string SharedConfigName = "Dyflissu.json";
        public const string ServerConfigName = "Dyflissu.Server.json";
        
        /// <summary>
        /// Loads available configs.
        /// </summary>
        public static void LoadConfiguration()
        {
            LoadSharedConfiguration();
            LoadServerConfiguration();
        }

        private static void LoadServerConfiguration()
        {
            if (!File.Exists(ServerConfigName)) return;
            
            string configJson = File.ReadAllText(ServerConfigName);
            ServerConfiguration = JsonConvert.DeserializeObject<ServerConfiguration>(configJson);
        }

        private static void LoadSharedConfiguration()
        {
            if (File.Exists(SharedConfigName))
            {
                string configJson = File.ReadAllText(SharedConfigName);
                SharedConfiguration = JsonConvert.DeserializeObject<SharedConfiguration>(configJson);
            }
            else
            {
                throw new FileNotFoundException("Not found configuration file.");
            }
        }
        
        public static SharedConfiguration SharedConfiguration { get; private set; }
        public static ServerConfiguration ServerConfiguration { get; private set; }
    }
}