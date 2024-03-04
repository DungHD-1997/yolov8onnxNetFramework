namespace GUI_YOLOv8
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.mode = new System.Windows.Forms.GroupBox();
            this.rb_sm = new System.Windows.Forms.RadioButton();
            this.rb_dt = new System.Windows.Forms.RadioButton();
            this.rb_cls = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rb_pose = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.mode.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(44, 202);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(708, 202);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(640, 480);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // mode
            // 
            this.mode.Controls.Add(this.rb_pose);
            this.mode.Controls.Add(this.rb_sm);
            this.mode.Controls.Add(this.rb_dt);
            this.mode.Controls.Add(this.rb_cls);
            this.mode.Location = new System.Drawing.Point(44, 49);
            this.mode.Name = "mode";
            this.mode.Size = new System.Drawing.Size(200, 135);
            this.mode.TabIndex = 3;
            this.mode.TabStop = false;
            this.mode.Text = "Mode";
            // 
            // rb_sm
            // 
            this.rb_sm.AutoSize = true;
            this.rb_sm.Location = new System.Drawing.Point(59, 79);
            this.rb_sm.Name = "rb_sm";
            this.rb_sm.Size = new System.Drawing.Size(67, 16);
            this.rb_sm.TabIndex = 6;
            this.rb_sm.Text = "Segment";
            this.rb_sm.UseVisualStyleBackColor = true;
            this.rb_sm.CheckedChanged += new System.EventHandler(this.rb_sm_CheckedChanged);
            // 
            // rb_dt
            // 
            this.rb_dt.AutoSize = true;
            this.rb_dt.Location = new System.Drawing.Point(59, 47);
            this.rb_dt.Name = "rb_dt";
            this.rb_dt.Size = new System.Drawing.Size(57, 16);
            this.rb_dt.TabIndex = 5;
            this.rb_dt.Text = "Detect";
            this.rb_dt.UseVisualStyleBackColor = true;
            this.rb_dt.CheckedChanged += new System.EventHandler(this.rb_dt_CheckedChanged);
            // 
            // rb_cls
            // 
            this.rb_cls.AutoSize = true;
            this.rb_cls.Checked = true;
            this.rb_cls.Location = new System.Drawing.Point(59, 16);
            this.rb_cls.Name = "rb_cls";
            this.rb_cls.Size = new System.Drawing.Size(62, 16);
            this.rb_cls.TabIndex = 4;
            this.rb_cls.TabStop = true;
            this.rb_cls.Text = "Classifi";
            this.rb_cls.UseVisualStyleBackColor = true;
            this.rb_cls.CheckedChanged += new System.EventHandler(this.rb_cls_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1447, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.cameraToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fileToolStripMenuItem.Text = "Image";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cameraToolStripMenuItem.Text = "Camera";
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.videoToolStripMenuItem.Text = "Video";
            // 
            // rb_pose
            // 
            this.rb_pose.AutoSize = true;
            this.rb_pose.Location = new System.Drawing.Point(59, 113);
            this.rb_pose.Name = "rb_pose";
            this.rb_pose.Size = new System.Drawing.Size(48, 16);
            this.rb_pose.TabIndex = 7;
            this.rb_pose.Text = "Pose";
            this.rb_pose.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1447, 694);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mode);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.mode.ResumeLayout(false);
            this.mode.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox mode;
        private System.Windows.Forms.RadioButton rb_sm;
        private System.Windows.Forms.RadioButton rb_dt;
        private System.Windows.Forms.RadioButton rb_cls;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.RadioButton rb_pose;
    }
}

