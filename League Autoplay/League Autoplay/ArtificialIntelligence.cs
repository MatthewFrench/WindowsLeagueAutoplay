using League_Auto_Key_Presser;
using League_Autoplay.High_Performance_Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay
{
    public class ArtificialIntelligence
    {
        UserInterface userInterface;
        VisualCortex visualCortex;
        MotorCortex motorCortex;
        bool grabbingScreen = false;
        Stopwatch timerPerformanceStopwatch, screenCapturePerformanceStopWatch;
        int timerPerformanceCount = 0;
        double timerPerformanceLength = 0;
        ATimer logicTimer = null;
        bool updateDisplayImage = false;

        public ArtificialIntelligence(UserInterface userInterface, VisualCortex visualCortex, MotorCortex motorCortex)
        {
            this.userInterface = userInterface;
            this.visualCortex = visualCortex;
            this.motorCortex = motorCortex;
            TimerResolution.setTimerResolution(1.0);
        
            timerPerformanceStopwatch = new Stopwatch();
            timerPerformanceStopwatch.Start();
            screenCapturePerformanceStopWatch = new Stopwatch();
        }
        private void logic()
        {
            TaskScheduler aiContext = TaskScheduler.Current;

            //Measure timer performance
            timerPerformanceStopwatch.Stop();
            double aiMilliseconds = timerPerformanceStopwatch.DurationInMilliseconds();
            double aiFps = (1000.0 / aiMilliseconds);
            timerPerformanceCount++;
            timerPerformanceLength += aiMilliseconds;
            if (timerPerformanceLength >= 1000)
            {
                double averageMilliseconds = timerPerformanceLength / timerPerformanceCount;
                double averageFps = 1000.0 / averageMilliseconds;
                userInterface.setAIPerformanceLabel("" + Math.Round(averageFps, 4) + " fps (" + Math.Round(averageMilliseconds, 4) + " ms)");
                timerPerformanceCount = 0;
                timerPerformanceLength = 0;
            }
            timerPerformanceStopwatch.Start();

            //Only grab the screen when it's done processing. 
            //Do on separate task so we don't freeze the AI.
            if (grabbingScreen == false)
            {
                screenCapturePerformanceStopWatch.Start();
                grabbingScreen = true;
                Task t = Task.Run(() =>
                {
                    //visualCortex.runTest();
                    visualCortex.grabScreenAndDetect();
                    updateDetectionData();
                }).ContinueWith(_ => {
                    //Run on UI thread
                    if (updateDisplayImage)
                    {
                        userInterface.setDisplayImage(visualCortex.getDisplayImage());
                    }
                    grabbingScreen = false;
                    screenCapturePerformanceStopWatch.Stop();
                    double screenMilliseconds = screenCapturePerformanceStopWatch.DurationInMilliseconds();
                    double screenFps = (1000.0 / screenMilliseconds);
                    userInterface.setScreenPerformanceLabel("" + Math.Round(screenFps, 4) + " fps (" + Math.Round(screenMilliseconds, 4) + " ms)");
                }, aiContext);
            }
        }
        void updateDetectionData()
        {
            //Pull the detection data from the C++

        }
        public void createAITimer(int milliseconds = 16)
        {
            if (logicTimer != null)
            {
                logicTimer.Stop();
            }
            // 0 = 40fps when set to 1ms, 8fps screen, 15fps is 7.5fps
            // 1 = 65fps when set to 1ms, 8fps screen, 40fps is 8fps
            // 2 = 1000fps when set to 1ms, 6 fps screen, 60fps is 6.35 fps
            // 3 = 1000fps when set to 1ms, 7.5 fps screen, 60fps is 7.65 fps, 200fps is 8fps
            logicTimer = new ATimer(3, milliseconds, new ATimer.ElapsedTimerDelegate(() => { logic(); }));
            logicTimer.Start();
        }
        public void setUpdateDisplayImage(bool b)
        {
            updateDisplayImage = b;
            visualCortex.setShouldCaptureDisplayImage(b);
        }
    }
}
