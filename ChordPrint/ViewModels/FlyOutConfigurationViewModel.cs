using ChordPrint.Utils;
using ReactiveUI.Fody.Helpers;

namespace ChordPrint.ViewModels
{
    public class FlyOutConfigurationViewModel
    {
        private readonly IConfigurationService _configurationService;

        public FlyOutConfigurationViewModel(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            ConfigurationFile = _configurationService.LoadConfigurationFile();
        }

        [Reactive] public ConfigurationFile ConfigurationFile { get; set; }

        public void Save()
        {
            _configurationService.SaveConfiguration(ConfigurationFile);
        }
    }

    public class ConfigurationFile
    {
        public ConfigSettings settings { get; set; }

        public ConfigPdf pdf { get; set; }
    }

    public class ConfigPdf
    {
        public ConfigFonts fonts { get; set; }
        public ConfigDiagrams diagrams { get; set; }
        public int margintop { get; set; }
        public int marginbottom { get; set; }
        public int marginleft { get; set; }
        public int marginright { get; set; }
        public int headspace { get; set; }
        public int footspace { get; set; }
    }

    public class ConfigDiagrams
    {
        public string show { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public double hspace { get; set; }
        public int vspace { get; set; }
        public int vcells { get; set; }
        public double linewidth { get; set; }
    }

    public class ConfigFonts
    {
        public FontElement title { get; set; }
        public FontElement subtitle { get; set; }
        public FontElement text { get; set; }
        public FontElement comment { get; set; }
        public FontElement chord { get; set; }
    }

    public class FontElement
    {
        public string name { get; set; }
        public int size { get; set; }
        public string color { get; set; }
    }

    public class ConfigSettings
    {
        public string titles { get; set; }
        public string columns { get; set; }
    }
}