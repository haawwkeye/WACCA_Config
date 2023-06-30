using main = WACCA_Config.Main;

namespace WACCA_Config
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            while (main.Instance == null) { Thread.Sleep(100); }
            Application.Run(main.Instance);
        }
    }
}