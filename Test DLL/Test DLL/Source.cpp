#include <stdio.h>
#include <iostream>

#include "Utility.h"
#include "Detection/DetectionManager.h"
#include "Detection/Ability Manager/AbilityManager.h"

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

	__declspec(dllexport) void loadAbilityManagerLevelUpImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadLevelUpImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void loadAbilityManagerLevelDotImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadLevelDotImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void loadAbilityManagerLevelUpDisabledImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadLevelUpDisabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void loadAbilityManagerAbilityEnabledImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadAbilityEnabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void loadAbilityManagerAbilityDisabledImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadAbilityDisabledImageData(data, imageWidth, imageHeight);
	}
	__declspec(dllexport) void loadAbilityManagerEnabledSummonerSpellImageData(uint8_t * data, int imageWidth, int imageHeight) {
		AbilityManager::loadEnabledSummonerSpellImageData(data, imageWidth, imageHeight);
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