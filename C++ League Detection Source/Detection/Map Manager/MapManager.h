//
//  MapManager
//  Fluffy Pug
//
//  Created by Matthew French on 6/12/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "../../Utility.h"
#include <time.h>

class MapManager {
    
public:
    static ImageData mapTopLeftCornerImageData, shopIconImageData;/*, locationTopLeftCornerImageData, locationTopRightCornerImageData, locationBottomLeftCornerImageData, locationBottomRightCornerImageData;*/
    MapManager();
    
    static GenericObject* detectMap(ImageData imageData, uint8_t *pixel, int x, int y);
    static GenericObject* detectShop(ImageData imageData, uint8_t *pixel, int x, int y);
    static GenericObject* detectLocation(ImageData imageData, uint8_t *pixel, int x, int y);
};