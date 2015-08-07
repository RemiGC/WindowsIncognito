using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace wpfIncognito
{
    class AppListBuilder
    {
        public static Collection<fileBlocker> GetAppList()
        {
            Collection<fileBlocker> appList;

            //Deserialize own xml
            if (File.Exists("applist.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Collection<fileBlocker>));
                TextReader reader = new StreamReader("applist.xml");
                object obj = deserializer.Deserialize(reader);
                appList = (Collection<fileBlocker>)obj;
                reader.Close();


                //Verify if files still exists and requery file info
                foreach (fileBlocker file in appList)
                {
                    if (File.Exists(file.FullPath))
                    {
                        file.ReCheckFileInfo();
                    }
                    else
                    {
                        appList.Remove(file);
                    }
                }
            }
            else
            {
                appList = new Collection<fileBlocker>();
            }

            string pathJumpList = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent\AutomaticDestinations");
            XDocument xdoc = XDocument.Load("AppIDList.xml");
            DirectoryInfo diTop = new DirectoryInfo(pathJumpList);

            foreach (FileInfo fileJump in diTop.EnumerateFiles("*.automaticDestinations-ms"))
            {
                bool fileAlreadyPresent = appList.Any(f => f.FileName.Equals(fileJump.Name,StringComparison.CurrentCultureIgnoreCase));
                if (!fileAlreadyPresent)
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
            }

            return appList;
        }
    }
}
