using ChordPrint.Properties;

namespace ChordPrint
{
    public class GlobalContext
    {
        public string ConfigFilePath => Settings.Default.ConfigurationFile;
    }
}