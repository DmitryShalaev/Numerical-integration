using Graphs;

namespace WindowForm {
    public partial class Window : Form {
        public Graph? graph;
        public Window() {
            InitializeComponent();
        }

        public void Show(Graph.ParserFunc func, double a, double b, double delta, Graph.Method method) {
            graph = new(PB, func, a, b, delta);
            graph.Visualize(method);

            this.Text = method.ToString().Replace('_', ' ');
            L_Answer.Text = "Answer: " + graph.answer.ans + "; Splits: " + graph.answer.number_of_splits;

            this.Show();
        }

        private void Item_SizeChanged(object sender, EventArgs e) {
            graph?.ReDraw();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            graph.SaveGraph();
        }
    }
}
