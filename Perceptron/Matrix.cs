using System;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron {
    /// <summary>
    /// Contains matrix operations.
    /// </summary>
	internal class Matrix {
        /// <summary>
        /// Rows
        /// </summary>
        private int rows;

        /// <summary>
        /// Columns
        /// </summary>
        private int cols;

        /// <summary>
        /// Array data
        /// </summary>
        private float[,] data;

        /// <summary>
        /// Random number generator
        /// </summary>
        private static readonly Random rng = new Random();

        /// <summary>
        /// Rows
        /// </summary>
        public int Rows {
            get {
                return rows;
            }
        }

        /// <summary>
        /// Columns
        /// </summary>
        public int Cols {
            get {
                return cols;
            }
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rows">Rows</param>
        /// <param name="cols">Columns</param>
		public Matrix(int rows, int cols) {
            this.rows = rows;
            this.cols = cols;
            this.data = new float[rows,cols];
        }

        /// <summary>
        /// Create a Matrix object from an array.
        /// </summary>
        /// <param name="inputs">Array of data</param>
        /// <returns>Returns a Matrix object filled with the array's data.</returns>
        public static Matrix FromArray(float[] inputs) {
            Matrix m = new Matrix(inputs.Length, 1);
            for (int i = 0; i < inputs.Length; i++) {
                m.data[i, 0] = inputs[i];
            }
            return m;
        }

        /// <summary>
        /// Convert Matrix object to array.
        /// </summary>
        /// <returns>Returns an array.</returns>
        public float[] ToArray() {
            float[] array = new float[this.data.Length];
            for (int i = 0; i < array.Length; i++) {
                array[i] = this.data[i, 0];
            }
            return array;
        }

        /// <summary>
        /// Fill Matrix with random data.
        /// </summary>
        public void Randomize() {
            Parallel.For(0, this.rows, i => {
                Parallel.For(0, this.cols, j => {
                    this.data[i,j] = rng.NextFloat(-1.0F, 2.0F);
                });
            });
        }

        /// <summary>
        /// Override <c>ToString()</c> method to pretty-print a Matrix object.
        /// </summary>
        /// <returns>Returns Matrix object as string.</returns>
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

        /// <summary>
        /// Add a number to each element of the array.
        /// </summary>
        /// <param name="n">Scalar number</param>
        public void Add(int n) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] += n;
                }
            }
        }

        /// <summary>
        /// Add each element of the Matrices.
        /// </summary>
        /// <param name="m">Matrix object</param>
        public void Add(Matrix m) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] += m.data[i,j];
                }
            }
        }

        /// <summary>
        /// Subtract a number to each element of the array.
        /// </summary>
        /// <param name="n">Scalar number.</param>
        public void Subtract(int n) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] -= n;
                }
            }
        }

        /// <summary>
        /// Subtract each element of the Matrices.
        /// </summary>
        /// <param name="m">Matrix object</param>
        public void Subtract(Matrix m) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] -= m.data[i,j];
                }
            }
        }

        /// <summary>
        /// Subtract 2 Matrices and return a new object.
        /// </summary>
        /// <param name="a">Matrix object</param>
        /// <param name="b">Matrix object</param>
        /// <returns>Return a new Matrix object.</returns>
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
        /// Scalar product.
        /// Multiply each element of the array with the given number.
        /// </summary>
        /// <param name="n">Number</param>
        public void Multiply(float n) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] *= n;
                }
            }
        }

        /// <summary>
        /// Hadamard product (element-wise multiplication).
        /// Multiply each element of the array with each element of the given array.
        /// </summary>
        /// <param name="m">Matrix object</param>
        public void Multiply(Matrix m) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] *= m.data[i,j];
                }
            }
        }

        /// <summary>
        /// Matrix product.
        /// </summary>
        /// <param name="a">Matrix object</param>
        /// <param name="b">Matrix object</param>
        /// <returns>Returns a new Matrix object.</returns>
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

        /// <summary>
        /// Transpose a Matrix.
        /// </summary>
        /// <param name="m">Matrix object</param>
        /// <returns>Returns a new Matrix object.</returns>
        public static Matrix Transpose(Matrix m) {
            Matrix c = new Matrix(m.cols, m.rows);
            for (int i = 0; i < m.rows; i++) {
                for (int j = 0; j < m.cols; j++) {
                    c.data[j,i] = m.data[i,j];
                }
            }
            return c;
        }

        /// <summary>
        /// Transpose a Matrix.
        /// </summary>
        public void Transpose() {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[j,i] = this.data[i,j];
                }
            }
        }

        /// <summary>
        /// Apply a function to every element of the array.
        /// </summary>
        /// <param name="function">Delegate with encapsulates a method</param>
        public void Map(Func<float, float> function) {
            for (int i = 0; i < this.rows; i++) {
                for (int j = 0; j < this.cols; j++) {
                    this.data[i,j] = function(this.data[i,j]);
                }
            }
        }

        /// <summary>
        /// Apply a function to every element of the array.
        /// </summary>
        /// <param name="m">Matrix object</param>
        /// <param name="function">Delegate with encapsulates a method</param>
        /// <returns>Returns a new Matrix object.</returns>
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
