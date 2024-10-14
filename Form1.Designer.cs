namespace Paint {
    partial class Paint {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Paint));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開啟ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.儲存檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.結束ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.檢視ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.縮小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.畫筆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自由ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直線ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.矩形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.圓ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.橢圓ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.復原UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重做RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案ToolStripMenuItem,
            this.檢視ToolStripMenuItem,
            this.畫筆ToolStripMenuItem,
            this.操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1540, 42);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            this.檔案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開啟ToolStripMenuItem,
            this.新增ToolStripMenuItem,
            this.儲存檔案ToolStripMenuItem,
            this.結束ToolStripMenuItem});
            this.檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            this.檔案ToolStripMenuItem.Size = new System.Drawing.Size(81, 38);
            this.檔案ToolStripMenuItem.Text = "檔案";
            // 
            // 開啟ToolStripMenuItem
            // 
            this.開啟ToolStripMenuItem.Name = "開啟ToolStripMenuItem";
            this.開啟ToolStripMenuItem.Size = new System.Drawing.Size(241, 44);
            this.開啟ToolStripMenuItem.Text = "開啟圖檔";
            this.開啟ToolStripMenuItem.Click += new System.EventHandler(this.開啟ToolStripMenuItem_Click);
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(241, 44);
            this.新增ToolStripMenuItem.Text = "新增畫布";
            this.新增ToolStripMenuItem.Click += new System.EventHandler(this.New_canva_click);
            // 
            // 儲存檔案ToolStripMenuItem
            // 
            this.儲存檔案ToolStripMenuItem.Name = "儲存檔案ToolStripMenuItem";
            this.儲存檔案ToolStripMenuItem.Size = new System.Drawing.Size(241, 44);
            this.儲存檔案ToolStripMenuItem.Text = "儲存檔案";
            this.儲存檔案ToolStripMenuItem.Click += new System.EventHandler(this.儲存檔案ToolStripMenuItem_Click);
            // 
            // 結束ToolStripMenuItem
            // 
            this.結束ToolStripMenuItem.Name = "結束ToolStripMenuItem";
            this.結束ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.結束ToolStripMenuItem.Text = "結束";
            this.結束ToolStripMenuItem.Click += new System.EventHandler(this.結束ToolStripMenuItem_Click);
            // 
            // 檢視ToolStripMenuItem
            // 
            this.檢視ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.放大ToolStripMenuItem,
            this.縮小ToolStripMenuItem});
            this.檢視ToolStripMenuItem.Name = "檢視ToolStripMenuItem";
            this.檢視ToolStripMenuItem.Size = new System.Drawing.Size(81, 38);
            this.檢視ToolStripMenuItem.Text = "檢視";
            this.檢視ToolStripMenuItem.Click += new System.EventHandler(this.檢視ToolStripMenuItem_Click);
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(193, 44);
            this.放大ToolStripMenuItem.Text = "放大";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.Enlarge_click);
            // 
            // 縮小ToolStripMenuItem
            // 
            this.縮小ToolStripMenuItem.Name = "縮小ToolStripMenuItem";
            this.縮小ToolStripMenuItem.Size = new System.Drawing.Size(193, 44);
            this.縮小ToolStripMenuItem.Text = "縮小";
            this.縮小ToolStripMenuItem.Click += new System.EventHandler(this.Shrink_click);
            // 
            // 畫筆ToolStripMenuItem
            // 
            this.畫筆ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自由ToolStripMenuItem,
            this.直線ToolStripMenuItem,
            this.矩形ToolStripMenuItem,
            this.圓ToolStripMenuItem,
            this.橢圓ToolStripMenuItem});
            this.畫筆ToolStripMenuItem.Name = "畫筆ToolStripMenuItem";
            this.畫筆ToolStripMenuItem.Size = new System.Drawing.Size(81, 38);
            this.畫筆ToolStripMenuItem.Text = "畫筆";
            // 
            // 自由ToolStripMenuItem
            // 
            this.自由ToolStripMenuItem.Name = "自由ToolStripMenuItem";
            this.自由ToolStripMenuItem.Size = new System.Drawing.Size(193, 44);
            this.自由ToolStripMenuItem.Text = "自由";
            this.自由ToolStripMenuItem.Click += new System.EventHandler(this.自由ToolStripMenuItem_Click);
            // 
            // 直線ToolStripMenuItem
            // 
            this.直線ToolStripMenuItem.Name = "直線ToolStripMenuItem";
            this.直線ToolStripMenuItem.Size = new System.Drawing.Size(193, 44);
            this.直線ToolStripMenuItem.Text = "直線";
            this.直線ToolStripMenuItem.Click += new System.EventHandler(this.直線ToolStripMenuItem_Click);
            // 
            // 矩形ToolStripMenuItem
            // 
            this.矩形ToolStripMenuItem.Name = "矩形ToolStripMenuItem";
            this.矩形ToolStripMenuItem.Size = new System.Drawing.Size(193, 44);
            this.矩形ToolStripMenuItem.Text = "矩形";
            this.矩形ToolStripMenuItem.Click += new System.EventHandler(this.矩形ToolStripMenuItem_Click);
            // 
            // 圓ToolStripMenuItem
            // 
            this.圓ToolStripMenuItem.Name = "圓ToolStripMenuItem";
            this.圓ToolStripMenuItem.Size = new System.Drawing.Size(193, 44);
            this.圓ToolStripMenuItem.Text = "圓";
            this.圓ToolStripMenuItem.Click += new System.EventHandler(this.圓ToolStripMenuItem_Click);
            // 
            // 橢圓ToolStripMenuItem
            // 
            this.橢圓ToolStripMenuItem.Name = "橢圓ToolStripMenuItem";
            this.橢圓ToolStripMenuItem.Size = new System.Drawing.Size(193, 44);
            this.橢圓ToolStripMenuItem.Text = "橢圓";
            this.橢圓ToolStripMenuItem.Click += new System.EventHandler(this.橢圓ToolStripMenuItem_Click);
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.復原UndoToolStripMenuItem,
            this.重做RedoToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(81, 38);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 復原UndoToolStripMenuItem
            // 
            this.復原UndoToolStripMenuItem.Name = "復原UndoToolStripMenuItem";
            this.復原UndoToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.復原UndoToolStripMenuItem.Text = "復原(Undo)";
            this.復原UndoToolStripMenuItem.Click += new System.EventHandler(this.復原UndoToolStripMenuItem_Click);
            // 
            // 重做RedoToolStripMenuItem
            // 
            this.重做RedoToolStripMenuItem.Name = "重做RedoToolStripMenuItem";
            this.重做RedoToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.重做RedoToolStripMenuItem.Text = "重做(Redo)";
            this.重做RedoToolStripMenuItem.Click += new System.EventHandler(this.重做RedoToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1540, 736);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "圖片檔(*.jpg, *.png,*.gif,*.jpeg)|*.jpg;*.png;*.gif;*.jpeg";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 42);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1540, 42);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(46, 36);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.Pallate_Click);
            // 
            // Paint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 819);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Paint";
            this.Text = "Paint";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 檔案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開啟ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 結束ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem 檢視ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 縮小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 畫筆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自由ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直線ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 儲存檔案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 矩形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 圓ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem 橢圓ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 復原UndoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重做RedoToolStripMenuItem;
    }
}

