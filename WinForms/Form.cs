using Parser.Analog;
using Parser.Mathematical;

namespace WinForms {
    public partial class Form : System.Windows.Forms.Form {

        List<Graph> graphs = new();
        public Form() {
            InitializeComponent();

            try {
                MathParser mathParser = new();
                mathParser.Parse("x^4");
                double a = -1, b = 1, delta = 0.001;

#if DEBUG
               using(StreamWriter SW = new("1.txt"))
                   for(double i = a; i <= b; i += delta)
                       SW.Write($"{i};{Math.Pow(i,4)}\n");
#endif         

                graphs.Add(new(PB_1, mathParser.Evaluate, a, b, delta));
                graphs.Add(new(PB_2, mathParser.Evaluate, a, b, delta));
                graphs.Add(new(PB_3, mathParser.Evaluate, a, b, delta));
                graphs.Add(new(PB_4, mathParser.Evaluate, a, b, delta));
                graphs.Add(new(PB_5, mathParser.Evaluate, a, b, delta));
                graphs[0].Visualize(Graph.Method.left_rectangle);
                graphs[1].Visualize(Graph.Method.right_rectangle);
                graphs[2].Visualize(Graph.Method.midpoint_rectangle);
                graphs[3].Visualize(Graph.Method.trapezoid);
                graphs[4].Visualize(Graph.Method.simpson);

                AnalogParser analogParser = new("1.txt");

                graphs.Add(new(PB_6, analogParser, delta));
                graphs.Add(new(PB_7, analogParser, delta));
                graphs.Add(new(PB_8, analogParser, delta));
                graphs.Add(new(PB_9, analogParser, delta));
                graphs.Add(new(PB_10, analogParser, delta));
                graphs[5].Visualize(Graph.Method.left_rectangle);
                graphs[6].Visualize(Graph.Method.right_rectangle);
                graphs[7].Visualize(Graph.Method.midpoint_rectangle);
                graphs[8].Visualize(Graph.Method.trapezoid);
                graphs[9].Visualize(Graph.Method.simpson);

            } catch(Exception e) {
                MessageBox.Show(e.ToString());
                throw;
            }

        }

        private void PB_Click(object sender, EventArgs e) {
            foreach(var item in graphs) {
                if((sender as PictureBox).Name == item.PB.Name) {
                    item.ReDraw();
                    return;
                }
            }

        }
    }
}