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
using System.Diagnostics;

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
        //private SerialPort sp_1 = new SerialPort("COM12", 9600);
        //private SerialPort sp_1 = new SerialPort("COM6", 9600);//,Parity.None,8,StopBits.One
        
        private Boolean m_blnUploading = false;
        private int m_intRecordsToUpload = 0;
        private int m_intRecordsUploaded = 0;
        private string m_strUploadFile = "";
        private System.IO.StreamWriter m_swLog;
        private System.IO.StreamWriter m_swRangeDataFile;
        private ArrayList m_listRealtimeDataTableColDefs = null;
        private NpgsqlConnection m_connPG = null;

        private void ControlForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtFwdDive0Pos.Text = Properties.Settings.Default.FwdDive0Pos;
                txtAftRudder0Pos.Text = Properties.Settings.Default.AftRudder0Pos;
                txtAftPitch0Pos.Text = Properties.Settings.Default.AftPitch0Pos;
                txtRunNum.Text = Properties.Settings.Default.RunNum;

                cboCommand.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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
            
            //sp_1.DataReceived += new SerialDataReceivedEventHandler(sp_1_DataReceived);
            //try
            //{
            //    sp_1.Open();
            //}
            //catch (Exception ex)
            //{
            //    SetRTBText(rtb, "WARNING: could not open Range Finder port: " + ex.Message + Environment.NewLine);
            //    //MessageBox.Show("Could not open Range Finder port: " + ex.Message,"Port Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //}
        }
        private void ControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Properties.Settings.Default.FwdDive0Pos = txtFwdDive0Pos.Text;
                Properties.Settings.Default.AftRudder0Pos = txtAftRudder0Pos.Text;
                Properties.Settings.Default.AftPitch0Pos = txtAftPitch0Pos.Text;
                Properties.Settings.Default.RunNum = txtRunNum.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //private void sp_1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        string strReceived = sp_1.ReadLine().Trim();
        //        SetRTBText(rtb, strReceived);
        //        //if (strReceived.StartsWith("{") && strReceived.EndsWith("}"))
        //        //{
        //        //    //SetRTBText(rtb, strReceived + Environment.NewLine);
        //        //    String[] arrayValue = strReceived.Trim().TrimStart('{').TrimEnd('}').Split(',');
        //        //    string strRange = arrayValue[1];
        //        //    String[] arrayRange = strRange.Trim().Split('|');
        //        //    string strRangeValue = arrayRange[1];
        //        //    SetControlText(this.meta_id_20, strRangeValue);

        //        //    if (m_swRangeDataFile != null)
        //        //    {
        //        //        m_swRangeDataFile.WriteLine(strReceived);
        //        //        m_swRangeDataFile.Flush();
        //        //    }

        //        //    //do the insert into the postgres realtime table here
        //        //    RealtimeInsertIntoDT(strReceived, 3, m_listRealtimeDataTableColDefs);
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                int intRunNum = int.Parse(txtRunNum.Text);

                string strReceived = sp.ReadLine();
                if (!strReceived.StartsWith("VMDPE"))
                {
                    if (strReceived.StartsWith("{"))
                    {

                        ArrayList listMetadataIDs = new ArrayList();

                        //this is a data packet - display each part in the textboxes
                        String strRaw = strReceived.Trim().TrimStart('{').TrimEnd('}');
                        String[] arrayRaw = strRaw.Split(',');
                        foreach (String strPart in arrayRaw)
                        {
                            String[] arrayValue = strPart.Trim().Split('|');
                            int intMetadataID = int.Parse(arrayValue[0].ToString());
                            listMetadataIDs.Add(intMetadataID);

                            String strValue = arrayValue[1].ToString();

                            //search for the matching control and write the value to it
                            string strControlName = "meta_id_" + intMetadataID.ToString();
                            if (this.groupBox3.Controls.ContainsKey(strControlName))
                            {
                                Control ctr = this.groupBox3.Controls[strControlName];
                                SetControlText(ctr, strValue);
                            }

                            //check the state and if it has been changed to idle - check for range finder log and close it
                            //this is because when run is complete onboard system will change to idle state
                            if (intMetadataID == 4)
                            {
                                if (strValue == "IDLE")
                                {
                                    if (m_swRangeDataFile != null)
                                    {
                                        m_swRangeDataFile.Close();
                                        m_swRangeDataFile = null;
                                    }
                                } 
                            }

                            //convert heading to direction -180 - +180
                            if (intMetadataID == 14)
                            {
                                double dblHeading;
                                if (double.TryParse(strValue,out dblHeading)){
                                    double dblDirection = dblHeading;
                                    if (dblDirection > 180) { dblDirection = dblDirection - 360; }
                                    SetControlText(this.txtDirection, dblDirection.ToString());
                                }
                            }

                            //if (intMetadataID == 13) { SetControlText(this.meta_id_13,strValue); }//rtc
                            //if (intMetadataID == 4) {
                            //    //this is the state - check for alarm
                            //    SetControlText(this.meta_id_4,strValue);

                            //}
                            //if (intMetadataID == 1) { SetControlText(this.meta_id_1,strValue); }

                            //leak sensors
                            if (intMetadataID == 2) { 
                                //SetControlText(this.meta_id_2, strValue);
                                if (strValue == "1")
                                {AlertUser(true, Color.Red);} 
                                else { AlertUser(false, TextBox.DefaultBackColor); }
                                
                            }
                            if (intMetadataID == 3) {
                                //SetControlText(this.meta_id_3, strValue);
                                if (strValue == "1")
                                {AlertUser(true, Color.Red);}
                                else { AlertUser(false, TextBox.DefaultBackColor); }   
                            }
                            
                            //low voltage warning
                            if (intMetadataID == 23)
                            {
                                double dblBusVoltage = double.Parse(strValue);
                                if (dblBusVoltage < 7 && dblBusVoltage > 0)
                                { AlertUser(true, Color.Blue); }
                                else { AlertUser(false, TextBox.DefaultBackColor); }
                            }
                            
                        }

                        //do the insert into the postgres realtime table here
                        if (listMetadataIDs.Contains(29))
                        {
                            //29 is a control plane position value - load this record into channel 2 (4Hz data)
                            RealtimeInsertIntoDT(strReceived, 2, intRunNum, m_listRealtimeDataTableColDefs);
                        } else
                        {


                            RealtimeInsertIntoDT(strReceived, 1, intRunNum, m_listRealtimeDataTableColDefs);
                        }
      
                        if (m_blnUploading)
                        {
                            //store record in current data file
                            m_swLog.WriteLine(strReceived);
                            m_swLog.Flush();
                            m_intRecordsUploaded++;
                        }

                    } else if (strReceived.StartsWith("records|")){
                        if (m_blnUploading)
                        {
                            //this is the record count
                            String[] arrayRaw = strReceived.Split('|');
                            String strRecords = arrayRaw[1].ToString();
                            m_intRecordsToUpload = int.Parse(strRecords);
                            SetRTBText(rtb, "records to upload = " + m_intRecordsToUpload.ToString());
                        }
                    }
                    else if (strReceived.StartsWith("upload|done")){
                        if (m_blnUploading)
                        {
                            //upload has finished
                            MessageBox.Show("Upload has completed " + m_intRecordsUploaded.ToString() + " of " + m_intRecordsToUpload.ToString() + " records were uploaded to: " + m_strUploadFile, "Upload complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            m_swLog.Close();
                            m_blnUploading = false;

                            //change state to idle
                            sp.WriteLine("IDLE,0");
                        }
                    } else
                    {
                        //for any other message just log to RTB
                        SetRTBText(rtb, strReceived);
                    }

                    //log everything to the rich textbox
                    //Console.WriteLine(txt);
                    //SetRTBText(rtb, strReceived);

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                SetRTBText(rtb, ex.ToString());
            }
        }
        
        private void AlertUser(Boolean blnAlertON,Color col)
        {
            try
            {
                if (blnAlertON)
                {
                    SetControlBackcolor(this, col);
                    if (this.chkBeep.Checked) { Console.Beep(500, 1000); }
                }
                else
                {
                    SetControlBackcolor(this, col);
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
            string strSend = cboCommand.SelectedItem.ToString() + "," + this.txtCommandParam.Text;
            sp.WriteLine(strSend);
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
            
        }

        private void tbFwdDive_Scroll(object sender, EventArgs e)
        {
            sp.WriteLine("SERVOFWDDIVE," + this.tbFwdDive.Value.ToString());        
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
            sp.WriteLine("STATIC_TRIM," + this.txtDepthSetpoint.Text);
        }

        private void btnCalibrateIMU_Click(object sender, EventArgs e)
        {
            sp.WriteLine("CALIBRATE_IMU,0");
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
        }

        private void tbRudder_Scroll(object sender, EventArgs e)
        {
            sp.WriteLine("SERVOAFTRUDDER," + this.tbRudder.Value.ToString());    
        }

        

        private void cboBatteryPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            sp.WriteLine("PUSHROD," + this.cboBatteryPos.Text);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //do checks
            if (txtDataDir.Text.Length == 0)
            {
                MessageBox.Show("Log path has not been set", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (File.Exists(txtDataDir.Text))
            {
                MessageBox.Show("The log file already exists", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Putting the system into Upload pause operation and upload SD Card data to a file - do you want to continue?","Continue?",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                //have the file ready to go
                m_strUploadFile = txtDataDir.Text;
                m_swLog = new System.IO.StreamWriter(m_strUploadFile, true);

                m_blnUploading = true;

                //change state to upload
                sp.WriteLine("UPLOAD,0");
                       
            }
         
        }

        private void btnLoadIntoDT_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog fileBrowser = new OpenFileDialog();
                fileBrowser.Filter = "txt files (*.log)|*.log|All files (*.*)|*.*";
                fileBrowser.Title = "Specify a log file to load into DT database...";
                if (txtDataDir.Text.Length > 0)
                {
                    fileBrowser.InitialDirectory = txtDataDir.Text;
                }

                if (fileBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    string strFileName = fileBrowser.FileName;
                    LoadIntoDT(strFileName);
                    //MessageBox.Show(fileBrowser.FileName);
                }

                fileBrowser.Dispose();
                fileBrowser = null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void LoadIntoDT(string strInputFile)
        {
            try
            {

                //do checks
                if (txtDBConn.Text.Length == 0)
                {
                    MessageBox.Show("DT database connection has not been set", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                //get DT table defs
                Hashtable hashTableDefs = GetTableDefs("dest_table","dest_column");

                //open the logfile
                StreamReader reader = File.OpenText(strInputFile);

                string strInDateFormat = "H:m:s d/M/yyyy";
                string strOutDateFormat = "yyyy-MM-dd HH:mm:ss";
               
                //handle sub second date format
                if (strInputFile.EndsWith("_subsecond.LOG")){
                    strInDateFormat = "H:m:s.f d/M/yyyy";
                    strOutDateFormat = "yyyy-MM-dd HH:mm:ss.f";
                }

                int intRecord = 1;
                while (reader.Peek() != -1)
                {

                    //Console.WriteLine(intRecord.ToString());

                    //read a line and split it up
                    String strLine = reader.ReadLine().Trim().TrimStart('{').TrimEnd('}');
                    String[] arrayLine = strLine.Split(new char[] { ',' }, StringSplitOptions.None);

                    //get a hash of all data values
                    string strTime = "";
                    Hashtable hashInsertData = new Hashtable(); 
                    foreach (string strDataValue in arrayLine)
                    {
                        
                        String[] arrayParts = strDataValue.Split(new char[] { '|' }, StringSplitOptions.None);
                        if (arrayParts.Length == 2)
                        {
                            string strMetaID = arrayParts[0].Trim();
                            string strValue = arrayParts[1].Trim();
                            int intMetaID = int.Parse(strMetaID);
                            

                            if (intMetaID == 13)
                            {
                                //'2017-07-28 11:42:42.846621+00'
                               
                                DateTime dteThis = DateTime.ParseExact(strValue, strInDateFormat, null); //16:56:13 5/2/2023

                                //TimeZoneInfo myZone = TimeZoneInfo.Local;
                                //myZone.IsDaylightSavingTime(dteThis);
                                
                                //DateTime dtUTC = TimeZoneInfo.ConvertTimeToUtc(dteThis, myZone);

                                //DateTime dteLoc = dteThis.ToLocalTime();
                                //DateTime dteUTM = dteLoc.ToUniversalTime();

                                //datetimes need to be inserted in the local timezone
                                //the postgres timestamptz field type is time zone aware and assumes any insert datetime is local
                                //it will store the actual datetime as utc, but whenever it is queried using sql the local time will be returned
                                //the postgres database timezone is stored against the postgres server properties and this is used to define
                                //what timezone the data is displayed in
                                strTime = "'" + dteThis.ToString(strOutDateFormat) + "'";
                            } 
                            else
                            {
                                if (!hashInsertData.ContainsKey(intMetaID)) { hashInsertData.Add(intMetaID, strValue); }
                            }
                        }
                    }


                    if (strTime.Length > 0)
                    {
                        ArrayList listSQLInserts = new ArrayList();
                        //add the time series master record
                        listSQLInserts.Add("insert into dt_ts_master (time)" + " values ("+ strTime + ") ON CONFLICT DO NOTHING");

                        foreach (string strDestTable in hashTableDefs.Keys)
                        {
                            //this is a destination table
                            string strColumns = "time";
                            string strValues = strTime;

                            //go through columns and build sql
                            ArrayList listColDefs = (ArrayList)hashTableDefs[strDestTable];
                            foreach (Hashtable hashColDef in listColDefs)
                            {
                                int intThisMetaID = (int)hashColDef["METADATAID"];
                                if (hashInsertData.ContainsKey(intThisMetaID))
                                {
                                    //the log record contains this metadata id
                                                                       
                                    string strValue = hashInsertData[intThisMetaID].ToString();
                                    int intDQ = 1;

                                    string strColName = hashColDef["DESTCOL"].ToString();
                                    //Console.WriteLine(strColName);
                                    string strType = hashColDef["DESTCOLTYPE"].ToString();
                                    object objMinValue = hashColDef["MINVAL"];
                                    object objMaxValue = hashColDef["MAXVAL"];
                                    if (strType == "str")
                                    {
                                        if (strValue.Length > 0)
                                        {
                                            strValue = "'" + strValue + "'";
                                        }   
                                    } else if (strType == "int")
                                    {
                                        int intValue;
                                        if (int.TryParse(strValue, out intValue))
                                        {
                                            strValue = intValue.ToString();

                                            //check min and max
                                            if (objMinValue != DBNull.Value){ if (intValue < double.Parse(objMinValue.ToString())) { intDQ = 2;}}
                                            if (objMaxValue != DBNull.Value) { if (intValue > double.Parse(objMaxValue.ToString())) { intDQ = 2; } }

                                        } else { strValue = ""; }
                                    } else if (strType == "dbl")
                                    {
                                        double dblValue;
                                        if (double.TryParse(strValue, out dblValue))
                                        {
                                            strValue = dblValue.ToString();

                                            //check min and max
                                            if (objMinValue != DBNull.Value) { if (dblValue < double.Parse(objMinValue.ToString())) { intDQ = 2; } }
                                            if (objMaxValue != DBNull.Value) { if (dblValue > double.Parse(objMaxValue.ToString())) { intDQ = 2; } }

                                        } else { strValue = ""; }
                                    }

                                    if (strValue.Length > 0)
                                    {
                                        
                                        strColumns = strColumns + "," + strColName + "," + strColName + "_dq";
                                        strValues = strValues + "," + strValue + "," + intDQ.ToString();
                                    }    
                                }

                            }

                            if (strColumns != "time")
                            {
                                //build sql insert for this table
                                listSQLInserts.Add("insert into " + strDestTable + " (" + strColumns + ")" + " values (" + strValues + ") ON CONFLICT DO NOTHING");
                            }        
                        }

                        //do the inserts for this record (time point)
                        PGInsert(listSQLInserts);

                    }

                    intRecord++;
                }
                reader.Close();

                MessageBox.Show("Load complete!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private Hashtable GetDataLabels() {
            try
            {
                Hashtable hashDataLabels = new Hashtable();

                //get the metadata records
                NpgsqlCommand commPG = new NpgsqlCommand("select metadata_id,data_label from dt_data_config order by metadata_id", m_connPG);
                NpgsqlDataReader readerPG = commPG.ExecuteReader();  
                while (readerPG.Read())
                {
                    object objMetaDataID = readerPG.GetValue(0);
                    object objLabel = readerPG.GetValue(1);
                    hashDataLabels.Add(int.Parse(objMetaDataID.ToString()), objLabel.ToString());
                }
                readerPG.Close();
                commPG.Dispose();

                return hashDataLabels;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        private Hashtable GetTableDefs(string strDestTableCol,string strDestColCol)
        {
            try
            {
                Hashtable hashTableDefs = new Hashtable();
    
                string strPreviousDestTable = "";

                ArrayList listColDefs = new ArrayList();

                //get the metadata records
                NpgsqlCommand commPG = new NpgsqlCommand("select "+ strDestTableCol + ","+ strDestColCol + ",expected_min_value,expected_max_value,metadata_id,dest_column_type from dt_data_config order by " + strDestTableCol, m_connPG);
                NpgsqlDataReader readerPG = commPG.ExecuteReader();
                while (readerPG.Read())
                {
                    object objDestTable = readerPG.GetValue(0);
                    object objDestColumn = readerPG.GetValue(1);
                    object objMinVal = readerPG.GetValue(2);
                    object objMaxVal = readerPG.GetValue(3);
                    object objMetaDataID = readerPG.GetValue(4);
                    object objDestColumnType = readerPG.GetValue(5);

                    if (objDestTable != null && objDestColumn != null && objMetaDataID != null && objDestColumnType != null)
                    {
                        string strDestTable = objDestTable.ToString();
                        string strDestColumn = objDestColumn.ToString();
                        string strDestColumnType = objDestColumnType.ToString();

                        if (strDestTable.Length > 0 && strDestColumn.Length > 0 && strDestColumnType.Length > 0)
                        {

                            int intMetaDataID = int.Parse(objMetaDataID.ToString());

                            Hashtable hashColumnDef = new Hashtable();
                            hashColumnDef.Add("METADATAID", intMetaDataID);
                            hashColumnDef.Add("DESTCOL", strDestColumn);
                            hashColumnDef.Add("DESTCOLTYPE", strDestColumnType);
                            hashColumnDef.Add("MINVAL", objMinVal);
                            hashColumnDef.Add("MAXVAL", objMaxVal);

                            if (strDestTable == strPreviousDestTable || strPreviousDestTable.Length == 0)
                            {
                                //same table - keep adding columns to the list of cols
                                listColDefs.Add(hashColumnDef);

                            }
                            else
                            {
                                //new table - store the previous column list against the previous table name
                                hashTableDefs.Add(strPreviousDestTable, listColDefs);

                                //create a new column def list
                                listColDefs = new ArrayList();
                                listColDefs.Add(hashColumnDef);
                            }

                            strPreviousDestTable = strDestTable;
                        }    
                    }
                }
                //there will always be one last column def list - store this
                hashTableDefs.Add(strPreviousDestTable, listColDefs);

                readerPG.Close();
                commPG.Dispose();
                
                return hashTableDefs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        private void PGInsert(ArrayList listSQLInserts)
        {
            try
            {
                foreach (string strSQL in listSQLInserts)
                {
                    NpgsqlCommand commPG = new NpgsqlCommand(strSQL, m_connPG);
                    commPG.ExecuteNonQuery();
                    commPG.Dispose();
                }
      
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            SyncClocks();
        }
        private void SyncClocks()
        {

            DateTime dteNow = DateTime.Now;
            string strParam = dteNow.Year.ToString() + "|" + dteNow.Month.ToString() + "|" + dteNow.Day.ToString() + "|" + dteNow.Hour.ToString() + "|" + dteNow.Minute.ToString() + "|" + dteNow.Second.ToString();

            //try to set Harmonia time
            try
            {
                sp.WriteLine("TIMESET," + strParam);
            }
            catch (Exception ex)
            {
                SetRTBText(rtb, "Could not set time on harmonia: " + ex.Message + Environment.NewLine);
            }

            //try to set range finder time
            //try
            //{
            //    sp_1.WriteLine("TIMESET," + strParam);
            //}
            //catch (Exception ex)
            //{
            //    SetRTBText(rtb, "Could not set time on range finder: " + ex.Message + Environment.NewLine);
            //}

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

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {

                //sync the two clocks
                SyncClocks();

                //make sure data file directory has been set
                if (txtDataDir.Text.Length == 0)
                {
                    MessageBox.Show("Data file directory has not been set - need to set this before using the Run state", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //if (!Directory.Exists(txtDataDir.Text))
                //{
                //    MessageBox.Show("Data file directory does not exist - need to fix this before using the Run state", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                //need a pause between clock sync and run command, otherwise it doesn't get through
                System.Threading.Thread.Sleep(2000);

                string strParam = this.txtDepthSP.Text + "|" + this.txtPitchSP.Text + "|" + this.txtFwdThrottle.Text + "|" + this.txtFwdTime.Text + "|" + this.txtRevThrottle.Text + "|" + this.txtRevTime.Text + "|" + this.txtFwdDive0Pos.Text + "|" + this.txtAftPitch0Pos.Text + "|" + this.txtAftRudder0Pos.Text + "|" + this.txtTrimTime.Text + "|" + this.txtDirectionSP.Text + "|" + this.txtZigZagPeriod.Text;
                sp.WriteLine("RUN," + strParam);

                //string strOutLogFile = Path.Combine(txtDataDir.Text, "range_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log");
                //m_swRangeDataFile = new System.IO.StreamWriter(strOutLogFile, true);
   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {

                FolderBrowserDialog fbd = new FolderBrowserDialog();

                string strDataDir = this.txtDataDir.Text;
                if (strDataDir.Length > 0)
                {
                    if (Directory.Exists(strDataDir))
                    {
                        fbd.SelectedPath = strDataDir;
                    }
                }

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtDataDir.Text = fbd.SelectedPath;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                txtDirectionSP.Text = txtDirection.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                //sp.WriteLine("SERVOFWDDIVE," + this.txtFwdDive0Pos.Text);

                string strParam = this.txtFwdDive0Pos.Text + "|555|555";
                sp.WriteLine("SERVO_TEST," + strParam);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                //sp.WriteLine("SERVOAFTDIVE," + this.txtAftPitch0Pos.Text);

                string strParam = "555|" + this.txtAftPitch0Pos.Text + "|555";
                sp.WriteLine("SERVO_TEST," + strParam);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {


                //sp.WriteLine("SERVOAFTRUDDER," + this.txtAftRudder0Pos.Text);

                string strParam = "555|555|" + this.txtAftRudder0Pos.Text;
                sp.WriteLine("SERVO_TEST," + strParam);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog fileBrowser = new OpenFileDialog();
                fileBrowser.Filter = "txt files (*.log)|*.log|All files (*.*)|*.*";
                fileBrowser.Title = "Specify a log file to export to excel...";
                if (txtDataDir.Text.Length > 0)
                {
                    fileBrowser.InitialDirectory = txtDataDir.Text;
                }

                if (fileBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    string strFileName = fileBrowser.FileName;
                    ExportToExcel(strFileName);
                    //MessageBox.Show(fileBrowser.FileName);
                }

                fileBrowser.Dispose();
                fileBrowser = null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ExportToExcel(string strInputFile)
        {
            try
            {

                string strOutCSVFile = Path.Combine(Path.GetDirectoryName(strInputFile), Path.GetFileNameWithoutExtension(strInputFile) + ".csv");
                if (File.Exists(strOutCSVFile))
                {
                    MessageBox.Show("An output file already exists with the name: " + strOutCSVFile + " (delete this first before exporting)", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Hashtable hashDataLabels = GetDataLabels();
                
                //open the logfile
                StreamReader reader = File.OpenText(strInputFile);

                //open the output csv file
                StreamWriter swOut = new System.IO.StreamWriter(strOutCSVFile, false);

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
                        if (arrayParts.Length == 2)
                        {
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
                                    strValue = dteThis.ToString();

                                }

                                strHeader = strHeader + strComma + strLabel;
                                strCSV = strCSV + strComma + strValue;
                                strComma = ",";
                            }
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
                        }
                        else
                        {
                            SetRTBText(rtb, "WARNING: line skipped format of line is not consistent with other data lines: " + strLine);
                        }
                    }


                    intRecord++;
                }
                reader.Close();
                swOut.Close();
  
                MessageBox.Show("Export complete!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RealtimeInsertIntoDT(string strReceived, int intDataChannel, int intRunNum, ArrayList listColDefs)
        {
            try
            {

                if (listColDefs == null)
                {
                    return;
                }
               
                //read a line and split it up
                String strLine = strReceived.Trim().TrimStart('{').TrimEnd('}');
                String[] arrayLine = strLine.Split(new char[] { ',' }, StringSplitOptions.None);

                //get a hash of all data values
                string strTime = "";
                Hashtable hashInsertData = new Hashtable();
                foreach (string strDataValue in arrayLine)
                {

                    String[] arrayParts = strDataValue.Split(new char[] { '|' }, StringSplitOptions.None);
                    if (arrayParts.Length == 2)
                    {
                        string strMetaID = arrayParts[0].Trim();
                        string strValue = arrayParts[1].Trim();
                        int intMetaID = int.Parse(strMetaID);

                        if (intMetaID == 13)
                        {
                            int intMillis = -1;
                            if (strValue.Contains("#"))
                            {
                                //this is a sub second time with millis at the start - split this off
                                string[] arrayTime = strValue.Split(new char[] { '#' }, StringSplitOptions.None);
                                intMillis = int.Parse(arrayTime[0]);
                                strValue = arrayTime[1].Trim();
                            }


                            DateTime dteThis = DateTime.ParseExact(strValue, "H:m:s d/M/yyyy", null); //16:56:13 5/2/2023

                            //datetimes need to be inserted in the local timezone
                            //the postgres timestamptz field type is time zone aware and assumes any insert datetime is local
                            //it will store the actual datetime as utc, but whenever it is queried using sql the local time will be returned
                            //the postgres database timezone is stored against the postgres server properties and this is used to define
                            //what timezone the data is displayed in
                            strTime = "'" + dteThis.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        }
                        else
                        {
                            if (!hashInsertData.ContainsKey(intMetaID)) { hashInsertData.Add(intMetaID, strValue); }
                        }
                    }
                }


                if (strTime.Length > 0)
                {
                    ArrayList listSQLInserts = new ArrayList();

                    //this is a destination table
                    string strColumns = "time,data_channel,run_number";
                    string strValues = strTime + "," + intDataChannel.ToString() +  "," + intRunNum.ToString();

                    //go through columns and build sql 
                    foreach (Hashtable hashColDef in listColDefs)
                    {
                        int intThisMetaID = (int)hashColDef["METADATAID"];
                        if (hashInsertData.ContainsKey(intThisMetaID))
                        {
                            //the log record contains this metadata id

                            string strValue = hashInsertData[intThisMetaID].ToString();
                            string strColName = hashColDef["DESTCOL"].ToString();
                            string strType = hashColDef["DESTCOLTYPE"].ToString();

                            if (strType == "str")
                            {
                                if (strValue.Length > 0)
                                {
                                    strValue = "'" + strValue + "'";
                                }
                            }
                            else if (strType == "int")
                            {
                                int intValue;
                                if (int.TryParse(strValue, out intValue))
                                {
                                    strValue = intValue.ToString();
                                }
                                else { strValue = ""; }
                            }
                            else if (strType == "dbl")
                            {
                                double dblValue;
                                if (double.TryParse(strValue, out dblValue))
                                {
                                    strValue = dblValue.ToString();
                                }
                                else { strValue = ""; }
                            }

                            if (strValue.Length > 0)
                            {

                                strColumns = strColumns + "," + strColName;
                                strValues = strValues + "," + strValue;
                            }
                        }

                    }

                    if (strColumns != "time")
                    {
                        
                        //build sql insert for this table
                        listSQLInserts.Add("insert into dt_ts_realtime_data (" + strColumns + ")" + " values (" + strValues + ") ON CONFLICT DO NOTHING");
                    }


                    //do the inserts for this record (time point)
                    PGInsert(listSQLInserts);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


     
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                //do checks
                if (txtDBConn.Text.Length == 0)
                {
                    MessageBox.Show("DT database connection has not been set", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //open database
                m_connPG = new NpgsqlConnection(txtDBConn.Text);
                m_connPG.Open();

                //get DT realtime data table def
                Hashtable hashTableDefs = GetTableDefs("realtime_table", "realtime_column");
                m_listRealtimeDataTableColDefs = (ArrayList)hashTableDefs["dt_ts_realtime_data"];


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnTestServos_Click(object sender, EventArgs e)
        {
            try
            {
                string strParam = this.txtFwdDive0Pos.Text + "|" + this.txtAftPitch0Pos.Text + "|" + this.txtAftRudder0Pos.Text;
                sp.WriteLine("SERVO_TEST," + strParam);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                txtPitchSP.Text = meta_id_15.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                txtDepthSP.Text = meta_id_1.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
  
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //handle your keys here


            if (keyData == Keys.Left)
            {

                sp.WriteLine("RUDDER,5"); //param is the servo step value

                return true;
            }
            //capture right arrow key
            if (keyData == Keys.Right)
            {

                sp.WriteLine("RUDDER,-5");

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileBrowser = new OpenFileDialog();
                fileBrowser.Filter = "txt files (*.log)|*.log|All files (*.*)|*.*";
                fileBrowser.Title = "Specify a 4Hz logfile to format time";
                if (txtDataDir.Text.Length > 0)
                {
                    fileBrowser.InitialDirectory = txtDataDir.Text;
                }

                if (fileBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    string strFileName = fileBrowser.FileName;
                    timeFormatter(strFileName);
                    //MessageBox.Show(fileBrowser.FileName);
                }

                fileBrowser.Dispose();
                fileBrowser = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void timeFormatter(string strInputFile)
        {
            try
            {

                string strOutFile = Path.Combine(Path.GetDirectoryName(strInputFile), Path.GetFileNameWithoutExtension(strInputFile) + "_subsecond.LOG");
                if (File.Exists(strOutFile))
                {
                    MessageBox.Show("An output file already exists with the name: " + strOutFile + " (delete this first before exporting)", "Convert Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
  
                //open the output file
                StreamWriter swOut = new System.IO.StreamWriter(strOutFile, false);

                //open the logfile
                StreamReader reader = File.OpenText(strInputFile);

                DateTime dtePrevious = DateTime.Now;
                int intMillisPrevious = 0;

                int intMillisRunning = 0;

                int intRecord = 1;
                while (reader.Peek() != -1)
                {

                    //Console.WriteLine(intRecord.ToString());

                    //read a line and split it up
                    String strLine = reader.ReadLine().Trim().TrimStart('{').TrimEnd('}');
                    String[] arrayLine = strLine.Split(new char[] { ',' }, StringSplitOptions.None);

                    //read the record
                    string strTime = "";
                    int intMillis = 0;
                    DateTime dteThis = DateTime.Now;
                    int intMillisStart = 0;
                    string strAllExceptTime = "";
                    foreach (string strDataValue in arrayLine)
                    {
                        String[] arrayParts = strDataValue.Split(new char[] { '|' }, StringSplitOptions.None);
                        if (arrayParts.Length == 2)
                        {
                            string strMetaID = arrayParts[0].Trim();
                            string strValue = arrayParts[1].Trim();
                            int intMetaID = int.Parse(strMetaID);

                            if (intMetaID == 13)
                            {
                                //'2017-07-28 11:42:42.846621+00'
                                dteThis = DateTime.ParseExact(strValue, "H:m:s d/M/yyyy", null); //16:56:13 5/2/2023                    
                                strTime = "'" + dteThis.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                            }
                            else if (intMetaID == 35)
                            {
                                intMillis = int.Parse(strValue);   
                            }
                            if(intMetaID != 13){
                                strAllExceptTime =  strAllExceptTime + "," + strDataValue;      
                            }

                        }
                    }

                    

                    if (strTime.Length > 0 && intMillis > 0)
                    {
                        if (intRecord == 1)
                        {
                            //first record
                            intMillisStart = intMillis;
                            intMillisRunning = 0;

                        } else {
                            TimeSpan ts = dteThis.Subtract(dtePrevious);
                            if (ts.Seconds == 0)
                            {
                                //same time as last record - need to compare millis values
                                int intMillisSpan = intMillis - intMillisPrevious;
                                intMillisRunning = intMillisRunning + intMillisSpan;

                            } else
                            {
                                //time has incremented to next second
                                intMillisStart = intMillis;
                                intMillisRunning = 0;
                            }
                        }

                        //write the record
                        double dblSeconds = intMillisRunning / 1000.00; //convert to seconds
                        DateTime dteSubSecond = dteThis.AddSeconds(Math.Round(dblSeconds, 1));
                        string strTimeSubSecond = dteSubSecond.ToString("H:m:s.f d/M/yyyy");
                        string strNew = "{13|" + strTimeSubSecond + strAllExceptTime + "}";
                        swOut.WriteLine(strNew);

                        //Debug.Print(strNew);

                        dtePrevious = dteThis;
                        intMillisPrevious = intMillis;
                    }

                    

                    intRecord +=1;
                }

                reader.Close();
                swOut.Close();

                MessageBox.Show("Convert complete!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void timeFormatterOLD(string strInputFile)
        {
            try
            {

                string strOutFile = Path.Combine(Path.GetDirectoryName(strInputFile), Path.GetFileNameWithoutExtension(strInputFile) + "_subsecond.LOG");
                if (File.Exists(strOutFile))
                {
                    MessageBox.Show("An output file already exists with the name: " + strOutFile + " (delete this first before exporting)", "Convert Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //open the output file
                StreamWriter swOut = new System.IO.StreamWriter(strOutFile, false);

                //open the logfile
                StreamReader reader = File.OpenText(strInputFile);

                DateTime dtePrevious = DateTime.Now;
                int intMillisPrevious = 0;

                int intMillisRunning = 0;

                int intRecord = 1;
                while (reader.Peek() != -1)
                {

                    //Console.WriteLine(intRecord.ToString());

                    //read a line and split it up
                    String strLine = reader.ReadLine().Trim().TrimStart('{').TrimEnd('}');
                    String[] arrayLine = strLine.Split(new char[] { ',' }, StringSplitOptions.None);

                    //read the record
                    string strTime = "";
                    int intMillis = 0;
                    DateTime dteThis = DateTime.Now;
                    int intMillisStart = 0;
                    string strAllExceptTime = "";
                    foreach (string strDataValue in arrayLine)
                    {
                        String[] arrayParts = strDataValue.Split(new char[] { '|' }, StringSplitOptions.None);
                        if (arrayParts.Length == 2)
                        {
                            string strMetaID = arrayParts[0].Trim();
                            string strValue = arrayParts[1].Trim();
                            int intMetaID = int.Parse(strMetaID);

                            if (intMetaID == 13)
                            {

                                String[] arraySubParts = strValue.Split(new char[] { '#' }, StringSplitOptions.None);

                                intMillis = int.Parse(arraySubParts[0]);

                                //'2017-07-28 11:42:42.846621+00'
                                dteThis = DateTime.ParseExact(arraySubParts[1], "H:m:s d/M/yyyy", null); //16:56:13 5/2/2023                    
                                strTime = "'" + dteThis.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                            }
                            
                            if (intMetaID != 13)
                            {
                                strAllExceptTime = strAllExceptTime + "," + strDataValue;
                            }

                        }
                    }



                    if (strTime.Length > 0 && intMillis > 0)
                    {
                        if (intRecord == 1)
                        {
                            //first record
                            intMillisStart = intMillis;
                            intMillisRunning = 0;

                        }
                        else
                        {
                            TimeSpan ts = dteThis.Subtract(dtePrevious);
                            if (ts.Seconds == 0)
                            {
                                //same time as last record - need to compare millis values
                                int intMillisSpan = intMillis - intMillisPrevious;
                                intMillisRunning = intMillisRunning + intMillisSpan;

                            }
                            else
                            {
                                //time has incremented to next second
                                intMillisStart = intMillis;
                                intMillisRunning = 0;
                            }
                        }

                        //write the record
                        double dblSeconds = intMillisRunning / 1000.00; //convert to seconds
                        DateTime dteSubSecond = dteThis.AddSeconds(Math.Round(dblSeconds, 1));
                        string strTimeSubSecond = dteSubSecond.ToString("H:m:s.f d/M/yyyy");
                        string strNew = "{13|" + strTimeSubSecond + strAllExceptTime + ",35|" + intMillis.ToString() + "}";
                        swOut.WriteLine(strNew);

                        //Debug.Print(strNew);

                        dtePrevious = dteThis;
                        intMillisPrevious = intMillis;
                    }



                    intRecord += 1;
                }

                reader.Close();
                swOut.Close();

                MessageBox.Show("Convert complete!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnMotorTest_Click(object sender, EventArgs e)
        {
            try
            {

                ArrayList listThrottleValues = new ArrayList();
                
                listThrottleValues.Add(-720);
                listThrottleValues.Add(-320);
                listThrottleValues.Add(-240); 
                listThrottleValues.Add(480);
                

                foreach (int intValue in listThrottleValues)
                {
                    sp.WriteLine("PROPELL," + intValue.ToString());
                    System.Threading.Thread.Sleep(2000);
                    sp.WriteLine("PROPELL," + intValue.ToString());
                    System.Threading.Thread.Sleep(2000);

                    sp.WriteLine("PROPELL,0");
                    System.Threading.Thread.Sleep(2000);
                    sp.WriteLine("PROPELL,0");
                    System.Threading.Thread.Sleep(2000);
                    sp.WriteLine("PROPELL,0");
                    System.Threading.Thread.Sleep(2000);
                    //wait 60 seconds
                    System.Threading.Thread.Sleep(60000);
                }

                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnRunIncrement_Click(object sender, EventArgs e)
        {
            try
            {

                int intNextRunNum = int.Parse(txtRunNum.Text) + 1;
                txtRunNum.Text = intNextRunNum.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
    }
}
