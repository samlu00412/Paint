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
            this.threBar = new System.Windows.Forms.TrackBar();
            this.threLabel = new System.Windows.Forms.Label();
            this.select_mode_Box = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threBar)).BeginInit();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Location = new System.Drawing.Point(10, 44);
            this.previewBox.Margin = new System.Windows.Forms.Padding(2);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(546, 321);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewBox.TabIndex = 5;
            this.previewBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(562, 337);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cancel_click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(634, 337);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 28);
            this.button2.TabIndex = 7;
            this.button2.Text = "確定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.confirm_click);
            // 
            // threBar
            // 
            this.threBar.Location = new System.Drawing.Point(10, 11);
            this.threBar.Margin = new System.Windows.Forms.Padding(2);
            this.threBar.Maximum = 25500;
            this.threBar.Name = "threBar";
            this.threBar.Size = new System.Drawing.Size(486, 56);
            this.threBar.TabIndex = 8;
            this.threBar.Scroll += new System.EventHandler(this.BarScroll);
            // 
            // threLabel
            // 
            this.threLabel.AutoSize = true;
            this.threLabel.Location = new System.Drawing.Point(522, 18);
            this.threLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.threLabel.Name = "threLabel";
            this.threLabel.Size = new System.Drawing.Size(25, 15);
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
            this.select_mode_Box.Location = new System.Drawing.Point(567, 47);
            this.select_mode_Box.Name = "select_mode_Box";
            this.select_mode_Box.Size = new System.Drawing.Size(132, 31);
            this.select_mode_Box.TabIndex = 10;
            this.select_mode_Box.Text = "Binary";
            this.select_mode_Box.TextChanged += new System.EventHandler(this.change_mode);
            // 
            // binarization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 375);
            this.Controls.Add(this.select_mode_Box);
            this.Controls.Add(this.threLabel);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.threBar);
            this.Location = new System.Drawing.Point(10, 10);
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
    }
}