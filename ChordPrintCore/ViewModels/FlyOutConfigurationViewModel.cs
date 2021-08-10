using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using ChordPrintCore.Utils;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ChordPrintCore.ViewModels
{
    public class FlyOutConfigurationViewModel
    {
        private readonly IConfigurationService _configurationService;

        public FlyOutConfigurationViewModel(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            ConfigurationFile = _configurationService.LoadConfigurationFile();

            SettingsTitlePositions = new ObservableCollection<SettingsTitle>
            {
                SettingsTitle.Center,
                SettingsTitle.Left,
                SettingsTitle.Right
            };

            this.WhenAnyValue(vm => vm.ConfigurationFile)
                .Where(vm => vm != null)
                .DistinctUntilChanged()
                .Subscribe(vm =>
                {
                    SelectedSettingsTitle = (SettingsTitle) Enum.Parse(typeof(SettingsTitle),
                        ConfigurationFile.settings.titles, true);
                });
        }

        [Reactive] public ConfigurationFile ConfigurationFile { get; set; }

        [Reactive] public ObservableCollection<SettingsTitle> SettingsTitlePositions { get; set; }

        [Reactive] public SettingsTitle SelectedSettingsTitle { get; set; }

        public void Save()
        {
            _configurationService.SaveConfiguration(ConfigurationFile);
        }
    }
}