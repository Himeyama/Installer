using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;


namespace Installer
{
    public partial class CopyFiles : Page
    {
        MainWindow mainWindow = null!;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e != null && e.Parameter != null)
            {
                mainWindow = (MainWindow)e.Parameter;
                DeleteDirectory(mainWindow.InstallDir);
                CopyDirectory(mainWindow.config.source, mainWindow.InstallDir);
            }
            base.OnNavigatedTo(e);
        }

        public CopyFiles()
        {
            InitializeComponent();
        }

        void CopyDirectory(string sourceDir, string destinationDir)
        {
            DirectoryInfo dir = new(sourceDir);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDir);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destinationDir);

            FileInfo[] files = dir.GetFiles();
            int iItem = 0;
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destinationDir, file.Name);
                iItem++;
                file.CopyTo(temppath, false);
                int progress = iItem / files.Length * 100;
                progressBar.Value = progress;
                FileName.Text = temppath + "\n" + FileName.Text;
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destinationDir, subdir.Name);
                CopyDirectory(subdir.FullName, temppath);
            }
        }

        void DeleteDirectory(string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                return;
            }

            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, false);

            ExitButton.IsEnabled = true;
            Installing.Text = Installed.Text;
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
