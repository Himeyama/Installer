using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;


namespace Installer
{
    public partial class Uninstall : Page
    {
        MainWindow mainWindow = null!;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null && e.Parameter != null){
                mainWindow = (MainWindow)e.Parameter;
            }
            base.OnNavigatedTo(e);
        }

        public Uninstall()
        {
            InitializeComponent();
        }

        async void ExecUninstall(object sender, RoutedEventArgs e)
        {
            await ExecUninstallCore();
        }
        
        async Task<bool> ExecUninstallCore()
        {
            if(mainWindow.config.applicationName == "")
                return false;
        
            string localAppdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if(mainWindow.InstallDir != localAppdataPath){
                await Task.Run(() => CopyFiles.DeleteDirectory(mainWindow.InstallDir));
            }

            string startMenuProgramsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs";
            string appDir = startMenuProgramsPath + "\\" + mainWindow.config.applicationName;
            
            await Task.Run(() => CopyFiles.DeleteDirectory(appDir));

            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\{mainWindow.config.applicationName}.lnk";
            if (File.Exists(shortcutPath))
                File.Delete(shortcutPath);

            UninstalledMsg.Visibility = Visibility.Visible;
            UninstallMsg.Visibility = Visibility.Collapsed;
            CloseButton.Visibility = Visibility.Visible;
            UninstallButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
            return true;
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
