using AutoItX3Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay
{
    class MotorCortex
    {
        static AutoItX3 _autoIT = new AutoItX3();

        MotorCortex()
        {
            _autoIT.AutoItSetOption("SendKeyDelay", 0);
        }
        void typeText(String text)
        {
            _autoIT.Send(text);
        }
        void clickMouse()
        {
            _autoIT.MouseClick("LEFT", getMouseX(), getMouseY(), 1, 0);
        }
        void clickMouseAt(int x, int y, int speed=0)
        {
            _autoIT.MouseClick("LEFT", x, y, 1, speed);
        }
        void clickMouseTwiceAt(int x, int y, int speed = 0)
        {
            _autoIT.MouseClick("LEFT", x, y, 2, speed);
        }
        void clickMouseRight()
        {
            _autoIT.MouseClick("RIGHT", getMouseX(), getMouseY(), 1, 0);
        }
        void clickMouseRightAt(int x, int y, int speed = 0)
        {
            _autoIT.MouseClick("RIGHT", x, y, speed);
        }
        void clickMouseRightTwiceAt(int x, int y, int speed = 0)
        {
            _autoIT.MouseClick("RIGHT", x, y, 2, speed);
        }
        void pressMouse()
        {
            _autoIT.MouseDown();
        }
        void pressMouseRight()
        {
            _autoIT.MouseDown("RIGHT");
        }
        void releaseMouse()
        {
            _autoIT.MouseUp();
        }
        void releaseMouseRight()
        {
            _autoIT.MouseUp("RIGHT");
        }
        void moveMouseTo(int x, int y, int speed = 0)
        {
            _autoIT.MouseMove(x, y, speed);
        }
        void dragMouse(int fromX, int fromY, int toX, int toY, int speed = 0)
        {
            _autoIT.MouseClickDrag("LEFT",fromX, fromY, toX, toY, speed);
        }
        void dragMouseRight(int fromX, int fromY, int toX, int toY, int speed = 0)
        {
            _autoIT.MouseClickDrag("RIGHT", fromX, fromY, toX, toY, speed);
        }
        int getMouseX()
        {
            return _autoIT.MouseGetPosX();
        }
        int getMouseY()
        {
            return _autoIT.MouseGetPosY();
        }
    }
}
