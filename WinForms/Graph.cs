using Parser.Analog;
using Parser.Mathematical;

namespace WinForms {
    internal class Graph {
        public enum Method {
            left_rectangle,
            right_rectangle,
            midpoint_rectangle,
            trapezoid,
            simpson
        }

        public delegate double MathParserFunc(double x);
        private readonly PictureBox PB;
        private readonly MathParser? mathParser;
        private readonly AnalogParser? analogParser;
        private readonly double a;
        private readonly double b;
        private readonly double delta;
        private readonly Bitmap bmp;
        private readonly Graphics gfx;

        private double[] axisLimits;
        private Point origin;

        public Graph(PictureBox pictureBox, MathParser mathParser, double a, double b, double delta) {
            PB = pictureBox;

            this.mathParser = mathParser;
            this.a = a;
            this.b = b;
            this.delta = delta;

            bmp = new Bitmap(PB.Width, PB.Height);
            gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.White);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            PB.Image = bmp;

            double min = double.MaxValue, max = double.MinValue;
            for(double i = a; i < b; i += 0.0001) {
                double tmp = mathParser.Evaluate(i);
                if(min >= tmp)
                    min = tmp;
                else if(max <= tmp)
                    max = tmp;
            }

            axisLimits = new double[] { a, b, min, max };

            PlotGrapg();
        }

        public Graph(PictureBox pictureBox, AnalogParser parser, double delta) {
            PB = pictureBox;

            analogParser = parser;
            a = parser.LeftBorder;
            b = parser.RightBorder;
            this.delta = delta;

            bmp = new Bitmap(PB.Width, PB.Height);
            gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.White);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            PB.Image = bmp;

            axisLimits = new double[] { a, b, parser.BottomBorder, parser.UpperBorder };

            PlotGrapg();
        }

        private Point GetPixelFromLocation(double x, double y) {
            double pxPerUnitX = bmp.Width  / (axisLimits[1] - axisLimits[0]);
            double pxPerUnitY = bmp.Height / (axisLimits[3] - axisLimits[2]);
            int xPx = (int)((x - axisLimits[0]) * pxPerUnitX);
            int yPx = (bmp.Height-1) - (int)((y - axisLimits[2]) * pxPerUnitY);
            return new Point(xPx, yPx);
        }

        public void Visualize(Method method) {
            if(mathParser != null) {
                switch(method) {
                    case Method.left_rectangle:
                        MathRectangle(Integral.Method.left_rectangle(mathParser, a, b, delta), 0);
                        break;
                    case Method.right_rectangle:
                        MathRectangle(Integral.Method.right_rectangle(mathParser, a, b, delta), 1);
                        break;
                    case Method.midpoint_rectangle:
                        MathRectangle(Integral.Method.midpoint_rectangle(mathParser, a, b, delta), 0.5);
                        break;
                    case Method.trapezoid:
                        MathTrapezoid(Integral.Method.trapezoid(mathParser, a, b, delta));
                        break;
                    case Method.simpson:
                        MathSimpson(Integral.Method.simpson(mathParser, a, b, delta));
                        break;
                }
            } else {
                switch(method) {
                    case Method.left_rectangle:
                        AnalogRectangle(Integral.Method.left_rectangle(analogParser), 0);
                        break;
                    case Method.right_rectangle:
                        AnalogRectangle(Integral.Method.left_rectangle(analogParser), 1);
                        break;
                    case Method.midpoint_rectangle:
                        AnalogRectangle(Integral.Method.left_rectangle(analogParser), 0.5);
                        break;
                    case Method.trapezoid:
                        AnalogTrapezoid(Integral.Method.trapezoid(analogParser));
                        break;
                    case Method.simpson:
                        AnalogSimpson(Integral.Method.simpson(analogParser));
                        break;
                }
            }
        }

        private void MathRectangle(Integral.Answer answer, double frac) {
            List<Rectangle> rectangles = new();
            double dx = (b - a) / answer.number_of_splits;
            double offset = frac * dx;
            for(double i = 0; i <= answer.number_of_splits; i++) {
                Point p = GetPixelFromLocation((a + dx  * i) - offset, mathParser.Evaluate(a + dx * i));
                if(mathParser.Evaluate(a + dx * i) >= 0) {
                    rectangles.Add(new(p, new(GetPixelFromLocation(((a + dx * i) - offset) + dx, 0).X - p.X, origin.Y - p.Y)));
                } else {
                    int height = p.Y - origin.Y;
                    p.Y = origin.Y;
                    rectangles.Add(new(p, new(GetPixelFromLocation(((a + dx * i) - offset) + dx, 0).X - p.X, height)));
                }
            }

            gfx.DrawRectangles(Pens.Red, rectangles.ToArray());
            gfx.DrawString($"{answer.number_of_splits} : {frac}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
        }

        private void AnalogRectangle(Integral.Answer answer, double frac) {
            List<Rectangle> rectangles = new();
            double dx = (b - a) / answer.number_of_splits;
            double offset = frac * dx;
            for(int i = 0; i < analogParser.Count - 1; i++) {
                Point p = GetPixelFromLocation(analogParser[i].point.X- offset, analogParser[i].point.Y);
                if(analogParser[i].point.Y >= 0) {
                    rectangles.Add(new(p, new(GetPixelFromLocation(analogParser[i + 1].point.X - offset, 0).X - p.X, origin.Y - p.Y)));
                } else {
                    int height = p.Y - origin.Y;
                    p.Y = origin.Y;
                    rectangles.Add(new(p, new(GetPixelFromLocation(analogParser[i + 1].point.X - offset, 0).X - p.X, height)));
                }
            }

            gfx.DrawRectangles(Pens.Red, rectangles.ToArray());
            gfx.DrawString($"{answer.number_of_splits} : {frac}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
        }

        private void MathTrapezoid(Integral.Answer answer) {

            double dx = (b - a) / answer.number_of_splits;
            for(double i = 0; i <= answer.number_of_splits; i++) {
                List<Point> trapezoids = new();
                trapezoids.Add(GetPixelFromLocation((a + dx * i), 0));
                trapezoids.Add(GetPixelFromLocation((a + dx * i), mathParser.Evaluate(a + dx * i)));
                trapezoids.Add(GetPixelFromLocation(((a + dx * i) + dx), mathParser.Evaluate((a + dx * i) + dx)));
                trapezoids.Add(GetPixelFromLocation((a + dx * i) + dx, 0));
                gfx.DrawPolygon(Pens.Red, trapezoids.ToArray());
            }

            gfx.DrawString($"{answer.number_of_splits}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
        }

        private void AnalogTrapezoid(Integral.Answer answer) {
            for(int i = 0; i < analogParser.Count - 1; i++) {
                List<Point> trapezoids = new();
                trapezoids.Add(GetPixelFromLocation(analogParser[i].point.X, 0));
                trapezoids.Add(GetPixelFromLocation(analogParser[i].point.X, analogParser[i].point.Y));
                trapezoids.Add(GetPixelFromLocation(analogParser[i + 1].point.X, analogParser[i + 1].point.Y));
                trapezoids.Add(GetPixelFromLocation(analogParser[i + 1].point.X, 0));
                gfx.DrawPolygon(Pens.Red, trapezoids.ToArray());
            }

            gfx.DrawString($"{answer.number_of_splits}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
        }

        private void MathSimpson(Integral.Answer answer) {

            double dx = (b - a) / answer.number_of_splits;
            List<Point> parabolas = new();
            for(double i = 0; i <= answer.number_of_splits; i++) {
                parabolas.Add(GetPixelFromLocation((a + dx * i), mathParser.Evaluate(a + dx * i)));
                gfx.DrawLine(Pens.Red, GetPixelFromLocation(((a + dx * i)), 0), GetPixelFromLocation(((a + dx * i)), mathParser.Evaluate((a + dx * i))));
            }
            gfx.DrawCurve(Pens.Red, parabolas.ToArray());

            gfx.DrawString($"{answer.number_of_splits}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
        }

        private void AnalogSimpson(Integral.Answer answer) {
            List<Point> parabolas = new();
            for(int i = 0; i < analogParser.Count-1; i++) {
                parabolas.Add(GetPixelFromLocation(analogParser[i].point.X, analogParser[i].point.Y));
                gfx.DrawLine(Pens.Red, GetPixelFromLocation(analogParser[i].point.X, 0), GetPixelFromLocation(analogParser[i].point.X, analogParser[i].point.Y));
            }
            gfx.DrawCurve(Pens.Red, parabolas.ToArray());

            gfx.DrawString($"{answer.number_of_splits}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
        }

        private void PlotGrapg() {
            List<Point> points = new();

            if(mathParser != null) {
                for(double i = a; i < b; i += 0.0001)
                    points.Add(GetPixelFromLocation(i, mathParser.Evaluate(i)));
            } else {
                for(int i = 0; i < analogParser.Count; i++)
                    points.Add(GetPixelFromLocation(analogParser[i].point.X, analogParser[i].point.Y));
            }

            origin = GetPixelFromLocation(0, 0);
            gfx.DrawLine(Pens.LightGray, 0, origin.Y, bmp.Width, origin.Y);
            gfx.DrawLine(Pens.LightGray, origin.X, 0, origin.X, bmp.Height);

            gfx.DrawLines(Pens.Blue, points.ToArray());

            gfx.DrawString($"{Math.Round(axisLimits[0])}; {Math.Round(axisLimits[1])}; {Math.Round(axisLimits[2])}; {Math.Round(axisLimits[3])} ", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 0);

            PB.Image = bmp;
        }
    }
}
