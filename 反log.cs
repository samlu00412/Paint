using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using OpenCvSharp;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmguCVMat = Emgu.CV.Mat;
using OpenCvSharpMat = OpenCvSharp.Mat;
using Size = OpenCvSharp.Size;
namespace Paint
{
    public partial class 反log : Form
    {
        Mat show;
        Paint mainform;
        public 反log(Paint _mainform)
        {
            InitializeComponent();
            mainform = _mainform;
        }

        private void 反log_Load(object sender, EventArgs e)
        {


            show = 反Log變換(50.0);
            
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = BitmapConverter.ToBitmap(show);
            UpdateCanvas();
        }
        private void UpdateCanvas()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = BitmapConverter.ToBitmap(show);
        }
        private async void 反log_Scroll(object sender, EventArgs e)
        {
            label1.Text = (trackBar1.Value/10.0).ToString();

            // 異步更新預覽
            await UpdatePreviewAsync(trackBar1.Value/10.0);
        }

        private async Task UpdatePreviewAsync(double gamma)
        {
            // 在後台進行，減少UI卡頓
            double temp = trackBar1.Value/10.0;
            show = await Task.Run(() => 反Log變換(temp));

            // 更新PictureBox顯示
            UpdateCanvas();
        }
        private void check_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cance_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private OpenCvSharpMat 反Log變換(double c)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(mainform.canvas.Size(), mainform.canvas.Type());

            // 進行反對數變換
            for (int y = 0; y < mainform.canvas.Rows; y++)
            {
                for (int x = 0; x < mainform.canvas.Cols; x++)
                {
                    Vec3b color = mainform.canvas.At<Vec3b>(y, x);
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

    }
}
