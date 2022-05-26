using System;

namespace Integral;
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
	private readonly ParserFunc func;
	private readonly double a, b, delta;

	public Method(ParserFunc func, double a, double b, double delta) {
		this.func = func;
		this.a = a;
		this.b = b;
		this.delta = delta;
	}

	public delegate double ParserFunc(double x);

	private Answer run(ParserFunc func) {
		double d = 1;
		int n = 1;

		while(Math.Abs(d) > delta)
			d = func(n++) - func(n);

		double a = func(n);

		return new(n, a + d);
	}

	private double rectangle(double n, double frac) {
		double dx = (b - a) / n;
		double sum = 0;
		double xstart = a + frac * dx;

		for(int i = 0; i < n; i++)
			sum += func(xstart + i * dx);

		return sum * dx;
	}

	public Answer left_rectangle() {
		double integrate(double n) => rectangle(n, 0.0);

		return run(integrate);
	}

	public Answer right_rectangle() {
		double integrate(double n) => rectangle(n, 1.0);
		return run(integrate);
	}

	public Answer midpoint_rectangle() {
		double integrate(double n) => rectangle(n, 0.5);
		return run(integrate);
	}

	public Answer trapezoid() {
		double integrate(double n) {
			double dx = (b - a) / n;
			double sum = (func(a) + func(b)) / 2;

			for(int i = 1; i < n; i++)
				sum += func(a + i * dx);

			return sum * dx;
		}
		return run(integrate);
	}

	public Answer simpson() {
		double integrate(double n) {
			double dx = (b - a) / n;
			double sum = 0;
			for(double i = 0; i < n; i++)
				sum += func(a + i * dx) + 4 * func(a + i * dx + dx / 2) + func(a + i * dx + dx);

			return dx / 6 * sum;
		}
		return run(integrate);
	}
}
