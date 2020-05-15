using System;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ChordPrint.Properties;
using ChordPrint.Utils;
using ChordPrint.View;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ChordPrint.ViewModels
{
    public interface IChildViewModel
    {
        void SetParentViewModel(IParentViewModel parentViewModel);
    }

    public class SettingsViewModel : ReactiveObject, ICloseable, IChildViewModel
    {
        private readonly FileManager _fileManager;


        public SettingsViewModel()
        {
            _fileManager = new FileManager();
            SaveCommand = ReactiveCommand.CreateFromTask(SaveSettings);
            GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);
            SelectFileCommand = ReactiveCommand.CreateFromTask(SelectFile);

            this.WhenAnyValue(vm => vm.ParentViewModel)
                .Where(vm => vm != null)
                .DistinctUntilChanged()
                .Subscribe(vm => { ConfigFilePath = ParentViewModel._globalContext.ConfigFilePath; });
        }

        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        [Reactive] public IParentViewModel ParentViewModel { get; set; }

        [Reactive] public string ConfigFilePath { get; set; }

        public ICommand SelectFileCommand { get; set; }
        [Reactive] public string ConfigFileText { get; set; }

        public void SetParentViewModel(IParentViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
            SetConfigFile();
        }

        public event EventHandler CloseRequest;

        private async Task SelectFile()
        {
            var configFilePath = _fileManager.AskUserToSelectFile();
            if (!string.IsNullOrEmpty(configFilePath))
            {
                ConfigFilePath = configFilePath;
                ConfigFileText = File.ReadAllText(ConfigFilePath);
            }
        }

        private async Task GoBack()
        {
            Close();
        }

        private async Task SaveSettings()
        {
            // Save configuration file
            File.WriteAllText(ConfigFilePath, ConfigFileText);

            Settings.Default.ConfigurationFile = ConfigFilePath;

            Close();
        }

        public event Action RequestClose;

        public virtual void Close()
        {
            if (RequestClose != null)
            {
                RequestClose();
            }
        }

        private void SetConfigFile()
        {
            if (!string.IsNullOrEmpty(ConfigFilePath))
            {
                ConfigFileText = File.ReadAllText(ConfigFilePath);
            }
        }
    }
}