namespace PaintApp
{
    partial class Normalize
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
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.normalmax = new System.Windows.Forms.TrackBar();
            this.確認 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normalmax)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Location = new System.Drawing.Point(86, 106);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(406, 263);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 6;
            this.pictureBoxPreview.TabStop = false;
            // 
            // normalmax
            // 
            this.normalmax.Location = new System.Drawing.Point(39, 12);
            this.normalmax.Maximum = 255;
            this.normalmax.Name = "normalmax";
            this.normalmax.Size = new System.Drawing.Size(550, 69);
            this.normalmax.TabIndex = 7;
            this.normalmax.Scroll += new System.EventHandler(this.normalmax_Scroll);
            // 
            // 確認
            // 
            this.確認.Location = new System.Drawing.Point(617, 320);
            this.確認.Name = "確認";
            this.確認.Size = new System.Drawing.Size(76, 49);
            this.確認.TabIndex = 8;
            this.確認.Text = "確認";
            this.確認.UseVisualStyleBackColor = true;
            this.確認.Click += new System.EventHandler(this.Confirm_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(640, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "0";
            // 
            // Normalize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.確認);
            this.Controls.Add(this.normalmax);
            this.Controls.Add(this.pictureBoxPreview);
            this.Name = "Normalize";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normalmax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.TrackBar normalmax;
        private System.Windows.Forms.Button 確認;
        private System.Windows.Forms.Label label1;
    }
}