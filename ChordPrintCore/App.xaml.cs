using System.Windows;
using ChordPrintCore.Utils;
using ChordPrintCore.View;
using ChordPrintCore.ViewModels;
using ReactiveUI;
using Splat;

namespace ChordPrintCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(() => new SettingsView(), typeof(IViewFor<SettingsViewModel>));
            Locator.CurrentMutable.Register(() => new FlyOutConfigurationView(),
                typeof(IViewFor<FlyOutConfigurationViewModel>));

            Locator.CurrentMutable.Register(() => new ConfigurationService(), typeof(IConfigurationService));
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}