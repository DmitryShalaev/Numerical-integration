using System;

namespace Integral {
	public class Answer {
		public Answer(double n, double ans) {
			this.splits = n;
			this.ans = ans;
		}

		public double splits { get; set; }
		public double ans { get; set; }

		public override string ToString() => $"{splits}\t{ans}";
	}

	public class Method {

		public delegate double ParserFunc(double x);

		static private Answer run(ParserFunc func, double delta) {
			double d = 1;
			int n = 1;

			while(Math.Abs(d) > delta)
				d = (func(n++) - func(n));

			double a = func(n);

			return new(n, (a + d) / 2);
		}

		static private double rectangle(ParserFunc func, double a, double b, double n, double frac) {
			double dx = (b - a) / n;
			double sum = 0;
			double xstart = a + frac * dx;

			for(int i = 0; i < n; i++) {
				sum += func(xstart + i * dx);
			}

			return sum * dx;
		}

		static public Answer left_rectangle(ParserFunc func, double a, double b, double delta) {
			double integrate(double n) {
				return rectangle(func, a, b, n, 0.0);
			}

			return run(integrate, delta);
		}

		static public Answer right_rectangle(ParserFunc func, double a, double b, double delta) {
			double integrate(double n) {
				return rectangle(func, a, b, n, 1.0);
			}
			return run(integrate, delta);
		}

		static public Answer midpoint_rectangle(ParserFunc func, double a, double b, double delta) {
			double integrate(double n) {
				return rectangle(func, a, b, n, 0.5);
			}
			return run(integrate, delta);
		}

		static public Answer trapezoid(ParserFunc func, double a, double b, double delta) {
			double integrate(double n) {
				double dx = (b - a) / n;
				double sum = (func(a) + func(b)) / 2;

				for(int i = 1; i < n; i++)
					sum += func(a + i * dx);

				return sum * dx;
			}
			return run(integrate, delta);
		}

		static public Answer simpson(ParserFunc func, double a, double b, double delta) {
			double integrate(double n) {
				double dx = (b - a) / n;
				double sum = 0;
				for(double i = 0; i < n; i++)
					sum += func(a + i * dx) + 4 * func((a + i * dx) + dx / 2) + func((a + i * dx) + dx);

				return (dx / 6) * sum;
			}
			return run(integrate, delta);
		}
	}
}
