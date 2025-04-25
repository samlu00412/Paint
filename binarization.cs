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
        private bool TrySetThresholdMode(string modeName)
        {
            if (mode.TryGetValue(modeName, out var normalType))
            {
                type = normalType;
                adaptiveModeSelected = false;
                return true;
            }
            else if (adaptiveMode.TryGetValue(modeName, out var adpType))
            {
                adaptiveType = adpType;
                adaptiveModeSelected = true;
                return true;
            }

            Console.WriteLine($"[錯誤] 模式名稱無效: {modeName}");
            return false;
        }
        public binarization(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
        }
        private void binarization_Load(object sender, EventArgs e)
        {
            
            
            tempCanvas = tempCanvas.Resize(new OpenCvSharp.Size(tempCanvas.Width / previewScale, tempCanvas.Height / previewScale));
            threBar.Scroll += new EventHandler(BarScroll);
            select_mode_Box.TextChanged += new EventHandler(change_mode);
            UpdatePictureBox(tempCanvas);
        }
        public static void OpenAndSetThresholdMode(Paint mainform, string modeName, double thresholdValue, int? blockSize = null, int? cValue = null, int? fftSeed = null)
        {
            int defaultBlockSize = 11;
            int defaultCValue = 2;
            int defaultSeed = 10;
           
            var binarizationForm = new PaintApp.binarization(mainform)
            {
                ShowInTaskbar = false,
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                StartPosition = FormStartPosition.Manual,
                Location = new System.Drawing.Point(-2000, -2000)
            };

            binarizationForm.Show();
            Application.DoEvents();
            binarizationForm.thresholdVal = thresholdValue;
            //binarizationForm.threBar.Value = (int)(thresholdValue * 100.0);

            Console.WriteLine("hi i am fine");
            //binarizationForm.select_mode_Box.Text = modeName;
            //binarizationForm.change_mode(binarizationForm.select_mode_Box, EventArgs.Empty);
            binarizationForm.TrySetThresholdMode(modeName);
            Console.WriteLine("hi i am fine");
            binarizationForm.blockSizeBar.Value = blockSize ?? defaultBlockSize;

            binarizationForm.cValueBar.Value = cValue ?? defaultCValue;
            Console.WriteLine("hi i am fine");
            binarizationForm.seed.Value = fftSeed ?? defaultSeed;

            var type = mainform.canvas.Type();

            if (type == MatType.CV_32FC2)
            {
                binarizationForm.ConvertFFTtoVisible(mainform.canvas);
            }
            else if (type == MatType.CV_8UC1)
            {
                binarizationForm.ApplyThreshold();
            }
            else
            {
                // 自動轉為灰階
                Mat gray = new Mat();
                Cv2.CvtColor(mainform.canvas, gray, ColorConversionCodes.BGR2GRAY);
                mainform.canvas = gray;
                binarizationForm.ApplyThreshold();
            }
            binarizationForm.Close();


        }
        
        public void SetThresholdMode(string modeName, double thresholdValue)
        {
            if (mode.ContainsKey(modeName))
            {
                type = mode[modeName];
            }
            thresholdVal = thresholdValue;
            threBar.Value = (int)(thresholdVal / magnitudeScalar);
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
            int localBlockSize = blockSizeBar.Value;
            int localC = cValueBar.Value;
            await Task.Run(() =>
            {
                if (__mainform.canvas.Type() == MatType.CV_32FC2) {
                    ConvertFFTtoVisible(previewResult);
                }
                else if (adaptiveModeSelected)
                {
                    if (localBlockSize % 2 == 0) localBlockSize++; // 確保 blockSize 為奇數
                    int C = localC;
                    // 使用自適應二值化
                    Cv2.AdaptiveThreshold(
                        __mainform.canvas, previewResult,
                        255,
                        adaptiveType,
                        ThresholdTypes.Binary,
                        localBlockSize, C
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
                ifft();
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
            else if (__mainform.canvas.Type() == MatType.CV_8UC1 && adaptiveModeSelected!=true) {
                Cv2.Threshold(__mainform.canvas, __mainform.canvas, thresholdVal, 255, type);
            }
            else
            {
                Cv2.AdaptiveThreshold(
                        __mainform.canvas, __mainform.canvas,
                        255,
                        adaptiveType,
                        ThresholdTypes.Binary,
                        blockSizeBar.Value, cValueBar.Value
                    );
            }

            Close();
        }

        private void clear(Mat img) {
            //img.Dispose();
            img = null;
        }
        private void ShiftDFT(Mat magImage)
        {
            int cx = magImage.Cols / 2;
            int cy = magImage.Rows / 2;

            Mat q0 = new Mat(magImage, new Rect(0, 0, cx, cy));   // Top-Left
            Mat q1 = new Mat(magImage, new Rect(cx, 0, cx, cy));  // Top-Right
            Mat q2 = new Mat(magImage, new Rect(0, cy, cx, cy));  // Bottom-Left
            Mat q3 = new Mat(magImage, new Rect(cx, cy, cx, cy)); // Bottom-Right
            Mat tmp = new Mat();
            q0.CopyTo(tmp);
            q3.CopyTo(q0);
            tmp.CopyTo(q3);

            q1.CopyTo(tmp);
            q2.CopyTo(q1);
            tmp.CopyTo(q2);

            q0.Dispose();
            q1.Dispose();
            q2.Dispose();
            q3.Dispose();
            tmp.Dispose();
            q0 = null;
            q1 = null;
            q2 = null;
            q3 = null;
            tmp = null;
        }
        private void ifft()
        {
            Mat mat = new Mat();
            ConvertFFTtoVisible_two(mat);
            ShiftDFT(mat);

            Mat inverseTransform = new Mat();
            Cv2.Idft(mat, inverseTransform, DftFlags.Scale | DftFlags.RealOutput);

            // Step 3: 取得實部（因為 IFFT 會輸出 CV_32FC2）
            Mat[] planes = new Mat[2];
            Cv2.Split(inverseTransform, out planes);
            Mat restoredImage = planes[0]; // 取實部

            Rect roi = new Rect(0, 0, __mainform.ow, __mainform.oh);
            Mat cropped = new Mat(restoredImage, roi);

            // Step 4: 轉換回 8-bit 影像（可顯示）
            Mat displayImage = new Mat();
            Cv2.Normalize(cropped, displayImage, 0, 255, NormTypes.MinMax);
            displayImage.ConvertTo(displayImage, MatType.CV_8UC1);


            // 更新 canvas
            ifftimage.Image = BitmapConverter.ToBitmap(displayImage);
            planes[0].Dispose();
            inverseTransform.Dispose();
            restoredImage.Dispose();
            displayImage.Dispose();
        }
        private void ignore_cross(object sender, EventArgs e) {
            check = !check;
        }
        private Mat GetCenterRegionMask(Mat magnitude, OpenCvSharp.Point seedPoint, double regionThreshold)
        {
            // 初始化遮罩：與 magnitude 同樣大小，初始值為 0
            Mat mask = Mat.Zeros(magnitude.Size(), MatType.CV_8UC1);

            // 建立 BFS 隊列
            Queue<OpenCvSharp.Point> queue = new Queue<OpenCvSharp.Point>();
            queue.Enqueue(seedPoint);

            // 取得種子點像素值（基準灰度）
            byte seedValue = magnitude.At<byte>(seedPoint.Y, seedPoint.X);

            // 記錄是否拜訪過
            bool[,] visited = new bool[magnitude.Rows, magnitude.Cols];
            //bfs dfs
            while (queue.Count > 0)
            {
                var pt = queue.Dequeue();

                if (pt.X < 0 || pt.X >= magnitude.Cols || pt.Y < 0 || pt.Y >= magnitude.Rows)
                {
                    continue;
                }

                if (visited[pt.Y, pt.X])
                    continue;

                visited[pt.Y, pt.X] = true;

                byte currentValue = magnitude.At<byte>(pt.Y, pt.X);
                if (Math.Abs(currentValue - seedValue) <= regionThreshold)
                {
                    mask.Set<byte>(pt.Y, pt.X, 255);

                    // 加入 4 向鄰點
                    queue.Enqueue(new OpenCvSharp.Point(pt.X + 1, pt.Y));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X - 1, pt.Y));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X, pt.Y + 1));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X, pt.Y - 1));

                    
                    queue.Enqueue(new OpenCvSharp.Point(pt.X + 1, pt.Y+1));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X + 1, pt.Y-1));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X - 1, pt.Y+1));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X - 1, pt.Y-1));

                    
                    queue.Enqueue(new OpenCvSharp.Point(pt.X + 2, pt.Y));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X - 2, pt.Y));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X, pt.Y + 2));
                    queue.Enqueue(new OpenCvSharp.Point(pt.X, pt.Y - 2));

                }
            }
            
            return mask;
        }

        private void ApplyThreshold()
        {
            if (__mainform.canvas.Type() == MatType.CV_8UC1)
            {
                Mat result = new Mat();

                if (adaptiveModeSelected)
                {
                    Console.Write("owo");
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
            }
        }
        Mat displayPreview = new Mat();
        public void ConvertFFTtoVisible(Mat targetCanvas)
        {
            Mat[] fftPlanes = new Mat[2];
            Cv2.Split(__mainform.canvas, out fftPlanes); // CV_32FC2 -> 2 x CV_32FC1

            // 計算 Magnitude，做 log 與 normalize，變成灰階圖可視化
            Mat magnitude = new Mat();
            Cv2.Magnitude(fftPlanes[0], fftPlanes[1], magnitude);
            Cv2.Log(magnitude + 1, magnitude);
            Cv2.Normalize(magnitude, magnitude, 0, 255, NormTypes.MinMax);
            magnitude.ConvertTo(magnitude, MatType.CV_8UC1);

            // 對 magnitude 做 threshold
            Mat thresholdedFFT = new Mat();
            Cv2.Threshold(magnitude, thresholdedFFT, thresholdVal, 255, type);

            // 區域生長遮罩（以 magnitude 中心點）
            OpenCvSharp.Point centerSeed = new OpenCvSharp.Point(magnitude.Cols / 2, magnitude.Rows / 2);
            int seedVal = 0;
            seed.Invoke(new Action(() => {
                seedVal = seed.Value;
            }));

            Mat centerMask = GetCenterRegionMask(magnitude, centerSeed, seedVal);

            // 合併 threshold 結果與中心遮罩
            Cv2.BitwiseOr(thresholdedFFT, centerMask, thresholdedFFT); // thresholdedFFT 是 mask

            // 🔁 將 single-channel mask 轉為 float，用來套用到 fftPlanes
            Mat maskFloat = new Mat();
            thresholdedFFT.ConvertTo(maskFloat, MatType.CV_32FC1, 1.0 / 255.0); // 0 或 1 的浮點遮罩

            // 套用 mask 到實部與虛部
            Mat maskedRe = new Mat();
            Mat maskedIm = new Mat();
            Cv2.Multiply(fftPlanes[0], maskFloat, maskedRe);
            Cv2.Multiply(fftPlanes[1], maskFloat, maskedIm);

            // 合併回雙通道 FFT 結果
            Mat maskedFFT = new Mat();
            Cv2.Merge(new Mat[] { maskedRe, maskedIm }, maskedFFT);

            // ✅ 更新 targetCanvas，保持 CV_32FC2
            maskedFFT.CopyTo(targetCanvas);

            // ✅ 顯示 preview 圖（單通道）
            previewBox.Image = BitmapConverter.ToBitmap(thresholdedFFT);

            // 清除暫存
            clear(fftPlanes[0]);
            clear(fftPlanes[1]);
            clear(magnitude);
            clear(centerMask);
            clear(maskFloat);
            clear(maskedRe);
            clear(maskedIm);
            clear(maskedFFT);
        }
        private void ConvertFFTowo(Mat targetCanvas)
        {
            Mat[] fftPlanes = new Mat[2];
            Cv2.Split(__mainform.canvas, out fftPlanes); // CV_32FC2 -> 2 x CV_32FC1

            // 計算 Magnitude，做 log 與 normalize，變成灰階圖可視化
            Mat magnitude = new Mat();
            Cv2.Magnitude(fftPlanes[0], fftPlanes[1], magnitude);
            Cv2.Log(magnitude + 1, magnitude);
            Cv2.Normalize(magnitude, magnitude, 0, 255, NormTypes.MinMax);
            magnitude.ConvertTo(magnitude, MatType.CV_8UC1);

            // 對 magnitude 做 threshold
            Mat thresholdedFFT = new Mat();
            Cv2.Threshold(magnitude, thresholdedFFT, thresholdVal, 255, type);

            // 區域生長遮罩（以 magnitude 中心點）
            OpenCvSharp.Point centerSeed = new OpenCvSharp.Point(magnitude.Cols / 2, magnitude.Rows / 2);
            int seedVal = 0;
            seed.Invoke(new Action(() => {
                seedVal = seed.Value;
            }));

            Mat centerMask = GetCenterRegionMask(magnitude, centerSeed, seedVal);

            // 合併 threshold 結果與中心遮罩
            Cv2.BitwiseOr(thresholdedFFT, centerMask, thresholdedFFT); // thresholdedFFT 是 mask

            // 🔁 將 single-channel mask 轉為 float，用來套用到 fftPlanes
            Mat maskFloat = new Mat();
            thresholdedFFT.ConvertTo(maskFloat, MatType.CV_32FC1, 1.0 / 255.0); // 0 或 1 的浮點遮罩

            // 套用 mask 到實部與虛部
            Mat maskedRe = new Mat();
            Mat maskedIm = new Mat();
            Cv2.Multiply(fftPlanes[0], maskFloat, maskedRe);
            Cv2.Multiply(fftPlanes[1], maskFloat, maskedIm);

            // 合併回雙通道 FFT 結果
            Mat maskedFFT = new Mat();
            Cv2.Merge(new Mat[] { maskedRe, maskedIm }, maskedFFT);

            // ✅ 更新 targetCanvas，保持 CV_32FC2
            maskedFFT.CopyTo(targetCanvas);

            // ✅ 顯示 preview 圖（單通道）
            previewBox.Image = BitmapConverter.ToBitmap(thresholdedFFT);

            // 清除暫存
            clear(fftPlanes[0]);
            clear(fftPlanes[1]);
            clear(magnitude);
            clear(thresholdedFFT);
            clear(centerMask);
            clear(maskFloat);
            clear(maskedRe);
            clear(maskedIm);
            clear(maskedFFT);
        }
        private void ConvertFFTtoVisible_two(Mat targetCanvas)
        {
            // 將 FFT 拆成實部與虛部
            Mat[] fftPlanes = new Mat[2];
            Cv2.Split(__mainform.canvas, out fftPlanes); // CV_32FC2 -> 2 x CV_32FC1

            // 計算 Magnitude，做 log 與 normalize，變成灰階圖可視化
            Mat magnitude = new Mat();
            Cv2.Magnitude(fftPlanes[0], fftPlanes[1], magnitude);
            Cv2.Log(magnitude + 1, magnitude);
            Cv2.Normalize(magnitude, magnitude, 0, 255, NormTypes.MinMax);
            magnitude.ConvertTo(magnitude, MatType.CV_8UC1);

            // 對 magnitude 做 threshold
            Mat thresholdedFFT = new Mat();
            Cv2.Threshold(magnitude, thresholdedFFT, thresholdVal, 255, type);

            // 區域生長遮罩（以 magnitude 中心點）
            OpenCvSharp.Point centerSeed = new OpenCvSharp.Point(magnitude.Cols / 2, magnitude.Rows / 2);
            int seedVal = 0;
            seed.Invoke(new Action(() => {
                seedVal = seed.Value;
            }));

            Mat centerMask = GetCenterRegionMask(magnitude, centerSeed, seedVal);

            // 合併 threshold 結果與中心遮罩
            Cv2.BitwiseOr(thresholdedFFT, centerMask, thresholdedFFT); // thresholdedFFT 是 mask

            // 🔁 將 single-channel mask 轉為 float，用來套用到 fftPlanes
            Mat maskFloat = new Mat();
            thresholdedFFT.ConvertTo(maskFloat, MatType.CV_32FC1, 1.0 / 255.0); // 0 或 1 的浮點遮罩

            // 套用 mask 到實部與虛部
            Mat maskedRe = new Mat();
            Mat maskedIm = new Mat();
            Cv2.Multiply(fftPlanes[0], maskFloat, maskedRe);
            Cv2.Multiply(fftPlanes[1], maskFloat, maskedIm);

            // 合併回雙通道 FFT 結果
            Mat maskedFFT = new Mat();
            Cv2.Merge(new Mat[] { maskedRe, maskedIm }, maskedFFT);

            // ✅ 更新 targetCanvas，保持 CV_32FC2
            maskedFFT.CopyTo(targetCanvas);

            // ✅ 顯示 preview 圖（單通道）
            ifftimage.Image = BitmapConverter.ToBitmap(thresholdedFFT);

            // 清除暫存
            clear(fftPlanes[0]);
            clear(fftPlanes[1]);
            clear(magnitude);
            clear(thresholdedFFT);
            clear(centerMask);
            clear(maskFloat);
            clear(maskedRe);
            clear(maskedIm);
            clear(maskedFFT);
        }

        private async void modify_value(object sender, MouseEventArgs e) {
            await UpdatePreviewAsync(thresholdVal);
        }

        private void blockSizeBar_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"{blockSizeBar.Value}";
        }

        private void cValueBar_Scroll(object sender, EventArgs e)
        {
            label2.Text = $"{cValueBar.Value}";
        }

        private void seed_Scroll(object sender, EventArgs e)
        {
            seedlabel.Text= $"{seed.Value}";
        }

    }
}
