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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.mode = new System.Windows.Forms.GroupBox();
            this.rb_pose = new System.Windows.Forms.RadioButton();
            this.rb_sm = new System.Windows.Forms.RadioButton();
            this.rb_dt = new System.Windows.Forms.RadioButton();
            this.rb_cls = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_image = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_video = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_play = new System.Windows.Forms.Button();
            this.btn_pause = new System.Windows.Forms.Button();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.mode.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(73, 417);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1067, 720);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(1180, 417);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1067, 720);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // mode
            // 
            this.mode.Controls.Add(this.rb_pose);
            this.mode.Controls.Add(this.rb_sm);
            this.mode.Controls.Add(this.rb_dt);
            this.mode.Controls.Add(this.rb_cls);
            this.mode.Location = new System.Drawing.Point(73, 142);
            this.mode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mode.Name = "mode";
            this.mode.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.mode.Size = new System.Drawing.Size(263, 208);
            this.mode.TabIndex = 3;
            this.mode.TabStop = false;
            this.mode.Text = "Mode";
            // 
            // rb_pose
            // 
            this.rb_pose.AutoSize = true;
            this.rb_pose.Location = new System.Drawing.Point(98, 170);
            this.rb_pose.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rb_pose.Name = "rb_pose";
            this.rb_pose.Size = new System.Drawing.Size(70, 22);
            this.rb_pose.TabIndex = 7;
            this.rb_pose.Text = "Pose";
            this.rb_pose.UseVisualStyleBackColor = true;
            // 
            // rb_sm
            // 
            this.rb_sm.AutoSize = true;
            this.rb_sm.Location = new System.Drawing.Point(98, 118);
            this.rb_sm.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rb_sm.Name = "rb_sm";
            this.rb_sm.Size = new System.Drawing.Size(98, 22);
            this.rb_sm.TabIndex = 6;
            this.rb_sm.Text = "Segment";
            this.rb_sm.UseVisualStyleBackColor = true;
            this.rb_sm.CheckedChanged += new System.EventHandler(this.rb_sm_CheckedChanged);
            // 
            // rb_dt
            // 
            this.rb_dt.AutoSize = true;
            this.rb_dt.Location = new System.Drawing.Point(98, 70);
            this.rb_dt.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rb_dt.Name = "rb_dt";
            this.rb_dt.Size = new System.Drawing.Size(84, 22);
            this.rb_dt.TabIndex = 5;
            this.rb_dt.Text = "Detect";
            this.rb_dt.UseVisualStyleBackColor = true;
            this.rb_dt.CheckedChanged += new System.EventHandler(this.rb_dt_CheckedChanged);
            // 
            // rb_cls
            // 
            this.rb_cls.AutoSize = true;
            this.rb_cls.Checked = true;
            this.rb_cls.Location = new System.Drawing.Point(98, 24);
            this.rb_cls.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rb_cls.Name = "rb_cls";
            this.rb_cls.Size = new System.Drawing.Size(87, 22);
            this.rb_cls.TabIndex = 4;
            this.rb_cls.TabStop = true;
            this.rb_cls.Text = "Classifi";
            this.rb_cls.UseVisualStyleBackColor = true;
            this.rb_cls.CheckedChanged += new System.EventHandler(this.rb_cls_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(2032, 36);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_image,
            this.ts_video,
            this.cameraToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(54, 32);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // ts_image
            // 
            this.ts_image.Name = "ts_image";
            this.ts_image.Size = new System.Drawing.Size(270, 34);
            this.ts_image.Text = "Image";
            this.ts_image.Click += new System.EventHandler(this.ts_image_Click);
            // 
            // ts_video
            // 
            this.ts_video.Name = "ts_video";
            this.ts_video.Size = new System.Drawing.Size(270, 34);
            this.ts_video.Text = "Video";
            this.ts_video.Click += new System.EventHandler(this.ts_video_Click);
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.cameraToolStripMenuItem.Text = "Camera";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_pause);
            this.groupBox1.Controls.Add(this.btn_play);
            this.groupBox1.Location = new System.Drawing.Point(432, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 208);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Video Control";
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(69, 49);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(103, 43);
            this.btn_play.TabIndex = 0;
            this.btn_play.Text = "Play";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.Location = new System.Drawing.Point(69, 118);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(103, 43);
            this.btn_pause.TabIndex = 0;
            this.btn_pause.Text = "Pause";
            this.btn_pause.UseVisualStyleBackColor = true;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2032, 1280);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.mode);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.mode.ResumeLayout(false);
            this.mode.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.RadioButton rb_pose;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ts_image;
        private System.Windows.Forms.ToolStripMenuItem ts_video;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_pause;
        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
    }
}

