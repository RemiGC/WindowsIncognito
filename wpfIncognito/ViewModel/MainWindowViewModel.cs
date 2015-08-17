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
        private TaskbarIcon tb;

        public IncognitoViewModel Incognito { get; private set; }
        public SoftwareListViewModel SoftwareList { get; private set; }
        public SettingsViewModel Settings { get; private set; }

        public RelayCommand ShowWindowCommand { get; private set; }
        public RelayCommand HideWindowCommand { get; private set; }
        public RelayCommand CloseWindowCommand { get; private set; }

        public MainWindowViewModel(IncognitoSettings settings, SoftwareRepository softwareRepository, TaskbarIcon icon)
        {
            _softwareRepository = softwareRepository;
            _incognitoSettings = settings;
            tb = icon;
            this._folderWatcher = new FolderWatcher(_softwareRepository);
            this.SoftwareList = new SoftwareListViewModel(_softwareRepository, _incognitoSettings);
            this.Incognito = new IncognitoViewModel(_softwareRepository, _incognitoSettings);
            this.Settings = new SettingsViewModel(_incognitoSettings);
            CloseWindowCommand = new RelayCommand(CloseWindowExecute);
            ShowWindowCommand = new RelayCommand(ShowWindowExecute, () => ShowWindowCanExecute());
            HideWindowCommand = new RelayCommand(HideWindowExecute, () => HideWindowCanExecute());
            tb.DoubleClickCommand = ShowWindowCommand;
            TaskIconContextMenuSetup();
        }

        private void TaskIconContextMenuSetup()
        {
            //add Incognito On
            MenuItem miON = new MenuItem();
            miON.Header = "Incognito ON";
            miON.Command = Incognito.IncognitoOnCommand;
            tb.ContextMenu.Items.Add(miON);

            //Add Incognito Off
            MenuItem miOFF = new MenuItem();
            miOFF.Header = "Incognito OFF";
            miOFF.Command = Incognito.IncognitoOffCommand;
            tb.ContextMenu.Items.Add(miOFF);

            //add separator
            tb.ContextMenu.Items.Add(new Separator());

            //Add show
            MenuItem miShow = new MenuItem();
            miShow.Header = "Show";
            miShow.Command = ShowWindowCommand;
            tb.ContextMenu.Items.Add(miShow);

            //add hide
            MenuItem miHide = new MenuItem();
            miHide.Header = "Hide";
            miHide.Command = HideWindowCommand;
            tb.ContextMenu.Items.Add(miHide);

            //add separator
            tb.ContextMenu.Items.Add(new Separator());

            //add close
            MenuItem miClose = new MenuItem();
            miClose.Header = "Close";
            miClose.Command = CloseWindowCommand;
            tb.ContextMenu.Items.Add(miClose);
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
