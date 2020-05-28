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
                        view => view.ColumnNumbers.Value)
                    .DisposeWith(d);

                #region Settings

                this.OneWayBind(ViewModel,
                        viewModel => viewModel.SettingsTitlePositions,
                        view => view.TitlePositionComboBox.ItemsSource)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.SelectedSettingsTitle,
                        view => view.TitlePositionComboBox.SelectedItem)
                    .DisposeWith(d);

                TitlePositionComboBox.Events().SelectionChanged.Subscribe(e =>
                {
                    ViewModel.ConfigurationFile.settings.titles =
                        TitlePositionComboBox.SelectedItem.ToString().ToLower();
                });

                #endregion
            });

            this.Events().IsVisibleChanged.Subscribe(d => { ViewModel.Save(); });
        }
    }
}