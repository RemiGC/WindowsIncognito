using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfIncognito.Model;
using SettingsProviderNet;
using GalaSoft.MvvmLight.CommandWpf;

namespace wpfIncognito.ViewModel
{
    public class SettingsViewModel: ViewModelBase
    {
        IncognitoSettings _incognitoSettings;

        public bool IncognitoModeOnStartup
        {
            get
            {
                return _incognitoSettings.LockOnStartup;
            }
            set
            {
                _incognitoSettings.LockOnStartup = value;
                RaisePropertyChanged("IncognitoModeOnStartup");
            }
        }

        public bool MinimizeOnStartup
        {
            get
            {
                return _incognitoSettings.MinimizeOnStartup;
            }
            set
            {
                _incognitoSettings.MinimizeOnStartup = value;
                RaisePropertyChanged("MinimizeOnStartup");
            }
        }

        public bool MinimizeToSystemTray
        {
            get
            {
                return _incognitoSettings.MinimizeToSystemTray;
            }
            set
            {
                _incognitoSettings.MinimizeToSystemTray = value;
                RaisePropertyChanged("MinimizeToSystemTray");
            }
        }

        public SettingsViewModel(IncognitoSettings settings)
        {
            _incognitoSettings = settings;
        }
    }
}
