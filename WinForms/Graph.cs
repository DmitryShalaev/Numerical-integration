using Parser.Analog;

namespace Graphs {
	public class Graph {
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
		public Integral.Answer? answer;
		private Method? lastMethod;
		private readonly double a;
		private readonly double b;
		private readonly double delta;
		public Bitmap bmp;
		private Graphics gfx;

		private double[] axisLimits;
		private Point origin;

		private Size? saveSize;

		public Graph(PictureBox pictureBox, ParserFunc func, double a, double b, double delta, double? bottomBorder = null, double? upperBorder = null) {
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

			if(bottomBorder == null || upperBorder == null) {
				double min = double.MaxValue, max = double.MinValue;
				for(double i = a; i < b; i += ((b - a) / bmp.Width)) {
					double tmp = func(i);
					if(min >= tmp)
						min = tmp;
					else if(max <= tmp)
						max = tmp;
				}
				axisLimits = new double[] { a, b, min, max };
			} else {
				axisLimits = new double[] { a, b, bottomBorder ?? 0, upperBorder ?? 0 };
			}

			PlotGraph();
		}

		public Graph(PictureBox pictureBox, AnalogParser parser, double delta) :
			this(pictureBox, parser.Interpolate, parser.LeftBorder, parser.RightBorder,
				delta, parser.BottomBorder, parser.UpperBorder) { }

		private Point GetPixelFromLocation(double x, double y) {
			double pxPerUnitX = Math.Max(bmp.Width  / (axisLimits[1] - axisLimits[0]),0);
			double pxPerUnitY = Math.Max(bmp.Height / (axisLimits[3] - axisLimits[2]),0);
			int xPx = (int)((x - axisLimits[0]) * pxPerUnitX);
			int yPx = bmp.Height - 1 - (int)((y - axisLimits[2]) * pxPerUnitY);
			return new Point(xPx, yPx);
		}

		public void Visualize(Method? method, Integral.Answer? answer = null) {
			switch(method) {
				case Method.left_rectangle:
					Rectangle(answer ?? Integral.Method.left_rectangle(func.Invoke, a, b, delta), 0);
					break;
				case Method.right_rectangle:
					Rectangle(answer ?? Integral.Method.right_rectangle(func.Invoke, a, b, delta), 1);
					break;
				case Method.midpoint_rectangle:
					Rectangle(answer ?? Integral.Method.midpoint_rectangle(func.Invoke, a, b, delta), 0.5);
					break;
				case Method.trapezoid:
					Trapezoid(answer ?? Integral.Method.trapezoid(func.Invoke, a, b, delta));
					break;
				case Method.simpson:
					Simpson(answer ?? Integral.Method.simpson(func.Invoke, a, b, delta));
					break;
			}
			lastMethod = method;
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
			Rectangle[] rec = rectangles.ToArray();
			gfx.DrawRectangles(Pens.Red, rectangles.ToArray());
		}

		private void Trapezoid(Integral.Answer answer) {
			this.answer = answer;

			double dx = (b - a) / answer.number_of_splits;

			for(double i = 0; i < answer.number_of_splits; i++) {
				List<Point> trapezoids = new();
				trapezoids.Add(GetPixelFromLocation((a + dx * i), 0));
				trapezoids.Add(GetPixelFromLocation((a + dx * i), func(a + dx * i)));
				trapezoids.Add(GetPixelFromLocation(((a + dx * i) + dx), func((a + dx * i) + dx)));
				trapezoids.Add(GetPixelFromLocation((a + dx * i) + dx, 0));
				gfx.DrawPolygon(Pens.Red, trapezoids.ToArray());
			}
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
		}

		private void PlotGraph() {
			origin = GetPixelFromLocation(0, 0);
			gfx.DrawLine(Pens.LightGray, 0, origin.Y, bmp.Width, origin.Y);
			gfx.DrawLine(Pens.LightGray, origin.X, 0, origin.X, bmp.Height);

			List<Point> points = new();
			for(double i = a; i <= b; i += ((b - a) / bmp.Width))
				points.Add(GetPixelFromLocation(i, func(i)));

			gfx.DrawLines(Pens.Blue, points.ToArray());
		}

		public void ReDraw() {
			if(answer != null && lastMethod != null) {

				bmp = new Bitmap(Math.Max(PB.Width, 1), Math.Max(PB.Height, 1));
				gfx = Graphics.FromImage(bmp);
				gfx.Clear(Color.White);
				gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				PB.Image = bmp;

				PlotGraph();
				Visualize(lastMethod, answer);
			}
		}

		public void SaveGraph() {
			using(SaveFileDialog saveFileDialog = new SaveFileDialog()) {
				saveFileDialog.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp";
				saveFileDialog.FileName = lastMethod.ToString() ?? "";
				if(saveFileDialog.ShowDialog() == DialogResult.OK) {
					PictureBox pictureBox = new PictureBox();

					SizeForm.SetSizeForm setSizeForm = new(PB.Size);
					setSizeForm.FormClosing += new FormClosingEventHandler(SetSizeFormClosing);
					setSizeForm.ShowDialog();

					if(saveSize != null) {
						pictureBox.Size = (Size)saveSize;

						Graph PBGraph =new(pictureBox, func, a, b, delta);
						PBGraph.Visualize(lastMethod);
						PBGraph.bmp.Save(saveFileDialog.FileName);
					}
				}
			}
		}

		private void SetSizeFormClosing(object? sender, FormClosingEventArgs e) {
			if((sender as SizeForm.SetSizeForm).SetSizeSuccessful) {
				saveSize = new(Math.Max((sender as SizeForm.SetSizeForm).width, 1), Math.Max((sender as SizeForm.SetSizeForm).height, 1));
			}
		}
	}
}
