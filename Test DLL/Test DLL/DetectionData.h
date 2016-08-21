#include "Detection\Objects\Tower.h"
#include "Detection\Objects\Champion.h"
#include "Detection\Objects\Minion.h"
#include "Detection\Objects\GenericObject.h"
#include "Detection\Objects\SelfHealth.h"

#pragma once
struct DetectionDataStruct
{
	//Everything that says Array is a pointer array
	//All of the arrays and pointers in this will be data that is allocated and will have to be manually deleted.

	Minion  *allyMinionsArray = nullptr;
	int numberOfAllyMinions = 0;
	Minion *enemyMinionsArray = nullptr;
	int numberOfEnemyMinions = 0;
	Champion *allyChampionsArray = nullptr;
	int numberOfAllyChampions = 0;
	Champion *enemyChampionsArray = nullptr;
	int numberOfEnemyChampions = 0;
	Champion *selfChampionsArray = nullptr;
	int numberOfSelfChampions = 0;
	Tower *enemyTowersArray = nullptr;
	int numberOfEnemyTowers = 0;
	bool spell1LevelUpAvailable = false, spell2LevelUpAvailable = false, spell3LevelUpAvailable = false, spell4LevelUpAvailable = false;
	GenericObject* spell1LevelUp = nullptr, *spell2LevelUp = nullptr, *spell3LevelUp = nullptr, *spell4LevelUp = nullptr;
	GenericObject *spell1LevelDotsArray = nullptr, *spell2LevelDotsArray = nullptr, *spell3LevelDotsArray = nullptr, *spell4LevelDotsArray = nullptr;
	int numberOfSpell1Dots=0, numberOfSpell2Dots=0, numberOfSpell3Dots=0, numberOfSpell4Dots=0;
	bool spell1LevelDotsVisible = false, spell2LevelDotsVisible = false, spell3LevelDotsVisible = false, spell4LevelDotsVisible = false;
	int currentLevel = 0;
	bool spell1ActiveAvailable = false, spell2ActiveAvailable = false, spell3ActiveAvailable = false, spell4ActiveAvailable = false;
	GenericObject* spell1Active = nullptr, *spell2Active = nullptr, *spell3Active = nullptr, *spell4Active = nullptr;
	bool summonerSpell1ActiveAvailable = false, summonerSpell2ActiveAvailable = false;
	GenericObject* summonerSpell1Active = nullptr, *summonerSpell2Active = nullptr;
	bool trinketActiveAvailable = false;
	GenericObject* trinketActive = nullptr;
	bool item1ActiveAvailable = false, item2ActiveAvailable = false, item3ActiveAvailable = false, item4ActiveAvailable = false, item5ActiveAvailable = false, item6ActiveAvailable = false;
	GenericObject* item1Active = nullptr, *item2Active = nullptr, *item3Active = nullptr, *item4Active = nullptr, *item5Active = nullptr, *item6Active = nullptr;
	bool potionActiveAvailable = false;
	int potionOnActive = 0;
	GenericObject* potionActive = nullptr;
	bool potionBeingUsedShown = false;
	GenericObject* potionBeingUsed = nullptr;
	bool shopAvailableShown = false;
	GenericObject* shopAvailable = nullptr;
	bool shopTopLeftCornerShown = false;
	GenericObject* shopTopLeftCorner = nullptr;
	bool shopBottomLeftCornerShown = false;
	GenericObject* shopBottomLeftCorner = nullptr;
	GenericObject *buyableItemsArray = nullptr;
	int numberOfBuyableItems = 0;
	bool mapVisible = false;
	GenericObject* map = nullptr;
	bool mapShopVisible = false;
	GenericObject* mapShop = nullptr;
	bool mapSelfLocationVisible = false;
	GenericObject* mapSelfLocation = nullptr;
	bool selfHealthBarVisible = false;
	SelfHealth* selfHealthBar = nullptr;
	bool surrenderAvailable = false;
	GenericObject* surrenderActive = nullptr;
};

