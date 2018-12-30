using System;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron {
	internal class Matrix {
        private int rows;
        private int cols;
        private float[,] data;
        private static readonly Random rand = new Random();

        public int Rows {
            get {
                return rows;
            }
        }

        public int Cols {
            get {
                return cols;
            }
        }

		public Matrix(int rows, int cols) {
            this.rows = rows;
            this.cols = cols;
            this.data = new float[rows,cols];
        }

        public static Matrix FromArray(float[] inputs) {
            Matrix m = new Matrix(inputs.Length, 1);
            for (int i = 0; i < inputs.Length; i++) {
                m.data[i, 0] = inputs[i];
            }
            return m;
        }

        public float[] ToArray() {
            float[] array = new float[this.data.Length];
            for (int i = 0; i < array.Length; i++) {
                array[i] = this.data[i, 0];
            }
            return array;
        }

        public void Randomize() {
            Parallel.For(0, this.rows, i => {
                Parallel.For(0, this.cols, j => {
                    this.data[i,j] = rand.NextFloat(-1.0F, 2.0F);
                });
            });
        }

        public override string ToString() {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    s.Append(this.data[i,j] + " ");
                }
                s.AppendLine();
            }
            return s.ToString();
        }

        public void Add(int n) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] += n;
                }
            }
        }

        public void Add(Matrix m) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] += m.data[i,j];
                }
            }
        }

        public void Subtract(int n) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] -= n;
                }
            }
        }

        public void Subtract(Matrix m) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] -= m.data[i,j];
                }
            }
        }

        public static Matrix Subtract(Matrix a, Matrix b) {
            Matrix c = new Matrix(a.rows, a.cols);
            for (int i = 0; i < c.rows; i++) {
                for (int j = 0; j < c.cols; j++) {
                    c.data[i,j] = a.data[i,j] - b.data[i,j];
                }
            }
            return c;
        }

        /// <summary>
        /// Scalar product
        /// </summary>
        /// <param name="n"></param>
        public void Multiply(float n) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] *= n;
                }
            }
        }

        /// <summary>
        /// Hadamard product (element-wise multiplication)
        /// </summary>
        /// <param name="m"></param>
        public void Multiply(Matrix m) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] *= m.data[i,j];
                }
            }
        }

        public static Matrix Multiply(Matrix a, Matrix b) {
            if (a.cols != b.rows) {
                throw new Exception("Columns of A must match columns of B");
            }
            Matrix c = new Matrix(a.rows, b.cols);
            for (int i = 0; i < c.rows; i++) {
                for (int j = 0; j < c.cols; j++) {
                    float sum = 0;
                    for (int k = 0; k < a.cols; k++) {
                        sum += a.data[i,k] * b.data[k,j];
                    }
                    c.data[i,j] = sum;
                }
            }
            return c;
        }

        public static Matrix Transpose(Matrix m) {
            Matrix c = new Matrix(m.cols, m.rows);
            for (int i = 0; i < m.rows; i++) {
                for (int j = 0; j < m.cols; j++) {
                    c.data[j,i] = m.data[i,j];
                }
            }
            return c;
        }

        public void Transpose() {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[j,i] = this.data[i,j];
                }
            }
        }

        public void Map(Func<float, float> function) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] = function(this.data[i,j]);
                }
            }
        }

        public static Matrix Map(Matrix m, Func<float, float> function) {
            Matrix c = new Matrix(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++) {
                for (int j = 0; j < m.cols; j++) {
                    c.data[i,j] = function(m.data[i,j]);
                }
            }
            return c;
        }
	}
}
