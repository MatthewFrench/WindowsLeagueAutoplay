cmake_minimum_required(VERSION 3.5.1)

set(CMAKE_CXX_STANDARD 14)
set(CMAKE_CXX_STANDARD_REQUIRED on)

SET( CMAKE_CXX_FLAGS  "${CMAKE_CXX_FLAGS} -fopenmp" )
SET( CMAKE_C_FLAGS  "${CMAKE_C_FLAGS} -fopenmp" )
SET( CMAKE_EXE_LINKER_FLAGS  "${CMAKE_EXE_LINKER_FLAGS} -fopenmp" )
SET( CMAKE_C_FLAGS  "${CMAKE_C_FLAGS}  -falign-functions=32 -falign-loops=32" )
SET( CMAKE_C_FLAGS  "-std=gnu++" )

SET( LINKER_FLAGS  "${LINKER_FLAGS} -fopenmp" )
SET( LINKER_FLAGS  "${LINKER_FLAGS} -flto" )

set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -fopenmp")
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -fopenmp")
set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} ${OpenMP_C_FLAGS}")
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${OpenMP_CXX_FLAGS}")
set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} ${OpenMP_EXE_LINKER_FLAGS}")

message(STATUS "Checking OpenMP")
    message("Found OpenMP! ^_^")
    # add flags for OpenMP
    set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} ${OpenMP_C_FLAGS}")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${OpenMP_CXX_FLAGS}")
    set(CMAKE_SHARED_LINKER_FLAGS "${CMAKE_SHARED_LINKER_FLAGS} ${OpenMP_SHARED_LINKER_FLAGS}")
    set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} ${OpenMP_EXE_LINKER_FLAGS}")


SET(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Ofast")
SET(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -Ofast")

project (artificialIntelligenceBenchmark)

file(GLOB_RECURSE all_SRC "Test DLL/*.cpp" "Test DLL/*.h")

add_library(artificialIntelligenceBenchmark SHARED ${all_SRC})

include_directories(/usr/local/include)
link_directories(/usr/local/lib)

target_link_libraries(artificialIntelligenceBenchmark  ${OpenMP_CXX_FLAGS})

