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
            if (e != null && e.Parameter != null)
                mainWindow = (MainWindow)e.Parameter;
            base.OnNavigatedTo(e);
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
