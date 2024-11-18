using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSize = OpenCvSharp.Size;
using Pen;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.ConstrainedExecution;
using OpenCvSharp.Dnn;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using EmguCVMat = Emgu.CV.Mat;
using OpenCvSharpMat = OpenCvSharp.Mat;
using Paint;
using System.Threading.Tasks;
using System.Numerics;

namespace Paint {
    
    public partial class Paint : Form 
    {
        public OpenCvSharpMat canvas;
        public OpenCvSharpMat tempCanvas = new OpenCvSharpMat(); 
        private OpenCvSharpMat chart = new OpenCvSharpMat();
        private OpenCvSharp.Point startpoint;
        private OpenCvSharp.Point prevPoint;
        private OpenCvSharp.Point currentPoint; 

        private Bitmap canvasBitmap;
        private bool isDrawing = false,isGray = false; 
        private string drawMode = "Free"; 
        private Rectangle showAspect;
        private Scalar currentColor = new Scalar(0, 0, 0);
        private int penThickness = 2;

        private Stack<PenMotion> action = new Stack<PenMotion>();
        private Stack<PenMotion> reaction = new Stack<PenMotion>();

        private OpenCvSize init_kernal = new OpenCvSize(1, 1);
        private double init_sigma = 1.0;
        public Paint() 
        {
            InitializeComponent();

            this.KeyPreview = true; // 允許表單偵測按鍵
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            PenMotion penMotion = new PenMotion(this);
        }

        private void Form1_Load(object sender, EventArgs e) {

        }
        private void Form1_MouseWheel(object sender, MouseEventArgs e) {
            if (Control.ModifierKeys == Keys.Control) {
                if (e.Delta > 0) //up
                    Enlarge_click(sender, e);
                else if (e.Delta < 0) //down
                    Shrink_click(sender, e);
            }
        }
        private void Form1_KeyDown(object sender,KeyEventArgs e) {
            if(e.Control && e.KeyCode == Keys.Z) 
                復原UndoToolStripMenuItem_Click(sender,e);
            else if(e.Control && e.KeyCode == Keys.Y) 
                重做RedoToolStripMenuItem_Click(sender, e);
            else if(e.Control && e.KeyCode == Keys.S) 
                儲存檔案ToolStripMenuItem_Click(sender,e);
        }
        private void Enlarge_click(object sender, EventArgs e) {
            pictureBox1.Width = Convert.ToInt32(pictureBox1.Width*1.1);
            pictureBox1.Height = Convert.ToInt32(pictureBox1.Height * 1.1);
            showAspect = GetImageRectangleInPictureBox();
        }

        private void Shrink_click(object sender, EventArgs e) {
            pictureBox1.Width = Convert.ToInt32(pictureBox1.Width / 1.1);
            pictureBox1.Height = Convert.ToInt32(pictureBox1.Height / 1.1);
            showAspect = GetImageRectangleInPictureBox();
        }

        private int CalculateDistance(OpenCvSharp.Point P1, OpenCvSharp.Point P2) {
            return (int)Math.Sqrt(Math.Pow(P2.X-P1.X,2) + Math.Pow(P2.Y - P1.Y, 2));
        }
        private void New_canva_click(object sender, EventArgs e) {
            int width = 1280, height = 720;
            canvas = new OpenCvSharpMat(new OpenCvSize(width, height), MatType.CV_8UC3, Scalar.All(255));
            BitmapConverter.ToBitmap(canvas);
            pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
            pictureBox1.Width = width;
            pictureBox1.Height = height;
            tempCanvas = canvas.Clone();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                isDrawing = true;
                startpoint = ConvertToImageCoordinates(e.Location);
                prevPoint = ConvertToImageCoordinates(e.Location);
            }
            else if(e.Button == MouseButtons.Right) {
                isDrawing = true;
                startpoint.X = System.Windows.Forms.Cursor.Position.X;
                startpoint.Y = System.Windows.Forms.Cursor.Position.Y;
            }
        }
        //detecting mouse moving
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (isDrawing && e.Button == MouseButtons.Left) {
                if (drawMode == "Free") { //除了自由繪製其他皆要預覽
                    currentPoint = ConvertToImageCoordinates(e.Location);
                    DrawFinalShape();
                    prevPoint = currentPoint;
                    UpdateCanvas();
                }
                else {
                    currentPoint = ConvertToImageCoordinates(e.Location);
                    DrawPreviewShape();
                    ShowTempCanvas();
                    //tempCanvas.Dispose();
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
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            if (isDrawing && e.Button == MouseButtons.Left) {
                isDrawing = false;
                currentPoint = ConvertToImageCoordinates(e.Location);
                DrawFinalShape(); // 繪製圖形
                UpdateCanvas(); // 更新顯示
            }
            else if (isDrawing && e.Button == MouseButtons.Right )
                isDrawing = false;
        }
        private void ShowTempCanvas() {
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();

            pictureBox1.Image = BitmapConverter.ToBitmap(tempCanvas);
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
                vertex.X = startpoint.X + (currentPoint.X - startpoint.X)/2;//等腰三角形
                vertex.Y = currentPoint.Y;
                vertex2.X = currentPoint.X;
                vertex2.Y = startpoint.Y;
                OpenCvSharp.Point[] TrianglePoint = { startpoint, vertex2, vertex };
                Cv2.Polylines(tempCanvas, new[] {TrianglePoint}, true, currentColor, penThickness);
            }
            else {
                Cv2.Line(tempCanvas,prevPoint,currentPoint, currentColor, penThickness);
                prevPoint = currentPoint;
            }
        }
        //final
        private void DrawFinalShape() {
            PenMotion tempAct;
            switch (drawMode) {
                case "Line":
                    Cv2.Line(canvas, prevPoint, currentPoint, currentColor, penThickness);
                    tempAct = new PenMotion("Line", prevPoint, currentPoint, currentColor, penThickness, 0,new OpenCvSize(0,0),null,canvas);
                    break;
                case "Rectangle":
                    Cv2.Rectangle(canvas, prevPoint, currentPoint, currentColor, penThickness); // 最終繪製黑色矩形
                    tempAct = new PenMotion("Rectangle", prevPoint, currentPoint, currentColor, penThickness, 0, new OpenCvSize(0, 0), null,canvas);
                    break;
                case "Circle":
                    Cv2.Circle(canvas, startpoint, CalculateDistance(startpoint, currentPoint), currentColor, penThickness);
                    tempAct = new PenMotion("Circle", startpoint, currentPoint, currentColor, penThickness, CalculateDistance(startpoint, currentPoint), new OpenCvSize(0, 0), null,canvas);
                    break;
                case "Ellipse":
                    OpenCvSize size = new OpenCvSize(Math.Abs(currentPoint.X - startpoint.X), Math.Abs(currentPoint.Y - startpoint.Y));
                    Cv2.Ellipse(canvas, startpoint, size, 0, 0, 360, currentColor, penThickness);
                    tempAct = new PenMotion("Ellipse", startpoint, currentPoint, currentColor, penThickness, CalculateDistance(startpoint, currentPoint), size, null,canvas);
                    break;
                case "Triangle":
                    OpenCvSharp.Point vertex;
                    OpenCvSharp.Point vertex2;
                    vertex.X = startpoint.X + (currentPoint.X - startpoint.X) / 2;//等腰三角形
                    vertex.Y = currentPoint.Y;
                    vertex2.X = currentPoint.X;
                    vertex2.Y = startpoint.Y;
                    OpenCvSharp.Point[] TrianglePoint = { startpoint, vertex2, vertex };
                    Cv2.Polylines(canvas, new[] { TrianglePoint }, true, currentColor, penThickness);
                    tempAct = new PenMotion("Triangle", startpoint, currentPoint, currentColor, penThickness,0, new OpenCvSize(0,0), TrianglePoint,canvas);
                    break;
                default:
                    Cv2.Line(canvas, prevPoint, currentPoint, currentColor, penThickness);
                    tempAct = new PenMotion("Free", prevPoint, currentPoint, currentColor, penThickness, 0, new OpenCvSize(0, 0), null, canvas);
                    break; 
            }
            action.Push(tempAct);
            //清空下一步
            reaction.Clear();
        }
        private void UpdateCanvas() {
            if (pictureBox1.Image != null) {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
        }
        private Rectangle GetImageRectangleInPictureBox() {
            // 計算圖像縮放後在 PictureBox 中的實際顯示範圍
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
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            ToolStripMenuItem temp = (ToolStripMenuItem)sender;
            Console.WriteLine(temp.Text);
            drawMode = TypeToMode[temp.Text];
        }

        private void 儲存檔案ToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
            saveFileDialog.Title = "Save an Image File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                string filePath = saveFileDialog.FileName;//System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                Cv2.ImWrite(filePath, canvas);
            }
            if(saveFileDialog != null) 
                saveFileDialog.Dispose();
        }
        private void 開啟ToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
            openFileDialog.Title = "打開圖片";
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                canvas = Cv2.ImRead(openFileDialog.FileName);
                pictureBox1.Load(openFileDialog.FileName);
                tempCanvas = canvas.Clone();
            }
        }
        private void 結束ToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void 復原UndoToolStripMenuItem_Click(object sender, EventArgs e) {
            if (action.Count > 0) {
                reaction.Push(action.Pop());
                Redraw();
                UpdateCanvas();
            }
        }
        private void 重做RedoToolStripMenuItem_Click(object sender, EventArgs e) {
            if(reaction.Count > 0) {
                action.Push(reaction.Pop());
                Redraw();
                UpdateCanvas(); // 更新顯示
            }
        }
        private void Redraw() {
            foreach (PenMotion act in action) {
                switch (act.Type) {
                    case "Line":
                        Cv2.Line(canvas, act.Start, act.End, act.Pencolor, act.Thickness);
                        break;
                    case "Rectangle":
                        Cv2.Rectangle(canvas, act.Start, act.End, act.Pencolor, act.Thickness);
                        break;
                    case "Circle":
                        Cv2.Circle(canvas, act.Start, CalculateDistance(act.Start, act.End), act.Pencolor, act.Thickness);
                        break;
                    case "Ellipse":
                        Cv2.Ellipse(canvas, act.Start, act.Size, 0, 0, 360, act.Pencolor, act.Thickness);
                        break;
                    case "Triangle":
                        Cv2.Polylines(canvas,new[] {act.Vertexes},true,act.Pencolor, act.Thickness);
                        break;
                    default:
                        Cv2.Line(canvas, act.Start, act.End, act.Pencolor, act.Thickness);
                        break;
                }
            }
        }


        private void 轉換成灰階ToolStripMenuItem_Click(object sender, EventArgs e) {
            if(canvas.Channels()!=1)
                Cv2.CvtColor(canvas,canvas,ColorConversionCodes.BGR2GRAY);
            isGray = true;
            UpdateCanvas();
        }

        private void 高斯模糊ToolStripMenuItem_Click(object sender, EventArgs e) {
            Gauss blurform = new Gauss(init_kernal,init_sigma,this);
            if (blurform.ShowDialog() == DialogResult.OK) 
                UpdateCanvas();
            blurform.Dispose();
        }
        private void 亮度對比度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lightness trackbarForm = new lightness(this);
            if (trackbarForm.ShowDialog() == DialogResult.OK)
            {
                double alpha = trackbarForm.trackBar1.Value/100.0; // 對比度
                int beta = (int)trackbarForm.trackBar2.Value; // 亮度
                bool isNegative = (alpha < 0); // 檢查是否需要負片效果

                // 調整對比度和亮度
                if (!isNegative)
                    canvas = AdjustContrastAndBrightness(canvas, alpha, beta);
                else
                    canvas = AdjustNegativeContrast(canvas, alpha, beta);
                Console.WriteLine($"Alpha: {alpha}, Beta: {beta}, IsNegative: {isNegative}");
            }
            UpdateCanvas();
        }
        private OpenCvSharpMat AdjustNegativeContrast(OpenCvSharpMat image, double alpha, int beta)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int c = 0; c < 3; c++)
                    {
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

        private OpenCvSharpMat AdjustContrastAndBrightness(OpenCvSharpMat image, double alpha, int beta)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int c = 0; c < 3; c++)
                    {
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
        private void 伽瑪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gamma trackbarForm = new Gamma(this);
            if (trackbarForm.ShowDialog() == DialogResult.OK)
            {
                double gamma = trackbarForm.trackBar1.Value/100.0; 

                canvas = 伽瑪轉換(canvas, gamma);

            }
            UpdateCanvas();
        }
        private OpenCvSharpMat 伽瑪轉換(OpenCvSharpMat image, double gamma)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());


            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int c = 0; c < 3; c++)
                    {
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
                UpdateCanvas();
            LP_filter.Dispose();
        }

        private void 高通濾波ToolStripMenuItem_Click(object sender, EventArgs e) {
            High_pass HP_filter = new High_pass(this);
            if(HP_filter.ShowDialog() == DialogResult.OK)
                UpdateCanvas();
            
            HP_filter.Dispose();
        }
        private void Pallate_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Color selectedColor = colorDialog.Color;
                currentColor = new Scalar(selectedColor.B, selectedColor.G, selectedColor.R); // BGR 格式
            }
        }

        private void log變換ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log trackbarForm = new log(this);
            if (trackbarForm.ShowDialog() == DialogResult.OK)
            {
                double c = 255.0 / Math.Log(1 + trackbarForm.trackBar1.Value) ;
                canvas = Log變換(canvas, c);

            }
            UpdateCanvas();
        }

        private void 反logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            反log trackbarForm = new 反log(this);
            if (trackbarForm.ShowDialog() == DialogResult.OK)
            {
                double c = trackbarForm.trackBar1.Value/10.0;
                canvas = 反Log變換(canvas, c);

            }
            UpdateCanvas();
        }

        private OpenCvSharpMat Log變換(OpenCvSharpMat image,double c)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int cIdx = 0; cIdx < 3; cIdx++)
                    {
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
        private OpenCvSharpMat 反Log變換(OpenCvSharpMat image, double c)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(image.Size(), image.Type());

            // 進行反對數變換
            for (int y = 0; y < image.Rows; y++)
            {
                for (int x = 0; x < image.Cols; x++)
                {
                    Vec3b color = image.At<Vec3b>(y, x);
                    Vec3b newColor = new Vec3b();

                    for (int cIdx = 0; cIdx < 3; cIdx++)
                    {
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
        private void btnShowHistogram_Click(object sender, EventArgs e)
        {
            // 計算亮度直方圖
            int[] histogram = CalculateBrightnessHistogram(canvas);

            // 繪製直方圖
            DrawBrightnessHistogram(histogram);
        }
        private int[] CalculateBrightnessHistogram(OpenCvSharpMat image)
        {
            // 將圖像轉換為灰度圖像
            OpenCvSharpMat grayImage = new OpenCvSharpMat();
            if (!isGray)
            {
                Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayImage = image;
            }

            // 初始化直方圖數組
            int[] histogram = new int[256];

            // 遍歷灰度圖像，計算每個亮度級別的像素數量
            for (int y = 0; y < grayImage.Rows; y++)
            {
                for (int x = 0; x < grayImage.Cols; x++)
                {
                    byte pixelValue = grayImage.At<byte>(y, x);
                    histogram[pixelValue]++;
                }
            }

            return histogram;
        }
        private void DrawBrightnessHistogram(int[] histogram)
        {
            // 初始化 Chart 控件
            Chart histogramChart = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            // 設置 ChartArea
            ChartArea chartArea = new ChartArea
            {
                Name = "ChartArea",
                AxisX =
        {
            Title = "Brightness",
            Minimum = 0, // 調整X軸起點，留出空間
            Maximum = 255,
            Interval = 50 // 設置X軸標籤的間隔
        },
                AxisY =
        {
            Title = "Frequency",
            Minimum = 0,
            IntervalAutoMode = IntervalAutoMode.VariableCount // 自動調整Y軸刻度
        }
            };
            histogramChart.ChartAreas.Add(chartArea);

            // 添加數據系列
            Series series = new Series
            {
                Name = "Brightness",
                ChartType = SeriesChartType.Column,
                Color = Color.Gray
            };

            // 將直方圖數據添加到系列中
            for (int i = 0; i < histogram.Length; i++)
            {
                series.Points.AddXY(i, histogram[i]);
            }

            histogramChart.Series.Add(series);

            // 將 Chart 控件添加到表單中
            Form histogramForm = new Form
            {
                Text = "Brightness Histogram",
                Width = 800,
                Height = 600
            };
            histogramForm.Controls.Add(histogramChart);
            histogramForm.Show();
        }
        private OpenCvSharpMat ManualDFT(OpenCvSharpMat image)
        {
            // 將影像轉換為灰階影像
            OpenCvSharpMat grayImage = new OpenCvSharpMat();
            if (image.Channels() != 1)
                Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            else
                grayImage = image.Clone();

            int width = grayImage.Width;
            int height = grayImage.Height;

            // 初始化頻率域的實部和虛部
            double[,] realPart = new double[height, width];
            double[,] imagPart = new double[height, width];

            // 建立角度查找表
            double[,] cosTableX = new double[height, height];
            double[,] sinTableX = new double[height, height];
            double[,] cosTableY = new double[width, width];
            double[,] sinTableY = new double[width, width];

            // 計算 X 方向的查找表
            for (int x = 0; x < height; x++)
            {
                for (int u = 0; u < height; u++)
                {
                    double normalizedX = (double)x / height;
                    double angleU = -2.0 * Math.PI * normalizedX * u;
                    cosTableX[u, x] = Math.Cos(angleU);
                    sinTableX[u, x] = Math.Sin(angleU);
                }
            }

            // 計算 Y 方向的查找表
            for (int y = 0; y < width; y++)
            {
                for (int v = 0; v < width; v++)
                {
                    double normalizedY = (double)y / width;
                    double angleV = -2.0 * Math.PI * normalizedY * v;
                    cosTableY[v, y] = Math.Cos(angleV);
                    sinTableY[v, y] = Math.Sin(angleV);
                }
            }

            // 手動計算 2D DFT
            Parallel.For(0, height, u =>
            {
                for (int v = 0; v < width; v++)
                {
                    double realSum = 0.0;
                    double imagSum = 0.0;

                    for (int x = 0; x < height; x++)
                    {
                        for (int y = 0; y < width; y++)
                        {
                            double pixel = grayImage.At<byte>(x, y);
                            double cosValue = cosTableX[u, x] * cosTableY[v, y];
                            double sinValue = sinTableX[u, x] * sinTableY[v, y];

                            realSum += pixel * cosValue;
                            imagSum += pixel * sinValue;
                        }
                    }

                    realPart[u, v] = realSum;
                    imagPart[u, v] = imagSum;
                }
            });

            // 計算幅度並對數縮放
            OpenCvSharpMat magnitudeImage = new OpenCvSharpMat(height, width, MatType.CV_64F);
            for (int u = 0; u < height; u++)
            {
                for (int v = 0; v < width; v++)
                {
                    double magnitude = Math.Sqrt(realPart[u, v] * realPart[u, v] + imagPart[u, v] * imagPart[u, v]);
                    magnitudeImage.Set(u, v, Math.Log(1 + magnitude)); // 對數縮放
                }
            }

            // 將低頻部分移動到頻譜圖中心
            ShiftDFT(magnitudeImage);

            // 正規化幅度影像，便於顯示
            Cv2.Normalize(magnitudeImage, magnitudeImage, 0, 255, NormTypes.MinMax);
            magnitudeImage.ConvertTo(magnitudeImage, MatType.CV_8U);

            return magnitudeImage;
        }



        private async void 手動傅立葉變換ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 禁用選單或按鈕，避免重複執行
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null) menuItem.Enabled = false;

            try
            {
                // 使用非同步任務執行傅立葉變換
                canvas = await Task.Run(() => ManualDFT(canvas));
                //ShowImageWithCustomSize("Manual Fourier Transform Spectrum", magnitudeImage, 800, 600);
                UpdateCanvas();
                //Cv2.WaitKey(0); // 保持視窗開啟
            }
            catch (Exception ex)
            {
                MessageBox.Show($"錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 恢復選單或按鈕狀態
                if (menuItem != null) menuItem.Enabled = true;
            }
        }

        // 將頻譜圖的低頻部分移動到中心
        private void ShiftDFT(OpenCvSharpMat magImage)
        {
            int cx = magImage.Cols / 2;
            int cy = magImage.Rows / 2;

            OpenCvSharpMat q0 = new OpenCvSharpMat(magImage, new Rect(0, 0, cx, cy));   // Top-Left
            OpenCvSharpMat q1 = new OpenCvSharpMat(magImage, new Rect(cx, 0, cx, cy));  // Top-Right
            OpenCvSharpMat q2 = new OpenCvSharpMat(magImage, new Rect(0, cy, cx, cy));  // Bottom-Left
            OpenCvSharpMat q3 = new OpenCvSharpMat(magImage, new Rect(cx, cy, cx, cy)); // Bottom-Right

            OpenCvSharpMat tmp = new OpenCvSharpMat();
            q0.CopyTo(tmp);
            q3.CopyTo(q0);
            tmp.CopyTo(q3);

            q1.CopyTo(tmp);
            q2.CopyTo(q1);
            tmp.CopyTo(q2);
        }
        private void ShowImageWithCustomSize(string windowName, OpenCvSharpMat image, int width, int height)
        {
            // 創建一個可調整大小的視窗
            Cv2.NamedWindow(windowName, OpenCvSharp.WindowFlags.Normal);

            // 設置視窗大小
            Cv2.ResizeWindow(windowName, width, height);

            // 顯示圖像
            Cv2.ImShow(windowName, image);
        }
        private OpenCvSharpMat FFTDFT(OpenCvSharpMat image)
        {
            // 將影像轉換為灰階影像
            OpenCvSharpMat grayImage = new OpenCvSharpMat();
            if (image.Channels() != 1)
                Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            else
                grayImage = image.Clone();

            // 擴展影像大小以適配 DFT 的計算（最佳大小）
            int optimalRows = Cv2.GetOptimalDFTSize(grayImage.Rows);
            int optimalCols = Cv2.GetOptimalDFTSize(grayImage.Cols);
            Cv2.CopyMakeBorder(grayImage, grayImage, 0, optimalRows - grayImage.Rows, 0, optimalCols - grayImage.Cols, BorderTypes.Constant, Scalar.All(0));

            // 建立複數影像平面（實部 + 虛部）
            OpenCvSharpMat[] planes = { new OpenCvSharpMat(grayImage.Size(), MatType.CV_32F), new OpenCvSharpMat(grayImage.Size(), MatType.CV_32F) };
            grayImage.ConvertTo(planes[0], MatType.CV_32F); // 實部為影像本身
            planes[1].SetTo(Scalar.All(0)); // 虛部為零
            OpenCvSharpMat complexImage = new OpenCvSharpMat();
            Cv2.Merge(planes, complexImage);

            // 進行 DFT 轉換
            Cv2.Dft(complexImage, complexImage, DftFlags.ComplexOutput);

            // 分離頻譜的實部和虛部
            Cv2.Split(complexImage, out planes);

            // 計算幅度：sqrt(re^2 + im^2)
            OpenCvSharpMat magnitudeImage = new OpenCvSharpMat();
            Cv2.Magnitude(planes[0], planes[1], magnitudeImage);

            // 對數縮放處理（Log Scale）
            magnitudeImage += Scalar.All(1); // 避免對數的零點
            Cv2.Log(magnitudeImage, magnitudeImage);

            // 移動頻譜圖中心
            ShiftDFT(magnitudeImage);

            // 將結果正規化到 0-255 範圍以便顯示
            Cv2.Normalize(magnitudeImage, magnitudeImage, 0, 255, NormTypes.MinMax);
            magnitudeImage.ConvertTo(magnitudeImage, MatType.CV_8U);

            return magnitudeImage;
        }
        private void 使用FFT傅立葉變換ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas = FFTDFT(canvas);
            UpdateCanvas();
            //ShowImageWithCustomSize("FFT Fourier Transform Spectrum", canvas, 800, 600);
            //Cv2.WaitKey(0); // 保持視窗開啟
        }
        private Complex[,] FFT2D(double[,] input)
        {
            int rows = input.GetLength(0);
            int cols = input.GetLength(1);

            // 將輸入轉換為複數矩陣
            Complex[,] complexInput = new Complex[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    complexInput[i, j] = new Complex(input[i, j], 0);

            // 對每一行進行 FFT
            for (int i = 0; i < rows; i++)
            {
                Complex[] row = new Complex[cols];
                for (int j = 0; j < cols; j++)
                    row[j] = complexInput[i, j];

                Complex[] fftRow = FFTProcessor.FFT(row);
                for (int j = 0; j < cols; j++)
                    complexInput[i, j] = fftRow[j];
            }

            // 對每一列進行 FFT
            for (int j = 0; j < cols; j++)
            {
                Complex[] col = new Complex[rows];
                for (int i = 0; i < rows; i++)
                    col[i] = complexInput[i, j];

                Complex[] fftCol = FFTProcessor.FFT(col);
                for (int i = 0; i < rows; i++)
                    complexInput[i, j] = fftCol[i];
            }

            return complexInput;
        }
        private OpenCvSharpMat VisualizeFFT(Complex[,] fftResult)
        {
            int rows = fftResult.GetLength(0);
            int cols = fftResult.GetLength(1);

            OpenCvSharpMat magnitudeImage = new OpenCvSharpMat(rows, cols, MatType.CV_64F);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double magnitude = Complex.Abs(fftResult[i, j]);
                    magnitudeImage.Set(i, j, Math.Log(1 + magnitude)); // 對數縮放
                }
            }

            // 移動低頻到中心
            ShiftDFT(magnitudeImage);

            // 正規化到 0-255
            Cv2.Normalize(magnitudeImage, magnitudeImage, 0, 255, NormTypes.MinMax);
            magnitudeImage.ConvertTo(magnitudeImage, MatType.CV_8U);

            return magnitudeImage;
        }
        private OpenCvSharpMat ManualFFT(OpenCvSharpMat image)
        {
            // 轉換為灰階影像
            OpenCvSharpMat grayImage = new OpenCvSharpMat();
            if (image.Channels() != 1)
                Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            else
                grayImage = image.Clone();

            // 轉換為雙精度陣列
            int rows = grayImage.Rows;
            int cols = grayImage.Cols;
            double[,] input = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    input[i, j] = grayImage.At<byte>(i, j);

            // 計算 2D FFT
            Complex[,] fftResult = FFT2D(input);

            // 可視化頻譜圖
            return VisualizeFFT(fftResult);
        }
        private void 自己實現FFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvas = ManualFFT(canvas);
            UpdateCanvas();
            //Cv2.ImShow("Manual FFT Spectrum", magnitudeImage);
            //Cv2.WaitKey(0); // 保持視窗
        }
        public static Complex[] IFFT(Complex[] input)
        {
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

        private Complex[,] IFFT2D(Complex[,] input)
        {
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
            for (int i = 0; i < paddedRows; i++)
            {
                Complex[] row = new Complex[paddedCols];
                for (int j = 0; j < paddedCols; j++)
                    row[j] = paddedInput[i, j];

                Complex[] ifftRow = IFFT(row);
                for (int j = 0; j < paddedCols; j++)
                    paddedInput[i, j] = ifftRow[j];
            }

            // 對每一列進行 IFFT
            for (int j = 0; j < paddedCols; j++)
            {
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

        private OpenCvSharpMat ReconstructImageFromFFT(Complex[,] ifftResult)
        {
            int rows = ifftResult.GetLength(0);
            int cols = ifftResult.GetLength(1);

            OpenCvSharpMat reconstructedImage = new OpenCvSharpMat(rows, cols, MatType.CV_8U);
            double min = double.MaxValue, max = double.MinValue;

            // 提取實部並找到最小值和最大值
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double value = ifftResult[i, j].Real;
                    min = Math.Min(min, value);
                    max = Math.Max(max, value);
                }
            }

            // 防止全黑問題
            if (min == max)
            {
                min = 0;
                max = 1;
            }

            // 映射實部值到 [0, 255]
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double value = ifftResult[i, j].Real;
                    byte pixelValue = (byte)((value - min) / (max - min) * 255);
                    reconstructedImage.Set(i, j, pixelValue);
                }
            }

            return reconstructedImage;
        }


        private OpenCvSharpMat RestoreImageFromFFT(Complex[,] fftResult)
        {
            // 執行逆 FFT
            Complex[,] ifftResult = IFFT2D(fftResult);

            // 還原影像
            return ReconstructImageFromFFT(ifftResult);
        }
        private void FFT還原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: 將 canvas 轉換為複數矩陣
                int rows = canvas.Rows;
                int cols = canvas.Cols;

                Complex[,] fftResult = new Complex[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Vec2f complexValue = canvas.At<Vec2f>(i, j); // 假設 canvas 是複數影像 (兩通道)
                        fftResult[i, j] = new Complex(complexValue.Item0, complexValue.Item1);
                    }
                }

                // Step 2: 執行逆 FFT
                OpenCvSharpMat restoredImage = RestoreImageFromFFT(fftResult);

                // Step 3: 更新 canvas 並顯示還原影像
                canvas = restoredImage;
                UpdateCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"還原過程出現錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FFT還原2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 確保 canvas 是兩通道浮點影像 (CV_32FC2)
                if (canvas.Empty() || canvas.Channels() != 2 || canvas.Type() != MatType.CV_32FC2)
                {
                    OpenCvSharpMat realPart = canvas.Clone();
                    OpenCvSharpMat imaginaryPart = OpenCvSharpMat.Zeros(canvas.Size(), MatType.CV_32F); // 虛部為零

                    // 確保實部和虛部的深度一致
                    if (realPart.Depth() != MatType.CV_32F || imaginaryPart.Depth() != MatType.CV_32F)
                    {
                        realPart.ConvertTo(realPart, MatType.CV_32F);
                        imaginaryPart.ConvertTo(imaginaryPart, MatType.CV_32F);
                    }

                    // 合併成雙通道
                    OpenCvSharpMat mergedCanvas = new OpenCvSharpMat();
                    Cv2.Merge(new[] { realPart, imaginaryPart }, mergedCanvas);
                    canvas = mergedCanvas;
                }

                // Step 1: 執行逆傅立葉變換
                OpenCvSharpMat restoredImage = PerformIFFT(canvas);

                // Step 2: 更新 canvas 並顯示還原影像
                canvas = restoredImage.Clone();
                UpdateCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"還原過程出現錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private OpenCvSharpMat PerformIFFT(OpenCvSharpMat complexImage)
        {
            OpenCvSharpMat inverseTransform = new OpenCvSharpMat();

            // 確保 complexImage 是雙通道浮點矩陣
            if (complexImage.Channels() != 2 || complexImage.Type() != MatType.CV_32FC2)
                throw new ArgumentException("Input image must be a two-channel floating-point matrix (CV_32FC2).");

            // 執行逆傅立葉變換
            Cv2.Dft(complexImage, inverseTransform, DftFlags.Inverse | DftFlags.RealOutput);

            // 檢查數據範圍
            double min, max;
            Cv2.MinMaxLoc(inverseTransform, out min, out max);
            Console.WriteLine($"Inverse Transform Min: {min}, Max: {max}");

            // 如果範圍過小，可能意味著輸入的頻譜不正確
            if (max - min < 1e-6)
                throw new Exception("Invalid frequency spectrum. The result may be too small or invalid.");

            // 正規化到 [0, 255] 範圍
            Cv2.Normalize(inverseTransform, inverseTransform, 0, 255, NormTypes.MinMax);
            inverseTransform.ConvertTo(inverseTransform, MatType.CV_8U);

            return inverseTransform;
        }

        private void 放棄toolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: 确保 canvas 是灰阶图像
                if (canvas.Channels() != 1)
                    Cv2.CvtColor(canvas, canvas, ColorConversionCodes.BGR2GRAY);

                // Step 2: 填充到最近的 2 的幂次方大小
                OpenCvSharpMat paddedImage = PadToPowerOfTwo(canvas);

                // Step 3: 计算 FFT
                Complex[,] fftResult = Compute2DFFT(paddedImage);

                // Step 4: 可视化 FFT 频谱
                OpenCvSharpMat spectrum = VisualizeFFT2(fftResult);
                Cv2.ImShow("FFT Spectrum", spectrum);

                // Step 5: 执行逆 FFT
                Complex[,] ifftResult = IFFT2D(fftResult);

                // Step 6: 重建图像
                OpenCvSharpMat restoredImage = ReconstructImageFromFFT2(ifftResult, canvas.Rows, canvas.Cols);
                Cv2.ImShow("Restored Image", restoredImage);

                // 更新 canvas
                canvas = restoredImage;
                UpdateCanvas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"还原过程出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 填充影像到 2 的幂次方大小
        private OpenCvSharpMat PadToPowerOfTwo(OpenCvSharpMat image)
        {
            int paddedRows = (int)Math.Pow(2, Math.Ceiling(Math.Log(image.Rows, 2)));
            int paddedCols = (int)Math.Pow(2, Math.Ceiling(Math.Log(image.Cols, 2)));

            OpenCvSharpMat paddedImage = new OpenCvSharpMat();
            Cv2.CopyMakeBorder(image, paddedImage, 0, paddedRows - image.Rows, 0, paddedCols - image.Cols, BorderTypes.Constant, Scalar.All(0));

            return paddedImage;
        }

        // 裁剪影像到原始大小
        private OpenCvSharpMat CropToOriginalSize(OpenCvSharpMat image, int originalRows, int originalCols)
        {
            return new OpenCvSharpMat(image, new Rect(0, 0, originalCols, originalRows));
        }

        // 执行 2D FFT
        private Complex[,] Compute2DFFT(OpenCvSharpMat image)
        {
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

        // 2D FFT 的可视化
        private OpenCvSharpMat VisualizeFFT2(Complex[,] fftResult)
        {
            int rows = fftResult.GetLength(0);
            int cols = fftResult.GetLength(1);

            OpenCvSharpMat magnitudeImage = new OpenCvSharpMat(rows, cols, MatType.CV_64F);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    magnitudeImage.Set(i, j, Math.Log(1 + Complex.Abs(fftResult[i, j])));

            // 将低频部分移动到中心
            ShiftDFT(magnitudeImage);

            // 标准化到 0-255
            Cv2.Normalize(magnitudeImage, magnitudeImage, 0, 255, NormTypes.MinMax);
            magnitudeImage.ConvertTo(magnitudeImage, MatType.CV_8U);

            return magnitudeImage;
        }

        // 逆傅立叶变换并重建图像
        private OpenCvSharpMat ReconstructImageFromFFT2(Complex[,] ifftResult, int originalRows, int originalCols)
        {
            int rows = ifftResult.GetLength(0);
            int cols = ifftResult.GetLength(1);

            OpenCvSharpMat reconstructedImage = new OpenCvSharpMat(rows, cols, MatType.CV_64F);

            // 提取实部并映射到 [0, 255]
            double min = double.MaxValue, max = double.MinValue;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    double value = ifftResult[i, j].Real;
                    min = Math.Min(min, value);
                    max = Math.Max(max, value);
                }

            // 防止全黑问题
            if (Math.Abs(max - min) < 1e-6) { min = 0; max = 1; }

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    double value = ifftResult[i, j].Real;
                    double pixelValue = (value - min) / (max - min) * 255;
                    reconstructedImage.Set(i, j, pixelValue);
                }

            // 裁剪回原始大小
            reconstructedImage = CropToOriginalSize(reconstructedImage, originalRows, originalCols);
            reconstructedImage.ConvertTo(reconstructedImage, MatType.CV_8U);

            return reconstructedImage;
        }
    }

}
