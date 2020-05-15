using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ChordPrint.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace ChordPrint.ViewModels
{
    public interface IParentViewModel
    {
        GlobalContext _globalContext { get; }
    }

    public static class Extensions
    {
        public static Window GetView<T>(this T viewModel) where T : ReactiveObject
        {
            var view = Locator.Current.GetService<IViewFor<T>>();
            view.ViewModel = viewModel;
            var window = view as Window;
            if (window == null)
            {
                throw new TypeAccessException("view not implement IViewFor");
            }

            return window;
        }
    }

    public class MainViewModel : ReactiveObject, IParentViewModel
    {
        private readonly FileManager _fileManager;
        private readonly MessageService _messageService;

        public MainViewModel()
        {
            _globalContext = new GlobalContext();
            _messageService = new MessageService();
            _fileManager = new FileManager();

            CreateFileCommand = ReactiveCommand.CreateFromTask(CreateFile);
            OpenFileCommand = ReactiveCommand.CreateFromTask(AskUserAndOpenFile);
            ConvertCommand = ReactiveCommand.CreateFromTask(Convert);
            SaveFileCommand = ReactiveCommand.CreateFromTask(SaveAndConvert);
            OpenConfigFileSettingsCommand = ReactiveCommand.CreateFromTask(OpenConfigFileSettings);
            ConvertFolderFilesCommand = ReactiveCommand.CreateFromTask(ConvertFolderFiles);

            this.WhenAnyValue(vm => vm.CurrentFilePath)
                .Where(vm => !string.IsNullOrEmpty(vm))
                .DistinctUntilChanged()
                .Subscribe(vm => SetWindowTitle(CurrentFilePath));

            this.WhenAnyValue(vm => vm.SelectedDirective)
                .Where(vm => vm != Directive.Unknown)
                .DistinctUntilChanged()
                .Subscribe(vm => AddDirective());

            InitializeGlobalContext();

            DirectivesList = new ObservableCollection<Directive>
            {
                Directive.Titre,
                Directive.SousTitre,
                Directive.Commentaire,
                Directive.CommentaireBox,
                Directive.StartOfChorus,
                Directive.EndOfChorus,
                Directive.StartOfTab,
                Directive.DefineChord,
                Directive.Columns,
                Directive.EndOfTab
            };
        }

        [Reactive] public string EditorText { get; set; }

        [Reactive] public string PdfFilePath { get; set; }

        [Reactive] public string CurrentFilePath { get; set; }

        public ICommand OpenFileCommand { get; set; }
        public ICommand OpenConfigFileSettingsCommand { get; set; }

        public ICommand ConvertCommand { get; set; }

        public ICommand SaveFileCommand { get; set; }

        [Reactive] public string TextSize { get; set; }
        public ICommand CreateFileCommand { get; set; }

        [Reactive] public string MainWindowTitle { get; set; } = "ChordPrint";

        public ICommand ConvertFolderFilesCommand { get; set; }

        [Reactive] public Visibility ProgressBarVisibility { get; set; } = Visibility.Collapsed;

        [Reactive] public bool IsSettingsFlyoutOpened { get; set; }

        [Reactive] public IEnumerable DirectivesList { get; set; }

        [Reactive] public Directive SelectedDirective { get; set; }

        [Reactive] public int EditorTextCaretIndex { get; set; }
        [Reactive] public Visibility PdfViewerVisibility { get; set; }

        public GlobalContext _globalContext { get; }

        private async Task AddDirective()
        {
            var selectedIs = SelectedDirective;
            if (!string.IsNullOrEmpty(EditorText))
            {
                var previousCaret = EditorTextCaretIndex;
                string newDisplay = EditorText.Substring(0, EditorTextCaretIndex) + selectedIs.Value +
                                    EditorText.Substring(EditorTextCaretIndex,
                                        EditorText.Length - EditorTextCaretIndex);
                EditorText = newDisplay;
            }
        }

        private async Task ConvertFolderFiles()
        {
            ProgressBarVisibility = Visibility.Visible;
            using (FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "Sélectionner le dossier contenant les fichiers à convertir",
                //UseDescriptionForTitle = true
            })
            {
                DialogResult result = dialog.ShowDialog();
                string folderToProcess = dialog.SelectedPath;
                if (!string.IsNullOrEmpty(folderToProcess))
                {
                    DirectoryInfo d = new DirectoryInfo(folderToProcess);
                    FileInfo[] Files = d.GetFiles("*.cho");

                    foreach (FileInfo fileToConvert in Files)
                    {
                        await ConvertFileToPdf(fileToConvert.FullName);
                    }
                }
            }

            ProgressBarVisibility = Visibility.Collapsed;
        }

        private async Task CreateFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Fichier ChordPro|*.cho",
                Title = "Choisir l'emplacement du fichier à créer"
            };
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                CurrentFilePath = saveFileDialog.FileName;
                // Saves the Image via a FileStream created by the OpenFile method.
                FileStream fs =
                    (FileStream) saveFileDialog.OpenFile();
                fs.Close();
            }

            await OpenFile(CurrentFilePath);
        }

        private void InitializeGlobalContext()
        {
            //_globalContext.ConfigFilePath = Settings.Default.ConfigurationFile;
        }

        private async Task OpenConfigFileSettings()
        {
            var viewModel = new SettingsViewModel();
            viewModel.SetParentViewModel(this);

            var view = viewModel.GetView();
            view.Show();
        }

        private async Task Convert()
        {
            if (!string.IsNullOrEmpty(CurrentFilePath) && !string.IsNullOrEmpty(_globalContext.ConfigFilePath))
            {
                SaveFile();
                await ConvertFileToPdfAndPrintPdf(CurrentFilePath);
            }
        }

        private async Task AskUserAndOpenFile()
        {
            CurrentFilePath = GetFilePath();
            await OpenFile(CurrentFilePath);
        }

        private async Task OpenFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            EditorText = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(_globalContext.ConfigFilePath))
            {
                await ConvertFileToPdfAndPrintPdf(CurrentFilePath);
            }
        }

        private void SetWindowTitle(string fileName)
        {
            MainWindowTitle = $"ChordPrint - {fileName}";
        }

        private string GetFilePath()
        {
            var inputFilePath = _fileManager.AskUserToSelectFile("Fichier ChordPro|*.cho");
            if (string.IsNullOrEmpty(inputFilePath))
            {
                return null;
            }

            return inputFilePath;
        }

        private async Task SaveAndConvert()
        {
            if (!string.IsNullOrEmpty(CurrentFilePath) && !string.IsNullOrEmpty(_globalContext.ConfigFilePath))
            {
                SaveFile();

                await ConvertFileToPdfAndPrintPdf(CurrentFilePath);
            }
        }

        private void SaveFile()
        {
            File.WriteAllText(CurrentFilePath, EditorText);
        }

        private async Task ConvertFileToPdfAndPrintPdf(string filePath)
        {
            PdfFilePath = null;
            var resultPdfPath = await ConvertFileToPdf(filePath);

            if (File.Exists(resultPdfPath))
            {
                PdfFilePath = "file:///" + resultPdfPath;
            }
        }

        private async Task<string> ConvertFileToPdf(string filePath)
        {
            var exePath = "chordpro.exe";
            var configFilePath = _globalContext.ConfigFilePath;
            if (string.IsNullOrEmpty(configFilePath))
            {
                await ShowErrorMessage("Chordpro config file not defined");
                return null;
            }

            var filename = Path.GetFileName(filePath);
            if (string.IsNullOrEmpty(filename))
            {
                await ShowErrorMessage("Input file not defined");
                return null;
            }

            var directoryPath = Path.GetDirectoryName(filePath);
            if (string.IsNullOrEmpty(directoryPath))
            {
                await ShowErrorMessage("Directory not found");
                return null;
            }

            var outputFilePath = directoryPath + "\\" + filename.Replace(".cho", ".pdf");
            var arguments = $"\"{filePath}\" -o \"{outputFilePath}\" --cfg \"{configFilePath}\"";

            if (!string.IsNullOrEmpty(TextSize))
            {
                arguments += $" --text-size={TextSize}";
            }

            var result = await ProcessAsyncHelper.ExecuteShellCommand(exePath, arguments, int.MaxValue);

            var error = result.Output;
            if (!string.IsNullOrEmpty(error))
            {
                await ShowErrorMessage(error);
                return null;
            }

            return outputFilePath;
        }

        private async Task ShowErrorMessage(string errorMessage)
        {
            PdfViewerVisibility = Visibility.Hidden;
            await _messageService.MessageBoxShowAsync(errorMessage);
            PdfViewerVisibility = Visibility.Visible;
        }
    }
}