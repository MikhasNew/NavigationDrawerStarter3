using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NavigationDrawerStarter.Configs.ManagerCore
{
    public sealed class ConfigurationManager
    {
        /// <summary>
        /// holds a reference to the single created instance, if any.
        /// </summary>
        private static readonly Lazy<ConfigurationManager> lazy = new Lazy<ConfigurationManager>(() => new ConfigurationManager());

        /// <summary>
        /// Getting reference to the single created instance, creating one if necessary.
        /// </summary>
        public static ConfigurationManager ConfigManager { get; } = lazy.Value;

        public AppConfiguration BankConfiguration { get; set; }
        public Dictionary<int, string> MccConfiguration { get; set; }
        private ConfigurationManager()
        {
            BankConfiguration = this.Read();
            MccConfiguration = this.ReadMcc();

        }
        /// <summary>
        /// Read the configuration files and return Configuration Object
        /// </summary>
        private AppConfiguration Read()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "NavigationDrawerStarter.Configs.ConfigBank.json";
            string jsonFile = "";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonFile = reader.ReadToEnd(); //Make string equal to full file
            }

            var configs = JsonConvert.DeserializeObject<AppConfiguration>(jsonFile);
            //var mccCodes = JsonConvert.DeserializeObject<Dictionary<int, string>>(jsonFile);
            return configs;
        }
        private Dictionary<int, string> ReadMcc()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "NavigationDrawerStarter.Configs.ConfigMcc.json";
            string jsonFile = "";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonFile = reader.ReadToEnd(); //Make string equal to full file
            }

            var configs = JsonConvert.DeserializeObject<Dictionary<int, string>>(jsonFile);
            //var mccCodes = JsonConvert.DeserializeObject<Dictionary<int, string>>(jsonFile);
            return configs;
        }
    }
}