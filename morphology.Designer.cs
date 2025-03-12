namespace PaintApp
{
    partial class morphology {
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
            this.iterBar = new System.Windows.Forms.TrackBar();
            this.iterLabel = new System.Windows.Forms.Label();
            this.select_mode_Box = new System.Windows.Forms.ComboBox();
            this.cancel = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterBar)).BeginInit();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Location = new System.Drawing.Point(10, 44);
            this.previewBox.Margin = new System.Windows.Forms.Padding(2);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(546, 321);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewBox.TabIndex = 6;
            this.previewBox.TabStop = false;
            // 
            // iterBar
            // 
            this.iterBar.Location = new System.Drawing.Point(10, 11);
            this.iterBar.Margin = new System.Windows.Forms.Padding(2);
            this.iterBar.Maximum = 12;
            this.iterBar.Minimum = 1;
            this.iterBar.Name = "iterBar";
            this.iterBar.Size = new System.Drawing.Size(486, 56);
            this.iterBar.TabIndex = 9;
            this.iterBar.Value = 1;
            this.iterBar.Scroll += new System.EventHandler(this.bar_scroll);
            // 
            // iterLabel
            // 
            this.iterLabel.AutoSize = true;
            this.iterLabel.Location = new System.Drawing.Point(522, 18);
            this.iterLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.iterLabel.Name = "iterLabel";
            this.iterLabel.Size = new System.Drawing.Size(14, 15);
            this.iterLabel.TabIndex = 10;
            this.iterLabel.Text = "1";
            // 
            // select_mode_Box
            // 
            this.select_mode_Box.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.select_mode_Box.FormattingEnabled = true;
            this.select_mode_Box.Items.AddRange(new object[] {
            "侵蝕",
            "膨脹",
            "開運算",
            "閉運算",
            "形態學梯度",
            "頂帽",
            "黑帽"});
            this.select_mode_Box.Location = new System.Drawing.Point(567, 44);
            this.select_mode_Box.Name = "select_mode_Box";
            this.select_mode_Box.Size = new System.Drawing.Size(132, 31);
            this.select_mode_Box.TabIndex = 11;
            this.select_mode_Box.Text = "侵蝕";
            this.select_mode_Box.TextChanged += new System.EventHandler(this.select_op);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(562, 337);
            this.cancel.Margin = new System.Windows.Forms.Padding(2);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(66, 28);
            this.cancel.TabIndex = 12;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_click);
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(634, 337);
            this.confirm.Margin = new System.Windows.Forms.Padding(2);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(66, 28);
            this.confirm.TabIndex = 13;
            this.confirm.Text = "確定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_click);
            // 
            // morphology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 375);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.select_mode_Box);
            this.Controls.Add(this.iterLabel);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.iterBar);
            this.Name = "morphology";
            this.Text = "morphology";
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewBox;
        public System.Windows.Forms.TrackBar iterBar;
        private System.Windows.Forms.Label iterLabel;
        private System.Windows.Forms.ComboBox select_mode_Box;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button confirm;
    }
}