using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalysis = System.Diagnostics.CodeAnalysis;
// The Funny
using static WACCA_Config.ScreenRotation;
using Orientation = WACCA_Config.ScreenRotation.Orientation;
using System.Xml.Linq;

namespace WACCA_Config
{
    public class LaunchHandler
    {
        // Processes
        internal static Process? WACVR;
        internal static Process? WACCA;

        [CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
        public static uint uDisplay = 1; // The display that WACCA is on
        //public static LaunchHandler Instance { get; private set; }

        public static void StartWACVR()
        {
            string path = WACCA_Handler.GetPath("%WACVR%");
            if (!Directory.Exists(Path.Combine(path, "WACVR"))) return; // Not using WACVR
            string WACVRDir = Path.Combine(path, "WACVR");
            ProcessStartInfo startInfo = new()
            {
                WindowStyle = ProcessWindowStyle.Maximized,
                FileName = Path.Combine(WACVRDir, "WACVR.exe"),
                WorkingDirectory = WACVRDir
            };

            Rotate(uDisplay, (Orientation)OrientationLang.Portrait);
            HandleMercury();

            WACVR = Process.Start(startInfo);

            Task.Factory.StartNew(() =>
            {
                // TODO: Hide Form
                WACVR?.WaitForExit(-1);
                WACVR = null;
                WACCA?.Kill(true);
                Rotate(uDisplay, (Orientation)OrientationLang.Landscape);
            });
        }

        public static void OnMercuryDeath()
        {
            WACCA = null; // Process killed
            // TODO: Attempt restart
            if (WACVR == null) return; // WACVR Closed so don't attempt restart
            string WACCA_bin = WACCA_Handler.GetPath("%WACCA.bin%");
            ProcessStartInfo startInfo = new()
            {
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = Path.Combine(WACCA_bin, "start.bat"),
                WorkingDirectory = WACCA_bin
            };
            HandleMercury(); // Start waiting for Mercury again
            Process.Start(startInfo); // Start WACCA again
        }

        public static void HandleMercury()
        {
            Task.Factory.StartNew(async () =>
            {
                WACCA = await WaitForMercury();
                Debug.WriteLine(WACCA.Id);
                WACCA.WaitForExit(-1);
                Debug.WriteLine("WACCA Died!");
                OnMercuryDeath();
            });
        }

        public static Task<Process> WaitForMercury()
        {
            return Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Waiting for Mercury...");

                while (Process.GetProcessesByName("Mercury-Win64-Shipping").Length == 0) { Thread.Sleep(100); }

                Process process = Process.GetProcessesByName("Mercury-Win64-Shipping")[0]; // Get the first one

                return process;
            });
        }
    }
}
