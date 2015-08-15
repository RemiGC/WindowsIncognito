using Hardcodet.Wpf.TaskbarNotification;
using SettingsProviderNet;
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
        private TaskbarIcon tb;
        private IncognitoSettings appSettings;
        private SettingsProvider settingsProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            tb = (TaskbarIcon)FindResource("taskbarIcon");
            settingsProvider = new SettingsProvider(new LocalAppDataStorage("WindowsIncognito"));
            appSettings = settingsProvider.GetSettings<IncognitoSettings>();

            MainWindow mw = new MainWindow();
            var viewModel = new MainWindowViewModel(appSettings,tb);
            mw.DataContext = viewModel;
            mw.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            settingsProvider.SaveSettings(appSettings);
            base.OnExit(e);
        }
    }
}
