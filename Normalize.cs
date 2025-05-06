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
using OpenCvSharp.Extensions;
namespace PaintApp
{
    public partial class Normalize : Form
    {
        private Paint __mainform;

        public Normalize(Paint mainform)
        {

            __mainform = mainform;
            InitializeComponent();
            UpdatePreview();
        }
        private void Cancel_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Confirm_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Cv2.Normalize(__mainform.canvas, __mainform.canvas, 0, normalmax.Value, NormTypes.MinMax);
            Close();
        }
        private void UpdatePreview()
        {
            Mat normalized = NormalizeImage(__mainform.canvas);

            // 顯示處理後的影像
            pictureBoxPreview.Image?.Dispose();
            pictureBoxPreview.Image = BitmapConverter.ToBitmap(normalized);
        }

        private Mat NormalizeImage(Mat input)
        {
            Mat output = new Mat();

            // 進行影像正規化，將像素值範圍縮放至 0-255
            Cv2.Normalize(input, output, 0, normalmax.Value, NormTypes.MinMax);

            return output;
        }

        private void normalmax_Scroll(object sender, EventArgs e)
        {
            label1.Text=normalmax.Value.ToString();
            UpdatePreview();
        }
        public static void OpenAndSetNormalize(Paint mainform, int val) {
            // 建立 equalizeHist form（雖然我們不會實際 Show 它）
            var form = new PaintApp.equalizeHist(mainform);

            // 直接處理畫布影像
            Mat output = new Mat();
            Cv2.Normalize(mainform.canvas, output, 0, val, NormTypes.MinMax);
            mainform.canvas = output;

            // 更新畫布顯示
            mainform.AdjustmentCanvas();

            // 清除視窗資源（實際沒 Show 但可清理）
            form.Dispose();
        }
    }
}
