using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using StudyDiffuseShading.Model;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;

namespace StudyDiffuseShading.ViewModel {
    public class MainWindowVM : DependencyObject {
        #region Variables
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
            "Image", typeof(BitmapSource), typeof(MainWindowVM));
        public static readonly DependencyProperty PixelGainProperty = DependencyProperty.Register(
            "PixelGain", typeof(double), typeof(MainWindowVM));
        public static readonly DependencyProperty PixelBiasProperty = DependencyProperty.Register(
            "PixelBias", typeof(double), typeof(MainWindowVM));
        public static readonly DependencyProperty SampleNumberProperty = DependencyProperty.Register(
            "SampleNumber", typeof(double), typeof(MainWindowVM), new PropertyMetadata(1.0, (o, e) => {
                ((MainWindowVM)o).updatedSampleNumber();
            }));
        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(
            "ImageWidth", typeof(int), typeof(MainWindowVM), new PropertyMetadata(100));
        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(
            "ImageHeight", typeof(int), typeof(MainWindowVM), new PropertyMetadata(100));


        private Engine engine;
        #endregion Variables

        public MainWindowVM() {
            var eye = new Vector3D(278, 273, 800);
            var dir = new Vector3D(0, 0, -1);
            var ybase = new Vector3D(0, 1, 0);
            var zbase = -dir;
            var xbase = Vector3D.CrossProduct(ybase, zbase);
            var rotate = new Matrix3D(xbase.X, xbase.Y, xbase.Z, 0, ybase.X, ybase.Y, ybase.Z, 0, zbase.X, zbase.Y, zbase.Z, 0, 0, 0, 0, 1);

            var camera = Matrix3D.Multiply(rotate, MathUtil.translate(eye));

            this.engine = new Engine(camera);
        }


        public void render() {
            engine.Width = ImageWidth;
            engine.Height = ImageHeight;
            engine.render();
            Image = engine.getResult().getImage();
        }

        #region EventHandlers
        public void updatedSampleNumber() {
            if (engine == null)
                return;

            engine.SampleNum = (int)((double)SampleNumber + 0.5);
        }
        #endregion EventHandlers


        # region DependencyProperty
        public BitmapSource Image {
            get {
                return (BitmapSource) GetValue(ImageProperty);
            }
            set {
                SetValue(ImageProperty, value);
            }
        }
        public double PixelGain {
            get { return (double)GetValue(PixelGainProperty); }
            set { SetValue(PixelGainProperty, value); }
        }
        public double PixelBias {
            get { return (double)GetValue(PixelBiasProperty); }
            set { SetValue(PixelBiasProperty, value); }
        }
        public double SampleNumber {
            get { return (double)GetValue(SampleNumberProperty); }
            set { SetValue(SampleNumberProperty, value); }
        }
        public int ImageWidth {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public int ImageHeight {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        # endregion

    }
}
