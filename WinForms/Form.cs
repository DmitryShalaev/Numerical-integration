using Parser.Mathematical;

namespace WinForms {
    public partial class Form : System.Windows.Forms.Form {
        public Form() {
            InitializeComponent();

            try {

                MathParser parser = new();
                parser.Parse("sin(x)*10");

                Graph graph = new(PB_1, parser.Evaluate, -10, 10);
                graph.PlotData();

            } catch(Exception e) {
                MessageBox.Show(e.Message);
                throw;
            }

        }
    }
}