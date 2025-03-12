using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintApp{
    public partial class CLAHE : Form {
        private double limit_val = 2.0;
        private Paint __mainform;
        private Mat tempCanvas;
        private const int previewScale = 2;

        public CLAHE(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            tempCanvas = tempCanvas.Resize(new OpenCvSharp.Size(tempCanvas.Width / previewScale, tempCanvas.Height / previewScale));
            Limit_bar.Scroll += LimitBarScroll;
            Preview_box.BringToFront();
            UpdatePictureBox(tempCanvas);

        }

        private void Cancel_btn_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Confirm_btn_Click(object sender, EventArgs e) {
            DialogResult= DialogResult.OK;
            var final_clahe = Cv2.CreateCLAHE(clipLimit: limit_val, tileGridSize: new OpenCvSharp.Size(8, 8));
            final_clahe.Apply(__mainform.canvas,__mainform.canvas);
            final_clahe.Dispose();
            Close();
        }

        private async void LimitBarScroll(object sender, EventArgs e) {
            limit_val = Limit_bar.Value * 0.1;
            limit_label.Text = $"{limit_val}";
            await UpdatePreviewAsync(limit_val);
        }
        private async Task UpdatePreviewAsync(double Limit) {
            var clahe = Cv2.CreateCLAHE(clipLimit: Limit, tileGridSize: new OpenCvSharp.Size(8, 8));
            await Task.Run(() => clahe.Apply(__mainform.canvas,tempCanvas));
            UpdatePictureBox(tempCanvas);
            clahe.Dispose();
        }
        private void UpdatePictureBox(Mat image) {
            if (Preview_box.Image != null)
                Preview_box.Image.Dispose();
            Preview_box.Image = BitmapConverter.ToBitmap(image);
        }
    }
}
