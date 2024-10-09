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


namespace Paint {
    public partial class Paint : Form {
        private Mat canvas; // 畫布
        private Mat tempCanvas; // 預覽用畫布
        private OpenCvSharp.Point startpoint;
        private OpenCvSharp.Point prevPoint;
        private OpenCvSharp.Point currentPoint; // 當前鼠標位置
        private Point prevMouse;
        private Bitmap canvasBitmap;
        private bool isDrawing = false,isDragging = false; // 判斷是否正在繪製
        private string drawMode = "Free"; // 繪製模式
        private Rectangle showAspect;
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
            canvas = new Mat(new OpenCvSharp.Size(width, height), MatType.CV_8UC3, Scalar.All(255));
            //tempCanvas = canvas.Clone();
            canvasBitmap = BitmapConverter.ToBitmap(canvas); // 初始轉換 Bitmap
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
                    tempCanvas = canvas.Clone();
                    DrawPreviewShape();
                    ShowTempCanvas();
                    tempCanvas.Dispose();
                }
            }
            else if (isDragging) {
                int deltaX = e.X - prevMouse.X;
                int deltaY = e.Y - prevMouse.Y;
                pictureBox1.Left += deltaX;
                pictureBox1.Top += deltaY;
                prevMouse = ConvertToImageCoordinates(e.Location);
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
                Cv2.Line(tempCanvas, prevPoint, currentPoint, Scalar.Black, 2);
            }
            else if (drawMode == "Rectangle") {
                Cv2.Rectangle(tempCanvas, prevPoint, currentPoint, Scalar.Black, 2);
            }
            else if (drawMode == "Circle") {
                Cv2.Circle(tempCanvas,startpoint,CalculateDistance(startpoint,currentPoint),Scalar.Black,2);
            }
        }
        //final
        private void DrawFinalShape() {
            if (drawMode == "Line") {
                Cv2.Line(canvas, prevPoint, currentPoint, Scalar.Black, 2);
            }
            else if(drawMode == "Free"){
                Cv2.Line(canvas, prevPoint, currentPoint, Scalar.Black, 2);
            }
            else if (drawMode == "Rectangle") {
                Cv2.Rectangle(canvas, prevPoint, currentPoint, Scalar.Black, 2); // 最終繪製黑色矩形
            }
            else if(drawMode == "Circle") {
                Cv2.Circle(canvas, startpoint, CalculateDistance(startpoint, currentPoint), Scalar.Black, 2);
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
            // 取得 PictureBox 中顯示圖像的大小
            var imgRect = GetImageRectangleInPictureBox();
            // 計算圖像的縮放比例
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

        private void 矩形ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Rectangle";
        }
        private void 儲存檔案ToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.FileName = "Untitled"; // 預設文件名

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                canvasBitmap = BitmapConverter.ToBitmap(canvas);

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

        private void 圓ToolStripMenuItem_Click(object sender, EventArgs e) {
            drawMode = "Circle";
        }
    }
}
