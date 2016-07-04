using System;
using System.Drawing.Imaging;
using System.IO;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;
using Rectangle = SharpDX.Rectangle;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DesktopDuplication
{
    /// <summary>
    /// Provides access to frame-by-frame updates of a particular desktop (i.e. one monitor), with image and cursor information.
    /// </summary>
    /// 
    public class DesktopDuplicator
    {
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void Copy(byte* startPointer, byte* startDestinationPointer, int width, int height);
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void initializeDetectionManager();
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void processDetection(byte* dataPointer, int width, int height);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadLevelUpImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadLevelDotImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadLevelUpDisabledImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadAbilityEnabledImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadAbilityDisabledImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AbilityManager_loadEnabledSummonerSpellImageData(byte* data, int imageWidth, int imageHeight);
        //Ally image loading code
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadTopLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadBottomLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadBottomRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadTopRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyChampionManager_loadHealthSegmentImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadTopLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadBottomLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadBottomRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadTopRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadHealthSegmentImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void AllyMinionManager_loadWardImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadTopLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadTopRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadHealthSegmentImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomBarLeftSideImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomBarRightSideImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SelfChampionManager_loadBottomBarAverageHealthColorImageData(byte* data, int imageWidth, int imageHeight);
        //Enemy image loading code

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadTopLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadBottomLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadBottomRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadTopRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyChampionManager_loadHealthSegmentImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadTopLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadBottomLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadBottomRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadTopRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyMinionManager_loadHealthSegmentImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadTopLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadBottomLeftImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadBottomRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadTopRightImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void EnemyTowerManager_loadHealthSegmentImageData(byte* data, int imageWidth, int imageHeight);
        //Item image loading code

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadTrinketItemImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadItemImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadPotionImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadUsedPotionImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ItemManager_loadUsedPotionInnerImageData(byte* data, int imageWidth, int imageHeight);
        //Map image loading code

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void MapManager_loadMapTopLeftCornerImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void MapManager_loadShopIconImageData(byte* data, int imageWidth, int imageHeight);
        //Surrender image loading code
        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void SurrenderManager_loadSurrenderImageData(byte* data, int imageWidth, int imageHeight);
        //Shop image loading code

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopTopLeftCornerImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopAvailableImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBottomLeftCornerImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBuyableItemTopLeftCornerImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBuyableItemBottomLeftCornerImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBuyableItemTopRightCornerImageData(byte* data, int imageWidth, int imageHeight);

        [DllImport("Test DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ShopManager_loadShopBuyableItemBottomRightCornerImageData(byte* data, int imageWidth, int imageHeight);

        private Device mDevice;
        private Texture2DDescription mTextureDesc;
        private OutputDescription mOutputDesc;
        private OutputDuplication mDeskDupl;

        private Texture2D desktopImageTexture = null;
        private OutputDuplicateFrameInformation frameInfo = new OutputDuplicateFrameInformation();
        private int mWhichOutputDevice = -1;

        private Bitmap finalImage1, finalImage2;
        private bool isFinalImage1 = false;

        int count = 0;
        long total = 0;

        private Bitmap FinalImage
        {
            get
            {
                return isFinalImage1 ? finalImage1 : finalImage2;
            }
            set
            {
                if (isFinalImage1)
                {
                    finalImage2 = value;
                    if (finalImage1 != null) finalImage1.Dispose();
                }
                else
                {
                    finalImage1 = value;
                    if (finalImage2 != null) finalImage2.Dispose();
                }
                isFinalImage1 = !isFinalImage1;
            }
        }
        
        /// <summary>
        /// Duplicates the output of the specified monitor.
        /// </summary>
        /// <param name="whichMonitor">The output device to duplicate (i.e. monitor). Begins with zero, which seems to correspond to the primary monitor.</param>
        public DesktopDuplicator(int whichMonitor)
            : this(0, whichMonitor) {

            initializeDetectionManager();
            //var dllFile = new FileInfo(@".\C++ DLL League Autoplay.dll");
            //Console.WriteLine("Loaded DLL: {0}", dllFile.FullName);
            //var DLL = Assembly.LoadFile(dllFile.FullName);
            loadDetectionImages();
        }

        public unsafe byte* getBytesForBitmap(Bitmap bitmap)
        {
            System.Drawing.Rectangle boundsRect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);

            BitmapData bitmapData = bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, FinalImage.PixelFormat);
            IntPtr bitmapPtr = bitmapData.Scan0;

            IntPtr toPtr = Marshal.AllocHGlobal(sizeof(byte) * bitmap.Width * bitmap.Height * 4);

            IntPtr originalPtr = toPtr;

            for (int y = 0; y < bitmap.Height; y++)
            {
                // Copy a single line 
                Utilities.CopyMemory(toPtr, bitmapPtr, bitmap.Width * 4);

                // Advance pointers
                bitmapPtr = IntPtr.Add(bitmapPtr, bitmapData.Stride);
                toPtr = IntPtr.Add(toPtr, bitmapData.Stride);
            }

            bitmap.UnlockBits(bitmapData);

            return (byte*)originalPtr.ToPointer();
        }

        public unsafe void loadDetectionImages()
        {
            Bitmap image;
            image = new Bitmap(Image.FromFile("Resources/Skill Bar/Enabled Summoner Spell.png"));
            AbilityManager_loadEnabledSummonerSpellImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Skill Bar/Leveled Dot.png"));
            AbilityManager_loadLevelDotImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Skill Bar/Level Up.png"));
            AbilityManager_loadLevelUpImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Skill Bar/Level Up Disabled.png"));
            AbilityManager_loadLevelUpDisabledImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Skill Bar/Enabled Ability.png"));
            AbilityManager_loadAbilityEnabledImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Skill Bar/Disabled Ability.png"));
            AbilityManager_loadAbilityDisabledImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Champion Health Bar/Top Left Corner.png"));
            AllyChampionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Champion Health Bar/Bottom Left Corner.png"));
            AllyChampionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Champion Health Bar/Bottom Right Corner.png"));
            AllyChampionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Champion Health Bar/Top Right Corner.png"));
            AllyChampionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Champion Health Bar/Health Segment.png"));
            AllyChampionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ward/Pink Ward.png"));
            AllyMinionManager_loadWardImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Minion Health Bar/Top Left Corner.png"));
            AllyMinionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Minion Health Bar/Bottom Left Corner.png"));
            AllyMinionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Minion Health Bar/Bottom Right Corner.png"));
            AllyMinionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Minion Health Bar/Top Right Corner.png"));
            AllyMinionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Ally Minion Health Bar/Health Segment.png"));
            AllyMinionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Self Health Bar/Top Left Corner.png"));
            SelfChampionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Self Health Bar/Bottom Left Corner.png"));
            SelfChampionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Self Health Bar/Bottom Right Corner.png"));
            SelfChampionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Self Health Bar/Top Right Corner.png"));
            SelfChampionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Self Health Bar/Health Segment.png"));
            SelfChampionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Self Health Bar/Bottom Bar Left Side.png"));
            SelfChampionManager_loadBottomBarLeftSideImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Self Health Bar/Bottom Bar Right Side.png"));
            SelfChampionManager_loadBottomBarRightSideImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Self Health Bar/Bottom Bar Average Health Color.png"));
            SelfChampionManager_loadBottomBarAverageHealthColorImageData(getBytesForBitmap(image), image.Width, image.Height);
            

            image = new Bitmap(Image.FromFile("Resources/Enemy Champion Health Bar/Top Left Corner.png"));
            EnemyChampionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Champion Health Bar/Bottom Left Corner.png"));
            EnemyChampionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Champion Health Bar/Bottom Right Corner.png"));
            EnemyChampionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Champion Health Bar/Top Right Corner.png"));
            EnemyChampionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Champion Health Bar/Health Segment.png"));
            EnemyChampionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Minion Health Bar/Top Left Corner.png"));
            EnemyMinionManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Minion Health Bar/Bottom Left Corner.png"));
            EnemyMinionManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Minion Health Bar/Bottom Right Corner.png"));
            EnemyMinionManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Minion Health Bar/Top Right Corner.png"));
            EnemyMinionManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Minion Health Bar/Health Segment.png"));
            EnemyMinionManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Tower Health Bar/Top Left Corner.png"));
            EnemyTowerManager_loadTopLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Tower Health Bar/Bottom Left Corner.png"));
            EnemyTowerManager_loadBottomLeftImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Tower Health Bar/Bottom Right Corner.png"));
            EnemyTowerManager_loadBottomRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Tower Health Bar/Top Right Corner.png"));
            EnemyTowerManager_loadTopRightImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Enemy Tower Health Bar/Health Segment.png"));
            EnemyTowerManager_loadHealthSegmentImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Item Bar/Trinket Active.png"));
            ItemManager_loadTrinketItemImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Item Bar/Usable Item.png"));
            ItemManager_loadItemImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Item Bar/Potion.png"));
            ItemManager_loadPotionImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Item Bar/Used Potion.png"));
            ItemManager_loadUsedPotionImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Item Bar/Used Potion Inner.png"));
            ItemManager_loadUsedPotionInnerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Map/Shop Icon.png"));
            MapManager_loadShopIconImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Map/Map Top Left Corner.png"));
            MapManager_loadMapTopLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Surrender/Surrender.png"));
            SurrenderManager_loadSurrenderImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Shop/Buyable Item Top Left Corner.png"));
            ShopManager_loadShopBuyableItemTopLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Shop/Buyable Item Bottom Left Corner.png"));
            ShopManager_loadShopBuyableItemBottomLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Shop/Buyable Item Top Right Corner.png"));
            ShopManager_loadShopBuyableItemTopRightCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Shop/Buyable Item Bottom Right Corner.png"));
            ShopManager_loadShopBuyableItemBottomRightCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Shop/Shop Top Left Corner.png"));
            ShopManager_loadShopTopLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Shop/Shop Available.png"));
            ShopManager_loadShopAvailableImageData(getBytesForBitmap(image), image.Width, image.Height);

            image = new Bitmap(Image.FromFile("Resources/Shop/Shop Bottom Left Corner.png"));
            ShopManager_loadShopBottomLeftCornerImageData(getBytesForBitmap(image), image.Width, image.Height);

        }

        /// <summary>
        /// Duplicates the output of the specified monitor on the specified graphics adapter.
        /// </summary>
        /// <param name="whichGraphicsCardAdapter">The adapter which contains the desired outputs.</param>
        /// <param name="whichOutputDevice">The output device to duplicate (i.e. monitor). Begins with zero, which seems to correspond to the primary monitor.</param>
        public DesktopDuplicator(int whichGraphicsCardAdapter, int whichOutputDevice)
        {
            this.mWhichOutputDevice = whichOutputDevice;
            Adapter1 adapter = null;
            try
            {
                adapter = new Factory1().GetAdapter1(whichGraphicsCardAdapter);
            }
            catch (SharpDXException)
            {
                throw new DesktopDuplicationException("Could not find the specified graphics card adapter.");
            }
            this.mDevice = new Device(adapter);
            Output output = null;
            try
            {
                output = adapter.GetOutput(whichOutputDevice);
            }
            catch (SharpDXException)
            {
                throw new DesktopDuplicationException("Could not find the specified output device.");
            }
            var output1 = output.QueryInterface<Output1>();
            this.mOutputDesc = output.Description;
            this.mTextureDesc = new Texture2DDescription()
            {
                CpuAccessFlags = CpuAccessFlags.Read,
                BindFlags = BindFlags.None,
                Format = Format.B8G8R8A8_UNorm,
                Width = this.mOutputDesc.DesktopBounds.Width,
                Height = this.mOutputDesc.DesktopBounds.Height,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Staging
            };

            try
            {
                this.mDeskDupl = output1.DuplicateOutput(mDevice);
            }
            catch (SharpDXException ex)
            {
                if (ex.ResultCode.Code == SharpDX.DXGI.ResultCode.NotCurrentlyAvailable.Result.Code)
                {
                    throw new DesktopDuplicationException("There is already the maximum number of applications using the Desktop Duplication API running, please close one of the applications and try again.");
                }
            }
        }

        /// <summary>
        /// Retrieves the latest desktop image and associated metadata.
        /// </summary>
        public DesktopFrame GetLatestFrame()
        {
            var frame = new DesktopFrame();
            // Try to get the latest frame; this may timeout
            bool retrievalTimedOut = RetrieveFrame();
            if (retrievalTimedOut)
                return null;
            try
            {
                RetrieveFrameMetadata(frame);
                RetrieveCursorMetadata(frame);
                ProcessFrame(frame);
            }
            catch
            {
                ReleaseFrame();
            }
            try
            {
                ReleaseFrame();
            }
            catch { 
            //    throw new DesktopDuplicationException("Couldn't release frame.");  
            }
            return frame;
        }

        private bool RetrieveFrame()
        {
            if (desktopImageTexture == null)
                desktopImageTexture = new Texture2D(mDevice, mTextureDesc);
            SharpDX.DXGI.Resource desktopResource = null;
            frameInfo = new OutputDuplicateFrameInformation();
            try
            {
                mDeskDupl.AcquireNextFrame(500, out frameInfo, out desktopResource);
            }
            catch (SharpDXException ex)
            {
                if (ex.ResultCode.Code == SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
                {
                    return true;
                }
                if (ex.ResultCode.Failure)
                {
                    throw new DesktopDuplicationException("Failed to acquire next frame.");
                }
            }
            using (var tempTexture = desktopResource.QueryInterface<Texture2D>())
                mDevice.ImmediateContext.CopyResource(tempTexture, desktopImageTexture);
            desktopResource.Dispose();
            return false;
        }

        private void RetrieveFrameMetadata(DesktopFrame frame)
        {

            if (frameInfo.TotalMetadataBufferSize > 0)
            {
                // Get moved regions
                int movedRegionsLength = 0;
                OutputDuplicateMoveRectangle[] movedRectangles = new OutputDuplicateMoveRectangle[frameInfo.TotalMetadataBufferSize];
                mDeskDupl.GetFrameMoveRects(movedRectangles.Length, movedRectangles, out movedRegionsLength);
                frame.MovedRegions = new MovedRegion[movedRegionsLength / Marshal.SizeOf(typeof(OutputDuplicateMoveRectangle))];
                for (int i = 0; i < frame.MovedRegions.Length; i++)
                {
                    frame.MovedRegions[i] = new MovedRegion()
                    {
                        Source = new System.Drawing.Point(movedRectangles[i].SourcePoint.X, movedRectangles[i].SourcePoint.Y),
                        Destination = new System.Drawing.Rectangle(movedRectangles[i].DestinationRect.X, movedRectangles[i].DestinationRect.Y, movedRectangles[i].DestinationRect.Width, movedRectangles[i].DestinationRect.Height)
                    };
                }

                // Get dirty regions
                int dirtyRegionsLength = 0;
                Rectangle[] dirtyRectangles = new Rectangle[frameInfo.TotalMetadataBufferSize];
                mDeskDupl.GetFrameDirtyRects(dirtyRectangles.Length, dirtyRectangles, out dirtyRegionsLength);
                frame.UpdatedRegions = new System.Drawing.Rectangle[dirtyRegionsLength / Marshal.SizeOf(typeof(Rectangle))];
                for (int i = 0; i < frame.UpdatedRegions.Length; i++)
                {
                    frame.UpdatedRegions[i] = new System.Drawing.Rectangle(dirtyRectangles[i].X, dirtyRectangles[i].Y, dirtyRectangles[i].Width, dirtyRectangles[i].Height);
                }
            }
            else
            {
                frame.MovedRegions = new MovedRegion[0];
                frame.UpdatedRegions = new System.Drawing.Rectangle[0];
            }
        }

        private void RetrieveCursorMetadata(DesktopFrame frame)
        {
            var pointerInfo = new PointerInfo();

            // A non-zero mouse update timestamp indicates that there is a mouse position update and optionally a shape change
            if (frameInfo.LastMouseUpdateTime == 0)
                return;

            bool updatePosition = true;

            // Make sure we don't update pointer position wrongly
            // If pointer is invisible, make sure we did not get an update from another output that the last time that said pointer
            // was visible, if so, don't set it to invisible or update.

            if (!frameInfo.PointerPosition.Visible && (pointerInfo.WhoUpdatedPositionLast != this.mWhichOutputDevice))
                updatePosition = false;

            // If two outputs both say they have a visible, only update if new update has newer timestamp
            if (frameInfo.PointerPosition.Visible && pointerInfo.Visible && (pointerInfo.WhoUpdatedPositionLast != this.mWhichOutputDevice) && (pointerInfo.LastTimeStamp > frameInfo.LastMouseUpdateTime))
                updatePosition = false;

            // Update position
            if (updatePosition)
            {
                pointerInfo.Position = new SharpDX.Point(frameInfo.PointerPosition.Position.X, frameInfo.PointerPosition.Position.Y);
                pointerInfo.WhoUpdatedPositionLast = mWhichOutputDevice;
                pointerInfo.LastTimeStamp = frameInfo.LastMouseUpdateTime;
                pointerInfo.Visible = frameInfo.PointerPosition.Visible;
            }
                        
            // No new shape
            if (frameInfo.PointerShapeBufferSize == 0)
                return;

            if (frameInfo.PointerShapeBufferSize > pointerInfo.BufferSize)
            {
                pointerInfo.PtrShapeBuffer = new byte[frameInfo.PointerShapeBufferSize];
                pointerInfo.BufferSize = frameInfo.PointerShapeBufferSize;
            }

            try
            {
                unsafe
                {
                    fixed (byte* ptrShapeBufferPtr = pointerInfo.PtrShapeBuffer)
                    {
                        mDeskDupl.GetFramePointerShape(frameInfo.PointerShapeBufferSize, (IntPtr)ptrShapeBufferPtr, out pointerInfo.BufferSize, out pointerInfo.ShapeInfo);
                    }
                }
            }
            catch (SharpDXException ex)
            {
                if (ex.ResultCode.Failure)
                {
                    throw new DesktopDuplicationException("Failed to get frame pointer shape.");
                }
            }

            //frame.CursorVisible = pointerInfo.Visible;
            frame.CursorLocation = new System.Drawing.Point(pointerInfo.Position.X, pointerInfo.Position.Y);
        }
        
        private void ProcessFrame(DesktopFrame frame)
        {
            // Get the desktop capture texture
            var mapSource = mDevice.ImmediateContext.MapSubresource(desktopImageTexture, 0, MapMode.Read, MapFlags.None);

            FinalImage = new System.Drawing.Bitmap(mOutputDesc.DesktopBounds.Width, mOutputDesc.DesktopBounds.Height, PixelFormat.Format32bppRgb);
            var boundsRect = new System.Drawing.Rectangle(0, 0, mOutputDesc.DesktopBounds.Width, mOutputDesc.DesktopBounds.Height);
            var sourcePtr = mapSource.DataPointer;

            var mapDest = FinalImage.LockBits(boundsRect, ImageLockMode.WriteOnly, FinalImage.PixelFormat);
            var destPtr = mapDest.Scan0;

            Stopwatch performanceWatch = new Stopwatch();
            performanceWatch.Start();



            // Test 1
            
            unsafe
            {
                byte* startDestinationPointer = (byte*)destPtr.ToPointer();

                byte* startPointer = (byte*)sourcePtr.ToPointer();

                Copy(startPointer, startDestinationPointer, mOutputDesc.DesktopBounds.Width, mOutputDesc.DesktopBounds.Height);

                processDetection(startPointer, mOutputDesc.DesktopBounds.Width, mOutputDesc.DesktopBounds.Height);

                /*
                int height = mOutputDesc.DesktopBounds.Height;
                int width = mOutputDesc.DesktopBounds.Width;
                int channels = 4;
                const int r_channel = 2;
                const int g_channel = 1;
                const int b_channel = 0;
                const int a_channel = 3;

                int loc = 0;
               
                for (int y = 0; y < height; y++)
                {
                    
                   for (int x = 0; x < width; x++)
                    {
                    
                        
                        startDestinationPointer[loc + b_channel] = startPointer[loc + b_channel];
                        startDestinationPointer[loc + g_channel] = startPointer[loc + g_channel];
                        startDestinationPointer[loc + r_channel] = startPointer[loc + r_channel];
                        startDestinationPointer[loc + a_channel] = startPointer[loc + a_channel];

                        loc += 4;
                        
                    }
                    
                }
             */
            }

            FinalImage.UnlockBits(mapDest);
             //End Test 1


            //Test 2, 6 times faster than test 1
            /*
            for (int y = 0; y < mOutputDesc.DesktopBounds.Height; y++)
            {
                // Copy a single line 
                Utilities.CopyMemory(destPtr, sourcePtr, mOutputDesc.DesktopBounds.Width * 4);

                // Advance pointers
                sourcePtr = IntPtr.Add(sourcePtr, mapSource.RowPitch);
                destPtr = IntPtr.Add(destPtr, mapDest.Stride);
            }

            // Release source and dest locks
            FinalImage.UnlockBits(mapDest);
            */


            performanceWatch.Stop();

            Console.WriteLine("Elapsed milliseconds: {0}", performanceWatch.Elapsed.Ticks / 10000.0);
            Console.WriteLine("Elapsed fps: {0}", 1000.0 / (performanceWatch.Elapsed.Ticks / 10000.0));
            count++;
            total += performanceWatch.Elapsed.Ticks;
            Console.WriteLine("Average milliseconds: {0}", total / 10000.0 / count);
            Console.WriteLine("Average fps: {0}", 1000.0 / (total / 10000.0 / count));
            //Console.WriteLine("Add 1 + 1 = {0}", Add(1,1));


            /*

            // Copy pixels from screen capture Texture to GDI bitmap
            var mapDest = FinalImage.LockBits(boundsRect, ImageLockMode.WriteOnly, FinalImage.PixelFormat);
            var destPtr = mapDest.Scan0;

            for (int y = 0; y < mOutputDesc.DesktopBounds.Height; y++)
            {
                // Copy a single line 
                Utilities.CopyMemory(destPtr, sourcePtr, mOutputDesc.DesktopBounds.Width * 4);

                // Advance pointers
                sourcePtr = IntPtr.Add(sourcePtr, mapSource.RowPitch);
                destPtr = IntPtr.Add(destPtr, mapDest.Stride);
            }

            // Release source and dest locks
            FinalImage.UnlockBits(mapDest);
            */

            mDevice.ImmediateContext.UnmapSubresource(desktopImageTexture, 0);
            
            frame.DesktopImage = FinalImage;
        }

        private void ReleaseFrame()
        {
            try
            {
                mDeskDupl.ReleaseFrame();
            }
            catch (SharpDXException ex)
            {
                if (ex.ResultCode.Failure)
                {
                    throw new DesktopDuplicationException("Failed to release frame.");
                }
            }
        }
    }
}
