using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;


namespace Installer
{
    public partial class InstallOption : Page
    {
        MainWindow mainWindow = null!;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null && e.Parameter != null){
                mainWindow = (MainWindow)e.Parameter;
                CreateShortcut.IsChecked = mainWindow.createShortcut;
                CreateStartmenuDirectory.IsChecked = mainWindow.createStartmenuDir;
            }
            base.OnNavigatedTo(e);
        }

        public InstallOption()
        {
            InitializeComponent();
        }

        void NextInstall(object sender, RoutedEventArgs e)
        {
            mainWindow.createShortcut = CreateShortcut.IsChecked;
            mainWindow.createStartmenuDir = CreateStartmenuDirectory.IsChecked;
            mainWindow.MainFrame.Navigate(typeof(Confirm), mainWindow);
        }

        void BackInstall(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(typeof(WhereInstall), mainWindow);
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
