//
//  TurretManager.m
//  Fluffy Pug
//
//  Created by Matthew French on 6/12/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "EnemyTowerManager.h"

ImageData EnemyTowerManager::topLeftImageData;//TODO = loadImage("Resources/Enemy Tower Health Bar/Top Left Corner.png");

ImageData EnemyTowerManager::bottomLeftImageData;//TODO = loadImage("Resources/Enemy Tower Health Bar/Bottom Left Corner.png");
ImageData EnemyTowerManager::bottomRightImageData;//TODO = loadImage("Resources/Enemy Tower Health Bar/Bottom Right Corner.png");
ImageData EnemyTowerManager::topRightImageData;//TODO = loadImage("Resources/Enemy Tower Health Bar/Top Right Corner.png");
ImageData EnemyTowerManager::healthSegmentImageData;//TODO = loadImage("Resources/Enemy Tower Health Bar/Health Segment.png");

EnemyTowerManager::EnemyTowerManager () {}

void EnemyTowerManager::loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
	topLeftImageData = makeImageData(data, imageWidth, imageHeight);
}
void EnemyTowerManager::loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight) {
	bottomLeftImageData = makeImageData(data, imageWidth, imageHeight);
}
void EnemyTowerManager::loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
	bottomRightImageData = makeImageData(data, imageWidth, imageHeight);
}
void EnemyTowerManager::loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight) {
	topRightImageData = makeImageData(data, imageWidth, imageHeight);
}
void EnemyTowerManager::loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight) {
	healthSegmentImageData = makeImageData(data, imageWidth, imageHeight);
}

//To Validate, at least 2 corners need detected then we detect the health percentage
void EnemyTowerManager::validateTowerBars(ImageData imageData, std::vector<Tower*>* detectedTowerBars) {
    //Remove duplicates
    for (size_t i = 0; i < detectedTowerBars->size(); i++) {
        Tower* tower = (*detectedTowerBars)[i];
        int detectedCorners = 1;
        for (size_t j = 0; j < detectedTowerBars->size(); j++) {
            if (j != i) {
                Tower* tower2 = (*detectedTowerBars)[j];
                if (tower2->topLeft.x == tower->topLeft.x && tower->topLeft.y == tower2-> topLeft.y) {
                    detectedTowerBars->erase(detectedTowerBars->begin() + j);
                    j--;
                    if (tower2->detectedBottomLeft) tower->detectedBottomLeft = true;
                    if (tower2->detectedBottomRight) tower->detectedBottomRight = true;
                    if (tower2->detectedTopLeft) tower->detectedTopLeft = true;
                    if (tower2->detectedTopRight) tower->detectedTopRight = true;
                    detectedCorners++;
                }
            }
        }
        if (detectedCorners < 2) {
            detectedTowerBars->erase(detectedTowerBars->begin() + i);
            i--;
        }
        tower->towerCenter.x = tower->topLeft.x+126/2; tower->towerCenter.y = tower->topLeft.y+200;
    }
    
    //Detect health
    for (size_t i = 0; i < detectedTowerBars->size(); i++) {
        Tower* tower = (*detectedTowerBars)[i];
        tower->health = 0;
        for (int x = 125; x >= 0; x--) {
            for (int y = 0; y < healthSegmentImageData.imageHeight; y++) {
                if (x + tower->topLeft.x >= 0 && x + tower->topLeft.x < imageData.imageWidth &&
                    y + tower->topLeft.y >= 0 && y + tower->topLeft.y < imageData.imageHeight) {
                uint8_t* healthBarColor = getPixel2(healthSegmentImageData, 0, y);
                uint8_t*  p = getPixel2(imageData, x + tower->topLeft.x, y + tower->topLeft.y);
                if (getColorPercentage(healthBarColor, p) >= 0.85) {
                    tower->health = (float)x / 125 * 100;
                    y = healthSegmentImageData.imageHeight + 1;
                    x = -1;
                }
                }
            }
        }
        if (tower->health == 0) {
            detectedTowerBars->erase(detectedTowerBars->begin() + i);
            i--;
        }
    }
}