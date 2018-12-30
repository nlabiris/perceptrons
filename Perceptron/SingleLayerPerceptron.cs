using System;
using System.Collections.Generic;

// Reference: https://appliedgo.net/perceptron/

namespace Perceptron {
	internal class SingleLayerPerceptron {
		public static Random rng = new Random();
		private float[] weights;
		private float bias;
		private float a;
		private float b;
		private float learningRate;


		/// <summary>
		/// Create a new perceptron with n inputs.
		/// </summary>
		/// <remarks>
		/// Weights and bias are initialized with random values between -1 and 1.
		/// </remarks>
		/// <param name="n">Number of inputs</param>
		/// <param name="learningRate">Learning rate</param>
		/// <param name="a">a</param>
		/// <param name="b">b</param>
		/// <returns>A new perceptron.</returns>
		public SingleLayerPerceptron(int n, float learningRate, float a, float b) {
			this.a = a;
			this.b = b;
			this.learningRate = learningRate;
			this.weights = new float[n];
			for (int i = 0; i < n; i++) {
				this.weights[i] = rng.NextFloat(-1.0F, 1.0F);
			}
			this.bias = rng.NextFloat(-1.0F, 1.0F);
		}

		/// <summary>
		/// Activate function.
		/// </summary>
		/// <param name="f"></param>
		/// <returns>The output value</returns>
		private int Activate(float f) {
			return (f >= 0) ? 1 : 0;
		}

		/// <summary>
		/// Linear function. Parameter <c>a</c> specifies the gradient of the line
		/// (that is, how steep the line is), and <c>b</c> sets the offset.
		/// </summary>
		/// <param name="x"></param>
		/// <returns>
		/// For a point (x,y), if the value of y is larger than the result of f(x),
		/// then (x,y) is above the line.
		/// </returns>
		private float f(float x) {
			return a * x + b;
		}

		/// <summary>
		/// This is our teacher’s solution manual.
		/// </summary>
		/// <param name="point"></param>
		/// <returns>1 if the point (x,y) is above the line y = ax + b, else 0.</returns>
		public int IsAboveLine(float[] point) {
			float x = point[0];
			float y = point[1];
			return (y > f(x)) ? 1 : 0;
		}
		
		/// <summary>
		/// <c>FeedForward</c> implements the core functionality of the perceptron.
		/// It weighs the input signals, sums them up, adds the bias, 
		/// and runs the result through the Heaviside step function. 
		/// </summary>
		/// <param name="inputs"></param>
		/// <returns>The output of the step function</returns>
		private int FeedForward(float[] inputs) {
			float sum = this.bias;
			for (int i = 0; i < this.weights.Length; i++) {
				sum += inputs[i] * this.weights[i];
			}
			return this.Activate(sum);
		}

		/// <summary>
		/// <c>Train</c> is our teacher. The teacher generates random test points and feeds them to the perceptron.
		/// Then the teacher compares the answer against the solution from the ‘solution manual’
		/// and tells the perceptron how far it is off
		/// 
		/// During the learning phase, the perceptron adjusts the weights
		/// and the bias based on how much the perceptron’s answer differs from the correct answer.
		/// </summary>
		/// <param name="inputs"></param>
		public void Train(float[] inputs, float target) {
			int guess = this.FeedForward(inputs);
			int delta = (int)target - guess;

			for (int i = 0; i < this.weights.Length; i++) {
				this.weights[i] += inputs[i] * delta * this.learningRate;
			}
			this.bias += delta * this.learningRate;
		}

		/// <summary>
		/// This is our test function.
		/// </summary>
		/// <returns>The number of correct answers.</returns>
		public int Test(float[] inputs) {
			int correctAnswers = 0;
			if (this.FeedForward(inputs) == this.IsAboveLine(inputs)) {
				correctAnswers += 1;
			}
			return correctAnswers;
		}
	}
}
