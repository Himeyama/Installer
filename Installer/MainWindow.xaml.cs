using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Windows.Graphics;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

using System.Globalization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Resources;


namespace Installer
{
    public sealed partial class MainWindow : Window
    {
        string LCID { get; set; } = CultureInfo.CurrentCulture.Name;
        string AppTitle { get; set; } = "";
        public string LicenseDocument { get; set; } = "";
        public string InstallDir { get; set; } = "";
        public bool? createShortcut { get; set; } = true;
        public bool? createStartmenuDir { get; set; } = true;
        public Config config { get; set; } = null!;
        public bool configFileNoExistError { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            setWindowSize(1200, 800);

            string configFile = "AppConfig.json";
            if (!File.Exists(configFile))
            {
                configFileNoExistError = true;
                MainFrame.Navigate(typeof(Agreement), this);
                return;
            }

            using (var sr = new StreamReader(configFile))
            {
                config = JsonSerializer.Deserialize<Config>(sr.ReadToEnd())!;
            }
            if (LCID == "ja-JP")
            {
                LicenseDocument = config.jaJP.Document!;
                AppTitle = config.jaJP.ApplicationName!;
            }
            else
            {
                LicenseDocument = config.enUS.Document!;
                AppTitle = config.enUS.ApplicationName!;
            }

            InstallDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + config.applicationName;
            if(Directory.Exists(InstallDir)){
                // アンインストール
                MainFrame.Navigate(typeof(Uninstall), this);
            }
            else{
                MainFrame.Navigate(typeof(Agreement), this);
            }
        }

        void setWindowSize(int width, int height)
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(myWndId);
            appWindow.Resize(new SizeInt32(width, height));
        }
    }
}
