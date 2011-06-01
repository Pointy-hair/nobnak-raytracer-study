using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
namespace StudyDiffuseShading.Model {
    public class Window {
        private int width;
        private int height;
        private int size;
        private double scale;


        public Window(int width, int height, double scale) {
            this.scale = scale;
            this.width = width;
            this.height = height;
            this.size = width * height;
        }


        public Vector3D getRay(int index) {
            int row = index / width;
            int col = index % width;
            return new Vector3D();
        }


        public int Size {
            get { return size; }
        }
    }
}
