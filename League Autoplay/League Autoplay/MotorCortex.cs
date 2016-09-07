using AutoIt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay
{
    public class MotorCortex
    {
        //static AutoIt.AutoItX _autoIT = new AutoIt.AutoItX();

        static public void init()
        {
            AutoItX.AutoItSetOption("SendKeyDelay", 0);
        }
        static public void typeText(String text, bool justText=false)
        {
            if (!justText)
            {
                AutoItX.Send(text);
            } else
            {
                AutoItX.Send(text, 1);
            }
        }
        static public void clickMouse()
        {
            AutoItX.MouseClick("LEFT", getMouseX(), getMouseY(), 1, 0);
        }
        static public void clickMouseAt(int x, int y, int speed=0)
        {
            AutoItX.MouseClick("LEFT", x, y, 1, speed);
        }
        static public void clickMouseTwiceAt(int x, int y, int speed = 0)
        {
            AutoItX.MouseClick("LEFT", x, y, 2, speed);
        }
        static public void clickMouseRight()
        {
            AutoItX.MouseClick("RIGHT", getMouseX(), getMouseY(), 1, 0);
        }
        static public void clickMouseRightAt(int x, int y, int speed = 0)
        {
            AutoItX.MouseClick("RIGHT", x, y, 1, speed);
        }
        static public void clickMouseRightTwiceAt(int x, int y, int speed = 0)
        {
            AutoItX.MouseClick("RIGHT", x, y, 2, speed);
        }
        static public void pressMouse()
        {
            AutoItX.MouseDown();
        }
        static public void pressMouseRight()
        {
            AutoItX.MouseDown("RIGHT");
        }
        static public void releaseMouse()
        {
            AutoItX.MouseUp();
        }
        static public void releaseMouseRight()
        {
            AutoItX.MouseUp("RIGHT");
        }
        static public void moveMouseTo(int x, int y, int speed = 0)
        {
            AutoItX.MouseMove(x, y, speed);
        }
        static public void dragMouse(int fromX, int fromY, int toX, int toY, int speed = 0)
        {
            AutoItX.MouseClickDrag("LEFT",fromX, fromY, toX, toY, speed);
        }
        static public void dragMouseRight(int fromX, int fromY, int toX, int toY, int speed = 0)
        {
            AutoItX.MouseClickDrag("RIGHT", fromX, fromY, toX, toY, speed);
        }
        static public int getMouseX()
        {
            return AutoItX.MouseGetPos().X;
        }
        static public int getMouseY()
        {
            return AutoItX.MouseGetPos().Y;
        }
    }
}
