using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Paint;

namespace Paint {
    public partial class lightness : Form {
        private const int scaleFactor = 100; // 放大倍數，這樣可以達到兩位小數的精度
        public double TrackBarValue1 { get; private set; }
        public double TrackBarValue2 { get; private set; }
        public lightness() {
            InitializeComponent();
            trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
            trackBar2.Scroll += new EventHandler(trackBar2_Scroll);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {

        }

        private void lightness_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;  // 設置 DialogResult 為 OK，表示用戶點擊了確定
            Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            bri_label.Text = $"Value: {trackBar1.Value/(double)scaleFactor}";
        }
        private void trackBar2_Scroll(object sender, EventArgs e) {
            con_label.Text = $"Value: {trackBar2.Value}";
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e) {
            TrackBarValue1 = trackBar1.Value / (double)scaleFactor;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e) {
            TrackBarValue2 = trackBar2.Value;
        }

        
    }
}
