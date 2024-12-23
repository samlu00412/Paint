using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Paint {
    public partial class contour : Form {
        private Paint _mainForm;
        private Mat originalImage, previewImage;
        private const int previewScale = 2; // 預覽縮小倍率
        private double low_thre = 1.0;
        private double high_thre;
        public contour(Paint mainform) {
            InitializeComponent();
            _mainForm = mainform;
            originalImage = _mainForm.canvas.Clone();
            previewImage = originalImage.Resize(new OpenCvSharp.Size(originalImage.Width / previewScale, originalImage.Height / previewScale));
            UpdatePictureBox(previewImage);
        }

        private void cancel_click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void low_thre_scroll(object sender, EventArgs e) {
            low_thre = low_thre_bar.Value / 100.0;
            if(3*low_thre >= 255.0) 
                high_thre = 255.0;
            else
                high_thre =3 * low_thre;
            low_thre_label.Text = $"{low_thre}";
            await UpdatePreviewAsync();
        }
        private async Task UpdatePreviewAsync() {
            // 在後台進行伽瑪校正，減少UI卡頓
            await Task.Run(() => Cv2.Canny(originalImage,previewImage,low_thre,high_thre));

            // 更新PictureBox顯示
            UpdatePictureBox(previewImage);
        }
        private void confirm_click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Cv2.Canny(_mainForm.canvas, _mainForm.canvas, low_thre, high_thre);
            Close();
        }
        private void UpdatePictureBox(Mat image) {
            // 更新 PictureBox，避免資源洩漏
            if (pictureBox1.Image != null) {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = BitmapConverter.ToBitmap(image);
        }
    }
}
