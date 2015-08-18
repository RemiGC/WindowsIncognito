using Hardcodet.Wpf.TaskbarNotification;
using SettingsProviderNet;
using System.Windows;
using wpfIncognito.DataAccess;
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
        private SoftwareRepository softwareRepository;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            settingsProvider = new SettingsProvider(new LocalAppDataStorage("WindowsIncognito"));
            appSettings = settingsProvider.GetSettings<IncognitoSettings>();

            MainWindow mw = new MainWindow();
            softwareRepository = new SoftwareRepository();
            var viewModel = new MainWindowViewModel(appSettings, softwareRepository);
            mw.DataContext = viewModel;
            mw.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            settingsProvider.SaveSettings(appSettings);
            softwareRepository.Save();
            base.OnExit(e);
        }
    }
}
