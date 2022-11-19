
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
            this.SuspendLayout();
            // 
            // rtb
            // 
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Location = new System.Drawing.Point(0, 200);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(800, 250);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(480, 66);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 39);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(149, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 26);
            this.textBox1.TabIndex = 2;
            // 
            // btnDeflate
            // 
            this.btnDeflate.Location = new System.Drawing.Point(211, 142);
            this.btnDeflate.Name = "btnDeflate";
            this.btnDeflate.Size = new System.Drawing.Size(97, 33);
            this.btnDeflate.TabIndex = 3;
            this.btnDeflate.Text = "Deflate";
            this.btnDeflate.UseVisualStyleBackColor = true;
            this.btnDeflate.Click += new System.EventHandler(this.btnDeflate_Click);
            // 
            // btnInflate
            // 
            this.btnInflate.Location = new System.Drawing.Point(108, 142);
            this.btnInflate.Name = "btnInflate";
            this.btnInflate.Size = new System.Drawing.Size(97, 33);
            this.btnInflate.TabIndex = 4;
            this.btnInflate.Text = "Inflate";
            this.btnInflate.UseVisualStyleBackColor = true;
            this.btnInflate.Click += new System.EventHandler(this.btnInflate_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(348, 142);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(97, 33);
            this.btnForward.TabIndex = 5;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(451, 142);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(97, 33);
            this.btnReverse.TabIndex = 6;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnInflate);
            this.Controls.Add(this.btnDeflate);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtb);
            this.Name = "ControlForm";
            this.Padding = new System.Windows.Forms.Padding(0, 200, 0, 0);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ControlForm_Load);
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
    }
}

