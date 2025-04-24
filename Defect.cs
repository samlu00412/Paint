using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaintApp;
using OpenCvSharp;
using OpenCvSharp.Extensions;


namespace Paint {
    public partial class Defect : Form {
        private PaintApp.Paint _mainForm;
        OpenCvSharp.Point[][] contours;
        HierarchyIndex[] hierarchy;
        private Mat binary, previewImage;
        private const int previewScale = 2; // 預覽縮小倍率
        private int paddingX = 10;
        private int paddingY = 10;
        private int skipped_size = 1;

        public Defect(PaintApp.Paint mainform) {
            InitializeComponent();
            _mainForm = mainform;
            binary = _mainForm.canvas.Clone();
            previewImage = _mainForm.origin_picture.Resize(new OpenCvSharp.Size(binary.Width / previewScale, binary.Height / previewScale));
            UpdatePictureBox(previewImage);
        }

        private void confirm_click(object sender, EventArgs e) {
            
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();

            int numLabels = Cv2.ConnectedComponentsWithStats(binary, labels, stats, centroids);

            for (int i = 1; i < numLabels; i++) {
                int area = stats.Get<int>(i, (int)ConnectedComponentsTypes.Area);
                if (area < skipped_size) continue;

                int x = stats.Get<int>(i, (int)ConnectedComponentsTypes.Left);
                int y = stats.Get<int>(i, (int)ConnectedComponentsTypes.Top);
                int width = stats.Get<int>(i, (int)ConnectedComponentsTypes.Width);
                int height = stats.Get<int>(i, (int)ConnectedComponentsTypes.Height);

                // 擴張框線尺寸
                x = Math.Max(0, x - paddingX);
                y = Math.Max(0, y - paddingY);
                width = Math.Min(_mainForm.origin_picture.Width - x, width + paddingX * 2);
                height = Math.Min(_mainForm.origin_picture.Height - y, height + paddingY * 2);

                Rect boundingBox = new Rect(x, y, width, height);
                Cv2.Rectangle(_mainForm.origin_picture, boundingBox, new Scalar(0, 255, 0), 2);
            }

            if (_mainForm.canvas != null) {
                _mainForm.canvas.Dispose();
                _mainForm.canvas = null;
            }
            _mainForm.canvas = _mainForm.origin_picture.Clone();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_click(object sender, EventArgs e) {
            DialogResult=DialogResult.Cancel;
            Close();
        }
        private async Task UpdatePreviewAsync() {
            await Task.Run(() => {
                Mat labels = new Mat();
                Mat stats = new Mat();
                Mat centroids = new Mat();

                int numLabels = Cv2.ConnectedComponentsWithStats(binary, labels, stats, centroids);

                for (int i = 1; i < numLabels; i++) {
                    int area = stats.Get<int>(i, (int)ConnectedComponentsTypes.Area);
                    if (area < skipped_size) continue;

                    int x = stats.Get<int>(i, (int)ConnectedComponentsTypes.Left);
                    int y = stats.Get<int>(i, (int)ConnectedComponentsTypes.Top);
                    int width = stats.Get<int>(i, (int)ConnectedComponentsTypes.Width);
                    int height = stats.Get<int>(i, (int)ConnectedComponentsTypes.Height);

                    x = Math.Max(0, x - paddingX);
                    y = Math.Max(0, y - paddingY);
                    width = Math.Min(previewImage.Width - x, width + paddingX * 2);
                    height = Math.Min(previewImage.Height - y, height + paddingY * 2);
                    Rect boundingBox = new Rect(x, y, width, height);
                    if(width > skipped_size && height > skipped_size)
                        Cv2.Rectangle(previewImage, boundingBox, new Scalar(0, 255, 0), 2);
                }
            });

            // 更新PictureBox顯示
            UpdatePictureBox(previewImage);
        }

        private async void modify_size(object sender, EventArgs e) {
            size_label.Text = size_bar.Value.ToString();
            skipped_size = size_bar.Value;
            await UpdatePreviewAsync();
        }
        public static void OpenAndSetDefectMode(PaintApp.Paint mainform, int skipSize = 1)
        {
            var defectForm = new Defect(mainform)
            {
                ShowInTaskbar = false,
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                StartPosition = FormStartPosition.Manual,
                Location = new System.Drawing.Point(-2000, -2000) // 背景執行不顯示
            };

            defectForm.Show();
            Application.DoEvents();

            // 設定跳過小區塊面積閾值
            skipSize = Math.Max(defectForm.size_bar.Minimum, Math.Min(skipSize, defectForm.size_bar.Maximum));
            defectForm.size_bar.Value = skipSize;
            defectForm.size_label.Text = skipSize.ToString();

            // 更新預覽圖
            defectForm.modify_size(null, EventArgs.Empty); // 或手動 await defectForm.UpdatePreviewAsync()

            // 直接執行套用邏輯
            defectForm.confirm_click(null, EventArgs.Empty);

            defectForm.Close();
        }
        private void UpdatePictureBox(Mat image) {
            // 更新 PictureBox，避免資源洩漏
            if (preview_box.Image != null) {
                preview_box.Image.Dispose();
                preview_box.Image = null;
            }
            preview_box.Image = BitmapConverter.ToBitmap(image);
        }
    }
}
