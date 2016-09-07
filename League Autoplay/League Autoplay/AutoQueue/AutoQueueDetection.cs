using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace League_Autoplay.AutoQueue
{
    public class AutoQueueDetection
    {

        unsafe static byte* getPixel2(byte* imageData, int imageWidth, int x, int y)
        {
            return imageData + (y * imageWidth + x) * 4;
        }

        public static unsafe Position findImageInScreen(Bitmap screen, Bitmap findImage, int searchX, int searchY, int searchWidth, int searchHeight, double matchingPercent)
        {
            System.Drawing.Rectangle screenBoundsRect = new System.Drawing.Rectangle(0, 0, screen.Width, screen.Height);
            var screenBitmapData = screen.LockBits(screenBoundsRect, ImageLockMode.WriteOnly, screen.PixelFormat);
            var screenBitmapPointer = screenBitmapData.Scan0;
            byte* screenData = (byte*)screenBitmapPointer.ToPointer();

            Position returnPosition = new Position();
            returnPosition.x = -1;
            returnPosition.y = -1;

            Position pos = new Position();
            pos.x = searchX;
            pos.y = searchY;

            System.Drawing.Rectangle boundsRect = new System.Drawing.Rectangle(0, 0, findImage.Width, findImage.Height);
            var bitmapData = findImage.LockBits(boundsRect, ImageLockMode.WriteOnly, findImage.PixelFormat);
            var bitmapPointer = bitmapData.Scan0;
            byte* matchButtonData = (byte*)bitmapPointer.ToPointer();

            int maxX = Math.Min(pos.x + searchWidth, screen.Width);
            int maxY = Math.Min(pos.y + searchHeight, screen.Height);

            for (int x = pos.x; x < pos.x + searchWidth; x++)
            {
                for (int y = pos.y; y < pos.y + searchHeight; y++)
                {
                    bool foundImage = detectImageAtPixel(screen, screenData, findImage, matchButtonData, x, y, matchingPercent);
                    if (foundImage)
                    {
                        returnPosition.x = x;
                        returnPosition.y = y;
                        x = screen.Width;
                        y = screen.Height;
                    }
                }
            }

            findImage.UnlockBits(bitmapData);

            screen.UnlockBits(screenBitmapData);

            return returnPosition;
        }



        unsafe static bool detectImageAtPixel(Bitmap screenImage, byte* screenData, Bitmap findImage, byte* findImageData, int xOnScreen, int yOnScreen, double matchingPercent)
        {
            byte* onScreenPixel = getPixel2(screenData, screenImage.Width, xOnScreen, yOnScreen);

            //GenericObject * object = NULL;
            //double percent;
            if ( getImageAtPixelPercentageOptimizedExact(onScreenPixel, xOnScreen, yOnScreen, screenImage.Width, screenImage.Height, findImage, findImageData, matchingPercent) >= matchingPercent)
            {
                //Console.WriteLine("Found image with percent match: " + percent);
                return true;
            }

            return false;
        }

        //public static Bitmap matchingBitmap = new Bitmap(1024, 768, PixelFormat.Format24bppRgb);

        unsafe static double getImageAtPixelPercentageOptimizedExact(byte* onScreenpixel, int xOnScreen, int yOnScreen, int screenWidth, int screenHeight, Bitmap findImage, byte* findImageData, double minimumPercentage)
        {
            /*
            if (xOnScreen == 369 && yOnScreen == 396)
            {
                for (int x = 0; x < 1024; x++)
                {
                    for (int y = 0; y < 768; y++)
                    {
                        matchingBitmap.SetPixel(0, 0, Color.White);
                    }
                }
                Console.WriteLine("Scanning pixel: ");
            }*/

            int pixels = 0;
            int maxPixelCount = findImage.Width * findImage.Height;
            int skipPixels = 4 * (screenWidth - findImage.Width);
            double percentage = 0.0;
            byte* pixel2 = findImageData;
            byte* pixel = onScreenpixel;

            if (xOnScreen + findImage.Width > screenWidth || yOnScreen + findImage.Height > screenHeight)
            {
                return 0.0;
            }
            /*
            if (xOnScreen == 369 && yOnScreen == 396)
            {

                Console.WriteLine("Scanning part 2: ");
            }*/

            int perfectPixels = 0;
            int nonPerfectPixels = 0;
            for (int y1 = 0; y1 < findImage.Height; y1++)
            {
                for (int x1 = 0; x1 < findImage.Width; x1++)
                {
                    if (pixel2[3] != 0)
                    {
                        pixels++;
                        double p = getColorPercentage(pixel, pixel2);
                        /*
                        if (xOnScreen == 369 && yOnScreen == 396)
                        {
                            Console.WriteLine("Pixel comparison: " + p);
                            Console.WriteLine("Pixel x: " + x1 + ",y: " + y1 + "{"+pixel[0] + "," + pixel[1] + "," + pixel[2]+ "} vs " + "{" + pixel2[0] + ","+ pixel2[1] + "," + pixel2[2] + "}");
                            
                            matchingBitmap.SetPixel(xOnScreen + x1, yOnScreen + y1, Color.FromArgb(pixel[2],pixel[1],pixel[0]));
                             
                        }
                    */

                       percentage += p;
                        if (p < minimumPercentage)
                        {
                            /*
                            if (xOnScreen == 369 && yOnScreen == 396)
                            {

                                Console.WriteLine("Color percent is less than required so returning. Percent: " + percentage + ", maxPixelCount: " + maxPixelCount);
                                Console.WriteLine("Returning: " + percentage / maxPixelCount);
                            }*/
                            return percentage / maxPixelCount;
                        }
                        if (p == 1.0)
                        {
                            perfectPixels++;
                        }
                        else
                        {
                            nonPerfectPixels++;
                        }
                    }
                    else { maxPixelCount--; }
                    pixel2 += 4;
                    pixel += 4;
                }
                pixel += skipPixels;
            }
            /*
            if (xOnScreen == 369 && yOnScreen == 396)
            {

                Console.WriteLine("Scanning part 3: with match: " + (percentage / pixels));
                Console.WriteLine("Image comparison: " + pixels + " pixels, " + percentage + "%");
            }

            if (percentage / pixels == 1.0)
            {
                Console.WriteLine("Perfect image comparison: " + pixels + " pixels, " + percentage + "%");
            } */
            return percentage / pixels;
        }
        unsafe static double getColorPercentage(byte* pixel, byte* pixel2)
        {
            //pixel 1 is 255, 255, 255
            //pixel 2 is 0, 0, 0
            //match is 0

            //pixel 1 and 2 is 255, 255, 255
            //match is 1.0
            const double scale = 1.0 / (255.0 * 255.0 * 255.0); // compile-time constant
            int m0 = 255 - Math.Abs(pixel[0] - pixel2[0]); // NB: use std::abs rather than fabs
            int m1 = 255 - Math.Abs(pixel[1] - pixel2[1]); // and keep all of this part
            int m2 = 255 - Math.Abs(pixel[2] - pixel2[2]); // in the integer domain
            int m = m0 * m1 * m2;
            return (double)m * scale;
        }
    }
}
