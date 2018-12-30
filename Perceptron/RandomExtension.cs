using System;

namespace Perceptron {
	internal static class RandomExtension {
		public static double NextDouble(this Random rand, double minValue, double maxValue) {
			return rand.NextDouble() * Math.Abs(maxValue - minValue) + minValue;
		}

		public static float NextFloat(this Random rand) {
			return (float)rand.NextDouble();
		}

		public static float NextFloat(this Random rand, float maxValue) {
			return (float)(rand.NextDouble() * (double)maxValue);
		}

        public static float NextFloat(this Random rand, float minValue, float maxValue) {
			return (float)(rand.NextDouble() * Math.Abs((double)maxValue - (double)minValue) + minValue);
		}
	}
}
