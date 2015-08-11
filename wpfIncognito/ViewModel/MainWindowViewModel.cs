using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfIncognito.DataAccess;

namespace wpfIncognito.ViewModel
{
    public class MainWindowViewModel
    {
        SoftwareRepository _softwareRepository;

        public IncognitoViewModel Incognito { get; private set; }
        public SoftwareListViewModel SoftwareList { get; private set; }

        public MainWindowViewModel()
        {
            _softwareRepository = new SoftwareRepository();
            this.SoftwareList = new SoftwareListViewModel(_softwareRepository);
            this.Incognito = new IncognitoViewModel(_softwareRepository);
        }
    }
}
