using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Controls;
using wpfIncognito.ViewModel;
using System.Windows.Input;

namespace wpfIncognito
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AllSoftwareExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            this.Height = ((Expander)sender).ActualHeight - 20;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                MainWindowViewModel mv = (MainWindowViewModel)this.DataContext;
                if(mv.Settings.MinimizeToSystemTray)
                {
                    if(mv.HideWindowCommand.CanExecute(null))
                    { 
                        mv.HideWindowCommand.Execute(null);
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel mv = (MainWindowViewModel)this.DataContext;
            if (mv.Settings.MinimizeOnStartup)
            {
                this.WindowState = WindowState.Minimized;
            }
        }
    }
}
