using AuthorizationForm;
using Graphs;
using Parser.Analog;
using Parser.Mathematical;

namespace WinForms {
    public partial class MainForm : System.Windows.Forms.Form {

        MathParser? mathParser;
        double a = 0, b = 0, delta = 0;

        public MainForm() {
            #region Main window customization
            InitializeComponent();

            this.Location = new((System.Windows.Forms.Screen.GetWorkingArea(this).Width / 2) - (this.Width / 2),
                                 System.Windows.Forms.Screen.GetWorkingArea(this).Height - this.Height);

            CLB_Methods.SetItemChecked(0, true);
            #endregion

#if !DEBUG
            #region Launching the authorization window
            Authorization authForm = new Authorization();
            authForm.FormClosing += new FormClosingEventHandler(AuthorizationFormClosing);
            authForm.ShowDialog();
            #endregion
#endif

            try {



            } catch(Exception e) {
                MessageBox.Show(e.ToString());
                throw;
            }

        }

        private void Calculat() {
            try {
                if(TB_A.Text != "" && TB_B.Text != "" && TB_MathFunc.Text != "" && double.Parse(TB_Delta.Text) > 0) {
                    mathParser = new();

                    mathParser.Parse(TB_A.Text);
                    a = mathParser.Evaluate();

                    mathParser.Parse(TB_B.Text);
                    b = mathParser.Evaluate();

                    delta = double.Parse(TB_Delta.Text);

                    mathParser.Parse(TB_MathFunc.Text);

                    ItemForms.Item itemForms = new();
                    itemForms.Drow(mathParser.Evaluate, a, b, delta, Graph.Method.simpson);
                } else {
                    MessageBox.Show("The lower limit, upper limit, and math fields must be filled in " +
                                    "and Delta must be greater than zero");
                }
            } catch(Exception e) {
                MessageBox.Show(e.Message.ToString());
            }
        }

        private void B_Calculate_Click(object sender, EventArgs e) {
            Calculat();
        }

        private void TB_MathFunc_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == ((char)Keys.Enter)) {
                Calculat();
            }
        }

        private void B_LoadFile_Click(object sender, EventArgs e) {
            using(OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Filter = "(*.txt *.csv)|*.txt;*.csv";
                if(openFileDialog.ShowDialog() == DialogResult.OK) {
                    using(StreamReader reader = new StreamReader(openFileDialog.OpenFile())) {
                        AnalogParser analogParser = new(openFileDialog.FileName);

                        ItemForms.Item itemForms = new();
                        itemForms.Drow(analogParser, double.Parse(TB_Delta.Text), Graph.Method.simpson);
                    }
                }
            }
        }

        private void AuthorizationFormClosing(object? sender, FormClosingEventArgs e) {
            if(!(sender as Authorization).logInSuccessful)
                this.Close();
        }

        private void CLB_Methods_SelectedIndexChanged(object sender, EventArgs e) {
            MessageBox.Show(e.ToString());
        }
    }
}