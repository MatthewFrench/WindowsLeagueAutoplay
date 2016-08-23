using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace League_Autoplay
{
    public struct Champion
    {
        Position topLeft, topRight, bottomLeft, bottomRight, characterCenter;
        bool detectedTopLeft, detectedBottomLeft, detectedTopRight, detectedBottomRight;
        double health;
    }

        public struct GenericObject
    {
        Position topLeft, topRight, bottomLeft, bottomRight, center;
    }

        public struct Minion
    {
        Position topLeft, topRight, bottomLeft, bottomRight, characterCenter;
        bool detectedTopLeft, detectedBottomLeft, detectedTopRight, detectedBottomRight;
        double health;
    }

        public struct Position
    {
        int x, y;
    }

    public struct SelfHealth
    {
        Position topLeft, topRight, bottomLeft, bottomRight, characterCenter;
        bool detectedLeftSide, detectedRightSide;
        double health;
    }

        public struct Tower
    {
        Position topLeft, topRight, bottomLeft, bottomRight, towerCenter;
        bool detectedTopLeft, detectedBottomLeft, detectedTopRight, detectedBottomRight;
        double health;
    }


    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DetectionDataStruct
    {
        //[MarshalAs(UnmanagedType.LPTStr)]
        Minion* allyMinionsArray;
        int numberOfAllyMinions;
        Minion* enemyMinionsArray;
        int numberOfEnemyMinions;
        Champion* allyChampionsArray;
        int numberOfAllyChampions;
        Champion* enemyChampionsArray;
        int numberOfEnemyChampions;
        Champion* selfChampionsArray;
        int numberOfSelfChampions;
        Tower* enemyTowersArray;
        int numberOfEnemyTowers;
        bool spell1LevelUpAvailable, spell2LevelUpAvailable, spell3LevelUpAvailable, spell4LevelUpAvailable;
        GenericObject* spell1LevelUp;
        GenericObject* spell2LevelUp;
        GenericObject* spell3LevelUp;
        GenericObject* spell4LevelUp;
        GenericObject* spell1LevelDotsArray;
        GenericObject* spell2LevelDotsArray;
        GenericObject* spell3LevelDotsArray;
        GenericObject* spell4LevelDotsArray;
	int numberOfSpell1Dots, numberOfSpell2Dots, numberOfSpell3Dots, numberOfSpell4Dots;
        bool spell1LevelDotsVisible, spell2LevelDotsVisible, spell3LevelDotsVisible, spell4LevelDotsVisible;
        int currentLevel;
        bool spell1ActiveAvailable, spell2ActiveAvailable, spell3ActiveAvailable, spell4ActiveAvailable;
        GenericObject* spell1Active;
        GenericObject* spell2Active;
        GenericObject* spell3Active;
        GenericObject* spell4Active;
	bool summonerSpell1ActiveAvailable, summonerSpell2ActiveAvailable;
        GenericObject* summonerSpell1Active;
        GenericObject* summonerSpell2Active;
	bool trinketActiveAvailable;
        GenericObject* trinketActive;
        bool item1ActiveAvailable, item2ActiveAvailable, item3ActiveAvailable, item4ActiveAvailable, item5ActiveAvailable, item6ActiveAvailable;
        GenericObject* item1Active;
        GenericObject* item2Active;
        GenericObject* item3Active;
        GenericObject* item4Active;
        GenericObject* item5Active;
        GenericObject* item6Active;
	bool potionActiveAvailable;
        int potionOnActive;
        GenericObject* potionActive;
        bool potionBeingUsedShown;
        GenericObject* potionBeingUsed;
        bool shopAvailableShown;
        GenericObject* shopAvailable;
        bool shopTopLeftCornerShown;
        GenericObject* shopTopLeftCorner;
        bool shopBottomLeftCornerShown;
        GenericObject* shopBottomLeftCorner;
        GenericObject* buyableItemsArray;
        int numberOfBuyableItems;
        bool mapVisible;
        GenericObject* map;
        bool mapShopVisible;
        GenericObject* mapShop;
        bool mapSelfLocationVisible;
        GenericObject* mapSelfLocation;
        bool selfHealthBarVisible;
        SelfHealth* selfHealthBar;
        bool surrenderAvailable;
        GenericObject* surrenderActive;
    }
}
