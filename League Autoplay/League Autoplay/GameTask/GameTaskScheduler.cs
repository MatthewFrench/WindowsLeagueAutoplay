using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Autoplay.GameTask
{
    public class GameTaskScheduler
    {
        static List<GameTask> tasks = new List<GameTask>();

        public static void ProcessTasks()
        {
            foreach (GameTask task in tasks)
            {
                task.Process();
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                GameTask task = tasks[i];
                if (task.IsFinished())
                {
                    tasks.RemoveAt(i);
                    i--;
                }
            }
        }
        public static void Delay(int milliseconds, Action action)
        {
            GameTask task = new GameTask(milliseconds, action);
            tasks.Add(task);
        }
        public static int getCount()
        {
            return tasks.Count;
        }
    }
}
