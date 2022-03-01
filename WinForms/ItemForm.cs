using Graphs;
using Parser.Analog;

namespace ItemForms {
    public partial class Item : Form {
        Graph? graph;
        Graph? relocatable;
        Point mousePosition;
        Point cameraPosition;
        public Item() {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(PB_MouseWheel);
        }

        public void Drow(Graph.ParserFunc func, double a, double b, double delta, Graph.Method method) {
            this.Show();
            graph = new(PB, func, a, b, delta);
            graph.Visualize(method);
        }

        public void Drow(AnalogParser parser, double delta, Graph.Method method) {
            Drow(parser.Interpolate, parser.LeftBorder, parser.RightBorder, delta, method);
        }

        private void PB_MouseMove(object sender, MouseEventArgs e) {
            if(relocatable != null) {
                Point point = new Point(cameraPosition.X+(mousePosition.X - Control.MousePosition.X),
                                        cameraPosition.Y+(mousePosition.Y - Control.MousePosition.Y));
                relocatable.camera.Position = point;
                relocatable.ReDraw();
            }
        }

        private void PB_MouseDown(object sender, MouseEventArgs e) {
            mousePosition = Control.MousePosition;
            cameraPosition = graph.camera.Position;
            relocatable = graph;
            return;
        }

        private void PB_MouseUp(object sender, MouseEventArgs e) {
            relocatable = null;
        }

        void PB_MouseWheel(object? sender, MouseEventArgs e) {
            if(e.Delta > 0) {
                graph.camera.Zoom += 0.5;
            } else {
                graph.camera.Zoom -= 0.5;
            }
            graph.ReDraw();
        }
    }
}
