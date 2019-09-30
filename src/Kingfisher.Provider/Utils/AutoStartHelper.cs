using System;
using System.Reflection;
using Microsoft.Win32;

namespace Kingfisher.Provider.Utils
{
    public static class AutoStartHelper
    {
        private const string RegistryKey_Run = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string AutoStartValueName = "Kingfisher";

        public static bool IsStartupItem()
        {
            using (var key = GetReadable())
                return IsStartupItem(key);
        }

        public static bool IsStartupItem(RegistryKey key)
        {
            return key?.GetValue(AutoStartValueName) != null;
        }

        public static void SetAutoStart(bool autostart, bool hidden)
        {
            using (var key = GetWriteable())
            {
                if (key == null)
                    return;

                string hiddenParam = hidden ? " -hidden" : string.Empty;

                var oldPath = (string)key.GetValue(AutoStartValueName);
                var newPath = Assembly.GetEntryAssembly().Location;
                if (newPath.EndsWith(".dll"))
                {
                    newPath = newPath.Substring(0, newPath.Length - 4) + ".exe";
                }

                newPath += hiddenParam;

                var isCurrentlyAutostarting = IsStartupItem(key);

                bool nochange = autostart == isCurrentlyAutostarting;
                if (nochange && !autostart)
                    return;

                if (autostart)
                    if (oldPath == newPath)
                        return;
                    else  // autostart is enabled but the .exe file was moved to another location -> update path.
                        key.SetValue(AutoStartValueName, newPath);
                else
                    key.DeleteValue(AutoStartValueName, false);
            }
        }

        private static RegistryKey GetReadable()
        {
            return GetKey(false);
        }

        private static RegistryKey GetWriteable()
        {
            return GetKey(true);
        }

        private static RegistryKey GetKey(bool writeable)
        {
            return Registry.CurrentUser.OpenSubKey(RegistryKey_Run, writeable);
        }
    }
}