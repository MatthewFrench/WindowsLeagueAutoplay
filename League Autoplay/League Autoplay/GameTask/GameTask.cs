using League_Autoplay.High_Performance_Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay.GameTask
{
    public class GameTask
    {
        Action taskAction;
        int taskDelay;
        Stopwatch taskStopwatch;
        bool taskFinished = false;
        public GameTask(int delay, Action action)
        {
            taskAction = action;
            taskDelay = delay;
            taskStopwatch = new Stopwatch();
        }
        public void Process()
        {
            if (taskStopwatch.DurationInMilliseconds() >= taskDelay)
            {
                taskFinished = true;
                taskAction();
            }
        }
        public bool IsFinished()
        {
            return taskFinished;
        }
    }
}
