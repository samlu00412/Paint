using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint {
    
    public partial class Low_pass : Form {
        private int Ksize = 3;
        private Paint __mainform;
        private Mat tempCanvas;
        private const int previewScale = 2; // 預覽縮小倍率

        public Low_pass(Paint mainform) {
            InitializeComponent();
            __mainform = mainform;
            tempCanvas = mainform.canvas.Clone();
            tempCanvas = tempCanvas.Resize(new OpenCvSharp.Size(tempCanvas.Width / previewScale, tempCanvas.Height / previewScale));
            Kernal_bar.Scroll += Kernal_change_scroll;
            UpdatePictureBox(tempCanvas);
        }

        private void Cancel_btn_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            tempCanvas.Dispose();
            Close();
        }

        private void Confirm_btn_Click(object sender, EventArgs e) {
            DialogResult= DialogResult.OK;
            Cv2.MedianBlur(__mainform.canvas, __mainform.canvas, Ksize);
            tempCanvas.Dispose();
            Close();
        }

        private async void Kernal_change_scroll(object sender, EventArgs e) {
                Kersize.Text = $"{Kernal_bar.Value*2 - 3}";
                Ksize = Int32.Parse(Kersize.Text);
                await UpdatePreviewAsync(Ksize);
        }

        private void Change_Ksize(object sender, EventArgs e) {
            Ksize = Int32.Parse(Kersize.Text);
        }
        private async Task UpdatePreviewAsync(int kernal) {  
            await Task.Run(() => Cv2.MedianBlur(__mainform.canvas,tempCanvas,kernal));          
            UpdatePictureBox(tempCanvas);
        }
        private void UpdatePictureBox(Mat image) {
            if (Preview_box.Image != null) 
                Preview_box.Image.Dispose();
            Preview_box.Image = BitmapConverter.ToBitmap(image);
        }
    }
}
