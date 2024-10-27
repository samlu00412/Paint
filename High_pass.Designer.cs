namespace Paint {
    partial class High_pass {
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
            this.Kernal_bar = new System.Windows.Forms.TrackBar();
            this.Preview_box = new System.Windows.Forms.PictureBox();
            this.Confirm_btn = new System.Windows.Forms.Button();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.Kersize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Kernal_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Preview_box)).BeginInit();
            this.SuspendLayout();
            // 
            // Kernal_bar
            // 
            this.Kernal_bar.BackColor = System.Drawing.SystemColors.Control;
            this.Kernal_bar.Location = new System.Drawing.Point(17, 16);
            this.Kernal_bar.Margin = new System.Windows.Forms.Padding(4);
            this.Kernal_bar.Maximum = 14;
            this.Kernal_bar.Minimum = 3;
            this.Kernal_bar.Name = "Kernal_bar";
            this.Kernal_bar.Size = new System.Drawing.Size(790, 90);
            this.Kernal_bar.SmallChange = 2;
            this.Kernal_bar.TabIndex = 9;
            this.Kernal_bar.Value = 3;
            this.Kernal_bar.Scroll += new System.EventHandler(this.Kernal_bar_Scroll);
            // 
            // Preview_box
            // 
            this.Preview_box.Location = new System.Drawing.Point(17, 71);
            this.Preview_box.Margin = new System.Windows.Forms.Padding(4);
            this.Preview_box.Name = "Preview_box";
            this.Preview_box.Size = new System.Drawing.Size(887, 513);
            this.Preview_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Preview_box.TabIndex = 10;
            this.Preview_box.TabStop = false;
            // 
            // Confirm_btn
            // 
            this.Confirm_btn.Location = new System.Drawing.Point(1028, 539);
            this.Confirm_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Confirm_btn.Name = "Confirm_btn";
            this.Confirm_btn.Size = new System.Drawing.Size(108, 45);
            this.Confirm_btn.TabIndex = 12;
            this.Confirm_btn.Text = "確定";
            this.Confirm_btn.UseVisualStyleBackColor = true;
            this.Confirm_btn.Click += new System.EventHandler(this.Confirm_click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Location = new System.Drawing.Point(912, 539);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(108, 45);
            this.Cancel_btn.TabIndex = 11;
            this.Cancel_btn.Text = "取消";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_click);
            // 
            // Kersize
            // 
            this.Kersize.AutoSize = true;
            this.Kersize.Location = new System.Drawing.Point(848, 28);
            this.Kersize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Kersize.Name = "Kersize";
            this.Kersize.Size = new System.Drawing.Size(21, 24);
            this.Kersize.TabIndex = 13;
            this.Kersize.Text = "3";
            // 
            // High_pass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 600);
            this.Controls.Add(this.Kersize);
            this.Controls.Add(this.Confirm_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.Preview_box);
            this.Controls.Add(this.Kernal_bar);
            this.Name = "High_pass";
            this.Text = "High_pass";
            this.Load += new System.EventHandler(this.High_pass_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Kernal_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Preview_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TrackBar Kernal_bar;
        private System.Windows.Forms.PictureBox Preview_box;
        private System.Windows.Forms.Button Confirm_btn;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.Label Kersize;
    }
}