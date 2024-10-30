using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Size = OpenCvSharp.Size;


namespace Paint {
    public partial class Gauss : Form {
        public Size Kernal = new Size(1,1);
        public double Sigma = 1.0;
        private Paint __mainform;
        private Mat tempCanvas;
        private const int previewScale = 2;
        public Gauss(OpenCvSharp.Size initKernal, double initSigma,Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            Kernal_value.Text = "";Sigma_value.Text = "";
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
            Kernal.Width = Kernal_bar.Value*2 - 3;
            Kernal.Height = Kernal_bar.Value*2 - 3;
            Kernal_value.Text = $"{Kernal_bar.Value*2 - 3}";
            await UpdatePreviewAsync(Kernal, Sigma);
        }

        private async void Sigma_bar_Scroll(object sender, EventArgs e) {
            Sigma = Sigma_bar.Value / 100.0;
            Sigma_value.Text = $"{Sigma_bar.Value / 100.0}";
            await UpdatePreviewAsync(Kernal, Sigma);
        }
        private async Task UpdatePreviewAsync(Size K,double S) {
            await Task.Run(() => Cv2.GaussianBlur(__mainform.canvas, tempCanvas, K, S));
            UpdatePictureBox(tempCanvas);
        }

        private void Kernal_bar_ValueChanged(object sender, EventArgs e) {
            Kernal.Width = Kernal_bar.Value * 2 - 3;
            Kernal.Height = Kernal_bar.Value * 2 - 3;
        }

        private void Sigma_bar_ValueChanged(object sender, EventArgs e) {
            Sigma = Sigma_bar.Value / 100.0;
        }

        private void Confirm_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Cv2.GaussianBlur(__mainform.canvas, __mainform.canvas, Kernal, Sigma);
            Close();
        }

        private void Discard_Click(object sender, EventArgs e) {
            DialogResult= DialogResult.Cancel;
            Close();
        }
    }
}
