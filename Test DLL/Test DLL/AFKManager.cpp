#include "AFKManager.h"

ImageData AFKManager::continueImageData;//TODO = loadImage("Resources/AFK/AFK.png");

void AFKManager::loadAFKImageData(uint8_t * data, int imageWidth, int imageHeight) {
	continueImageData = makeImageData(data, imageWidth, imageHeight);
}

GenericObject* AFKManager::detectAFKAtPixel(ImageData imageData, uint8_t *pixel, int x, int y) {
	GenericObject* object = NULL;
	if (getImageAtPixelPercentageOptimizedExact(pixel, x, y, imageData.imageWidth, imageData.imageHeight, continueImageData, 0.9) >= 0.9) {
		object = new GenericObject();
		object->topLeft.x = x;
		object->topLeft.y = y;
		object->bottomLeft.x = x;
		object->bottomLeft.y = y + continueImageData.imageHeight;
		object->topRight.x = x + continueImageData.imageWidth;
		object->topRight.y = y;
		object->bottomRight.x = x + continueImageData.imageWidth;
		object->bottomRight.y = y + continueImageData.imageHeight;
		object->center.x = (object->topRight.x - object->topLeft.x) / 2 + object->topLeft.x;
		object->center.y = (object->bottomLeft.y - object->topLeft.y) / 2 + object->topLeft.y;
	}

	return object;
}