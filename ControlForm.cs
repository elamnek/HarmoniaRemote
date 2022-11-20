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
                
                string txt = sp.ReadLine();
                if (!txt.StartsWith("VMDPE"))
                {
                    Console.WriteLine(txt);
                    SetControlText(rtb, txt);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void SetControlText(Control control, string text)
        {
            control.Invoke((MethodInvoker)delegate { control.Text = control.Text +  text; });
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
    }
}
