using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;
using Gma.UserActivityMonitor;
using AutoItX3Lib;

using System.Reflection;
using System.Web;
using System.Runtime;
using System.Configuration;

using System.Threading;

namespace League_Auto_Key_Presser
{
    public partial class Form1 : Form
    {
        static AutoItX3 _autoIT = new AutoItX3();
        static AutoItX3 _autoIT1 = new AutoItX3();
        static AutoItX3 _autoIT2 = new AutoItX3();
        static AutoItX3 _autoIT3 = new AutoItX3();
        static AutoItX3 _autoIT4 = new AutoItX3();

        ATimer timer = null;

        int keyQCountUp = 0;
        int keyWCountUp = 0;
        int keyECountUp = 0;
        int keyRCountUp = 0;
        int spell1Sent = 0;
        int spell2Sent = 0;
        int spell3Sent = 0;
        int spell4Sent = 0;
        bool keyTPressed = false;

        bool pressingSpell1 = false;
        bool pressingSpell2 = false;
        bool pressingSpell3 = false;
        bool pressingSpell4 = false;

        Stopwatch spell1Stopwatch = Stopwatch.StartNew();
        Stopwatch spell2Stopwatch = Stopwatch.StartNew();
        Stopwatch spell3Stopwatch = Stopwatch.StartNew();
        Stopwatch spell4Stopwatch = Stopwatch.StartNew();
        Stopwatch activeStopwatch = Stopwatch.StartNew();
        Stopwatch wardHopStopwatch = Stopwatch.StartNew();
        Stopwatch pressingWardStopwatch = Stopwatch.StartNew();

        int pressSpell1Interval = 10;
        int pressSpell2Interval = 10;
        int pressSpell3Interval = 10;
        int pressSpell4Interval = 100;
        int pressActiveInterval = 500;

        int pressWardInterval = 6000;

        bool autoKeyOnBool = true;
        bool active1OnBool = false;
        bool active2OnBool = false;
        bool active3OnBool = false;
        bool active5OnBool = false;
        bool active6OnBool = false;
        bool active7OnBool = false;
        bool wardOnBool = false;

        bool wardHopOn = false;
        bool qPreactivateW = false;
        bool qPreactivateE = false;
        bool qPreactivateR = false;

        bool wPreactivateQ = false;
        bool wPreactivateE = false;
        bool wPreactivateR = false;

        bool ePreactivateQ = false;
        bool ePreactivateW = false;
        bool ePreactivateR = false;

        bool rPreactivateQ = false;
        bool rPreactivateW = false;
        bool rPreactivateE = false;

        char wardHopKey = 'Q';

        char activeKey = 'E';

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtSetTimerResolution(int DesiredResolution, bool SetResolution, out int CurrentResolution);

        public Form1()
        {
            InitializeComponent();

            //Load state
            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count != 0)
            {
                pressSpell1Interval = Convert.ToInt32(appSettings["pressSpell1Interval"]);
                pressSpell2Interval = Convert.ToInt32(appSettings["pressSpell2Interval"]);
                pressSpell3Interval = Convert.ToInt32(appSettings["pressSpell3Interval"]);
                pressSpell4Interval = Convert.ToInt32(appSettings["pressSpell4Interval"]);
                pressActiveInterval = Convert.ToInt32(appSettings["pressActiveInterval"]);
                autoKeyOnBool = Convert.ToBoolean(appSettings["autoKeyOnBool"]);
                active1OnBool = Convert.ToBoolean(appSettings["active1OnBool"]);
                active2OnBool = Convert.ToBoolean(appSettings["active2OnBool"]);
                active3OnBool = Convert.ToBoolean(appSettings["active3OnBool"]);
                active5OnBool = Convert.ToBoolean(appSettings["active5OnBool"]);
                active6OnBool = Convert.ToBoolean(appSettings["active6OnBool"]);
                active7OnBool = Convert.ToBoolean(appSettings["active7OnBool"]);
                wardOnBool = Convert.ToBoolean(appSettings["wardOnBool"]);
                wardHopOn = Convert.ToBoolean(appSettings["wardHopOn"]);
                qPreactivateW = Convert.ToBoolean(appSettings["qPreactivateW"]);
                qPreactivateE = Convert.ToBoolean(appSettings["qPreactivateE"]);
                qPreactivateR = Convert.ToBoolean(appSettings["qPreactivateR"]);
                wPreactivateQ = Convert.ToBoolean(appSettings["wPreactivateQ"]);
                wPreactivateE = Convert.ToBoolean(appSettings["wPreactivateE"]);
                wPreactivateR = Convert.ToBoolean(appSettings["wPreactivateR"]);
                ePreactivateQ = Convert.ToBoolean(appSettings["ePreactivateQ"]);
                ePreactivateW = Convert.ToBoolean(appSettings["ePreactivateW"]);
                ePreactivateR = Convert.ToBoolean(appSettings["ePreactivateR"]);
                rPreactivateQ = Convert.ToBoolean(appSettings["rPreactivateQ"]);
                rPreactivateW = Convert.ToBoolean(appSettings["rPreactivateW"]);
                rPreactivateE = Convert.ToBoolean(appSettings["rPreactivateE"]);
                wardHopKey = Convert.ToChar(appSettings["wardHopKey"]);
                activeKey = Convert.ToChar(appSettings["activeKey"]);
            }
            qValueText.Text = pressSpell1Interval.ToString();
            wValueText.Text = pressSpell2Interval.ToString();
            eValueText.Text = pressSpell3Interval.ToString();
            rValueText.Text = pressSpell4Interval.ToString();
            activeValueText.Text = pressActiveInterval.ToString();
            autoKeyOn.Checked = autoKeyOnBool;
            active1On.Checked = active1OnBool;
            active2On.Checked = active2OnBool;
            active3On.Checked = active3OnBool;
            active5On.Checked = active5OnBool;
            active6On.Checked = active6OnBool;
            active7On.Checked = active7OnBool;
            wardCheckbox.Checked = wardOnBool;
            wardHopCheckBox.Checked = wardHopOn;
            qActivateWCheckBox.Checked = qPreactivateW;
            qActivateECheckBox.Checked = qPreactivateE;
            qActivateRCheckBox.Checked = qPreactivateR;
            wActivateQCheckBox.Checked = wPreactivateQ;
            wActivateECheckBox.Checked = wPreactivateE;
            wActivateRCheckBox.Checked = wPreactivateR;
            eActivateQCheckBox.Checked = ePreactivateQ;
            eActivateWCheckBox.Checked = ePreactivateW;
            eActivateRCheckBox.Checked = ePreactivateR;
            rActivateQCheckBox.Checked = rPreactivateQ;
            rActivateWCheckBox.Checked = rPreactivateW;
            rActivateECheckBox.Checked = rPreactivateE;
            wardHopKeyComboBox.Text = wardHopKey.ToString();
            activeKeyComboBox.Text = activeKey.ToString();
            //End load state



            HookManager.KeyDown += HookManager_KeyDown;
            HookManager.KeyUp += HookManager_KeyUp;

            // The requested resolution in 100 ns units:
            int DesiredResolution = 1000;
            // Note: The supported resolutions can be obtained by a call to NtQueryTimerResolution()

            int CurrentResolution = 0;

            // 1. Requesting a higher resolution
            // Note: This call is similar to timeBeginPeriod.
            // However, it to to specify the resolution in 100 ns units.
            if (NtSetTimerResolution(DesiredResolution, true, out CurrentResolution) != 0)
            {
                Console.WriteLine("Setting resolution failed");
            }
            else
            {
                Console.WriteLine("CurrentResolution [100 ns units]: " + CurrentResolution);
            }

            ATimer.ElapsedTimerDelegate callback = timer_Tick;
            timer = new ATimer(3, 1, callback);
            timer.Start();

            activeKeyComboBox.Text = "E";
            wardHopKeyComboBox.Text = "Q";

            _autoIT.AutoItSetOption("SendKeyDelay", 0);
            _autoIT1.AutoItSetOption("SendKeyDelay", 0);
            _autoIT2.AutoItSetOption("SendKeyDelay", 0);
            _autoIT3.AutoItSetOption("SendKeyDelay", 0);
            _autoIT4.AutoItSetOption("SendKeyDelay", 0);

            _autoIT.AutoItSetOption("SendKeyDownDelay", 0);
            _autoIT1.AutoItSetOption("SendKeyDownDelay", 0);
            _autoIT2.AutoItSetOption("SendKeyDownDelay", 0);
            _autoIT3.AutoItSetOption("SendKeyDownDelay", 0);
            _autoIT4.AutoItSetOption("SendKeyDownDelay", 0);
        }
        void timer_Tick()
        {
            //Ward hop
            if (keyTPressed && wardHopOn)
            {
                //Place ward
                if (wardHopStopwatch.ElapsedMilliseconds >= 1000)
                {
                    wardHopStopwatch.Restart();
                    tapWard();
                }
                //Try to hop
                if (wardHopKey == 'Q') preactivateQ(pressSpell1Interval);
                if (wardHopKey == 'W') preactivateW(pressSpell2Interval);
                if (wardHopKey == 'E') preactivateE(pressSpell3Interval);
                if (wardHopKey == 'R') preactivateR(pressSpell4Interval);
            }
                if (pressingSpell1)
                {
                    if (autoKeyOnBool)
                    {
                    if (qPreactivateW) preactivateW(pressSpell1Interval);
                    if (qPreactivateE) preactivateE(pressSpell1Interval);
                    if (qPreactivateR) preactivateR(pressSpell1Interval);
                    preactivateQ(pressSpell1Interval);
                    }
                    if (activeKey == 'Q')
                    {
                        runActives();
                    }
                }
                if (pressingSpell2)
                {
                    if (autoKeyOnBool)
                    {
                    if (wPreactivateQ) preactivateQ(pressSpell2Interval);
                    if (wPreactivateE) preactivateE(pressSpell2Interval);
                    if (wPreactivateR) preactivateR(pressSpell2Interval);
                    preactivateW(pressSpell2Interval);
                    }
                    if (activeKey == 'W')
                    {
                        runActives();
                    }
                }
                if (pressingSpell3)
                {
                    if (autoKeyOnBool)
                    {
                    if (ePreactivateQ) preactivateQ(pressSpell3Interval);
                    if (ePreactivateW) preactivateW(pressSpell3Interval);
                    if (ePreactivateR) preactivateR(pressSpell3Interval);
                    preactivateE(pressSpell3Interval);
                    }
                    if (activeKey == 'E')
                    {
                        runActives();
                    }
                }
                if (pressingSpell4)
                {
                    if (autoKeyOnBool)
                    {
                    if (rPreactivateQ) preactivateQ(pressSpell4Interval);
                    if (rPreactivateW) preactivateW(pressSpell4Interval);
                    if (rPreactivateE) preactivateE(pressSpell4Interval);
                    preactivateR(pressSpell4Interval);
                    }
                    if (activeKey == 'R')
                    {
                        runActives();
                    }
                }
                
        }
        void preactivateQ(int interval)
        {
            if (spell1Stopwatch.ElapsedMilliseconds >= interval)
            {
                spell1Stopwatch.Restart();
                tapSpell1();
            }
        }
        void preactivateW(int interval)
        {
            if (spell2Stopwatch.ElapsedMilliseconds >= interval)
            {
                spell2Stopwatch.Restart();
                tapSpell2();
            }
        }
        void preactivateE(int interval)
        {
            if (spell3Stopwatch.ElapsedMilliseconds >= interval)
            {
                spell3Stopwatch.Restart();
                tapSpell3();
            }
        }
        void preactivateR(int interval)
        {
            if (spell4Stopwatch.ElapsedMilliseconds >= interval)
            {
                spell4Stopwatch.Restart();
                tapSpell4();
            }
        }

        void runActives()
        {
            if (activeStopwatch.ElapsedMilliseconds >= pressActiveInterval)
            {
                activeStopwatch.Restart();
                if (active1OnBool)
                {
                    tapActive1();
                }
                if (active2OnBool)
                {
                    tapActive2();
                }
                if (active3OnBool)
                {
                    tapActive3();
                }
                if (active5OnBool)
                {
                    tapActive5();
                }
                if (active6OnBool)
                {
                    tapActive6();
                }
                if (active7OnBool)
                {
                    tapActive7();
                }
            }
            if (pressingWardStopwatch.ElapsedMilliseconds >= pressWardInterval && wardOnBool)
            {
                pressingWardStopwatch.Restart();
                tapWard();
            }
        }

        void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //bool go = false;
            if (e.KeyCode == Keys.T && keyTPressed)
            { //T key
                keyTPressed = false;
                wardHopStopwatch.Restart();
                //go = true;
            }
            if (e.KeyCode == Keys.Q) // && keyQPressed
            { //Q
                if (autoKeyOnBool)
                {
                    keyQCountUp++;
                    if (keyQCountUp > spell1Sent && spell1Sent != 0)
                    {
                        pressingSpell1 = false;
                        keyQCountUp = 0;
                        spell1Sent = 0;
                    }
                }
                else
                {
                    pressingSpell1 = false;
                }
            }
            if (e.KeyCode == Keys.W)
            { //W
                if (autoKeyOnBool)
                {
                    keyWCountUp++;
                    if (keyWCountUp > spell2Sent && spell2Sent != 0)
                    {
                        pressingSpell2 = false;
                        keyWCountUp = 0;
                        spell2Sent = 0;
                    }
                }
                else
                {
                    pressingSpell2 = false;
                }
            }
            if (e.KeyCode == Keys.E)
            { //E
                if (autoKeyOnBool)
                {
                    keyECountUp++;
                    if (keyECountUp > spell3Sent && spell3Sent != 0)
                    {
                        pressingSpell3 = false;
                        keyECountUp = 0;
                        spell3Sent = 0;
                    }
                }
                else
                {
                    pressingSpell3 = false;
                }
            }
            if (e.KeyCode == Keys.R)
            { //R
                if (autoKeyOnBool)
                {
                    keyRCountUp++;
                    if (keyRCountUp > spell4Sent && spell4Sent != 0)
                    {
                        pressingSpell4 = false;
                        keyRCountUp = 0;
                        spell4Sent = 0;
                    }
                }
                else
                {
                    pressingSpell4 = false;
                }
            }
        }

        void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bool go = false;
            if (e.KeyCode == Keys.T && !keyTPressed)
            { //T key
                keyTPressed = true;
                go = true;
            }
            if (e.KeyCode == Keys.Q)
            { //Q
                if (!pressingSpell1)
                {
                    pressingSpell1 = true;
                    go = true;
                    keyQCountUp = 0;
                    spell1Sent = 0;
                }
            }
            if (e.KeyCode == Keys.W)
            { //W
                if (!pressingSpell2)
                {
                    pressingSpell2 = true;
                    go = true;
                    keyWCountUp = 0;
                    spell2Sent = 0;
                }
            }
            if (e.KeyCode == Keys.E && !pressingSpell3)
            { //E
                if (!pressingSpell3)
                {
                    pressingSpell3 = true;
                    go = true;
                    keyECountUp = 0;
                    spell3Sent = 0;
                }
            }
            if (e.KeyCode == Keys.R && !pressingSpell4)
            { //R
                if (!pressingSpell4)
                {
                    pressingSpell4 = true;
                    go = true;
                    keyRCountUp = 0;
                    spell4Sent = 0;
                }
            }
            if (go)
            {
                runLogicPress();
            }
        }
        void runLogicPress()
        {
            timer_Tick();
        }
        void tapSpell1()
        {
            spell1Sent++;
            Task.Factory.StartNew(() =>
            {
                _autoIT1.Send("q");
            });
        }
        void tapSpell2()
        {
            spell2Sent++;
            Task.Factory.StartNew(() =>
            {
                _autoIT2.Send("w");
            });
        }
        void tapSpell3()
        {
            spell3Sent++;
            Task.Factory.StartNew(() =>
            {
                _autoIT3.Send("e");
            });
        }
        void tapSpell4()
        {
            spell4Sent++;
            Task.Factory.StartNew(() =>
            {
                _autoIT4.Send("r");
            });
        }
        void tapActive1()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("1");
            });
        }
        void tapActive2()
        {
                Task.Factory.StartNew(() =>
                {
                    _autoIT.Send("2");
                });
        }
        void tapActive3()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("3");
            });
        }
        void tapWard()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("4");
            });
        }
        void tapActive5()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("5");
            });
        }
        void tapActive6()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("6");
            });
        }
        void tapActive7()
        {
            Task.Factory.StartNew(() => {
                _autoIT.Send("7"); 
            });
        }

        private void active1On_CheckedChanged(object sender, EventArgs e)
        {
            active1OnBool = ((CheckBox)sender).Checked;
        }

        private void active2On_CheckedChanged(object sender, EventArgs e)
        {
            active2OnBool = ((CheckBox)sender).Checked;
        }

        private void active3On_CheckedChanged(object sender, EventArgs e)
        {
            active3OnBool = ((CheckBox)sender).Checked;
        }

        private void autoKeyOn_CheckedChanged(object sender, EventArgs e)
        {
            autoKeyOnBool = ((CheckBox)sender).Checked;

            pressingSpell1 = false;
            pressingSpell2 = false;
            pressingSpell3 = false;
            pressingSpell4 = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Stop();
            //Save state
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings.Clear();
            settings.Add("pressSpell1Interval", pressSpell1Interval.ToString());
            settings.Add("pressSpell2Interval", pressSpell2Interval.ToString());
            settings.Add("pressSpell3Interval", pressSpell3Interval.ToString());
            settings.Add("pressSpell4Interval", pressSpell4Interval.ToString());
            settings.Add("pressActiveInterval", pressActiveInterval.ToString());
            settings.Add("autoKeyOnBool", autoKeyOnBool.ToString());
            settings.Add("active1OnBool", active1OnBool.ToString());
            settings.Add("active2OnBool", active2OnBool.ToString());
            settings.Add("active3OnBool", active3OnBool.ToString());
            settings.Add("active5OnBool", active5OnBool.ToString());
            settings.Add("active6OnBool", active6OnBool.ToString());
            settings.Add("active7OnBool", active7OnBool.ToString());
            settings.Add("wardHopOn", wardHopOn.ToString());
            settings.Add("wardOnBool", wardOnBool.ToString());
            settings.Add("qPreactivateW", qPreactivateW.ToString());
            settings.Add("qPreactivateE", qPreactivateE.ToString());
            settings.Add("qPreactivateR", qPreactivateR.ToString());
            settings.Add("wPreactivateQ", wPreactivateQ.ToString());
            settings.Add("wPreactivateE", wPreactivateE.ToString());
            settings.Add("wPreactivateR", wPreactivateR.ToString());
            settings.Add("ePreactivateQ", ePreactivateQ.ToString());
            settings.Add("ePreactivateW", ePreactivateW.ToString());
            settings.Add("ePreactivateR", ePreactivateR.ToString());
            settings.Add("rPreactivateQ", rPreactivateQ.ToString());
            settings.Add("rPreactivateW", rPreactivateW.ToString());
            settings.Add("rPreactivateE", rPreactivateE.ToString());
            settings.Add("wardHopKey", wardHopKey.ToString());
            settings.Add("activeKey", activeKey.ToString());
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            Application.Exit();
            Environment.Exit(0);
        }

        private void qValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell1Interval = Convert.ToInt32(qValueText.Text);
        }

        private void wValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell2Interval = Convert.ToInt32(wValueText.Text);
        }

        private void eValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell3Interval = Convert.ToInt32(eValueText.Text);
        }

        private void rValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell4Interval = Convert.ToInt32(rValueText.Text);
        }

        private void activeValueText_TextChanged(object sender, EventArgs e)
        {
            pressActiveInterval = Convert.ToInt32(activeValueText.Text);
        }

        private void active5On_CheckedChanged(object sender, EventArgs e)
        {
            active5OnBool = ((CheckBox)sender).Checked;
        }

        private void active6On_CheckedChanged(object sender, EventArgs e)
        {
            active6OnBool = ((CheckBox)sender).Checked;
        }

        private void active7On_CheckedChanged(object sender, EventArgs e)
        {
            active7OnBool = ((CheckBox)sender).Checked;
        }

        private void wardCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            wardOnBool = ((CheckBox)sender).Checked;
        }

        private void activeKeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeKey = activeKeyComboBox.Text.ToCharArray()[0];
        }

        private void qActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            qPreactivateW = ((CheckBox)sender).Checked;
        }

        private void qActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            qPreactivateE = ((CheckBox)sender).Checked;
        }

        private void qActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            qPreactivateR = ((CheckBox)sender).Checked;
        }

        private void wActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            wPreactivateQ = ((CheckBox)sender).Checked;
        }

        private void wActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            wPreactivateE = ((CheckBox)sender).Checked;
        }

        private void wActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            wPreactivateR = ((CheckBox)sender).Checked;
        }

        private void eActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ePreactivateQ = ((CheckBox)sender).Checked;
        }

        private void eActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ePreactivateW = ((CheckBox)sender).Checked;
        }

        private void eActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ePreactivateR = ((CheckBox)sender).Checked;
        }

        private void rActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rPreactivateQ = ((CheckBox)sender).Checked;
        }

        private void rActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rPreactivateW = ((CheckBox)sender).Checked;
        }

        private void rActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rPreactivateE = ((CheckBox)sender).Checked;
        }

        private void wardHopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            wardHopOn = ((CheckBox)sender).Checked;
        }

        private void wardHopKeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            wardHopKey = wardHopKeyComboBox.Text.ToCharArray()[0];
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    
}
