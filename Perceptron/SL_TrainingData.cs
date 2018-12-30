namespace Perceptron {
	internal class SL_TrainingData {
		public float[,] inputs;
        public float[,] targets;
        public int length;

        public SL_TrainingData(int length) {
            this.length = length;
            this.inputs = new float[length, 2];
            this.targets = new float[length, 1];
        }

        public void Create(SingleLayerPerceptron p) {
            for (int i = 0, length = this.inputs.GetLength(1); i < length; i++) {
                this.inputs[i,0] = SingleLayerPerceptron.rng.NextFloat(-11);
                this.inputs[i,1] = SingleLayerPerceptron.rng.NextFloat(-11);
                this.targets[i,0] = p.IsAboveLine(new float[] { this.inputs[i,0], this.inputs[i,1] });
            }
		}
	}
}
