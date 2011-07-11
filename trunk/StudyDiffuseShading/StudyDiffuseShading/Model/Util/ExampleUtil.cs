using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Primitive;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Lighting;

namespace StudyDiffuseShading.Model.Util {
    public static class ExampleUtil {

        public static void buildCornelBox(Construction primitives, Illumination illumination, double diffuse) {
            IMaterial matte = new Matte(diffuse, Constant.WHITE);
            IMaterial emitter = new Emissive(Constant.WHITE, 1.0, 50.0);
# if false
            var rightMaterial = new Mirror(specular, Constant.GREEN);
            var leftMaterial = new Mirror(specular, Constant.RED); 
# else
            var rightMaterial = new Matte(diffuse, Constant.GREEN);
            var leftMaterial = new Matte(diffuse, Constant.RED);
# endif

            // 下面
            #region Floor
            //white
            //552.8 0.0   0.0   
            //  0.0 0.0   0.0
            //  0.0 0.0 559.2
            //549.6 0.0 559.2
            primitives.add(new Triangle(
                new Vector3D(552.8, 0.0, 0.0),
                new Vector3D(0.0, 0.0, -559.2),
                new Vector3D(0.0, 0.0, 0.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(552.8, 0.0, 0.0),
                new Vector3D(549.6, 0.0, -559.2),
                new Vector3D(0.0, 0.0, -559.2),
                matte));
            #endregion Floor

            // 照明
            #region Light
            //343.0 548.8 227.0
            //343.0 548.8 332.0
            //213.0 548.8 332.0
            //213.0 548.8 227.0
            var light1 = new Triangle(
                new Vector3D(343.0, 548, -227.0),
                new Vector3D(213.0, 548, -332.0),
                new Vector3D(343.0, 548, -332.0),
                emitter);
            primitives.add(light1);
            illumination.addLight(light1);
            var light2 = new Triangle(
                new Vector3D(343.0, 548, -227.0),
                new Vector3D(213.0, 548, -227.0),
                new Vector3D(213.0, 548, -332.0),
                emitter);
            primitives.add(light2);
            illumination.addLight(light2);
            #endregion Light

            // 上面
            #region Ceiling
            //556.0 548.8   0.0
            //556.0 548.8 559.2
            //0.0 548.8 559.2
            //0.0 548.8   0.0
            primitives.add(new Triangle(
                new Vector3D(556.0, 548.8, 0.0),
                new Vector3D(0.0, 548.8, -559.2),
                new Vector3D(556.0, 548.8, -559.2),
                matte));
            primitives.add(new Triangle(
                new Vector3D(556.0, 548.8, 0.0),
                new Vector3D(0.0, 548.8, 0.0),
                new Vector3D(0.0, 548.8, -559.2),
                matte));
            #endregion Ceiling

            // 背面
            #region Backward
            //549.6   0.0 559.2
            //0.0   0.0 559.2
            //0.0 548.8 559.2
            //556.0 548.8 559.2
            primitives.add(new Triangle(
                new Vector3D(549.6, 0.0, -559.2),
                new Vector3D(0.0, 548.8, -559.2),
                new Vector3D(0.0, 0.0, -559.2),
                matte));
            primitives.add(new Triangle(
                new Vector3D(549.6, 0.0, -559.2),
                new Vector3D(556.0, 548.8, -559.2),
                new Vector3D(0.0, 548.8, -559.2),
                matte));
            #endregion Backward

            // 左面
            #region Left
            //0.0   0.0 559.2   
            //0.0   0.0   0.0
            //0.0 548.8   0.0
            //0.0 548.8 559.2
            primitives.add(new Triangle(
                new Vector3D(0.0, 0.0, -559.2),
                new Vector3D(0.0, 548.8, 0.0),
                new Vector3D(0.0, 0.0, 0.0),
                leftMaterial));
            primitives.add(new Triangle(
                new Vector3D(0.0, 0.0, -559.2),
                new Vector3D(0.0, 548.8, -559.2),
                new Vector3D(0.0, 548.8, 0.0),
                leftMaterial));
            #endregion Left

            // 右面
            #region Right
            //552.8   0.0   0.0
            //549.6   0.0 559.2
            //556.0 548.8 559.2
            //556.0 548.8   0.0
            primitives.add(new Triangle(
                new Vector3D(552.8, 0.0, 0.0),
                new Vector3D(556.0, 548.8, -559.2),
                new Vector3D(549.6, 0.0, -559.2),
                rightMaterial));
            primitives.add(new Triangle(
                new Vector3D(552.8, 0.0, 0.0),
                new Vector3D(556.0, 548.8, 0.0),
                new Vector3D(556.0, 548.8, -559.2),
                rightMaterial));
            #endregion Right

            // 小さい箱
            #region SmallBox
            //130.0 165.0  65.0
            // 82.0 165.0 225.0
            //240.0 165.0 272.0
            //290.0 165.0 114.0
            primitives.add(new Triangle(
                new Vector3D(130.0, 165.0, -65.0),
                new Vector3D(240.0, 165.0, -272.0),
                new Vector3D(82.0, 165.0, -225.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(130.0, 165.0, -65.0),
                new Vector3D(290.0, 165.0, -114.0),
                new Vector3D(240.0, 165.0, -272.0),
                matte));

            //290.0   0.0 114.0
            //290.0 165.0 114.0
            //240.0 165.0 272.0
            //240.0   0.0 272.0
            primitives.add(new Triangle(
                new Vector3D(290.0, 0.0, -114.0),
                new Vector3D(240.0, 165.0, -272.0),
                new Vector3D(290.0, 165.0, -114.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(290.0, 0.0, -114.0),
                new Vector3D(240.0, 0.0, -272.0),
                new Vector3D(240.0, 165.0, -272.0),
                matte));

            //130.0   0.0  65.0
            //130.0 165.0  65.0
            //290.0 165.0 114.0
            //290.0   0.0 114.0
            primitives.add(new Triangle(
                new Vector3D(130.0, 0.0, -65.0),
                new Vector3D(290.0, 165.0, -114.0),
                new Vector3D(130.0, 165.0, -65.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(130.0, 0.0, -65.0),
                new Vector3D(290.0, 0.0, -114),
                new Vector3D(290.0, 165.0, -114.0),
                matte));

            // 82.0   0.0 225.0
            // 82.0 165.0 225.0
            //130.0 165.0  65.0
            //130.0   0.0  65.0
            primitives.add(new Triangle(
                new Vector3D(82.0, 0.0, -225.0),
                new Vector3D(130.0, 165.0, -65.0),
                new Vector3D(82.0, 165.0, -225.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(82.0, 0.0, -225.0),
                new Vector3D(130.0, 0.0, -65.0),
                new Vector3D(130.0, 165.0, -65.0),
                matte));

            //240.0   0.0 272.0
            //240.0 165.0 272.0
            // 82.0 165.0 225.0
            // 82.0   0.0 225.0
            primitives.add(new Triangle(
                new Vector3D(240.0, 0.0, -272.0),
                new Vector3D(82.0, 165.0, -225.0),
                new Vector3D(240.0, 165.0, -272.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(240.0, 0.0, -272.0),
                new Vector3D(82.0, 0.0, -225.0),
                new Vector3D(82.0, 165.0, -225.0),
                matte));
            #endregion SmallBox

            // 大きい箱
            #region BigBox
            //423.0 330.0 247.0
            //265.0 330.0 296.0
            //314.0 330.0 456.0
            //472.0 330.0 406.0
            primitives.add(new Triangle(
                new Vector3D(423.0, 330.0, -247.0),
                new Vector3D(314.0, 330.0, -456.0),
                new Vector3D(265.0, 330.0, -296.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(423.0, 330.0, -247.0),
                new Vector3D(472.0, 330.0, -406.0),
                new Vector3D(314.0, 330.0, -456.0),
                matte));

            //423.0   0.0 247.0
            //423.0 330.0 247.0
            //472.0 330.0 406.0
            //472.0   0.0 406.0
            primitives.add(new Triangle(
                new Vector3D(423.0, 0.0, -247.0),
                new Vector3D(472.0, 330.0, -406.0),
                new Vector3D(423.0, 330.0, -247.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(423.0, 0.0, -247.0),
                new Vector3D(472.0, 0.0, -406.0),
                new Vector3D(472.0, 330.0, -406.0),
                matte));

            //472.0   0.0 406.0
            //472.0 330.0 406.0
            //314.0 330.0 456.0
            //314.0   0.0 456.0
            primitives.add(new Triangle(
                new Vector3D(472.0, 0.0, -406.0),
                new Vector3D(314.0, 330.0, -456.0),
                new Vector3D(472.0, 330.0, -406.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(472.0, 0.0, -406.0),
                new Vector3D(314.0, 0.0, -456.0),
                new Vector3D(314.0, 330.0, -456.0),
                matte));

            //314.0   0.0 456.0
            //314.0 330.0 456.0
            //265.0 330.0 296.0
            //265.0   0.0 296.0
            primitives.add(new Triangle(
                new Vector3D(314.0, 0.0, -456.0),
                new Vector3D(265.0, 330.0, -296.0),
                new Vector3D(314.0, 330.0, -456.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(314.0, 0.0, -456.0),
                new Vector3D(265.0, 0.0, -296.0),
                new Vector3D(265.0, 330.0, -296.0),
                matte));

            //265.0   0.0 296.0
            //265.0 330.0 296.0
            //423.0 330.0 247.0
            //423.0   0.0 247.0
            primitives.add(new Triangle(
                new Vector3D(265.0, 0.0, -296.0),
                new Vector3D(423.0, 330.0, -247.0),
                new Vector3D(265.0, 330.0, -296.0),
                matte));
            primitives.add(new Triangle(
                new Vector3D(265.0, 0.0, -296.0),
                new Vector3D(423.0, 0.0, -247.0),
                new Vector3D(423.0, 330.0, -247.0),
                matte));

            #endregion BigBox
        }
    }
}
