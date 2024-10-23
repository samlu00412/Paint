namespace Paint {
    partial class Gauss {
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
            this.Sigma_bar = new System.Windows.Forms.TrackBar();
            this.Kernal_bar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Sigma_value = new System.Windows.Forms.Label();
            this.Kernal_value = new System.Windows.Forms.Label();
            this.Discard = new System.Windows.Forms.Button();
            this.Confirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Sigma_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Kernal_bar)).BeginInit();
            this.SuspendLayout();
            // 
            // Sigma_bar
            // 
            this.Sigma_bar.Location = new System.Drawing.Point(122, 227);
            this.Sigma_bar.Maximum = 500;
            this.Sigma_bar.Name = "Sigma_bar";
            this.Sigma_bar.Size = new System.Drawing.Size(499, 90);
            this.Sigma_bar.TabIndex = 2;
            this.Sigma_bar.Value = 100;
            this.Sigma_bar.Scroll += new System.EventHandler(this.Sigma_bar_Scroll);
            this.Sigma_bar.ValueChanged += new System.EventHandler(this.Sigma_bar_ValueChanged);
            // 
            // Kernal_bar
            // 
            this.Kernal_bar.Location = new System.Drawing.Point(122, 83);
            this.Kernal_bar.Maximum = 29;
            this.Kernal_bar.Minimum = 1;
            this.Kernal_bar.Name = "Kernal_bar";
            this.Kernal_bar.Size = new System.Drawing.Size(499, 90);
            this.Kernal_bar.TabIndex = 3;
            this.Kernal_bar.Value = 1;
            this.Kernal_bar.Scroll += new System.EventHandler(this.Kernal_bar_Scroll);
            this.Kernal_bar.ValueChanged += new System.EventHandler(this.Kernal_bar_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "標準差";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "核大小";
            // 
            // Sigma_value
            // 
            this.Sigma_value.AutoSize = true;
            this.Sigma_value.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Sigma_value.Location = new System.Drawing.Point(627, 230);
            this.Sigma_value.Name = "Sigma_value";
            this.Sigma_value.Size = new System.Drawing.Size(110, 32);
            this.Sigma_value.TabIndex = 7;
            this.Sigma_value.Text = "核大小";
            // 
            // Kernal_value
            // 
            this.Kernal_value.AutoSize = true;
            this.Kernal_value.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Kernal_value.Location = new System.Drawing.Point(627, 90);
            this.Kernal_value.Name = "Kernal_value";
            this.Kernal_value.Size = new System.Drawing.Size(110, 32);
            this.Kernal_value.TabIndex = 8;
            this.Kernal_value.Text = "核大小";
            // 
            // Discard
            // 
            this.Discard.Location = new System.Drawing.Point(469, 345);
            this.Discard.Name = "Discard";
            this.Discard.Size = new System.Drawing.Size(131, 51);
            this.Discard.TabIndex = 9;
            this.Discard.Text = "取消";
            this.Discard.UseVisualStyleBackColor = true;
            this.Discard.Click += new System.EventHandler(this.Discard_Click);
            // 
            // Confirm
            // 
            this.Confirm.Location = new System.Drawing.Point(606, 345);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(131, 51);
            this.Confirm.TabIndex = 10;
            this.Confirm.Text = "確定";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // Gauss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.Discard);
            this.Controls.Add(this.Kernal_value);
            this.Controls.Add(this.Sigma_value);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Kernal_bar);
            this.Controls.Add(this.Sigma_bar);
            this.Name = "Gauss";
            this.Text = "Gauss";
            ((System.ComponentModel.ISupportInitialize)(this.Sigma_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Kernal_bar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar Sigma_bar;
        private System.Windows.Forms.TrackBar Kernal_bar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Sigma_value;
        private System.Windows.Forms.Label Kernal_value;
        private System.Windows.Forms.Button Discard;
        private System.Windows.Forms.Button Confirm;
    }
}