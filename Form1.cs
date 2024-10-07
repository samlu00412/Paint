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


namespace Paint {
    public partial class Paint : Form {
        private Mat canvas; // 畫布
        private Mat tempCanvas; // 預覽用畫布
        private OpenCvSharp.Point startPoint; // 起點
        private OpenCvSharp.Point prevPoint;
        private OpenCvSharp.Point currentPoint; // 當前鼠標位置
        private Bitmap canvasBitmap;
        private bool isDrawing = false; // 判斷是否正在繪製
        private string drawMode = "Line"; // 繪製模式
        public Paint() {
            InitializeComponent();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void 開啟ToolStripMenuItem_Click(object sender, EventArgs e) {
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                pictureBox1.Load(openFileDialog.FileName);
                SizeImage();
                //Centralize();
            }
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
        }

        private void Shrink_click(object sender, EventArgs e) {
            pictureBox1.Width = Convert.ToInt32(pictureBox1.Width / 1.1);
            pictureBox1.Height = Convert.ToInt32(pictureBox1.Height / 1.1);
        }

        private void New_canva_click(object sender, EventArgs e) {
            int width = 1280, height = 720;
            canvas = new Mat(new OpenCvSharp.Size(width, height), MatType.CV_8UC3, Scalar.All(255));
            //tempCanvas = canvas.Clone();
            canvasBitmap = BitmapConverter.ToBitmap(canvas); // 初始時轉換一次 Bitmap
            pictureBox1.Image = canvasBitmap; // 顯示初始畫布
            pictureBox1.Width = canvasBitmap.Width;
            pictureBox1.Height = canvasBitmap.Height;
            UpdateCanvas();
        }

        
        //detecting mouse click
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                isDrawing = true;
                prevPoint = new OpenCvSharp.Point(e.X, e.Y);
            }
        }
        //detecting mouse moving
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (isDrawing) {
                currentPoint = new OpenCvSharp.Point(e.X, e.Y); // 當前點
                //tempCanvas = canvas.Clone();
                DrawFinalShape(); // 繪製預覽圖形
                //ShowTempCanvas();
                
                prevPoint = currentPoint;
                UpdateCanvas(); // 更新顯示   
                //tempCanvas.Dispose();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
            if (isDrawing) {
                isDrawing = false;
                //currentPoint = new OpenCvSharp.Point(e.X, e.Y); // 最終點
                //DrawFinalShape(); // 繪製最終圖形到原始畫布上
                //UpdateCanvas(); // 更新顯示
                //tempCanvas.Dispose();
            }
        }
        private void ShowTempCanvas() {
            Bitmap bitmap = BitmapConverter.ToBitmap(tempCanvas);
            pictureBox1.Image = bitmap;
        }
        //preview
        private void DrawPreviewShape() {
            if (drawMode == "Line") {
                Cv2.Line(canvas, prevPoint, currentPoint, Scalar.Black, 2);
            }
            else if (drawMode == "Rectangle") {
                Cv2.Rectangle(tempCanvas, startPoint, currentPoint, Scalar.Black, 2);
            }
        }
        //final
        private void DrawFinalShape() {
            if (drawMode == "Line") {
                
                Cv2.Line(canvas, prevPoint, currentPoint, Scalar.Black, 2); // 最終繪製黑色線條
            }
            else if (drawMode == "Rectangle") {
                Cv2.Rectangle(canvas, startPoint, currentPoint, Scalar.Black, 2); // 最終繪製黑色矩形
            }
        }
        private void UpdateCanvas() {
            canvasBitmap = BitmapConverter.ToBitmap(canvas); // 將 Mat 轉換為 Bitmap 並更新顯示
            pictureBox1.Image = canvasBitmap;
            pictureBox1.Refresh(); // 刷新顯示
            //Centralize();
        }
    }
}
