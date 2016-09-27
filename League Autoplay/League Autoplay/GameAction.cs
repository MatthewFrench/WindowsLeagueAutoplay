using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay
{
    public class GameAction
    {
        Action<GameAction> actionFunction;
        bool actionFinished = false;
        bool actionRunning = false;
        String actionID = "";

        public GameAction(Action<GameAction> function, String ID)
        {
            actionFunction = function;
            actionID = ID;
        }
        public void runAction()
        {
            actionRunning = true;
        }
        public void finished()
        {
            actionFinished = true;
            actionRunning = false;
        }
        public bool isFinished()
        {
            return actionFinished;
        }
        public bool isRunning()
        {
            return actionRunning;
        }
        public String getID()
        {
            return actionID;
        }
    }
}
