using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using League_Autoplay.High_Performance_Timer;
using Debug = System.Diagnostics.Debug;

namespace League_Autoplay.AutoQueue
{
    public class AutoQueueManager
    {
        Bitmap acceptMatchButton, dontSendButton, randomChampButton, reconnectButton, playAgainButton;
        Position acceptMatchButtonPosition, dontSendButtonPosition, randomChampButtonPosition, reconnectButtonPosition, playAgainButtonPosition;
        Stopwatch acceptMatchClickStopwatch, dontSendClickStopwatch, randomChampClickStopwatch, reconnectButtonClickStopwatch, playAgainButtonStopwatch;

        public AutoQueueManager()
        {
            string dir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            acceptMatchButton = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Auto Queue Images\\Accept Match Button.png")));
            dontSendButton = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Auto Queue Images\\Dont Send Button.png")));
            randomChampButton = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Auto Queue Images\\Random Champ Button.png")));
            reconnectButton = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Auto Queue Images\\Reconnect Button.png")));
            playAgainButton = new Bitmap(Image.FromFile(Path.Combine(dir, "AnalysisImages\\Resources\\Auto Queue Images\\Play Again Button.png")));

            acceptMatchClickStopwatch = new Stopwatch();
            dontSendClickStopwatch = new Stopwatch();
            randomChampClickStopwatch = new Stopwatch();
            reconnectButtonClickStopwatch = new Stopwatch();
            playAgainButtonStopwatch = new Stopwatch();
        }

        public void reset()
        {

        }
        public void runAutoQueue(Bitmap screen)
        {

            //Loop through pixels on the screen and look for any of those four buttons.
            if (acceptMatchClickStopwatch.DurationInMilliseconds() >= 500)
            {
                acceptMatchButtonPosition = AutoQueueDetection.findImageInScreen(screen, acceptMatchButton, 366, 393, 10, 10, 0.95);
                if (acceptMatchButtonPosition.x != -1)
                {
                    MotorCortex.clickMouseAt(acceptMatchButtonPosition.x + 10, acceptMatchButtonPosition.y = 10);
                    moveMouseDelayed();
                    acceptMatchClickStopwatch.Reset();
                    return;
                }
            }

            if (dontSendClickStopwatch.DurationInMilliseconds() >= 500)
            {
                dontSendButtonPosition = AutoQueueDetection.findImageInScreen(screen, dontSendButton, 465, 535, 10, 10, 0.95);
                if (dontSendButtonPosition.x != -1)
                {
                    MotorCortex.clickMouseAt(dontSendButtonPosition.x + 10, dontSendButtonPosition.y = 10);
                    moveMouseDelayed();
                    dontSendClickStopwatch.Reset();
                    return;
                }
            }

            if (randomChampClickStopwatch.DurationInMilliseconds() >= 500)
            {
                randomChampButtonPosition = AutoQueueDetection.findImageInScreen(screen, randomChampButton, 235, 186, 10, 10, 0.95);
                if (randomChampButtonPosition.x != -1)
                {
                    MotorCortex.clickMouseAt(randomChampButtonPosition.x + 10, randomChampButtonPosition.y = 10);
                    moveMouseDelayed();
                    randomChampClickStopwatch.Reset();
                    return;
                }
            }

            if (reconnectButtonClickStopwatch.DurationInMilliseconds() >= 500)
            {
                reconnectButtonPosition = AutoQueueDetection.findImageInScreen(screen, reconnectButton, 438, 394, 10, 10, 0.95);
                if (reconnectButtonPosition.x != -1)
                {
                    MotorCortex.clickMouseAt(reconnectButtonPosition.x + 10, reconnectButtonPosition.y = 10);
                    moveMouseDelayed();
                    reconnectButtonClickStopwatch.Reset();
                    return;
                }
            }

            if (playAgainButtonStopwatch.DurationInMilliseconds() >= 500)
            {
                playAgainButtonPosition = AutoQueueDetection.findImageInScreen(screen, playAgainButton, 776, 616, 10, 10, 0.95);
                if (playAgainButtonPosition.x != -1)
                {
                    MotorCortex.clickMouseAt(playAgainButtonPosition.x + 10, playAgainButtonPosition.y = 10);
                    moveMouseDelayed();
                    playAgainButtonStopwatch.Reset();
                    return;
                }
            }
        }
        public void enteredLeagueOfLegends()
        {

        }
        public void exitedLeagueOfLegends()
        {
            MotorCortex.moveMouseTo(0, 0, 1);
        }
        public void moveMouseDelayed()
        {
            Task.Delay(200).ContinueWith(_ =>
            {
                MotorCortex.moveMouseTo(0, 0, 1);
            });
        }
    }
}
