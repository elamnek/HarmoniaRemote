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
        
        private void ControlForm_Load(object sender, EventArgs e)
        {
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            try
            {
                sp.Open();
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
                    if (strReceived.StartsWith("{") && strReceived.EndsWith("}"))
                    {
                        //this is a data packet - display each part in the textboxes




                    }
                    
                    //Console.WriteLine(txt);
                    SetRTBText(rtb, strReceived);

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
        public static void SetLBLText(Control lbl, string text)
        {
            lbl.Invoke((MethodInvoker)delegate { lbl.Text = text; });
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
    }
}
