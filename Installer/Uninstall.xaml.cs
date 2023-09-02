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

        void ExecUninstall(object sender, RoutedEventArgs e)
        {
            if(mainWindow.config.applicationName == "")
                return;
        
            string localAppdataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if(mainWindow.InstallDir != localAppdataPath){
                CopyFiles.DeleteDirectory(mainWindow.InstallDir);
            }

            string startMenuProgramsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs";
            string appDir = startMenuProgramsPath + "\\" + mainWindow.config.applicationName;
            CopyFiles.DeleteDirectory(appDir);

            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\{mainWindow.config.applicationName}.lnk";
            if (File.Exists(shortcutPath))
                File.Delete(shortcutPath);

            UninstalledMsg.Visibility = Visibility.Visible;
            UninstallMsg.Visibility = Visibility.Collapsed;
            CloseButton.Visibility = Visibility.Visible;
            UninstallButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Collapsed;
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
