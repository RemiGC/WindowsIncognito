using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using wpfIncognito.DataAccess;
using wpfIncognito.Model;

namespace wpfIncognito.ViewModel
{
    public class IncognitoViewModel:ViewModelBase
    {
        SoftwareRepository _softwareRepository;
        IncognitoSettings _incognitoSettings;
        bool isIncognito;
        public RelayCommand IncognitoOnCommand { get; private set; }
        public RelayCommand IncognitoOffCommand { get; private set; }

        public ObservableCollection<fileBlocker> AllSoftwares
        {
            get;
            set;
        }

        public IncognitoViewModel(SoftwareRepository softwareRepository, IncognitoSettings settings)
        {
            if (softwareRepository == null)
            {
                throw new ArgumentNullException("softwareRepository");
            }
            this._softwareRepository = softwareRepository;
            this.AllSoftwares = new ObservableCollection<fileBlocker>(softwareRepository.GetSoftwares());
            this._incognitoSettings = settings;

            if(_incognitoSettings.LockOnStartup)
            {
                LockAll();
            } else
            {
                UnlockAll();
            }

            isIncognito = _incognitoSettings.LockOnStartup;

            IncognitoOnCommand = new RelayCommand(IncognitoOnExecute, () => IncognitoOnCanExecute);
            IncognitoOffCommand = new RelayCommand(IncognitoOffExecute, () => IncognitoOffCanExecute);
        }

        public bool IsIncognitoModeOn
        {
            get
            {
                return isIncognito;
            }
        }

        private bool LockAll()
        {
            bool allLocked = true;
            foreach (fileBlocker fb in AllSoftwares)
            {
                if (!fb.Lock())
                {
                    allLocked = false;
                }
            }
            return allLocked;
        }

        private void UnlockAll()
        {
            foreach (fileBlocker fb in AllSoftwares)
            {
                if (fb.IsLocked)
                {
                    fb.Unlock();
                }
            }
        }

        #region All Incognito on/off Command

        void IncognitoOnExecute()
        {
            if(!LockAll())
            {
                MessageBox.Show("Impossible to activate incognito mode for all software\nCheck the status column in all software for more details");
            }
            
            isIncognito = true;
            RaisePropertyChanged("IsIncognitoModeOn");
        }

        bool IncognitoOnCanExecute
        {
            get
            {
                return AllSoftwares.Any(fb => fb.IsLocked == false);
            }
        }

        void IncognitoOffExecute()
        {
            UnlockAll();
            isIncognito = false;
            RaisePropertyChanged("IsIncognitoModeOn");
        }

        bool IncognitoOffCanExecute
        {
            get
            {
                return AllSoftwares.Any(fb => fb.IsLocked == true);
            }
        }
        #endregion
    }
}
