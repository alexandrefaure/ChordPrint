namespace ChordPrintCore.ViewModels
{
    public class ConfigPdf
    {
        public ConfigFonts fonts { get; set; }
        public ConfigDiagrams diagrams { get; set; }
        public string margintop { get; set; }
        public string marginbottom { get; set; }
        public string marginleft { get; set; }
        public string marginright { get; set; }
        public string headspace { get; set; }
        public string footspace { get; set; }
    }
}