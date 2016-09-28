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
            actionFunction(this);
        }
        public void finished()
        {
            //Add a 50 millisecond delay
            Task.Delay(50).ContinueWith(_ =>
            {
                actionFinished = true;
                actionRunning = false;
                Console.WriteLine("Finished Action");
            });
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
