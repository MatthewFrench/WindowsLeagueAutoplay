//
//  AllyMinionManager.h
//  Fluffy Pug
//
//  Created by Matthew French on 5/27/15.
//  Copyright (c) 2015 Matthew French. All rights reserved.
//

#include "../../Utility.h"
#include <time.h>
#include <vector>

class EnemyChampionManager {
public:
    static ImageData topLeftImageData, bottomLeftImageData,
    bottomRightImageData, topRightImageData, healthSegmentImageData;
    
    EnemyChampionManager();
    static void validateChampionBars(ImageData imageData, std::vector<Champion*>* detectedChampionBars);



    inline static Champion* detectChampionBarAtPixel(ImageData* imageData, uint8_t *pixel, int x, int y) {
    Champion* champ = NULL;
    if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData->imageWidth, imageData->imageHeight, topLeftImageData, 0.8) >=  0.8) {
        int barTopLeftX = x + 3;
        int barTopLeftY = y + 3;
        champ = new Champion();
        champ->topLeft.x = barTopLeftX;
        champ->topLeft.y = barTopLeftY;
        champ->bottomLeft.x = barTopLeftX;
        champ->bottomLeft.y = barTopLeftY + 9;
        champ->topRight.x = barTopLeftX + 104;
        champ->topRight.y = barTopLeftY;
        champ->bottomRight.x = barTopLeftX + 104;
        champ->bottomRight.y = barTopLeftY + 9;
        champ->detectedTopLeft = true;
    } else if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData->imageWidth, imageData->imageHeight, bottomLeftImageData, 0.8) >=  0.8) { // Look for bottom left corner
        int barTopLeftX = x + 3;
        int barTopLeftY = y - 8;
        champ = new Champion();
        champ->topLeft.x = barTopLeftX;
        champ->topLeft.y = barTopLeftY;
        champ->bottomLeft.x = barTopLeftX;
        champ->bottomLeft.y = barTopLeftY + 9;
        champ->topRight.x = barTopLeftX + 104;
        champ->topRight.y = barTopLeftY;
        champ->bottomRight.x = barTopLeftX + 104;
        champ->bottomRight.y = barTopLeftY + 9;
        champ->detectedBottomLeft = true;
    } else if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData->imageWidth, imageData->imageHeight, topRightImageData, 0.8) >=  0.8) { // Look for top right corner
        int barTopLeftX = x - 101 - 2;
        int barTopLeftY = y + 3;
        champ = new Champion();
        champ->topLeft.x = barTopLeftX;
        champ->topLeft.y = barTopLeftY;
        champ->bottomLeft.x = barTopLeftX;
        champ->bottomLeft.y = barTopLeftY + 9;
        champ->topRight.x = barTopLeftX + 104;
        champ->topRight.y = barTopLeftY;
        champ->bottomRight.x = barTopLeftX + 104;
        champ->bottomRight.y = barTopLeftY + 9;
        champ->detectedTopRight = true;
    } else if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData->imageWidth, imageData->imageHeight, bottomRightImageData, 0.8) >=  0.8) { // Look for bottom right corner
        int barTopLeftX = x - 101 - 2;
        int barTopLeftY = y - 8;
        champ = new Champion();
        champ->topLeft.x = barTopLeftX;
        champ->topLeft.y = barTopLeftY;
        champ->bottomLeft.x = barTopLeftX;
        champ->bottomLeft.y = barTopLeftY + 9;
        champ->topRight.x = barTopLeftX + 104;
        champ->topRight.y = barTopLeftY;
        champ->bottomRight.x = barTopLeftX + 104;
        champ->bottomRight.y = barTopLeftY + 9;
        champ->detectedBottomRight = true;
    }
    return champ;
}
};