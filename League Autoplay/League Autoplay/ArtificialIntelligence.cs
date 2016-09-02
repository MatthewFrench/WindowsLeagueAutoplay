using League_Auto_Key_Presser;
using League_Autoplay.High_Performance_Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public byte[] ToByteArray(DetectionDataStruct data)
        {
            byte[] arr = null;
            IntPtr ptr = IntPtr.Zero;
            try
            {
                int size = Marshal.SizeOf(data);
                arr = new byte[size];
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(data, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            return arr;
        }

        unsafe void updateDetectionData()
        {
            Console.WriteLine("\n");
            Console.WriteLine("C# Detection data struct size: " + Marshal.SizeOf(new DetectionDataStruct()));
            Console.WriteLine("C# Tower data struct size: " + Marshal.SizeOf(new Tower()));
            Console.WriteLine("C# SelfHealth data struct size: " + Marshal.SizeOf(new SelfHealth()));
            Console.WriteLine("C# Position data struct size: " + Marshal.SizeOf(new Position()));
            Console.WriteLine("C# Minion data struct size: " + Marshal.SizeOf(new Minion()));
            Console.WriteLine("C# GenericObject data struct size: " + Marshal.SizeOf(new GenericObject()));
            Console.WriteLine("C# Champion data struct size: " + Marshal.SizeOf(new Champion()));
            Console.WriteLine("\n");

            //Pull the detection data from the C++
            Console.WriteLine("Test Starting Detection data test");
            DetectionDataStruct detectionData = visualCortex.getVisualDetectionData();
            
            Console.WriteLine("Reading detection data");

            Console.WriteLine("Detected in C#: ");
            if (detectionData.numberOfAllyMinions > 0)
            {
                Console.WriteLine("\t"+ detectionData.numberOfAllyMinions + " ally minions");
            }
            if (detectionData.numberOfAllyChampions > 0)
            {
                Console.WriteLine("\t"+ detectionData.numberOfAllyChampions + " ally champions");
            }
            if (detectionData.numberOfSelfChampions > 0)
            {
                Console.WriteLine("\t"+ detectionData.numberOfSelfChampions + " self champions");
            }
            if (detectionData.numberOfEnemyMinions > 0)
            {
                Console.WriteLine("\t"+ detectionData.numberOfEnemyMinions + " enemy minions");
            }
            if (detectionData.numberOfEnemyChampions > 0)
            {
                Console.WriteLine("\t"+ detectionData.numberOfEnemyChampions + " enemy champions");
            }
            if (detectionData.numberOfEnemyTowers > 0)
            {
                Console.WriteLine("\t"+ detectionData.numberOfEnemyTowers + " enemy towers");
            }
            if (detectionData.selfHealthBarVisible)
            {
                Console.WriteLine("\tCan see self health bar");
                Console.WriteLine("\tSelf health: " + ((SelfHealth*)detectionData.selfHealthBar.ToPointer())->health);
            }
            if (detectionData.spell1LevelUpAvailable)
            {
                Console.WriteLine("\tLevel up spell 1 available");
            }
            if (detectionData.spell2LevelUpAvailable)
            {
                Console.WriteLine("\tLevel up spell 2 available");
            }
            if (detectionData.spell3LevelUpAvailable)
            {
                Console.WriteLine("\tLevel up spell 3 available");
            }
            if (detectionData.spell4LevelUpAvailable)
            {
                Console.WriteLine("\tLevel up spell 4 available");
            }
            if (detectionData.currentLevel > 0)
            {
                Console.WriteLine("\tDetected current level: " + detectionData.currentLevel);
            }
            if (detectionData.spell1ActiveAvailable)
            {
                Console.WriteLine("\tSpell 1 available");
            }
            if (detectionData.spell2ActiveAvailable)
            {
                Console.WriteLine("\tSpell 2 available");
            }
            if (detectionData.spell3ActiveAvailable)
            {
                Console.WriteLine("\tSpell 3 available");
            }
            if (detectionData.spell4ActiveAvailable)
            {
                Console.WriteLine("\tSpell 4 available");
            }
            if (detectionData.summonerSpell1ActiveAvailable)
            {
                Console.WriteLine("\tSummoner spell 1 available");
            }
            if (detectionData.summonerSpell2ActiveAvailable)
            {
                Console.WriteLine("\tSummoner spell 2 available");
            }
            if (detectionData.trinketActiveAvailable)
            {
                Console.WriteLine("\tTrinket active available");
            }
            if (detectionData.item1ActiveAvailable)
            {
                Console.WriteLine("\tItem 1 active available");
            }
            if (detectionData.item2ActiveAvailable)
            {
                Console.WriteLine("\tItem 2 active available");
            }
            if (detectionData.item3ActiveAvailable)
            {
                Console.WriteLine("\tItem 3 active available");
            }
            if (detectionData.item4ActiveAvailable)
            {
                Console.WriteLine("\tItem 4 active available");
            }
            if (detectionData.item5ActiveAvailable)
            {
                Console.WriteLine("\tItem 5 active available");
            }
            if (detectionData.item6ActiveAvailable)
            {
                Console.WriteLine("\tItem 6 active available");
            }
            if (detectionData.potionActiveAvailable)
            {
                Console.WriteLine("\tPotion active available");

                Console.WriteLine("\t\tPotion in slot " + detectionData.potionOnActive);
            }
            if (detectionData.potionBeingUsedShown)
            {
                Console.WriteLine("\tPotion being used");
            }
            if (detectionData.shopAvailableShown)
            {
                Console.WriteLine("\tShop is available");
            }
            if (detectionData.shopTopLeftCornerShown)
            {
                Console.WriteLine("\tShop top left corner is visible");
            }
            if (detectionData.shopBottomLeftCornerShown)
            {
                Console.WriteLine("\tShop bottom left corner is visible");
            }
            if (detectionData.numberOfBuyableItems > 0)
            {
                Console.WriteLine("\tBuyable items: " + detectionData.numberOfBuyableItems);
            }
            if (detectionData.mapVisible)
            {
                Console.WriteLine("\tMap is visible");
            }
            if (detectionData.mapShopVisible)
            {
                Console.WriteLine("\tShop on map is visible");
            }
            if (detectionData.mapSelfLocationVisible)
            {
                Console.WriteLine("\tLocation on map is visible");
            }
            if (detectionData.surrenderAvailable)
            {
                Console.WriteLine("\tSurrender is visible");
            }

            Console.WriteLine("\nC# bytes");
            byte[] bytes = ToByteArray(detectionData);
            Console.WriteLine("Bytes count: " + Marshal.SizeOf(detectionData));
            Console.WriteLine("[ " + BitConverter.ToString(bytes).Replace("-", " ").ToLower() + " ]");
            Console.WriteLine("\n");

            visualCortex.freeVisualDetectionData(ref detectionData);
            Console.WriteLine("Test Ending Detection data test");
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
