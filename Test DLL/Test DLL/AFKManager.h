#include "Utility.h"
#include <time.h>

class AFKManager {
public:
	static ImageData continueImageData;

	static void loadAFKImageData(uint8_t * data, int imageWidth, int imageHeight);

	static GenericObject* detectAFKAtPixel(ImageData imageData, uint8_t *pixel, int x, int y);
};