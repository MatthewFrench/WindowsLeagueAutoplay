﻿using League_Autoplay.High_Performance_Timer;
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
    public partial class UserInterface : Form
    {
        ArtificialIntelligence artificialIntelligence;
        VisualCortex visualCortex;
        MotorCortex motorCortex;
        TaskScheduler uiContext;
        public UserInterface()
        {
            InitializeComponent();
            uiContext = TaskScheduler.FromCurrentSynchronizationContext();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            visualCortex = new VisualCortex();
            visualCortex.setShouldCaptureDisplayImage(true);

            motorCortex = new MotorCortex();

            artificialIntelligence = new ArtificialIntelligence(this, visualCortex, motorCortex);
        }
        public TaskScheduler getUIContext()
        {
            return uiContext;
        }
        public void setDisplayImage(Bitmap b)
        {
            TaskHelper.RunTask(uiContext, ()=>{
                screenCaptureBox.Image = b;
            });
        }
        public void setAIPerformanceLabel(String text)
        {
            TaskHelper.RunTask(uiContext, () => {
                aiPerformanceLabel.Text = text;
            });
        }
        public void setScreenPerformanceLabel(String text)
        {
            TaskHelper.RunTask(uiContext, () => {
                screenPerformanceLabel.Text = text;
            });
        }
    }
}