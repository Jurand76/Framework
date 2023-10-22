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

        [JsonPropertyName("number_of_instances")]
        public int Number_of_instances { get; set; }

        [JsonPropertyName("series")]
        public string Series { get; set; }

        [JsonPropertyName("machine_type")]
        public string Machine_type { get; set; }

        [JsonPropertyName("exists_GPU")]
        public bool Exists_GPU { get; set; }

        [JsonPropertyName("number_of_GPU")]
        public int Number_of_GPU { get; set; }

        [JsonPropertyName("type_of_GPU")]
        public string Type_of_GPU { get; set; }

        [JsonPropertyName("local_SSD")]
        public string Local_SSD { get; set; }

        [JsonPropertyName("datacenter")]
        public string Datacenter { get; set; }

        [JsonPropertyName("committed_usage")]
        public string Committed_usage{ get; set; }


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
