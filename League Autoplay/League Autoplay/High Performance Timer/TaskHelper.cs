using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay.High_Performance_Timer
{
    public class TaskHelper
    {
        public static void RunTask(TaskScheduler context, Action action)
        {
            Task uiTask = new Task(action);
            uiTask.Start(context);
        }
    }
}
