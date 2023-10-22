using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace Framework.model
{
    public class ConfigModel
    {
        [JsonPropertyName("browser")]
        public string Browser { get; set; }

        [JsonPropertyName("url_google_cloud")]
        public string Url_google_cloud { get; set; }

        [JsonPropertyName("url_mail_service")]
        public string Url_mail_service { get; set; }

        [JsonPropertyName("timeout")]
        public int Timeout { get; set; }

        [JsonPropertyName("screenshots_directory")]
        public string Screenshots_directory { get; set; }

        private static ConfigModel _config;

        static ConfigModel()
        {
            LoadConfiguration();
        }

        private static void LoadConfiguration()
        {
            try
            {
                string configPath = @"configuration\config.json";

                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException($"Configuration file not found at {configPath}");
                }

                string configContent = File.ReadAllText(configPath);
                _config = JsonSerializer.Deserialize<ConfigModel>(configContent);

                // Print loaded values to the console for verification
                Console.WriteLine($"Loaded Config Values:");
                Console.WriteLine($"Browser: {_config.Browser}");
                Console.WriteLine($"Url: {_config.Url_google_cloud}");
                Console.WriteLine($"Timeout: {_config.Timeout}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the configuration: {ex.Message}");
            }
        }

        public static ConfigModel GetConfiguration()
        {
            return _config ?? new ConfigModel();
        }
    }
}
