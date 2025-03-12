using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PaintApp
{
    public partial class Gamma : Form
    {
        private Paint _mainForm;
        private Mat originalImage, previewImage;
        private const int previewScale = 2; // 預覽縮小倍率

        public Gamma(Paint mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            trackBar1.TickFrequency = 100;
        }

        public void Gamma_Load(object sender, EventArgs e)
        {
            originalImage = _mainForm.canvas.Clone();
            previewImage = originalImage.Resize(new OpenCvSharp.Size(originalImage.Width / previewScale, originalImage.Height / previewScale));
            UpdatePictureBox(previewImage);
        }

        private async void gamma_Scroll(object sender, EventArgs e)
        {
            double gammaValue = trackBar1.Value / 100.0;
            label1.Text = gammaValue.ToString();

            // 異步更新預覽
            await UpdatePreviewAsync(gammaValue);
        }

        private async Task UpdatePreviewAsync(double gamma)
        {
            // 在後台進行伽瑪校正，減少UI卡頓
            var processedImage = await Task.Run(() => ApplyGammaCorrection(previewImage, gamma));

            // 更新PictureBox顯示
            UpdatePictureBox(processedImage);
        }

        private void check_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void UpdatePictureBox(Mat image)
        {
            // 更新 PictureBox，避免資源洩漏
            if (pictureBox1.Image != null){
                pictureBox1.Image.Dispose();
            }

            pictureBox1.Image = BitmapConverter.ToBitmap(image);
        }

        private Mat ApplyGammaCorrection(Mat image, double gamma)
        {
            Mat correctedImage = new Mat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b color = image.At<Vec3b>(y, x);
                    for (int c = 0; c < 3; c++)
                    {
                        double pixel = color[c] / 255.0;
                        pixel = Math.Pow(pixel, gamma);
                        correctedImage.At<Vec3b>(y, x)[c] = (byte)Clamp(pixel * 255.0, 0, 255);
                    }
                }
            }

            return correctedImage;
        }

        private double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
