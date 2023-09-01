using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Installer
{
    public partial class InstallOption : Page
    {
        MainWindow mainWindow = null!;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null && e.Parameter != null)
                mainWindow = (MainWindow)e.Parameter;
            base.OnNavigatedTo(e);
        }

        public InstallOption()
        {
            InitializeComponent();
            // InstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MyApp";
        }

        void NextInstall(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(typeof(InstallOption), mainWindow);
        }

        void BackInstall(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(typeof(WhereInstall), mainWindow);
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }

        void CreateShortcutCheck(object sender, RoutedEventArgs e){
            mainWindow.createShortcut = (bool)CreateShortcut.IsChecked!;
        }
    }
}
