//
//  AllyMinionManager.h
//  Fluffy Pug
//
//  Created by Matthew French on 5/27/15.
//  Copyright (c) 2015 Matthew French. All rights reserved.
//

#include "../../Utility.h"
#include <vector>

#ifndef AllyMinionManager_H
#define AllyMinionManager_H

class AllyMinionManager {
public:
    AllyMinionManager();
    
    static ImageData topLeftImageData, bottomLeftImageData,
    bottomRightImageData, topRightImageData, healthSegmentImageData, wardImageData;

	static void loadTopLeftImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadBottomLeftImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadBottomRightImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadTopRightImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadHealthSegmentImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadWardImageData(uint8_t * data, int imageWidth, int imageHeight);
    
    static void validateMinionBars(ImageData imageData, std::vector<Minion*>* detectedMinionBars);

    static constexpr double coloredPixelPrecision = 0.97; //0.97
    static constexpr double overalImagePrecision = 0.97; //0.97
    inline static Minion* detectMinionBarAtPixel(ImageData* imageData, uint8_t *pixel, int x, int y) {
    Minion* minion = NULL;
    //Look top left corner
    if (pixel[0] == 0 && pixel[1] == 0 && pixel[2] == 0) {
        if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData->imageWidth, imageData->imageHeight, topLeftImageData, coloredPixelPrecision) >=  overalImagePrecision) {
			int barTopLeftX = x + 1;
            int barTopLeftY = y + 1;
            minion = new Minion();
            minion->topLeft.x = barTopLeftX;
            minion->topLeft.y = barTopLeftY;
            minion->bottomLeft.x = barTopLeftX;
            minion->bottomLeft.y = barTopLeftY + 4;
            minion->topRight.x = barTopLeftX + 62;
            minion->topRight.y = barTopLeftY;
            minion->bottomRight.x = barTopLeftX + 62;
            minion->bottomRight.y = barTopLeftY + 4;
            minion->health = 0;
            minion->detectedTopLeft = true;
        } else if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData->imageWidth, imageData->imageHeight, bottomLeftImageData, coloredPixelPrecision) >=  overalImagePrecision) {
        // Look for bottom left corner
            int barTopLeftX = x + 1;
            int barTopLeftY = y - 3;
            minion = new Minion();
            minion->topLeft.x = barTopLeftX;
            minion->topLeft.y = barTopLeftY;
            minion->bottomLeft.x = barTopLeftX;
            minion->bottomLeft.y = barTopLeftY + 4;
            minion->topRight.x = barTopLeftX + 62;
            minion->topRight.y = barTopLeftY;
            minion->bottomRight.x = barTopLeftX + 62;
            minion->bottomRight.y = barTopLeftY + 4;
            minion->detectedBottomLeft = true;
            minion->health = 0;
        }
    }
    
    return minion;
}
};

#endif