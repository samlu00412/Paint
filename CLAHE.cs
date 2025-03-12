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
        private Mat lab = new Mat();
        private Mat[] labchannels;
        private Mat lchannel = new Mat();
        private Mat lclahe = new Mat();
        private const int previewScale = 2;

        public CLAHE(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            if (__mainform.canvas.Channels() != 1) {
                Cv2.CvtColor(__mainform.canvas, lab, ColorConversionCodes.BGR2Lab);
                labchannels = Cv2.Split(lab);
                lchannel = labchannels[0];
                labchannels[0].CopyTo(lclahe);
            }
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
            if(__mainform.canvas.Channels() == 1)
                await Task.Run(() => clahe.Apply(__mainform.canvas,tempCanvas));
            else {
                if (lclahe != null) {
                    lclahe.Dispose();
                    lclahe = null;
                }
                await Task.Run(() => clahe.Apply(lchannel, lclahe));
            }
            UpdatePictureBox(tempCanvas);
            clahe.Dispose();
        }
        private void UpdatePictureBox(Mat image) {
            if (Preview_box.Image != null)
                Preview_box.Image.Dispose();
            if(__mainform.canvas.Channels() != 1) {
                labchannels[0] = lclahe;
                Cv2.Merge(labchannels, lab);
                tempCanvas.Dispose();
                tempCanvas = null;
                tempCanvas = lab.Clone();
            }
            Preview_box.Image = BitmapConverter.ToBitmap(image);
        }
    }
}
