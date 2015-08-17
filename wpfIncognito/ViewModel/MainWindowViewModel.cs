using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using System.Windows.Controls;
using wpfIncognito.DataAccess;
using wpfIncognito.Model;


namespace wpfIncognito.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        SoftwareRepository _softwareRepository;
        IncognitoSettings _incognitoSettings;
        FolderWatcher _folderWatcher;

        public IncognitoViewModel Incognito { get; private set; }
        public SoftwareListViewModel SoftwareList { get; private set; }
        public SettingsViewModel Settings { get; private set; }

        public RelayCommand ShowWindowCommand { get; private set; }
        public RelayCommand HideWindowCommand { get; private set; }
        public RelayCommand CloseWindowCommand { get; private set; }

        public MainWindowViewModel(IncognitoSettings settings, SoftwareRepository softwareRepository)
        {
            _softwareRepository = softwareRepository;
            _incognitoSettings = settings;
            this._folderWatcher = new FolderWatcher(_softwareRepository);
            this.SoftwareList = new SoftwareListViewModel(_softwareRepository, _incognitoSettings);
            this.Incognito = new IncognitoViewModel(_softwareRepository, _incognitoSettings);
            this.Settings = new SettingsViewModel(_incognitoSettings);
            CloseWindowCommand = new RelayCommand(CloseWindowExecute);
            ShowWindowCommand = new RelayCommand(ShowWindowExecute, () => ShowWindowCanExecute());
            HideWindowCommand = new RelayCommand(HideWindowExecute, () => HideWindowCanExecute());
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }

        void CloseWindowExecute()
        {
            Application.Current.MainWindow.Close();
        }


        void ShowWindowExecute()
        {
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        bool ShowWindowCanExecute()
        {
            return !Application.Current.MainWindow.IsVisible;
        }

        void HideWindowExecute()
        {
            Application.Current.MainWindow.Hide();
        }

        bool HideWindowCanExecute()
        {
            return Application.Current.MainWindow.IsVisible;
        }
    }
}
