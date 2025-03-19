namespace PaintApp
{
    partial class equalizeHist
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
            this.確認 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Location = new System.Drawing.Point(88, 157);
            this.pictureBoxPreview.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(609, 394);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 7;
            this.pictureBoxPreview.TabStop = false;
            // 
            // 確認
            // 
            this.確認.Location = new System.Drawing.Point(924, 449);
            this.確認.Margin = new System.Windows.Forms.Padding(4);
            this.確認.Name = "確認";
            this.確認.Size = new System.Drawing.Size(114, 74);
            this.確認.TabIndex = 9;
            this.確認.Text = "確認";
            this.確認.UseVisualStyleBackColor = true;
            this.確認.Click += new System.EventHandler(this.Confirm_click);
            // 
            // equalizeHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 683);
            this.Controls.Add(this.確認);
            this.Controls.Add(this.pictureBoxPreview);
            this.Name = "equalizeHist";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button 確認;
    }
}