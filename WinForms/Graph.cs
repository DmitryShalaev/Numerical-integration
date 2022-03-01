﻿using Parser.Analog;

namespace WinForms {
    internal class Graph {
        public enum Method {
            left_rectangle,
            right_rectangle,
            midpoint_rectangle,
            trapezoid,
            simpson
        }

        public delegate double ParserFunc(double x);
        public readonly PictureBox PB;
        private readonly ParserFunc func;
        private readonly AnalogParser? analogParser;
        private Integral.Answer? answer;
        private Method? lastMethod;
        private readonly double a;
        private readonly double b;
        private readonly double delta;
        private Bitmap bmp;
        private Graphics gfx;

        private double[] axisLimits;
        private Point origin;

        public Graph(PictureBox pictureBox, ParserFunc func, double a, double b, double delta) {
            PB = pictureBox;

            this.func = func;
            this.a = a;
            this.b = b;
            this.delta = delta;

            bmp = new Bitmap(PB.Width, PB.Height);
            gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.White);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            PB.Image = bmp;

            double min = double.MaxValue, max = double.MinValue;
            for(double i = a; i < b; i += ((b - a) / bmp.Width)) {
                double tmp = func(i);
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
            func = parser.Interpolate;
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

        public void Visualize(Method? method, Integral.Answer? answer = null) {
            switch(method) {
                case Method.left_rectangle:
                    lastMethod = Method.left_rectangle;
                    Rectangle(answer ?? Integral.Method.left_rectangle(func.Invoke, a, b, delta), 0);
                    break;
                case Method.right_rectangle:
                    lastMethod = Method.right_rectangle;
                    Rectangle(answer ?? Integral.Method.right_rectangle(func.Invoke, a, b, delta), 1);
                    break;
                case Method.midpoint_rectangle:
                    lastMethod = Method.midpoint_rectangle;
                    Rectangle(answer ?? Integral.Method.midpoint_rectangle(func.Invoke, a, b, delta), 0.5);
                    break;
                case Method.trapezoid:
                    lastMethod = Method.trapezoid;
                    Trapezoid(answer ?? Integral.Method.trapezoid(func.Invoke, a, b, delta));
                    break;
                case Method.simpson:
                    lastMethod = Method.simpson;
                    Simpson(answer ?? Integral.Method.simpson(func.Invoke, a, b, delta));
                    break;
            }
        }

        private void Rectangle(Integral.Answer answer, double frac) {
            List<Rectangle> rectangles = new();

            this.answer = answer;

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

#if DEBUG
            gfx.DrawString($"{answer.number_of_splits} : {frac}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
#endif
        }

        private void Trapezoid(Integral.Answer answer) {
            this.answer = answer;

            double dx = (b - a) / answer.number_of_splits;

            for(double i = 0; i <= answer.number_of_splits; i++) {
                List<Point> trapezoids = new();
                trapezoids.Add(GetPixelFromLocation((a + dx * i), 0));
                trapezoids.Add(GetPixelFromLocation((a + dx * i), func(a + dx * i)));
                trapezoids.Add(GetPixelFromLocation(((a + dx * i) + dx), func((a + dx * i) + dx)));
                trapezoids.Add(GetPixelFromLocation((a + dx * i) + dx, 0));
                gfx.DrawPolygon(Pens.Red, trapezoids.ToArray());
            }

#if DEBUG
            gfx.DrawString($"{answer.number_of_splits}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
#endif
        }

        private void Simpson(Integral.Answer answer) {
            this.answer = answer;

            double dx = (b - a) / answer.number_of_splits;
            List<Point> parabolas = new();
            for(double i = 0; i <= answer.number_of_splits; i++) {
                parabolas.Add(GetPixelFromLocation((a + dx * i), func(a + dx * i)));
                gfx.DrawLine(Pens.Red, GetPixelFromLocation(((a + dx * i)), 0), GetPixelFromLocation(((a + dx * i)), func((a + dx * i))));
            }
            gfx.DrawLine(Pens.Red, GetPixelFromLocation(a, 0), GetPixelFromLocation(b, 0));

            gfx.DrawCurve(Pens.Red, parabolas.ToArray());

#if DEBUG
            gfx.DrawString($"{answer.number_of_splits}\t{answer.ans}", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 20);
#endif
        }

        private void PlotGrapg() {
            origin = GetPixelFromLocation(0, 0);
            gfx.DrawLine(Pens.LightGray, 0, origin.Y, bmp.Width, origin.Y);
            gfx.DrawLine(Pens.LightGray, origin.X, 0, origin.X, bmp.Height);

            List<Point> points = new();            
            for(double i = a; i <= b; i += ((b - a) / bmp.Width))
                points.Add(GetPixelFromLocation(i, func(i)));
            
            gfx.DrawLines(Pens.Blue, points.ToArray());

#if DEBUG
            gfx.DrawString($"{Math.Round(axisLimits[0])}; {Math.Round(axisLimits[1])}; {Math.Round(axisLimits[2])}; {Math.Round(axisLimits[3])} ", new Font("Arial", 10), new SolidBrush(Color.Black), bmp.Width / 2 - 100, 0);
#endif
        }

        public void ReDraw() {
            if(answer != null && lastMethod != null) {

                bmp = new Bitmap(PB.Width, PB.Height);
                gfx = Graphics.FromImage(bmp);
                gfx.Clear(Color.White);
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                PB.Image = bmp;

                PlotGrapg();
                Visualize(lastMethod, answer);
            }
        }
    }
}
