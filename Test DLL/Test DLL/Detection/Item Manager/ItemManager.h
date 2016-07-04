//
//  ItemManager.h
//  Fluffy Pug
//
//  Created by Matthew French on 6/11/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "../../Utility.h"
#include <time.h>

class ItemManager {
public:
    static ImageData trinketItemImageData, itemImageData, potionImageData, usedPotionImageData, usedPotionInnerImageData;
    ItemManager();

	static void loadTrinketItemImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadItemImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadPotionImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadUsedPotionImageData(uint8_t * data, int imageWidth, int imageHeight);
	static void loadUsedPotionInnerImageData(uint8_t * data, int imageWidth, int imageHeight);
    
    static GenericObject* detectTrinketActiveAtPixel(ImageData imageData, uint8_t *pixel, int x, int y);
    static GenericObject* detectItemActiveAtPixel(ImageData imageData, uint8_t *pixel, int x, int y);
    static GenericObject* detectPotionActiveAtPixel(ImageData imageData, uint8_t *pixel, int x, int y);
    static GenericObject* detectUsedPotionAtPixel(ImageData imageData, uint8_t *pixel, int x, int y);
};