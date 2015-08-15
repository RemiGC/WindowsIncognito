﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsProviderNet;
using System.ComponentModel;

namespace wpfIncognito.Model
{
    public class IncognitoSettings
    {
        [DefaultValue(false)]
        [DisplayName("Incognito ON on startup")]
        [Description("Set Incognito mode ON when starting the application")]
        public bool LockOnStartup { get; set; }

        [DefaultValue(false)]
        [DisplayName("Minimize Window on startup")]
        public bool MinimizeOnStartup { get; set; }

        [DefaultValue(false)]
        [DisplayName("Minimize to system tray")]
        public bool MinimizeToSystemTray { get; set; }
    }
}
