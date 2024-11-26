using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint {
    
    public partial class Low_pass : Form {
        private int Ksize = 3;
        private Paint __mainform;
        private Mat tempCanvas;
        private const int previewScale = 2; // 預覽縮小倍率

        public Low_pass(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            tempCanvas = tempCanvas.Resize(new OpenCvSharp.Size(tempCanvas.Width / previewScale, tempCanvas.Height / previewScale));
            Kernal_bar.Scroll += Kernal_change_scroll;
            Preview_box.BringToFront();
            UpdatePictureBox(tempCanvas);
        }

        private void Cancel_btn_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            tempCanvas.Dispose();
            Close();
        }

        private void Confirm_btn_Click(object sender, EventArgs e) {
            DialogResult= DialogResult.OK;
            __mainform.canvas = ManualMedianBlur(__mainform.canvas, Ksize);
            tempCanvas.Dispose();
            Close();
        }

        private async void Kernal_change_scroll(object sender, EventArgs e) {
                Kersize.Text = $"{Kernal_bar.Value*2 - 3}";
                Ksize = Int32.Parse(Kersize.Text);
                await UpdatePreviewAsync(Ksize);
        }

        private void Change_Ksize(object sender, EventArgs e) {
            Ksize = Int32.Parse(Kersize.Text);
        }
        private async Task UpdatePreviewAsync(int kernal) {  
            await Task.Run(() => Cv2.MedianBlur(__mainform.canvas, tempCanvas, kernal));
            UpdatePictureBox(tempCanvas);
        }
        private void UpdatePictureBox(Mat image) {
            if (Preview_box.Image != null) 
                Preview_box.Image.Dispose();
            Preview_box.Image = BitmapConverter.ToBitmap(image);
        }

        private void Low_pass_Load(object sender, EventArgs e) {

        }

        private int clamp(int num, int min, int max) {
            if (num < min) return min;
            else if (num > max) return max;
            return num;
        }
        private Mat ManualMedianBlur(Mat src, int ksize) {
            if (ksize % 2 == 0 || ksize < 1)
                throw new ArgumentException("Kernel size must be an odd number and greater than 1.");

            int rows = src.Rows;
            int cols = src.Cols;

            // 創建輸出圖像
            Mat result = new Mat(rows, cols, src.Type());
            int radius = ksize / 2; 

            // 遍歷每個像素
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    List<byte> window = new List<byte>();

                    // 獲取窗口內的像素值
                    for (int ki = -radius; ki <= radius; ki++) {
                        for (int kj = -radius; kj <= radius; kj++) {
                            int y = clamp(i + ki, 0, rows - 1);
                            int x = clamp(j + kj, 0, cols - 1); 
                            window.Add(src.At<byte>(y, x));
                        }
                    }
                    window.Sort();
                    byte median = window[window.Count / 2];

                    // 設定中值為當前像素的新值
                    result.Set(i, j, median);
                }
            }
            return result;
        }
    }
}
