using System;
using System.Runtime;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;
using System.Windows.Forms;
using Emgu.CV.Ocl;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Size = OpenCvSharp.Size;


namespace Paint {
    public partial class Gauss : Form {
        public Size Kernal = new Size(1, 1);
        public double Sigma = 1.0;
        public int KS = 1;
        private Paint __mainform;
        private Mat tempCanvas;
        private const int previewScale = 2;
        public Gauss(OpenCvSharp.Size initKernal, double initSigma, Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            Kernal_value.Text = ""; Sigma_value.Text = "";
            tempCanvas = tempCanvas.Resize(new OpenCvSharp.Size(tempCanvas.Width / previewScale, tempCanvas.Height / previewScale));
            Kernal_bar.Scroll += new EventHandler(Kernal_bar_Scroll);
            Sigma_bar.Scroll += new EventHandler(Sigma_bar_Scroll);
            Preview_box.BringToFront();
            UpdatePictureBox(tempCanvas);
        }

        private void UpdatePictureBox(Mat image) {
            if (Preview_box.Image != null)
                Preview_box.Image.Dispose();
            Preview_box.Image = BitmapConverter.ToBitmap(image);
        }

        private async void Kernal_bar_Scroll(object sender, EventArgs e) {
            Kernal.Width = Kernal_bar.Value * 2 - 3;
            Kernal.Height = Kernal_bar.Value * 2 - 3;
            KS = Kernal_bar.Value * 2 - 3;
            Kernal_value.Text = $"{Kernal_bar.Value * 2 - 3}";
            await UpdatePreviewAsync(Kernal, Sigma);
        }

        private async void Sigma_bar_Scroll(object sender, EventArgs e) {
            Sigma = Sigma_bar.Value / 100.0;
            Sigma_value.Text = $"{Sigma_bar.Value / 100.0}";
            await UpdatePreviewAsync(Kernal, Sigma);
        }
        private async Task UpdatePreviewAsync(Size K, double S) {
            await Task.Run(() => Cv2.GaussianBlur(__mainform.canvas, tempCanvas, K, S));
            UpdatePictureBox(tempCanvas);
        }

        private void Kernal_bar_ValueChanged(object sender, EventArgs e) {
            Kernal.Width = Kernal_bar.Value * 2 - 3;
            Kernal.Height = Kernal_bar.Value * 2 - 3;
            KS = Kernal_bar.Value * 2 - 3;
        }

        private void Sigma_bar_ValueChanged(object sender, EventArgs e) {
            Sigma = Sigma_bar.Value / 100.0;
        }

        private void Confirm_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            //__mainform.canvas = ManualGaussianBlur(__mainform.canvas, MakeKernal(KS,Sigma));
            Cv2.GaussianBlur(__mainform.canvas, __mainform.canvas, Kernal, Sigma);
            Close();
        }

        private void Discard_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Gauss_Load(object sender, EventArgs e) {

        }

        private double[,] MakeKernal(int kerSize, double Sig) {
            double[,] kernel = new double[kerSize, kerSize];
            double sum = 0;
            int center = kerSize / 2;
            for (int i = 0; i < kerSize; i++) {
                for (int j = 0; j < kerSize; j++) {
                    int x = i - center;
                    int y = j - center;
                    kernel[i, j] = Math.Exp(-(x * x + y * y) / (2.0 * Sig * Sig)) / (2.0 * Math.PI * Sig * Sig);
                    sum += kernel[i, j];
                }
            }

            for (int i = 0; i < kerSize; i++) {
                for (int j = 0; j < kerSize; j++) {
                    kernel[i, j] /= sum;
                }
            }
            return kernel;
        }
        
        public static T clamp<T>(T value, T min, T max) where T : IComparable<T> {
            if (value.CompareTo(min) < 0) return min;
            if (value.CompareTo(max) > 0) return max;
            return value;
        }
        private Mat ManualGaussianBlur(Mat img, double[,] kernel) {
            int rows = img.Width;
            int cols = img.Height;
            int kernelSize = kernel.GetLength(0);
            int radius = kernelSize / 2;
            Mat result = new Mat(cols,rows,img.Type());

            for (int y = 0; y < cols; y++) {
                for (int x = 0; x < rows; x++) {
                    double newValue = 0;
                    for (int ki = -radius; ki <= radius; ki++) {
                        for (int kj = -radius; kj <= radius; kj++) {
                            int ny = clamp(y + ki, 0, cols - 1);
                            int nx = clamp(x + kj, 0, rows - 1);
                            newValue += img.At<byte>(ny, nx) * kernel[ki + radius, kj + radius];
                        }
                    }
                    result.At<byte>(y, x) = (byte)clamp(newValue, 0, 255);
                }
            }

            return result;
        }
    }
}
