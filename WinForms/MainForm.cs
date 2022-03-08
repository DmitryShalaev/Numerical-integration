using Graphs;
using Manager;
using Parser.Analog;
using Parser.Mathematical;

namespace WinForms {
	public partial class MainForm : System.Windows.Forms.Form {
		WindowManager windowManager;

		private double a;
		private double b;
		private double delta { get { return double.Parse(TB_Delta.Text.Replace('.', ',')); } }

		private Graph.ParserFunc? parserFunc;

		public MainForm() {
			InitializeComponent();

			this.Location = new((System.Windows.Forms.Screen.GetWorkingArea(this).Width / 2) - (this.Width / 2),
								(System.Windows.Forms.Screen.GetWorkingArea(this).Height / 2) + (this.Height / 2));

			windowManager = new(this, CLB_Methods);

			toggleALLToolStripMenuItem_Click("Enable ALL", EventArgs.Empty);
			
#if !DEBUG
			#region Launching the authorization window
			AuthorizationForm.Authorization authForm = new();
			authForm.FormClosing += new FormClosingEventHandler(AuthorizationFormClosing);
			authForm.ShowDialog();
			#endregion
#endif
		}

		private void ParseFunction() {
			try {
				if(!string.IsNullOrWhiteSpace(TB_A.Text) && !string.IsNullOrWhiteSpace(TB_B.Text) &&
					!string.IsNullOrWhiteSpace(TB_MathFunc.Text) && delta > 0) {
					MathParser mathParser = new();

					mathParser.Parse(TB_A.Text.Replace(".", ","));
					a = mathParser.Evaluate();

					mathParser.Parse(TB_B.Text.Replace(".", ","));
					b = mathParser.Evaluate();

					if(!(a < b))
						throw new Exception("The lower limit must be less than the upper");

					mathParser.Parse(TB_MathFunc.Text.Replace(".", ","));
					parserFunc = mathParser.Evaluate;

					windowManager.Refresh(parserFunc, a, b, delta);

					B_Update.Visible = false;
				} else {
					throw new Exception("The lower limit, upper limit, and math fields must be filled in " +
									"and Delta must be greater than zero");
				}
			} catch(Exception e) {
				MessageBox.Show(e.Message.ToString());
			}
		}

		private void B_Calculate_Click(object sender, EventArgs e) {
			ParseFunction();
		}

		private void TB_MathFunc_KeyPress(object sender, KeyPressEventArgs e) {
			if(e.KeyChar == ((char)Keys.Enter)) {
				ParseFunction();
			}
		}

		private void B_LoadFile_Click(object sender, EventArgs e) {
			try {
				using(OpenFileDialog openFileDialog = new OpenFileDialog()) {
					openFileDialog.Filter = "(*.txt *.csv)|*.txt;*.csv";
					if(openFileDialog.ShowDialog() == DialogResult.OK) {
						using(StreamReader reader = new StreamReader(openFileDialog.OpenFile())) {
							AnalogParser analogParser = new(openFileDialog.FileName);

							a = analogParser.LeftBorder;
							b = analogParser.RightBorder;

							parserFunc = analogParser.Interpolate;

							windowManager.Refresh(parserFunc, a, b, delta);

							B_Update.Visible = true;
						}
					}
				}
			} catch(Exception ex) {
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void AuthorizationFormClosing(object? sender, FormClosingEventArgs e) {
			if(!(sender as AuthorizationForm.Authorization).logInSuccessful)
				this.Close();
		}

		private void CLB_Methods_ItemCheck(object sender, ItemCheckEventArgs e) {
			if(e.NewValue == CheckState.Unchecked) {
				windowManager.Remove((Graph.Method)e.Index);
			} else {
				windowManager.Add((Graph.Method)e.Index);
				if(parserFunc != null) {
					windowManager.Refresh(parserFunc, a, b, delta, (Graph.Method)e.Index);
				}
			}
		}

		private void B_Update_Click(object sender, EventArgs e) {
			if(parserFunc != null)
				windowManager.Refresh(parserFunc, a, b, delta);
		}

		private void toggleALLToolStripMenuItem_Click(object sender, EventArgs e) {
			for(int i = 0; i < CLB_Methods.Items.Count; i++)
				CLB_Methods.SetItemChecked(i, sender.ToString() == "Enable ALL" ? true : false);
		}

		private void resetALLToolStripMenuItem_Click(object sender, EventArgs e) {
			windowManager.ResetAll();
		}
	}
}