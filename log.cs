using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmguCVMat = Emgu.CV.Mat;
using OpenCvSharpMat = OpenCvSharp.Mat;
using Size = OpenCvSharp.Size;
namespace PaintApp
{
    public partial class log : Form
    {
        Paint mainform;
        OpenCvSharpMat show;
        public log(Paint _mainform)
        {
            InitializeComponent();
            mainform = _mainform;
        }
        private void UpdateCanvas()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = BitmapConverter.ToBitmap(show);
        }
        private void log_Load(object sender, EventArgs e)
        {
            for (int y = 0; y < mainform.canvas.Rows; y++)
            {
                for (int x = 0; x < mainform.canvas.Cols; x++)
                {
                    Vec3b color = mainform.canvas.At<Vec3b>(y, x);
                    trackBar1.Value = Math.Max(trackBar1.Value, Math.Max(color[0], Math.Max(color[1], color[2])));
                }
            }
            show=Log變換(trackBar1.Value);
            UpdateCanvas();
        }
        private OpenCvSharpMat Log變換(int maxPixelValue)
        {
            OpenCvSharpMat newImage = new OpenCvSharpMat(mainform.canvas.Size(), mainform.canvas.Type());

            double c = 255.0 / Math.Log(1 + maxPixelValue);
            for (int y = 0; y < mainform.canvas.Rows; y++)
            {
                for (int x = 0; x < mainform.canvas.Cols; x++)
                {
                    Vec3b color = mainform.canvas.At<Vec3b>(y, x);
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

        private async void log_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();

            // 異步更新預覽
            await UpdatePreviewAsync(trackBar1.Value);
        }

        private async Task UpdatePreviewAsync(double gamma)
        {
            // 在後台進行伽瑪校正，減少UI卡頓
            int temp = trackBar1.Value;
            show = await Task.Run(() => Log變換(temp));

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
    }
}
