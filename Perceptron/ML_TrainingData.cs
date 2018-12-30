using System;

namespace Perceptron {
	internal class ML_TrainingData {
		public float[,] data;
        private int inputsLength;
        private int targetsLength;

        public ML_TrainingData(int inputsLength, int targetsLength) {
            this.inputsLength = inputsLength;
            this.targetsLength = targetsLength;
        }

        public void Create() {
            this.data = new float[,] {
                {0.0F, 0.0F, 0.0F},
                {0.0F, 1.0F, 1.0F},
                {1.0F, 0.0F, 1.0F},
                {1.0F, 1.0F, 0.0F}
            };
        }

        public float[,] GetInputs() {
            int rows = this.data.GetLength(0);
            int cols = this.inputsLength;
            float[,] inp = new float[rows, cols];
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    inp[i,j] = this.data[i,j];
                }
            }
            return inp;
        }

        public float[,] GetOutputs() {
            int rows = this.data.GetLength(0);
            int cols = this.data.GetLength(1); // 3
            int tgts = this.targetsLength; // 1
            float[,] tgt = new float[rows, tgts];
            for (int i = 0; i < rows; i++) {
                for (int j = cols - tgts, k = 0; j < cols; j++, k++) {
                    Console.WriteLine("{0} - {1}", i, k);
                    tgt[i,k] = this.data[i,j];
                }
            }
            return tgt;
        }
	}
}
