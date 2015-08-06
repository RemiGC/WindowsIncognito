using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace wpfIncognito
{
    class AppListBuilder
    {
        public static Collection<fileBlocker> GetAppList()
        {
            string pathJumpList = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent\AutomaticDestinations");
            XDocument xdoc = XDocument.Load("AppIDList.xml");
            DirectoryInfo diTop = new DirectoryInfo(pathJumpList);
            Collection<fileBlocker> appList= new Collection<fileBlocker>();

            foreach (FileInfo fileJump in diTop.EnumerateFiles("*.automaticDestinations-ms"))
            {
                var query = from application in xdoc.Element("applications").Elements("application")
                            where application.Attribute("AppID").Value == Path.GetFileNameWithoutExtension(fileJump.Name)
                            select application;
                XElement oneApplication = query.SingleOrDefault();

                if (oneApplication != null)
                {
                    appList.Add(new fileBlocker(oneApplication.Element("description").Value.ToString(), oneApplication.Attribute("AppID").Value.ToString()));
                }
                else
                {
                    appList.Add(new fileBlocker(Path.GetFileNameWithoutExtension(fileJump.Name), Path.GetFileNameWithoutExtension(fileJump.Name)));
                }
            }

            return appList;
        }
    }
}
