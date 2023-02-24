﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.IO;
using Npgsql;

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
        private Boolean m_blnUploading = false;
        private System.IO.StreamWriter m_swLog;

        private void ControlForm_Load(object sender, EventArgs e)
        {
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            try
            {
                sp.Open();
            }
            catch (Exception ex)
            {
                SetRTBText(rtb, "WARNING: could not open Harmonia port: " + ex.Message + Environment.NewLine);
                //MessageBox.Show("Could not open Harmonia port: " + ex.Message, "Port Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            sp_1.DataReceived += new SerialDataReceivedEventHandler(sp_1_DataReceived);
            try
            {
                sp_1.Open();
            }
            catch (Exception ex)
            {
                SetRTBText(rtb, "WARNING: could not open Range Finder port: " + ex.Message + Environment.NewLine);
                //MessageBox.Show("Could not open Range Finder port: " + ex.Message,"Port Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                            if (intMetadataID == 21) { SetControlText(this.meta_id_21, strValue); }
                            if (intMetadataID == 14) { SetControlText(this.meta_id_14, strValue); }
                            if (intMetadataID == 15) { SetControlText(this.meta_id_15, strValue); }
                            if (intMetadataID == 16) { SetControlText(this.meta_id_16, strValue); }
                            if (intMetadataID == 22) { SetControlText(this.meta_id_22, strValue); }
                            if (intMetadataID == 23) { SetControlText(this.meta_id_23, strValue); }
                            if (intMetadataID == 24) { SetControlText(this.meta_id_24, strValue); }
                            if (intMetadataID == 25) { SetControlText(this.meta_id_25, strValue); }
                            if (intMetadataID == 26) { SetControlText(this.meta_id_26, strValue); }

                        }

                        if (m_blnUploading)
                        {
                            //store record in current data file
                            m_swLog.WriteLine(strReceived);
                            m_swLog.Flush();
                        }

                    }
                }

                //log everything to the rich textbox
                //Console.WriteLine(txt);
                SetRTBText(rtb, strReceived);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                SetRTBText(rtb, ex.ToString());
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
            sp.WriteLine("STATIC_TRIM,0.1");
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

        

        private void cboBatteryPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            sp.WriteLine("PUSHROD," + this.cboBatteryPos.Text);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Putting the system into Upload pause operation and upload SD Card data to a file - do you want to continue?","Continue?",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                //have the file ready to go
                m_swLog = new System.IO.StreamWriter(@"C:\Business\Submarine\subjects\research_project\data\HARMONIA_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log", true);

                m_blnUploading = true;

                //change state to upload
                sp.WriteLine("UPLOAD,0");
                
              
            }
         
        }

        private void btnInterchangeToExcel_Click(object sender, EventArgs e)
        {
            try
            {

                NpgsqlConnection connPG = new NpgsqlConnection("Server=localhost;Port=5432;UserId=postgres;Password=euge;Database=example");
                connPG.Open();

                NpgsqlCommand commPG = new NpgsqlCommand("select metadata_id,data_label from dt_data_config order by metadata_id", connPG);
                NpgsqlDataReader readerPG = commPG.ExecuteReader();

                Hashtable hashDataLabels = new Hashtable();
                while (readerPG.Read())
                {
                    object objMetaDataID = readerPG.GetValue(0);
                    object objLabel = readerPG.GetValue(1);
                    hashDataLabels.Add(int.Parse(objMetaDataID.ToString()), objLabel.ToString());
                }
                readerPG.Close();
                commPG.Dispose();
                connPG.Close();


                StreamWriter swOut = new System.IO.StreamWriter(@"C:\Business\Submarine\subjects\research_project\data\HARMONIA_06022023.csv", false);

                StreamReader reader = File.OpenText(@"C:\Business\Submarine\subjects\research_project\data\HARMONIA_06022023.LOG");
                DateTime dteCurrent = DateTime.ParseExact("17:45:0 6/2/2023", "H:m:s d/M/yyyy", null); ;
                DateTime dtePrevious = DateTime.Now;
                int intRecord = 1;

                String strFirstHeader = "";

                while (reader.Peek() != -1)
                {

                    //read a line and split it up
                    String strLine = reader.ReadLine().Trim().TrimStart('{').TrimEnd('}');
                    String[] arrayLine = strLine.Split(new char[] { ',' }, StringSplitOptions.None);

                    string strCSV = "";
                    string strComma = "";

                    string strHeader = "";


                    foreach (string strDataValue in arrayLine)
                    {
                        String[] arrayParts = strDataValue.Split(new char[] { '|' }, StringSplitOptions.None);

                        string strMetaID = arrayParts[0].Trim();
                        string strValue = arrayParts[1].Trim();

                        int intMetaID = int.Parse(strMetaID);

                        if (hashDataLabels.ContainsKey(intMetaID))
                        {

                            string strLabel = hashDataLabels[intMetaID].ToString();

                            if (strMetaID == "13")
                            {
                                DateTime dteThis = DateTime.ParseExact(strValue, "H:m:s d/M/yyyy", null); //16:56:13 5/2/2023
                                                                                                          //DateTime dteLoc = dteThis.ToLocalTime();
                                                                                                          //DateTime dteUTM = dteLoc.ToUniversalTime();

                                if (intRecord > 1)
                                {
                                    TimeSpan ts = dteThis - dtePrevious;
                                    if (ts.TotalSeconds >= 0)
                                    {
                                        //Console.WriteLine(ts.TotalSeconds.ToString() + " gap at " + dteThis.ToString());
                                        //MessageBox.Show(dteThis.ToString());

                                        dteCurrent = dteCurrent.AddSeconds(ts.TotalSeconds);

                                    }
                                    else
                                    {
                                        //time has been reset - just add a 5 minute can because sub was probably turned off
                                        dteCurrent = dteCurrent.AddMinutes(5);
                                    }
                                }

                                //Console.WriteLine(dteThis.ToString() + ">>" + dteCurrent.ToString());

                                strValue = dteCurrent.ToString();


                                dtePrevious = dteThis;
                            }

                            strHeader = strHeader + strComma + strLabel;
                            strCSV = strCSV + strComma + strValue;
                            strComma = ",";    
                        }
 
                    }

                    if (intRecord == 1)
                    {
                        strFirstHeader = strHeader;
                        swOut.WriteLine(strHeader);
                        swOut.WriteLine(strCSV);
                    }
                    else
                    {
                        if (strHeader == strFirstHeader)
                        {
                            swOut.WriteLine(strCSV);
                        } else
                        {
                            Console.WriteLine("WARNING: line skipped format of line is not consistent with other data lines: " + strLine);
                        }
                    }


                    


                    intRecord++;
                }
                reader.Close();
                swOut.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dteNow = DateTime.Now;
                string strParam = dteNow.Year.ToString() + "|" + dteNow.Month.ToString() + "|" + dteNow.Day.ToString() + "|" + dteNow.Hour.ToString() + "|" + dteNow.Minute.ToString() + "|" + dteNow.Second.ToString();

                sp.WriteLine("TIMESET," + strParam);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.rtb.Clear();
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            try
            {

                if (m_swLog != null)
                {
                    m_swLog.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
