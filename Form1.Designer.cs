using System;
using System.Windows.Forms;

namespace PaintApp {
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
            this.三角形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.復原UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重做RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.繪製亮度直方圖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.描繪輪廓ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findContoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.框選瑕疵ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.邊緣ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.遮罩效果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.臨界處理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.型態變化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.調整ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.強度轉換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.亮度對比度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.伽瑪ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.log變換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cLAHEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.空間濾波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高斯模糊ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.低通濾波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高通濾波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.轉換成灰階ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放棄ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iFFTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.色彩空間ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bGRToGrayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToBGRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bGRToHSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSVToBGRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bGRToLabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labToBGRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bGRToYUVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yUVToBGRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bGRToRGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBToBGRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bGRToRGBAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBAToBGRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bGRToYCRCBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yCRCBToBGRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.負片處理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.正規化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equalizeHistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.thicknessBar = new System.Windows.Forms.TrackBar();
            this.thicknessLabel = new System.Windows.Forms.Label();
            this.製作GroundTruthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.資料比對ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.統計ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thicknessBar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案ToolStripMenuItem,
            this.檢視ToolStripMenuItem,
            this.畫筆ToolStripMenuItem,
            this.操作ToolStripMenuItem,
            this.遮罩效果ToolStripMenuItem,
            this.調整ToolStripMenuItem,
            this.正規化ToolStripMenuItem,
            this.資料比對ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(711, 24);
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
            this.檔案ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.檔案ToolStripMenuItem.Text = "檔案";
            // 
            // 開啟ToolStripMenuItem
            // 
            this.開啟ToolStripMenuItem.Name = "開啟ToolStripMenuItem";
            this.開啟ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.開啟ToolStripMenuItem.Text = "開啟圖檔";
            this.開啟ToolStripMenuItem.Click += new System.EventHandler(this.開啟ToolStripMenuItem_Click);
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.新增ToolStripMenuItem.Text = "新增畫布";
            this.新增ToolStripMenuItem.Click += new System.EventHandler(this.New_canva_click);
            // 
            // 儲存檔案ToolStripMenuItem
            // 
            this.儲存檔案ToolStripMenuItem.Name = "儲存檔案ToolStripMenuItem";
            this.儲存檔案ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.儲存檔案ToolStripMenuItem.Text = "儲存檔案";
            this.儲存檔案ToolStripMenuItem.Click += new System.EventHandler(this.儲存檔案ToolStripMenuItem_Click);
            // 
            // 結束ToolStripMenuItem
            // 
            this.結束ToolStripMenuItem.Name = "結束ToolStripMenuItem";
            this.結束ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.結束ToolStripMenuItem.Text = "結束";
            this.結束ToolStripMenuItem.Click += new System.EventHandler(this.結束ToolStripMenuItem_Click);
            // 
            // 檢視ToolStripMenuItem
            // 
            this.檢視ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.放大ToolStripMenuItem,
            this.縮小ToolStripMenuItem});
            this.檢視ToolStripMenuItem.Name = "檢視ToolStripMenuItem";
            this.檢視ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.檢視ToolStripMenuItem.Text = "檢視";
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.放大ToolStripMenuItem.Text = "放大";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.Enlarge_click);
            // 
            // 縮小ToolStripMenuItem
            // 
            this.縮小ToolStripMenuItem.Name = "縮小ToolStripMenuItem";
            this.縮小ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
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
            this.橢圓ToolStripMenuItem,
            this.三角形ToolStripMenuItem});
            this.畫筆ToolStripMenuItem.Name = "畫筆ToolStripMenuItem";
            this.畫筆ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.畫筆ToolStripMenuItem.Text = "畫筆";
            // 
            // 自由ToolStripMenuItem
            // 
            this.自由ToolStripMenuItem.Name = "自由ToolStripMenuItem";
            this.自由ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.自由ToolStripMenuItem.Text = "自由";
            this.自由ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 直線ToolStripMenuItem
            // 
            this.直線ToolStripMenuItem.Name = "直線ToolStripMenuItem";
            this.直線ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.直線ToolStripMenuItem.Text = "直線";
            this.直線ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 矩形ToolStripMenuItem
            // 
            this.矩形ToolStripMenuItem.Name = "矩形ToolStripMenuItem";
            this.矩形ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.矩形ToolStripMenuItem.Text = "矩形";
            this.矩形ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 圓ToolStripMenuItem
            // 
            this.圓ToolStripMenuItem.Name = "圓ToolStripMenuItem";
            this.圓ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.圓ToolStripMenuItem.Text = "圓";
            this.圓ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 橢圓ToolStripMenuItem
            // 
            this.橢圓ToolStripMenuItem.Name = "橢圓ToolStripMenuItem";
            this.橢圓ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.橢圓ToolStripMenuItem.Text = "橢圓";
            this.橢圓ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 三角形ToolStripMenuItem
            // 
            this.三角形ToolStripMenuItem.Name = "三角形ToolStripMenuItem";
            this.三角形ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.三角形ToolStripMenuItem.Text = "三角形";
            this.三角形ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.復原UndoToolStripMenuItem,
            this.重做RedoToolStripMenuItem,
            this.繪製亮度直方圖ToolStripMenuItem,
            this.描繪輪廓ToolStripMenuItem,
            this.findContoursToolStripMenuItem,
            this.框選瑕疵ToolStripMenuItem,
            this.邊緣ToolStripMenuItem,
            this.製作GroundTruthToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 復原UndoToolStripMenuItem
            // 
            this.復原UndoToolStripMenuItem.Name = "復原UndoToolStripMenuItem";
            this.復原UndoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.復原UndoToolStripMenuItem.Text = "復原(Undo)";
            this.復原UndoToolStripMenuItem.Click += new System.EventHandler(this.復原UndoToolStripMenuItem_Click);
            // 
            // 重做RedoToolStripMenuItem
            // 
            this.重做RedoToolStripMenuItem.Name = "重做RedoToolStripMenuItem";
            this.重做RedoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.重做RedoToolStripMenuItem.Text = "重做(Redo)";
            this.重做RedoToolStripMenuItem.Click += new System.EventHandler(this.重做RedoToolStripMenuItem_Click);
            // 
            // 繪製亮度直方圖ToolStripMenuItem
            // 
            this.繪製亮度直方圖ToolStripMenuItem.Name = "繪製亮度直方圖ToolStripMenuItem";
            this.繪製亮度直方圖ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.繪製亮度直方圖ToolStripMenuItem.Text = "繪製亮度直方圖";
            this.繪製亮度直方圖ToolStripMenuItem.Click += new System.EventHandler(this.btnShowHistogram_Click);
            // 
            // 描繪輪廓ToolStripMenuItem
            // 
            this.描繪輪廓ToolStripMenuItem.Name = "描繪輪廓ToolStripMenuItem";
            this.描繪輪廓ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.描繪輪廓ToolStripMenuItem.Text = "描繪輪廓";
            this.描繪輪廓ToolStripMenuItem.Click += new System.EventHandler(this.draw_contour_click);
            // 
            // findContoursToolStripMenuItem
            // 
            this.findContoursToolStripMenuItem.Name = "findContoursToolStripMenuItem";
            this.findContoursToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.findContoursToolStripMenuItem.Text = "FindContours";
            this.findContoursToolStripMenuItem.Click += new System.EventHandler(this.findContoursToolStripMenuItem_Click);
            // 
            // 框選瑕疵ToolStripMenuItem
            // 
            this.框選瑕疵ToolStripMenuItem.Name = "框選瑕疵ToolStripMenuItem";
            this.框選瑕疵ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.框選瑕疵ToolStripMenuItem.Text = "框選瑕疵";
            this.框選瑕疵ToolStripMenuItem.Click += new System.EventHandler(this.find_defect);
            // 
            // 邊緣ToolStripMenuItem
            // 
            this.邊緣ToolStripMenuItem.Name = "邊緣ToolStripMenuItem";
            this.邊緣ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.邊緣ToolStripMenuItem.Text = "邊緣";
            this.邊緣ToolStripMenuItem.Click += new System.EventHandler(this.邊緣ToolStripMenuItem_Click);
            // 
            // 遮罩效果ToolStripMenuItem
            // 
            this.遮罩效果ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.臨界處理ToolStripMenuItem,
            this.型態變化ToolStripMenuItem,
            this.lUTToolStripMenuItem});
            this.遮罩效果ToolStripMenuItem.Name = "遮罩效果ToolStripMenuItem";
            this.遮罩效果ToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.遮罩效果ToolStripMenuItem.Text = "遮罩/效果";
            // 
            // 臨界處理ToolStripMenuItem
            // 
            this.臨界處理ToolStripMenuItem.Name = "臨界處理ToolStripMenuItem";
            this.臨界處理ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.臨界處理ToolStripMenuItem.Text = "臨界處理";
            this.臨界處理ToolStripMenuItem.Click += new System.EventHandler(this.二值化click);
            // 
            // 型態變化ToolStripMenuItem
            // 
            this.型態變化ToolStripMenuItem.Name = "型態變化ToolStripMenuItem";
            this.型態變化ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.型態變化ToolStripMenuItem.Text = "型態變化";
            this.型態變化ToolStripMenuItem.Click += new System.EventHandler(this.型態click);
            // 
            // lUTToolStripMenuItem
            // 
            this.lUTToolStripMenuItem.Name = "lUTToolStripMenuItem";
            this.lUTToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.lUTToolStripMenuItem.Text = "LUT";
            this.lUTToolStripMenuItem.Click += new System.EventHandler(this.lUTToolStripMenuItem_Click);
            // 
            // 調整ToolStripMenuItem
            // 
            this.調整ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.強度轉換ToolStripMenuItem,
            this.空間濾波ToolStripMenuItem,
            this.轉換成灰階ToolStripMenuItem,
            this.放棄ToolStripMenuItem,
            this.iFFTToolStripMenuItem,
            this.色彩空間ToolStripMenuItem,
            this.rGBToolStripMenuItem,
            this.負片處理ToolStripMenuItem});
            this.調整ToolStripMenuItem.Name = "調整ToolStripMenuItem";
            this.調整ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.調整ToolStripMenuItem.Text = "調整";
            // 
            // 強度轉換ToolStripMenuItem
            // 
            this.強度轉換ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.亮度對比度ToolStripMenuItem,
            this.伽瑪ToolStripMenuItem,
            this.log變換ToolStripMenuItem,
            this.反logToolStripMenuItem,
            this.cLAHEToolStripMenuItem});
            this.強度轉換ToolStripMenuItem.Name = "強度轉換ToolStripMenuItem";
            this.強度轉換ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.強度轉換ToolStripMenuItem.Text = "強度轉換";
            // 
            // 亮度對比度ToolStripMenuItem
            // 
            this.亮度對比度ToolStripMenuItem.Name = "亮度對比度ToolStripMenuItem";
            this.亮度對比度ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.亮度對比度ToolStripMenuItem.Text = "亮度/對比度";
            this.亮度對比度ToolStripMenuItem.Click += new System.EventHandler(this.亮度對比度ToolStripMenuItem_Click);
            // 
            // 伽瑪ToolStripMenuItem
            // 
            this.伽瑪ToolStripMenuItem.Name = "伽瑪ToolStripMenuItem";
            this.伽瑪ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.伽瑪ToolStripMenuItem.Text = "伽瑪";
            this.伽瑪ToolStripMenuItem.Click += new System.EventHandler(this.伽瑪ToolStripMenuItem_Click);
            // 
            // log變換ToolStripMenuItem
            // 
            this.log變換ToolStripMenuItem.Name = "log變換ToolStripMenuItem";
            this.log變換ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.log變換ToolStripMenuItem.Text = "log變換";
            this.log變換ToolStripMenuItem.Click += new System.EventHandler(this.log變換ToolStripMenuItem_Click);
            // 
            // 反logToolStripMenuItem
            // 
            this.反logToolStripMenuItem.Name = "反logToolStripMenuItem";
            this.反logToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.反logToolStripMenuItem.Text = "反log";
            this.反logToolStripMenuItem.Click += new System.EventHandler(this.反logToolStripMenuItem_Click);
            // 
            // cLAHEToolStripMenuItem
            // 
            this.cLAHEToolStripMenuItem.Name = "cLAHEToolStripMenuItem";
            this.cLAHEToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.cLAHEToolStripMenuItem.Text = "CLAHE";
            this.cLAHEToolStripMenuItem.Click += new System.EventHandler(this.cLAHEToolStripMenuItem_Click);
            // 
            // 空間濾波ToolStripMenuItem
            // 
            this.空間濾波ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.高斯模糊ToolStripMenuItem,
            this.低通濾波ToolStripMenuItem,
            this.高通濾波ToolStripMenuItem});
            this.空間濾波ToolStripMenuItem.Name = "空間濾波ToolStripMenuItem";
            this.空間濾波ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.空間濾波ToolStripMenuItem.Text = "空間濾波";
            // 
            // 高斯模糊ToolStripMenuItem
            // 
            this.高斯模糊ToolStripMenuItem.Name = "高斯模糊ToolStripMenuItem";
            this.高斯模糊ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.高斯模糊ToolStripMenuItem.Text = "高斯模糊";
            this.高斯模糊ToolStripMenuItem.Click += new System.EventHandler(this.高斯模糊ToolStripMenuItem_Click);
            // 
            // 低通濾波ToolStripMenuItem
            // 
            this.低通濾波ToolStripMenuItem.Name = "低通濾波ToolStripMenuItem";
            this.低通濾波ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.低通濾波ToolStripMenuItem.Text = "低通濾波";
            this.低通濾波ToolStripMenuItem.Click += new System.EventHandler(this.低通濾波ToolStripMenuItem_Click);
            // 
            // 高通濾波ToolStripMenuItem
            // 
            this.高通濾波ToolStripMenuItem.Name = "高通濾波ToolStripMenuItem";
            this.高通濾波ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.高通濾波ToolStripMenuItem.Text = "高通濾波";
            this.高通濾波ToolStripMenuItem.Click += new System.EventHandler(this.高通濾波ToolStripMenuItem_Click);
            // 
            // 轉換成灰階ToolStripMenuItem
            // 
            this.轉換成灰階ToolStripMenuItem.Name = "轉換成灰階ToolStripMenuItem";
            this.轉換成灰階ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.轉換成灰階ToolStripMenuItem.Text = "轉換成灰階";
            this.轉換成灰階ToolStripMenuItem.Click += new System.EventHandler(this.轉換成灰階ToolStripMenuItem_Click);
            // 
            // 放棄ToolStripMenuItem
            // 
            this.放棄ToolStripMenuItem.Name = "放棄ToolStripMenuItem";
            this.放棄ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.放棄ToolStripMenuItem.Text = "使用FFT";
            this.放棄ToolStripMenuItem.Click += new System.EventHandler(this.放棄toolStripMenuItem_Click);
            // 
            // iFFTToolStripMenuItem
            // 
            this.iFFTToolStripMenuItem.Name = "iFFTToolStripMenuItem";
            this.iFFTToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.iFFTToolStripMenuItem.Text = "IFFT";
            this.iFFTToolStripMenuItem.Click += new System.EventHandler(this.iFFTToolStripMenuItem_Click);
            // 
            // 色彩空間ToolStripMenuItem
            // 
            this.色彩空間ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bGRToGrayscaleToolStripMenuItem,
            this.grayscaleToBGRToolStripMenuItem,
            this.bGRToHSVToolStripMenuItem,
            this.hSVToBGRToolStripMenuItem,
            this.bGRToLabToolStripMenuItem,
            this.labToBGRToolStripMenuItem,
            this.bGRToYUVToolStripMenuItem,
            this.yUVToBGRToolStripMenuItem,
            this.bGRToRGBToolStripMenuItem,
            this.rGBToBGRToolStripMenuItem,
            this.bGRToRGBAToolStripMenuItem,
            this.rGBAToBGRToolStripMenuItem,
            this.bGRToYCRCBToolStripMenuItem,
            this.yCRCBToBGRToolStripMenuItem});
            this.色彩空間ToolStripMenuItem.Name = "色彩空間ToolStripMenuItem";
            this.色彩空間ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.色彩空間ToolStripMenuItem.Text = "色彩空間";
            // 
            // bGRToGrayscaleToolStripMenuItem
            // 
            this.bGRToGrayscaleToolStripMenuItem.Name = "bGRToGrayscaleToolStripMenuItem";
            this.bGRToGrayscaleToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bGRToGrayscaleToolStripMenuItem.Text = "BGR to Grayscale";
            this.bGRToGrayscaleToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // grayscaleToBGRToolStripMenuItem
            // 
            this.grayscaleToBGRToolStripMenuItem.Name = "grayscaleToBGRToolStripMenuItem";
            this.grayscaleToBGRToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.grayscaleToBGRToolStripMenuItem.Text = "Grayscale to BGR";
            this.grayscaleToBGRToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // bGRToHSVToolStripMenuItem
            // 
            this.bGRToHSVToolStripMenuItem.Name = "bGRToHSVToolStripMenuItem";
            this.bGRToHSVToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bGRToHSVToolStripMenuItem.Text = "BGR to HSV";
            this.bGRToHSVToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // hSVToBGRToolStripMenuItem
            // 
            this.hSVToBGRToolStripMenuItem.Name = "hSVToBGRToolStripMenuItem";
            this.hSVToBGRToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.hSVToBGRToolStripMenuItem.Text = "HSV to BGR";
            this.hSVToBGRToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // bGRToLabToolStripMenuItem
            // 
            this.bGRToLabToolStripMenuItem.Name = "bGRToLabToolStripMenuItem";
            this.bGRToLabToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bGRToLabToolStripMenuItem.Text = "BGR to Lab";
            this.bGRToLabToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // labToBGRToolStripMenuItem
            // 
            this.labToBGRToolStripMenuItem.Name = "labToBGRToolStripMenuItem";
            this.labToBGRToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.labToBGRToolStripMenuItem.Text = "Lab to BGR";
            this.labToBGRToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // bGRToYUVToolStripMenuItem
            // 
            this.bGRToYUVToolStripMenuItem.Name = "bGRToYUVToolStripMenuItem";
            this.bGRToYUVToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bGRToYUVToolStripMenuItem.Text = "BGR to YUV";
            this.bGRToYUVToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // yUVToBGRToolStripMenuItem
            // 
            this.yUVToBGRToolStripMenuItem.Name = "yUVToBGRToolStripMenuItem";
            this.yUVToBGRToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.yUVToBGRToolStripMenuItem.Text = "YUV to BGR";
            this.yUVToBGRToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // bGRToRGBToolStripMenuItem
            // 
            this.bGRToRGBToolStripMenuItem.Name = "bGRToRGBToolStripMenuItem";
            this.bGRToRGBToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bGRToRGBToolStripMenuItem.Text = "BGR to RGB";
            this.bGRToRGBToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // rGBToBGRToolStripMenuItem
            // 
            this.rGBToBGRToolStripMenuItem.Name = "rGBToBGRToolStripMenuItem";
            this.rGBToBGRToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.rGBToBGRToolStripMenuItem.Text = "RGB to BGR";
            this.rGBToBGRToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // bGRToRGBAToolStripMenuItem
            // 
            this.bGRToRGBAToolStripMenuItem.Name = "bGRToRGBAToolStripMenuItem";
            this.bGRToRGBAToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bGRToRGBAToolStripMenuItem.Text = "BGR to RGBA";
            this.bGRToRGBAToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // rGBAToBGRToolStripMenuItem
            // 
            this.rGBAToBGRToolStripMenuItem.Name = "rGBAToBGRToolStripMenuItem";
            this.rGBAToBGRToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.rGBAToBGRToolStripMenuItem.Text = "RGBA to BGR";
            this.rGBAToBGRToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // bGRToYCRCBToolStripMenuItem
            // 
            this.bGRToYCRCBToolStripMenuItem.Name = "bGRToYCRCBToolStripMenuItem";
            this.bGRToYCRCBToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.bGRToYCRCBToolStripMenuItem.Text = "BGR to XYZ";
            this.bGRToYCRCBToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // yCRCBToBGRToolStripMenuItem
            // 
            this.yCRCBToBGRToolStripMenuItem.Name = "yCRCBToBGRToolStripMenuItem";
            this.yCRCBToBGRToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.yCRCBToBGRToolStripMenuItem.Text = "XYZ to BGR";
            this.yCRCBToBGRToolStripMenuItem.Click += new System.EventHandler(this.colortransformToolStripMenuItem_Click);
            // 
            // rGBToolStripMenuItem
            // 
            this.rGBToolStripMenuItem.Name = "rGBToolStripMenuItem";
            this.rGBToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.rGBToolStripMenuItem.Text = "RGB調整";
            this.rGBToolStripMenuItem.Click += new System.EventHandler(this.rGBToolStripMenuItem_Click);
            // 
            // 負片處理ToolStripMenuItem
            // 
            this.負片處理ToolStripMenuItem.Name = "負片處理ToolStripMenuItem";
            this.負片處理ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.負片處理ToolStripMenuItem.Text = "負片處理";
            this.負片處理ToolStripMenuItem.Click += new System.EventHandler(this.negative_image);
            // 
            // 正規化ToolStripMenuItem
            // 
            this.正規化ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalizeToolStripMenuItem,
            this.equalizeHistToolStripMenuItem});
            this.正規化ToolStripMenuItem.Name = "正規化ToolStripMenuItem";
            this.正規化ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.正規化ToolStripMenuItem.Text = "正規化";
            // 
            // normalizeToolStripMenuItem
            // 
            this.normalizeToolStripMenuItem.Name = "normalizeToolStripMenuItem";
            this.normalizeToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.normalizeToolStripMenuItem.Text = "Normalize";
            this.normalizeToolStripMenuItem.Click += new System.EventHandler(this.normalizeToolStripMenuItem_Click);
            // 
            // equalizeHistToolStripMenuItem
            // 
            this.equalizeHistToolStripMenuItem.Name = "equalizeHistToolStripMenuItem";
            this.equalizeHistToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.equalizeHistToolStripMenuItem.Text = "equalizeHist";
            this.equalizeHistToolStripMenuItem.Click += new System.EventHandler(this.equalizeHistToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 42);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(711, 368);
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
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(711, 39);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.Pallate_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.復原UndoToolStripMenuItem_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.重做RedoToolStripMenuItem_Click);
            // 
            // thicknessBar
            // 
            this.thicknessBar.Location = new System.Drawing.Point(126, 24);
            this.thicknessBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.thicknessBar.Maximum = 30;
            this.thicknessBar.Minimum = 1;
            this.thicknessBar.Name = "thicknessBar";
            this.thicknessBar.Size = new System.Drawing.Size(375, 45);
            this.thicknessBar.TabIndex = 3;
            this.thicknessBar.Value = 2;
            this.thicknessBar.Scroll += new System.EventHandler(this.change_thickness);
            // 
            // thicknessLabel
            // 
            this.thicknessLabel.AutoSize = true;
            this.thicknessLabel.Location = new System.Drawing.Point(513, 30);
            this.thicknessLabel.Name = "thicknessLabel";
            this.thicknessLabel.Size = new System.Drawing.Size(11, 12);
            this.thicknessLabel.TabIndex = 4;
            this.thicknessLabel.Text = "2";
            // 
            // 製作GroundTruthToolStripMenuItem
            // 
            this.製作GroundTruthToolStripMenuItem.Name = "製作GroundTruthToolStripMenuItem";
            this.製作GroundTruthToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.製作GroundTruthToolStripMenuItem.Text = "製作Ground Truth";
            this.製作GroundTruthToolStripMenuItem.Click += new System.EventHandler(this.製作GroundTruthToolStripMenuItem_Click);
            // 
            // 資料比對ToolStripMenuItem
            // 
            this.資料比對ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.統計ToolStripMenuItem});
            this.資料比對ToolStripMenuItem.Name = "資料比對ToolStripMenuItem";
            this.資料比對ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.資料比對ToolStripMenuItem.Text = "資料比對";
            // 
            // 統計ToolStripMenuItem
            // 
            this.統計ToolStripMenuItem.Name = "統計ToolStripMenuItem";
            this.統計ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.統計ToolStripMenuItem.Text = "統計";
            this.統計ToolStripMenuItem.Click += new System.EventHandler(this.統計ToolStripMenuItem_Click);
            // 
            // Paint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 410);
            this.Controls.Add(this.thicknessLabel);
            this.Controls.Add(this.thicknessBar);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "Paint";
            this.Text = "Paint";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thicknessBar)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem 三角形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem 調整ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 強度轉換ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 空間濾波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 轉換成灰階ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 亮度對比度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高斯模糊ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 伽瑪ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 低通濾波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高通濾波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem log變換ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 繪製亮度直方圖ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放棄ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iFFTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 色彩空間ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bGRToGrayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToBGRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bGRToHSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hSVToBGRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bGRToLabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labToBGRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bGRToYUVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yUVToBGRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bGRToRGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBToBGRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bGRToRGBAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBAToBGRToolStripMenuItem;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.ToolStripMenuItem 遮罩效果ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 型態變化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 臨界處理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 描繪輪廓ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findContoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cLAHEToolStripMenuItem;
        private System.Windows.Forms.TrackBar thicknessBar;
        private System.Windows.Forms.Label thicknessLabel;
        private System.Windows.Forms.ToolStripMenuItem lUTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 正規化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equalizeHistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bGRToYCRCBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yCRCBToBGRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 負片處理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 框選瑕疵ToolStripMenuItem;
        private ToolStripMenuItem 邊緣ToolStripMenuItem;
        private ToolStripMenuItem 製作GroundTruthToolStripMenuItem;
        private ToolStripMenuItem 資料比對ToolStripMenuItem;
        private ToolStripMenuItem 統計ToolStripMenuItem;
    }
}

