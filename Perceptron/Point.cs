namespace Perceptron {
	public class Point {
		private float x;
		private float y;
		private bool hit;

		public float X {
			get {
				return x;
			}
			set {
				x = value;
			}
		}

		public float Y {
			get {
				return y;
			}
			set {
				y = value;
			}
		}

		public bool Hit {
			get {
				return hit;
			}
			set {
				hit = value;
			}
		}

		public Point (float x, float y, bool hit = false) {
			this.x = x;
			this.y = y;
			this.hit = hit;
		}
	}
}
