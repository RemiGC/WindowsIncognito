using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using wpfIncognito.DataAccess;
using wpfIncognito.Model;

namespace wpfIncognito.ViewModel
{
    public class IncognitoViewModel:ViewModelBase , INotifyPropertyChanged
    {
        SoftwareRepository _softwareRepository;
        bool isIncognito;
        RelayCommand _IncognitoOnCommand;
        RelayCommand _IncognitoOffCommand;

        public ObservableCollection<fileBlocker> AllSoftwares
        {
            get;
            set;
        }

        public IncognitoViewModel(SoftwareRepository softwareRepository)
        {
            isIncognito = false;
            if (softwareRepository == null)
            {
                throw new ArgumentNullException("softwareRepository");
            }
            this._softwareRepository = softwareRepository;
            this.AllSoftwares = new ObservableCollection<fileBlocker>(softwareRepository.GetSoftwares());
        }

        protected override void OnDispose()
        {
            this.AllSoftwares.Clear();
        }

        public bool IsIncognitoModeOn
        {
            get
            {
                return isIncognito;
            }
        }

        #region All Incognito on/off Command
        public ICommand IncognitoOnCommand
        {
            get
            {
                if (_IncognitoOnCommand == null)
                {
                    _IncognitoOnCommand = new RelayCommand(p => this.IncognitoOnExecute(), p => this.IncognitoOnCanExecute);

                }
                return _IncognitoOnCommand;
            }
        }

        void IncognitoOnExecute()
        {
            // TODO ADD exception
            foreach (fileBlocker fb in AllSoftwares)
            {
                if (!fb.IsLocked)
                {
                    fb.Lock();
                }
            }
            isIncognito = true;
            OnPropertyChanged("IsIncognitoModeOn");
        }

        bool IncognitoOnCanExecute
        {
            get
            {
                return AllSoftwares.Any(fb => fb.IsLocked == false);
            }
        }

        public ICommand IncognitoOffCommand
        {
            get
            {
                if (_IncognitoOffCommand == null)
                {
                    _IncognitoOffCommand = new RelayCommand(p => this.IncognitoOffExecute(), p => this.IncognitoOffCanExecute);

                }
                return _IncognitoOffCommand;
            }
        }

        void IncognitoOffExecute()
        {
            // TODO ADD exception
            foreach (fileBlocker fb in AllSoftwares)
            {
                if (fb.IsLocked)
                {
                    fb.Unlock();
                }
            }
            isIncognito = false;
            OnPropertyChanged("IsIncognitoModeOn");
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
