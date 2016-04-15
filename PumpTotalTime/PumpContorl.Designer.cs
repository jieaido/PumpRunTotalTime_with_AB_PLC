namespace PumpTotalTime
{
    partial class PumpContorl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PumpContorl));
            this.button1 = new System.Windows.Forms.Button();
            this.PumpNameLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LastStartTime = new System.Windows.Forms.Label();
            this.TotalRunTime = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(31, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "累计清零";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PumpNameLabel
            // 
            this.PumpNameLabel.AutoSize = true;
            this.PumpNameLabel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PumpNameLabel.Location = new System.Drawing.Point(27, 20);
            this.PumpNameLabel.Name = "PumpNameLabel";
            this.PumpNameLabel.Size = new System.Drawing.Size(89, 19);
            this.PumpNameLabel.TabIndex = 1;
            this.PumpNameLabel.Text = "电机名称";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 79);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "最后启动时间";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "累计运行时间";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // LastStartTime
            // 
            this.LastStartTime.AutoSize = true;
            this.LastStartTime.Location = new System.Drawing.Point(150, 112);
            this.LastStartTime.Name = "LastStartTime";
            this.LastStartTime.Size = new System.Drawing.Size(101, 12);
            this.LastStartTime.TabIndex = 6;
            this.LastStartTime.Text = "最后启动时间显示";
            // 
            // TotalRunTime
            // 
            this.TotalRunTime.AutoSize = true;
            this.TotalRunTime.Location = new System.Drawing.Point(150, 155);
            this.TotalRunTime.Name = "TotalRunTime";
            this.TotalRunTime.Size = new System.Drawing.Size(101, 12);
            this.TotalRunTime.TabIndex = 7;
            this.TotalRunTime.Text = "累计运行时间显示";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(163, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 26);
            this.button2.TabIndex = 8;
            this.button2.Text = "查看详情";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PumpContorl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TotalRunTime);
            this.Controls.Add(this.LastStartTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PumpNameLabel);
            this.Controls.Add(this.button1);
            this.Name = "PumpContorl";
            this.Size = new System.Drawing.Size(270, 192);
            this.Load += new System.EventHandler(this.PumpContorl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label PumpNameLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LastStartTime;
        private System.Windows.Forms.Label TotalRunTime;
        private System.Windows.Forms.Button button2;
    }
}
