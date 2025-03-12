namespace PaintApp




{
    partial class FindContour
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
            this.Preview_box = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.blockSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.cTrackBar = new System.Windows.Forms.TrackBar();
            this.tempsee = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Preview_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blockSizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempsee)).BeginInit();
            this.SuspendLayout();
            // 
            // Preview_box
            // 
            this.Preview_box.Location = new System.Drawing.Point(79, 67);
            this.Preview_box.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Preview_box.Name = "Preview_box";
            this.Preview_box.Size = new System.Drawing.Size(549, 363);
            this.Preview_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Preview_box.TabIndex = 0;
            this.Preview_box.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(662, 55);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(670, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1462, 464);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 56);
            this.button1.TabIndex = 2;
            this.button1.Text = "確認";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Confirm_click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1462, 528);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 56);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Cancel_click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Binary",
            "BinaryInv",
            "Truncate",
            "ToZero",
            "ToZeroInv",
            "Otsu",
            "AdaptiveMean",
            "AdaptiveGaussian"});
            this.comboBox1.Location = new System.Drawing.Point(1397, 67);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 32);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "Binary";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // blockSizeTrackBar
            // 
            this.blockSizeTrackBar.Location = new System.Drawing.Point(662, 155);
            this.blockSizeTrackBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.blockSizeTrackBar.Maximum = 51;
            this.blockSizeTrackBar.Minimum = 3;
            this.blockSizeTrackBar.Name = "blockSizeTrackBar";
            this.blockSizeTrackBar.Size = new System.Drawing.Size(670, 45);
            this.blockSizeTrackBar.SmallChange = 2;
            this.blockSizeTrackBar.TabIndex = 5;
            this.blockSizeTrackBar.Value = 11;
            this.blockSizeTrackBar.Scroll += new System.EventHandler(this.blockSizeTrackBar_Scroll);
            // 
            // cTrackBar
            // 
            this.cTrackBar.Location = new System.Drawing.Point(662, 255);
            this.cTrackBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cTrackBar.Maximum = 30;
            this.cTrackBar.Minimum = -30;
            this.cTrackBar.Name = "cTrackBar";
            this.cTrackBar.Size = new System.Drawing.Size(670, 45);
            this.cTrackBar.TabIndex = 6;
            this.cTrackBar.Value = 2;
            this.cTrackBar.Scroll += new System.EventHandler(this.cTrackBar_Scroll);
            // 
            // tempsee
            // 
            this.tempsee.Location = new System.Drawing.Point(79, 464);
            this.tempsee.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tempsee.Name = "tempsee";
            this.tempsee.Size = new System.Drawing.Size(549, 363);
            this.tempsee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.tempsee.TabIndex = 7;
            this.tempsee.TabStop = false;
            // 
            // FindContour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1615, 1049);
            this.Controls.Add(this.tempsee);
            this.Controls.Add(this.cTrackBar);
            this.Controls.Add(this.blockSizeTrackBar);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.Preview_box);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FindContour";
            this.Text = "FindContour";
            ((System.ComponentModel.ISupportInitialize)(this.Preview_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blockSizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempsee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Preview_box;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TrackBar blockSizeTrackBar;
        private System.Windows.Forms.TrackBar cTrackBar;
        private System.Windows.Forms.PictureBox tempsee;
    }
}