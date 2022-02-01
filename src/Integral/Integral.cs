using Parser;
using System;
using System.Threading.Tasks;

namespace Integral {
    class Method {

        delegate double integrateFunc(double n);
        static void run(integrateFunc integrate, double delta, string str) {
            double d = 1, n = 1;

            while(Math.Abs(d) > delta) {
                d = (integrate(n++) - integrate(n));
            }

            double a = Math.Abs(integrate(n));
            double b = a + d;

            if(a > b)
                (a, b) = (b, a);

            Console.WriteLine(str);
            Console.WriteLine("{0}	{1}	{2}", n, a, b);
        }

        private static double rectangle_rule(MathParser func, double a, double b, double n, double frac) {
            double dx = (b - a) / n;
            double sum = 0.0;
            double xstart = a + frac * dx;

            for(int i = 0; i < n; i++) {
                sum += func.Evaluate(xstart + i * dx);
            }

            return sum * dx;
        }

        public static async Task left_rectangle_rule_Async(MathParser func, double a, double b, double delta) {
            await Task.Run(() => left_rectangle_rule(func, a, b, delta));
        }
        public static void left_rectangle_rule(MathParser func, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle_rule(func, a, b, n, 0.0);
            }
            run(integrate, delta, "Left rectangle:");
        }

        public static async Task right_rectangle_rule_Async(MathParser func, double a, double b, double delta) {
            await Task.Run(() => right_rectangle_rule(func, a, b, delta));
        }
        public static void right_rectangle_rule(MathParser func, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle_rule(func, a, b, n, 1.0);
            }
            run(integrate, delta, "Right rectangle:");
        }

        public static async Task midpoint_rectangle_rule_Async(MathParser func, double a, double b, double delta) {
            await Task.Run(() => midpoint_rectangle_rule(func, a, b, delta));
        }
        public static void midpoint_rectangle_rule(MathParser func, double a, double b, double delta) {
            double integrate(double n) {
                return rectangle_rule(func, a, b, n, 0.5);
            }
            run(integrate, delta, "Midpoint rectangle:");
        }

        public static async Task trapezoid_rule_Async(MathParser func, double a, double b, double delta) {
            await Task.Run(() => trapezoid_rule(func, a, b, delta));
        }
        public static void trapezoid_rule(MathParser func, double a, double b, double delta) {
            double integrate(double n) {
                double dx = (b - a) / n;
                double sum = 0.5 * (func.Evaluate(a) + func.Evaluate(b));

                for(int i = 1; i < n; i++)
                    sum += func.Evaluate(a + i * dx);

                return sum * dx;
            }
            run(integrate, delta, "Trapezoidal:");
        }

        public static async Task simpson_rule_Async(MathParser func, double a, double b, double delta) {
            await Task.Run(() => simpson_rule(func, a, b, delta));
        }
        public static void simpson_rule(MathParser func, double a, double b, double delta) {
            double integrate(double n) {
                double dx = (b - a) / n;
                double sum = 0;
                for(double i = 0; i < n; i++)
                    sum += func.Evaluate(a + i * dx) + 4 * func.Evaluate((a + i * dx) + dx / 2) + func.Evaluate((a + i * dx) + dx);

                return (dx / 6) * sum;
            }
            run(integrate, delta, "Simpson:");
        }
    }
}
