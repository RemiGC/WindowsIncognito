using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using wpfIncognito.Model;

namespace wpfIncognito.DataAccess
{
    public class SoftwareRepository
    {
        readonly List<fileBlocker> appList;

        public SoftwareRepository()
        {
            //Deserialize own xml
            if (File.Exists("applist.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<fileBlocker>));
                TextReader reader = new StreamReader("applist.xml");
                object obj = deserializer.Deserialize(reader);
                appList = (List<fileBlocker>)obj;
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
                appList = new List<fileBlocker>();
            }

            string pathJumpList = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent\AutomaticDestinations");
            XDocument xdoc = XDocument.Load("AppIDList.xml");
            DirectoryInfo diTop = new DirectoryInfo(pathJumpList);

            foreach (FileInfo fileJump in diTop.EnumerateFiles("*.automaticDestinations-ms"))
            {
                bool fileAlreadyPresent = appList.Any(f => f.FileName.Equals(fileJump.Name, StringComparison.CurrentCultureIgnoreCase));
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
        }

        public List<fileBlocker> GetSoftwares()
        {
            return new List<fileBlocker>(appList);
        }
    }
}
