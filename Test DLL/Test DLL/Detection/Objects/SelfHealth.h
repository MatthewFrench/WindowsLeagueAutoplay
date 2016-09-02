//
//  SelfHealth.h
//  Fluffy Pug
//
//  Created by Matthew French on 9/10/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "Position.h"

#ifndef SelfHealth_cpp
#define SelfHealth_cpp
#pragma pack(1)
typedef struct SelfHealth {
	Position topLeft, topRight, bottomLeft, bottomRight, characterCenter;
	bool detectedLeftSide = false, detectedRightSide = false;
	double health = 0;
} SelfHealth;
#pragma pack()
#endif