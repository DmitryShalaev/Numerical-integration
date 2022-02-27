using Parser.Analog;
using Parser.Mathematical;
using System;

namespace Integral {
    public class Answer {
        public Answer(double n, double a, double b) {
            this.number_of_splits = n;
            this.ans = Math.Abs((a + b) / 2);
        }

        public Answer(double n, double a) {
            this.number_of_splits = n;
            this.ans = Math.Abs(a);
        }

        public double number_of_splits { get; set; }
        public double ans { get; set; }

        public override string ToString() => $"{number_of_splits}\t{ans}";
    }

    public class Method {

        public delegate double MathParserFunc(double x);
        delegate double AnalogParserFunc();

        static private Answer run(MathParserFunc func, double delta) {
            double d = 1;
            int n = 1;

            while(Math.Abs(d) > delta)
                d = (func(n++) - func(n));

            double a = Math.Abs(func(n));

            return new(n, a, a + d);
        }

        static private double rectangle(MathParserFunc func, double a, double b, double n, double frac) {
            double dx = (b - a) / n;
            double sum = 0;
            double xstart = a + frac * dx;

            for(int i = 0; i < n; i++) {
                sum += func(xstart + i * dx);
            }

            return sum * dx;
        }

        static public Answer left_rectangle(MathParser parser, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle(parser.Evaluate, a, b, n, 0.0);
            }

            return run(integrate, delta);
        }
        static public Answer left_rectangle(AnalogParser parser) {
            double integrate() {
                double sum = 0;

                for(int i = 0; i < parser.Count - 1; i++)
                    sum += parser[i].point.Y * (parser[i + 1].point.X - parser[i].point.X);

                return sum;
            }
            return new(parser.Count, integrate());
        }

        static public Answer right_rectangle(MathParser parser, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle(parser.Evaluate, a, b, n, 1.0);
            }
            return run(integrate, delta);
        }
        static public Answer right_rectangle(AnalogParser parser) {
            double integrate() {
                double sum = 0;

                for(int i = 1; i < parser.Count; i++)
                    sum += parser[i].point.Y * (parser[i].point.X - parser[i - 1].point.X);

                return sum;
            }
            return new(parser.Count, integrate());
        }

        static public Answer midpoint_rectangle(MathParser parser, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle(parser.Evaluate, a, b, n, 0.5);
            }
            return run(integrate, delta);
        }
        static public Answer midpoint_rectangle(AnalogParser parser) {
            double integrate() {
                double sum = 0;

                for(int i = 1; i < parser.Count; i++)
                    sum += ((parser[i - 1].point.Y + parser[i].point.Y) / 2) * (parser[i].point.X - parser[i - 1].point.X);

                return sum;
            }
            return new(parser.Count, integrate());
        }

        static public Answer trapezoid(MathParser parser, double a, double b, double delta) {
            double integrate(double n) {
                double dx = (b - a) / n;
                double sum = (parser.Evaluate(a) + parser.Evaluate(b)) / 2;

                for(int i = 1; i < n; i++)
                    sum += parser.Evaluate(a + i * dx);

                return sum * dx;
            }
            return run(integrate, delta);
        }
        static public Answer trapezoid(AnalogParser parser) {
            double integrate() {
                double sum = ((parser[0].point.Y/2)*(parser[1].point.X-parser.LeftBorder))+
                             ((parser[^1].point.Y/2)*(parser.RightBorder-parser[^2].point.X));

                for(int i = 1; i < parser.Count - 1; i++)
                    sum += parser[i].point.Y * (parser[i + 1].point.X - parser[i].point.X);

                return sum;
            }
            return new(parser.Count, integrate());
        }

        static public Answer simpson(MathParser parser, double a, double b, double delta) {
            double integrate(double n) {
                double dx = (b - a) / n;
                double sum = 0;
                for(double i = 0; i < n; i++)
                    sum += parser.Evaluate(a + i * dx) + 4 * parser.Evaluate((a + i * dx) + dx / 2) + parser.Evaluate((a + i * dx) + dx);

                return (dx / 6) * sum;
            }
            return run(integrate, delta);
        }
        static public Answer simpson(AnalogParser parser) {
            double integrate() {
                double dx = (parser.RightBorder - parser.LeftBorder)/parser.Count;
                double sum = 0;

                for(int i = 1; i < parser.Count - 1; i += 2)
                    sum += parser[i - 1].point.Y + 4 * parser[i].point.Y + parser[i + 1].point.Y;

                return (dx / 3) * sum;
            }
            return new(parser.Count, integrate());
        }
    }
}
