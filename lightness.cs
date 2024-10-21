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

namespace Paint {
    public partial class lightness : Form {

        public int TrackBarValue1 { get; private set; }
        public int TrackBarValue2 { get; private set; }
        public lightness() {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {

        }

        private void lightness_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            TrackBarValue1 = trackBar1.Value;
            TrackBarValue2 = trackBar2.Value;

            // 關閉表單
            this.DialogResult = DialogResult.OK;  // 設置 DialogResult 為 OK，表示用戶點擊了確定
            this.Close();
        }
    }
}
