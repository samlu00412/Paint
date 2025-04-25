namespace PaintApp
{
    partial class binarization {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.threLabel = new System.Windows.Forms.Label();
            this.select_mode_Box = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.threBar = new System.Windows.Forms.TrackBar();
            this.blockSizeBar = new System.Windows.Forms.TrackBar();
            this.cValueBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.seed = new System.Windows.Forms.TrackBar();
            this.seedlabel = new System.Windows.Forms.Label();
            this.ifftimage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blockSizeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cValueBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ifftimage)).BeginInit();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Location = new System.Drawing.Point(12, 225);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(615, 386);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewBox.TabIndex = 5;
            this.previewBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(633, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cancel_click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(714, 405);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 7;
            this.button2.Text = "確定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.confirm_click);
            // 
            // threLabel
            // 
            this.threLabel.AutoSize = true;
            this.threLabel.Location = new System.Drawing.Point(588, 21);
            this.threLabel.Name = "threLabel";
            this.threLabel.Size = new System.Drawing.Size(28, 18);
            this.threLabel.TabIndex = 9;
            this.threLabel.Text = "0.0";
            // 
            // select_mode_Box
            // 
            this.select_mode_Box.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.select_mode_Box.FormattingEnabled = true;
            this.select_mode_Box.Items.AddRange(new object[] {
            "Binary",
            "Binary_inverse",
            "Tozero",
            "Tozero_inverse",
            "Trunc",
            "Otsu",
            "Adaptive_Mean",
            "Adaptive_Gaussian"});
            this.select_mode_Box.Location = new System.Drawing.Point(638, 57);
            this.select_mode_Box.Name = "select_mode_Box";
            this.select_mode_Box.Size = new System.Drawing.Size(148, 36);
            this.select_mode_Box.TabIndex = 10;
            this.select_mode_Box.Text = "Binary";
            this.select_mode_Box.TextChanged += new System.EventHandler(this.change_mode);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(638, 122);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(142, 22);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "去除中央十字";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.ignore_cross);
            // 
            // threBar
            // 
            this.threBar.Location = new System.Drawing.Point(12, 14);
            this.threBar.Maximum = 25500;
            this.threBar.Name = "threBar";
            this.threBar.Size = new System.Drawing.Size(546, 69);
            this.threBar.TabIndex = 8;
            this.threBar.Scroll += new System.EventHandler(this.BarScroll);
            this.threBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.modify_value);
            // 
            // blockSizeBar
            // 
            this.blockSizeBar.Location = new System.Drawing.Point(12, 75);
            this.blockSizeBar.Maximum = 51;
            this.blockSizeBar.Minimum = 3;
            this.blockSizeBar.Name = "blockSizeBar";
            this.blockSizeBar.Size = new System.Drawing.Size(546, 69);
            this.blockSizeBar.SmallChange = 2;
            this.blockSizeBar.TabIndex = 12;
            this.blockSizeBar.TickFrequency = 2;
            this.blockSizeBar.Value = 3;
            this.blockSizeBar.Scroll += new System.EventHandler(this.blockSizeBar_Scroll);
            // 
            // cValueBar
            // 
            this.cValueBar.Location = new System.Drawing.Point(12, 150);
            this.cValueBar.Maximum = 30;
            this.cValueBar.Minimum = -30;
            this.cValueBar.Name = "cValueBar";
            this.cValueBar.Size = new System.Drawing.Size(546, 69);
            this.cValueBar.TabIndex = 13;
            this.cValueBar.Value = 2;
            this.cValueBar.Scroll += new System.EventHandler(this.cValueBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(588, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(588, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "2";
            // 
            // seed
            // 
            this.seed.Location = new System.Drawing.Point(849, 14);
            this.seed.Maximum = 150;
            this.seed.Name = "seed";
            this.seed.Size = new System.Drawing.Size(546, 69);
            this.seed.TabIndex = 16;
            this.seed.Value = 2;
            this.seed.Scroll += new System.EventHandler(this.seed_Scroll);
            this.seed.MouseUp += new System.Windows.Forms.MouseEventHandler(this.modify_value);
            // 
            // seedlabel
            // 
            this.seedlabel.AutoSize = true;
            this.seedlabel.Location = new System.Drawing.Point(1430, 21);
            this.seedlabel.Name = "seedlabel";
            this.seedlabel.Size = new System.Drawing.Size(16, 18);
            this.seedlabel.TabIndex = 17;
            this.seedlabel.Text = "2";
            // 
            // ifftimage
            // 
            this.ifftimage.Location = new System.Drawing.Point(849, 225);
            this.ifftimage.Name = "ifftimage";
            this.ifftimage.Size = new System.Drawing.Size(615, 386);
            this.ifftimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ifftimage.TabIndex = 18;
            this.ifftimage.TabStop = false;
            // 
            // binarization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1636, 634);
            this.Controls.Add(this.ifftimage);
            this.Controls.Add(this.seedlabel);
            this.Controls.Add(this.seed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cValueBar);
            this.Controls.Add(this.blockSizeBar);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.select_mode_Box);
            this.Controls.Add(this.threLabel);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.threBar);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "binarization";
            this.Text = "binarization";
            this.Load += new System.EventHandler(this.binarization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blockSizeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cValueBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ifftimage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label threLabel;
        private System.Windows.Forms.ComboBox select_mode_Box;
        private System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.TrackBar threBar;
        public System.Windows.Forms.TrackBar blockSizeBar;
        public System.Windows.Forms.TrackBar cValueBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TrackBar seed;
        private System.Windows.Forms.Label seedlabel;
        private System.Windows.Forms.PictureBox ifftimage;
    }
}