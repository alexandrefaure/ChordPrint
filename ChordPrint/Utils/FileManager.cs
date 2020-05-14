using Microsoft.Win32;

namespace ChordPrint.Utils
{
    public class FileManager
    {
        public string AskUserToSelectFile()
        {
            string inputFilePath = null;
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Fichier ChordPro|*.cho",
                Title = "Choisir le fichier à ouvrir"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                inputFilePath = openFileDialog.FileName;
            }

            return inputFilePath;
        }
    }
}