using System;
using System.Collections.Generic;
using System.Text;

namespace ImgGetCoordinates.Libs.OS
{
    public static class Clipboard
    {
        public static void Copy(string val)
        {
            try
            {
                if (OperatingSystem.IsWindows())
                {
                    $"echo {val} | clip".Bat();
                }

                if (OperatingSystem.IsMacOS())
                {
                    $"echo \"{val}\" | pbcopy".Bash();
                }

                if (OperatingSystem.IsLinux())
                {
                    $"echo -n \"{val}\" | xclip -selection clipboard".BashNoResult();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}