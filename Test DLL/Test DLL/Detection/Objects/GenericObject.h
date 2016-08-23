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

typedef struct GenericObject {
    Position topLeft, topRight, bottomLeft, bottomRight, center;
} GenericObject;

#endif