using ChordPrintCore.ViewModels;

namespace ChordPrintCore.Utils
{
    public interface IConfigurationService
    {
        void SaveConfiguration(ConfigurationFile configurationFile);
        ConfigurationFile LoadConfigurationFile();
        string LoadConfigurationFileText();
    }
}