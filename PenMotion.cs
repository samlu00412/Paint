using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Pen {

    public class Storage {
        public Mat Canvas { get; set; }
        public Storage(Mat canvas) {
            Canvas = canvas;
        }
    }
}
