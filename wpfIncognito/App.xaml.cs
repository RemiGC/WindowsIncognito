using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpfIncognito.ViewModel;

namespace wpfIncognito
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mw = new MainWindow();
            var viewModel = new MainWindowViewModel();
            mw.DataContext = viewModel;
            mw.Show();
        }
    }
}
