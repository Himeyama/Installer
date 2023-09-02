using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;

namespace Installer
{
    public partial class Agreement : Page
    {
        MainWindow mainWindow = null!;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null && e.Parameter != null){
                mainWindow = (MainWindow)e.Parameter;
                if(mainWindow.configFileNoExistError){
                    LicenseAgreement.Visibility = Visibility.Collapsed;
                    NoExistConfigFile.Visibility = Visibility.Visible;
                    ReadFollowing.Visibility = Visibility.Collapsed;
                    ReadLicense.Visibility = Visibility.Collapsed;
                    LicenseDoc.Visibility = Visibility.Collapsed;
                    AcceptArea.Visibility = Visibility.Collapsed;
                }
            }
            base.OnNavigatedTo(e);
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }

        public Agreement()
        {
            InitializeComponent();
        }

        void NextWhereInstall(object sender, RoutedEventArgs e){
            mainWindow.MainFrame.Navigate(typeof(WhereInstall), mainWindow);
        }
    }
}
