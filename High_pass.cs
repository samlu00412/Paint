using System;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint {
    public partial class High_pass : Form{
        private int Ksize = 3; //default
        private Paint __mainform;
        private Mat tempCanvas;
        private const int previewScale = 2;
        public High_pass(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            tempCanvas = tempCanvas.Resize(new OpenCvSharp.Size(tempCanvas.Width / previewScale, tempCanvas.Height / previewScale));
            Kernal_bar.Scroll += Kernal_bar_Scroll;
            Preview_box.BringToFront();
            UpdatePictureBox(tempCanvas);
        }

        private void High_pass_Load(object sender, EventArgs e) {

        }

        private void Cancel_click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Confirm_click(object sender, EventArgs e) {
            DialogResult =DialogResult.OK;
            Mat finalKernal = new Mat(Ksize, Ksize, MatType.CV_32F, GenerateKernal(Ksize));
            Cv2.Filter2D(__mainform.canvas, __mainform.canvas, MatType.CV_8U, finalKernal);
            finalKernal.Dispose();
            Close();
        }

        private async void Kernal_bar_Scroll(object sender, EventArgs e) {
            Kersize.Text = $"{Kernal_bar.Value * 2 - 3}";
            Ksize = Int32.Parse(Kersize.Text);
            await UpdatePreviewAsync(Ksize);
        }

        private async Task UpdatePreviewAsync(int kernal) {
            Mat tempKernal = new Mat(Ksize, Ksize, MatType.CV_32F, GenerateKernal(Ksize));
            await Task.Run(() => Cv2.Filter2D(__mainform.canvas, tempCanvas, MatType.CV_8U, tempKernal));
            UpdatePictureBox(tempCanvas);
            tempKernal.Dispose();
        }

        private void UpdatePictureBox(Mat image) {
            if (Preview_box.Image != null)
                Preview_box.Image.Dispose();
            Preview_box.Image = BitmapConverter.ToBitmap(image);
        }
        private float[,] GenerateKernal(int size) {
            float[,] kernal = new float[size, size];
            for(int i = 0; i < kernal.GetLength(0); i++) {
                for (int j = 0; j < kernal.GetLength(1); j++)
                    kernal[i, j] = -1;
            }
            kernal[size / 2, size / 2] = size * size - 1;
            return kernal;
        }
    }
}
