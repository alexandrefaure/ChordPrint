namespace ChordPrint.ViewModels
{
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
}