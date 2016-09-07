//using DesktopDuplication;
//using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using League_Autoplay.High_Performance_Timer;
using SharpDX;

namespace League_Autoplay
{
    public class VisualCortex
    {
        int count = 0;
        double total = 0;
        public Bitmap testImage;
        //private DesktopDuplicator desktopDuplicator;

        bool test = false;
        String testImageName = "AnalysisImages\\Resources\\Test Images\\New Victory Screen Test.png";
        bool shouldCaptureDisplayImage = false;
        bool recordDisplayImage = false;
        Bitmap displayImage;

        High_Performance_Timer.Stopwatch saveStopwatch;

        public VisualCortex()
        {
            saveStopwatch = new High_Performance_Timer.Stopwatch();
            //try
            //{
            //    desktopDuplicator = new DesktopDuplicator(0);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            initializeDetectionManager();
            loadDetectionImages();


            string dir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            testImage = new Bitmap(Image.FromFile(Path.Combine(dir, testImageName)));

            //int width = desktopDuplicator.getFrameWidth();
            //int height = desktopDuplicator.getFrameHeight();
            //Image processing

            //Console.WriteLine("Detection Width: " + width);
            //Console.WriteLine("Detection Height: " + height);
        }

        public Bitmap getDisplayImage()
        {
            return displayImage;
        }

        public void setShouldCaptureDisplayImage(bool b)
        {
            shouldCaptureDisplayImage = b;
        }

        public Bitmap grabScreen2(bool detect)
        {
            Bitmap returnValue = null;
            if (test)
            {
                if (test)
                {
                    //TEST CODE
                    System.Drawing.Rectangle boundsRect = new System.Drawing.Rectangle(0, 0, testImage.Width, testImage.Height);
                    var bitmapData = testImage.LockBits(boundsRect, ImageLockMode.WriteOnly, testImage.PixelFormat);
                    var bitmapPointer = bitmapData.Scan0;
                    unsafe
                    {
                        Console.WriteLine("Test image width: " + testImage.Width + ", height: " + testImage.Height);
                        processDetection((byte*)bitmapPointer.ToPointer(), testImage.Width, testImage.Height);
                    }

                    testImage.UnlockBits(bitmapData);
                    displayImage = new Bitmap(testImage);
                    returnValue = new Bitmap(testImage);
                }
            }
            else
            {


                Bitmap desktopBMP = new Bitmap(
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

                Graphics g = Graphics.FromImage(desktopBMP);

                g.CopyFromScreen(0, 0, 0, 0,
                   new Size(
                   System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                   System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height));


                g.Dispose();
                if (detect)
                {
                    System.Drawing.Rectangle boundsRect = new System.Drawing.Rectangle(0, 0, desktopBMP.Width, desktopBMP.Height);
                    var bitmapData = desktopBMP.LockBits(boundsRect, ImageLockMode.WriteOnly, desktopBMP.PixelFormat);
                    var bitmapPointer = bitmapData.Scan0;
                    unsafe
                    {
                        processDetection((byte*)bitmapPointer.ToPointer(), desktopBMP.Width, desktopBMP.Height);
                    }

                    desktopBMP.UnlockBits(bitmapData);
                }
                if (shouldCaptureDisplayImage)
                {
                    displayImage = desktopBMP;
                }
                if (saveStopwatch.DurationInSeconds() > 1.0 && recordDisplayImage)
                {
                    saveStopwatch.Reset();
                    displayImage.Save("Recording/AI Record " + desktopBMP.Width + "x" + desktopBMP.Height + " " + Environment.TickCount + ".png", System.Drawing.Imaging.ImageFormat.Png);
                }
                returnValue = new Bitmap(testImage);
            }

            System.GC.Collect();

            return returnValue;
        }
        public unsafe void freeVisualDetectionData(ref DetectionDataStruct data)
        {
            freeDetectionData(ref data);
        }

        public unsafe DetectionDataStruct getVisualDetectionData()
        {
            DetectionDataStruct data = new DetectionDataStruct();
            getDetectionData(ref data);
            return data;
        }

        public void runTest()
        {
            System.Drawing.Rectangle boundsRect = new System.Drawing.Rectangle(0, 0, testImage.Width, testImage.Height);
            var bitmapData = testImage.LockBits(boundsRect, ImageLockMode.WriteOnly, testImage.PixelFormat);
            var bitmapPointer = bitmapData.Scan0;

            High_Performance_Timer.Stopwatch performanceWatch = new High_Performance_Timer.Stopwatch();

            unsafe
            {
                processDetection((byte*)bitmapPointer.ToPointer(), testImage.Width, testImage.Height);
            }


            testImage.UnlockBits(bitmapData);

            //Console.WriteLine("Elapsed milliseconds: {0}", performanceWatch.DurationInMilliseconds());
            //Console.WriteLine("Elapsed fps: {0}", 1000.0 / (performanceWatch.DurationInMilliseconds() / 10000.0));
            count++;
            total += performanceWatch.DurationInMilliseconds();
            //Console.WriteLine("Average milliseconds: {0}", total / count);
            //Console.WriteLine("Average fps: {0}", 1000.0 / (total  / count));
        }
        public bool isTesting()
        {
            return test;
        }
        

        public unsafe void loadDetectionImages()
        {
            string dir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

            Bitmap image;
            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Skill Bar\\Enabled Summoner Spell.png")));
            //Console.WriteLine("Image: {0}", image);
            byte* bytes = getBytesForBitmap(image);
            AbilityManager_loadEnabledSummonerSpellImageData(bytes, image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Skill Bar\\Leveled Dot.png")));
            AbilityManager_loadLevelDotImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Skill Bar\\Level Up.png")));
            AbilityManager_loadLevelUpImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Skill Bar\\Level Up Disabled.png")));
            AbilityManager_loadLevelUpDisabledImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Skill Bar\\Enabled Ability.png")));
            AbilityManager_loadAbilityEnabledImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Skill Bar\\Disabled Ability.png")));
            AbilityManager_loadAbilityDisabledImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Champion Health Bar\\Top Left Corner.png")));
            AllyChampionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Champion Health Bar\\Bottom Left Corner.png")));
            AllyChampionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Champion Health Bar\\Bottom Right Corner.png")));
            AllyChampionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Champion Health Bar\\Top Right Corner.png")));
            AllyChampionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Champion Health Bar\\Health Segment.png")));
            AllyChampionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ward\\Pink Ward.png")));
            AllyMinionManager_loadWardImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Minion Health Bar\\Top Left Corner.png")));
            AllyMinionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Minion Health Bar\\Bottom Left Corner.png")));
            AllyMinionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Minion Health Bar\\Bottom Right Corner.png")));
            AllyMinionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Minion Health Bar\\Top Right Corner.png")));
            AllyMinionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Ally Minion Health Bar\\Health Segment.png")));
            AllyMinionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Self Health Bar\\Top Left Corner.png")));
            SelfChampionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Self Health Bar\\Bottom Left Corner.png")));
            SelfChampionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Self Health Bar\\Bottom Right Corner.png")));
            SelfChampionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Self Health Bar\\Top Right Corner.png")));
            SelfChampionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Self Health Bar\\Health Segment.png")));
            SelfChampionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Self Health Bar\\Bottom Bar Left Side.png")));
            SelfChampionManager_loadBottomBarLeftSideImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Self Health Bar\\Bottom Bar Right Side.png")));
            SelfChampionManager_loadBottomBarRightSideImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Self Health Bar\\Bottom Bar Average Health Color.png")));
            SelfChampionManager_loadBottomBarAverageHealthColorImageData(getBytesForBitmap(image), image.Width, image.Height);


            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Champion Health Bar\\Top Left Corner.png")));
            EnemyChampionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Champion Health Bar\\Bottom Left Corner.png")));
            EnemyChampionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Champion Health Bar\\Bottom Right Corner.png")));
            EnemyChampionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Champion Health Bar\\Top Right Corner.png")));
            EnemyChampionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Champion Health Bar\\Health Segment.png")));
            EnemyChampionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Minion Health Bar\\Top Left Corner.png")));
            EnemyMinionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Minion Health Bar\\Bottom Left Corner.png")));
            EnemyMinionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Minion Health Bar\\Bottom Right Corner.png")));
            EnemyMinionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Minion Health Bar\\Top Right Corner.png")));
            EnemyMinionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Minion Health Bar\\Health Segment.png")));
            EnemyMinionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Tower Health Bar\\Top Left Corner.png")));
            EnemyTowerManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Tower Health Bar\\Bottom Left Corner.png")));
            EnemyTowerManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Tower Health Bar\\Bottom Right Corner.png")));
            EnemyTowerManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Tower Health Bar\\Top Right Corner.png")));
            EnemyTowerManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Enemy Tower Health Bar\\Health Segment.png")));
            EnemyTowerManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Item Bar\\Trinket Active.png")));
            ItemManager_loadTrinketItemImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Item Bar\\Usable Item.png")));
            ItemManager_loadItemImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Item Bar\\Potion.png")));
            ItemManager_loadPotionImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Item Bar\\Used Potion.png")));
            ItemManager_loadUsedPotionImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Item Bar\\Used Potion Inner.png")));
            ItemManager_loadUsedPotionInnerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Map\\Shop Icon.png")));
            MapManager_loadShopIconImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Map\\Map Top Left Corner.png")));
            MapManager_loadMapTopLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Surrender\\Surrender.png")));
            SurrenderManager_loadSurrenderImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Continue\\Continue.png")));
            ContinueManager_loadContinueImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\AFK\\AFK.png")));
            AFKManager_loadAFKImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Stop Working\\Stop Working.png")));
            StoppedWorkingManager_loadStoppedWorkingImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Shop\\Buyable Item Top Left Corner.png")));
            ShopManager_loadShopBuyableItemTopLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Shop\\Buyable Item Bottom Left Corner.png")));
            ShopManager_loadShopBuyableItemBottomLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Shop\\Buyable Item Top Right Corner.png")));
            ShopManager_loadShopBuyableItemTopRightCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Shop\\Buyable Item Bottom Right Corner.png")));
            ShopManager_loadShopBuyableItemBottomRightCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Shop\\Shop Top Left Corner.png")));
            ShopManager_loadShopTopLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Shop\\Shop Available.png")));
            ShopManager_loadShopAvailableImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Shop\\Shop Bottom Left Corner.png")));
            ShopManager_loadShopBottomLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

        }
        public unsafe byte* getBytesForBitmap(Bitmap bitmap)
        {
            System.Drawing.Rectangle boundsRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);

            BitmapData bitmapData = bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            IntPtr bitmapPtr = bitmapData.Scan0;

            IntPtr toPtr = Marshal.AllocHGlobal(sizeof(byte) * bitmap.Width * bitmap.Height * 4);

            Utilities.CopyMemory(toPtr, bitmapPtr, sizeof(byte) * bitmap.Width * bitmap.Height * 4);

            bitmap.UnlockBits(bitmapData);

            return (byte*)toPtr.ToPointer();
        }
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        //[return: MarshalAs(UnmanagedType.LPStruct)]
        public static extern unsafe void getDetectionData(ref DetectionDataStruct data);
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void freeDetectionData(ref DetectionDataStruct data);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void Copy(byte* startPointer, byte* startDestinationPointer, Int32 width, Int32 height);
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void initializeDetectionManager();
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void processDetection(byte* dataPointer, Int32 width, Int32 height);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadLevelUpImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadLevelDotImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadLevelUpDisabledImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadAbilityEnabledImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadAbilityDisabledImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadEnabledSummonerSpellImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        //Ally image loading code
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadTopLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadBottomLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadBottomRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadTopRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadHealthSegmentImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadTopLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadBottomLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadBottomRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadTopRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadHealthSegmentImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadWardImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadTopLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadTopRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadHealthSegmentImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomBarLeftSideImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomBarRightSideImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomBarAverageHealthColorImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        //Enemy image loading code

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadTopLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadBottomLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadBottomRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadTopRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadHealthSegmentImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadTopLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadBottomLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadBottomRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadTopRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadHealthSegmentImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadTopLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadBottomLeftImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadBottomRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadTopRightImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadHealthSegmentImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        //Item image loading code

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadTrinketItemImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadItemImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadPotionImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadUsedPotionImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadUsedPotionInnerImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        //Map image loading code

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void MapManager_loadMapTopLeftCornerImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void MapManager_loadShopIconImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        //Surrender image loading code
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SurrenderManager_loadSurrenderImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        //Continue image loading code
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ContinueManager_loadContinueImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AFKManager_loadAFKImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void StoppedWorkingManager_loadStoppedWorkingImageData(byte* data, Int32 imageWidth, Int32 imageHeight);
        //Shop image loading code

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopTopLeftCornerImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopAvailableImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBottomLeftCornerImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBuyableItemTopLeftCornerImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBuyableItemBottomLeftCornerImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBuyableItemTopRightCornerImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBuyableItemBottomRightCornerImageData(byte* data, Int32 imageWidth, Int32 imageHeight);

    }
}
