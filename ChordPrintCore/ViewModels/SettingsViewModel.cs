using System;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ChordPrintCore.Utils;
using ChordPrintCore.View;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ChordPrintCore.ViewModels
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
            ResetFileCommand = ReactiveCommand.CreateFromTask(ResetFile);

            this.WhenAnyValue(vm => vm.ParentViewModel)
                .Where(vm => vm != null)
                .DistinctUntilChanged()
                .Subscribe(vm => { ConfigFilePath = ParentViewModel._globalContext.ConfigFilePath; });
        }

        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand ResetFileCommand { get; set; }

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
            var configFilePath = _fileManager.AskUserToSelectFile("Fichier JSON|*.json");
            if (!string.IsNullOrEmpty(configFilePath))
            {
                ConfigFilePath = configFilePath;
                ConfigFileText = File.ReadAllText(ConfigFilePath);
            }
        }

        private async Task ResetFile()
        {
            var configFilePath = "myconfig.json";

            ExecuteCommand($"chordpro.exe --print-default-config > {configFilePath}");

            if (!string.IsNullOrEmpty(configFilePath))
            {
                ConfigFilePath = configFilePath;
                ConfigFileText = File.ReadAllText(ConfigFilePath);
            }
        }

        static void ExecuteCommand(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;

            Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            Console.WriteLine("ExitCode: " + exitCode, "ExecuteCommand");
            process.Close();
        }

        private async Task GoBack()
        {
            Close();
        }

        private async Task SaveSettings()
        {
            // Save configuration file
            File.WriteAllText(ConfigFilePath, ConfigFileText);

            Settings.Default.ConfigurationFilePath = ConfigFilePath;

            Settings.Default.Save();

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