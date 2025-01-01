namespace Paint {
    partial class CLAHE {
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
            this.limit_label = new System.Windows.Forms.Label();
            this.Confirm_btn = new System.Windows.Forms.Button();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.Preview_box = new System.Windows.Forms.PictureBox();
            this.Limit_bar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.Preview_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Limit_bar)).BeginInit();
            this.SuspendLayout();
            // 
            // limit_label
            // 
            this.limit_label.AutoSize = true;
            this.limit_label.Location = new System.Drawing.Point(844, 22);
            this.limit_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.limit_label.Name = "limit_label";
            this.limit_label.Size = new System.Drawing.Size(38, 24);
            this.limit_label.TabIndex = 18;
            this.limit_label.Text = "2.0";
            // 
            // Confirm_btn
            // 
            this.Confirm_btn.Location = new System.Drawing.Point(1028, 539);
            this.Confirm_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Confirm_btn.Name = "Confirm_btn";
            this.Confirm_btn.Size = new System.Drawing.Size(108, 45);
            this.Confirm_btn.TabIndex = 17;
            this.Confirm_btn.Text = "確定";
            this.Confirm_btn.UseVisualStyleBackColor = true;
            this.Confirm_btn.Click += new System.EventHandler(this.Confirm_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Location = new System.Drawing.Point(912, 539);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(108, 45);
            this.Cancel_btn.TabIndex = 16;
            this.Cancel_btn.Text = "取消";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // Preview_box
            // 
            this.Preview_box.Location = new System.Drawing.Point(17, 71);
            this.Preview_box.Margin = new System.Windows.Forms.Padding(4);
            this.Preview_box.Name = "Preview_box";
            this.Preview_box.Size = new System.Drawing.Size(887, 513);
            this.Preview_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Preview_box.TabIndex = 15;
            this.Preview_box.TabStop = false;
            // 
            // Limit_bar
            // 
            this.Limit_bar.BackColor = System.Drawing.SystemColors.Control;
            this.Limit_bar.Location = new System.Drawing.Point(17, 16);
            this.Limit_bar.Margin = new System.Windows.Forms.Padding(4);
            this.Limit_bar.Maximum = 100;
            this.Limit_bar.Minimum = 1;
            this.Limit_bar.Name = "Limit_bar";
            this.Limit_bar.Size = new System.Drawing.Size(790, 45);
            this.Limit_bar.SmallChange = 2;
            this.Limit_bar.TabIndex = 14;
            this.Limit_bar.Value = 20;
            this.Limit_bar.Scroll += new System.EventHandler(this.LimitBarScroll);
            // 
            // CLAHE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 600);
            this.Controls.Add(this.limit_label);
            this.Controls.Add(this.Confirm_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.Preview_box);
            this.Controls.Add(this.Limit_bar);
            this.Name = "CLAHE";
            this.Text = "CLAHE";
            ((System.ComponentModel.ISupportInitialize)(this.Preview_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Limit_bar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label limit_label;
        private System.Windows.Forms.Button Confirm_btn;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.PictureBox Preview_box;
        public System.Windows.Forms.TrackBar Limit_bar;
    }
}