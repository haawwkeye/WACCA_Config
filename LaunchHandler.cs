using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAnalysis = System.Diagnostics.CodeAnalysis;
// ScreenRotation stuff
using static WACCA_Config.ScreenRotation;
using Orientation = WACCA_Config.ScreenRotation.Orientation;

namespace WACCA_Config
{
    public class LaunchHandler
    {
        // Processes
        internal static Process? WACVR;
        internal static Process? WACCA;

        private static uint? uDisplay; // The display that WACCA is on

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

            //Rotate(uDisplay, (Orientation)OrientationLang.Portrait);
            HandleMercury();

            WACVR = Process.Start(startInfo);

            Task.Factory.StartNew(() =>
            {
                // TODO: Hide Form
                WACVR?.WaitForExit(-1);
                WACVR = null;
                WACCA?.Kill(true);
                if (uDisplay != null) Rotate((uint)uDisplay, (Orientation)OrientationLang.Landscape);
                uDisplay = null; // uDisplay is no longer needed since we will get it next time it starts
            });
        }

        public static void OnMercuryDeath()
        {
            // TODO: Add error detection based on if WACCA closed due to fatal error (if this is possible)
            WACCA = null; // Process killed (and we don't need this for now)
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

        public static uint? GetDisplayNumberFromScreen(Screen screen)
        {
            uint? sceenId = uint.Parse(screen.DeviceName.Split("DISPLAY")[1]);

            Debug.WriteLine(screen.DeviceName);

            return sceenId;
        }

        public static void HandleMercury()
        {
            // Start a new task
            Task.Factory.StartNew(async () =>
            {
                // Wait for WACCA to start
                WACCA = await WaitForMercury();
                // Set the Display to the one WACCA is using
                uDisplay = GetDisplayNumberFromScreen(Screen.FromHandle(WACCA.MainWindowHandle)); // This should be the uDisplay
                Debug.WriteLine(uDisplay); // DEBUG
                // TODO: Fullscreen just incase (idk how to do this currently but it should be possible)
                // Rotate to Portrait since you HAVE to play in Portrait
                if (uDisplay != null) Rotate((uint)uDisplay, (Orientation)OrientationLang.Portrait);
                Debug.WriteLine(WACCA.Id);
                // Wait for exit so we can fast restart (might make this a setting instead)
                WACCA.WaitForExit(-1);
                Debug.WriteLine("WACCA Died!");
                OnMercuryDeath();
            });
        }

        public static Task<Process> WaitForMercury()
        {
            // Start a new task
            return Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Waiting for Mercury...");

                // Repeat this line until you get processes with this name
                while (Process.GetProcessesByName("Mercury-Win64-Shipping").Length == 0) { Thread.Sleep(100); }

                // Define the first one as the process
                return Process.GetProcessesByName("Mercury-Win64-Shipping")[0]; ;
            });
        }
    }
}
