using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Kingfisher.Provider.Utils
{
    // got this from https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
    // also, WPF is only available on Windows, but I'm keeping the hope alive :-)
    public class ProcessHelper
    {
        public static void Start(string filename)
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                filename = filename.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {filename}") {CreateNoWindow = true});
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", filename);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", filename);
            }
        }
    }
}