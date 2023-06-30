using System;
using System.Collections.Generic;
using WACCA_Config.Properties;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int CX, int CY, int uFlags);
        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        //internal static event EventHandler? injectedEventHandler;

        // Processes
        internal static Process? WACVR;
        internal static Process? WACCA;

        private static uint? uDisplay; // The display that WACCA is on

        public static void StartWACVR()
        {
            string path = WACCA_Handler.GetPath("%WACVR%");
            if (!Directory.Exists(Path.Combine(path, "WACVR"))) return; // Not using WACVR
            Main.Instance.ToggleUI(false);
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

            WACVR?.WaitForExit(-1);
            WACVR = null;
            WACCA?.Kill(true);
            if (uDisplay != null) Rotate((uint)uDisplay, (Orientation)OrientationLang.Landscape);
            uDisplay = null; // uDisplay is no longer needed since we will get it next time it starts
            Main.Instance.ToggleUI(true);
        }

        public static void OnMercuryDeath()
        {
            // TODO: Add error detection based on if WACCA closed due to fatal error (if this is possible)
            WACCA = null; // Process killed (and we don't need this for now)
            if (WACVR == null) return; // WACVR Closed so don't attempt restart
            // TODO: Make restart/kill thing an option
            WACVR?.Close(); // Kill WACCA!
            //string WACCA_bin = WACCA_Handler.GetPath("%WACCA.bin%");
            //ProcessStartInfo startInfo = new()
            //{
            //    WindowStyle = ProcessWindowStyle.Normal,
            //    FileName = Path.Combine(WACCA_bin, "start.bat"),
            //    WorkingDirectory = WACCA_bin
            //};
            //HandleMercury(); // Start waiting for Mercury again
            //Process.Start(startInfo); // Start WACCA again
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
                //Process injected = await WaitForInject();

                //injected.OutputDataReceived += Injected_OutputDataReceived;
                //injectedEventHandler.

                //WACCA.BeginOutputReadLine();
                //Debug.WriteLine(WACCA.StandardOutput.ReadToEnd());

                //StreamReader outputStream = WACCA.StandardOutput;
                // Set the Display to the one WACCA is using
                uDisplay = GetDisplayNumberFromScreen(Screen.FromHandle(WACCA.MainWindowHandle)); // This should be the uDisplay
                Debug.WriteLine(uDisplay); // DEBUG
                // TODO: Add options to switch between the two methods plus options to config the methods
                if (Settings.Default.FullscreenMode)
                {
                    // fullscreen method
                    // TODO: Fullscreen just incase (idk how to do this currently but it should be possible)
                    // Rotate to Portrait since you HAVE to play in Portrait
                    if (uDisplay != null) Rotate((uint)uDisplay, (Orientation)OrientationLang.Portrait);
                } else {
                    // Non fullscreen method
                    //outputStream.
                    Thread.Sleep(10000); // Wait 10 seconds before running this
                    MoveWindow(WACCA.MainWindowHandle, 0, 0, 540, 960, true);

                    int style = GetWindowLong(WACCA.MainWindowHandle, -16);
                    _=SetWindowLong(WACCA.MainWindowHandle, -16, style & ~(0x00800000 | 0x00400000 | 0x00040000));
                    int exstyle = GetWindowLong(WACCA.MainWindowHandle, -20);
                    _=SetWindowLong(WACCA.MainWindowHandle, -20, exstyle & ~(0x00000001 | 0x00000200 | 0x00020000));
                    SetWindowPos(WACCA.MainWindowHandle, IntPtr.Zero, 0, 0, 0, 0, 0x0020 | 0x0002 | 0x0001 | 0x0004 | 0x0200);
                }
                
                Debug.WriteLine(WACCA.Id);
                // Wait for exit so we can fast restart (might make this a setting instead)
                WACCA.WaitForExit(-1);
                Debug.WriteLine("WACCA Died!");
                OnMercuryDeath();
            });
        }

        //private static void Injected_OutputDataReceived(object sender, DataReceivedEventArgs e)
        //{
        //    Debug.WriteLine($"Event Fired!\n Data: {e.Data}");
        //    //injectedEventHandler(sender, e);
        //}

        //public static Task<Process> WaitForInject()
        //{
        //    // Start a new task
        //    return Task.Factory.StartNew(() =>
        //    {
        //        Debug.WriteLine("Waiting for inject...");

        //        // Repeat this line until you get processes with this name
        //        while (Process.GetProcessesByName("inject").Length != 2) { Thread.Sleep(100); }
        //        Process[] list = Process.GetProcessesByName("inject");

        //        if (list[0].StartTime > list[1].StartTime) return list[0];
        //        return list[1];
        //    });
        //}

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
