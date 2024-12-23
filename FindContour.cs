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
using OpenCvSharp.Internal.Vectors;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Paint
{
    public partial class FindContour : Form
    {
        private Paint _mainform;
        private Mat tempCanvas;
        private Mat canvas;
        public FindContour(Paint mainform)
        {
            InitializeComponent();
            _mainform = mainform;
            canvas = mainform.canvas.Clone();
            tempCanvas = canvas.Clone();
        }
        private void Cancel_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Confirm_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        public void ApplyThreshold(int threshold, string method)
        {
            switch (method)
            {
                case "Binary":
                    UpdateThreshold(threshold, ThresholdTypes.Binary);
                    break;
                case "BinaryInv":
                    UpdateThreshold(threshold, ThresholdTypes.BinaryInv);
                    break;
                case "Truncate":
                    UpdateThreshold(threshold, ThresholdTypes.Trunc);
                    break;
                case "ToZero":
                    UpdateThreshold(threshold, ThresholdTypes.Tozero);
                    break;
                case "ToZeroInv":
                    UpdateThreshold(threshold, ThresholdTypes.TozeroInv);
                    break;
                case "Otsu":
                    UpdateThresholdOtsu();
                    break;
                case "AdaptiveMean":
                    UpdateAdaptiveThreshold(AdaptiveThresholdTypes.MeanC, blockSizeTrackBar.Value, cTrackBar.Value);
                    break;
                case "AdaptiveGaussian":
                    UpdateAdaptiveThreshold(AdaptiveThresholdTypes.GaussianC, blockSizeTrackBar.Value, cTrackBar.Value);
                    break;
                default:
                    UpdateThreshold(threshold, ThresholdTypes.Binary);
                    break;
            }

        }
        private void UpdateThreshold(int threshold, ThresholdTypes type)
        {
            Cv2.Threshold(canvas, tempCanvas, threshold, 255, type);
            UpdatePictureBoxtempsee();
            findCounterAsync();
        }

        private void findCounterAsync()
        {
            OpenCvSharp.Point[][] contours = Array.Empty<OpenCvSharp.Point[]>();
            HierarchyIndex[] hierarchy = Array.Empty<HierarchyIndex>();
            Cv2.FindContours(tempCanvas, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            Mat result = new Mat();

            Cv2.CvtColor(canvas, result, ColorConversionCodes.GRAY2BGR);

            Cv2.DrawContours(result, contours, -1, Scalar.Green, 3);

            tempCanvas = result.Clone();
            UpdatePictureBox();
        }

        private void UpdatePictureBox()
        {
            if (Preview_box.Image != null)
            {
                Preview_box.Image.Dispose();
            }
            Preview_box.Image = BitmapConverter.ToBitmap(tempCanvas);
        }
        private void UpdatePictureBoxtempsee()
        {
            if (tempsee.Image != null)
            {
                tempsee.Image.Dispose();
            }
            tempsee.Image = BitmapConverter.ToBitmap(tempCanvas);
        }
        private void UpdateThresholdOtsu()
        {
            Cv2.Threshold(canvas, tempCanvas, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

            findCounterAsync();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int threshold = trackBar1.Value;
            string selectedMethod = comboBox1.SelectedItem.ToString();

            ApplyThreshold(threshold, selectedMethod);
        }
        private void UpdateAdaptiveThreshold(AdaptiveThresholdTypes adaptiveType, int blockSize, double c)
        {
            if (blockSize % 2 == 0 || blockSize < 3)
                blockSize = 3;
            Cv2.AdaptiveThreshold(
                canvas,     
                tempCanvas,    
                255,         
                adaptiveType, 
                ThresholdTypes.Binary,
                blockSize,   
                c             
            );
            UpdatePictureBoxtempsee();
            findCounterAsync();
        }
        private void blockSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            int blockSize = blockSizeTrackBar.Value;
            blockSize = (blockSize % 2 == 0) ? blockSize + 1 : blockSize; 
            double c = cTrackBar.Value;

            string selectedMethod = comboBox1.SelectedItem.ToString();
            if (selectedMethod == "AdaptiveMean" || selectedMethod == "AdaptiveGaussian")
            {
                AdaptiveThresholdTypes type = (selectedMethod == "AdaptiveMean")
                    ? AdaptiveThresholdTypes.MeanC
                    : AdaptiveThresholdTypes.GaussianC;
                UpdateAdaptiveThreshold(type, blockSize, c);
            }
            
        }
        private void cTrackBar_Scroll(object sender, EventArgs e)
        {
            double c = cTrackBar.Value;
            int blockSize = blockSizeTrackBar.Value;
            blockSize = (blockSize % 2 == 0) ? blockSize + 1 : blockSize; 

            string selectedMethod = comboBox1.SelectedItem.ToString();
            if (selectedMethod == "AdaptiveMean" || selectedMethod == "AdaptiveGaussian")
            {
                AdaptiveThresholdTypes type = (selectedMethod == "AdaptiveMean")
                    ? AdaptiveThresholdTypes.MeanC
                    : AdaptiveThresholdTypes.GaussianC;
                UpdateAdaptiveThreshold(type, blockSize, c);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int threshold = trackBar1.Value;
            UpdatePictureBoxtempsee();
            ApplyThreshold(threshold, comboBox1.SelectedItem.ToString());
        }
    }
}
