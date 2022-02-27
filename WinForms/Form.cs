using Parser.Mathematical;
using Parser.Analog;

namespace WinForms {
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();

            try {

                MathParser parser = new();
                parser.Parse("sin(x)");

                Graph graph1 = new(PB_1, parser.Evaluate, -10, 10);
                graph1.PlotData();

                AnalogParser analogParser = new("1.txt");
                Graph graph2 = new(PB_2, analogParser);
                graph2.PlotData();

            } catch(Exception e) {
                MessageBox.Show(e.Message);
                throw;
            }

        }
    }
}