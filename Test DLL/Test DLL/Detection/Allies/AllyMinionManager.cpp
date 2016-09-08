//
//  AllyMinionManager.m
//  Fluffy Pug
//
//  Created by Matthew French on 5/27/15.
//  Copyright (c) 2015 Matthew French. All rights reserved.
//

#include "AllyMinionManager.h"

ImageData AllyMinionManager::wardImageData;// = loadImage("Resources/Ward/Pink Ward.png");

ImageData AllyMinionManager::topLeftImageData;// = loadImage("Resources/Ally Minion Health Bar/Top Left Corner.png");

ImageData AllyMinionManager::bottomLeftImageData;// = loadImage("Resources/Ally Minion Health Bar/Bottom Left Corner.png");
ImageData AllyMinionManager::bottomRightImageData;// = loadImage("Resources/Ally Minion Health Bar/Bottom Right Corner.png");
ImageData AllyMinionManager::topRightImageData;// = loadImage("Resources/Ally Minion Health Bar/Top Right Corner.png");
ImageData AllyMinionManager::healthSegmentImageData;// = loadImage("Resources/Ally Minion Health Bar/Health Segment.png");

AllyMinionManager::AllyMinionManager() {}

void AllyMinionManager::loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
	topLeftImageData = makeImageData(data, imageWidth, imageHeight);
}
void AllyMinionManager::loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
	bottomLeftImageData = makeImageData(data, imageWidth, imageHeight);
}
void AllyMinionManager::loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
	bottomRightImageData = makeImageData(data, imageWidth, imageHeight);
}
void AllyMinionManager::loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
	topRightImageData = makeImageData(data, imageWidth, imageHeight);
}
void AllyMinionManager::loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight) {
	healthSegmentImageData = makeImageData(data, imageWidth, imageHeight);
}
void AllyMinionManager::loadWardImageData(uint8_t * data, int imageWidth, int imageHeight) {
	wardImageData = makeImageData(data, imageWidth, imageHeight);
}

//To Validate, at least 2 corners need detected then we detect the health percentage

void AllyMinionManager::validateMinionBars(ImageData imageData, std::vector<Minion*>* detectedMinionBars) {
	const double coloredPixelPrecision = 0.96; //0.97
	const double overalImagePrecision = 0.96; //0.97
	const double allyMinionHealthMatch = 0.80; //0.87

//Remove duplicates
	for (size_t i = 0; i < detectedMinionBars->size(); i++) {
		Minion* minion = (*detectedMinionBars)[i];
		int detectedCorners = 1;
		for (size_t j = 0; j < detectedMinionBars->size(); j++) {
			if (j != i) {
				Minion* minion2 = (*detectedMinionBars)[j];
				if (minion2->topLeft.x == minion->topLeft.x && minion->topLeft.y == minion2->topLeft.y) {
					detectedMinionBars->erase(detectedMinionBars->begin() + j);
					j--;
					if (minion2->detectedBottomLeft) minion->detectedBottomLeft = true;
					if (minion2->detectedBottomRight) minion->detectedBottomRight = true;
					if (minion2->detectedTopLeft) minion->detectedTopLeft = true;
					if (minion2->detectedTopRight) minion->detectedTopRight = true;
					if (minion2->health > minion->health) minion->health = minion2->health;
					detectedCorners++;
					delete minion2;
				}
			}
		}
		if (detectedCorners < 2) {
			detectedMinionBars->erase(detectedMinionBars->begin() + i);
			delete minion;
			i--;
		}
		else {
			minion->characterCenter.x = minion->topLeft.x + 30; minion->characterCenter.y = minion->topLeft.y + 32;
		}
	}

	//Detect health

	for (size_t i = 0; i < detectedMinionBars->size(); i++) {

		Minion* minion = (*detectedMinionBars)[i];
		if (minion->health == 0) {
			for (int x = 61; x >= 0; x--) {
				for (int y = 0; y < healthSegmentImageData.imageHeight; y++) {
					if (x + minion->topLeft.x >= 0 && x + minion->topLeft.x < imageData.imageWidth &&
						y + minion->topLeft.y >= 0 && y + minion->topLeft.y < imageData.imageHeight) {
						uint8_t* healthBarColor = getPixel2(healthSegmentImageData, 0, y);
						uint8_t* p = getPixel2(imageData, x + minion->topLeft.x, y + minion->topLeft.y);
						if (getColorPercentage(healthBarColor, p) >= allyMinionHealthMatch) {
							minion->health = (float)x / 61 * 100;
							y = healthSegmentImageData.imageHeight + 1;
							x = -1;
						}
					}
				}
			}
		}
		if (minion->health == 0) { //Not a minion
			detectedMinionBars->erase(detectedMinionBars->begin() + i);
			delete minion;
			i--;
		}
		else {
			int minionBarSize = 1;
			for (int x = minionBarSize; x <= 61; x++) {
				for (int y = 0; y < healthSegmentImageData.imageHeight; y++) {
					if (x + minion->topLeft.x >= 0 && x + minion->topLeft.x < imageData.imageWidth &&
						y + minion->topLeft.y >= 0 && y + minion->topLeft.y < imageData.imageHeight) {
						uint8_t* healthBarColor = getPixel2(healthSegmentImageData, 0, y);
						uint8_t* p = getPixel2(imageData, x + minion->topLeft.x, y + minion->topLeft.y);
						if (getColorPercentage(healthBarColor, p) < allyMinionHealthMatch) {
							y = healthSegmentImageData.imageHeight + 1;
							minionBarSize = x - 1;
							x = 62;
						}
					}
				}
			}

			//Detect if ward
			//Ward is 136, 136, 136
			bool isWard = false;

			if (minionBarSize == 12 || minionBarSize == 13 || minionBarSize == 14 || minionBarSize == 18 || minionBarSize == 17) {
				//19 pixels to the right

				if (minion->topLeft.x + minionBarSize + 2 < imageData.imageWidth) {
					int wardNum = 0;
					for (int y = 0; y < 4; y++) {
						uint8_t* rightSide1 = getPixel2(imageData, minion->topLeft.x + minionBarSize + 1, minion->topLeft.y + y);
						uint8_t* rightSide2 = getPixel2(imageData, minion->topLeft.x + minionBarSize + 2, minion->topLeft.y + y);
						if (rightSide1[0] == 0 && rightSide1[1] == 0 && rightSide1[2] == 0 &&
							rightSide2[0] == 0 && rightSide2[1] == 0 && rightSide2[2] == 0) {
							wardNum++;
							//printf("Ward has two bars on the right\n");
						}
					}
					if (wardNum == 4) isWard = true;

				}
				if (imageData, minion->topLeft.x - 2 > 0) {
					int wardNum = 0;
					for (int y = 0; y < 4; y++) {
						uint8_t* leftSide1 = getPixel2(imageData, minion->topLeft.x - 1, minion->topLeft.y + y);
						uint8_t* leftSide2 = getPixel2(imageData, minion->topLeft.x - 2, minion->topLeft.y + y);
						if (leftSide1[0] == 0 && leftSide1[1] == 0 && leftSide1[2] == 0 &&
							leftSide2[0] == 0 && leftSide2[1] == 0 && leftSide2[2] == 0) {
							wardNum++;
							//isWard = true;
							//printf("Ward has two bars on the left\n");
						}
					}
					if (wardNum == 4) isWard = true;

				}
			}
			else {
				//If there is ward white on top, ignore
				//Reksai tunnel detection
				if (minion->topLeft.y - 3 >= 0) {
					uint8_t* top1 = getPixel2(imageData, minion->topLeft.x, minion->topLeft.y - 2);
					uint8_t* top2 = getPixel2(imageData, minion->topLeft.x, minion->topLeft.y - 3);
					if (top1[0] == 136 && top1[1] == 136 && top1[2] == 136 &&
						top2[0] == 136 && top2[1] == 136 && top2[2] == 136) {
						isWard = true;
					}
				}

			}


			if (isWard) {
				detectedMinionBars->erase(detectedMinionBars->begin() + i);
				delete minion;
				i--;
			}
		}
	}
}