using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

namespace Installer
{
    public partial class WhereInstall : Page
    {
        MainWindow mainWindow = null!;
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null && e.Parameter != null)
                mainWindow = (MainWindow)e.Parameter;
            base.OnNavigatedTo(e);
        }

        public WhereInstall()
        {
            InitializeComponent();
        }

        void NextInstall(object sender, RoutedEventArgs e){
            mainWindow.MainFrame.Navigate(typeof(WhereInstall), mainWindow);
        }
    }
}
