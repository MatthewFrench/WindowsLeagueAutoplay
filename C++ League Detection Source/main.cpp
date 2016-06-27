#define cimg_display 0
#define cimg_use_png
#include "lib/CImg.h"
#include "Utility.h"
#include "Detection/DetectionManager.h"

using namespace cimg_library; 

double runTestAndOutput(string folder, string pngImageName);
void printDetected(DetectionManager* detectionManager);
void outputImage(ImageData image, DetectionManager* detection, CImg<unsigned char> render, string name);

int main() {
	double simulationTime = 0.0;
	int tests = 0;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Abilities Active"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Enemy Tower Detection"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Hextech Gunblade"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test In-Game Detection"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Item Actives"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Level Up Dot"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Level Up"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Self Champion"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Shop Available"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Shop Items"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Shop Items 2"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Shop Open"); tests++;

	simulationTime += runTestAndOutput("Resources/Test Images/", "Test Used Potion"); tests++;

	printf("\n");
	printf("• Finished %d tests...\n", tests);
	printf("• Average analysis time: %g\n", simulationTime / tests);
	printf("• About %d frames per second\n", (int)(1.0/(simulationTime / tests)));
	printf("\n");
	return(0);
}

double runTestAndOutput(string folder, string pngImageName) {
	printf("-------------------\n");
	printf(("Starting detection on %s\n"), (pngImageName + ".png").c_str());
	ImageData image = loadImage(folder + pngImageName + ".png");

	DetectionManager* detection = new DetectionManager();

	double simulation_time = read_timer( );

	detection->processDetection(&image);

	simulation_time = read_timer( ) - simulation_time;


	printDetected(detection);
	
	printf("Finished single image detection in %g seconds!\n", simulation_time);
	printf("-------------------\n");
	CImg<unsigned char> render1(image.imageWidth,image.imageHeight,1,4);
	render1.fill(0);
	outputImage(image, detection, render1, "Output/Output "+ pngImageName + ".png");

	CImg<unsigned char> render2((folder + pngImageName + ".png").c_str());
	render2.save_png(("Output/Output " + pngImageName + " Overlay Original.png").c_str());
	outputImage(image, detection, render2, "Output/Output "+pngImageName + " Overlay.png");

	return simulation_time;
}

void outputImage(ImageData image, DetectionManager* detectionManager, CImg<unsigned char> render, string name) {
	for (int i = 0; i < detectionManager->getAllyMinions()->size(); i++) {
		Minion* minion = (*(detectionManager->getAllyMinions()))[i];
		const unsigned char color[] = { 0,0,255, 255 };
		const unsigned char colorAlpha[] = { 0,0,255, 255 };
		double across = minion->health/100.0 * (minion->bottomRight.x - minion->topLeft.x) + minion->topLeft.x;
		render.draw_rectangle(minion->topLeft.x, minion->topLeft.y, across, minion->bottomRight.y, colorAlpha);
		render.draw_line(minion->topLeft.x, minion->topLeft.y, minion->topRight.x, minion->topRight.y,color);
		render.draw_line(minion->bottomLeft.x, minion->bottomLeft.y, minion->bottomRight.x, minion->bottomRight.y,color);
		render.draw_line(minion->topLeft.x, minion->topLeft.y, minion->bottomLeft.x, minion->bottomLeft.y,color);
		render.draw_line(minion->topRight.x, minion->topRight.y, minion->bottomRight.x, minion->bottomRight.y,color);
	}

	for (int i = 0; i < detectionManager->getEnemyMinions()->size(); i++) {
		Minion* minion = (*(detectionManager->getEnemyMinions()))[i];
		const unsigned char color[] = { 255,0,0, 255 };
		const unsigned char colorAlpha[] = { 255,0,0, 50 };
		double across = minion->health/100.0 * (minion->bottomRight.x - minion->topLeft.x) + minion->topLeft.x;
		render.draw_rectangle(minion->topLeft.x, minion->topLeft.y, across, minion->bottomRight.y, colorAlpha);
		render.draw_line(minion->topLeft.x, minion->topLeft.y, minion->topRight.x, minion->topRight.y,color);
		render.draw_line(minion->bottomLeft.x, minion->bottomLeft.y, minion->bottomRight.x, minion->bottomRight.y,color);
		render.draw_line(minion->topLeft.x, minion->topLeft.y, minion->bottomLeft.x, minion->bottomLeft.y,color);
		render.draw_line(minion->topRight.x, minion->topRight.y, minion->bottomRight.x, minion->bottomRight.y,color);
	}

	for (int i = 0; i < detectionManager->getAllyChampions()->size(); i++) {
		Champion* champion = (*(detectionManager->getAllyChampions()))[i];
		const unsigned char color[] = { 0,0,255, 255 };
		const unsigned char colorAlpha[] = { 0,0,255, 50 };
		double across = champion->health/100.0 * (champion->bottomRight.x - champion->topLeft.x) + champion->topLeft.x;
		render.draw_rectangle(champion->topLeft.x, champion->topLeft.y, across, champion->bottomRight.y, colorAlpha);
		render.draw_line(champion->topLeft.x, champion->topLeft.y, champion->topRight.x, champion->topRight.y,color);
		render.draw_line(champion->bottomLeft.x, champion->bottomLeft.y, champion->bottomRight.x, champion->bottomRight.y,color);
		render.draw_line(champion->topLeft.x, champion->topLeft.y, champion->bottomLeft.x, champion->bottomLeft.y,color);
		render.draw_line(champion->topRight.x, champion->topRight.y, champion->bottomRight.x, champion->bottomRight.y,color);
	}

	for (int i = 0; i < detectionManager->getEnemyChampions()->size(); i++) {
		Champion* champion = (*(detectionManager->getEnemyChampions()))[i];
		const unsigned char color[] = { 255,0,0, 255 };
		const unsigned char colorAlpha[] = { 255,0,0, 50 };
		double across = champion->health/100.0 * (champion->bottomRight.x - champion->topLeft.x) + champion->topLeft.x;
		render.draw_rectangle(champion->topLeft.x, champion->topLeft.y, across, champion->bottomRight.y, colorAlpha);
		render.draw_line(champion->topLeft.x, champion->topLeft.y, champion->topRight.x, champion->topRight.y,color);
		render.draw_line(champion->bottomLeft.x, champion->bottomLeft.y, champion->bottomRight.x, champion->bottomRight.y,color);
		render.draw_line(champion->topLeft.x, champion->topLeft.y, champion->bottomLeft.x, champion->bottomLeft.y,color);
		render.draw_line(champion->topRight.x, champion->topRight.y, champion->bottomRight.x, champion->bottomRight.y,color);
	}
	for (int i = 0; i < detectionManager->getEnemyTowers()->size(); i++) {
		Tower* tower = (*(detectionManager->getEnemyTowers()))[i];
		const unsigned char color[] = { 255,0,0, 255 };
		const unsigned char colorAlpha[] = { 255,0,0, 50 };
		double across = tower->health/100.0 * (tower->bottomRight.x - tower->topLeft.x) + tower->topLeft.x;
		render.draw_rectangle(tower->topLeft.x, tower->topLeft.y, across, tower->bottomRight.y, colorAlpha);
		render.draw_line(tower->topLeft.x, tower->topLeft.y, tower->topRight.x, tower->topRight.y,color);
		render.draw_line(tower->bottomLeft.x, tower->bottomLeft.y, tower->bottomRight.x, tower->bottomRight.y,color);
		render.draw_line(tower->topLeft.x, tower->topLeft.y, tower->bottomLeft.x, tower->bottomLeft.y,color);
		render.draw_line(tower->topRight.x, tower->topRight.y, tower->bottomRight.x, tower->bottomRight.y,color);
	}
	for (int i = 0; i < detectionManager->getSelfChampions()->size(); i++) {
		Champion* champion = (*(detectionManager->getSelfChampions()))[i];
		const unsigned char color[] = { 0,255,0, 255 };
		const unsigned char colorAlpha[] = { 0,255,0, 50 };
		double across = champion->health/100.0 * (champion->bottomRight.x - champion->topLeft.x) + champion->topLeft.x;
		render.draw_rectangle(champion->topLeft.x, champion->topLeft.y, across, champion->bottomRight.y, colorAlpha);
		render.draw_line(champion->topLeft.x, champion->topLeft.y, champion->topRight.x, champion->topRight.y,color);
		render.draw_line(champion->bottomLeft.x, champion->bottomLeft.y, champion->bottomRight.x, champion->bottomRight.y,color);
		render.draw_line(champion->topLeft.x, champion->topLeft.y, champion->bottomLeft.x, champion->bottomLeft.y,color);
		render.draw_line(champion->topRight.x, champion->topRight.y, champion->bottomRight.x, champion->bottomRight.y,color);
	}
	if (detectionManager->getSelfHealthBarVisible()) {
		SelfHealth* healthBar = detectionManager->getSelfHealthBar();
		const unsigned char color[] = { 0,255,0, 255 };
		const unsigned char colorAlpha[] = { 0,255,0, 50 };
		double across = healthBar->health/100.0 * (healthBar->bottomRight.x - healthBar->topLeft.x) + healthBar->topLeft.x;
		render.draw_rectangle(healthBar->topLeft.x, healthBar->topLeft.y, across, healthBar->bottomRight.y, colorAlpha);
		render.draw_line(healthBar->topLeft.x, healthBar->topLeft.y, healthBar->topRight.x, healthBar->topRight.y,color);
		render.draw_line(healthBar->bottomLeft.x, healthBar->bottomLeft.y, healthBar->bottomRight.x, healthBar->bottomRight.y,color);
		render.draw_line(healthBar->topLeft.x, healthBar->topLeft.y, healthBar->bottomLeft.x, healthBar->bottomLeft.y,color);
		render.draw_line(healthBar->topRight.x, healthBar->topRight.y, healthBar->bottomRight.x, healthBar->bottomRight.y,color);
	}
	const unsigned char border[] = { 255,255,255, 100 };
	if (detectionManager->getSpell1LevelUpVisible()) {
		GenericObject * object = detectionManager->getSpell1LevelUp();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);


		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSpell2LevelUpVisible()) {
		GenericObject * object = detectionManager->getSpell2LevelUp();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSpell3LevelUpVisible()) {
		GenericObject * object = detectionManager->getSpell3LevelUp();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSpell4LevelUpVisible()) {
		GenericObject * object = detectionManager->getSpell4LevelUp();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getCurrentLevel() > 0) {
		for (int i = 0; i < detectionManager->getSpell1LevelDots()->size(); i++) {
			GenericObject* object = (*(detectionManager->getSpell1LevelDots()))[i];
			const unsigned char color[] = { 0,0,0, 50 };
			render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

			render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
			render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
			render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
			render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
		}
		for (int i = 0; i < detectionManager->getSpell2LevelDots()->size(); i++) {
			GenericObject* object = (*(detectionManager->getSpell2LevelDots()))[i];
			const unsigned char color[] = { 0,0,0, 50 };
			render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

			render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
			render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
			render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
			render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
		}
		for (int i = 0; i < detectionManager->getSpell3LevelDots()->size(); i++) {
			GenericObject* object = (*(detectionManager->getSpell3LevelDots()))[i];
			const unsigned char color[] = { 0,0,0, 50 };
			render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

			render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
			render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
			render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
			render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
		}
		for (int i = 0; i < detectionManager->getSpell4LevelDots()->size(); i++) {
			GenericObject* object = (*(detectionManager->getSpell4LevelDots()))[i];
			const unsigned char color[] = { 0,0,0, 50 };
			render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

			render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
			render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
			render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
			render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
		}
	}
	if (detectionManager->getSpell1Available()) {
		GenericObject * object = detectionManager->getSpell1();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSpell2Available()) {
		GenericObject * object = detectionManager->getSpell2();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSpell3Available()) {
		GenericObject * object = detectionManager->getSpell3();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSpell4Available()) {
		GenericObject * object = detectionManager->getSpell4();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSummonerSpell1Available()) {
		GenericObject * object = detectionManager->getSummonerSpell1();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSummonerSpell2Available()) {
		GenericObject * object = detectionManager->getSummonerSpell2();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getTrinketActiveAvailable()) {
		GenericObject * object = detectionManager->getTrinketActive();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getItem1ActiveAvailable()) {
		GenericObject * object = detectionManager->getItem1Active();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getItem2ActiveAvailable()) {
		GenericObject * object = detectionManager->getItem2Active();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getItem3ActiveAvailable()) {
		GenericObject * object = detectionManager->getItem3Active();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getItem4ActiveAvailable()) {
		GenericObject * object = detectionManager->getItem4Active();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getItem5ActiveAvailable()) {
		GenericObject * object = detectionManager->getItem5Active();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getItem6ActiveAvailable()) {
		GenericObject * object = detectionManager->getItem6Active();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getPotionActiveAvailable()) {
		GenericObject * object = detectionManager->getPotionActive();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getPotionBeingUsedVisible()) {
		GenericObject * object = detectionManager->getPotionBeingUsed();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getShopAvailable()) {
		GenericObject * object = detectionManager->getShopAvailableObject();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getShopTopLeftCornerVisible()) {
		GenericObject * object = detectionManager->getShopTopLeftCorner();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getShopBottomLeftCornerVisible()) {
		GenericObject * object = detectionManager->getShopBottomleftCorner();
		const unsigned char color[] = { 0,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getBuyableItems()->size() > 0) {
		for (int i = 0; i < detectionManager->getBuyableItems()->size(); i++) {
			GenericObject* object = (*(detectionManager->getBuyableItems()))[i];
			const unsigned char color[] = { 0,0,0, 50 };
			render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

			render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
			render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
			render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
			render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
		}
	}
	if (detectionManager->getMapVisible()) {
		GenericObject * object = detectionManager->getMap();
		const unsigned char color[] = { 100,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getMapShopVisible()) {
		GenericObject * object = detectionManager->getMapShop();
		const unsigned char color[] = { 0,100,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getMapLocationVisible()) {
		GenericObject * object = detectionManager->getMapLocation();
		const unsigned char color[] = { 0,0,100, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}
	if (detectionManager->getSurrenderAvailable()) {
		GenericObject * object = detectionManager->getSurrender();
		const unsigned char color[] = { 255,0,0, 50 };
		render.draw_rectangle(object->topLeft.x, object->topLeft.y, object->bottomRight.x, object->bottomRight.y, color);

		render.draw_line(object->topLeft.x, object->topLeft.y, object->topRight.x, object->topRight.y,border);
		render.draw_line(object->bottomLeft.x, object->bottomLeft.y, object->bottomRight.x, object->bottomRight.y,border);
		render.draw_line(object->topLeft.x, object->topLeft.y, object->bottomLeft.x, object->bottomLeft.y,border);
		render.draw_line(object->topRight.x, object->topRight.y, object->bottomRight.x, object->bottomRight.y,border);
	}

	render.save_png(name.c_str());
}

void printDetected(DetectionManager* detection) {
	printf("Detected: \n");
	if (detection->getAllyMinions()->size() > 0) {
		printf("\t%lu ally minions\n", detection->getAllyMinions()->size());
	}
	if (detection->getAllyChampions()->size() > 0) {
		printf("\t%lu ally champions\n", detection->getAllyChampions()->size());
	}
	if (detection->getSelfChampions()->size() > 0) {
		printf("\t%lu self champions\n", detection->getSelfChampions()->size());
	}
	if (detection->getEnemyMinions()->size() > 0) {
		printf("\t%lu enemy minions\n", detection->getEnemyMinions()->size());
	}
	if (detection->getEnemyChampions()->size() > 0) {
		printf("\t%lu enemy champions\n", detection->getEnemyChampions()->size());
	}
	if (detection->getEnemyTowers()->size() > 0) {
		printf("\t%lu enemy towers\n", detection->getEnemyTowers()->size());
	}
	if (detection->getSelfHealthBarVisible()) {
		printf("\tCan see self health bar\n");
		printf("\tSelf health: %f\n", detection->getSelfHealthBar()->health);
	}
	if (detection->getSpell1LevelUpVisible()) {
		printf("\tLevel up spell 1 available\n");
	}
	if (detection->getSpell2LevelUpVisible()) {
		printf("\tLevel up spell 2 available\n");
	}
	if (detection->getSpell3LevelUpVisible()) {
		printf("\tLevel up spell 3 available\n");
	}
	if (detection->getSpell4LevelUpVisible()) {
		printf("\tLevel up spell 4 available\n");
	}
	if (detection->getCurrentLevel() > 0) {
		printf("\tDetected current level: %d\n", detection->getCurrentLevel());
	}
	if (detection->getSpell1Available()) {
		printf("\tSpell 1 available\n");
	}
	if (detection->getSpell2Available()) {
		printf("\tSpell 2 available\n");
	}
	if (detection->getSpell3Available()) {
		printf("\tSpell 3 available\n");
	}
	if (detection->getSpell4Available()) {
		printf("\tSpell 4 available\n");
	}
	if (detection->getSummonerSpell1Available()) {
		printf("\tSummoner spell 1 available\n");
	}
	if (detection->getSummonerSpell2Available()) {
		printf("\tSummoner spell 2 available\n");
	}
	if (detection->getTrinketActiveAvailable()) {
		printf("\tTrinket active available\n");
	}
	if (detection->getItem1ActiveAvailable()) {
		printf("\tItem 1 active available\n");
	}
	if (detection->getItem2ActiveAvailable()) {
		printf("\tItem 2 active available\n");
	}
	if (detection->getItem3ActiveAvailable()) {
		printf("\tItem 3 active available\n");
	}
	if (detection->getItem4ActiveAvailable()) {
		printf("\tItem 4 active available\n");
	}
	if (detection->getItem5ActiveAvailable()) {
		printf("\tItem 5 active available\n");
	}
	if (detection->getItem6ActiveAvailable()) {
		printf("\tItem 6 active available\n");
	}
	if (detection->getPotionActiveAvailable()) {
		printf("\tPotion active available\n");

		printf("\t\tPotion in slot %d\n", detection->getPotionActiveItemSlot());
	}
	if (detection->getPotionBeingUsedVisible()) {
		printf("\tPotion being used\n");
	}
	if (detection->getShopAvailable()) {
		printf("\tShop is available\n");
	}
	if (detection->getShopTopLeftCornerVisible()) {
		printf("\tShop top left corner is visible\n");
	}
	if (detection->getShopBottomLeftCornerVisible()) {
		printf("\tShop bottom left corner is visible\n");
	}
	if (detection->getBuyableItems()->size() > 0) {
		printf("\tBuyable items: %lu\n", detection->getBuyableItems()->size());
	}
	if (detection->getMapVisible()) {
		printf("\tMap is visible\n");
	}
	if (detection->getMapShopVisible()) {
		printf("\tShop on map is visible\n");
	}
	if (detection->getMapLocationVisible()) {
		printf("\tLocation on map is visible\n");
	}
	if (detection->getSurrenderAvailable()) {
		printf("\tSurrender is visible\n");
	}
}