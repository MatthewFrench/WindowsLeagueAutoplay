using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using DiagnosticStopwatch = System.Diagnostics.Stopwatch;

namespace League_Autoplay.High_Performance_Timer
{
    public class Stopwatch
    {
        
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);

        private long startTime, stopTime;
        private long freq;
        private double freqDouble;
        
        //DiagnosticStopwatch watch;
        //double ticksPerMillisecond;

        // Constructor
        public Stopwatch()
        {
            //watch = new DiagnosticStopwatch();
            //watch.Start();
            
            startTime = 0;
            stopTime = 0;

            if (QueryPerformanceFrequency(out freq) == false)
            {
                // high-performance counter not supported
                throw new Win32Exception();
            }
            QueryPerformanceCounter(out startTime);
            freqDouble = Convert.ToDouble(freq);
            //ticksPerMillisecond = Convert.ToDouble(TimeSpan.TicksPerMillisecond);
        }

        // Start the timer
        public void Reset()
        {
            // lets do the waiting threads there work
            //Thread.Sleep(0);

            startTime = 0;
            stopTime = 0;

            QueryPerformanceCounter(out startTime);
            //watch.Restart();
        }

        // Stop the timer
        //public void Stop()
        //{
        //    QueryPerformanceCounter(out stopTime);
        //}

        public double DurationInHours()
        {
            //return Convert.ToDouble(watch.ElapsedTicks) / ticksPerMillisecond / 1000.0 / 60.0 / 60.0;
            QueryPerformanceCounter(out stopTime);
            return (double)(stopTime - startTime) / 60.0 / 60.0 / freqDouble;
        }

        public double DurationInMinutes()
        {
            //Console.WriteLine("Returning: " + (Convert.ToDouble(watch.ElapsedTicks) / ticksPerMillisecond / 1000.0 / 60.0) + " vs " + watch.Elapsed.Minutes);
            //return Convert.ToDouble(watch.ElapsedTicks) / ticksPerMillisecond / 1000.0 / 60.0;
            QueryPerformanceCounter(out stopTime);
            return (double)(stopTime - startTime) / 60.0 / freqDouble;
        }

        // Returns the duration of the timer (in seconds)
        public double DurationInSeconds()
        {
            //return Convert.ToDouble(watch.ElapsedTicks) / ticksPerMillisecond / 1000.0;
            QueryPerformanceCounter(out stopTime);
            return (double)(stopTime - startTime) / freqDouble;
        }

        public double DurationInMilliseconds()
        {
            
            //return Convert.ToDouble(watch.ElapsedTicks) / ticksPerMillisecond;
            QueryPerformanceCounter(out stopTime);
            return (double)(stopTime - startTime) * 1000.0 / freqDouble;
        }
    }
}
