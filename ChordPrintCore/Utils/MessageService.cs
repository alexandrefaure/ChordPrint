using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ChordPrintCore.Utils
{
    public class MessageService
    {
        public async Task MessageBoxShowAsync(string error)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            await metroWindow.ShowMessageAsync("Error", error);
        }
    }
}