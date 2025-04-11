namespace Paint {
    partial class Defect {
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
            this.preview_box = new System.Windows.Forms.PictureBox();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.size_bar = new System.Windows.Forms.TrackBar();
            this.size_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.preview_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.size_bar)).BeginInit();
            this.SuspendLayout();
            // 
            // preview_box
            // 
            this.preview_box.Location = new System.Drawing.Point(7, 35);
            this.preview_box.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.preview_box.Name = "preview_box";
            this.preview_box.Size = new System.Drawing.Size(409, 257);
            this.preview_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.preview_box.TabIndex = 6;
            this.preview_box.TabStop = false;
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(475, 270);
            this.confirm.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(49, 22);
            this.confirm.TabIndex = 10;
            this.confirm.Text = "確定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(421, 270);
            this.cancel.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(49, 22);
            this.cancel.TabIndex = 9;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_click);
            // 
            // size_bar
            // 
            this.size_bar.Location = new System.Drawing.Point(7, 8);
            this.size_bar.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.size_bar.Maximum = 255;
            this.size_bar.Minimum = 1;
            this.size_bar.Name = "size_bar";
            this.size_bar.Size = new System.Drawing.Size(365, 45);
            this.size_bar.TabIndex = 11;
            this.size_bar.Value = 1;
            this.size_bar.Scroll += new System.EventHandler(this.modify_size);
            // 
            // size_label
            // 
            this.size_label.AutoSize = true;
            this.size_label.Location = new System.Drawing.Point(391, 14);
            this.size_label.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.size_label.Name = "size_label";
            this.size_label.Size = new System.Drawing.Size(11, 12);
            this.size_label.TabIndex = 12;
            this.size_label.Text = "1";
            // 
            // Defect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 300);
            this.Controls.Add(this.size_label);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.preview_box);
            this.Controls.Add(this.size_bar);
            this.Name = "Defect";
            this.Text = "Defect";
            ((System.ComponentModel.ISupportInitialize)(this.preview_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.size_bar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox preview_box;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
        public System.Windows.Forms.TrackBar size_bar;
        private System.Windows.Forms.Label size_label;
    }
}