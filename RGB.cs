using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharpMat = OpenCvSharp.Mat;
namespace PaintApp



{
    public partial class RGBtrans : Form
    {
        private int Ksize = 3; //default
        private Paint __mainform;
        private Mat tempCanvas;
        private const int previewScale = 2;
        public RGBtrans(Paint mainform)
        {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            Preview_box.BringToFront();
            UpdatePictureBox(tempCanvas);
        }
        public static void OpenAndSetRGBTransform(Paint mainform, double ra, double rb, double ga, double gb, double ba, double bb)
        {
            // 創建 `RGBtrans` 視窗實例
            RGBtrans rgbForm = new RGBtrans(mainform);

            // 設定 RGB 變換參數
            rgbForm.ra = ra;
            rgbForm.rb = rb;
            rgbForm.ga = ga;
            rgbForm.gb = gb;
            rgbForm.ba = ba;
            rgbForm.bb = bb;

            // 直接應用變換
            OpenCvSharpMat temp = rgbForm.trans(mainform.canvas);
            mainform.canvas = temp;

            // 更新畫布顯示
            mainform.AdjustmentCanvas();

            // 清理資源
            rgbForm.Dispose();
        }


        private void Cancel_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Confirm_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            OpenCvSharpMat temp = trans(__mainform.canvas);
            __mainform.canvas = temp;
            Close();
        }


        private async Task UpdatePreviewAsync()
        {
            // 异步执行图像转换
            var newImage = await Task.Run(() =>
            {
                return trans(__mainform.canvas); 
            });

            // 更新 PictureBox
            UpdatePictureBox(newImage);
        }

        private OpenCvSharpMat trans(OpenCvSharpMat image)
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
                        if (c == 0)
                        {
                            double newValue = color[c] * ba + bb;
                            newColor[c] = (byte)(newValue < 0 ? 0 : (newValue > 255 ? 255 : newValue));
                        }
                        else if(c == 1)
                        {
                            double newValue = color[c] * ga + gb;
                            newColor[c] = (byte)(newValue < 0 ? 0 : (newValue > 255 ? 255 : newValue));
                        }
                        else
                        {
                            double newValue = color[c] * ra + rb;
                            newColor[c] = (byte)(newValue < 0 ? 0 : (newValue > 255 ? 255 : newValue));
                        }
                    }

                    newImage.Set(y, x, newColor);
                }
            }
            return newImage;
        }
        private void UpdatePictureBox(Mat image)
        {
            if (Preview_box.Image != null)
                Preview_box.Image.Dispose();
            Preview_box.Image = BitmapConverter.ToBitmap(image);
        }

        private void RGB_Load(object sender, EventArgs e)
        {

        }
        double ra =1.0, rb=0.0;
        double ga =1.0, gb=0.0;
        double ba =1.0, bb=0.0;

        private void textBox2_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (double.TryParse(textBox2.Text, out double number))
            {
                rb = number;
                _ = UpdatePreviewAsync();
            }
            
        }

        private void textBox3_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (double.TryParse(textBox3.Text, out double number))
            {
                ga = number;
                _ = UpdatePreviewAsync();
            }
        }

        private void textBox4_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (double.TryParse(textBox4.Text, out double number))
            {
                gb = number;
                _ = UpdatePreviewAsync();
            }
        }


        private void textBox5_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (double.TryParse(textBox5.Text, out double number))
            {
                ba = number;
                _ = UpdatePreviewAsync();
            }
        }

        private void textBox6_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (double.TryParse(textBox6.Text, out double number))
            {
                bb = number;
                _ = UpdatePreviewAsync();
            }
        }


        private void textBox1_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double number))
            {
                ra= number;
                _ = UpdatePreviewAsync();
            }
        }

    }
}
