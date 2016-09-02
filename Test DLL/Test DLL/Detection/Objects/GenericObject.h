//
//  GenericObject.h
//  Fluffy Pug
//
//  Created by Matthew French on 9/10/15.
//  Copyright © 2015 Matthew French. All rights reserved.
//

#include "Position.h"

#ifndef GenericObject_cpp
#define GenericObject_cpp
#pragma pack(1)
typedef struct GenericObject {
    Position topLeft, topRight, bottomLeft, bottomRight, center;
} GenericObject;
#pragma pack()
#endif