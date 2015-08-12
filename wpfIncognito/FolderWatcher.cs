using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfIncognito.DataAccess;
using wpfIncognito.Model;

namespace wpfIncognito
{
    public class FolderWatcher
    {
        SoftwareRepository _softwareRepository;
        private FileSystemWatcher watcher;

        public FolderWatcher(SoftwareRepository List)
        {
            _softwareRepository = List;
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
            var currentFile = _softwareRepository.GetSoftwares().Single(i => i.FileName.Equals(e.Name,StringComparison.InvariantCultureIgnoreCase));
            currentFile.ReCheckFileInfo();
        }
    }
}
