using System;
using System.Collections.Generic;
using System.Linq;

namespace Perceptron {
	internal class Program {
		/// <summary>
		/// Entry point of the application.
		/// </summary>
		/// <param name="args"></param>
		// public static void Main(string[] args) {
		// 	MultiLayerPerceptron p = new MultiLayerPerceptron(2,2,1,0.1F);
		// 	ML_TrainingData training = new ML_TrainingData(2,1);
		// 	training.Create();
		// 	float[,] inputs = training.GetInputs();
		// 	float[,] outputs = training.GetOutputs();
		// 	for (int i = 0; i < 1000000; i++) {
		// 		int r = MultiLayerPerceptron.rng.Next(4);
		// 		float[] inp = new float[] { inputs[r,0], inputs[r,1] };
		// 		float[] outp = new float[] { outputs[r,0] };
		// 		p.Train(inp, outp);
		// 	}
		// 	for (int i = 0,length = training.data.GetLength(0); i < length; i++) {
		// 		float[] inp = new float[] { inputs[i,0], inputs[i,1] };
		// 		Console.WriteLine(p.Test(inp)[0]);
		// 	}
        // }

		public static void Main(string[] args) {
			Matrix a = new Matrix(2,3);
			Matrix b = new Matrix(3,1);
			a.Randomize();
			b.Randomize();
			Console.WriteLine(a);
			Console.WriteLine(b);
			Console.WriteLine(Matrix.Multiply(a,b));
        }

		// public static void Main(string[] args) {
		// 	SingleLayerPerceptron p = new SingleLayerPerceptron(2, 0.000001F, 0.4F, -1.0F);
		// 	SL_TrainingData data = new SL_TrainingData(100000);
		// 	data.Create(p);
		// 	for (int i = 0; i < data.length; i++) {
		// 		p.Train(new float[] { data.inputs[i,0], data.inputs[i,1] }, data.targets[i,0]);
		// 	}
		// 	int successRate = 0;
		// 	for (int i = 0; i < data.length; i++) {
		// 		successRate += p.Test(new float[] { data.inputs[i,0], data.inputs[i,1] });
		// 	}
		// 	Console.WriteLine("{0}/{1} of the answers were correct", successRate, data.length);
        // }

		private static void Draw(List<Point> dict) {
			int consoleWidth = 78;
			int consoleHeight = 40;

			var minX = dict.Min(c => c.X);
			var minY = dict.Min(c => c.Y);
			var maxX = dict.Max(c => c.X);
			var maxY = dict.Max(c => c.Y);

			// normalize points to new coordinates
			var normalized = dict.
				Select(c => new Point(c.X - minX, c.Y - minY, c.Hit)).
				Select(c => new Point((int)Math.Round((float) (c.X) / (maxX - minX) * (consoleWidth - 1)), (int)Math.Round((float) (c.Y) / (maxY - minY) * (consoleHeight - 1)), c.Hit)).ToArray();
			Func<int, int, bool> IsHit = (hx, hy) => {
				return normalized.Any(c => c.X == hx && c.Y == hy);
			};
			Func<int, int, bool> HitPoint = (hx, hy) => {
				return normalized.Any(c => (c.X == hx && c.Y == hy && c.Hit) ? true : false);
			};

			for (int y = consoleHeight - 1; y > 0; y -= 2) {
				Console.Write(y == consoleHeight - 1 ? '┌' : '│');
				for (int x = 0; x < consoleWidth; x++) {
					bool hitTop = IsHit(x, y);
					bool hitBottom = IsHit(x, y - 1);                    
					if (hitBottom && hitTop) {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.BackgroundColor = ConsoleColor.Black;
						Console.Write('█');
					} else if (hitTop) {
						Console.ForegroundColor = (HitPoint(x, y)) ? ConsoleColor.Green : ConsoleColor.Red;
						Console.BackgroundColor = ConsoleColor.Black;
						Console.Write('▀');
					} else if (hitBottom) {
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = (HitPoint(x, y - 1)) ? ConsoleColor.Green : ConsoleColor.Red;
						Console.Write('▀');
					} else {
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = ConsoleColor.Black;
						Console.Write('▀');
					}
				}
				Console.ResetColor();
				Console.WriteLine();
			}
			Console.WriteLine('└' + new string('─', (consoleWidth / 2) - 1) + '┴' + new string('─', (consoleWidth / 2) - 1) + '┘');
			Console.Write((dict.Min(x => x.X) + "/" + dict.Min(x => x.Y)).PadRight(consoleWidth / 3));
			Console.Write((dict.Max(x => x.Y) / 2).ToString().PadLeft(consoleWidth / 3 / 2).PadRight(consoleWidth / 3));
			Console.WriteLine(dict.Max(x => x.Y).ToString().PadLeft(consoleWidth / 3));
		}
	}
}
