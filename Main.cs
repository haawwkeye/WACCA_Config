namespace WACCA_Config
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            if (WACCA_Handler.WACCAPath.Length == 0) SelectPathLabel.Location = new Point(12, 49);
            ServerIP.Text = WACCA_Handler.ServerIP;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            // TODO: Do Startup stuff
            //IniDocument DefaultHardware = WACCA_Handler.ReadINIFile("%WACCA.Config%/DefaultHardware.ini");
            //Debug.WriteLine(DefaultHardware);
            LaunchHandler.StartWACVR();
            //["/Script/Mercury.MercuryNetworkSettings"].GetKeyData("t")
        }

        private void SelectPath_Click(object sender, EventArgs e)
        {
            string? path = WACCA_Handler.SelectFolder("Select WACCA Path");
            if (path == null) return;

            WACCA_Handler.WACCAPath = path;
            SelectPathLabel.Location = new Point(12, 1000);
        }

        private void ServerIP_TextChanged(object sender, EventArgs e)
        {
            if (WACCA_Handler.ServerIP != ServerIP.Text) WACCA_Handler.ServerIPChanged(ServerIP.Text);
        }
    }
}