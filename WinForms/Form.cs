using AuthorizationForm;
using Graphs;
using Parser.Mathematical;

namespace WinForms {
    public partial class Form : System.Windows.Forms.Form {

        List<Graph> graphs = new();

        public Form() {
            InitializeComponent();

#if !DEBUG
            #region Launching the authorization window
            Authorization authForm = new Authorization();
            authForm.FormClosing += new FormClosingEventHandler(AuthorizationFormClosing);
            authForm.ShowDialog();
            #endregion
#endif

            try {
                MathParser mathParser = new();
                mathParser.Parse("abs(sin(x))");
                double a = -10, b = 10, delta = 0.00001;

#if DEBUG
                using(StreamWriter SW = new("1.txt"))
                    for(double i = a; i <= b; i += delta)
                        SW.Write($"{i};{Math.Sin(i)}\n");
#endif

                //  AnalogParser analogParser = new("1.txt");


                ItemForms.Item itemForms = new();
                itemForms.Drow(mathParser.Evaluate, a, b, delta, Graph.Method.simpson);


            } catch(Exception e) {
                MessageBox.Show(e.ToString());
                throw;
            }

        }

        private void AuthorizationFormClosing(object? sender, FormClosingEventArgs e) {
            if(!(sender as Authorization).logInSuccessful)
                this.Close();
        }

    }
}