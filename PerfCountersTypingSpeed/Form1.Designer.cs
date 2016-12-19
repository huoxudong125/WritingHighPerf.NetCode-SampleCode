namespace PerfCountersTypingSpeed
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWpm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCpu = new System.Windows.Forms.TextBox();
            this.labelBytesPerSecond = new System.Windows.Forms.Label();
            this.textBoxBytesPerSec = new System.Windows.Forms.TextBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "WPM:";
            // 
            // textBoxWpm
            // 
            this.textBoxWpm.Location = new System.Drawing.Point(49, 9);
            this.textBoxWpm.Name = "textBoxWpm";
            this.textBoxWpm.ReadOnly = true;
            this.textBoxWpm.Size = new System.Drawing.Size(100, 20);
            this.textBoxWpm.TabIndex = 1;
            this.textBoxWpm.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "% CPU:";
            // 
            // textBoxCpu
            // 
            this.textBoxCpu.Location = new System.Drawing.Point(203, 9);
            this.textBoxCpu.Name = "textBoxCpu";
            this.textBoxCpu.ReadOnly = true;
            this.textBoxCpu.Size = new System.Drawing.Size(100, 20);
            this.textBoxCpu.TabIndex = 3;
            this.textBoxCpu.TabStop = false;
            // 
            // labelBytesPerSecond
            // 
            this.labelBytesPerSecond.AutoSize = true;
            this.labelBytesPerSecond.Location = new System.Drawing.Point(312, 13);
            this.labelBytesPerSecond.Name = "labelBytesPerSecond";
            this.labelBytesPerSecond.Size = new System.Drawing.Size(86, 13);
            this.labelBytesPerSecond.TabIndex = 4;
            this.labelBytesPerSecond.Text = "Bytes Alloc/Sec:";
            // 
            // textBoxBytesPerSec
            // 
            this.textBoxBytesPerSec.Location = new System.Drawing.Point(400, 9);
            this.textBoxBytesPerSec.Name = "textBoxBytesPerSec";
            this.textBoxBytesPerSec.ReadOnly = true;
            this.textBoxBytesPerSec.Size = new System.Drawing.Size(100, 20);
            this.textBoxBytesPerSec.TabIndex = 5;
            this.textBoxBytesPerSec.TabStop = false;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInput.Location = new System.Drawing.Point(12, 96);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(541, 166);
            this.textBoxInput.TabIndex = 6;
            this.textBoxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(296, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Sample Text: The quick brown fox jumped over the lazy hare.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Start typing a paragraph:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 274);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.textBoxBytesPerSec);
            this.Controls.Add(this.labelBytesPerSecond);
            this.Controls.Add(this.textBoxCpu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxWpm);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Perf Counters Demo - Typing Speed";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxWpm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCpu;
        private System.Windows.Forms.Label labelBytesPerSecond;
        private System.Windows.Forms.TextBox textBoxBytesPerSec;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

