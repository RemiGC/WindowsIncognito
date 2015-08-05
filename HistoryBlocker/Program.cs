using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HistoryBlocker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            applicationsList blockerList = new applicationsList();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*blockerList.Add(new fileBlocker("Explorer", "1b4dd67f29cb1962"));
            blockerList.Add(new fileBlocker("Media player classic", "d7a07b8500c35d28"));
            blockerList.Add(new fileBlocker("Notepad", "9b9cdc69c1c24e2b"));*/
            Application.Run(new frmIncognito(blockerList));
        }
    }
}
