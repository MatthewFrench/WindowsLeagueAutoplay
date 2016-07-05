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
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            visualCortex = new VisualCortex();
            visualCortex.runTest();
            visualCortex.grabScreenAndDetect();
        }
    }
}
