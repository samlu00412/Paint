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
    }
}
