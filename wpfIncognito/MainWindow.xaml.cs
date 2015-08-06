using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.IO;
using System.Windows.Shapes;
using System.Threading;

namespace wpfIncognito
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Collection<fileBlocker> fileBlockerList;
        private bool allIncognito;
        private FolderWatcher folderWatcher;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Initial Window Loading and populating
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            fileBlockerList = AppListBuilder.GetAppList();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {  
            dataGrid.ItemsSource = fileBlockerList;
            folderWatcher = new FolderWatcher(fileBlockerList);
        }
        #endregion

        private void SwitchIncognito()
        {
            if (allIncognito)
            {
                foreach (fileBlocker file in dataGrid.Items)
                {
                    if (file.IsLocked())
                    {
                        file.Unlock();
                    }
                }
            }
            else
            {
                foreach (fileBlocker file in dataGrid.Items)
                {
                    try
                    {
                        if (!file.IsLocked())
                        {
                            file.Lock();
                        }
                    }
                    catch (System.IO.IOException exception)
                    {
                        MessageBox.Show(exception.Message.ToString());
                    }
                }
            }
            allIncognito = !allIncognito;

            if(allIncognito)
            {
                btnIncognito.Content = "Un_incognito";
            }
            else
            {
                btnIncognito.Content = "_Incognito";
            }
        }

        #region Buttons action
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            string jumpListPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileBlocker.AutomaticDestinationPath);
            System.Diagnostics.Process.Start("explorer.exe", jumpListPath);
        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            foreach (fileBlocker file in dataGrid.SelectedItems)
            {
                try
                {
                    if (!file.IsLocked())
                    {
                        file.Lock();
                    }
                }
                catch (System.IO.IOException exception)
                {
                    MessageBox.Show(exception.Message.ToString());
                }
            }
        }

        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            foreach (fileBlocker file in dataGrid.SelectedItems)
            {
                if (file.IsLocked())
                {
                    file.Unlock();
                }
            }
        }

        private void btnIncognito_Click(object sender, RoutedEventArgs e)
        {
            SwitchIncognito();
        }

        #endregion
    }
}
