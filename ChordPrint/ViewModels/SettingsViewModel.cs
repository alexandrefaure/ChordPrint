using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public IParentViewModel _parentViewModel;

        public SettingsViewModel()
        {
            _fileManager = new FileManager();
            SaveCommand = ReactiveCommand.CreateFromTask(SaveSettings);
            GoBackCommand = ReactiveCommand.CreateFromTask(GoBack);
            SelectFileCommand = ReactiveCommand.CreateFromTask(SelectFile);
        }

        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        [Reactive] public string ConfigFilePath { get; set; }

        public ICommand SelectFileCommand { get; set; }
        [Reactive] public string ConfigFileText { get; set; }

        public void SetParentViewModel(IParentViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
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
            _parentViewModel._globalContext.ConfigFilePath = ConfigFilePath;
            File.WriteAllText(ConfigFilePath, ConfigFileText);
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
            var savedConfigFilePath = _parentViewModel._globalContext.ConfigFilePath;
            if (string.IsNullOrEmpty(savedConfigFilePath))
            {
                ConfigFilePath = @"C:\Users\FAURE\Dropbox\Musique\Mon SongBook\Chordii\chordproConfig.json";
            }
            else
            {
                ConfigFilePath = savedConfigFilePath;
            }

            if (!string.IsNullOrEmpty(ConfigFilePath))
            {
                ConfigFileText = File.ReadAllText(ConfigFilePath);
            }
        }
    }
}