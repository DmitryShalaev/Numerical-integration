using Graphs;

namespace WindowForm;
public partial class Window : Form {
	public Graph? graph;
	public Graph.Method method;
	private Size initialSize;
	private Point initiallocation;

	public bool IsShown { get; private set; }

	public Window(Graph.Method method) {
		InitializeComponent();

		this.method = method;
		IsShown = false;
		Text = method.ToString().Replace('_', ' ');
	}

	public void Show(Graph.ParserFunc func, double a, double b, double upperBorder, double bottomBorder, double delta, bool reverse) {
		IsShown = true;

		graph = new(PB, func, a, b, upperBorder, bottomBorder, delta);
		graph.Visualize(method);

		L_Answer.Text = "Answer: " + (reverse == true ? -graph.answer.ans : graph.answer.ans) + "; Splits: " + graph.answer.splits;
		MinimumSize = new(L_Answer.Width + 16, 0);
		Show();
	}

	private void Item_SizeChanged(object sender, EventArgs e) => graph?.ReDraw();

	public void SetSize(Size size) => this.Size = initialSize = size;

	public void SetLocation(Point location) => this.Location = initiallocation = location;

	private void SaveToolStripMenuItem_Click(object sender, EventArgs e) => graph.SaveGraph();

	private void ResetSizeToolStripMenuItem_Click(object sender, EventArgs e) => Reset();

	public void Reset() {
		WindowState = FormWindowState.Normal;
		Size = initialSize;
		Location = initiallocation;
	}

	private void PB_DoubleClick(object sender, EventArgs e) => this.WindowState = this.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
}
