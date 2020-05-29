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

                #region Title

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.title.size,
                        view => view.TitleSize.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.title.color,
                        view => view.TitleColor.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.title.name,
                        view => view.TitlePolice.Text)
                    .DisposeWith(d);

                #endregion

                #region SubTitle

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.subtitle.size,
                        view => view.SubTitleSize.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.subtitle.color,
                        view => view.SubTitleColor.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.subtitle.name,
                        view => view.SubTitlePolice.Text)
                    .DisposeWith(d);

                #endregion

                #region Text

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.text.size,
                        view => view.TextSize.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.text.color,
                        view => view.TextColor.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.text.name,
                        view => view.TextPolice.Text)
                    .DisposeWith(d);

                #endregion

                #region Comment

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.comment.size,
                        view => view.CommentSize.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.comment.color,
                        view => view.CommentColor.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.comment.name,
                        view => view.CommentPolice.Text)
                    .DisposeWith(d);

                #endregion

                #region Chord

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.chord.size,
                        view => view.ChordSize.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.chord.color,
                        view => view.ChordColor.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.fonts.chord.name,
                        view => view.ChordPolice.Text)
                    .DisposeWith(d);

                #endregion

                #region Diagrams

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.diagrams.show,
                        view => view.DiagramPosition.Text)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.diagrams.width,
                        view => view.DiagramWidth.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.diagrams.height,
                        view => view.DiagramHeight.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.diagrams.hspace,
                        view => view.DiagramHSpace.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.diagrams.vspace,
                        view => view.DiagramVSpace.Value)
                    .DisposeWith(d);

                this.Bind(ViewModel,
                        viewModel => viewModel.ConfigurationFile.pdf.diagrams.linewidth,
                        view => view.DiagramLineWidth.Value)
                    .DisposeWith(d);

                #endregion

                #endregion
            });

            this.Events().IsVisibleChanged.Subscribe(d => { ViewModel.Save(); });
        }
    }
}