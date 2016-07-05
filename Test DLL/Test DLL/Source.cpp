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
		printf("\t%zu ally minions\n", detection->getAllyMinions()->size());
	}
	if (detection->getAllyChampions()->size() > 0) {
		printf("\t%zu ally champions\n", detection->getAllyChampions()->size());
	}
	if (detection->getSelfChampions()->size() > 0) {
		printf("\t%zu self champions\n", detection->getSelfChampions()->size());
	}
	if (detection->getEnemyMinions()->size() > 0) {
		printf("\t%zu enemy minions\n", detection->getEnemyMinions()->size());
	}
	if (detection->getEnemyChampions()->size() > 0) {
		printf("\t%zu enemy champions\n", detection->getEnemyChampions()->size());
	}
	if (detection->getEnemyTowers()->size() > 0) {
		printf("\t%zu enemy towers\n", detection->getEnemyTowers()->size());
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
		printf("\tBuyable items: %zu\n", detection->getBuyableItems()->size());
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

	__declspec(dllexport) void processDetection(byte* dataPointer, int32_t width, int32_t height) {
		printf("Processing Detection\n");
		ImageData imageData = makeImageData(dataPointer, width, height);
		detectionManager->processDetection(&imageData);
		printf("Detection Processed\n");

		printDetected(detectionManager);
	}

	// Ability image loading code
	__declspec(dllexport) void AbilityManager_loadLevelUpImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		AbilityManager::loadLevelUpImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadLevelDotImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		AbilityManager::loadLevelDotImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadLevelUpDisabledImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		AbilityManager::loadLevelUpDisabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadAbilityEnabledImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		AbilityManager::loadAbilityEnabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadAbilityDisabledImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		AbilityManager::loadAbilityDisabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AbilityManager_loadEnabledSummonerSpellImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		AbilityManager::loadEnabledSummonerSpellImageData(data, imageWidth, imageHeight);
	}
	//Ally image loading code
	__declspec(dllexport) void AllyChampionManager_loadTopLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyChampionManager::loadTopLeftImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void AllyChampionManager_loadBottomLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyChampionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyChampionManager_loadBottomRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyChampionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyChampionManager_loadTopRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyChampionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyChampionManager_loadHealthSegmentImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyChampionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadTopLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyMinionManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadBottomLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyMinionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadBottomRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyMinionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadTopRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyMinionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadHealthSegmentImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyMinionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void AllyMinionManager_loadWardImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  AllyMinionManager::loadWardImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadTopLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SelfChampionManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SelfChampionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SelfChampionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadTopRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SelfChampionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadHealthSegmentImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SelfChampionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomBarLeftSideImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SelfChampionManager::loadBottomBarLeftSideImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomBarRightSideImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SelfChampionManager::loadBottomBarRightSideImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void SelfChampionManager_loadBottomBarAverageHealthColorImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SelfChampionManager::loadBottomBarAverageHealthColorImageData(data, imageWidth, imageHeight);
	}
		//Enemy image loading code

	__declspec(dllexport) void EnemyChampionManager_loadTopLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyChampionManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyChampionManager_loadBottomLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyChampionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyChampionManager_loadBottomRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyChampionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyChampionManager_loadTopRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyChampionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyChampionManager_loadHealthSegmentImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyChampionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadTopLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyMinionManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadBottomLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyMinionManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadBottomRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyMinionManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadTopRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyMinionManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyMinionManager_loadHealthSegmentImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyMinionManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadTopLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyTowerManager::loadTopLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadBottomLeftImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyTowerManager::loadBottomLeftImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadBottomRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyTowerManager::loadBottomRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadTopRightImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyTowerManager::loadTopRightImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void EnemyTowerManager_loadHealthSegmentImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  EnemyTowerManager::loadHealthSegmentImageData(data, imageWidth, imageHeight);
	}
		//Item image loading code

	__declspec(dllexport) void ItemManager_loadTrinketItemImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ItemManager::loadTrinketItemImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ItemManager_loadItemImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ItemManager::loadItemImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ItemManager_loadPotionImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ItemManager::loadPotionImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ItemManager_loadUsedPotionImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ItemManager::loadUsedPotionImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ItemManager_loadUsedPotionInnerImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ItemManager::loadUsedPotionInnerImageData(data, imageWidth, imageHeight);
	}
		//Map image loading code

	__declspec(dllexport) void MapManager_loadMapTopLeftCornerImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  MapManager::loadMapTopLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void MapManager_loadShopIconImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  MapManager::loadShopIconImageData(data, imageWidth, imageHeight);
	}
		//Surrender image loading code
	__declspec(dllexport) void SurrenderManager_loadSurrenderImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  SurrenderManager::loadSurrenderImageData(data, imageWidth, imageHeight);
	}
		//Shop image loading code

	__declspec(dllexport) void ShopManager_loadShopTopLeftCornerImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ShopManager::loadShopTopLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopAvailableImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ShopManager::loadShopAvailableImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBottomLeftCornerImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ShopManager::loadShopBottomLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBuyableItemTopLeftCornerImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ShopManager::loadShopBuyableItemTopLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBuyableItemBottomLeftCornerImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ShopManager::loadShopBuyableItemBottomLeftCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBuyableItemTopRightCornerImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
		  ShopManager::loadShopBuyableItemTopRightCornerImageData(data, imageWidth, imageHeight);

	}
	__declspec(dllexport) void ShopManager_loadShopBuyableItemBottomRightCornerImageData(byte * data, int32_t imageWidth, int32_t imageHeight) {
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

	__declspec(dllexport) void Copy(byte* startPointer, byte* startDestinationPointer, int32_t width, int32_t height) {
		const int32_t channels = 4;
		const int32_t r_channel = 2;
		const int32_t g_channel = 1;
		const int32_t b_channel = 0;
		const int32_t a_channel = 3;

		int32_t loc = 0;

		for (int32_t y = 0; y < height; y++)
		{

			//byte* upToRow = startPointer + y * width * channels;
			//byte* destinationUpToRow = startDestinationPointer + y * width * channels;

			for (int32_t x = 0; x < width; x++)
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