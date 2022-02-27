using Parser.Analog;

namespace WinForms {
    internal class Graph {
        public delegate double MathParserFunc(double x);
        private readonly PictureBox PB;
        private readonly MathParserFunc? func;
        private readonly AnalogParser? parser;
        private readonly double a;
        private readonly double b;
        private readonly Bitmap bmp;
        private readonly Graphics gfx;

        private double[] axisLimits;

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

            axisLimits = new double[] { a, b, -1, 1 };
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
        }

        private Point GetPixelFromLocation(double x, double y) {
            double pxPerUnitX = bmp.Width  / (axisLimits[1] - axisLimits[0]);
            double pxPerUnitY = bmp.Height / (axisLimits[3] - axisLimits[2]);
            int xPx = (int)((x - axisLimits[0]) * pxPerUnitX);
            int yPx = (bmp.Height-1) - (int)((y - axisLimits[2]) * pxPerUnitY);
            return new Point(xPx, yPx);
        }

        private double[] xs = new double[] { };
        private double[] ys = new double[] { };
        private void CreateData() {
            List<double> xsList = new List<double>();
            List<double> ysList = new List<double>();
            if(func!=null) {
                double min = double.MaxValue, max = double.MinValue;

                for(double i = a; i < b; i += 0.0001) {
                    double tmp = func(i);
                    if(min >= tmp) {
                        min = tmp;
                    } else if(max <= tmp) {
                        max = tmp;
                    }
                    axisLimits[2] = min;
                    axisLimits[3] = max;

                    xsList.Add(i);
                    ysList.Add(tmp);
                }
            } else {
                for(int i = 0; i < parser.Count; i++) {
                    xsList.Add(parser[i].point.X);
                    ysList.Add(parser[i].point.Y);
                }
            }

            xs = xsList.ToArray();
            ys = ysList.ToArray();
        }

        public void PlotData() {
            CreateData();

            Point origin = GetPixelFromLocation(0, 0);
            gfx.DrawLine(Pens.LightGray, 0, origin.Y, bmp.Width, origin.Y);
            gfx.DrawLine(Pens.LightGray, origin.X, 0, origin.X, bmp.Height);

            Point[] points = new Point[xs.Length];

            for(int i = 0; i < xs.Length; i++) {
                points[i] = GetPixelFromLocation(xs[i], ys[i]);
            }
            gfx.DrawLines(Pens.Blue, points);

            gfx.DrawString($"{Math.Round(axisLimits[0])}; {Math.Round(axisLimits[1])}; {Math.Round(axisLimits[2])}; {Math.Round(axisLimits[3])} ", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 50, 0);

            PB.Image = bmp;
        }
    }
}
