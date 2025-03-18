using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PaintApp
{
    public partial class LUT : Form
    {
        private Paint __mainform;
        public LUT(Paint mainform)
        {
            __mainform = mainform;
            InitializeComponent();
            
        }

        private void LUT_Load(object sender, EventArgs e)
        {

        }
        private void Cancel_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Confirm_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Bitmap bitmap = new Bitmap(pictureBoxPreview.Image);
            __mainform.canvas = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);
            Close();
        }
        private void beblack_Scroll(object sender, EventArgs e)
        {
            label1.Text = beblack.Value.ToString();
            UpdatePreviewAsync(beblack.Value, bewhite.Value);
        }

        private void bewhite_Scroll(object sender, EventArgs e)
        {
            label2.Text = bewhite.Value.ToString();
            UpdatePreviewAsync(beblack.Value, bewhite.Value);
        }

        private async Task UpdatePreviewAsync(int blackThreshold, int whiteThreshold)
        {
            await Task.Run(() =>
            {
                if (__mainform.canvas.Empty())
                    return;

                // 建立 LUT (查找表)
                Mat lut = new Mat(1, 256, MatType.CV_8UC1);
                byte[] lutData = new byte[256];

                for (int i = 0; i < 256; i++)
                {
                    if (i < blackThreshold)
                        lutData[i] = 0; // 低於黑閥值 → 黑色
                    else if (i > whiteThreshold)
                        lutData[i] = 255; // 高於白閥值 → 白色
                    else
                        lutData[i] = 128; 
                }

                lut.SetArray(lutData);  // ✅ 正確


                // 套用 LUT
                Mat result = new Mat();
                Cv2.LUT(__mainform.canvas, lut, result);

                // 顯示處理後的影像
                pictureBoxPreview.Invoke(new Action(() =>
                {
                    pictureBoxPreview.Image?.Dispose();
                    pictureBoxPreview.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
                }));
            });
        }
    }
}
