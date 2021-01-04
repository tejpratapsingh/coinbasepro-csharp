using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoinbasePro.Client
{
    public  class Config
    {
        public  string Folder { get; set; }
        public  string PassPhrase { get; set; }
        public  string ApiKey { get; set; }
        public  string ApiSecret { get; set; }
    }

    public  class ConfigManager
    {
        public Config GetConfig()
        {
            Config config;

            var config_file = ConfigurationManager.AppSettings.Get("file_path");

            
            var lines = File.ReadAllText(config_file);

            config = JsonConvert.DeserializeObject<Config>(lines);

            return config;
        }
    }

    public static class StringExtensions
    {
        public static string ToPath(this string fileName, Config config)
        {
            return Path.Combine(config.Folder, $"{fileName}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.json");
        }
    }
}
