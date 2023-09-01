using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;


namespace Installer
{
    public partial class Confirm : Page
    {
        MainWindow mainWindow = null!;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null && e.Parameter != null){
                mainWindow = (MainWindow)e.Parameter;
                CreateShortcut.Text = (bool)mainWindow.createShortcut! ? Yes.Text : No.Text;
                CreateStartmenuDirectory.Text = (bool)mainWindow.createStartmenuDir! ? Yes.Text : No.Text;
                InstallDestination.Text = mainWindow.InstallDir;
            }
            base.OnNavigatedTo(e);
        }

        public Confirm()
        {
            InitializeComponent();
        }

        void NextInstall(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(typeof(CopyFiles), mainWindow);
        }

        void BackInstall(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(typeof(InstallOption), mainWindow);
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
