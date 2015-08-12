using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Controls;

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
    }
}
