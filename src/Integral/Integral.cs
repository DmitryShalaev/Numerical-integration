using System;

namespace Integral {
    public class Method {
        public class Answer {
            public Answer(double n, double a, double b) {
                this.number_of_splits = n;
                this.left_border = a;
                this.right_border = b;
            }

            public double number_of_splits { get; set; }
            public double left_border { get; set; }
            public double right_border { get; set; }

            public override string ToString() => $"{number_of_splits}\t{left_border}\t{right_border}";
        }

        delegate double integrateFunc(double n);
        public delegate double MathParserFunc(double x);
        static private Answer run(integrateFunc func, double delta) {
            double d = 1;
            int n = 1;

            while(Math.Abs(d) > delta)
                d = (func(n++) - func(n));

            double a = Math.Abs(func(n));
            double b = a + d;

            if(a > b)
                (a, b) = (b, a);
            return new(n, a, b);
        }

        static private double rectangle_rule(MathParserFunc func, double a, double b, double n, double frac) {
            double dx = (b - a) / n;
            double sum = 0.0;
            double xstart = a + frac * dx;

            for(int i = 0; i < n; i++) {
                sum += func(xstart + i * dx);
            }

            return sum * dx;
        }

        static public Answer left_rectangle_rule(MathParserFunc func, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle_rule(func, a, b, n, 0.0);
            }
            return run(integrate, delta);
        }

        static public Answer right_rectangle_rule(MathParserFunc func, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle_rule(func, a, b, n, 1.0);
            }
            return run(integrate, delta);
        }

        static public Answer midpoint_rectangle_rule(MathParserFunc func, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle_rule(func, a, b, n, 0.5);
            }
            return run(integrate, delta);
        }

        static public Answer trapezoid_rule(MathParserFunc func, double a, double b, double delta) {
            double integrate(double n) {
                double dx = (b - a) / n;
                double sum = 0.5 * (func(a) + func(b));

                for(int i = 1; i < n; i++)
                    sum += func(a + i * dx);

                return sum * dx;
            }
            return run(integrate, delta);
        }

        static public Answer simpson_rule(MathParserFunc func, double a, double b, double delta) {
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
