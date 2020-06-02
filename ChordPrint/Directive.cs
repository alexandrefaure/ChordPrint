namespace ChordPrint
{
    public class Directive
    {
        private Directive(string displayName, string value)
        {
            Value = value;
            DisplayName = displayName;
        }

        public string Value { get; set; }
        public string DisplayName { get; set; }

        public static Directive Titre
        {
            get { return new Directive("Titre", "{title:}"); }
        }

        public static Directive SousTitre
        {
            get { return new Directive("Sous-titre", "{subtitle:}"); }
        }

        public static Directive Commentaire
        {
            get { return new Directive("Commentaire", "{comment:}"); }
        }

        public static Directive CommentaireBox
        {
            get { return new Directive("Commentaire box", "{comment_box:}"); }
        }

        public static Directive StartOfChorus
        {
            get { return new Directive("Début refrain", "{soc}"); }
        }

        public static Directive EndOfChorus
        {
            get { return new Directive("Fin refrain", "{eoc}"); }
        }

        public static Directive StartOfTab
        {
            get { return new Directive("Début tablature", "{sot}"); }
        }

        public static Directive EndOfTab
        {
            get { return new Directive("Fin tablature", "{eot}"); }
        }

        public static Directive DefineChord
        {
            get { return new Directive("Redéfinir accord", "{define: Am7 base-fret 5 frets 1 3 1 1 1 1}"); }
        }

        public static Directive AddChord
        {
            get { return new Directive("Ajouter accord", "{chord: Am7 base-fret 5 frets 1 3 1 1 1 1}"); }
        }

        public static Directive Columns
        {
            get { return new Directive("Colonnes", "{columns: 1}"); }
        }

        public static Directive Unknown
        {
            get { return new Directive("Inconnu", "unknown"); }
        }
    }
}