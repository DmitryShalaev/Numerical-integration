using Graphs;

namespace WindowForm {
	public partial class Window : Form {
		public Graph? graph;
		public Graph.Method method;
		private Size initialSize;
		private Point initiallocation;
		public Window(Graph.Method method) {
			InitializeComponent();

			this.method = method;
			this.Text = method.ToString().Replace('_', ' ');
		}

		public void Show(Graph.ParserFunc func, double a, double b, double delta) {
			graph = new(PB, func, a, b, delta);
			graph.Visualize(method);

			L_Answer.Text = "Answer: " + graph.answer.ans + "; Splits: " + graph.answer.number_of_splits;

			this.Show();
		}

		private void Item_SizeChanged(object sender, EventArgs e) {
			graph?.ReDraw();
		}

		public void SetSize(Size size) {
			this.Size = initialSize = size;
		}

		public void SetLocation(Point location) {
			this.Location = initiallocation = location;
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			graph.SaveGraph();
		}

		private void resetZizeToolStripMenuItem_Click(object sender, EventArgs e) {
			this.WindowState = FormWindowState.Normal;
			this.Size = initialSize;
			this.Location = initiallocation;
		}

		private void PB_DoubleClick(object sender, EventArgs e) {
			this.WindowState = this.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
		}
	}
}
