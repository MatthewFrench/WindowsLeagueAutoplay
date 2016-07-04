#include <stdio.h>

extern "C"
{
	__declspec(dllexport) void DisplayHelloFromDLL()
	{
		printf("Hello from DLL !\n");
	}
	__declspec(dllexport) double Add(double a, double b)
	{
		return a + b;
	}

	typedef unsigned char byte;

	__declspec(dllexport) void Copy(byte* startPointer, byte* startDestinationPointer, int width, int height) {
		const int channels = 4;
		const int r_channel = 2;
		const int g_channel = 1;
		const int b_channel = 0;
		const int a_channel = 3;

		int loc = 0;

		for (int y = 0; y < height; y++)
		{

			//byte* upToRow = startPointer + y * width * channels;
			//byte* destinationUpToRow = startDestinationPointer + y * width * channels;

			for (int x = 0; x < width; x++)
			{
				
				startDestinationPointer[loc + b_channel] = startPointer[loc + b_channel];
				startDestinationPointer[loc + g_channel] = startPointer[loc + g_channel];
				startDestinationPointer[loc + r_channel] = startPointer[loc + r_channel];
				startDestinationPointer[loc + a_channel] = startPointer[loc + a_channel];

				loc += 4;
				
				/*
				byte* pixel = upToRow + x * channels;
				byte* destinationPixel = destinationUpToRow + x * channels;


				destinationPixel[b_channel] = pixel[b_channel];
				destinationPixel[g_channel] = pixel[g_channel];
				destinationPixel[r_channel] = pixel[r_channel];
				destinationPixel[a_channel] = pixel[a_channel];
				*/
			}

		}
	}
}