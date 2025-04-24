using OpenCvSharp.Extensions;
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
    public partial class equalizeHist : Form
    {
        Paint __mainform;
        public equalizeHist(Paint mainform)
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

            // 將處理後的影像存入 __mainform.canvas
            __mainform.canvas = EqualizeImage(__mainform.canvas);

            Close();
        }

        private void UpdatePreview()
        {
            Mat equalized = EqualizeImage(__mainform.canvas);

            // 顯示處理後的影像
            pictureBoxPreview.Image?.Dispose();
            pictureBoxPreview.Image = BitmapConverter.ToBitmap(equalized);
        }

        private Mat EqualizeImage(Mat input)
        {
            Mat output = new Mat();
            Cv2.EqualizeHist(input, output); // 直方圖均衡化
            return output;
        }
        public static void OpenAndSetEqualizeHistMode(Paint mainform)
        {
            // 建立 equalizeHist form（雖然我們不會實際 Show 它）
            var form = new PaintApp.equalizeHist(mainform);

            // 直接處理畫布影像
            Mat output = new Mat();
            Cv2.EqualizeHist(mainform.canvas, output);
            mainform.canvas = output;

            // 更新畫布顯示
            mainform.AdjustmentCanvas();

            // 清除視窗資源（實際沒 Show 但可清理）
            form.Dispose();
        }
    }
}
