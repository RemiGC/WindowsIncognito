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
        ObservableCollection<ViewModelBase> _viewModels;

        public MainWindowViewModel()
        {
            _softwareRepository = new SoftwareRepository();

            SoftwareListViewModel viewModel = new SoftwareListViewModel(_softwareRepository);
            this.ViewModels.Add(viewModel);
        }

        public ObservableCollection<ViewModelBase> ViewModels
        {
            get
            {
                if (_viewModels == null) _viewModels = new ObservableCollection<ViewModelBase>();

                return _viewModels;
            }
        }
    }
}
