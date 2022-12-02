
namespace HarmoniaRemote
{
    partial class ControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnDeflate = new System.Windows.Forms.Button();
            this.btnInflate = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnReverse = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblThrottle = new System.Windows.Forms.Label();
            this.tbFwdDive = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFwdDive = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFwdDive)).BeginInit();
            this.SuspendLayout();
            // 
            // rtb
            // 
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Location = new System.Drawing.Point(0, 200);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(1003, 465);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Location = new System.Drawing.Point(340, 22);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 39);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 26);
            this.textBox1.TabIndex = 2;
            // 
            // btnDeflate
            // 
            this.btnDeflate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeflate.Location = new System.Drawing.Point(34, 148);
            this.btnDeflate.Name = "btnDeflate";
            this.btnDeflate.Size = new System.Drawing.Size(97, 33);
            this.btnDeflate.TabIndex = 3;
            this.btnDeflate.Text = "Deflate";
            this.btnDeflate.UseVisualStyleBackColor = true;
            this.btnDeflate.Click += new System.EventHandler(this.btnDeflate_Click);
            // 
            // btnInflate
            // 
            this.btnInflate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInflate.Location = new System.Drawing.Point(34, 108);
            this.btnInflate.Name = "btnInflate";
            this.btnInflate.Size = new System.Drawing.Size(97, 33);
            this.btnInflate.TabIndex = 4;
            this.btnInflate.Text = "Inflate";
            this.btnInflate.UseVisualStyleBackColor = true;
            this.btnInflate.Click += new System.EventHandler(this.btnInflate_Click);
            // 
            // btnForward
            // 
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForward.Location = new System.Drawing.Point(164, 108);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(97, 33);
            this.btnForward.TabIndex = 5;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReverse.Location = new System.Drawing.Point(164, 148);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(97, 33);
            this.btnReverse.TabIndex = 6;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(537, 22);
            this.trackBar1.Maximum = 180;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(69, 144);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 90;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(533, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Throttle";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblThrottle
            // 
            this.lblThrottle.AutoSize = true;
            this.lblThrottle.Location = new System.Drawing.Point(587, 86);
            this.lblThrottle.Name = "lblThrottle";
            this.lblThrottle.Size = new System.Drawing.Size(32, 20);
            this.lblThrottle.TabIndex = 9;
            this.lblThrottle.Text = "0%";
            // 
            // tbFwdDive
            // 
            this.tbFwdDive.Location = new System.Drawing.Point(647, 28);
            this.tbFwdDive.Maximum = 180;
            this.tbFwdDive.Name = "tbFwdDive";
            this.tbFwdDive.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbFwdDive.Size = new System.Drawing.Size(69, 144);
            this.tbFwdDive.TabIndex = 10;
            this.tbFwdDive.TickFrequency = 10;
            this.tbFwdDive.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbFwdDive.Value = 90;
            this.tbFwdDive.Scroll += new System.EventHandler(this.tbFwdDive_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(643, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Fwd Dive";
            // 
            // lblFwdDive
            // 
            this.lblFwdDive.AutoSize = true;
            this.lblFwdDive.Location = new System.Drawing.Point(706, 89);
            this.lblFwdDive.Name = "lblFwdDive";
            this.lblFwdDive.Size = new System.Drawing.Size(27, 20);
            this.lblFwdDive.TabIndex = 12;
            this.lblFwdDive.Text = "90";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(862, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "State";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblState.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(828, 66);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(122, 31);
            this.lblState.TabIndex = 15;
            this.lblState.Text = "Unknown";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1003, 665);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblFwdDive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbFwdDive);
            this.Controls.Add(this.lblThrottle);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnInflate);
            this.Controls.Add(this.btnDeflate);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtb);
            this.MinimumSize = new System.Drawing.Size(1025, 721);
            this.Name = "ControlForm";
            this.Padding = new System.Windows.Forms.Padding(0, 200, 0, 0);
            this.Text = "Harmonia Controller";
            this.Load += new System.EventHandler(this.ControlForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFwdDive)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnDeflate;
        private System.Windows.Forms.Button btnInflate;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblThrottle;
        private System.Windows.Forms.TrackBar tbFwdDive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFwdDive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblState;
    }
}

