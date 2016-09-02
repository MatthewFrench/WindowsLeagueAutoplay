using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace League_Autoplay
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Champion
    {
        public Position topLeft, topRight, bottomLeft, bottomRight, characterCenter;
        [MarshalAs(UnmanagedType.U1)]
        public bool detectedTopLeft, detectedBottomLeft, detectedTopRight, detectedBottomRight;
        public double health;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GenericObject
    {
        public Position topLeft, topRight, bottomLeft, bottomRight, center;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Minion
    {
        public Position topLeft, topRight, bottomLeft, bottomRight, characterCenter;
        [MarshalAs(UnmanagedType.U1)]
        public bool detectedTopLeft, detectedBottomLeft, detectedTopRight, detectedBottomRight;
        public double health;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Position
    {
        public int x, y;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SelfHealth
    {
        public Position topLeft, topRight, bottomLeft, bottomRight, characterCenter;
        [MarshalAs(UnmanagedType.U1)]
        public bool detectedLeftSide, detectedRightSide;
        public double health;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Tower
    {
        public Position topLeft, topRight, bottomLeft, bottomRight, towerCenter;
        [MarshalAs(UnmanagedType.U1)]
        public bool detectedTopLeft, detectedBottomLeft, detectedTopRight, detectedBottomRight;
        public double health;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct DetectionDataStruct
    {
        public IntPtr allyMinionsArray;
        public int numberOfAllyMinions;
        public IntPtr enemyMinionsArray;
        public int numberOfEnemyMinions;
        public IntPtr allyChampionsArray;
        public int numberOfAllyChampions;
        public IntPtr enemyChampionsArray;
        public int numberOfEnemyChampions;
        public IntPtr selfChampionsArray;
        public int numberOfSelfChampions;
        public IntPtr enemyTowersArray;
        public int numberOfEnemyTowers;
        [MarshalAs(UnmanagedType.U1)]
        public bool spell1LevelUpAvailable, spell2LevelUpAvailable, spell3LevelUpAvailable, spell4LevelUpAvailable;
        public IntPtr spell1LevelUp;
        public IntPtr spell2LevelUp;
        public IntPtr spell3LevelUp;
        public IntPtr spell4LevelUp;
        public IntPtr spell1LevelDotsArray;
        public IntPtr spell2LevelDotsArray;
        public IntPtr spell3LevelDotsArray;
        public IntPtr spell4LevelDotsArray;
        public int numberOfSpell1Dots, numberOfSpell2Dots, numberOfSpell3Dots, numberOfSpell4Dots;
        [MarshalAs(UnmanagedType.U1)]
        public bool spell1LevelDotsVisible, spell2LevelDotsVisible, spell3LevelDotsVisible, spell4LevelDotsVisible;
        public int currentLevel;
        [MarshalAs(UnmanagedType.U1)]
        public bool spell1ActiveAvailable, spell2ActiveAvailable, spell3ActiveAvailable, spell4ActiveAvailable;
        public IntPtr spell1Active;
        public IntPtr spell2Active;
        public IntPtr spell3Active;
        public IntPtr spell4Active;
        [MarshalAs(UnmanagedType.U1)]
        public bool summonerSpell1ActiveAvailable, summonerSpell2ActiveAvailable;
        public IntPtr summonerSpell1Active;
        public IntPtr summonerSpell2Active;
        [MarshalAs(UnmanagedType.U1)]
        public bool trinketActiveAvailable;
        public IntPtr trinketActive;
        [MarshalAs(UnmanagedType.U1)]
        public bool item1ActiveAvailable, item2ActiveAvailable, item3ActiveAvailable, item4ActiveAvailable, item5ActiveAvailable, item6ActiveAvailable;
        public IntPtr item1Active;
        public IntPtr item2Active;
        public IntPtr item3Active;
        public IntPtr item4Active;
        public IntPtr item5Active;
        public IntPtr item6Active;
        [MarshalAs(UnmanagedType.U1)]
        public bool potionActiveAvailable;
        public int potionOnActive;
        public IntPtr potionActive;
        [MarshalAs(UnmanagedType.U1)]
        public bool potionBeingUsedShown;
        public IntPtr potionBeingUsed;
        [MarshalAs(UnmanagedType.U1)]
        public bool shopAvailableShown;
        public IntPtr shopAvailable;
        [MarshalAs(UnmanagedType.U1)]
        public bool shopTopLeftCornerShown;
        public IntPtr shopTopLeftCorner;
        [MarshalAs(UnmanagedType.U1)]
        public bool shopBottomLeftCornerShown;
        public IntPtr shopBottomLeftCorner;
        public IntPtr buyableItemsArray;
        public int numberOfBuyableItems;
        [MarshalAs(UnmanagedType.U1)]
        public bool mapVisible;
        public IntPtr map;
        [MarshalAs(UnmanagedType.U1)]
        public bool mapShopVisible;
        public IntPtr mapShop;
        [MarshalAs(UnmanagedType.U1)]
        public bool mapSelfLocationVisible;
        public IntPtr mapSelfLocation;
        [MarshalAs(UnmanagedType.U1)]
        public bool selfHealthBarVisible;
        public IntPtr selfHealthBar;
        [MarshalAs(UnmanagedType.U1)]
        public bool surrenderAvailable;
        public IntPtr surrenderActive;
    }
}
