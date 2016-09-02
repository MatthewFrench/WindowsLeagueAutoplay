//
//  Champion.h
//  Fluffy Pug
//
//  Created by Matthew French on 9/10/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "Position.h"

#ifndef Champion_cpp
#define Champion_cpp
#pragma pack(1)
typedef struct Champion {
    Position topLeft, topRight, bottomLeft, bottomRight, characterCenter;
    bool detectedTopLeft=false, detectedBottomLeft=false, detectedTopRight=false, detectedBottomRight=false;
    double health=0;
} Champion;
#pragma pack()
#endif