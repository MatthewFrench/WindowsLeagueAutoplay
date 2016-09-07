#include "Utility.h"
#include <time.h>

class ContinueManager {
public:
	static ImageData continueImageData;

	static void loadContinueImageData(uint8_t * data, int imageWidth, int imageHeight);

	static GenericObject* detectContinueAtPixel(ImageData imageData, uint8_t *pixel, int x, int y);
};