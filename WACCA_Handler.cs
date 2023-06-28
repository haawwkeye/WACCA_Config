using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WACCA_Config.Properties;
// API
using Microsoft.WindowsAPICodePack.Dialogs;
using Yuu.Ini;
// UAssetAPI
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.UnrealTypes;
using UAssetAPI.PropertyTypes.Structs;

namespace WACCA_Config
{
    internal class WACCA_Handler
    {
        private static readonly Ping Ping = new();
        private static readonly CommonOpenFileDialog selectFolderDialog = new()
        {
            IsFolderPicker = true
        };

        // WACCA Version
        private static readonly EngineVersion UnrealVersion = EngineVersion.VER_UE4_19;

        #region Settings
        public static string WACCAPath
        {
            get => Settings.Default.WACCAPath;
            set
            {
                Settings.Default.WACCAPath = value;
                Settings.Default.Save();
            }
        }
        public static string ServerIP {
            get => Settings.Default.ServerIP;
            set {
                Settings.Default.ServerIP = value;
                Settings.Default.Save();
            }
        }
        public static bool OfflineMode {
            get => Settings.Default.OfflineMode;
            set {
                Settings.Default.OfflineMode = value;
                Settings.Default.Save();
            }
        }
        public static bool CustomSongEnabled {
            get => Settings.Default.CustomSongEnabled;
            set {
                Settings.Default.CustomSongEnabled = value;
                Settings.Default.Save();
            }
        }
        #endregion

        #region WACCA
        public static string GetPath(string path)
        {
            if (WACCAPath.Trim().Length == 0) throw new ArgumentException("WACCA Path not set!");
            return path
                .Replace("%WACCA.Config%", $"{WACCAPath}\\app\\WindowsNoEditor\\Mercury\\Config")
                .Replace("%WACCA.Content%", $"{WACCAPath}\\app\\WindowsNoEditor\\Mercury\\Content")
                .Replace("%WACCA.bin%", $"{WACCAPath}\\app\\bin")
                .Replace("%WACVR%", $"{WACCAPath}\\..\\");
        }
        #endregion

        #region ConfigUI Handler
        public static void ServerIPChanged(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { ServerIP = ip; return; }
            // TODO: IP Vaildation
        }
        #endregion

        #region File Handler
        public static IniDocument ReadINIFile(string filePath)
        {
            string fullPath = GetPath(filePath);
            return IniFileHandler.ReadFile(fullPath);
        }

        public static void WriteINIFile(string filePath, IniDocument data)
        {
            string fullPath = GetPath(filePath);
            IniFileHandler.WriteFile(fullPath, data);
        }

        public static string? SelectFolder(string title = "Select a Folder")
        {
            selectFolderDialog.Title = title;
            if (selectFolderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + selectFolderDialog.FileName);
                return selectFolderDialog.FileName;
            }

            return null;
        }

        // TODO: Write an uasset Handler for Content
        #endregion

        #region Server Handler
        public static bool ServerRunning(string IP_or_URL = "127.0.0.1")
        {
            PingReply? reply = null;
            
            try
            {
                Ping.Send(IP_or_URL);
            } catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return (reply != null) && (reply.Status == IPStatus.Success);
        }
        #endregion
    }

    public class WACCASong
    {
        // TODO: Start work on adding support for the SongTable
    }
}
