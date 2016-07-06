//
//  ItemManager.m
//  Fluffy Pug
//
//  Created by Matthew French on 6/11/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "ItemManager.h"

ImageData ItemManager::trinketItemImageData;//TODO = loadImage("Resources/Item Bar/Trinket Active.png");
ImageData ItemManager::itemImageData;//TODO = loadImage("Resources/Item Bar/Usable Item.png");
ImageData ItemManager::potionImageData;//TODO = loadImage("Resources/Item Bar/Potion.png");
ImageData ItemManager::usedPotionImageData;//TODO = loadImage("Resources/Item Bar/Used Potion.png");
ImageData ItemManager::usedPotionInnerImageData;//TODO = loadImage("Resources/Item Bar/Used Potion Inner.png");

ItemManager::ItemManager() {}

void ItemManager::loadTrinketItemImageData(uint8_t * data, int imageWidth, int imageHeight) {
	trinketItemImageData = makeImageData(data, imageWidth, imageHeight);
 }
 void ItemManager::loadItemImageData(uint8_t * data, int imageWidth, int imageHeight) {
	 itemImageData = makeImageData(data, imageWidth, imageHeight);
 }
 void ItemManager::loadPotionImageData(uint8_t * data, int imageWidth, int imageHeight) {
	 potionImageData = makeImageData(data, imageWidth, imageHeight);
 }
 void ItemManager::loadUsedPotionImageData(uint8_t * data, int imageWidth, int imageHeight) {
	 usedPotionImageData = makeImageData(data, imageWidth, imageHeight);
 }
 void ItemManager::loadUsedPotionInnerImageData(uint8_t * data, int imageWidth, int imageHeight) {
	 usedPotionInnerImageData = makeImageData(data, imageWidth, imageHeight);
 }

GenericObject* ItemManager::detectTrinketActiveAtPixel(ImageData imageData, uint8_t *pixel, int x, int y) {
    GenericObject* object = NULL;
    if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData.imageWidth, imageData.imageHeight, trinketItemImageData, 0.7) >=  0.7) {
        object = new GenericObject();
        object->topLeft.x = x;
        object->topLeft.y = y;
        object->bottomLeft.x = x;
        object->bottomLeft.y = y + trinketItemImageData.imageHeight;
        object->topRight.x = x + trinketItemImageData.imageWidth;
        object->topRight.y = y;
        object->bottomRight.x = x + trinketItemImageData.imageWidth;
        object->bottomRight.y = y + trinketItemImageData.imageHeight;
        object->center.x = (object->topRight.x - object->topLeft.x) / 2 + object->topLeft.x;
        object->center.y = (object->bottomLeft.y - object->topLeft.y) / 2 + object->topLeft.y;
    }
    
    return object;
}
GenericObject* ItemManager::detectItemActiveAtPixel(ImageData imageData, uint8_t *pixel, int x, int y) {
    GenericObject* object = NULL;
    if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData.imageWidth, imageData.imageHeight, itemImageData, 0.65) >=  0.65) {
        object = new GenericObject();
        object->topLeft.x = x;
        object->topLeft.y = y;
        object->bottomLeft.x = x;
        object->bottomLeft.y = y + itemImageData.imageHeight;
        object->topRight.x = x + itemImageData.imageWidth;
        object->topRight.y = y;
        object->bottomRight.x = x + itemImageData.imageWidth;
        object->bottomRight.y = y + itemImageData.imageHeight;
        object->center.x = (object->topRight.x - object->topLeft.x) / 2 + object->topLeft.x;
        object->center.y = (object->bottomLeft.y - object->topLeft.y) / 2 + object->topLeft.y;
    }
    
    return object;
}
GenericObject* ItemManager::detectPotionActiveAtPixel(ImageData imageData, uint8_t *pixel, int x, int y) {
    GenericObject* object = NULL;
    if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData.imageWidth, imageData.imageHeight, potionImageData, 0.7) >=  0.7) {
        object = new GenericObject();
        object->topLeft.x = x;
        object->topLeft.y = y;
        object->bottomLeft.x = x;
        object->bottomLeft.y = y + potionImageData.imageHeight;
        object->topRight.x = x + potionImageData.imageWidth;
        object->topRight.y = y;
        object->bottomRight.x = x + potionImageData.imageWidth;
        object->bottomRight.y = y + potionImageData.imageHeight;
        object->center.x = (object->topRight.x - object->topLeft.x) / 2 + object->topLeft.x;
        object->center.y = (object->bottomLeft.y - object->topLeft.y) / 2 + object->topLeft.y;
    }
    
    return object;
}
GenericObject* ItemManager::detectUsedPotionAtPixel(ImageData imageData, uint8_t *pixel, int x, int y) {
    GenericObject* object = NULL;
    if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData.imageWidth, imageData.imageHeight, usedPotionImageData, 0.8) >=  0.8) {
        
        //Now test if we have at least 50% of the inside somewhat matching the inner potion
        if (getImageAtPixelPercentageOptimized(getPixel2(imageData, x+1, y+1), x+1, y+1, imageData.imageWidth, imageData.imageHeight, usedPotionInnerImageData, 0.5) >= 0.5) {
        
            object = new GenericObject();
            object->topLeft.x = x;
            object->topLeft.y = y;
            object->bottomLeft.x = x;
            object->bottomLeft.y = y + usedPotionImageData.imageHeight;
            object->topRight.x = x + usedPotionImageData.imageWidth;
            object->topRight.y = y;
            object->bottomRight.x = x + usedPotionImageData.imageWidth;
            object->bottomRight.y = y + usedPotionImageData.imageHeight;
            object->center.x = (object->topRight.x - object->topLeft.x) / 2 + object->topLeft.x;
            object->center.y = (object->bottomLeft.y - object->topLeft.y) / 2 + object->topLeft.y;
        
        }
    }
    
    return object;
}