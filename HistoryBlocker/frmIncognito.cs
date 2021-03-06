﻿using System;
using System.IO;
using System.Windows.Forms;

namespace HistoryBlocker
{
    public partial class frmIncognito : Form
    {
        private applicationsList blockerList = null;
        public frmIncognito()
        {
            InitializeComponent();
            blockerList = new applicationsList();
            /*listApp.DataSource = blockerList;
            listApp.DisplayMember = "Status";
            listApp.ValueMember = "AppName";
            listApp.Refresh();
            listApp.Update();*/
            
        }

        public frmIncognito(applicationsList list)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Chrome_Incognito;
            blockerList = list;
            applicationsListBindingSource.DataSource = blockerList;
            /*listApp.DataSource = applicationsListBindingSource;
            listApp.DisplayMember = "Status";*/
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            for(int i = 0;i<dataGridAppList.SelectedRows.Count;i++)
            {
                try
                {
                    fileBlocker theBlocker = (fileBlocker)dataGridAppList.SelectedRows[i].DataBoundItem;
                    if (!theBlocker.IsLocked())
                    {
                        theBlocker.Lock();
                        this.applicationsListBindingSource.ResetItem(this.applicationsListBindingSource.IndexOf(theBlocker));
                    }
                }
                catch (System.IO.IOException exception)
                {
                    MessageBox.Show(exception.Message.ToString());
                }
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridAppList.SelectedRows.Count; i++)
            {
                fileBlocker theBlocker = (fileBlocker)dataGridAppList.SelectedRows[i].DataBoundItem;
                if (theBlocker.IsLocked())
                {
                    theBlocker.Unlock();
                    this.applicationsListBindingSource.ResetItem(this.applicationsListBindingSource.IndexOf(theBlocker));
                }
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string jumpListPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileBlocker.AutomaticDestinationPath);
            System.Diagnostics.Process.Start("explorer.exe", jumpListPath);
        }
    }
}
