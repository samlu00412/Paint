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


namespace Paint {
    public partial class Paint : Form {
        public Paint() {
            InitializeComponent();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void 開啟ToolStripMenuItem_Click(object sender, EventArgs e) {
            if(openFileDialog.ShowDialog() == DialogResult.OK) {
                pictureBox1.Load(openFileDialog.FileName);
                SizeImage();
                Centralize();
            }
        }
        //影像長寬讀取
        private void SizeImage() {
            pictureBox1.Width = pictureBox1.Image.Width;
            pictureBox1.Height = pictureBox1.Image.Height;
        }
        //影像置中
        private void Centralize() {
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
        }
    }
}
