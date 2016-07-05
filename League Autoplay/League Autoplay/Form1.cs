using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace League_Autoplay
{
    public partial class Form1 : Form
    {
        VisualCortex visualCortex;
        TaskScheduler uiContext;
        public Form1()
        {
            InitializeComponent();
            uiContext = TaskScheduler.FromCurrentSynchronizationContext();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            visualCortex = new VisualCortex();
            visualCortex.setShouldCaptureDisplayImage(true);
            visualCortex.runTest();

            System.Timers.Timer aTimer = new System.Timers.Timer(15);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;
            // Start the timer
            aTimer.Enabled = true;
        }
        bool grabbingScreen = false;
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            
            //Only grab the screen when it's done processing. Do on separate task so we don't freeze the UI.
            if (grabbingScreen == false)
            {
                grabbingScreen = true;
                Task t = Task.Run(() =>
                {
                    visualCortex.grabScreenAndDetect();
                }).ContinueWith(_ => {
                    //Run on UI thread
                    screenCaptureBox.Image = visualCortex.getDisplayImage();
                    grabbingScreen = false;
                }, uiContext);
            }
        }
    }
}
