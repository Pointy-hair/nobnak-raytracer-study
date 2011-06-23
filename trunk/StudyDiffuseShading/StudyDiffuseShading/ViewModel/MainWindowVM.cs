using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using StudyDiffuseShading.Model;

namespace StudyDiffuseShading.ViewModel {
    public class MainWindowVM : DependencyObject {
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
            "Image", typeof(BitmapSource), typeof(MainWindowVM));
        public static readonly DependencyProperty PixelGainProperty = DependencyProperty.Register(
            "PixelGain", typeof(double), typeof(MainWindowVM));
        public static readonly DependencyProperty PixelBiasProperty = DependencyProperty.Register(
            "PixelBias", typeof(double), typeof(MainWindowVM));


        private Engine engine;


        public MainWindowVM() {
            this.engine = new Engine();
        }


        public void render(int width, int height) {
            engine.render();
            Image = engine.getResult().getImage();
        }


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
        # endregion

    }
}
