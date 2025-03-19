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
        private bool adaptiveModeSelected = false;
        private Paint __mainform;
        private Mat tempCanvas;
        private bool check = false;
        private const int previewScale = 2;
        private AdaptiveThresholdTypes adaptiveType;
        private ThresholdTypes type = ThresholdTypes.Binary;
        private readonly Dictionary<string, ThresholdTypes> mode = new Dictionary<string, ThresholdTypes> {
            {"Binary", ThresholdTypes.Binary }, {"Binary_inverse", ThresholdTypes.BinaryInv},
            {"Tozero", ThresholdTypes.Tozero }, {"Tozero_inverse", ThresholdTypes.TozeroInv},
            {"Trunc", ThresholdTypes.Trunc}, {"Otsu", ThresholdTypes.Otsu}
        };

        private readonly Dictionary<string, AdaptiveThresholdTypes> adaptiveMode = new Dictionary<string, AdaptiveThresholdTypes> {
            {"Adaptive_Mean", AdaptiveThresholdTypes.MeanC},
            {"Adaptive_Gaussian", AdaptiveThresholdTypes.GaussianC}
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

            binarizationForm.ApplyThreshold();

            mainform.AdjustmentCanvas();
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
        private void BarScroll(object sender, EventArgs e) {
            thresholdVal = threBar.Value / magnitudeScalar;
            threLabel.Text = $"{thresholdVal}";
        }
        private async void change_mode(object sender, EventArgs e) {
            if (mode.ContainsKey(select_mode_Box.Text))
            {
                type = mode[select_mode_Box.Text];
                adaptiveModeSelected = false;
            }
            else if (adaptiveMode.ContainsKey(select_mode_Box.Text))
            {
                adaptiveType = adaptiveMode[select_mode_Box.Text];
                adaptiveModeSelected = true;
            }

            await UpdatePreviewAsync(thresholdVal);
        }
        private async Task UpdatePreviewAsync(double thre) {
            Mat previewResult = new Mat();

            await Task.Run(() =>
            {
                if (__mainform.canvas.Type() == MatType.CV_32FC2) {
                    ConvertFFTtoVisible(previewResult);
                }
                else if (adaptiveModeSelected)
                {
                    int blockSize = 11; // 預設值
                    blockSizeBar.Invoke(new Action(() =>
                    {
                        blockSize = blockSizeBar.Value;  // ✅ 確保這段程式碼在 UI 執行緒上執行
                    }));
                    if (blockSize % 2 == 0) blockSize++; // 確保 blockSize 為奇數
                    int C = 2;
                    blockSizeBar.Invoke(new Action(() =>
                    {
                        C = cValueBar.Value;  // ✅ 確保這段程式碼在 UI 執行緒上執行
                    }));
                    // 使用自適應二值化
                    Cv2.AdaptiveThreshold(
                        __mainform.canvas, previewResult,
                        255,
                        adaptiveType,
                        ThresholdTypes.Binary,
                        blockSize, C
                    );
                }
                else
                {
                    // 使用標準二值化
                    Cv2.Threshold(__mainform.canvas, previewResult, thre, 255, type);
                }
            });

            UpdatePictureBox(previewResult);
            clear(previewResult);
        }

        private void UpdatePictureBox(Mat image) {
            if (previewBox.Image != null) {
                previewBox.Image.Dispose();
                previewBox.Image = null;
            }

            if (image.Type() == MatType.CV_32FC2) {
                // 轉換 FFT 影像為預覽圖
                Mat[] fftPlanes = new Mat[2];
                Cv2.Split(image, out fftPlanes);
                Mat magnitude = new Mat();
                Cv2.Magnitude(fftPlanes[0], fftPlanes[1], magnitude);

                Cv2.Log(magnitude + 1, magnitude);
                Cv2.Normalize(magnitude, magnitude, 0, 255, NormTypes.MinMax);
                magnitude.ConvertTo(magnitude, MatType.CV_8UC1);
                previewBox.Image = BitmapConverter.ToBitmap(magnitude);
                clear(fftPlanes[0]);
                clear(fftPlanes[1]);
                clear(magnitude);
            }
            else {
                previewBox.Image = BitmapConverter.ToBitmap(image);
            }
        }


        private void cancel_click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void confirm_click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;

            if (__mainform.canvas.Type() == MatType.CV_32FC2) {
                ConvertFFTtoVisible(__mainform.canvas);
            }
            else if (__mainform.canvas.Type() == MatType.CV_8UC1) {
                Cv2.Threshold(__mainform.canvas, __mainform.canvas, thresholdVal, 255, type);
            }

            Close();
        }

        private void clear(Mat img) {
            img.Dispose();
            img = null;
        }

        private void ignore_cross(object sender, EventArgs e) {
            check = !check;
        }
        private void ApplyThreshold()
        {
            if (__mainform.canvas.Type() == MatType.CV_8UC1)
            {
                Mat result = new Mat();

                if (adaptiveModeSelected)
                {
                    int blockSize = blockSizeBar.Value;
                    if (blockSize % 2 == 0) blockSize++; // 確保 blockSize 為奇數
                    int C = cValueBar.Value;

                    Cv2.AdaptiveThreshold(__mainform.canvas, result, 255, adaptiveType, ThresholdTypes.Binary, blockSize, C);
                }
                else
                {
                    Cv2.Threshold(__mainform.canvas, result, thresholdVal, 255, type);
                }

                // 更新 `mainform.canvas`
                __mainform.canvas = result.Clone();
                UpdatePictureBox(result);

                // 清理記憶體
                result.Dispose();
            }
        }

        private void ConvertFFTtoVisible(Mat targetCanvas) {
            Mat[] fftPlanes = new Mat[2];
            Cv2.Split(__mainform.canvas, out fftPlanes);

            Mat magnitude = new Mat();
            Cv2.Magnitude(fftPlanes[0], fftPlanes[1], magnitude);

            // 確保 threshold 在正確範圍內
            Cv2.Log(magnitude + 1, magnitude);
            Cv2.Normalize(magnitude, magnitude, 0, 1, NormTypes.MinMax);

            Mat mask = new Mat();
            Cv2.Threshold(magnitude, mask, thresholdVal / 255.0, 1, type);

            // 確保 mask 轉換為浮點數，以便乘法運算
            Mat maskFloat = new Mat();
            mask.ConvertTo(maskFloat, MatType.CV_32FC1);
            int cx = mask.Cols / 2;
            int cy = mask.Rows / 2;
            int lineWidth = 15; // 紅線寬度範圍

            for (int i = Math.Max(0, cy - lineWidth); i < Math.Min(mask.Rows, cy + lineWidth); i++) {
                maskFloat.Row(i).SetTo(1);
            }
            for (int j = Math.Max(0, cx - lineWidth); j < Math.Min(mask.Cols, cx + lineWidth); j++) {
                maskFloat.Col(j).SetTo(1);
            }

            // 將遮罩應用到 FFT 影像
            Mat maskedRe = new Mat();
            Mat maskedIm = new Mat();
            Cv2.Multiply(fftPlanes[0], maskFloat, maskedRe);
            Cv2.Multiply(fftPlanes[1], maskFloat, maskedIm);
            Mat resultFFT = new Mat();
            Cv2.Merge(new Mat[] { maskedRe, maskedIm }, resultFFT);

            // 更新 canvas
            resultFFT.CopyTo(targetCanvas);

            // 清理資源
            clear(fftPlanes[0]);
            clear(fftPlanes[1]);
            clear(magnitude);
            clear(mask);
            clear(maskFloat);
            clear(maskedRe);
            clear(maskedIm);
            clear(resultFFT);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private async void modify_value(object sender, MouseEventArgs e) {
            await UpdatePreviewAsync(thresholdVal);
        }

        private void blockSizeBar_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"{blockSizeBar.Value}";
            UpdatePreviewAsync(thresholdVal);
        }

        private void cValueBar_Scroll(object sender, EventArgs e)
        {
            label2.Text = $"{cValueBar.Value}";
            UpdatePreviewAsync(thresholdVal);
        }
    }
}
