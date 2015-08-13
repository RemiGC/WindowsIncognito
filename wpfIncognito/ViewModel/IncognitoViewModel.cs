using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
                IncognitoOnExecute();
            } else
            {
                IncognitoOffExecute();
            }

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

        #region All Incognito on/off Command
        /*public ICommand IncognitoOnCommand
        {
            get
            {
                if (_IncognitoOnCommand == null)
                {
                    _IncognitoOnCommand = new RelayCommand(IncognitoOnExecute, () => IncognitoOnCanExecute);

                }
                return _IncognitoOnCommand;
            }
        }*/

        void IncognitoOnExecute()
        {
            bool allLocked = true;
            foreach (fileBlocker fb in AllSoftwares)
            {
                if(!fb.Lock())
                {
                    allLocked = false;
                }
            }

            if(!allLocked)
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

        /*public ICommand IncognitoOffCommand
        {
            get
            {
                if (_IncognitoOffCommand == null)
                {
                    _IncognitoOffCommand = new RelayCommand(IncognitoOffExecute, () => IncognitoOffCanExecute);

                }
                return _IncognitoOffCommand;
            }
        }*/

        void IncognitoOffExecute()
        {
            foreach (fileBlocker fb in AllSoftwares)
            {
                if (fb.IsLocked)
                {
                    fb.Unlock();
                }
            }
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
