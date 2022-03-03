using Graphs;
using Parser.Analog;

namespace ItemForms {
    public partial class Item : Form {
        Graph? graph;
        public Item() {
            InitializeComponent();
        }

        public void Drow(Graph.ParserFunc func, double a, double b, double delta, Graph.Method method) {
            this.Show();
            graph = new(PB, func, a, b, delta);
            graph.Visualize(method);
        }

        public void Drow(AnalogParser parser, double delta, Graph.Method method) {
            Drow(parser.Interpolate, parser.LeftBorder, parser.RightBorder, delta, method);
        }

   
    }
}
