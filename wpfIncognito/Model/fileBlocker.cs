using System;
using System.IO;
using System.ComponentModel;
using System.Xml.Serialization;

namespace wpfIncognito.Model
{
    [Serializable()]
    public class fileBlocker : INotifyPropertyChanged
    {
        private string appName;
        public string AppName
        {
            get
            {
                return this.appName;
            }
            set
            {
                this.appName = value;
                NotifyPropertyChanged("AppName");
            }
        }

        private ulong appID;

        [XmlAttribute("AppID")]
        public ulong AppID
        {
            get
            {
                return this.appID;
            }
            set
            {
                this.appID = value;
            }
        }

        private bool fileLocked;
        private FileInfo fileInfo;         

        public static string AutomaticDestinationPath = @"Microsoft\Windows\Recent\AutomaticDestinations";
        static string Extension = ".automaticDestinations-ms";

        [field: NonSerialized()]
        private FileStream fileStream;

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        //private applicationsList parent;

        private fileBlocker()
        {
        }

        public fileBlocker(string strAppName,string strAppMsFile)
        {
            this.fileLocked = false;
            this.AppID = UInt64.Parse(strAppMsFile,System.Globalization.NumberStyles.HexNumber);
            this.AppName = strAppName;
            ReCheckFileInfo();
        }

        public override string ToString()
        {
            return AppID.ToString("X");
        }

        public string AppIDHexa
        {
            get
            {
                return AppID.ToString("X");
            }
        }

        public void ReCheckFileInfo()
        {
            if (fileInfo != null)
            {
                FileInfo newFileInfo = new FileInfo(FullPath);
                if (newFileInfo.LastWriteTime != fileInfo.LastWriteTime)
                {
                    FileInfo oldFileInfo = fileInfo;
                    fileInfo = newFileInfo;
                    NotifyPropertyChanged("LastModified");
                    oldFileInfo = null;
                }
            }
            else
            {
                fileInfo = new FileInfo(FullPath);
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

        public bool Lock()
        {
            if (!fileLocked)
            {
                fileStream = new FileStream(FullPath, FileMode.Open, FileAccess.Write);
                if (fileStream != null)
                {
                    fileLocked = true;
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

        public string Status
        {
            get
            {
                if (IsLocked)
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
                return fileInfo.LastWriteTime;
            }
        }

        public string FileName
        {
            get
            {
                return AppID.ToString("X") + Extension;
            }
        }

        public string FullPath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AutomaticDestinationPath, FileName);
            }
        }


        public void Unlock()
        {
            if (fileLocked)
            {
                fileStream.Close();
                fileStream = null;
                fileLocked = false;
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
            return (this.AppID == that.AppID);
        }

        public override int GetHashCode()
        {
            return AppID.GetHashCode();
        }

        public bool IsLocked
        {
            get
            {
                return fileLocked;
            }
        }
    }
}
