using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;

namespace Paint {
    
    public partial class Paint : Form {
        private Mat Canvas; // 畫布
        private Mat tempCanvas; // 預覽用畫布
        private OpenCvSharp.Point startpoint;
        private OpenCvSharp.Point prevPoint;
        private OpenCvSharp.Point currentPoint; // 當前鼠標位置
        private Point vertex,vertex2;
        private Point prevMouse;
        private Bitmap canvasBitmap;
        private bool isDrawing = false,isDragging = false; // 判斷是否正在繪製
        private string drawMode = "Free"; // 繪製模式
        private Rectangle showAspect;
        private Scalar currentColor = new Scalar(0, 0, 0);
        private Stack<PenMotion> action = new Stack<PenMotion>();
        private Stack<PenMotion> reaction = new Stack<PenMotion>();
        
        public Paint() {
            InitializeComponent();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        
        //影像長寬讀取
        private void SizeImage() {
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
        }
        //影像置中
        private void Centralize() {
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
        }

        private void 檢視ToolStripMenuItem_Click(object sender, EventArgs e) {

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
            Canvas = new Mat(new OpenCvSharp.Size(width, height), MatType.CV_8UC3, Scalar.All(255));
            //tempCanvas = canvas.Clone();
            canvasBitmap = BitmapConverter.ToBitmap(Canvas); // 初始轉換 Bitmap
            pictureBox1.Image = canvasBitmap; // 顯示初始畫布
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
            if (isDrawing) {
                if (drawMode == "Free") { //除了自由繪製其他皆要預覽
                    currentPoint = ConvertToImageCoordinates(e.Location);
                    DrawFinalShape();
                    prevPoint = currentPoint;
                    UpdateCanvas();
                }
                else {
                    currentPoint = ConvertToImageCoordinates(e.Location);
                    tempCanvas = Canvas.Clone();
                    DrawPreviewShape();
                    ShowTempCanvas();
                    tempCanvas.Dispose();
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
                if(tempCanvas != null) {
                    tempCanvas.Dispose();
                }
            }
            else if (isDragging) {
                isDragging = false;
            }
        }
        private void ShowTempCanvas() {
            if (pictureBox1.Image != null) {
                pictureBox1.Image.Dispose();
            }
            Bitmap bitmap = BitmapConverter.ToBitmap(tempCanvas);
            pictureBox1.Image = bitmap;
        }
        //preview
        private void DrawPreviewShape() {
            if (drawMode == "Line") {
                Cv2.Line(tempCanvas, prevPoint, currentPoint, currentColor, 2);
            }
            else if (drawMode == "Rectangle") {
                Cv2.Rectangle(tempCanvas, prevPoint, currentPoint, currentColor, 2);
            }
            else if (drawMode == "Circle") {
                Cv2.Circle(tempCanvas, startpoint, CalculateDistance(startpoint, currentPoint), currentColor, 2);
            }
            else if (drawMode == "Ellipse") {
                Size size = new Size(Math.Abs(currentPoint.X - startpoint.X), Math.Abs(currentPoint.Y - startpoint.Y));
                Cv2.Ellipse(tempCanvas, startpoint, size, 0, 0, 360, currentColor, 2);
            }
            else if (drawMode == "Triangle") {
                vertex.X = startpoint.X + (currentPoint.X - startpoint.X)/2;//等腰三角形
                vertex.Y = currentPoint.Y;
                vertex2.X = currentPoint.X;
                vertex2.Y = startpoint.Y;
                Point[] TrianglePoint = { startpoint, vertex2, vertex };
                Cv2.Polylines(tempCanvas, new[] {TrianglePoint}, true, currentColor, 2);
            }
        }
        //final
        private void DrawFinalShape() {
            PenMotion tempAct;
            switch (drawMode) {
                case "Line":
                    Cv2.Line(Canvas, prevPoint, currentPoint, currentColor, 2);
                    tempAct = new PenMotion("Line", prevPoint, currentPoint, currentColor, 2, 0,new Size(0,0),null);
                    action.Push(tempAct);
                    break;
                case "Rectangle":
                    Cv2.Rectangle(Canvas, prevPoint, currentPoint, currentColor, 2); // 最終繪製黑色矩形
                    tempAct = new PenMotion("Rectangle", prevPoint, currentPoint, currentColor, 2, 0, new Size(0, 0), null);
                    action.Push(tempAct);
                    break;
                case "Circle":
                    Cv2.Circle(Canvas, startpoint, CalculateDistance(startpoint, currentPoint), currentColor, 2);
                    tempAct = new PenMotion("Circle", startpoint, currentPoint, currentColor, 2, CalculateDistance(startpoint, currentPoint), new Size(0, 0), null);
                    action.Push(tempAct);
                    break;
                case "Ellipse":
                    Size size = new Size(Math.Abs(currentPoint.X - startpoint.X), Math.Abs(currentPoint.Y - startpoint.Y));
                    Cv2.Ellipse(Canvas, startpoint, size, 0, 0, 360, currentColor, 2);
                    tempAct = new PenMotion("Ellipse", startpoint, currentPoint, currentColor, 2, CalculateDistance(startpoint, currentPoint), size, null);
                    action.Push(tempAct);
                    break;
                case "Triangle":
                    vertex.X = startpoint.X + (currentPoint.X - startpoint.X) / 2;//等腰三角形
                    vertex.Y = currentPoint.Y;
                    vertex2.X = currentPoint.X;
                    vertex2.Y = startpoint.Y;
                    Point[] TrianglePoint = { startpoint, vertex2, vertex };
                    Cv2.Polylines(Canvas, new[] { TrianglePoint }, true, currentColor, 2);
                    tempAct = new PenMotion("Triangle", startpoint, currentPoint, currentColor, 2,0, new Size(0,0), TrianglePoint);
                    action.Push(tempAct);
                    break;
                default:
                    Cv2.Line(Canvas, prevPoint, currentPoint, currentColor, 2);
                    tempAct = new PenMotion("Free", prevPoint, currentPoint, currentColor, 2, 0, new Size(0, 0), null);
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
            Bitmap bitmap = BitmapConverter.ToBitmap(Canvas);
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
        }
        private Rectangle GetImageRectangleInPictureBox() {
            
            if (pictureBox1.Image == null)
                return new Rectangle();

            // 計算圖像縮放後在 PictureBox 中的實際顯示範圍
            double imageAspect = (double)Canvas.Width / Canvas.Height;
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
            // 取得 PictureBox 中顯示圖像的大小
            var imgRect = GetImageRectangleInPictureBox();
            // 計算圖像的縮放比例
            double scaleX = (double)Canvas.Width / imgRect.Width;
            double scaleY = (double)Canvas.Height / imgRect.Height;

            // 將滑鼠座標轉換為圖像座標
            int x = (int)((mouseLocation.X - imgRect.X) * scaleX);
            int y = (int)((mouseLocation.Y - imgRect.Y) * scaleY);

            // 防止座標超出圖像邊界
            x = Math.Max(0, Math.Min(Canvas.Width - 1, x));
            y = Math.Max(0, Math.Min(Canvas.Height - 1, y));

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
        private void 儲存檔案ToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.FileName = "Untitled"; // 預設文件名

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                canvasBitmap = BitmapConverter.ToBitmap(Canvas);

                // 獲取選擇的文件類型
                string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                switch (extension) {
                    case ".jpg":
                        canvasBitmap.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".png":
                        canvasBitmap.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        MessageBox.Show("Unsupported file format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            if(saveFileDialog != null) {
                saveFileDialog.Dispose();
            }
        }
        private void 開啟ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                pictureBox1.Load(openFileDialog.FileName);
                SizeImage();
                //Centralize();
            }
        }
        private void 結束ToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void 復原UndoToolStripMenuItem_Click(object sender, EventArgs e) {
            if (action.Count > 0) {
                reaction.Push(action.Pop());
                Redraw();
                UpdateCanvas(); // 更新顯示
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
            Canvas.SetTo(Scalar.All(255));
            foreach (PenMotion act in action) {
                switch (act.Type) {
                    case "Line":
                        Cv2.Line(Canvas, act.Start, act.End, act.Pencolor, act.Thickness);
                        break;
                    case "Rectangle":
                        Cv2.Rectangle(Canvas, act.Start, act.End, act.Pencolor, act.Thickness);
                        break;
                    case "Circle":
                        Cv2.Circle(Canvas, act.Start, CalculateDistance(act.Start, act.End), act.Pencolor, act.Thickness);
                        break;
                    case "Ellipse":
                        Cv2.Ellipse(Canvas, act.Start, act.Size, 0, 0, 360, act.Pencolor, act.Thickness);
                        break;
                    case "Triangle":
                        Cv2.Polylines(Canvas,new[] {act.Vertexes},true,act.Pencolor, act.Thickness);
                        break;
                    default:
                        Cv2.Line(Canvas, act.Start, act.End, act.Pencolor, act.Thickness);
                        break;
                }
            }
        }

        private void 三角形ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Triangle";
        }

        private void Pallate_Click(object sender, EventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                // 取得使用者選擇的顏色
                Color selectedColor = colorDialog.Color;

                // 將顏色轉換為 OpenCV 的 BGR 格式
                currentColor = new Scalar(selectedColor.B, selectedColor.G, selectedColor.R); // BGR 格式
            }
        }

    }
    public class PenMotion {
        public string Type { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public Scalar Pencolor { get; set; }
        public int Thickness { get; set; }
        public int Radius { get; set; }
        public Size Size { get; set; }

        public Point[] Vertexes { get; set; }
        public PenMotion(string type, Point start, Point end, Scalar pencolor, int thickness, int radius, Size size, Point[] vertexes) {
            this.Type = type;
            this.Start = start;
            this.End = end;
            this.Pencolor = pencolor;
            this.Thickness = thickness;
            this.Radius = radius;
            this.Size = size;
            this.Vertexes = vertexes;
        }
    }
}
