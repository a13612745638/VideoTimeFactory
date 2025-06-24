namespace 视频时间工厂
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            label1 = new Label();
            labelVideoPath = new Label();
            labelVideoAspect = new Label();
            label2 = new Label();
            labelVideoDuration = new Label();
            label3 = new Label();
            labelVideoFormat = new Label();
            label4 = new Label();
            labelVideoFrameRate = new Label();
            label5 = new Label();
            label6 = new Label();
            dateTimePickerDate = new DateTimePicker();
            dateTimePickerTime = new DateTimePicker();
            label7 = new Label();
            button2 = new Button();
            label8 = new Label();
            label9 = new Label();
            textBoxVideoLeft = new TextBox();
            textBoxVideoTop = new TextBox();
            comboBoxFonts = new ComboBox();
            label10 = new Label();
            progressBar1 = new ProgressBar();
            label11 = new Label();
            textBoxFontsSize = new TextBox();
            button3 = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(39, 355);
            button1.Name = "button1";
            button1.Size = new Size(89, 35);
            button1.TabIndex = 0;
            button1.Text = "加载视频";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 33);
            label1.Name = "label1";
            label1.Size = new Size(59, 17);
            label1.TabIndex = 1;
            label1.Text = "视频位置:";
            // 
            // labelVideoPath
            // 
            labelVideoPath.AutoSize = true;
            labelVideoPath.Location = new Point(104, 33);
            labelVideoPath.Name = "labelVideoPath";
            labelVideoPath.Size = new Size(0, 17);
            labelVideoPath.TabIndex = 2;
            // 
            // labelVideoAspect
            // 
            labelVideoAspect.AutoSize = true;
            labelVideoAspect.Location = new Point(104, 61);
            labelVideoAspect.Name = "labelVideoAspect";
            labelVideoAspect.Size = new Size(0, 17);
            labelVideoAspect.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(39, 61);
            label2.Name = "label2";
            label2.Size = new Size(59, 17);
            label2.TabIndex = 3;
            label2.Text = "视频长宽:";
            // 
            // labelVideoDuration
            // 
            labelVideoDuration.AutoSize = true;
            labelVideoDuration.Location = new Point(104, 89);
            labelVideoDuration.Name = "labelVideoDuration";
            labelVideoDuration.Size = new Size(0, 17);
            labelVideoDuration.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 89);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 5;
            label3.Text = "视频时长:";
            // 
            // labelVideoFormat
            // 
            labelVideoFormat.AutoSize = true;
            labelVideoFormat.Location = new Point(104, 117);
            labelVideoFormat.Name = "labelVideoFormat";
            labelVideoFormat.Size = new Size(0, 17);
            labelVideoFormat.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(39, 117);
            label4.Name = "label4";
            label4.Size = new Size(59, 17);
            label4.TabIndex = 7;
            label4.Text = "视频格式:";
            // 
            // labelVideoFrameRate
            // 
            labelVideoFrameRate.AutoSize = true;
            labelVideoFrameRate.Location = new Point(104, 145);
            labelVideoFrameRate.Name = "labelVideoFrameRate";
            labelVideoFrameRate.Size = new Size(0, 17);
            labelVideoFrameRate.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(39, 145);
            label5.Name = "label5";
            label5.Size = new Size(59, 17);
            label5.TabIndex = 9;
            label5.Text = "视频帧率:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(39, 173);
            label6.Name = "label6";
            label6.Size = new Size(59, 17);
            label6.TabIndex = 11;
            label6.Text = "视频日期:";
            // 
            // dateTimePickerDate
            // 
            dateTimePickerDate.Location = new Point(104, 168);
            dateTimePickerDate.Name = "dateTimePickerDate";
            dateTimePickerDate.Size = new Size(205, 23);
            dateTimePickerDate.TabIndex = 12;
            dateTimePickerDate.Value = DateTime.Now;
            // 
            // dateTimePickerTime
            // 
            dateTimePickerTime.Format = DateTimePickerFormat.Time;
            dateTimePickerTime.Location = new Point(104, 196);
            dateTimePickerTime.Name = "dateTimePickerTime";
            dateTimePickerTime.ShowUpDown = true;
            dateTimePickerTime.Size = new Size(205, 23);
            dateTimePickerTime.TabIndex = 14;
            dateTimePickerTime.Value = DateTime.Now;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(39, 201);
            label7.Name = "label7";
            label7.Size = new Size(59, 17);
            label7.TabIndex = 13;
            label7.Text = "视频时间:";
            // 
            // button2
            // 
            button2.Location = new Point(145, 355);
            button2.Name = "button2";
            button2.Size = new Size(89, 35);
            button2.TabIndex = 15;
            button2.Text = "保存视频";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(27, 229);
            label8.Name = "label8";
            label8.Size = new Size(71, 17);
            label8.TabIndex = 16;
            label8.Text = "时间左边距:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(27, 257);
            label9.Name = "label9";
            label9.Size = new Size(71, 17);
            label9.TabIndex = 17;
            label9.Text = "时间上边距:";
            // 
            // textBoxVideoLeft
            // 
            textBoxVideoLeft.Location = new Point(104, 226);
            textBoxVideoLeft.Name = "textBoxVideoLeft";
            textBoxVideoLeft.Size = new Size(205, 23);
            textBoxVideoLeft.TabIndex = 18;
            textBoxVideoLeft.Text = "1200";
            // 
            // textBoxVideoTop
            // 
            textBoxVideoTop.Location = new Point(104, 254);
            textBoxVideoTop.Name = "textBoxVideoTop";
            textBoxVideoTop.Size = new Size(205, 23);
            textBoxVideoTop.TabIndex = 19;
            textBoxVideoTop.Text = "950";
            // 
            // comboBoxFonts
            // 
            comboBoxFonts.FormattingEnabled = true;
            comboBoxFonts.Location = new Point(104, 282);
            comboBoxFonts.Name = "comboBoxFonts";
            comboBoxFonts.Size = new Size(205, 25);
            comboBoxFonts.TabIndex = 20;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(39, 285);
            label10.Name = "label10";
            label10.Size = new Size(59, 17);
            label10.TabIndex = 21;
            label10.Text = "时间字体:";
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Bottom;
            progressBar1.Location = new Point(0, 422);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(379, 23);
            progressBar1.TabIndex = 22;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(15, 313);
            label11.Name = "label11";
            label11.Size = new Size(83, 17);
            label11.TabIndex = 23;
            label11.Text = "时间字体大小:";
            // 
            // textBoxFontsSize
            // 
            textBoxFontsSize.Location = new Point(104, 313);
            textBoxFontsSize.Name = "textBoxFontsSize";
            textBoxFontsSize.Size = new Size(205, 23);
            textBoxFontsSize.TabIndex = 24;
            textBoxFontsSize.Text = "50";
            // 
            // button3
            // 
            button3.Location = new Point(253, 355);
            button3.Name = "button3";
            button3.Size = new Size(89, 35);
            button3.TabIndex = 25;
            button3.Text = "打开目录";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 400);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(379, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 26;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = Color.Red;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(68, 17);
            toolStripStatusLabel1.Text = "消息提示：";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.ForeColor = Color.Red;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(0, 17);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(379, 445);
            Controls.Add(statusStrip1);
            Controls.Add(button3);
            Controls.Add(textBoxFontsSize);
            Controls.Add(label11);
            Controls.Add(progressBar1);
            Controls.Add(label10);
            Controls.Add(comboBoxFonts);
            Controls.Add(textBoxVideoTop);
            Controls.Add(textBoxVideoLeft);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(button2);
            Controls.Add(dateTimePickerTime);
            Controls.Add(label7);
            Controls.Add(dateTimePickerDate);
            Controls.Add(label6);
            Controls.Add(labelVideoFrameRate);
            Controls.Add(label5);
            Controls.Add(labelVideoFormat);
            Controls.Add(label4);
            Controls.Add(labelVideoDuration);
            Controls.Add(label3);
            Controls.Add(labelVideoAspect);
            Controls.Add(label2);
            Controls.Add(labelVideoPath);
            Controls.Add(label1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "视频时间工厂";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label labelVideoPath;
        private Label labelVideoAspect;
        private Label label2;
        private Label labelVideoDuration;
        private Label label3;
        private Label labelVideoFormat;
        private Label label4;
        private Label labelVideoFrameRate;
        private Label label5;
        private Label label6;
        private DateTimePicker dateTimePickerDate;
        private DateTimePicker dateTimePickerTime;
        private Label label7;
        private Button button2;
        private Label label8;
        private Label label9;
        private TextBox textBoxVideoLeft;
        private TextBox textBoxVideoTop;
        private ComboBox comboBoxFonts;
        private Label label10;
        private ProgressBar progressBar1;
        private Label label11;
        private TextBox textBoxFontsSize;
        private Button button3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
    }
}
