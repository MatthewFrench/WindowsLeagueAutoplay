struct MinionStruct {

};
struct ChampionStruct {

};
struct TowerStruct {

};
struct GenericObjectStruct {

};
struct SelfHealthStruct {

};

#pragma once
struct DetectionDataStruct
{
	//Everything that says Array is a pointer array

	MinionStruct  *allyMinionsArray;
	int numberOfAllyMinions;
	MinionStruct *enemyMinionsArray;
	int numberOfEnemyMinions;
	ChampionStruct *allyChampionsArray;
	int numberOfAllyChampions;
	ChampionStruct *enemyChampionsArray;
	int numberOfEnemyChampions;
	ChampionStruct *selfChampionsArray;
	int numberOfSelfChampions;
	TowerStruct *enemyTowersArray;
	int numberOfEnemyTowers;
	bool spell1LevelUpAvailable = false, spell2LevelUpAvailable = false, spell3LevelUpAvailable = false, spell4LevelUpAvailable = false;
	GenericObjectStruct* spell1LevelUp = nullptr, *spell2LevelUp = nullptr, *spell3LevelUp = nullptr, *spell4LevelUp = nullptr;
	GenericObjectStruct *spell1LevelDotsArray, *spell2LevelDotsArray, *spell3LevelDotsArray, *spell4LevelDotsArray;
	int numberOfSpell1Dots, numberOfSpell2Dots, numberOfSpell3Dots, numberOfSpell4Dots;
	bool spell1LevelDotsVisible = false, spell2LevelDotsVisible = false, spell3LevelDotsVisible = false, spell4LevelDotsVisible = false;
	int currentLevel = 0;
	bool spell1ActiveAvailable = false, spell2ActiveAvailable = false, spell3ActiveAvailable = false, spell4ActiveAvailable = false;
	GenericObjectStruct* spell1Active = nullptr, *spell2Active = nullptr, *spell3Active = nullptr, *spell4Active = nullptr;
	bool summonerSpell1ActiveAvailable = false, summonerSpell2ActiveAvailable = false;
	GenericObjectStruct* summonerSpell1Active = nullptr, *summonerSpell2Active = nullptr;
	bool trinketActiveAvailable = false;
	GenericObjectStruct* trinketActive = nullptr;
	bool item1ActiveAvailable = false, item2ActiveAvailable = false, item3ActiveAvailable = false, item4ActiveAvailable = false, item5ActiveAvailable = false, item6ActiveAvailable = false;
	GenericObjectStruct* item1Active = nullptr, *item2Active = nullptr, *item3Active = nullptr, *item4Active = nullptr, *item5Active = nullptr, *item6Active = nullptr;
	bool potionActiveAvailable = false;
	int potionOnActive;
	GenericObjectStruct* potionActive = nullptr;
	bool potionBeingUsedShown = false;
	GenericObjectStruct* potionBeingUsed = nullptr;
	bool shopAvailableShown = false;
	GenericObjectStruct* shopAvailable = nullptr;
	bool shopTopLeftCornerShown = false;
	GenericObjectStruct* shopTopLeftCorner = nullptr;
	bool shopBottomLeftCornerShown = false;
	GenericObjectStruct* shopBottomLeftCorner = nullptr;
	GenericObjectStruct *buyableItemsArray;
	int numberOfBuyableItems;
	bool mapVisible = false;
	GenericObjectStruct* map = nullptr;
	bool mapShopVisible = false;
	GenericObjectStruct* mapShop = nullptr;
	bool mapSelfLocationVisible = false;
	GenericObjectStruct* mapSelfLocation = nullptr;
	bool selfHealthBarVisible = false;
	SelfHealthStruct* selfHealthBar = nullptr;
	bool surrenderAvailable = false;
	GenericObjectStruct* surrenderActive = nullptr;
};

