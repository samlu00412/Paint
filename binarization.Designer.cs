namespace Paint {
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
            this.threBar = new System.Windows.Forms.TrackBar();
            this.threLabel = new System.Windows.Forms.Label();
            this.select_mode_Box = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threBar)).BeginInit();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Location = new System.Drawing.Point(8, 35);
            this.previewBox.Margin = new System.Windows.Forms.Padding(2);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(410, 257);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewBox.TabIndex = 5;
            this.previewBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(422, 270);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 22);
            this.button1.TabIndex = 6;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cancel_click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(476, 270);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 22);
            this.button2.TabIndex = 7;
            this.button2.Text = "確定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.confirm_click);
            // 
            // threBar
            // 
            this.threBar.Location = new System.Drawing.Point(8, 9);
            this.threBar.Margin = new System.Windows.Forms.Padding(2);
            this.threBar.Maximum = 25500;
            this.threBar.Name = "threBar";
            this.threBar.Size = new System.Drawing.Size(364, 45);
            this.threBar.TabIndex = 8;
            this.threBar.Scroll += new System.EventHandler(this.BarScroll);
            // 
            // threLabel
            // 
            this.threLabel.AutoSize = true;
            this.threLabel.Location = new System.Drawing.Point(392, 14);
            this.threLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.threLabel.Name = "threLabel";
            this.threLabel.Size = new System.Drawing.Size(20, 12);
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
            "Otsu"});
            this.select_mode_Box.Location = new System.Drawing.Point(425, 38);
            this.select_mode_Box.Margin = new System.Windows.Forms.Padding(2);
            this.select_mode_Box.Name = "select_mode_Box";
            this.select_mode_Box.Size = new System.Drawing.Size(100, 26);
            this.select_mode_Box.TabIndex = 10;
            this.select_mode_Box.Text = "Binary";
            this.select_mode_Box.TextChanged += new System.EventHandler(this.change_mode);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(425, 81);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "去除中央十字";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.ignore_cross);
            // 
            // binarization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 300);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.select_mode_Box);
            this.Controls.Add(this.threLabel);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.threBar);
            this.Location = new System.Drawing.Point(10, 10);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "binarization";
            this.Text = "binarization";
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TrackBar threBar;
        private System.Windows.Forms.Label threLabel;
        private System.Windows.Forms.ComboBox select_mode_Box;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}