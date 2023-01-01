﻿
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
            this.meta_id_13 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnIdle = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnManual = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDynamicTrim = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnStaticTrim = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.meta_id_3 = new System.Windows.Forms.TextBox();
            this.meta_id_7 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.meta_id_1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.meta_id_11 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.meta_id_10 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFwdDive)).BeginInit();
            this.SuspendLayout();
            // 
            // rtb
            // 
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Location = new System.Drawing.Point(0, 400);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(1530, 265);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Location = new System.Drawing.Point(645, 51);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 39);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(331, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 26);
            this.textBox1.TabIndex = 2;
            // 
            // btnDeflate
            // 
            this.btnDeflate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeflate.Location = new System.Drawing.Point(331, 137);
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
            this.btnInflate.Location = new System.Drawing.Point(331, 97);
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
            this.btnForward.Location = new System.Drawing.Point(461, 97);
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
            this.btnReverse.Location = new System.Drawing.Point(461, 137);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(97, 33);
            this.btnReverse.TabIndex = 6;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(777, 36);
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
            this.label1.Location = new System.Drawing.Point(773, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Throttle";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblThrottle
            // 
            this.lblThrottle.AutoSize = true;
            this.lblThrottle.Location = new System.Drawing.Point(828, 97);
            this.lblThrottle.Name = "lblThrottle";
            this.lblThrottle.Size = new System.Drawing.Size(32, 20);
            this.lblThrottle.TabIndex = 9;
            this.lblThrottle.Text = "0%";
            // 
            // tbFwdDive
            // 
            this.tbFwdDive.Location = new System.Drawing.Point(887, 42);
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
            this.label2.Location = new System.Drawing.Point(883, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Fwd Dive";
            // 
            // lblFwdDive
            // 
            this.lblFwdDive.AutoSize = true;
            this.lblFwdDive.Location = new System.Drawing.Point(940, 107);
            this.lblFwdDive.Name = "lblFwdDive";
            this.lblFwdDive.Size = new System.Drawing.Size(27, 20);
            this.lblFwdDive.TabIndex = 12;
            this.lblFwdDive.Text = "90";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 338);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "State";
            // 
            // meta_id_13
            // 
            this.meta_id_13.Location = new System.Drawing.Point(109, 293);
            this.meta_id_13.Name = "meta_id_13";
            this.meta_id_13.Size = new System.Drawing.Size(174, 26);
            this.meta_id_13.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Datetime";
            // 
            // btnIdle
            // 
            this.btnIdle.BackColor = System.Drawing.Color.Silver;
            this.btnIdle.Location = new System.Drawing.Point(130, 32);
            this.btnIdle.Name = "btnIdle";
            this.btnIdle.Size = new System.Drawing.Size(32, 33);
            this.btnIdle.TabIndex = 18;
            this.btnIdle.UseVisualStyleBackColor = false;
            this.btnIdle.Click += new System.EventHandler(this.btnIdle_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(89, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Idle";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Manual";
            // 
            // btnManual
            // 
            this.btnManual.BackColor = System.Drawing.Color.Fuchsia;
            this.btnManual.Location = new System.Drawing.Point(130, 68);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(32, 33);
            this.btnManual.TabIndex = 20;
            this.btnManual.UseVisualStyleBackColor = false;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.TabIndex = 25;
            this.label7.Text = "Dynamic trim";
            // 
            // btnDynamicTrim
            // 
            this.btnDynamicTrim.BackColor = System.Drawing.Color.Blue;
            this.btnDynamicTrim.Location = new System.Drawing.Point(130, 143);
            this.btnDynamicTrim.Name = "btnDynamicTrim";
            this.btnDynamicTrim.Size = new System.Drawing.Size(32, 33);
            this.btnDynamicTrim.TabIndex = 24;
            this.btnDynamicTrim.UseVisualStyleBackColor = false;
            this.btnDynamicTrim.Click += new System.EventHandler(this.btnDynamicTrim_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 23;
            this.label8.Text = "Static trim";
            // 
            // btnStaticTrim
            // 
            this.btnStaticTrim.BackColor = System.Drawing.Color.Yellow;
            this.btnStaticTrim.Location = new System.Drawing.Point(130, 107);
            this.btnStaticTrim.Name = "btnStaticTrim";
            this.btnStaticTrim.Size = new System.Drawing.Size(32, 33);
            this.btnStaticTrim.TabIndex = 22;
            this.btnStaticTrim.UseVisualStyleBackColor = false;
            this.btnStaticTrim.Click += new System.EventHandler(this.btnStaticTrim_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(55, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 20);
            this.label9.TabIndex = 27;
            this.label9.Text = "ALARM!";
            // 
            // btnAlarm
            // 
            this.btnAlarm.BackColor = System.Drawing.Color.Red;
            this.btnAlarm.Location = new System.Drawing.Point(130, 222);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(32, 33);
            this.btnAlarm.TabIndex = 26;
            this.btnAlarm.UseVisualStyleBackColor = false;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(85, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 20);
            this.label10.TabIndex = 29;
            this.label10.Text = "Run";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Green;
            this.button1.Location = new System.Drawing.Point(130, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 33);
            this.button1.TabIndex = 28;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // meta_id_3
            // 
            this.meta_id_3.Location = new System.Drawing.Point(109, 338);
            this.meta_id_3.Name = "meta_id_3";
            this.meta_id_3.Size = new System.Drawing.Size(174, 26);
            this.meta_id_3.TabIndex = 30;
            // 
            // meta_id_7
            // 
            this.meta_id_7.Location = new System.Drawing.Point(453, 338);
            this.meta_id_7.Name = "meta_id_7";
            this.meta_id_7.Size = new System.Drawing.Size(174, 26);
            this.meta_id_7.TabIndex = 34;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(313, 296);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 20);
            this.label11.TabIndex = 33;
            this.label11.Text = "External pressure";
            // 
            // meta_id_1
            // 
            this.meta_id_1.Location = new System.Drawing.Point(453, 293);
            this.meta_id_1.Name = "meta_id_1";
            this.meta_id_1.Size = new System.Drawing.Size(174, 26);
            this.meta_id_1.TabIndex = 32;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(398, 338);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 20);
            this.label12.TabIndex = 31;
            this.label12.Text = "RPM";
            // 
            // meta_id_11
            // 
            this.meta_id_11.Location = new System.Drawing.Point(812, 338);
            this.meta_id_11.Name = "meta_id_11";
            this.meta_id_11.Size = new System.Drawing.Size(174, 26);
            this.meta_id_11.TabIndex = 38;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(663, 299);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(129, 20);
            this.label13.TabIndex = 37;
            this.label13.Text = "Internal pressure";
            // 
            // meta_id_10
            // 
            this.meta_id_10.Location = new System.Drawing.Point(812, 293);
            this.meta_id_10.Name = "meta_id_10";
            this.meta_id_10.Size = new System.Drawing.Size(174, 26);
            this.meta_id_10.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(685, 344);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 20);
            this.label14.TabIndex = 35;
            this.label14.Text = "Internal temp.";
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1530, 665);
            this.Controls.Add(this.meta_id_11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.meta_id_10);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.meta_id_7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.meta_id_1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.meta_id_3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnAlarm);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDynamicTrim);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnStaticTrim);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnIdle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.meta_id_13);
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
            this.Padding = new System.Windows.Forms.Padding(0, 400, 0, 0);
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
        private System.Windows.Forms.TextBox meta_id_13;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnIdle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDynamicTrim;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnStaticTrim;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox meta_id_3;
        private System.Windows.Forms.TextBox meta_id_7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox meta_id_1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox meta_id_11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox meta_id_10;
        private System.Windows.Forms.Label label14;
    }
}

