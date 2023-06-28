namespace WACCA_Config
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
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
        }
    }
}