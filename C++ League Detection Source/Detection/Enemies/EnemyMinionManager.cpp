//
//  EnemyMinionManager.m
//  Fluffy Pug
//
//  Created by Matthew French on 5/27/15.
//  Copyright (c) 2015 Matthew French. All rights reserved.
//

#include "EnemyMinionManager.h"

ImageData EnemyMinionManager::topLeftImageData;//TODO = loadImage("Resources/Enemy Minion Health Bar/Top Left Corner.png");
ImageData EnemyMinionManager::bottomLeftImageData;//TODO = loadImage("Resources/Enemy Minion Health Bar/Bottom Left Corner.png");
ImageData EnemyMinionManager::bottomRightImageData;//TODO = loadImage("Resources/Enemy Minion Health Bar/Bottom Right Corner.png");
ImageData EnemyMinionManager::topRightImageData;//TODO = loadImage("Resources/Enemy Minion Health Bar/Top Right Corner.png");
ImageData EnemyMinionManager::healthSegmentImageData;//TODO = loadImage("Resources/Enemy Minion Health Bar/Health Segment.png");

EnemyMinionManager::EnemyMinionManager () {}

//To Validate, at least 2 corners need detected then we detect the health percentage

void EnemyMinionManager::validateMinionBars(ImageData imageData, std::vector<Minion*>* detectedMinionBars) {
      const double coloredPixelPrecision = 0.96; //0.97
    const double overalImagePrecision = 0.96; //0.97
    const double minionHealthMatch = 0.80; //0.87
    //Remove duplicates
    for (size_t i = 0; i < detectedMinionBars->size(); i++) {
        Minion* minion = (*detectedMinionBars)[i];
        int detectedCorners = 1;
        for (size_t j = 0; j < detectedMinionBars->size(); j++) {
            if (j != i) {
                Minion* minion2 = (*detectedMinionBars)[j];
                if (minion2->topLeft.x == minion->topLeft.x && minion->topLeft.y == minion2-> topLeft.y) {
                    detectedMinionBars->erase(detectedMinionBars->begin() + j);
                    j--;
                    if (minion2->detectedBottomLeft) minion->detectedBottomLeft = true;
                    if (minion2->detectedBottomRight) minion->detectedBottomRight = true;
                    if (minion2->detectedTopLeft) minion->detectedTopLeft = true;
                    if (minion2->detectedTopRight) minion->detectedTopRight = true;
                    if (minion2->health > minion->health) minion->health = minion2->health;
                    detectedCorners++;
                }
            }
        }
        if (detectedCorners < 2) {
            detectedMinionBars->erase(detectedMinionBars->begin() + i);
            i--;
        }
        minion->characterCenter.x = minion->topLeft.x+30; minion->characterCenter.y = minion->topLeft.y+32;
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
                        uint8_t*  p = getPixel2(imageData, x + minion->topLeft.x, y + minion->topLeft.y);
                        if (getColorPercentage(healthBarColor, p) >= minionHealthMatch) {
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
            i--;
        }
    }
    
    //Detect if ward
    //Ward is 193, 193, 193
    for (size_t i = 0; i < detectedMinionBars->size(); i++) {
        Minion* minion = (*detectedMinionBars)[i];
        bool isWard = false;
        for (int x = 61; x >= 0; x--) {
            for (int yOffset = -3; yOffset <= 1; yOffset++) {
                if (x + minion->topLeft.x >= 0 && x + minion->topLeft.x < imageData.imageWidth &&
                    yOffset + minion->topLeft.y >= 0 && yOffset + minion->topLeft.y < imageData.imageHeight) {
                    uint8_t*  p = getPixel2(imageData, x + minion->topLeft.x, yOffset + minion->topLeft.y);
                    if (isColor(p, 220, 220, 220, 45)) {
                        isWard = true;
                        x = -1;
                        yOffset = 4;
                    }
                }
            }
        }
        if (isWard) {
            detectedMinionBars->erase(detectedMinionBars->begin() + i);
            i--;
        }
    }
}