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
using System.IO;
using System.Xml;


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
            DialogResult = DialogResult.Cancel;
            InitializeComponent();
            _mainForm = mainform;
            binary = _mainForm.canvas.Clone();
            previewImage = _mainForm.origin_picture.Resize(new OpenCvSharp.Size(binary.Width / previewScale, binary.Height / previewScale));
            UpdatePictureBox(previewImage);
        }
        
        private void confirm_click(object sender, EventArgs e) {
            List<Rect> rectList = GetDefectBoundingBoxes(binary, _mainForm.origin_picture.Width, _mainForm.origin_picture.Height);
            List<Rect> mergedRects = MergeOverlappingRects(rectList);// all rects are here
            if (_mainForm.canvas != null)
                clear(_mainForm.canvas);
            _mainForm.canvas = _mainForm.origin_picture.Clone();
            foreach (var rect in mergedRects) 
                Cv2.Rectangle(_mainForm.canvas, rect, new Scalar(0, 255, 0), 2);

            string bmpPath = _mainForm.currentImagePath;
            string saveXmlFolder = @"C:\project_xmls";
            SaveToVocXml(bmpPath, mergedRects, saveXmlFolder);
            DialogResult = DialogResult.OK;
            Close();
        }
        private void clear(Mat obj) {
            obj.Dispose();
            obj = null;
        }
        private async Task UpdatePreviewAsync() {
            Mat previewResult = null; // 不直接操作 previewImage

            await Task.Run(() => {
                Mat temp = _mainForm.origin_picture.Clone();
                Mat resized = temp.Resize(new OpenCvSharp.Size(binary.Width / previewScale, binary.Height / previewScale));
                clear(temp);

                previewResult = resized; // 把 resize 結果保留，不立刻 assign
                List<Rect> rectList = GetDefectBoundingBoxes(binary, previewResult.Width, previewResult.Height);
                List<Rect> mergedRects = MergeOverlappingRects(rectList);
                List<Rect> scaledRects = mergedRects.Select(r =>
                new Rect(
                        (int)(r.X / (double)previewScale),
                        (int)(r.Y / (double)previewScale),
                        (int)(r.Width / (double)previewScale),
                        (int)(r.Height / (double)previewScale)
                    )
                ).ToList();
                foreach (var rect in scaledRects)
                    Cv2.Rectangle(previewResult, rect, new Scalar(0, 255, 0), 1);
            });

            // === 主執行緒再 assign 給 previewImage 並更新 UI ===
            if (previewImage != null)
                clear(previewImage);

            previewImage = previewResult;
            UpdatePictureBox(previewImage);
        }


        private List<Rect> GetDefectBoundingBoxes(Mat binImg, int imgWidth, int imgHeight) {
            List<Rect> rectList = new List<Rect>();
            using (Mat labels = new Mat())
            using (Mat stats = new Mat())
            using (Mat centroids = new Mat()) {
                int numLabels = Cv2.ConnectedComponentsWithStats(binImg, labels, stats, centroids);

                for (int i = 1; i < numLabels; i++) {
                    int area = stats.Get<int>(i, (int)ConnectedComponentsTypes.Area);
                    if (area < skipped_size) continue;

                    int x = stats.Get<int>(i, (int)ConnectedComponentsTypes.Left);
                    int y = stats.Get<int>(i, (int)ConnectedComponentsTypes.Top);
                    int width = stats.Get<int>(i, (int)ConnectedComponentsTypes.Width);
                    int height = stats.Get<int>(i, (int)ConnectedComponentsTypes.Height);

                    x = Math.Max(0, x - paddingX);
                    y = Math.Max(0, y - paddingY);
                    width = Math.Min(imgWidth - x, width + paddingX * 2);
                    height = Math.Min(imgHeight - y, height + paddingY * 2);

                    if (width > skipped_size && height > skipped_size)
                    {
                       _mainForm.finddefect=true;
                        rectList.Add(new Rect(x, y, width, height));
                    }
                        
                }
            }

            return rectList;
        }

        private List<Rect> MergeOverlappingRects(List<Rect> rects) {
            List<Rect> merged = new List<Rect>();

            foreach (var rect in rects) {
                bool mergedFlag = false;

                for (int i = 0; i < merged.Count; i++) {
                    if (IsOverlapping(merged[i], rect)) {
                        merged[i] = merged[i] | rect;
                        mergedFlag = true;
                        break;
                    }
                }

                if (!mergedFlag)
                    merged.Add(rect);
            }
            return merged;
        }

        private bool IsOverlapping(Rect a, Rect b) {
            return (a & b).Width > 0 && (a & b).Height > 0;
        }

        private void cancel_click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
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
            if (preview_box.Image != null) {
                preview_box.Image.Dispose();
                preview_box.Image = null;
            }
            preview_box.Image = BitmapConverter.ToBitmap(image);
        }
        public static void SaveToVocXml(string imagePath, List<Rect> boxes, string outputFolder) {
            var fileName = Path.GetFileNameWithoutExtension(imagePath);
            var image = new Mat(imagePath);
            int width = image.Width;
            int height = image.Height;

            string xmlPath = Path.Combine(outputFolder, fileName + ".xml");

            XmlDocument doc = new XmlDocument();
            XmlElement annotation = doc.CreateElement("annotation");
            doc.AppendChild(annotation);

            void AddTextElement(XmlElement parent, string name, string value) {
                var el = doc.CreateElement(name);
                el.InnerText = value;
                parent.AppendChild(el);
            }

            AddTextElement(annotation, "folder", Path.GetFileName(Path.GetDirectoryName(imagePath)));
            AddTextElement(annotation, "filename", Path.GetFileName(imagePath));
            AddTextElement(annotation, "path", imagePath);

            XmlElement size = doc.CreateElement("size");
            AddTextElement(size, "width", width.ToString());
            AddTextElement(size, "height", height.ToString());
            AddTextElement(size, "depth", image.Channels().ToString());
            annotation.AppendChild(size);

            AddTextElement(annotation, "segmented", "0");

            foreach (var rect in boxes) {
                XmlElement obj = doc.CreateElement("object");
                AddTextElement(obj, "name", "defect");
                AddTextElement(obj, "pose", "Unspecified");
                AddTextElement(obj, "truncated", "0");
                AddTextElement(obj, "difficult", "0");

                XmlElement bndbox = doc.CreateElement("bndbox");
                AddTextElement(bndbox, "xmin", rect.Left.ToString());
                AddTextElement(bndbox, "ymin", rect.Top.ToString());
                AddTextElement(bndbox, "xmax", (rect.Right).ToString());
                AddTextElement(bndbox, "ymax", (rect.Bottom).ToString());

                obj.AppendChild(bndbox);
                annotation.AppendChild(obj);
            }

            Directory.CreateDirectory(outputFolder);
            doc.Save(xmlPath);
        }
    }
}
