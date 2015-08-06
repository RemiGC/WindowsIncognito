using System;
using System.IO;
using System.ComponentModel;

namespace wpfIncognito
{
    public class fileBlocker : INotifyPropertyChanged//, IComparable
    {
        struct fileBlockerData
        {
            internal string applicationName;
            internal string msFile;
            internal string fullPath;
            internal bool fileLocked;
            internal FileInfo fileInfo;
        }

        public static string AutomaticDestinationPath = @"Microsoft\Windows\Recent\AutomaticDestinations";
        static string Extension = ".automaticDestinations-ms";

        FileStream fileStream;

        private fileBlockerData fileData;

        public event PropertyChangedEventHandler PropertyChanged;

        //private applicationsList parent;

        public fileBlocker(string strAppName,string strAppMsFile)
        {
            fileData.fileLocked = false;
            fileData.msFile = strAppMsFile;
            fileData.applicationName = strAppName;
            fileData.fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , AutomaticDestinationPath,  fileData.msFile + Extension);
            fileData.fileInfo = new FileInfo(fileData.fullPath);
        }

        public override string ToString()
        {
            return AppName;
        }

        public void ReCheckFileInfo()
        {
            FileInfo newFileInfo = new FileInfo(fileData.fullPath);
            if(newFileInfo.LastWriteTime != fileData.fileInfo.LastWriteTime)
            {
                fileData.fileInfo = newFileInfo;
                NotifyPropertyChanged("LastModified");
            }
        }

        public override bool Equals(object other)
        {
            bool result = false;
            if (other is fileBlocker)
            {
                fileBlocker that = (fileBlocker)other;
                result = InternalEquals(that);
            }

            return result;
        }

        /// <summary>
        /// return the msFile hexa to int
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ((string)AppName).GetHashCode();
        }

        public bool Lock()
        {
            if (!fileData.fileLocked)
            {
                fileStream = new FileStream(fileData.fullPath, FileMode.Open, FileAccess.Write);
                if (fileStream != null)
                {
                    fileData.fileLocked = true;
                    NotifyPropertyChanged("Status");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string AppName
        {
            get
            {
                return fileData.applicationName;
            }
        }

        public string Status
        {
            get
            {
                if (IsLocked())
                {
                    return "Locked";
                }
                else
                {
                    return "Unlocked";
                }
            }
        }

        public DateTime LastModified
        {
            get
            {
                return fileData.fileInfo.LastWriteTime;
            }
        }

        public string msName
        {
            get
            {
                return fileData.msFile;
            }
        }


        public void Unlock()
        {
            if (fileData.fileLocked)
            {
                fileStream.Close();
                fileStream = null;
                fileData.fileLocked = false;
                NotifyPropertyChanged("Status");
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        /// <summary>
        /// internal equals
        /// </summary>
        /// <param name="that">the other fileBlocker to test</param>
        /// <returns></returns>
        private bool InternalEquals(fileBlocker that)
        {
            return (this.msName == that.msName);
        }

        public bool IsLocked()
        {
            return fileData.fileLocked;
        }

        /*public int CompareTo(object obj)
        {
            fileBlocker fb = (fileBlocker)obj;
            return string.Compare(this.msName, fb.msName);
        }*/

        //#region IEditableObject

        /*internal applicationsList Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }/*

       public void BeginEdit()
        {
            if (!inTxn)
            {
                this.backupData = fileData;
                inTxn = true;
            }
        }

       public void CancelEdit()
        {
            if (inTxn)
            {
                this.fileData = backupData;
                inTxn = false;
            }
        }

       public void EndEdit()
        {
            if (inTxn)
            {
                backupData = new fileBlockerData();
                inTxn = false;
            }
        }

        private void OnFileBlockerDataChanged()
        {
            if (!inTxn && Parent != null)
            {
                Parent.fileBlockerChanged(this);
            }
        }
		
        #endregion*/
    }
}
