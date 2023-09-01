using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Installer
{
    public partial class WhereInstall : Page
    {
        MainWindow mainWindow = null!;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null && e.Parameter != null){
                mainWindow = (MainWindow)e.Parameter;
                InstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + mainWindow.config.applicationName;
            }
            base.OnNavigatedTo(e);
        }

        public WhereInstall()
        {
            InitializeComponent();
        }

        void NextInstall(object sender, RoutedEventArgs e)
        {
            mainWindow.InstallDir = InstallDir.Text;
            mainWindow.MainFrame.Navigate(typeof(InstallOption), mainWindow);
        }

        void BackInstall(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(typeof(Agreement), mainWindow);
        }

        async void OpenInstallDir(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(mainWindow);
            FolderPicker picker = new Windows.Storage.Pickers.FolderPicker();
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);
            picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add("*");
            StorageFolder folder = await picker.PickSingleFolderAsync();
            InstallDir.Text = folder.Path;
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
