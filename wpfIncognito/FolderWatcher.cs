using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfIncognito
{
    public class FolderWatcher
    {
        private Collection<fileBlocker> fileBlockerList;
        private FileSystemWatcher watcher;

        public FolderWatcher(Collection<fileBlocker> List)
        {
            fileBlockerList = List;
            watcher = new FileSystemWatcher(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent\AutomaticDestinations"), "*.automaticDestinations-ms");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Created;

            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string name = Path.GetFileNameWithoutExtension(e.Name);

            var currentFile = fileBlockerList.Single(i => i.msName == name);
            currentFile.ReCheckFileInfo();
        }
    }
}
