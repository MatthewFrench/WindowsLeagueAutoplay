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

        public MotorCortex()
        {
            Console.WriteLine("Auto it 1");
            AutoItX.AutoItSetOption("SendKeyDelay", 0);
            Console.WriteLine("Auto it 2");
        }
        void typeText(String text)
        {
            AutoItX.Send(text);
        }
        void clickMouse()
        {
            AutoItX.MouseClick("LEFT", getMouseX(), getMouseY(), 1, 0);
        }
        void clickMouseAt(int x, int y, int speed=0)
        {
            AutoItX.MouseClick("LEFT", x, y, 1, speed);
        }
        void clickMouseTwiceAt(int x, int y, int speed = 0)
        {
            AutoItX.MouseClick("LEFT", x, y, 2, speed);
        }
        void clickMouseRight()
        {
            AutoItX.MouseClick("RIGHT", getMouseX(), getMouseY(), 1, 0);
        }
        void clickMouseRightAt(int x, int y, int speed = 0)
        {
            AutoItX.MouseClick("RIGHT", x, y, speed);
        }
        void clickMouseRightTwiceAt(int x, int y, int speed = 0)
        {
            AutoItX.MouseClick("RIGHT", x, y, 2, speed);
        }
        void pressMouse()
        {
            AutoItX.MouseDown();
        }
        void pressMouseRight()
        {
            AutoItX.MouseDown("RIGHT");
        }
        void releaseMouse()
        {
            AutoItX.MouseUp();
        }
        void releaseMouseRight()
        {
            AutoItX.MouseUp("RIGHT");
        }
        void moveMouseTo(int x, int y, int speed = 0)
        {
            AutoItX.MouseMove(x, y, speed);
        }
        void dragMouse(int fromX, int fromY, int toX, int toY, int speed = 0)
        {
            AutoItX.MouseClickDrag("LEFT",fromX, fromY, toX, toY, speed);
        }
        void dragMouseRight(int fromX, int fromY, int toX, int toY, int speed = 0)
        {
            AutoItX.MouseClickDrag("RIGHT", fromX, fromY, toX, toY, speed);
        }
        int getMouseX()
        {
            return AutoItX.MouseGetPos().X;
        }
        int getMouseY()
        {
            return AutoItX.MouseGetPos().Y;
        }
    }
}
