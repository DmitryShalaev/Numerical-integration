using Parser.Analog;

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
        private readonly MathParserFunc? func;
        private readonly AnalogParser? parser;
        private readonly double a;
        private readonly double b;
        private readonly Bitmap bmp;
        private readonly Graphics gfx;

        private double[] axisLimits;
        private Point origin;

        public Graph(PictureBox pictureBox, MathParserFunc func, double a, double b) {
            PB = pictureBox;

            this.func = func;
            this.a = a;
            this.b = b;

            bmp = new Bitmap(PB.Width, PB.Height);
            gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.White);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            PB.Image = bmp;

            double min = double.MaxValue, max = double.MinValue;
            for(double i = a; i < b; i += 0.0001) {
                double tmp = func(i);
                if(min >= tmp)
                    min = tmp;
                else if(max <= tmp)
                    max = tmp;
            }

            axisLimits = new double[] { a, b, min, max };

            PlotGrapg();
        }

        public Graph(PictureBox pictureBox, AnalogParser parser) {
            PB = pictureBox;

            this.parser = parser;
            this.a = parser.LeftBorder;
            this.b = parser.RightBorder;

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

        public void Visualize(Method method, Integral.Answer answer) {
            if(func == null)
                throw new Exception("Math parser function not declared");

            switch(method) {
                case Method.left_rectangle:
                    rectangle(answer, 0);
                    break;
                case Method.right_rectangle:
                    rectangle(answer, 1);
                    break;
                case Method.midpoint_rectangle:
                    rectangle(answer, 0.5);
                    break;
                case Method.trapezoid:
                    trapezoid(answer);
                    break;
                case Method.simpson:
                    simpson(answer);
                    break;
            }
        }

        private void rectangle(Integral.Answer answer, double frac) {
            List<Rectangle> rectangles = new();
            double dx = (b - a) / answer.number_of_splits;
            double offset = frac * dx;
            for(double i = 0; i <= answer.number_of_splits; i++) {
                Point p = GetPixelFromLocation((a + dx  * i) - offset, func(a + dx * i));
                if(func(a + dx * i) >= 0) {
                    rectangles.Add(new(p, new(GetPixelFromLocation(((a + dx * i) - offset) + dx, 0).X - p.X, origin.Y - p.Y)));
                } else {
                    int height = p.Y - origin.Y;
                    p.Y = origin.Y;
                    rectangles.Add(new(p, new(GetPixelFromLocation(((a + dx * i) - offset) + dx, 0).X - p.X, height)));
                }
            }

            gfx.DrawRectangles(Pens.Red, rectangles.ToArray());
            gfx.DrawString($"{answer.number_of_splits} : {frac}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 50, 20);
        }

        private void trapezoid(Integral.Answer answer) {

            double dx = (b - a) / answer.number_of_splits;
            for(double i = 0; i <= answer.number_of_splits; i++) {
                List<Point> trapezoids = new();
                trapezoids.Add(GetPixelFromLocation((a + dx * i), 0));
                trapezoids.Add(GetPixelFromLocation((a + dx * i), func(a + dx * i)));
                trapezoids.Add(GetPixelFromLocation(((a + dx * i) + dx), func((a + dx * i) + dx)));
                trapezoids.Add(GetPixelFromLocation((a + dx * i) + dx, 0));
                gfx.DrawPolygon(Pens.Red, trapezoids.ToArray());
            }

            gfx.DrawString($"{answer.number_of_splits}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 50, 20);
        }

        private void simpson(Integral.Answer answer) {

            double dx = (b - a) / answer.number_of_splits;
            for(double i = 1; i <= answer.number_of_splits; i += 2) {
                List<Point> parabolas = new();
                parabolas.Add(GetPixelFromLocation(((a + dx * i) - dx), func((a + dx * i) - dx)));
                parabolas.Add(GetPixelFromLocation((a + dx * i), func(a + dx * i)));
                parabolas.Add(GetPixelFromLocation(((a + dx * i) + dx), func((a + dx * i) + dx)));
                gfx.DrawCurve(Pens.Red, parabolas.ToArray());
                gfx.DrawLine(Pens.Red, GetPixelFromLocation(((a + dx * i) - dx), 0), GetPixelFromLocation(((a + dx * i) - dx), func((a + dx * i) - dx)));
                gfx.DrawLine(Pens.Red, GetPixelFromLocation(((a + dx * i)), 0), GetPixelFromLocation(((a + dx * i)), func((a + dx * i))));
            }
            gfx.DrawString($"{answer.number_of_splits}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 50, 20);
        }

        private void PlotGrapg() {
            List<Point> points = new();

            if(func != null) {
                for(double i = a; i < b; i += 0.0001)
                    points.Add(GetPixelFromLocation(i, func(i)));
            } else {
                for(int i = 0; i < parser.Count; i++)
                    points.Add(GetPixelFromLocation(parser[i].point.X, parser[i].point.Y));
            }

            origin = GetPixelFromLocation(0, 0);
            gfx.DrawLine(Pens.LightGray, 0, origin.Y, bmp.Width, origin.Y);
            gfx.DrawLine(Pens.LightGray, origin.X, 0, origin.X, bmp.Height);

            gfx.DrawLines(Pens.Blue, points.ToArray());

            gfx.DrawString($"{Math.Round(axisLimits[0])}; {Math.Round(axisLimits[1])}; {Math.Round(axisLimits[2])}; {Math.Round(axisLimits[3])} ", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 50, 0);


        }
    }
}
