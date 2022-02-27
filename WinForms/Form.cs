using Parser.Analog;
using Parser.Mathematical;

namespace WinForms {
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();

            try {

                MathParser parser = new();
                parser.Parse("sin(x)");
                double a = -1, b = 5, delta = 0.00001;

                Graph graph1 = new(PB_1, parser.Evaluate, a, b);
                Graph graph2 = new(PB_2, parser.Evaluate, a, b);
                Graph graph3 = new(PB_3, parser.Evaluate, a, b);
                Graph graph4 = new(PB_4, parser.Evaluate, a, b);
                Graph graph5 = new(PB_5, parser.Evaluate, a, b);
                graph1.Visualize(Graph.Method.left_rectangle,       Integral.Method.left_rectangle(parser, a, b, delta));
                graph2.Visualize(Graph.Method.right_rectangle,      Integral.Method.right_rectangle(parser, a, b, delta));
                graph3.Visualize(Graph.Method.midpoint_rectangle,   Integral.Method.midpoint_rectangle(parser, a, b, delta));
                graph4.Visualize(Graph.Method.trapezoid,            Integral.Method.simpson(parser, a, b, delta));
                graph5.Visualize(Graph.Method.simpson,              Integral.Method.simpson(parser, a, b, delta));
                
                AnalogParser analogParser = new("1.txt");
                Graph graph6 = new(PB_6, analogParser);

            } catch(Exception e) {
                MessageBox.Show(e.Message);
                throw;
            }

        }
    }
}