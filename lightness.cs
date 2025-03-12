using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using PaintApp;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using EmguCVMat = Emgu.CV.Mat;
using OpenCvSharpMat = OpenCvSharp.Mat;
namespace PaintApp
{
    public partial class lightness : Form {
        Paint mainform;
        Mat show;
        private const int scaleFactor = 100; // 放大倍數，這樣可以達到兩位小數的精度
        public double TrackBarValue1 { get; private set; } = 1.0;
        public double TrackBarValue2 { get; private set; } = 0;
        public lightness(Paint _mainform) {
            InitializeComponent();
            mainform = _mainform;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {

        }

        private void lightness_Load(object sender, EventArgs e) {
            con_label.Text = $"Value: {trackBar2.Value}";
            bri_label.Text = $"Value: {trackBar1.Value / (double)scaleFactor}";
            show = mainform.canvas.Clone();
            UpdateCanvas();
        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;  // 設置 DialogResult 為 OK，表示用戶點擊了確定
            Close();
        }

        private async void trackBar1_Scroll(object sender, EventArgs e) {
            bri_label.Text = $"Value: {trackBar1.Value / (double)scaleFactor}";
            con_label.Text = $"Value: {trackBar2.Value}";
            double alpha = trackBar1.Value / (double)scaleFactor;
            int beta=trackBar2.Value;
            if (trackBar1.Value > 0)
                show = await Task.Run(() => AdjustContrastAndBrightness(mainform.canvas, alpha, beta));
            else
                show = await Task.Run(() => AdjustNegativeContrast(mainform.canvas, alpha, beta));
            UpdateCanvas();
        }

        private void button2_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void UpdateCanvas()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = BitmapConverter.ToBitmap(show);
        }
        private OpenCvSharpMat AdjustNegativeContrast(OpenCvSharpMat image, double alpha, int beta)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int c = 0; c < 3; c++)
                    {
                        // 反轉像素值並調整對比度
                        double pixel = 255 - color[c];
                        pixel = Math.Abs(alpha) * pixel + beta;
                        // 裁剪到0-255範圍
                        newColor[c] = (byte)(pixel < 0 ? 0 : (pixel > 255 ? 255 : pixel));
                    }

                    newImage.Set(y, x, newColor);
                }
            }

            return newImage;
        }

        private OpenCvSharpMat AdjustContrastAndBrightness(OpenCvSharpMat image, double alpha, int beta)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int c = 0; c < 3; c++)
                    {
                        // 調整對比度和亮度
                        double newValue = color[c] * alpha + beta;
                        // 裁剪到 0-255 範圍
                        newColor[c] = (byte)(newValue < 0 ? 0 : (newValue > 255 ? 255 : newValue));
                    }

                    newImage.Set(y, x, newColor);
                }
            }
            return newImage;
        }
    }
}
