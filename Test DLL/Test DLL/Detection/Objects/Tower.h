//
//  Tower.h
//  Fluffy Pug
//
//  Created by Matthew French on 9/10/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "Position.h"

typedef struct Tower {
	Position topLeft, topRight, bottomLeft, bottomRight, towerCenter;
	bool detectedTopLeft = false, detectedBottomLeft=false, detectedTopRight=false, detectedBottomRight=false;
	double health = 0;
} Tower;
/*
class Tower {
public:
    Tower();
    Position topLeft, topRight, bottomLeft, bottomRight, towerCenter;
    bool detectedTopLeft, detectedBottomLeft, detectedTopRight, detectedBottomRight;
    double health;
};*/