using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StudyDiffuseShading.Model.Helper {
    public interface IRandomFactory {
        Random makeRandom();
    }


    public class RandomFactory : IRandomFactory {
        private static Random seeder = new Random();

        private ThreadLocal<Random> localRandom;


        public RandomFactory() {
            this.localRandom = new ThreadLocal<Random>(() => {
                int seed;
                lock (seeder) {
                    seed = seeder.Next();
                }
                return new Random(seed);
            });
        }


        public Random makeRandom() {
            return localRandom.Value;
        }
    }
}
