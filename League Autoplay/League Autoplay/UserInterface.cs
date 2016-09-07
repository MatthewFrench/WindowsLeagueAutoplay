using League_Autoplay.High_Performance_Timer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
        TaskScheduler uiContext;
        public UserInterface()
        {
            InitializeComponent();
            uiContext = TaskScheduler.FromCurrentSynchronizationContext();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("Application showing");
            Debug.WriteLine("Application showing debug");
            visualCortex = new VisualCortex();
            visualCortex.setShouldCaptureDisplayImage(true);
            MotorCortex.init();

            artificialIntelligence = new ArtificialIntelligence(this, visualCortex);

            //Set fps box to 60fps
            aiFpsBox.SelectedIndex = 6;
            updateDisplayCheckBox.Checked = true;
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
        public void setDebugImage(Bitmap b)
        {
            TaskHelper.RunTask(uiContext, () => {
                debugBox.Image = b;
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

        private void aiFpsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set the AI speed
            int fps = int.Parse(aiFpsBox.Text);
            int milliseconds = (int)1000.0 / fps;
            artificialIntelligence.createAITimer(milliseconds);
        }

        private void updateDisplayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            artificialIntelligence.setUpdateDisplayImage(updateDisplayCheckBox.Checked);
        }
    }

    public class PictureBoxWithInterpolationMode : PictureBox
    {
        public InterpolationMode InterpolationMode { get; set; }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            base.OnPaint(paintEventArgs);
        }
    }
}
