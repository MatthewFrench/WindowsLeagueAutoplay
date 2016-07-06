using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay.High_Performance_Timer
{
   public  class TimerResolution
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtSetTimerResolution(int DesiredResolution, bool SetResolution, out int CurrentResolution);

        static public void setTimerResolution(double desiredResolutionInMilliseconds)
        {
            Console.WriteLine("Setting timer resolution");
            // The requested resolution in 100 ns units:
            int DesiredResolution = (int)Math.Floor(1000 * desiredResolutionInMilliseconds);
            // Note: The supported resolutions can be obtained by a call to NtQueryTimerResolution()

            int CurrentResolution = 0;

            // 1. Requesting a higher resolution
            // Note: This call is similar to timeBeginPeriod.
            // However, it to to specify the resolution in 100 ns units.
            if (NtSetTimerResolution(DesiredResolution, true, out CurrentResolution) != 0)
            {
                Console.WriteLine("Setting resolution failed");
            }
            Console.WriteLine("CurrentResolution [milliseconds]: " + (CurrentResolution / 1000.0));
        }
    }
}
