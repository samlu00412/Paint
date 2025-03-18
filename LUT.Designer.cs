namespace PaintApp
{
    partial class LUT
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
            this.beblack = new System.Windows.Forms.TrackBar();
            this.bewhite = new System.Windows.Forms.TrackBar();
            this.確認 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.beblack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bewhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // beblack
            // 
            this.beblack.Location = new System.Drawing.Point(44, 39);
            this.beblack.Maximum = 255;
            this.beblack.Name = "beblack";
            this.beblack.Size = new System.Drawing.Size(550, 69);
            this.beblack.TabIndex = 0;
            this.beblack.Scroll += new System.EventHandler(this.beblack_Scroll);
            // 
            // bewhite
            // 
            this.bewhite.Location = new System.Drawing.Point(44, 127);
            this.bewhite.Maximum = 255;
            this.bewhite.Name = "bewhite";
            this.bewhite.Size = new System.Drawing.Size(550, 69);
            this.bewhite.TabIndex = 1;
            this.bewhite.Scroll += new System.EventHandler(this.bewhite_Scroll);
            // 
            // 確認
            // 
            this.確認.Location = new System.Drawing.Point(680, 378);
            this.確認.Name = "確認";
            this.確認.Size = new System.Drawing.Size(76, 49);
            this.確認.TabIndex = 2;
            this.確認.Text = "確認";
            this.確認.UseVisualStyleBackColor = true;
            this.確認.Click += new System.EventHandler(this.Confirm_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(667, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(667, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Location = new System.Drawing.Point(64, 218);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(406, 263);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 5;
            this.pictureBoxPreview.TabStop = false;
            // 
            // LUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 555);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.確認);
            this.Controls.Add(this.bewhite);
            this.Controls.Add(this.beblack);
            this.Name = "LUT";
            this.Load += new System.EventHandler(this.LUT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.beblack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bewhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar beblack;
        private System.Windows.Forms.TrackBar bewhite;
        private System.Windows.Forms.Button 確認;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
    }
}