namespace Paint {
    partial class contour {
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.low_thre_bar = new System.Windows.Forms.TrackBar();
            this.cancel = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            this.low_thre_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.low_thre_bar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(16, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(887, 514);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // low_thre_bar
            // 
            this.low_thre_bar.Location = new System.Drawing.Point(16, 16);
            this.low_thre_bar.Maximum = 25500;
            this.low_thre_bar.Minimum = 1;
            this.low_thre_bar.Name = "low_thre_bar";
            this.low_thre_bar.Size = new System.Drawing.Size(790, 90);
            this.low_thre_bar.TabIndex = 6;
            this.low_thre_bar.Value = 100;
            this.low_thre_bar.Scroll += new System.EventHandler(this.low_thre_scroll);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(913, 539);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(107, 45);
            this.cancel.TabIndex = 7;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_click);
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(1030, 539);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(107, 45);
            this.confirm.TabIndex = 8;
            this.confirm.Text = "確定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_click);
            // 
            // low_thre_label
            // 
            this.low_thre_label.AutoSize = true;
            this.low_thre_label.Location = new System.Drawing.Point(848, 29);
            this.low_thre_label.Name = "low_thre_label";
            this.low_thre_label.Size = new System.Drawing.Size(38, 24);
            this.low_thre_label.TabIndex = 9;
            this.low_thre_label.Text = "1.0";
            // 
            // contour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 600);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.low_thre_label);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.low_thre_bar);
            this.Name = "contour";
            this.Text = "contour";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.low_thre_bar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TrackBar low_thre_bar;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Label low_thre_label;
    }
}