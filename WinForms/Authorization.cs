using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AuthorizationForm;
public partial class Authorization : Form {
	public bool logInSuccessful { get; private set; }

	private readonly Dictionary<string, byte[]> users;
	private readonly Regex regEx = new(@"^[0-9A-z-_]+");

	private readonly string Personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

	public Authorization() {
		try {
			using(StreamReader SR = new(Personal + "\\users.us"))
				users = JsonSerializer.Deserialize<Dictionary<string, byte[]>>(SR.ReadToEnd()) ?? new();

		} catch(FileNotFoundException) {
			users = new();
		}

		InitializeComponent();
	}

	private void B_LogIn_Click(object sender, EventArgs e) {
		if(regEx.Match(TB_Login.Text).Success && regEx.Match(TB_Password.Text).Success) {
			if(users.ContainsKey(TB_Login.Text)) {
				byte[] pass = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(TB_Password.Text));
				byte[] userPass = users[TB_Login.Text];
				bool success = true;
				if(userPass.Length == pass.Length) {
					for(int i = 0; i < pass.Length; i++) {
						if(pass[i] != userPass[i]) {
							success = false;
							break;
						}
					}
					if(success) {
						logInSuccessful = true;
						this.Close();
						return;
					}
				}
			}
		}
		TB_Password.Text = TB_Login.Text = "";

		MessageBox.Show("Login Error");
	}

	private void B_SingUp_Click(object sender, EventArgs e) {

		if(regEx.Match(TB_Login.Text).Success && regEx.Match(TB_Password.Text).Success) {
			if(users.ContainsKey(TB_Login.Text)) {
				TB_Password.Text = "";
				TB_Login.Text = "";

				MessageBox.Show("Login already in use");
			} else {
				users.Add(TB_Login.Text, MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(TB_Password.Text)));
				TB_Password.Text = "";

				using(StreamWriter SW = new(Personal + "\\users.us"))
					SW.WriteLine(JsonSerializer.Serialize(users));

				MessageBox.Show("Successful registration");
			}
			return;
		}
		TB_Password.Text = TB_Login.Text = "";

		MessageBox.Show("Sing Up Error");
	}

	private void TB_KeyPress(object sender, KeyPressEventArgs e) {
		if(e.KeyChar == (char)Keys.Enter) {
			B_LogIn_Click(sender, e);
		}
	}
}