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

namespace PaintApp {
    public partial class CLAHE : Form {
        private double limit_val = 2.0;
        private Paint __mainform;
        private Mat tempCanvas;
        private Mat lab = new Mat();
        private Mat[] labchannels;
        private Mat lchannel = new Mat();
        private Mat lclahe = new Mat();
        private const int previewScale = 2;
        public static void OpenAndSetCLAHEMode(Paint mainform, double clipLimit = 2.0)
        {
            var claheForm = new CLAHE(mainform)
            {
                ShowInTaskbar = false,
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                StartPosition = FormStartPosition.Manual,
                Location = new System.Drawing.Point(-2000, -2000) // 隱藏視窗
            };
            Console.WriteLine(clipLimit);
            claheForm.Show();
            Application.DoEvents();

            // 設定 limit（轉為 bar 值）
            int barValue = (int)(clipLimit * 10);
            claheForm.Limit_bar.Value = Math.Max(1, Math.Min(barValue, claheForm.Limit_bar.Maximum)); // clamp 在 bar 範圍內
            claheForm.limit_val = clipLimit;
            claheForm.limit_label.Text = $"{clipLimit}";

            // 更新預覽
            claheForm.UpdatePreviewAsync(clipLimit).ConfigureAwait(false);

            claheForm.Confirm_btn_Click(null, EventArgs.Empty); // 直接套用 CLAHE 效果
            claheForm.Close();
        }
        public CLAHE(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            tempCanvas = tempCanvas.Resize(new OpenCvSharp.Size(tempCanvas.Width / previewScale, tempCanvas.Height / previewScale));

            if (__mainform.canvas.Channels() == 3) {
                Cv2.CvtColor(tempCanvas, lab, ColorConversionCodes.BGR2Lab);
                labchannels = Cv2.Split(lab);
                lchannel = labchannels[0].Clone();
                lclahe = lchannel.Clone();
            }
            Limit_bar.Scroll += LimitBarScroll;
            Preview_box.BringToFront();
            UpdatePictureBox(tempCanvas);
        }

        private void Cancel_btn_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Confirm_btn_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            using (var final_clahe = Cv2.CreateCLAHE(clipLimit: limit_val, tileGridSize: new OpenCvSharp.Size(8, 8))) {
                if (__mainform.canvas.Channels() == 1) {
                    // 直接應用 CLAHE（灰階圖）
                    final_clahe.Apply(__mainform.canvas, __mainform.canvas);
                }
                else {
                    if (lchannel == null || lchannel.IsDisposed || labchannels == null || labchannels.Length < 1 || labchannels[0].IsDisposed) {
                        Cv2.CvtColor(__mainform.canvas, lab, ColorConversionCodes.BGR2Lab);
                        labchannels = Cv2.Split(lab);
                        lchannel = labchannels[0].Clone();
                    }

                    using (Mat processedL = new Mat()) {
                        final_clahe.Apply(lchannel, processedL);
                        labchannels[0] = processedL.Clone(); // 更新 L 通道
                    }
                    Cv2.Merge(labchannels, lab);
                    Cv2.CvtColor(lab, __mainform.canvas, ColorConversionCodes.Lab2BGR);
                }
            }
            Close();
        }

        private async void LimitBarScroll(object sender, EventArgs e) {
            limit_val = Limit_bar.Value * 0.1;
            limit_label.Text = $"{limit_val}";
            await UpdatePreviewAsync(limit_val);
        }
        private async Task UpdatePreviewAsync(double Limit) {
            using (var clahe = Cv2.CreateCLAHE(clipLimit: Limit, tileGridSize: new OpenCvSharp.Size(8, 8))) {
                if (__mainform.canvas.Channels() == 1) 
                    await Task.Run(() => clahe.Apply(__mainform.canvas, tempCanvas));
                else {
                    if (lchannel == null || lchannel.IsDisposed || labchannels == null || labchannels.Length < 1 || labchannels[0].IsDisposed) {
                        Cv2.CvtColor(__mainform.canvas, lab, ColorConversionCodes.BGR2Lab);
                        labchannels = Cv2.Split(lab);
                        lchannel = labchannels[0].Clone();
                    }
                    using (Mat tempLChannel = lchannel.Clone()) {
                        await Task.Run(() => clahe.Apply(lchannel, tempLChannel));
                        labchannels[0] = tempLChannel.Clone();
                        Cv2.Merge(labchannels, lab);
                        Cv2.CvtColor(lab, tempCanvas, ColorConversionCodes.Lab2BGR);
                    }
                }
            }
            UpdatePictureBox(tempCanvas);
        }
        private void UpdatePictureBox(Mat image) {
            if (Preview_box.Image != null) {
                Preview_box.Image.Dispose();
                Preview_box.Image = null;
            }
            Preview_box.Image = BitmapConverter.ToBitmap(image);
        }
    }
}