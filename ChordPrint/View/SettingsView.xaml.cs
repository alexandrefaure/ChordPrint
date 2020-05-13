using System;
using System.Reactive.Disposables;
using System.Windows;
using ChordPrint.ViewModels;
using ReactiveUI;

namespace ChordPrint.View
{
    public interface ICloseable
    {
        event EventHandler CloseRequest;
    }

    /// <summary>
    ///     Logique d'interaction pour SettingsView.xaml
    /// </summary>
    public partial class SettingsView : IViewFor<SettingsViewModel>
    {
        public SettingsView()
        {
            InitializeComponent();
            ViewModel = new SettingsViewModel();
            DataContext = ViewModel;

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigFilePath,
                        view => view.ConfigFileTextBox.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigFileText,
                        view => view.ConfigFileEditor.Text)
                    .DisposeWith(disposableRegistration);

                BindCommands(disposableRegistration);
            });
        }

        private void BindCommands(CompositeDisposable disposableRegistration)
        {
            this.BindCommand(ViewModel,
                    viewModel => viewModel.SaveCommand,
                    view => view.SaveButton)
                .DisposeWith(disposableRegistration);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.GoBackCommand,
                    view => view.BackButton)
                .DisposeWith(disposableRegistration);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.SelectFileCommand,
                    view => view.OpenFileButton)
                .DisposeWith(disposableRegistration);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as SettingsViewModel;
        }

        public SettingsViewModel ViewModel { get; set; }

        private void SettingsView_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = ViewModel;
            ViewModel.RequestClose += () =>
            {
                Close();
            };
        }
    }
}