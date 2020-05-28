using ChordPrint.ViewModels;

namespace ChordPrint.Utils
{
    public interface IConfigurationService
    {
        void SaveConfiguration(ConfigurationFile configurationFile);
        ConfigurationFile LoadConfigurationFile();
        string LoadConfigurationFileText();
    }
}