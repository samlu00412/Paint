using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;


namespace Paint {
    public partial class Paint : Form {
        public Paint() {
            InitializeComponent();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void 開啟ToolStripMenuItem_Click(object sender, EventArgs e) {
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                pictureBox1.Load(openFileDialog.FileName);
                SizeImage();
                //Centralize();
            }
        }
        //影像長寬讀取
        private void SizeImage() {
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
        }
        //影像置中
        private void Centralize() {
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
        }

        private void 檢視ToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void Enlarge_click(object sender, EventArgs e) {
            pictureBox1.Width = Convert.ToInt32(pictureBox1.Width*1.1);
            pictureBox1.Height = Convert.ToInt32(pictureBox1.Height * 1.1);
            //Centralize();
        }

        private void Shrink_click(object sender, EventArgs e) {
            pictureBox1.Width = Convert.ToInt32(pictureBox1.Width / 1.1);
            pictureBox1.Height = Convert.ToInt32(pictureBox1.Height / 1.1);
            //Centralize();
        }

        private void New_canva_click(object sender, EventArgs e) {
            int width = 1280, height = 720;
            Mat newCanvas = new Mat(new OpenCvSharp.Size(width, height), MatType.CV_8UC3, Scalar.All(255));
            // 將 Mat 轉換為 Bitmap
            Bitmap bitmap = BitmapConverter.ToBitmap(newCanvas);

            // 將 Bitmap 設置為 PictureBox 的圖像
            pictureBox1.Image = bitmap;
            pictureBox1.Width = width;
            pictureBox1.Height = height;
        }
    }
}
