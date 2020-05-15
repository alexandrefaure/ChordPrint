using Microsoft.Win32;

namespace ChordPrint.Utils
{
    public class FileManager
    {
        public string AskUserToSelectFile(string filter)
        {
            string inputFilePath = null;
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter,
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