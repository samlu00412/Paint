using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSize = OpenCvSharp.Size;
using Pen;
using System.Windows.Forms.DataVisualization.Charting;
using OpenCvSharpMat = OpenCvSharp.Mat;
using PaintApp;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms.VisualStyles;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis;
using Paint;
using System.Net.NetworkInformation;
using static PaintApp.Paint;
namespace PaintApp {

    public partial class Paint : Form {
        public int MAX_STACK_SIZE = 20;
        public OpenCvSharpMat canvas;
        public OpenCvSharpMat origin_picture;
        public OpenCvSharpMat tempCanvas = new OpenCvSharpMat();
        private OpenCvSharpMat chart = new OpenCvSharpMat();
        private OpenCvSharp.Point startpoint;
        private OpenCvSharp.Point prevPoint;
        private OpenCvSharp.Point currentPoint;
        public bool finddefect = false;
        public static Paint Instance { get; private set; }
        private Bitmap canvasBitmap;
        private bool isDrawing = false, isGrey = false;
        private string drawMode = "Free";
        private Rectangle showAspect;
        private Scalar currentColor = new Scalar(0, 0, 0);
        private int penThickness = 2;

        private Stack<Storage> Undo = new Stack<Storage>();
        private Stack<Storage> Redo = new Stack<Storage>();

        private OpenCvSize init_kernal = new OpenCvSize(1, 1);
        private double init_sigma = 1.0;
        private int realrows,realcols;
        private OpenCvSharp.Point convertPoint = new OpenCvSharp.Point(0, 0);
        public EvalWindow eval;
        public Paint() {
            InitializeComponent();
            this.KeyPreview = true; // 允許表單偵測按鍵
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            Instance = this; // 記錄當前的 Paint 視窗
            eval = new EvalWindow(this);
            eval.SetCode("Grayscale(); Threshold(\"Binary\", 0.3);");
            eval.Show();

        }
        public async Task<AOIInterface.AOIInterface> DetectAndFindAsync(Bitmap bitmap)
        {
            finddefect = false;
            canvas = BitmapConverter.ToMat(bitmap);
            origin_picture = canvas.Clone();
            UpdateCanvas();
            tempCanvas = canvas.Clone();
            SaveCurrentState();

            try
            {
                string codeText = File.ReadAllText("TextFile1.txt");
                eval.SetCode(codeText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取腳本失敗: " + ex.Message);
                eval.SetCode("// 無法讀取腳本");
            }

            await eval.RunCode();

            AOIInterface.AOIInterface result = new AOIInterface.AOIInterface
            {
                Bitmap = BitmapConverter.ToBitmap(canvas),
                Result = finddefect
            };

            return result;
        }

        // ✅ **建立 ScriptGlobals，讓 Eval 能夠存取 `Paint`**
        public class ScriptGlobals
        {
            public Paint PaintForm { get; set; }
            public void LowPass(int ksize)
            {
                Low_pass.OpenAndSetLowPassMode(PaintForm, ksize);
            }
            public void EqualizeHist()
            {
                equalizeHist.OpenAndSetEqualizeHistMode(PaintForm);
            }
            public void Grayscale()
            {
                PaintForm.轉換成灰階ToolStripMenuItem_Click(null, EventArgs.Empty);
            }
            public void FFT()
            {
                PaintForm.放棄toolStripMenuItem_Click(null, EventArgs.Empty);
                
            }
            public void Threshold(string modeName, double value, int blockSize = 11, int cValue = 2, int fftSeed = 10)
            {
                binarization.OpenAndSetThresholdMode(PaintForm, modeName, value, blockSize, cValue, fftSeed);
                PaintForm.AdjustmentCanvas();
            }
            public void IFFT()
            {
                PaintForm.iFFTToolStripMenuItem_Click(null, EventArgs.Empty);

            }
            public void ColorTransform(string mode)
            {
                var menuItem = new ToolStripMenuItem { Text = mode };
                PaintForm.colortransformToolStripMenuItem_Click(menuItem, EventArgs.Empty);
            }
            public void RGBmotify(int a,int b,int c,int d=666,int e=666,int f=666)
            {
                if (d == 666)
                    RGBtrans.OpenAndSetRGBTransform(PaintForm, a, 0, b, 0, c, 0);
                else
                    RGBtrans.OpenAndSetRGBTransform(PaintForm, a, b, c, d, e, f);
            }
            
            public void Clahe(double a)
            {
                CLAHE.OpenAndSetCLAHEMode(PaintForm,a);
                PaintForm.AdjustmentCanvas();
            }
            public void GAUSS(int kernelSize , double sigma)
            {
                Gauss.OpenAndSetGaussMode(PaintForm, kernelSize, sigma);
                PaintForm.AdjustmentCanvas();
            }
            public void C_B(double a, int b)
            {
                bool isNegative = (a < 0); // 檢查是否需要負片效果
                if (!isNegative)
                    PaintForm.canvas = PaintForm.AdjustContrastAndBrightness(PaintForm.canvas, a, b);
                else
                    PaintForm.canvas = PaintForm.AdjustNegativeContrast(PaintForm.canvas, a, b);
                PaintForm.AdjustmentCanvas();
            }
            public void Detect(int a)
            {
                Defect.OpenAndSetDefectMode(PaintForm, a);
                PaintForm.AdjustmentCanvas();
            }
            public void Reset()
            {
                PaintForm.canvas=PaintForm.origin_picture.Clone();
                PaintForm.AdjustmentCanvas();
            }
        }
        public class EvalWindow : Form
        {
            private TextBox codeInput;
            private TextBox outputBox;
            private Paint mainForm;

            public EvalWindow(Paint mainForm)
            {
                this.mainForm = mainForm;

                Text = "Code Evaluator";
                Size = new System.Drawing.Size(600, 400);

                codeInput = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Top,
                    Height = 200
                };

                Button runButton = new Button
                {
                    Text = "Run",
                    Dock = DockStyle.Bottom
                };

                outputBox = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    ReadOnly = true
                };

                runButton.Click += async (sender, e) =>
                {
                    try
                    {
                        var scriptOptions = ScriptOptions.Default
                            .WithReferences(AppDomain.CurrentDomain.GetAssemblies()
                                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                                .Select(a => MetadataReference.CreateFromFile(a.Location)))
                            .WithImports("System", "System.Linq", "System.Collections.Generic", "System.Windows.Forms", "PaintApp");

                        var globals = new ScriptGlobals { PaintForm = mainForm };

                        var result = await CSharpScript.EvaluateAsync<object>(
                            codeInput.Text, scriptOptions, globals: globals
                        );

                        outputBox.Text = result?.ToString() ?? "null";
                    }
                    catch (Exception ex)
                    {
                        outputBox.Text = "Error: " + ex.Message;
                    }
                };

                Controls.Add(codeInput);
                Controls.Add(runButton);
                Controls.Add(outputBox);
            }

            public void SetCode(string code)
            {
                codeInput.Text = code;
            }

            public void SetOutput(string output)
            {
                outputBox.Text = output;
            }
            public async Task RunCode()
            {
                try
                {
                    var scriptOptions = ScriptOptions.Default
                        .WithReferences(AppDomain.CurrentDomain.GetAssemblies()
                            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                            .Select(a => MetadataReference.CreateFromFile(a.Location)))
                        .WithImports("System", "System.Linq", "System.Collections.Generic", "System.Windows.Forms", "PaintApp");

                    var globals = new ScriptGlobals { PaintForm = mainForm };

                    var result = await CSharpScript.EvaluateAsync<object>(
                        codeInput.Text, scriptOptions, globals: globals
                    );

                    outputBox.Text = result?.ToString() ?? "null";
                }
                catch (Exception ex)
                {
                    outputBox.Text = "Error: " + ex.Message;
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e) {
            thicknessLabel.Text = $"Pen thickness: {penThickness}";
        }
        private void Form1_MouseWheel(object sender, MouseEventArgs e) {
            if (Control.ModifierKeys == Keys.Control) {
                if (e.Delta > 0) //up
                    Enlarge_click(sender, e);
                else if (e.Delta < 0) //down
                    Shrink_click(sender, e);
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.Z)
                復原UndoToolStripMenuItem_Click(sender, e);
            else if (e.Control && e.KeyCode == Keys.Y)
                重做RedoToolStripMenuItem_Click(sender, e);
            else if (e.Control && e.KeyCode == Keys.S)
                儲存檔案ToolStripMenuItem_Click(sender, e);
        }
        private void Enlarge_click(object sender, EventArgs e) {
            pictureBox1.Width = Convert.ToInt32(pictureBox1.Width * 1.1);
            pictureBox1.Height = Convert.ToInt32(pictureBox1.Height * 1.1);
            showAspect = GetImageRectangleInPictureBox();
        }

        private void Shrink_click(object sender, EventArgs e) {
            pictureBox1.Width = Convert.ToInt32(pictureBox1.Width / 1.1);
            pictureBox1.Height = Convert.ToInt32(pictureBox1.Height / 1.1);
            showAspect = GetImageRectangleInPictureBox();
        }

        private int CalculateDistance(OpenCvSharp.Point P1, OpenCvSharp.Point P2) {
            return (int)Math.Sqrt(Math.Pow(P2.X - P1.X, 2) + Math.Pow(P2.Y - P1.Y, 2));
        }
        private void New_canva_click(object sender, EventArgs e) {
            int width = 1280, height = 720;
            canvas = new OpenCvSharpMat(new OpenCvSize(width, height), MatType.CV_8UC3, Scalar.All(255));
            pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
            pictureBox1.Width = width;
            pictureBox1.Height = height;
            tempCanvas = canvas.Clone();
            SaveCurrentState();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                isDrawing = true;
                startpoint = ConvertToImageCoordinates(e.Location);
                prevPoint = ConvertToImageCoordinates(e.Location);
            }
            else if (e.Button == MouseButtons.Right) {
                isDrawing = true;
                startpoint.X = System.Windows.Forms.Cursor.Position.X;
                startpoint.Y = System.Windows.Forms.Cursor.Position.Y;
            }
        }
        //detecting mouse moving
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (isDrawing && e.Button == MouseButtons.Left) {
                currentPoint = ConvertToImageCoordinates(e.Location);
                DrawPreviewShape();
                ShowTempCanvas();
                if (drawMode != "Free") {
                    tempCanvas.SetTo(Scalar.All(255));
                    if (tempCanvas != null) {
                        tempCanvas.Dispose();
                        tempCanvas = null;
                    }
                    tempCanvas = canvas.Clone();
                }
            }
            else if (isDrawing && e.Button == MouseButtons.Right) {
                pictureBox1.Location = new System.Drawing.Point
                (
                    pictureBox1.Location.X + System.Windows.Forms.Cursor.Position.X - startpoint.X,
                    pictureBox1.Location.Y + System.Windows.Forms.Cursor.Position.Y - startpoint.Y
                );
                startpoint.X = System.Windows.Forms.Cursor.Position.X;
                startpoint.Y = System.Windows.Forms.Cursor.Position.Y;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            if (isDrawing && e.Button == MouseButtons.Left) {
                isDrawing = false;
                currentPoint = ConvertToImageCoordinates(e.Location);
                DrawFinalShape(); // 繪製圖形
                UpdateCanvas(); // 更新顯示
            }
            else if (isDrawing && e.Button == MouseButtons.Right)
                isDrawing = false;
        }
        private void ShowTempCanvas() {
            if (pictureBox1.Image != null) {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            if (tempCanvas.Type() != MatType.CV_32FC2){
                double ratioX = (double)pictureBox1.ClientSize.Width / canvas.Width;
                double ratioY = (double)pictureBox1.ClientSize.Height / canvas.Height;
                double scale = Math.Min(ratioX, ratioY); // 选择较小的比例适配 PictureBox

                int newWidth = (int)(tempCanvas.Width * scale);
                int newHeight = (int)(tempCanvas.Height * scale);

                // 使用最近邻插值缩放图片
                Mat resizedImage = new Mat();
                Cv2.Resize(tempCanvas, resizedImage, new OpenCvSharp.Size(newWidth, newHeight), 0, 0, InterpolationFlags.Nearest);

                // 更新到 PictureBox
                pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizedImage);
                resizedImage.Dispose();
            }
            else {
                if (pictureBox1.Image != null) {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                pictureBox1.Image = ConvertCV32FC2ToBitmap(tempCanvas, false);
            }
            GC.Collect(0);
            GC.WaitForPendingFinalizers();
        }
        //preview
        private void DrawPreviewShape() {
            if (drawMode == "Line") {
                Cv2.Line(tempCanvas, prevPoint, currentPoint, currentColor, penThickness);
            }
            else if (drawMode == "Rectangle") {
                Cv2.Rectangle(tempCanvas, prevPoint, currentPoint, currentColor, penThickness);
            }
            else if (drawMode == "Circle") {
                Cv2.Circle(tempCanvas, startpoint, CalculateDistance(startpoint, currentPoint), currentColor, penThickness);
            }
            else if (drawMode == "Ellipse") {
                OpenCvSize size = new OpenCvSize(Math.Abs(currentPoint.X - startpoint.X), Math.Abs(currentPoint.Y - startpoint.Y));
                Cv2.Ellipse(tempCanvas, startpoint, size, 0, 0, 360, currentColor, penThickness);
            }
            else if (drawMode == "Triangle") {
                OpenCvSharp.Point vertex;
                OpenCvSharp.Point vertex2;
                vertex.X = startpoint.X + (currentPoint.X - startpoint.X) / 2;//等腰三角形
                vertex.Y = currentPoint.Y;
                vertex2.X = currentPoint.X;
                vertex2.Y = startpoint.Y;
                OpenCvSharp.Point[] TrianglePoint = { startpoint, vertex2, vertex };
                Cv2.Polylines(tempCanvas, new[] { TrianglePoint }, true, currentColor, penThickness);
            }
            else {
                Cv2.Line(tempCanvas, prevPoint, currentPoint, currentColor, penThickness);
                prevPoint = currentPoint;
            }
        }
        //final
        private void DrawFinalShape() {
            DrawPreviewShape();
            if (canvas != null) {
                canvas.Dispose(); // 釋放舊的 canvas
                canvas = null;
            }
            canvas = new Mat();
            tempCanvas.CopyTo(canvas);
            SaveCurrentState();
            UpdateCanvas();
            Redo.Clear();
        }

        private void UpdateCanvas(Mat image = null){
            // 如果沒有傳入參數，則使用 canvas
            Mat displayImage = image ?? canvas;

            if (displayImage.Type() == MatType.CV_32FC2) {
                Console.WriteLine("owo owo owo");
                // 如果是複數矩陣，轉換為幅度圖顯示
                if (pictureBox1.Image != null) {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                pictureBox1.Image = ConvertCV32FC2ToBitmap(displayImage, false);
                return;
            }

            // 計算縮放比例
            double ratioX = (double)pictureBox1.Width / displayImage.Width;
            double ratioY = (double)pictureBox1.Height / displayImage.Height;
            double scale = Math.Min(ratioX, ratioY);

            int newWidth = (int)(displayImage.Width * scale);
            int newHeight = (int)(displayImage.Height * scale);

            // 縮放圖像
            Mat resizedImage = new Mat();
            Cv2.Resize(displayImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight), 0, 0, InterpolationFlags.Nearest);

            // 更新 pictureBox1
            if (pictureBox1.Image != null) {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            pictureBox1.Image = BitmapConverter.ToBitmap(resizedImage);

            // 釋放資源
            resizedImage.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private Rectangle GetImageRectangleInPictureBox() {
            // 計算圖像縮放後在 PictureBox 中的實際顯示範圍
            try {
                double imageAspect = (double)canvas.Width / canvas.Height;
                double boxAspect = (double)pictureBox1.Width / pictureBox1.Height;

                int width, height;
                if (imageAspect > boxAspect) {
                    // 圖像更寬，以寬為基準
                    width = pictureBox1.Width;
                    height = (int)(pictureBox1.Width / imageAspect);
                }
                else {
                    // 圖像更高，以高為基準
                    height = pictureBox1.Height;
                    width = (int)(pictureBox1.Height * imageAspect);
                }
                int x = (pictureBox1.Width - width) / 2;
                int y = (pictureBox1.Height - height) / 2;
                return new Rectangle(x, y, width, height);
            }
            catch(Exception ex) {
                MessageBox.Show($"{ex.Message}");
                return new Rectangle();
            }
        }

        private OpenCvSharp.Point ConvertToImageCoordinates(System.Drawing.Point mouseLocation) {
            var imgRect = GetImageRectangleInPictureBox();
            double scaleX = (double)canvas.Width / imgRect.Width;
            double scaleY = (double)canvas.Height / imgRect.Height;

            // 將滑鼠座標轉換為圖像座標
            int x = (int)((mouseLocation.X - imgRect.X) * scaleX);
            int y = (int)((mouseLocation.Y - imgRect.Y) * scaleY);

            // 防止座標超出圖像邊界
            x = Math.Max(0, Math.Min(canvas.Width - 1, x));
            y = Math.Max(0, Math.Min(canvas.Height - 1, y));

            return new OpenCvSharp.Point(x, y);
        }

        private readonly Dictionary<String, String> TypeToMode = new Dictionary<String, String>
        {
            {"自由","Free"},{"直線","Line"},{"橢圓","Ellipse" },{"圓","Circle"},
            {"矩形","Rectangle"},{"三角形","Triangle"}
        };
        private void SaveCurrentState() {
            if (Undo.Count >= MAX_STACK_SIZE){
                Undo = new Stack<Storage>(new Stack<Storage>(Undo).Skip(1)); // 移除最舊的狀態
            }
            Undo.Push(new Storage(canvas.Clone()));
            Redo.Clear();
        }
        public void AdjustmentCanvas() {

            SaveCurrentState();
            Redo.Clear();
            UpdateCanvas();
            canvas.CopyTo(tempCanvas);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            ToolStripMenuItem temp = (ToolStripMenuItem)sender;
            Console.WriteLine(temp.Text);
            drawMode = TypeToMode[temp.Text];
        }

        private void 儲存檔案ToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bitmap Image|*.bmp|JPeg Image|*.jpg|Gif Image|*.gif|Png Image|*.png";
            saveFileDialog.Title = "Save an Image File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                string filePath = saveFileDialog.FileName;//System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                if (canvas.Channels() == 2)
                    Cv2.ImWrite(filePath, BitmapConverter.ToMat(ConvertCV32FC2ToBitmap(canvas, false)));
                else
                    Cv2.ImWrite(filePath, canvas);
            }
            if (saveFileDialog != null)
                saveFileDialog.Dispose();
        }
        private void 開啟ToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog.Filter = "Bitmap Image|*.bmp|JPeg Image|*.jpg|Gif Image|*.gif|Png Image|*.png";
            openFileDialog.Title = "打開圖片";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                canvas = Cv2.ImRead(openFileDialog.FileName);
                origin_picture = Cv2.ImRead(openFileDialog.FileName);
                UpdateCanvas();
                tempCanvas = canvas.Clone();
                SaveCurrentState();
            }
        }
        private void 結束ToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void 復原UndoToolStripMenuItem_Click(object sender, EventArgs e) {
            if(Undo.Count > 0) {
                Redo.Push(Undo.Pop());
                if (Undo.Count == 0) 
                    Redo.Peek().Canvas.CopyTo(canvas);
                else
                    Undo.Peek().Canvas.CopyTo(canvas);
            }
            canvas.CopyTo(tempCanvas);
            UpdateCanvas();
        }
        private void 重做RedoToolStripMenuItem_Click(object sender, EventArgs e) {
            if(Redo.Count > 0) {
                Undo.Push(Redo.Pop());
                Undo.Peek().Canvas.CopyTo(canvas);
            }
            canvas.CopyTo(tempCanvas);
            UpdateCanvas();
        }

        public void 轉換成灰階ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (canvas.Channels() != 1)
                Cv2.CvtColor(canvas, canvas, ColorConversionCodes.BGR2GRAY);
            isGrey = true;
            AdjustmentCanvas();
        }

        private void 高斯模糊ToolStripMenuItem_Click(object sender, EventArgs e) {
            Gauss blurform = new Gauss(init_kernal, init_sigma, this);
            if (blurform.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            blurform.Dispose();
        }
        private void 亮度對比度ToolStripMenuItem_Click(object sender, EventArgs e) {
            lightness trackbarForm = new lightness(this);
            if (trackbarForm.ShowDialog() == DialogResult.OK) {
                double alpha = trackbarForm.trackBar1.Value / 100.0; // 對比度
                int beta = (int)trackbarForm.trackBar2.Value; // 亮度
                bool isNegative = (alpha < 0); // 檢查是否需要負片效果

                // 調整對比度和亮度
                if (!isNegative)
                    canvas = AdjustContrastAndBrightness(canvas, alpha, beta);
                else
                    canvas = AdjustNegativeContrast(canvas, alpha, beta);
                Console.WriteLine($"Alpha: {alpha}, Beta: {beta}, IsNegative: {isNegative}");
            }
            AdjustmentCanvas();
        }
        private OpenCvSharpMat AdjustNegativeContrast(OpenCvSharpMat image, double alpha, int beta) {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++) {
                for (int x = 0; x < image.Cols; x++) {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int c = 0; c < 3; c++) {
                        // 反轉像素值並調整對比度
                        double pixel = 255 - color[c];
                        pixel = Math.Abs(alpha) * pixel + beta;
                        // 裁剪到0-255範圍
                        newColor[c] = (byte)(pixel < 0 ? 0 : (pixel > 255 ? 255 : pixel));
                    }

                    newImage.Set(y, x, newColor);
                }
            }
            return newImage;
        }

        private OpenCvSharpMat AdjustContrastAndBrightness(OpenCvSharpMat image, double alpha, int beta) {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++) {
                for (int x = 0; x < image.Cols; x++) {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int c = 0; c < 3; c++) {
                        // 調整對比度和亮度
                        double newValue = color[c] * alpha + beta;
                        // 裁剪到 0-255 範圍
                        newColor[c] = (byte)(newValue < 0 ? 0 : (newValue > 255 ? 255 : newValue));
                    }

                    newImage.Set(y, x, newColor);
                }
            }
            return newImage;
        }
        private void 伽瑪ToolStripMenuItem_Click(object sender, EventArgs e) {
            Gamma trackbarForm = new Gamma(this);
            if (trackbarForm.ShowDialog() == DialogResult.OK) {
                double gamma = trackbarForm.trackBar1.Value / 100.0;
                canvas = 伽瑪轉換(canvas, gamma);
            }
            trackbarForm.Dispose();
            AdjustmentCanvas();
        }
        private OpenCvSharpMat 伽瑪轉換(OpenCvSharpMat image, double gamma) {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());


            for (int y = 0; y < image.Rows; y++) {
                for (int x = 0; x < image.Cols; x++) {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int c = 0; c < 3; c++) {
                        // 將像素值標準化到 [0, 1] 範圍
                        double pixel = color[c] / 255.0;
                        // 進行伽瑪校正
                        pixel = Math.Pow(pixel, gamma);
                        // 將結果轉換回 [0, 255] 範圍並裁剪
                        newColor[c] = (byte)(pixel * 255.0 > 255 ? 255 : (pixel * 255.0 < 0 ? 0 : pixel * 255.0));
                    }

                    newImage.Set(y, x, newColor);
                }
            }

            return newImage;
        }
        private void 低通濾波ToolStripMenuItem_Click(object sender, EventArgs e) {
            Low_pass LP_filter = new Low_pass(this);
            if (LP_filter.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            LP_filter.Dispose();
        }

        private void 高通濾波ToolStripMenuItem_Click(object sender, EventArgs e) {
            High_pass HP_filter = new High_pass(this);
            if (HP_filter.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            HP_filter.Dispose();
        }
        private void Pallate_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Color selectedColor = colorDialog.Color;
                currentColor = new Scalar(selectedColor.B, selectedColor.G, selectedColor.R); // BGR 格式
            }
            colorDialog.Dispose();
        }

        private void log變換ToolStripMenuItem_Click(object sender, EventArgs e) {
            log trackbarForm = new log(this);
            if (trackbarForm.ShowDialog() == DialogResult.OK) {
                double c = 255.0 / Math.Log(1 + trackbarForm.trackBar1.Value);
                canvas = Log變換(canvas, c);
            }
            trackbarForm.Dispose();
            AdjustmentCanvas();
        }

        private void 反logToolStripMenuItem_Click(object sender, EventArgs e) {
            反log trackbarForm = new 反log(this);
            if (trackbarForm.ShowDialog() == DialogResult.OK) {
                double c = trackbarForm.trackBar1.Value / 10.0;
                canvas = 反Log變換(canvas, c);
            }
            trackbarForm.Dispose();
            AdjustmentCanvas();
        }

        private OpenCvSharpMat Log變換(OpenCvSharpMat image, double c) {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++) {
                for (int x = 0; x < image.Cols; x++) {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int cIdx = 0; cIdx < 3; cIdx++) {
                        // 進行對數變換並標準化
                        double pixel = color[cIdx];
                        double logPixel = c * Math.Log(1 + pixel);

                        // 裁剪到0-255範圍
                        newColor[cIdx] = (byte)(logPixel > 255 ? 255 : (logPixel < 0 ? 0 : logPixel));
                    }

                    newImage.Set(y, x, newColor);
                }
            }

            return newImage;
        }
        private OpenCvSharpMat 反Log變換(OpenCvSharpMat image, double c) {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            // 進行反對數變換
            for (int y = 0; y < image.Rows; y++) {
                for (int x = 0; x < image.Cols; x++) {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int cIdx = 0; cIdx < 3; cIdx++) {
                        // 取得輸入像素值
                        double pixel = color[cIdx];

                        // 進行反對數變換
                        double invLogPixel = Math.Exp(pixel / c) - 1;

                        // 裁剪到0-255範圍
                        newColor[cIdx] = (byte)(invLogPixel > 255 ? 255 : (invLogPixel < 0 ? 0 : invLogPixel));
                    }

                    newImage.Set(y, x, newColor);
                }
            }

            return newImage;
        }
        private void btnShowHistogram_Click(object sender, EventArgs e) {
            // 計算亮度直方圖
            int[] histogram = CalculateBrightnessHistogram(canvas);

            // 繪製直方圖
            DrawBrightnessHistogram(histogram);
        }
        private int[] CalculateBrightnessHistogram(OpenCvSharpMat image) {
            // 將圖像轉換為灰度圖像
            OpenCvSharpMat grayImage = new OpenCvSharpMat();
            if (!isGrey) {
                Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            }
            else {
                grayImage = image;
            }

            // 初始化直方圖數組
            int[] histogram = new int[256];

            // 遍歷灰度圖像，計算每個亮度級別的像素數量
            for (int y = 0; y < grayImage.Rows; y++) {
                for (int x = 0; x < grayImage.Cols; x++) {
                    byte pixelValue = grayImage.At<byte>(y, x);
                    histogram[pixelValue]++;
                }
            }

            return histogram;
        }
        private void DrawBrightnessHistogram(int[] histogram) {
            // 初始化 Chart 控件
            Chart histogramChart = new Chart {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // 設置 ChartArea
            ChartArea chartArea = new ChartArea {
                Name = "ChartArea",
                AxisX ={
                        Title = "Brightness",
                        Minimum = 0, // 調整X軸起點，留出空間
                        Maximum = 255,
                        Interval = 50 // 設置X軸標籤的間隔
                },
                AxisY ={
                        Title = "Frequency",
                        Minimum = 0,
                        IntervalAutoMode = IntervalAutoMode.VariableCount // 自動調整Y軸刻度
                }
            };
            histogramChart.ChartAreas.Add(chartArea);

            // 添加數據系列
            Series series = new Series {
                Name = "Brightness",
                ChartType = SeriesChartType.Column,
                Color = Color.Gray
            };

            // 將直方圖數據添加到系列中
            for (int i = 0; i < histogram.Length; i++) {
                series.Points.AddXY(i, histogram[i]);
            }

            histogramChart.Series.Add(series);

            // 將 Chart 控件添加到表單中
            Form histogramForm = new Form {
                Text = "Brightness Histogram",
                Width = 800,
                Height = 600
            };
            histogramForm.Controls.Add(histogramChart);
            histogramForm.Show();
        }
        // 將頻譜圖的低頻部分移動到中心
        private void ShiftDFT(OpenCvSharpMat magImage) {
            int cx = magImage.Cols / 2;
            int cy = magImage.Rows / 2;

            OpenCvSharpMat q0 = new OpenCvSharpMat(magImage, new Rect(0, 0, cx, cy));   // Top-Left
            OpenCvSharpMat q1 = new OpenCvSharpMat(magImage, new Rect(cx, 0, cx, cy));  // Top-Right
            OpenCvSharpMat q2 = new OpenCvSharpMat(magImage, new Rect(0, cy, cx, cy));  // Bottom-Left
            OpenCvSharpMat q3 = new OpenCvSharpMat(magImage, new Rect(cx, cy, cx, cy)); // Bottom-Right
            Console.WriteLine(q0.Type().ToString());
            OpenCvSharpMat tmp = new OpenCvSharpMat();
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
        private void ShowImageWithCustomSize(string windowName, OpenCvSharpMat image, int width, int height) {
            // 創建一個可調整大小的視窗
            Cv2.NamedWindow(windowName, OpenCvSharp.WindowFlags.Normal);

            // 設置視窗大小
            Cv2.ResizeWindow(windowName, width, height);

            // 顯示圖像
            Cv2.ImShow(windowName, image);
        }
        private Complex[,] FFT2D(double[,] input) {
            int rows = input.GetLength(0);
            int cols = input.GetLength(1);

            // 將輸入轉換為複數矩陣
            Complex[,] complexInput = new Complex[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    complexInput[i, j] = new Complex(input[i, j], 0);

            // 對每一行進行 FFT
            for (int i = 0; i < rows; i++) {
                Complex[] row = new Complex[cols];
                for (int j = 0; j < cols; j++)
                    row[j] = complexInput[i, j];

                Complex[] fftRow = FFTProcessor.FFT(row);
                for (int j = 0; j < cols; j++)
                    complexInput[i, j] = fftRow[j];
            }

            // 對每一列進行 FFT
            for (int j = 0; j < cols; j++) {
                Complex[] col = new Complex[rows];
                for (int i = 0; i < rows; i++)
                    col[i] = complexInput[i, j];

                Complex[] fftCol = FFTProcessor.FFT(col);
                for (int i = 0; i < rows; i++)
                    complexInput[i, j] = fftCol[i];
            }

            return complexInput;
        }
       
        public static Complex[] IFFT(Complex[] input) {
            int originalLength = input.Length;

            // 如果長度不是 2 的次方，填充到最近的 2 的次方
            int paddedLength = (int)Math.Pow(2, Math.Ceiling(Math.Log(originalLength, 2)));
            Complex[] paddedInput = new Complex[paddedLength];

            // 填充原始數據
            for (int i = 0; i < originalLength; i++)
                paddedInput[i] = input[i];

            // 計算 IFFT
            int n = paddedInput.Length;
            Complex[] conjugated = new Complex[n];
            for (int i = 0; i < n; i++)
                conjugated[i] = Complex.Conjugate(paddedInput[i]);

            Complex[] fftResult = FFTProcessor.FFT(conjugated);

            Complex[] result = new Complex[n];
            for (int i = 0; i < n; i++)
                result[i] = Complex.Conjugate(fftResult[i]) / n;

            // 裁剪回原始長度
            Complex[] croppedResult = new Complex[originalLength];
            for (int i = 0; i < originalLength; i++)
                croppedResult[i] = result[i];

            return croppedResult;
        }

        private Complex[,] IFFT2D(Complex[,] input) {
            int originalRows = input.GetLength(0);
            int originalCols = input.GetLength(1);

            // 計算最近的 2 的次方
            int paddedRows = (int)Math.Pow(2, Math.Ceiling(Math.Log(originalRows, 2)));
            int paddedCols = (int)Math.Pow(2, Math.Ceiling(Math.Log(originalCols, 2)));

            // 填充到最近的 2 的次方
            Complex[,] paddedInput = new Complex[paddedRows, paddedCols];
            for (int i = 0; i < originalRows; i++)
                for (int j = 0; j < originalCols; j++)
                    paddedInput[i, j] = input[i, j];

            // 對每一行進行 IFFT
            for (int i = 0; i < paddedRows; i++) {
                Complex[] row = new Complex[paddedCols];
                for (int j = 0; j < paddedCols; j++)
                    row[j] = paddedInput[i, j];

                Complex[] ifftRow = IFFT(row);
                for (int j = 0; j < paddedCols; j++)
                    paddedInput[i, j] = ifftRow[j];
            }

            // 對每一列進行 IFFT
            for (int j = 0; j < paddedCols; j++) {
                Complex[] col = new Complex[paddedRows];
                for (int i = 0; i < paddedRows; i++)
                    col[i] = paddedInput[i, j];

                Complex[] ifftCol = IFFT(col);
                for (int i = 0; i < paddedRows; i++)
                    paddedInput[i, j] = ifftCol[i];
            }

            // 裁剪回原始大小
            Complex[,] croppedResult = new Complex[originalRows, originalCols];
            for (int i = 0; i < originalRows; i++)
                for (int j = 0; j < originalCols; j++)
                    croppedResult[i, j] = paddedInput[i, j];


            return croppedResult;
        }
        public int ow, oh;
        private void 放棄toolStripMenuItem_Click(object sender, EventArgs e) {
            try {

                //// Step 1: 确保 canvas 是灰阶图像
                if (canvas.Channels() != 1)
                    Cv2.CvtColor(canvas, canvas, ColorConversionCodes.BGR2GRAY);
                ow = canvas.Width;
                oh = canvas.Height;
                OpenCvSharpMat paddedImage = PadToPowerOfTwo(canvas);
                Mat complexImage = new Mat(paddedImage.Size(), MatType.CV_32FC2);
                Mat floatImage = new Mat();
                paddedImage.ConvertTo(floatImage, MatType.CV_32FC1); // ✅ 正確使用 ConvertTo()

                // Step 4: 建立兩個通道
                Mat[] planes = { floatImage, Mat.Zeros(paddedImage.Size(), MatType.CV_32FC1) };
                Cv2.Merge(planes, complexImage); // 合併為 CV_32FC2
                // Step 5: 計算 DFT
                Cv2.Dft(complexImage, complexImage, DftFlags.ComplexOutput);
                ShiftDFT(complexImage);

                // Step 6: 更新 canvas
                canvas = complexImage;
                AdjustmentCanvas();
                paddedImage.Dispose();
                paddedImage = null;
                floatImage.Dispose();
                floatImage = null;
                planes[0].Dispose();
                planes[0] = null;
                planes[1].Dispose();
                planes[1] = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return;
            }
            catch (Exception ex) {
                MessageBox.Show($"還原過程出錯：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 填充影像到 2 的幂次方大小
        private OpenCvSharpMat PadToPowerOfTwo(OpenCvSharpMat image) {
            int paddedRows = (int)Math.Pow(2, Math.Ceiling(Math.Log(image.Rows, 2)));
            int paddedCols = (int)Math.Pow(2, Math.Ceiling(Math.Log(image.Cols, 2)));

            OpenCvSharpMat paddedImage = new OpenCvSharpMat();
            Cv2.CopyMakeBorder(image, paddedImage, 0, paddedRows - image.Rows, 0, paddedCols - image.Cols, BorderTypes.Constant, Scalar.All(0));
            return paddedImage;
        }



        // 裁剪影像到原始大小
        private OpenCvSharpMat CropToOriginalSize(OpenCvSharpMat image, int originalRows, int originalCols) {
            return new OpenCvSharpMat(image, new Rect(0, 0, originalCols, originalRows));
        }

        // 执行 2D FFT
        private Complex[,] Compute2DFFT(OpenCvSharpMat image) {
            int rows = image.Rows;
            int cols = image.Cols;

            // 转换为双精度数组
            double[,] input = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    input[i, j] = image.At<byte>(i, j);

            // 计算 2D FFT
            return FFT2D(input);
        }


        // 逆傅立叶变换并重建图像
        private OpenCvSharpMat ReconstructImageFromFFT2(Complex[,] ifftResult, int originalRows, int originalCols) {
            int rows = ifftResult.GetLength(0);
            int cols = ifftResult.GetLength(1);

            OpenCvSharpMat reconstructedImage = new OpenCvSharpMat(rows, cols, MatType.CV_64F);

            // 提取实部并映射到 [0, 255]
            double min = double.MaxValue, max = double.MinValue;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++) {
                    double value = ifftResult[i, j].Real;
                    min = Math.Min(min, value);
                    max = Math.Max(max, value);
                }

            // 防止全黑问题
            if (Math.Abs(max - min) < 1e-6) { min = 0; max = 1; }

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++) {
                    double value = ifftResult[i, j].Real;
                    double pixelValue = (value - min) / (max - min) * 255;
                    reconstructedImage.Set(i, j, pixelValue);
                }

            // 裁剪回原始大小
            reconstructedImage = CropToOriginalSize(reconstructedImage, originalRows, originalCols);
            reconstructedImage.ConvertTo(reconstructedImage, MatType.CV_8U);

            return reconstructedImage;
        }
        private void ShowImageWithProportionalScaling(string windowName, OpenCvSharpMat image, int maxWidth, int maxHeight) {
            // 获取图像的原始尺寸
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // 按比例计算窗口尺寸
            double aspectRatio = (double)originalWidth / originalHeight;
            int windowWidth = maxWidth;
            int windowHeight = maxHeight;

            if (aspectRatio > 1) {
                // 宽度限制
                windowHeight = (int)(maxWidth / aspectRatio);
            }
            else {
                // 高度限制
                windowWidth = (int)(maxHeight * aspectRatio);
            }

            // 创建可调整大小的窗口并设置尺寸
            Cv2.NamedWindow(windowName, OpenCvSharp.WindowFlags.Normal);
            Cv2.ResizeWindow(windowName, windowWidth, windowHeight);

            // 显示图像
            Cv2.ImShow(windowName, image);
        }
        private OpenCvSharpMat ComplexArrayToMat(Complex[,] complexArray) {
            int rows = complexArray.GetLength(0);
            int cols = complexArray.GetLength(1);

            // 创建两通道 Mat，分别存储实部和虚部
            OpenCvSharpMat mat = new OpenCvSharpMat(rows, cols, MatType.CV_32FC2);

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    Vec2f value = new Vec2f((float)complexArray[i, j].Real, (float)complexArray[i, j].Imaginary);
                    mat.Set(i, j, value);
                }
            }

            return mat;
        }
        private Complex[,] MatToComplexArray(OpenCvSharpMat mat) {
            int rows = mat.Rows;
            int cols = mat.Cols;
            Complex[,] complexArray = new Complex[rows, cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    Vec2f value = mat.At<Vec2f>(i, j);
                    complexArray[i, j] = new Complex(value.Item0, value.Item1); // 实部和虚部
                }
            }

            return complexArray;
        }

        private void colortransformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string menuText = ((ToolStripMenuItem)sender).Text;
            Console.WriteLine(menuText);
            if(menuText == "BGR to Grayscale")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.BGR2GRAY);
                canvas = tempMat;
            }
            else if(menuText == "Grayscale to BGR")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.GRAY2BGR);
                canvas = tempMat;
            }
            else if(menuText == "BGR to HSV")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.BGR2HSV);
                canvas = tempMat;
            }
            else if(menuText == "HSV to BGR")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.HSV2BGR);
                canvas = tempMat;
            }
            else if(menuText == "BGR to Lab")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.BGR2Lab);
                canvas = tempMat;
            }
            else if(menuText == "Lab to BGR")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.Lab2BGR);
                canvas = tempMat;
            }
            else if(menuText == "BGR to YUV")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.BGR2YUV);
                canvas = tempMat;
            }
            else if(menuText == "YUV to BGR")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.YUV2BGR);
                canvas = tempMat;
            }
            else if(menuText == "BGR to RGB")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.BGR2RGB);
                canvas = tempMat;
            }
            else if(menuText == "RGB to BGR")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.RGB2BGR);
                canvas = tempMat;
            }
            else if(menuText == "BGR to RGBA")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.BGR2RGBA);
                canvas = tempMat;
            }
            else if(menuText == "RGBA to BGR")
            {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.RGBA2BGR);
                canvas = tempMat;
            }
            else if(menuText == "BGR to XYZ") {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.BGR2XYZ);
                canvas = tempMat;
            }
            else if (menuText == "XYZ to BGR") {
                Mat tempMat = new Mat();
                Cv2.CvtColor(canvas, tempMat, ColorConversionCodes.XYZ2BGR);
                canvas = tempMat;
            }
            AdjustmentCanvas();
            //UpdateCanvas();
        }

        private void 型態click(object sender, EventArgs e) {
            morphology morph = new morphology(this);
            if (morph.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            morph.Dispose();
        }




        private void 二值化click(object sender, EventArgs e) {
            if (!isGrey) {
                MessageBox.Show("The picture must be grey.");
                return;
            }
            binarization binary = new binarization(this);
            if(binary.ShowDialog() == DialogResult.OK) 
                AdjustmentCanvas();
            binary.Dispose();
        }

        private void draw_contour_click(object sender, EventArgs e) {
            if (!isGrey) {
                MessageBox.Show("The picture must be grey.");
                return;
            }
            contour con = new contour(this);
            if(con.ShowDialog() == DialogResult.OK) {
                AdjustmentCanvas();
            }
            con.Dispose();
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RGBtrans blurform = new RGBtrans(this);
            if (blurform.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
        }

        private void findContoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindContour findContour = new FindContour(this);
            if(canvas.Channels() != 1) {
                MessageBox.Show("The image must be grey");
                return;
            }
            if(findContour.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            findContour.Dispose();
        }

        private void lUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LUT lut = new LUT(this);
            if (lut.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            lut.Dispose();
        }
        private void normalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Normalize normalize = new Normalize(this);
            if (normalize.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            normalize.Dispose();
        }
        private void equalizeHistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            equalizeHist equalizehist = new equalizeHist(this);
            if (equalizehist.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            equalizehist.Dispose();
        }

        private void cLAHEToolStripMenuItem_Click(object sender, EventArgs e) {
            CLAHE cLAHE = new CLAHE(this);
            if (cLAHE.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            cLAHE.Dispose();
        }

        private void change_thickness(object sender, EventArgs e) {
            penThickness = thicknessBar.Value;
            thicknessLabel.Text = $"Pen thickness: {penThickness}";
        }

        private void negative_image(object sender, EventArgs e) {
            Cv2.BitwiseNot(canvas, canvas);
            AdjustmentCanvas();
        }

        private void find_defect(object sender, EventArgs e) {
            Defect defect = new Defect(this);
            if (defect.ShowDialog() == DialogResult.OK)
                AdjustmentCanvas();
            defect.Dispose();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 邊緣ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 轉 HSV 色彩空間
            Mat hsv = new Mat();
            Cv2.CvtColor(canvas, hsv, ColorConversionCodes.BGR2HSV);

            // 定義橘色 HSV 範圍（你可以根據實際調整）
            Scalar lowerOrange = new Scalar(5, 100, 100);   // H, S, V
            Scalar upperOrange = new Scalar(25, 255, 255);

            // 建立遮罩
            Mat mask = new Mat();
            Cv2.InRange(hsv, lowerOrange, upperOrange, mask);

            // 套用遮罩保留橘色區域
            Mat result = new Mat();
            Cv2.BitwiseAnd(canvas, canvas, result, mask);
            canvas.Dispose();
            canvas = result;
            AdjustmentCanvas();
        }

        private void iFFTToolStripMenuItem_Click(object sender, EventArgs e) {
            ShiftDFT(canvas);
            
            Mat inverseTransform = new Mat();
            Cv2.Idft(canvas, inverseTransform, DftFlags.Scale | DftFlags.RealOutput);

            // Step 3: 取得實部（因為 IFFT 會輸出 CV_32FC2）
            Mat[] planes = new Mat[2];
            Cv2.Split(inverseTransform, out planes);
            Mat restoredImage = planes[0]; // 取實部

            Rect roi = new Rect(0, 0, ow, oh);
            Mat cropped = new Mat(restoredImage, roi);

            // Step 4: 轉換回 8-bit 影像（可顯示）
            Mat displayImage = new Mat();
            Cv2.Normalize(cropped, displayImage, 0, 255, NormTypes.MinMax);
            displayImage.ConvertTo(displayImage, MatType.CV_8UC1);

            // 使用比例缩放顯示還原image
            ShowImageWithProportionalScaling("Restored Image", displayImage, 800, 800);

            // 更新 canvas
            canvas = displayImage.Clone();
            planes[0].Dispose();
            inverseTransform.Dispose();
            restoredImage.Dispose();
            displayImage.Dispose();
            AdjustmentCanvas();
        }

        public Bitmap ConvertCV32FC2ToBitmap(OpenCvSharp.Mat image,bool store8U) {
            if (image.Type() != MatType.CV_32FC2)
                throw new ArgumentException("Input Mat must be of type CV_32FC2.");

            // Step 1: Split into real and imaginary parts
            OpenCvSharpMat[] channels = Cv2.Split(image);

            // Step 2: Compute magnitude
            OpenCvSharpMat magnitude = new OpenCvSharpMat();
            Cv2.Magnitude(channels[0], channels[1], magnitude);

            // Step 3: Apply logarithmic scaling
            OpenCvSharpMat logMagnitude = new OpenCvSharpMat();
            Cv2.Log(magnitude + 1, logMagnitude); // Avoid log(0)

            // Step 4: Normalize to [0, 255]
            OpenCvSharpMat normalizedMagnitude = new OpenCvSharpMat();
            Cv2.Normalize(logMagnitude, normalizedMagnitude, 0, 255, NormTypes.MinMax);
            normalizedMagnitude.ConvertTo(normalizedMagnitude, MatType.CV_8U);
            
            // Step 5: Convert to Bitmap
            Bitmap bitmap = BitmapConverter.ToBitmap(normalizedMagnitude);

            // Release temporary Mats
            
            channels[0].Dispose();
            channels[1].Dispose();
            channels[0] = null;
            channels[1] = null;
            magnitude.Dispose();
            magnitude = null;
            logMagnitude.Dispose();
            logMagnitude = null;
            normalizedMagnitude.Dispose();
            normalizedMagnitude = null;
            return bitmap;
        }

        public Mat ConvertFFTToMaskable(Mat fftImage) {
            Mat[] planes = new Mat[2];
            Cv2.Split(fftImage, out planes);

            Mat magnitude = new Mat();
            Cv2.Magnitude(planes[0], planes[1], magnitude);

            Cv2.Log(magnitude + 1, magnitude);

            Cv2.Normalize(magnitude, magnitude, 0, 255, NormTypes.MinMax);
            magnitude.ConvertTo(magnitude, MatType.CV_8UC1);

            planes[0].Dispose();
            planes[0] = null;
            planes[1].Dispose();
            planes[1] = null;
            return magnitude;
        }
    }

}
