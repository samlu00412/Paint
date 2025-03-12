using Emgu.CV.Structure;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintApp
{
    public partial class binarization : Form {

        private const double magnitudeScalar = 100.0;
        private double thresholdVal = 0.0;
        private Paint __mainform;
        private Mat tempCanvas;
        private bool check = false;
        private const int previewScale = 2;
        private ThresholdTypes type = ThresholdTypes.Binary;
        private readonly Dictionary<string, ThresholdTypes> mode = new Dictionary<string, ThresholdTypes> {
            {"Binary",ThresholdTypes.Binary }, {"Binary_inverse",ThresholdTypes.BinaryInv},
            {"Tozero",ThresholdTypes.Tozero }, {"Tozero_inverse",ThresholdTypes.TozeroInv},
            {"Trunc", ThresholdTypes.Trunc}, {"Otsu", ThresholdTypes.Otsu}
        };
        public binarization(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            tempCanvas = tempCanvas.Resize(new OpenCvSharp.Size(tempCanvas.Width / previewScale, tempCanvas.Height / previewScale));
            threBar.Scroll += new EventHandler(BarScroll);
            select_mode_Box.TextChanged += new EventHandler(change_mode);
            UpdatePictureBox(tempCanvas);
        }
        public static void OpenAndSetThresholdMode(Paint mainform, string modeName, double thresholdValue)
        {
            binarization binarizationForm = new binarization(mainform);
            binarizationForm.SetThresholdMode(modeName, thresholdValue);

            // ✅ 直接套用二值化，不顯示視窗
            binarizationForm.ApplyThreshold();
            mainform.AdjustmentCanvas();

            // ✅ 釋放記憶體
            binarizationForm.Dispose();
        }

        public void SetThresholdMode(string modeName, double thresholdValue)
        {
            if (mode.ContainsKey(modeName))
            {
                type = mode[modeName];
            }
            thresholdVal = thresholdValue;
            threBar.Value = (int)(thresholdVal * magnitudeScalar);
            threLabel.Text = $"{thresholdVal}";
            UpdatePreviewAsync(thresholdVal).ConfigureAwait(false);
        }
        private async void BarScroll(object sender, EventArgs e) {
            thresholdVal = threBar.Value / magnitudeScalar;
            threLabel.Text = $"{thresholdVal}";
            await UpdatePreviewAsync(thresholdVal);
        }
        private async void change_mode(object sender, EventArgs e) {
            type = mode[select_mode_Box.Text];
            await UpdatePreviewAsync(thresholdVal);
        }
        private async Task UpdatePreviewAsync(double thre) {
            await Task.Run(() => Cv2.Threshold(__mainform.canvas,tempCanvas,thre,255,type));
            UpdatePictureBox(tempCanvas);
        }
        private void UpdatePictureBox(Mat image) {
            if (previewBox.Image != null) {
                previewBox.Image.Dispose();
                previewBox.Image = null;
            }
            if(image.Type() == MatType.CV_32FC2) {

            }
            else {
            previewBox.Image = BitmapConverter.ToBitmap(image);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        

        private void cancel_click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void confirm_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            
            Cv2.Threshold(__mainform.canvas,__mainform.canvas,thresholdVal,255,type);
            Close();
        }

        private void ignore_cross(object sender, EventArgs e) {
            check = !check;
        }
    }
}
