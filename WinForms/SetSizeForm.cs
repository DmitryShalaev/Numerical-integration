namespace SizeForm;
public partial class SetSizeForm : Form {

	public bool SetSizeSuccessful { get; private set; }
	public Size size { get; private set; }

	public SetSizeForm(Size size) {
		InitializeComponent();

		TB_Height.Text = size.Height.ToString();
		TB_Width.Text = size.Width.ToString();
	}

	private void B_OK_Click(object sender, EventArgs e) {
		if(!(string.IsNullOrWhiteSpace(TB_Height.Text) && string.IsNullOrWhiteSpace(TB_Width.Text))
			 && int.TryParse(TB_Height.Text, out int height) && int.TryParse(TB_Width.Text, out int width)) {

			SetSizeSuccessful = true;
			size = new(Math.Max(width, 1), Math.Max(height, 1));
			this.Close();
			return;
		}
	}

	private void B_Cancel_Click(object sender, EventArgs e) => this.Close();
}
