﻿using wpfIncognito.DataAccess;
using wpfIncognito.Model;

namespace wpfIncognito.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        SoftwareRepository _softwareRepository;
        IncognitoSettings _incognitoSettings;

        public IncognitoViewModel Incognito { get; private set; }
        public SoftwareListViewModel SoftwareList { get; private set; }

        public MainWindowViewModel(IncognitoSettings settings)
        {
            _softwareRepository = new SoftwareRepository();
            _incognitoSettings = settings;
            this.SoftwareList = new SoftwareListViewModel(_softwareRepository, _incognitoSettings);
            this.Incognito = new IncognitoViewModel(_softwareRepository, _incognitoSettings);
        }
    }
}
