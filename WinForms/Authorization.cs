namespace AuthorizationForm {
    public partial class Authorization : Form {
        public Authorization() {
            InitializeComponent();
        }

        public bool logInSuccessful { get; private set; }

        private void B_LogIn_Click(object sender, EventArgs e) {
            //TODO �������� �������� ����������� � ����������� ������������
            logInSuccessful = true;
            this.Close();
        }
    }
}