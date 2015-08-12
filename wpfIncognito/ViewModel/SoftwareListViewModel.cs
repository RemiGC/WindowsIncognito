﻿using System;
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
        IncognitoSettings _settings;

        RelayCommand _LockSoftware;
        RelayCommand _UnLockSoftware;

        public ObservableCollection<fileBlocker> AllSoftwares
        {
            get;
            set;
        }

        public SoftwareListViewModel(SoftwareRepository softwareRepository, IncognitoSettings settings)
        {
            if(softwareRepository == null)
            {
                throw new ArgumentNullException("softwareRepository");
            }
            this._softwareRepository = softwareRepository;
            this.AllSoftwares = new ObservableCollection<fileBlocker>(softwareRepository.GetSoftwares());
        }

        #region Lock/Unlock one software
        public ICommand LockCommand
        {
            get
            {
                if (_LockSoftware == null)
                {
                    _LockSoftware = new RelayCommand(p => this.LockExecute(), p => this.LockCanExecute());

                }
                return _LockSoftware;
            }
        }

        void LockExecute()
        {
            //fb.Lock();
            throw new NotImplementedException();
        }

        bool LockCanExecute()
        {
            //return !fb.IsLocked;
            throw new NotImplementedException();
        }

        #endregion
    }
}
