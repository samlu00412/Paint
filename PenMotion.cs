using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Pen {
    public class PenMotion {
        public Paint.Paint formins;
        
        public string Type { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public Scalar Pencolor { get; set; }
        public int Thickness { get; set; }
        public int Radius { get; set; }
        public Size Size { get; set; }
        public Point[] Vertexes { get; set; }
        public Mat Canva { get; set; }
        public PenMotion(string type, Point start, Point end, Scalar pencolor, int thickness, int radius, Size size, Point[] vertexes,Mat canva) {
            Type = type;
            Start = start;
            End = end;
            Pencolor = pencolor;
            Thickness = thickness;
            Radius = radius;
            Size = size;
            Vertexes = vertexes;
            Canva = canva;
        }
        public PenMotion(Paint.Paint FormInstance) {
            formins = FormInstance;
        }
        
    }
}
