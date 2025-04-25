using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AOIInterface
{
    public class AOIInterface
    {
        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }
        private Bitmap _bitmap;

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        private bool _result;

        public int[][] Detectmap
        {
            get { return _detectmap; }
            set { _detectmap = value; }
        }

        private int[][] _detectmap;

        public AOIInterface()
        {

        }

        public bool startDetect(Bitmap bitmap)
        {
            bool result = false;
            //todo detection

            return result;
        }
    }
}
