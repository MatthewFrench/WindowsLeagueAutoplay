using League_Auto_Key_Presser;
using League_Autoplay.High_Performance_Timer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using League_Autoplay.AutoQueue;

using System.IO;
using Microsoft.Win32.SafeHandles;

namespace League_Autoplay
{
    public class ArtificialIntelligence
    {
        UserInterface userInterface;
        VisualCortex visualCortex;
        bool grabbingScreen = false;
        High_Performance_Timer.Stopwatch timerPerformanceStopwatch, screenCapturePerformanceStopWatch;
        int timerPerformanceCount = 0;
        double timerPerformanceLength = 0;
        ATimer logicTimer = null;
        bool updateDisplayImage = false;
        Bitmap currentScreen = null;

        bool hasDetectionData = false;
        DetectionDataStruct currentDetectionData;
        private Object detectionDataLock = new Object();
        bool leagueIsInGame = false;

        BasicAI basicAI;
        AutoQueueManager autoQueue;

        High_Performance_Timer.Stopwatch fpsLimitStopwatch = new High_Performance_Timer.Stopwatch();
        int fpsLimit = 16;



        public ArtificialIntelligence(UserInterface userInterface, VisualCortex visualCortex)
        {

            
            
            if (logicTimer != null)
            {
                logicTimer.Stop();
            }
            // 0 = 40fps when set to 1ms, 8fps screen, 15fps is 7.5fps
            // 1 = 65fps when set to 1ms, 8fps screen, 40fps is 8fps
            // 2 = 1000fps when set to 1ms, 6 fps screen, 60fps is 6.35 fps
            // 3 = 1000fps when set to 1ms, 7.5 fps screen, 60fps is 7.65 fps, 200fps is 8fps
            logicTimer = new ATimer(3, 1000/120, new ATimer.ElapsedTimerDelegate(() => { logic(); }));
           
            


            this.userInterface = userInterface;
            this.visualCortex = visualCortex;
        
            timerPerformanceStopwatch = new High_Performance_Timer.Stopwatch();
            screenCapturePerformanceStopWatch = new High_Performance_Timer.Stopwatch();

            basicAI = new BasicAI();
            autoQueue = new AutoQueueManager();

            logicTimer.Start();
        }
        private void logic()
        {
            if (fpsLimitStopwatch.DurationInMilliseconds() < fpsLimit) return;
            fpsLimitStopwatch.Reset();

            bool leagueOfLegendsOpen = false;

            Process[] pname = Process.GetProcessesByName("league of legends");
            if (pname.Length != 0) leagueOfLegendsOpen = true;

            bool leagueOfLegendsClientOpen = false;
            pname = Process.GetProcessesByName("LolClient");
            if (pname.Length != 0) leagueOfLegendsClientOpen = true;

            TaskScheduler aiContext = TaskScheduler.Current;

            //Measure timer performance
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
            timerPerformanceStopwatch.Reset();

            //Only grab the screen when it's done processing. 
            //Do on separate task so we don't freeze the AI.
            if (grabbingScreen == false)
            {
                screenCapturePerformanceStopWatch.Reset();
                grabbingScreen = true;
                Task t = Task.Run(() =>
                {
                    //visualCortex.runTest();
                    Bitmap screen = visualCortex.grabScreen2(leagueOfLegendsOpen);

                    DetectionDataStruct data = visualCortex.getVisualDetectionData();
                    TaskHelper.RunTask(aiContext, () =>
                    {
                        updateDetectionData(ref data, screen);
                    });
                }).ContinueWith(_ => {
                    //Run on UI thread
                    if (updateDisplayImage)
                    {
                        userInterface.setDisplayImage(visualCortex.getDisplayImage());
                    }
                    grabbingScreen = false;
                    double screenMilliseconds = screenCapturePerformanceStopWatch.DurationInMilliseconds();
                    double screenFps = (1000.0 / screenMilliseconds);
                    userInterface.setScreenPerformanceLabel("" + Math.Round(screenFps, 4) + " fps (" + Math.Round(screenMilliseconds, 4) + " ms)");
                }, aiContext);
            }

            if (leagueIsInGame == false && leagueOfLegendsOpen)
            {
                basicAI.resetAI(autoQueue.getPickedLane());
                autoQueue.enteredLeagueOfLegends();
            }
            if (leagueIsInGame == true && !leagueOfLegendsOpen)
            {
                autoQueue.exitedLeagueOfLegends();
            }
            leagueIsInGame = leagueOfLegendsOpen;

            //Run basic AI algorithm
            if (leagueOfLegendsOpen)
            {
                lock (detectionDataLock)
                {
                    basicAI.processAI();
                }
            }
            else if ((leagueOfLegendsClientOpen || VisualCortex.IsTest) && currentScreen != null)
            {
                //Console.WriteLine("League of legends client open and scanning for auto queue");
                //Run auto queue
                autoQueue.runAutoQueue(currentScreen);
               // userInterface.setDebugImage(new Bitmap(AutoQueueDetection.matchingBitmap));
            }
            if (currentScreen != null) autoQueue.runErrorCheck(currentScreen);
            autoQueue.runSleepLogic();
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

        unsafe void updateDetectionData(ref DetectionDataStruct data, Bitmap screen)
        {
            lock(detectionDataLock)
            {
                if (hasDetectionData)
                {
                    visualCortex.freeVisualDetectionData(ref currentDetectionData);
                    hasDetectionData = false;
                }
                currentDetectionData = data;
                hasDetectionData = true;
                currentScreen = screen;

                /*
                if (currentDetectionData.selfHealthBarVisible == false)
                {
                    //Detect auto queue data
                    autoQueueData = AutoQueueDetection.detectAutoQueue();
                }*/

                basicAI.updateDetectionData(ref currentDetectionData);
            }

            bool leagueOfLegendsOpen = false;

            Process[] pname = Process.GetProcessesByName("league of legends");
            if (pname.Length != 0) leagueOfLegendsOpen = true;

            if (leagueOfLegendsOpen) Console.WriteLine("Detected in C#: ");


            if (data.numberOfAllyMinions > 0)
            {
                Console.WriteLine("\t"+ data.numberOfAllyMinions + " ally minions");

                for (int i = 0; i < data.numberOfAllyMinions; i++)
                {
                    Minion* minion = &(((Minion*)data.allyMinionsArray.ToPointer())[i]);
                    Console.WriteLine("\t\tminion at "+ minion->characterCenter.x + ", "+ minion->characterCenter.y + " with health " + minion->health);
                }
            }
            if (data.numberOfAllyChampions > 0)
            {
                Console.WriteLine("\t"+ data.numberOfAllyChampions + " ally champions");
                for (int i = 0; i < data.numberOfAllyChampions; i++)
                {
                    Champion* champion = &(((Champion*)data.allyChampionsArray.ToPointer())[i]);
                    Console.WriteLine("\t\tchampion at "+ champion->characterCenter.x + ", "+ champion->characterCenter.y + " with health " + champion->health);
                }
            }
            if (data.numberOfSelfChampions > 0)
            {
                Console.WriteLine("\t"+ data.numberOfSelfChampions + " self champions");
                for (int i = 0; i < data.numberOfSelfChampions; i++)
                {
                    Champion* champion = &(((Champion*)data.selfChampionsArray.ToPointer())[i]);
                    Console.WriteLine("\t\tchampion at "+ champion->characterCenter.x + ", "+ champion->characterCenter.y + " with health " + champion->health);
                }
            }
            if (data.numberOfEnemyMinions > 0)
            {
                Console.WriteLine("\t"+ data.numberOfEnemyMinions + " enemy minions");
                for (int i = 0; i < data.numberOfEnemyMinions; i++)
                {
                    Minion* minion = &(((Minion*)data.enemyMinionsArray.ToPointer())[i]);
                    Console.WriteLine("\t\tminion at "+ minion->characterCenter.x + ", "+ minion->characterCenter.y + " with health "+ minion->health);
                }
            }
            if (data.numberOfEnemyChampions > 0)
            {
                Console.WriteLine("\t"+ data.numberOfEnemyChampions + " enemy champions");
                for (int i = 0; i < data.numberOfEnemyChampions; i++)
                {
                    Champion* champion = &(((Champion*)data.enemyChampionsArray.ToPointer())[i]);
                    Console.WriteLine("\t\tchampion at "+ champion->characterCenter.x + ", "+ champion->characterCenter.y + " with health "+ champion->health);
                }
            }
            if (data.numberOfEnemyTowers > 0)
            {
                Console.WriteLine("\t"+ data.numberOfEnemyTowers + " enemy towers");
                for (int i = 0; i < data.numberOfEnemyTowers; i++)
                {
                    Tower* tower = &(((Tower*)data.enemyTowersArray.ToPointer())[i]);
                    Console.WriteLine("\t\ttower at "+ tower->towerCenter.x + ", "+ tower->towerCenter.y + " with health "+ tower->health);
                }
            }
            if (data.selfHealthBarVisible)
            {
                Console.WriteLine("\tCan see self health bar");
                Console.WriteLine("\tSelf health: " + ((SelfHealth*)data.selfHealthBar.ToPointer())->health);
            }
            if (data.spell1LevelUpAvailable)
            {
                Console.WriteLine("\tLevel up spell 1 available");
            }
            if (data.spell2LevelUpAvailable)
            {
                Console.WriteLine("\tLevel up spell 2 available");
            }
            if (data.spell3LevelUpAvailable)
            {
                Console.WriteLine("\tLevel up spell 3 available");
            }
            if (data.spell4LevelUpAvailable)
            {
                Console.WriteLine("\tLevel up spell 4 available");
            }
            if (data.currentLevel > 0)
            {
                Console.WriteLine("\tDetected current level: " + data.currentLevel);
            }
            if (data.spell1ActiveAvailable)
            {
                Console.WriteLine("\tSpell 1 available");
            }
            if (data.spell2ActiveAvailable)
            {
                Console.WriteLine("\tSpell 2 available");
            }
            if (data.spell3ActiveAvailable)
            {
                Console.WriteLine("\tSpell 3 available");
            }
            if (data.spell4ActiveAvailable)
            {
                Console.WriteLine("\tSpell 4 available");
            }
            if (data.summonerSpell1ActiveAvailable)
            {
                Console.WriteLine("\tSummoner spell 1 available");
            }
            if (data.summonerSpell2ActiveAvailable)
            {
                Console.WriteLine("\tSummoner spell 2 available");
            }
            if (data.trinketActiveAvailable)
            {
                Console.WriteLine("\tTrinket active available");
            }
            if (data.item1ActiveAvailable)
            {
                Console.WriteLine("\tItem 1 active available");
            }
            if (data.item2ActiveAvailable)
            {
                Console.WriteLine("\tItem 2 active available");
            }
            if (data.item3ActiveAvailable)
            {
                Console.WriteLine("\tItem 3 active available");
            }
            if (data.item4ActiveAvailable)
            {
                Console.WriteLine("\tItem 4 active available");
            }
            if (data.item5ActiveAvailable)
            {
                Console.WriteLine("\tItem 5 active available");
            }
            if (data.item6ActiveAvailable)
            {
                Console.WriteLine("\tItem 6 active available");
            }
            if (data.potionActiveAvailable)
            {
                Console.WriteLine("\tPotion active available");

                Console.WriteLine("\t\tPotion in slot " + data.potionOnActive);
            }
            if (data.potionBeingUsedShown)
            {
                Console.WriteLine("\tPotion being used");
            }
            if (data.shopAvailableShown)
            {
                Console.WriteLine("\tShop is available");
            }
            if (data.shopTopLeftCornerShown)
            {
                Console.WriteLine("\tShop top left corner is visible");
            }
            if (data.shopBottomLeftCornerShown)
            {
                Console.WriteLine("\tShop bottom left corner is visible");
            }
            if (data.numberOfBuyableItems > 0)
            {
                Console.WriteLine("\tBuyable items: " + data.numberOfBuyableItems);
            }
            if (data.mapVisible)
            {
                Console.WriteLine("\tMap is visible");
            }
            if (data.mapShopVisible)
            {
                Console.WriteLine("\tShop on map is visible");
            }
            if (data.mapSelfLocationVisible)
            {
                Console.WriteLine("\tLocation on map is visible");
            }
            if (data.surrenderAvailable)
            {
                Console.WriteLine("\tSurrender is visible");
            }
            if (data.continueAvailable)
            {
                GenericObject* active = (GenericObject*)data.continueActive.ToPointer();
                Console.WriteLine("\tContinue is visible at " + active->topLeft.x + ", " + active->topLeft.y);
            }
            if (data.afkAvailable)
            {
                Console.WriteLine("\tAFK is visible");
            }
            if (data.stoppedWorkingAvailable)
            {
                Console.WriteLine("\tStopped Working is visible");
            }

            Console.Out.Flush();

            /*
            Console.WriteLine("\nC# bytes");
            byte[] bytes = ToByteArray(detectionData);
            Console.WriteLine("Bytes count: " + Marshal.SizeOf(detectionData));
            Console.WriteLine("[ " + BitConverter.ToString(bytes).Replace("-", " ").ToLower() + " ]");
            Console.WriteLine("\n");
            */
            //visualCortex.freeVisualDetectionData(ref detectionData);
            //Console.WriteLine("Test Ending Detection data test");
        }
        public void setFPS(int milliseconds = 16)
        {
            fpsLimit = milliseconds;
            /*
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
            */
        }
        public void setUpdateDisplayImage(bool b)
        {
            updateDisplayImage = b;
            visualCortex.setShouldCaptureDisplayImage(b);
        }
        
    }
}
