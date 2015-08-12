using SettingsProviderNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpfIncognito.Model;
using wpfIncognito.ViewModel;

namespace wpfIncognito
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IncognitoSettings appSettings;
        private SettingsProvider settingsProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            settingsProvider = new SettingsProvider(new LocalAppDataStorage("WindowsIncognito"));
            appSettings = settingsProvider.GetSettings<IncognitoSettings>();

            MainWindow mw = new MainWindow();
            var viewModel = new MainWindowViewModel(appSettings);
            mw.DataContext = viewModel;
            mw.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            settingsProvider.SaveSettings(appSettings);
        }
    }
}
