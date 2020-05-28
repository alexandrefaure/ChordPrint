using ChordPrint.Properties;
using ChordPrint.ViewModels;
using Newtonsoft.Json;

namespace ChordPrint.Utils
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ConfigurationFile _defaultConfigFile = new ConfigurationFile
        {
            settings = new ConfigSettings
            {
                titles = "center",
                columns = "2"
            },
            pdf = new ConfigPdf
            {
                fonts = new ConfigFonts
                {
                    title = new FontElement
                    {
                        name = "Arial",
                        size = 16
                    },
                    subtitle = new FontElement
                    {
                        name = "Arial italic",
                        size = 10,
                        color = "dark grey"
                    },
                    text = new FontElement
                    {
                        name = "Arial",
                        size = 9
                    },
                    comment = new FontElement
                    {
                        name = "Arial",
                        size = 9
                    },
                    chord = new FontElement
                    {
                        name = "Arial bold",
                        size = 8,
                        color = "dark blue"
                    }
                },
                diagrams = new ConfigDiagrams
                {
                    show = "below",
                    width = 9,
                    height = 9,
                    hspace = 3.95,
                    vspace = 3,
                    vcells = 4,
                    linewidth = 0.1
                },
                margintop = 80,
                marginbottom = 40,
                marginleft = 30,
                marginright = 30,
                headspace = 60,
                footspace = 20
            }
        };

        public void SaveConfiguration(ConfigurationFile configurationFile)
        {
            Settings.Default.ConfigurationFile = JsonConvert.SerializeObject(configurationFile);
            Settings.Default.Save();
        }

        public ConfigurationFile LoadConfigurationFile()
        {
            var settingsConfigurationFile = Settings.Default.ConfigurationFile;
            if (string.IsNullOrEmpty(settingsConfigurationFile))
            {
                return _defaultConfigFile;
            }

            return
                JsonConvert.DeserializeObject<ConfigurationFile>(settingsConfigurationFile);
        }

        public string LoadConfigurationFileText()
        {
            return Settings.Default.ConfigurationFile;
        }
    }
}