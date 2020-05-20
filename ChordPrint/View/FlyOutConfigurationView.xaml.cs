using System;
using System.Reactive.Disposables;
using System.Windows.Controls;
using ChordPrint.Utils;
using ChordPrint.ViewModels;
using ReactiveUI;
using Splat;

namespace ChordPrint.View
{
    /// <summary>
    /// Logique d'interaction pour FlyOutConfigurationView.xaml
    /// </summary>
    public partial class FlyOutConfigurationView
    {
        public FlyOutConfigurationView()
        {
            InitializeComponent();
            ViewModel = new FlyOutConfigurationViewModel(Locator.Current.GetService<IConfigurationService>());
            this.WhenActivated(d =>
            {
                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.settings.columns,
                        view => view.ColumnNumberTextBox.Text)
                    .DisposeWith(d);
            });

            this.Events().IsVisibleChanged.Subscribe(d => { ViewModel.Save(); });
        }
    }
}