using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Svg;
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

        public Config config{ get; set; } = null!;

        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            setWindowSize(1200, 800);

            MainFrame.Navigate(typeof(Agreement), this);

            using (var sr = new StreamReader("../config/AppConfig.json"))
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
