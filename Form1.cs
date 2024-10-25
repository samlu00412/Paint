using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
//using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;
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

        private Size init_kernal = new Size(1, 1);
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
            canvas = new OpenCvSharpMat(new Size(width, height), MatType.CV_8UC3, Scalar.All(255));
            BitmapConverter.ToBitmap(canvas);
            pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
            pictureBox1.Width = width;
            pictureBox1.Height = height;
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
                    tempCanvas = canvas.Clone();
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
                Size size = new Size(Math.Abs(currentPoint.X - startpoint.X), Math.Abs(currentPoint.Y - startpoint.Y));
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
        }
        //final
        private void DrawFinalShape() {
            PenMotion tempAct;
            switch (drawMode) {
                case "Line":
                    Cv2.Line(canvas, prevPoint, currentPoint, currentColor, penThickness);
                    tempAct = new PenMotion("Line", prevPoint, currentPoint, currentColor, penThickness, 0,new Size(0,0),null);
                    break;
                case "Rectangle":
                    Cv2.Rectangle(canvas, prevPoint, currentPoint, currentColor, penThickness); // 最終繪製黑色矩形
                    tempAct = new PenMotion("Rectangle", prevPoint, currentPoint, currentColor, penThickness, 0, new Size(0, 0), null);
                    break;
                case "Circle":
                    Cv2.Circle(canvas, startpoint, CalculateDistance(startpoint, currentPoint), currentColor, penThickness);
                    tempAct = new PenMotion("Circle", startpoint, currentPoint, currentColor, penThickness, CalculateDistance(startpoint, currentPoint), new Size(0, 0), null);
                    break;
                case "Ellipse":
                    Size size = new Size(Math.Abs(currentPoint.X - startpoint.X), Math.Abs(currentPoint.Y - startpoint.Y));
                    Cv2.Ellipse(canvas, startpoint, size, 0, 0, 360, currentColor, penThickness);
                    tempAct = new PenMotion("Ellipse", startpoint, currentPoint, currentColor, penThickness, CalculateDistance(startpoint, currentPoint), size, null);
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
                    tempAct = new PenMotion("Triangle", startpoint, currentPoint, currentColor, penThickness,0, new Size(0,0), TrianglePoint);
                    break;
                default:
                    Cv2.Line(canvas, prevPoint, currentPoint, currentColor, penThickness);
                    tempAct = new PenMotion("Free", prevPoint, currentPoint, currentColor, penThickness, 0, new Size(0, 0), null);
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
            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.FileName = "Untitled"; 

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                string filePath = saveFileDialog.FileName;//System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                Cv2.ImWrite(filePath, canvas);
            }
            if(saveFileDialog != null) 
                saveFileDialog.Dispose();
        }
        private void 開啟ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                canvas = Cv2.ImRead(openFileDialog.FileName);
                pictureBox1.Load(openFileDialog.FileName);
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
            Gauss blurform = new Gauss(init_kernal,init_sigma);
            if (blurform.ShowDialog() == DialogResult.OK) {
                Cv2.GaussianBlur(canvas, canvas, blurform.Kernal, blurform.Sigma);
                UpdateCanvas();
            }
        }
        private void 亮度對比度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lightness trackbarForm = new lightness();
            if (trackbarForm.ShowDialog() == DialogResult.OK)
            {
                double alpha = trackbarForm.TrackBarValue1; // 對比度
                int beta = (int)trackbarForm.TrackBarValue2; // 亮度
                bool isNegative = (trackbarForm.TrackBarValue1<0); // 檢查是否需要負片效果

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

        private void Pallate_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Color selectedColor = colorDialog.Color;
                currentColor = new Scalar(selectedColor.B, selectedColor.G, selectedColor.R); // BGR 格式
            }
        }
    }
    
}
