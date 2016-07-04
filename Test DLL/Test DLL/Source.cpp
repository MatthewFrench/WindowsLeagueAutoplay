#include <stdio.h>
#include <iostream>

#include "Utility.h"
#include "Detection/DetectionManager.h"
#include "Detection/Ability Manager/AbilityManager.h"
#include "Detection/Allies/AllyChampionManager.h"
#include "Detection/Allies/AllyMinionManager.h"
#include "Detection/Allies/SelfChampionManager.h"
#include "Detection/Enemies/EnemyChampionManager.h"
#include "Detection/Enemies/EnemyMinionManager.h"
#include "Detection/Enemies/EnemyTowerManager.h"
#include "Detection/Item Manager/ItemManager.h"
#include "Detection/Map Manager/MapManager.h"
#include "Detection/Surrender Manager/SurrenderManager.h"
#include "Detection/Shop Manager/ShopManager.h"

static DetectionManager* detectionManager = nullptr;


void printDetected(DetectionManager* detection) {
	printf("Detected: \n");
	if (detection->getAllyMinions()->size() > 0) {
		printf("\t%I64u ally minions\n", detection->getAllyMinions()->size());
	}
	if (detection->getAllyChampions()->size() > 0) {
		printf("\t%I64u ally champions\n", detection->getAllyChampions()->size());
	}
	if (detection->getSelfChampions()->size() > 0) {
		printf("\t%I64u self champions\n", detection->getSelfChampions()->size());
	}
	if (detection->getEnemyMinions()->size() > 0) {
		printf("\t%I64u enemy minions\n", detection->getEnemyMinions()->size());
	}
	if (detection->getEnemyChampions()->size() > 0) {
		printf("\t%I64u enemy champions\n", detection->getEnemyChampions()->size());
	}
	if (detection->getEnemyTowers()->size() > 0) {
		printf("\t%I64u enemy towers\n", detection->getEnemyTowers()->size());
	}
	if (detection->getSelfHealthBarVisible()) {
		printf("\tCan see self health bar\n");
		printf("\tSelf health: %f\n", detection->getSelfHealthBar()->health);
	}
	if (detection->getSpell1LevelUpVisible()) {
		printf("\tLevel up spell 1 available\n");
	}
	if (detection->getSpell2LevelUpVisible()) {
		printf("\tLevel up spell 2 available\n");
	}
	if (detection->getSpell3LevelUpVisible()) {
		printf("\tLevel up spell 3 available\n");
	}
	if (detection->getSpell4LevelUpVisible()) {
		printf("\tLevel up spell 4 available\n");
	}
	if (detection->getCurrentLevel() > 0) {
		printf("\tDetected current level: %d\n", detection->getCurrentLevel());
	}
	if (detection->getSpell1Available()) {
		printf("\tSpell 1 available\n");
	}
	if (detection->getSpell2Available()) {
		printf("\tSpell 2 available\n");
	}
	if (detection->getSpell3Available()) {
		printf("\tSpell 3 available\n");
	}
	if (detection->getSpell4Available()) {
		printf("\tSpell 4 available\n");
	}
	if (detection->getSummonerSpell1Available()) {
		printf("\tSummoner spell 1 available\n");
	}
	if (detection->getSummonerSpell2Available()) {
		printf("\tSummoner spell 2 available\n");
	}
	if (detection->getTrinketActiveAvailable()) {
		printf("\tTrinket active available\n");
	}
	if (detection->getItem1ActiveAvailable()) {
		printf("\tItem 1 active available\n");
	}
	if (detection->getItem2ActiveAvailable()) {
		printf("\tItem 2 active available\n");
	}
	if (detection->getItem3ActiveAvailable()) {
		printf("\tItem 3 active available\n");
	}
	if (detection->getItem4ActiveAvailable()) {
		printf("\tItem 4 active available\n");
	}
	if (detection->getItem5ActiveAvailable()) {
		printf("\tItem 5 active available\n");
	}
	if (detection->getItem6ActiveAvailable()) {
		printf("\tItem 6 active available\n");
	}
	if (detection->getPotionActiveAvailable()) {
		printf("\tPotion active available\n");

		printf("\t\tPotion in slot %d\n", detection->getPotionActiveItemSlot());
	}
	if (detection->getPotionBeingUsedVisible()) {
		printf("\tPotion being used\n");
	}
	if (detection->getShopAvailable()) {
		printf("\tShop is available\n");
	}
	if (detection->getShopTopLeftCornerVisible()) {
		printf("\tShop top left corner is visible\n");
	}
	if (detection->getShopBottomLeftCornerVisible()) {
		printf("\tShop bottom left corner is visible\n");
	}
	if (detection->getBuyableItems()->size() > 0) {
		printf("\tBuyable items: %I64u\n", detection->getBuyableItems()->size());
	}
	if (detection->getMapVisible()) {
		printf("\tMap is visible\n");
	}
	if (detection->getMapShopVisible()) {
		printf("\tShop on map is visible\n");
	}
	if (detection->getMapLocationVisible()) {
		printf("\tLocation on map is visible\n");
	}
	if (detection->getSurrenderAvailable()) {
		printf("\tSurrender is visible\n");
	}
}

extern "C"
{
	__declspec(dllexport) void initializeDetectionManager() {
		AllocConsole();
		FILE *fptr;
		freopen_s(&fptr, "CONOUT$", "w", stdout);
		detectionManager = new DetectionManager();
		printf("Detection Manager Initialized\n");
	}

	__declspec(dllexport) void processDetection(byte* dataPointer, int width, int height) {
		printf("Processing Detection\n");
		ImageData imageData = makeImageData(dataPointer, width, height);
		detectionManager->processDetection(&imageData);
		printf("Detection Processed\n");

		printDetected(detectionManager);
	}

	// Ability image loading code
	__declspec(dllexport) void AbilityManager_loadLevelUpImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadLevelUpImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadLevelDotImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadLevelDotImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadLevelUpDisabledImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadLevelUpDisabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadAbilityEnabledImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadAbilityEnabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadAbilityDisabledImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadAbilityDisabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadEnabledSummonerSpellImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadEnabledSummonerSpellImageData(data, imageWidth, imageHeight);
	}
	//Ally image loading code
	__declspec(dllexport) void AllyChampionManager_loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyChampionManager::loadTopLeftImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AllyChampionManager_loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyChampionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyChampionManager_loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyChampionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyChampionManager_loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyChampionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyChampionManager_loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyChampionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyMinionManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyMinionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyMinionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyMinionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyMinionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadWardImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  AllyMinionManager::loadWardImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SelfChampionManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SelfChampionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SelfChampionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SelfChampionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SelfChampionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomBarLeftSideImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SelfChampionManager::loadBottomBarLeftSideImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomBarRightSideImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SelfChampionManager::loadBottomBarRightSideImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomBarAverageHealthColorImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SelfChampionManager::loadBottomBarAverageHealthColorImageData(data, imageWidth, imageHeight);
	}
		//Enemy image loading code

	__declspec(dllexport) void EnemyChampionManager_loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyChampionManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyChampionManager_loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyChampionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyChampionManager_loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyChampionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyChampionManager_loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyChampionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyChampionManager_loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyChampionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyMinionManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyMinionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyMinionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyMinionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyMinionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyTowerManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyTowerManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyTowerManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyTowerManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  EnemyTowerManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);
	}
		//Item image loading code

	__declspec(dllexport) void ItemManager_loadTrinketItemImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ItemManager::loadTrinketItemImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ItemManager_loadItemImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ItemManager::loadItemImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ItemManager_loadPotionImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ItemManager::loadPotionImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ItemManager_loadUsedPotionImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ItemManager::loadUsedPotionImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ItemManager_loadUsedPotionInnerImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ItemManager::loadUsedPotionInnerImageData(data, imageWidth, imageHeight);
	}
		//Map image loading code

	__declspec(dllexport) void MapManager_loadMapTopLeftCornerImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  MapManager::loadMapTopLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void MapManager_loadShopIconImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  MapManager::loadShopIconImageData(data, imageWidth, imageHeight);
	}
		//Surrender image loading code
	__declspec(dllexport) void SurrenderManager_loadSurrenderImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  SurrenderManager::loadSurrenderImageData(data, imageWidth, imageHeight);
	}
		//Shop image loading code

	__declspec(dllexport) void ShopManager_loadShopTopLeftCornerImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ShopManager::loadShopTopLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopAvailableImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ShopManager::loadShopAvailableImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBottomLeftCornerImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ShopManager::loadShopBottomLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBuyableItemTopLeftCornerImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ShopManager::loadShopBuyableItemTopLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBuyableItemBottomLeftCornerImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ShopManager::loadShopBuyableItemBottomLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBuyableItemTopRightCornerImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ShopManager::loadShopBuyableItemTopRightCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBuyableItemBottomRightCornerImageData(uint8_t * data, int imageWidth, int imageHeight) {
		  ShopManager::loadShopBuyableItemBottomRightCornerImageData(data, imageWidth, imageHeight);
	}

	__declspec(dllexport) void DisplayHelloFromDLL()
	{
		printf("Hello from DLL !\n");
	}
	__declspec(dllexport) double Add(double a, double b)
	{
		return a + b;
	}

	typedef unsigned char byte;

	__declspec(dllexport) void Copy(byte* startPointer, byte* startDestinationPointer, int width, int height) {
		const int channels = 4;
		const int r_channel = 2;
		const int g_channel = 1;
		const int b_channel = 0;
		const int a_channel = 3;

		int loc = 0;

		for (int y = 0; y < height; y++)
		{

			//byte* upToRow = startPointer + y * width * channels;
			//byte* destinationUpToRow = startDestinationPointer + y * width * channels;

			for (int x = 0; x < width; x++)
			{
				
				startDestinationPointer[loc + b_channel] = startPointer[loc + b_channel];
				startDestinationPointer[loc + g_channel] = startPointer[loc + g_channel];
				startDestinationPointer[loc + r_channel] = startPointer[loc + r_channel];
				startDestinationPointer[loc + a_channel] = startPointer[loc + a_channel];

				loc += 4;
				
				/*
				byte* pixel = upToRow + x * channels;
				byte* destinationPixel = destinationUpToRow + x * channels;


				destinationPixel[b_channel] = pixel[b_channel];
				destinationPixel[g_channel] = pixel[g_channel];
				destinationPixel[r_channel] = pixel[r_channel];
				destinationPixel[a_channel] = pixel[a_channel];
				*/
			}

		}
	}
}