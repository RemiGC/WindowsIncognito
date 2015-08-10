using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpfIncognito.DataAccess;
using wpfIncognito.Model;

namespace wpfIncognito.ViewModel
{
    public class SoftwareListViewModel:ViewModelBase
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

        public SoftwareListViewModel(SoftwareRepository softwareRepository)
        {
            isIncognito = false;
            if(softwareRepository == null)
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

        #region All Incognito on/off Command
        public ICommand IncognitoOnCommand
        {
            get
            {
                if(_IncognitoOnCommand == null)
                {
                    _IncognitoOnCommand = new RelayCommand(p => this.IncognitoOnExecute(), p => this.IncognitoOnCanExecute);

                }
                return _IncognitoOnCommand;
            }
        }

        void IncognitoOnExecute()
        {
            // TODO ADD exception
            foreach(fileBlocker fb in AllSoftwares)
            {
                if(!fb.IsLocked)
                {
                    fb.Lock();
                }
            }
            isIncognito = true;
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
            isIncognito = true;
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
