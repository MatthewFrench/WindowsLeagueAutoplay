//
//  ShopManager.h
//  Fluffy Pug
//
//  Created by Matthew French on 6/12/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "../../Utility.h"
#include <time.h>

class ShopManager {
public:
    static ImageData shopTopLeftCornerImageData, shopAvailableImageData, shopBottomLeftCornerImageData, shopBuyableItemTopLeftCornerImageData, shopBuyableItemBottomLeftCornerImageData, shopBuyableItemTopRightCornerImageData, shopBuyableItemBottomRightCornerImageData;
    ShopManager();
    static GenericObject* detectShopAvailable(ImageData imageData, uint8_t *pixel, int x, int y);
    inline static GenericObject* detectShopTopLeftCorner(ImageData* imageData, uint8_t *pixel, int x, int y);
    static GenericObject* detectShopBottomLeftCorner(ImageData imageData, uint8_t *pixel, int x, int y);
    static GenericObject* detectBuyableItems(ImageData imageData, uint8_t *pixel, int x, int y);
};

inline GenericObject* ShopManager::detectShopTopLeftCorner(ImageData* imageData, uint8_t *pixel, int x, int y) {
    GenericObject* object = NULL;
    if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData->imageWidth, imageData->imageHeight, shopTopLeftCornerImageData, 0.4) >=  0.8) {
        object = new GenericObject();
        object->topLeft.x = x;
        object->topLeft.y = y;
        object->bottomLeft.x = x;
        object->bottomLeft.y = y + shopTopLeftCornerImageData.imageHeight;
        object->topRight.x = x + shopTopLeftCornerImageData.imageWidth;
        object->topRight.y = y;
        object->bottomRight.x = x + shopTopLeftCornerImageData.imageWidth;
        object->bottomRight.y = y + shopTopLeftCornerImageData.imageHeight;
        object->center.x = (object->topRight.x - object->topLeft.x) / 2 + object->topLeft.x;
        object->center.y = (object->bottomLeft.y - object->topLeft.y) / 2 + object->topLeft.y;
    }
    
    return object;
}