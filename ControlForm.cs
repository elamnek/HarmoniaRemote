using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace HarmoniaRemote
{
    public partial class ControlForm : Form
    {
        public ControlForm()
        {
            InitializeComponent();
        }

        //create the serial connection
        private SerialPort sp = new SerialPort("COM4", 9600);
        private SerialPort sp_1 = new SerialPort("COM7", 115200);
        private void ControlForm_Load(object sender, EventArgs e)
        {
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            sp_1.DataReceived += new SerialDataReceivedEventHandler(sp_1_DataReceived);
            try
            {
                sp.Open();
                sp_1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void sp_1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string strReceived = sp_1.ReadLine();

                SetControlText(this.meta_id_20, strReceived);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string strReceived = sp.ReadLine();
                if (!strReceived.StartsWith("VMDPE"))
                {
                    if (strReceived.StartsWith("{"))
                    {
                        //this is a data packet - display each part in the textboxes
                        String strRaw = strReceived.Trim().TrimStart('{').TrimEnd('}');
                        String[] arrayRaw = strRaw.Split(',');
                        foreach (String strPart in arrayRaw)
                        {
                            String[] arrayValue = strPart.Trim().Split('|');
                            int intMetadataID = int.Parse(arrayValue[0].ToString());
                            String strValue = arrayValue[1].ToString();

                            if (intMetadataID == 13) { SetControlText(this.meta_id_13,strValue); }//rtc
                            if (intMetadataID == 4) {
                                //this is the state - check for alarm
                                SetControlText(this.meta_id_4,strValue);
                                
                            }
                            if (intMetadataID == 1) { SetControlText(this.meta_id_1,strValue); }
                            
                            //leak sensors
                            if (intMetadataID == 2) { 
                                SetControlText(this.meta_id_2, strValue);
                                if (strValue == "1")
                                {AlertUser(true);} 
                                else { AlertUser(false); }
                                
                            }
                            if (intMetadataID == 3) {
                                SetControlText(this.meta_id_3, strValue);
                                if (strValue == "1")
                                {AlertUser(true);}
                                else { AlertUser(false); }   
                            }
                            if (intMetadataID == 7) { SetControlText(this.meta_id_7,strValue); }
                            if (intMetadataID == 10) { SetControlText(this.meta_id_10,strValue); }
                            if (intMetadataID == 11) { SetControlText(this.meta_id_11,strValue); }
                            if (intMetadataID == 17) { SetControlText(this.meta_id_17, strValue); }
                            if (intMetadataID == 18) { SetControlText(this.meta_id_18, strValue); }
                            if (intMetadataID == 19) { SetControlText(this.meta_id_19, strValue); }

                        }

                    }
                    
                    //log everything to the rich textbox
                    //Console.WriteLine(txt);
                    SetRTBText(rtb, strReceived);

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void AlertUser(Boolean blnAlertON)
        {
            try
            {
                if (blnAlertON)
                {
                    SetControlBackcolor(this, Color.Red);
                    if (this.chkBeep.Checked) { Console.Beep(500, 1000); }
                }
                else
                {
                    SetControlBackcolor(this, TextBox.DefaultBackColor);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void SetRTBText(Control rtb, string text)
        {
            rtb.Invoke((MethodInvoker)delegate { rtb.Text = rtb.Text +  text; });
        }
        public static void SetControlText(Control ctl, string text)
        {
            ctl.Invoke((MethodInvoker)delegate { ctl.Text = text; });
        }
        public static void SetControlBackcolor(Control ctl, Color bcol)
        {
            ctl.Invoke((MethodInvoker)delegate { ctl.BackColor = bcol; });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sp.WriteLine(this.textBox1.Text);
        }

        private void btnDeflate_Click(object sender, EventArgs e)
        {
            sp.WriteLine("DEFLATE,255");
        }

        private void btnInflate_Click(object sender, EventArgs e)
        {
            sp.WriteLine("INFLATE,255");
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            sp.WriteLine("FORWARD,100");
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            sp.WriteLine("REVERSE,100");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //write
            sp.WriteLine("PROPELL," + this.trackBar1.Value.ToString());
            int intVal = this.trackBar1.Value;
            intVal = -90 * (90 - intVal)/100;
            this.lblThrottle.Text = intVal.ToString() + "%";
        }

        private void tbFwdDive_Scroll(object sender, EventArgs e)
        {
            sp.WriteLine("SERVOFWDDIVE," + this.tbFwdDive.Value.ToString());
            this.lblFwdDive.Text = this.tbFwdDive.Value.ToString();
        }

        private void btnIdle_Click(object sender, EventArgs e)
        {
            sp.WriteLine("IDLE,0");
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            sp.WriteLine("MANUAL,0");
        }

        private void btnStaticTrim_Click(object sender, EventArgs e)
        {
            sp.WriteLine("STATIC_TRIM,0");
        }

        private void btnDynamicTrim_Click(object sender, EventArgs e)
        {
            sp.WriteLine("DYNAMIC_TRIM,0");
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            sp.WriteLine("ALARM,0");
        }

        private void btnStopPump_Click(object sender, EventArgs e)
        {
            sp.WriteLine("INFLATE,0");
        }

        private void tbAftDive_Scroll(object sender, EventArgs e)
        {
            sp.WriteLine("SERVOAFTDIVE," + this.tbAftDive.Value.ToString());
            this.lblAftDive.Text = this.tbAftDive.Value.ToString();
        }

        private void tbRudder_Scroll(object sender, EventArgs e)
        {
            sp.WriteLine("SERVOAFTRUDDER," + this.tbRudder.Value.ToString());
            this.lblRudder.Text = this.tbRudder.Value.ToString();
        }

        private void tbPushrod_Scroll(object sender, EventArgs e)
        {
            sp.WriteLine("PUSHROD," + this.tbPushrod.Value.ToString());
        }
    }
}
