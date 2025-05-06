using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintApp {
    public partial class morphology : Form {
        private MorphTypes operation = MorphTypes.Erode;
        private readonly Dictionary<string, MorphTypes> mode = new Dictionary<string, MorphTypes> {
            {"侵蝕",MorphTypes.Erode }, {"膨脹",MorphTypes.Dilate},
            {"開運算",MorphTypes.Open }, {"閉運算",MorphTypes.Close},
            {"形態學梯度", MorphTypes.Gradient}, {"頂帽", MorphTypes.TopHat},
            {"黑帽", MorphTypes.BlackHat}
        };
        public static void OpenAndSetMorphologyMode(Paint mainform, string operationName = "侵蝕", int iteration = 1)
        {
            using (var morphForm = new morphology(mainform)
            {
                ShowInTaskbar = false,
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                StartPosition = FormStartPosition.Manual,
                Location = new System.Drawing.Point(-2000, -2000) // 隱藏視窗
            })
            {
                
                morphForm.Show();
                Application.DoEvents();
                morphForm.__mainform = mainform;
                morphForm.tempCanvas = mainform.canvas.Clone();
                morphForm.operation = morphForm.mode[operationName];
                morphForm.iteration = iteration;
                // 設定疊代次數
                morphForm.iterBar.Value = Math.Max(1, Math.Min(iteration, morphForm.iterBar.Maximum));
                morphForm.iterLabel.Text = $"{morphForm.iterBar.Value}";
                
                // 更新預覽
                morphForm.UpdatePreviewAsync(morphForm.iterBar.Value).ConfigureAwait(false);

                // 直接套用並關閉（模擬按下確認）
                morphForm.confirm_click(null, EventArgs.Empty);
            }
        }
        private int iteration = 1;
        private Paint __mainform;
        private Mat tempCanvas;
        private const int previewScale = 2;
        
        
        public morphology(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            UpdatePictureBox(tempCanvas);
        }

        private void UpdatePictureBox(Mat image) {
            if (previewBox.Image != null)
                previewBox.Image.Dispose();
            previewBox.Image = BitmapConverter.ToBitmap(image);
        }

        private void cancel_click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void confirm_click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Cv2.MorphologyEx(__mainform.canvas, __mainform.canvas, operation, new Mat(), iterations: iteration);
            Close();
        }

        private async void bar_scroll(object sender, EventArgs e) {
            iteration = iterBar.Value;
            iterLabel.Text = $"{iterBar.Value}";
            await UpdatePreviewAsync(iteration);
        }
        private async Task UpdatePreviewAsync(int iter) {
            await Task.Run(() => Cv2.MorphologyEx(__mainform.canvas,tempCanvas,operation,new Mat(),iterations:iter));
            UpdatePictureBox(tempCanvas);
        }

        private async void select_op(object sender, EventArgs e) {
            operation = mode[select_mode_Box.Text];
            await UpdatePreviewAsync(iteration);
        }
    }
}
