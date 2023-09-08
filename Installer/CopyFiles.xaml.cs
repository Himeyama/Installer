using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;
using System.IO.Abstractions;
using System.Runtime.InteropServices;
using WindowsShortcutFactory;
using System.IO;


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
                Install();
                ExitButton.IsEnabled = true;
                Installing.Text = Installed.Text;

                if (mainWindow.createShortcut == true)
                {
                    string targetPath = $"{mainWindow.InstallDir}\\{mainWindow.config.applicationName}.exe";
                    string lnkPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\{mainWindow.config.applicationName}.lnk";
                    using WindowsShortcut shortcut = new()
                    {
                        Path = targetPath,
                        Description = mainWindow.config.description
                    };

                    try
                    {
                        shortcut.Save(lnkPath);
                    }
                    catch (Exception ex)
                    {
                        File.WriteAllText("InstallError.log", $"create a shortcut in desktop: {ex.Message}");
                    }
                }

                if (mainWindow.createStartmenuDir == true)
                {
                    string startMenuProgramsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs";
                    string appDir = startMenuProgramsPath + "\\" + mainWindow.config.applicationName;
                    if (!Directory.Exists(appDir))
                        Directory.CreateDirectory(appDir);
                    string targetPath = $"{mainWindow.InstallDir}\\{mainWindow.config.applicationName}.exe";
                    string lnkPath = appDir + $"\\{mainWindow.config.applicationName}.lnk";
                    using WindowsShortcut shortcut = new()
                    {
                        Path = targetPath,
                        Description = mainWindow.config.description
                    };

                    try
                    {
                        shortcut.Save(lnkPath);
                    }
                    catch (Exception ex)
                    {
                        File.WriteAllText("InstallError.log", $"create a start menu: {ex.Message}");
                    }
                }
            }
            base.OnNavigatedTo(e);
        }

        async void Install(){
            await Task.Run(() => DeleteDirectory(mainWindow.InstallDir));
            await Task.Run(() => CopyDirectory(mainWindow.config.source, mainWindow.InstallDir));
        }

        public CopyFiles()
        {
            InitializeComponent();
        }

        bool CopyDirectory(string sourceDir, string destinationDir)
        {
            DirectoryInfo dir = new(sourceDir);

            if (!dir.Exists)
            {
                return false;
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
                // progressBar.Value = progress;
                SetProgress(progress);
                SetProgressFile(temppath);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destinationDir, subdir.Name);
                CopyDirectory(subdir.FullName, temppath);
            }

            return true;
        }

        public void SetProgress(int progress)
        {
            if (this.DispatcherQueue.HasThreadAccess)
            {
                progressBar.Value = progress;
            }
            else
            {
                bool isQueued = this.DispatcherQueue.TryEnqueue(
        Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal,
                    () =>
                    {
                        progressBar.Value = progress;
                    });
            }
        }

        public void SetProgressFile(string filename)
        {
            if (this.DispatcherQueue.HasThreadAccess)
            {
                FileName.Text = filename + "\n" + FileName.Text;
            }
            else
            {
                bool isQueued = this.DispatcherQueue.TryEnqueue(
        Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal,
                    () =>
                    {
                        FileName.Text = filename + "\n" + FileName.Text;
                    });
            }
        }

        static public bool DeleteDirectory(string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                return false;
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
            return true;
        }

        void Close(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
