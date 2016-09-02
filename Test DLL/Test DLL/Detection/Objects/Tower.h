//
//  Tower.h
//  Fluffy Pug
//
//  Created by Matthew French on 9/10/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "Position.h"

#ifndef Tower_cpp
#define Tower_cpp
#pragma pack(1)
typedef struct Tower {
	Position topLeft, topRight, bottomLeft, bottomRight, towerCenter;
	bool detectedTopLeft = false, detectedBottomLeft = false, detectedTopRight = false, detectedBottomRight = false;
	double health = 0;
} Tower;
#pragma pack()
#endif

/*
class Tower {
public:
    Tower();
    Position topLeft, topRight, bottomLeft, bottomRight, towerCenter;
    bool detectedTopLeft, detectedBottomLeft, detectedTopRight, detectedBottomRight;
    double health;
};*/