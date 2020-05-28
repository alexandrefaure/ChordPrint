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


                #region Settings

                #region Titles

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

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.settings.columns,
                        view => view.ColumnNumbers.Value)
                    .DisposeWith(d);

                #endregion

                #region PDF

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.margintop,
                        view => view.margintop.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.marginbottom,
                        view => view.marginbottom.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.marginleft,
                        view => view.marginleft.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.marginright,
                        view => view.marginright.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.headspace,
                        view => view.headspace.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.footspace,
                        view => view.footspace.Value)
                    .DisposeWith(d);

                #endregion
            });

            this.Events().IsVisibleChanged.Subscribe(d => { ViewModel.Save(); });
        }
    }
}