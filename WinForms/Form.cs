using Parser.Analog;
using Parser.Mathematical;

namespace WinForms {
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();

            try {

                MathParser mathParser = new();
                mathParser.Parse("cos(x)");
                double a = 0, b = 10, delta = 0.001;

                Graph graph1 = new(PB_1, mathParser, a, b, delta);
                Graph graph2 = new(PB_2, mathParser, a, b, delta);
                Graph graph3 = new(PB_3, mathParser, a, b, delta);
                Graph graph4 = new(PB_4, mathParser, a, b, delta);
                Graph graph5 = new(PB_5, mathParser, a, b, delta);
                graph1.Visualize(Graph.Method.left_rectangle);
                graph2.Visualize(Graph.Method.right_rectangle);
                graph3.Visualize(Graph.Method.midpoint_rectangle);
                graph4.Visualize(Graph.Method.trapezoid);
                graph5.Visualize(Graph.Method.simpson);
                
                AnalogParser analogParser = new("1.txt");

                Graph graph6 = new(PB_6, analogParser, delta);
                Graph graph7 = new(PB_7, analogParser, delta);
                Graph graph8 = new(PB_8, analogParser, delta);
                Graph graph9 = new(PB_9, analogParser, delta);
                Graph graph10 = new(PB_10, analogParser, delta);
                graph6.Visualize(Graph.Method.left_rectangle);
                graph7.Visualize(Graph.Method.right_rectangle);
                graph8.Visualize(Graph.Method.midpoint_rectangle);
                graph9.Visualize(Graph.Method.trapezoid);
                graph10.Visualize(Graph.Method.simpson);

            } catch(Exception e) {
                MessageBox.Show(e.Message);
                throw;
            }

        }
    }
}