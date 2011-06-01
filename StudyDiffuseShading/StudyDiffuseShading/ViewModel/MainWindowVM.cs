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

        private Engine engine;


        public MainWindowVM() {
            this.engine = new Engine();
        }


        public BitmapSource Image {
            get {
                return (BitmapSource) GetValue(ImageProperty);
            }
            set {
                SetValue(ImageProperty, value);
            }
        }

        public void render(int width, int height) {
            engine.render();
            Image = engine.getResult().getImage();
        }
    }
}
