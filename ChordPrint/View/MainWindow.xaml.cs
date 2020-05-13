using System.Reactive.Disposables;
using System.Windows;
using ChordPrint.ViewModels;
using ReactiveUI;

namespace ChordPrint.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
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

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as MainViewModel;
        }

        public MainViewModel ViewModel { get; set; }

        private void Editor_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            int s = Editor.SelectionStart;
            ViewModel.EditorTextCaretIndex = s;
            // get the first status bar item
        }

    }
}