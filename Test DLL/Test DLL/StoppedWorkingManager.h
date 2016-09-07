#include "Utility.h"
#include <time.h>

class StoppedWorkingManager {
public:
	static ImageData continueImageData;

	static void loadStoppedWorkingImageData(uint8_t * data, int imageWidth, int imageHeight);

	static GenericObject* detectStoppedWorkingAtPixel(ImageData imageData, uint8_t *pixel, int x, int y);
};