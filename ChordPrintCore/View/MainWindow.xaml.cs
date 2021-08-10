using System.Reactive.Disposables;
using System.Windows;
using ChordPrintCore.Utils;
using ChordPrintCore.ViewModels;
using MahApps.Metro.Controls;
using ReactiveUI;
using Splat;

namespace ChordPrintCore.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IViewFor<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(Locator.Current.GetService<IConfigurationService>());
            DataContext = ViewModel;
            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel,
                        viewModel => viewModel.MainWindowTitle,
                        view => view.Title)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.EditorText,
                        view => view.Editor.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.PdfFilePath,
                        view => view.pdfViewer.Source)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.PdfViewerVisibility,
                        view => view.pdfViewer.Visibility)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.TextSize,
                        view => view.TextSize.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.ProgressBarVisibility,
                        view => view.ProgressBar.Visibility)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.IsSettingsFlyoutOpened,
                        view => view.settingsFlyout.IsOpen)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.IsSettingsFlyoutOpened,
                        view => view.SettingsHeaderButton.IsChecked)
                    .DisposeWith(disposableRegistration);

                //Combobox
                this.Bind(ViewModel,
                        viewModel => viewModel.DirectivesList,
                        view => view.AddObjectComboBox.ItemsSource)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.SelectedDirective,
                        view => view.AddObjectComboBox.SelectionBoxItem)
                    .DisposeWith(disposableRegistration);

                BindCommands(disposableRegistration);
            });
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as MainViewModel;
        }

        public MainViewModel ViewModel { get; set; }

        private void BindCommands(CompositeDisposable disposableRegistration)
        {
            this.BindCommand(ViewModel,
                    viewModel => viewModel.CreateFileCommand,
                    view => view.NewFileButton)
                .DisposeWith(disposableRegistration);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.OpenFileCommand,
                    view => view.OpenButton)
                .DisposeWith(disposableRegistration);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.SaveFileCommand,
                    view => view.SaveButton)
                .DisposeWith(disposableRegistration);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.OpenConfigFileSettingsCommand,
                    view => view.ConfigFileSettingsButton)
                .DisposeWith(disposableRegistration);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.ConvertFolderFilesCommand,
                    view => view.ConvertFolderFiles)
                .DisposeWith(disposableRegistration);
        }

        private void Editor_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            int s = Editor.SelectionStart;
            ViewModel.EditorTextCaretIndex = s;
            // get the first status bar item
        }
    }
}