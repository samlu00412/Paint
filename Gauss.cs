using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Size = OpenCvSharp.Size;


namespace Paint {
    public partial class Gauss : Form {
        public Size Kernal = new Size(1,1);
        public double Sigma = 1.0;
        public Gauss(OpenCvSharp.Size initKernal, double initSigma) {
            InitializeComponent();
            Kernal_value.Text = "";Sigma_value.Text = "";
            Kernal_bar.Scroll += new EventHandler(Kernal_bar_Scroll);
            Sigma_bar.Scroll += new EventHandler(Sigma_bar_Scroll);
        }

        private void Kernal_bar_Scroll(object sender, EventArgs e) {
            Kernal.Width = Kernal_bar.Value;
            Kernal.Height = Kernal_bar.Value;
            Kernal_value.Text = $"{Kernal_bar.Value}";
        }

        private void Sigma_bar_Scroll(object sender, EventArgs e) {
            Sigma = Sigma_bar.Value / 100.0;
            Sigma_value.Text = $"{Sigma_bar.Value / 100.0}";
        }

        private void Kernal_bar_ValueChanged(object sender, EventArgs e) {
            Kernal.Width = Kernal_bar.Value;
            Kernal.Height = Kernal_bar.Value;
        }

        private void Sigma_bar_ValueChanged(object sender, EventArgs e) {
            Sigma = Sigma_bar.Value / 100.0;
        }

        private void Confirm_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Discard_Click(object sender, EventArgs e) {
            DialogResult= DialogResult.Cancel;
            Close();
        }
    }
}
