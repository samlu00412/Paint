using Emgu.CV.Structure;
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
        private readonly Dictionary<string, int> mode = new Dictionary<string, int> {
            {"Normal", 0 }, {"Gamma", 1},
            {"Sigmoid", 2 }, {"Piecewise", 3}
        };
        private int type = 0;
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
            Cv2.LUT(__mainform.canvas, buildLUT(beblack.Value, bewhite.Value),__mainform.canvas);
            Close();
        }
        private void beblack_Scroll(object sender, EventArgs e)
        {
            if(type == 0 || type == 3) {
                label1.Text = beblack.Value.ToString();
            }
            else if(type == 1) {
                label1.Text = (beblack.Value/64.0).ToString();
            }
            else if(type == 2) {
                label1.Text = (beblack.Value / 1260.0).ToString();
            }
            UpdatePreviewAsync(beblack.Value, bewhite.Value);
        }

        private void bewhite_Scroll(object sender, EventArgs e)
        {
            label2.Text = bewhite.Value.ToString();
            UpdatePreviewAsync(beblack.Value, bewhite.Value);
        }
        private Mat buildLUT(int Bthres,int Wthres) { //Bthres for gamma in gamma mode. Bthres and Wthres are for contrast and intersection respectively.
            Mat lut = new Mat(1, 256, MatType.CV_8UC1);
            byte[] lutData = new byte[256];
            switch (type) {
                case 0://normal
                    for (int i = 0; i < 256; i++) {
                        if (i < Bthres)
                            lutData[i] = 0; // 低於黑閥值 → 黑色
                        else if (i > Wthres)
                            lutData[i] = 255; // 高於白閥值 → 白色
                        else
                            lutData[i] = 128;
                    }
                    break;
                case 1://gamma
                    for (int i = 0; i < 256; i++) {
                        lutData[i] = (byte)(Math.Pow(i / 255.0, Bthres/48.0) * 255.0);
                    }
                    break;
                case 2://sigmoid
                    for (int i = 0; i < 256; i++) {
                        double value = 255.0 / (1.0 + Math.Exp(-Bthres/1020.0 * (i - Wthres)));
                        lutData[i] = (byte)Clamp(value, 0, 255);
                    }
                    break;
                case 3:
                    for (int i = 0; i < 256; i++) {
                        if (i < Bthres)
                            lutData[i] = (byte)255;  // 暗部增強
                        else if (i < Wthres)
                            lutData[i] = (byte)(i * 0.5); // 中間區域減弱
                        else
                            lutData[i] = (byte)255; // 亮部增強
                    }
                    break;
            }
            lut.SetArray(lutData);
            return lut;
        }
        private double Clamp(double val, int min, int max) {
            if (val < min)
                return min;
            if (val > max) return max;
            return val;
        }
        private async Task UpdatePreviewAsync(int blackThreshold, int whiteThreshold)
        {
            await Task.Run(() =>
            {
                if (__mainform.canvas.Empty())
                    return;

                // 套用 LUT
                Mat result = new Mat();
                if (__mainform.canvas.Type() == MatType.CV_8UC3) {
                    Mat[] channels = Cv2.Split(__mainform.canvas);
                    for (int i = 0; i < 3; i++) {
                        Cv2.LUT(channels[i], buildLUT(blackThreshold, whiteThreshold), channels[i]);
                    }
                    Cv2.Merge(channels, result);
                    foreach (Mat channel in channels) {
                        clear(channel);
                    }
                }
                else {     
                    Cv2.LUT(__mainform.canvas, buildLUT(blackThreshold,whiteThreshold), result);
                }
                // 顯示處理後的影像
                pictureBoxPreview.Invoke(new Action(() =>
                {
                    pictureBoxPreview.Image?.Dispose();
                    pictureBoxPreview.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
                    clear(result);
                }));
            });
        }
        private void clear(Mat img) {
            img.Dispose();
            img = null;
        }
        private void change_mode(object sender, EventArgs e) {
            if (mode.ContainsKey(modeBox.Text)) {
                type = mode[modeBox.Text];
            }
            UpdatePreviewAsync(beblack.Value, bewhite.Value);
            return;
        }
    }
}
