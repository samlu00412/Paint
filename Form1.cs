using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;
using Pen;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.ConstrainedExecution;

namespace Paint {
    
    public partial class Paint : Form {
        private Mat canvas; 
        private Mat initCanvas;
        private Mat tempCanvas = new Mat(); 
        private Point startpoint;
        private Point prevPoint;
        private Point currentPoint; 
        private Point vertex,vertex2;
        private Point prevMouse;
        private Bitmap canvasBitmap;
        private bool isDrawing = false,isDragging = false; 
        private string drawMode = "Free"; 
        private Rectangle showAspect;
        private Scalar currentColor = new Scalar(0, 0, 0);
        private int penThickness = 2;
        private Stack<PenMotion> action = new Stack<PenMotion>();
        private Stack<PenMotion> reaction = new Stack<PenMotion>();
        public Paint() {
            InitializeComponent();

            this.KeyPreview = true; // 允許表單偵測按鍵
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);
            Series series = new Series
            {
                Name = "亮度",
                Color = System.Drawing.Color.Blue,
                ChartType = SeriesChartType.Column // 使用柱狀圖
            };
            series.Points.Add(50);
            chart1.Series.Add(series);

            // 設置 Y 軸範圍
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 255;
            
            PenMotion penMotion = new PenMotion(this);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {

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
            else if(e.Control && e.Shift && e.KeyCode == Keys.S) 
                儲存檔案ToolStripMenuItem_Click(sender,e);
        }
        //影像長寬讀取
        private void SizeImage() {
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
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

        private int CalculateDistance(Point P1,Point P2) {
            return (int)Math.Sqrt(Math.Pow(P2.X-P1.X,2) + Math.Pow(P2.Y - P1.Y, 2));
        }
        private void New_canva_click(object sender, EventArgs e) {
            int width = 1280, height = 720;
            canvas = new Mat(new Size(width, height), MatType.CV_8UC3, Scalar.All(255));
            initCanvas = canvas.Clone();
            canvasBitmap = BitmapConverter.ToBitmap(canvas);
            pictureBox1.Image = canvasBitmap;
            pictureBox1.Width = canvasBitmap.Width;
            pictureBox1.Height = canvasBitmap.Height;
        }

        //detecting mouse click
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                isDrawing = true;
                startpoint = ConvertToImageCoordinates(e.Location);
                prevPoint = ConvertToImageCoordinates(e.Location);
            }
            else if(e.Button == MouseButtons.Right) {
                isDragging = true;
                startpoint = ConvertToImageCoordinates(e.Location);
                prevMouse = ConvertToImageCoordinates(e.Location);
            }
        }
        //detecting mouse moving
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {

            if (canvas != null)
            {
                Point cur = ConvertToImageCoordinates(e.Location);
                double value = canvas.Get<byte>(cur.Y, cur.X);
                chart1.Series[0].Points[0].SetValueY(value);
                chart1.Refresh();

            }
            if (isDrawing) {
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
            else if (isDragging) {
                currentPoint = ConvertToImageCoordinates(e.Location);
                int deltaX = currentPoint.X - startpoint.X;
                int deltaY = currentPoint.Y - startpoint.Y;
                pictureBox1.Left = pictureBox1.Location.X + deltaX;
                pictureBox1.Top = pictureBox1.Location.Y + deltaY;
                prevMouse = ConvertToImageCoordinates(e.Location);
                pictureBox1.SendToBack();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            if (isDrawing) {
                isDrawing = false;
                currentPoint = ConvertToImageCoordinates(e.Location);
                DrawFinalShape(); // 繪製圖形
                UpdateCanvas(); // 更新顯示
                if(tempCanvas != null)
                {
                    //tempCanvas.Dispose();
                }

            }
            else if (isDragging) 
                isDragging = false;
        }
        private void ShowTempCanvas() {
            if (pictureBox1.Image != null) 
                pictureBox1.Image.Dispose();

            Bitmap bitmap = BitmapConverter.ToBitmap(tempCanvas);
            pictureBox1.Image = bitmap;
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
                vertex.X = startpoint.X + (currentPoint.X - startpoint.X)/2;//等腰三角形
                vertex.Y = currentPoint.Y;
                vertex2.X = currentPoint.X;
                vertex2.Y = startpoint.Y;
                Point[] TrianglePoint = { startpoint, vertex2, vertex };
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
                    action.Push(tempAct);
                    break;
                case "Rectangle":
                    Cv2.Rectangle(canvas, prevPoint, currentPoint, currentColor, penThickness); // 最終繪製黑色矩形
                    tempAct = new PenMotion("Rectangle", prevPoint, currentPoint, currentColor, penThickness, 0, new Size(0, 0), null);
                    action.Push(tempAct);
                    break;
                case "Circle":
                    Cv2.Circle(canvas, startpoint, CalculateDistance(startpoint, currentPoint), currentColor, penThickness);
                    tempAct = new PenMotion("Circle", startpoint, currentPoint, currentColor, penThickness, CalculateDistance(startpoint, currentPoint), new Size(0, 0), null);
                    action.Push(tempAct);
                    break;
                case "Ellipse":
                    Size size = new Size(Math.Abs(currentPoint.X - startpoint.X), Math.Abs(currentPoint.Y - startpoint.Y));
                    Cv2.Ellipse(canvas, startpoint, size, 0, 0, 360, currentColor, penThickness);
                    tempAct = new PenMotion("Ellipse", startpoint, currentPoint, currentColor, penThickness, CalculateDistance(startpoint, currentPoint), size, null);
                    action.Push(tempAct);
                    break;
                case "Triangle":
                    vertex.X = startpoint.X + (currentPoint.X - startpoint.X) / 2;//等腰三角形
                    vertex.Y = currentPoint.Y;
                    vertex2.X = currentPoint.X;
                    vertex2.Y = startpoint.Y;
                    Point[] TrianglePoint = { startpoint, vertex2, vertex };
                    Cv2.Polylines(canvas, new[] { TrianglePoint }, true, currentColor, penThickness);
                    tempAct = new PenMotion("Triangle", startpoint, currentPoint, currentColor, penThickness,0, new Size(0,0), TrianglePoint);
                    action.Push(tempAct);
                    break;
                default:
                    Cv2.Line(canvas, prevPoint, currentPoint, currentColor, penThickness);
                    tempAct = new PenMotion("Free", prevPoint, currentPoint, currentColor, penThickness, 0, new Size(0, 0), null);
                    action.Push(tempAct);
                    break; 
            }
            if (reaction.Count != 0) {
                reaction.Clear();
            }
        }
        private void UpdateCanvas() {
            if (pictureBox1.Image != null) {
                pictureBox1.Image.Dispose();
            }
            Bitmap bitmap = BitmapConverter.ToBitmap(canvas);
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
        }
        private Rectangle GetImageRectangleInPictureBox() {
            if (pictureBox1.Image == null)
                return new Rectangle();
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

        private Point ConvertToImageCoordinates(System.Drawing.Point mouseLocation) {
            var imgRect = GetImageRectangleInPictureBox();
            double scaleX = (double)canvas.Width / imgRect.Width;
            double scaleY = (double)canvas.Height / imgRect.Height;

            // 將滑鼠座標轉換為圖像座標
            int x = (int)((mouseLocation.X - imgRect.X) * scaleX);
            int y = (int)((mouseLocation.Y - imgRect.Y) * scaleY);

            // 防止座標超出圖像邊界
            x = Math.Max(0, Math.Min(canvas.Width - 1, x));
            y = Math.Max(0, Math.Min(canvas.Height - 1, y));

            return new Point(x, y);
        }
        private void 自由ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Free";
        }
        private void 直線ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Line";
        }
        private void 橢圓ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Ellipse";
        }
        private void 圓ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Circle";
        }
        private void 矩形ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Rectangle";
        }
        private void 三角形ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Triangle";
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
                initCanvas = canvas.Clone();
                pictureBox1.Load(openFileDialog.FileName);
                SizeImage();
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
            initCanvas.CopyTo(canvas);
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

        private void toolStripButton2_Click(object sender, EventArgs e) {
            復原UndoToolStripMenuItem_Click(sender, e);
        }

        private void 轉換成灰階ToolStripMenuItem_Click(object sender, EventArgs e) {
            Cv2.CvtColor(canvas,canvas,ColorConversionCodes.BGR2GRAY);
            UpdateCanvas();
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
            重做RedoToolStripMenuItem_Click(sender, e);
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
