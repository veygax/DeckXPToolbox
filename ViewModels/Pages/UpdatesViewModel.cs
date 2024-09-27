using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DeckXPToolbox.Updates;

namespace DeckXPToolbox.ViewModels.Pages
{
    public class UpdatesViewModel : INotifyPropertyChanged
    {
        private string _downloadUrl;
        private string _downloadPath;
        private bool _isUpdateAvailable;
        private string _updateMessage;
        private bool _isInstallButtonEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

        public UpdatesViewModel()
        {
            CheckForUpdatesCommand = new RelayCommand(async () => await CheckForUpdates());
            InstallCommand = new RelayCommand(async () => await InstallUpdate(), () => IsInstallButtonEnabled);
        }

        public ICommand CheckForUpdatesCommand { get; }
        public ICommand InstallCommand { get; }

        public bool IsUpdateAvailable
        {
            get => _isUpdateAvailable;
            set
            {
                _isUpdateAvailable = value;
                OnPropertyChanged();
            }
        }

        public string UpdateMessage
        {
            get => _updateMessage;
            set
            {
                _updateMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsInstallButtonEnabled
        {
            get => _isInstallButtonEnabled;
            set
            {
                _isInstallButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private async Task CheckForUpdates()
        {
            try
            {

                var updateInfo = await UpdateManager.GetLatestUpdateInfoAsync();

                if (updateInfo != null && UpdateManager.IsNewerVersion(updateInfo.Version))
                {
                    _downloadUrl = updateInfo.DownloadUrl;
                    _downloadPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DeckXPToolbox-setup.exe");
                    
                    IsUpdateAvailable = true;
                    UpdateMessage = $"A new version {updateInfo.Version} is available.";
                    IsInstallButtonEnabled = true;
                }
                else
                {
                    UpdateMessage = "You are using the latest version.";
                    IsUpdateAvailable = false;
                }
            }
            catch (Exception ex)
            {
                UpdateMessage = $"Error checking for updates: {ex.Message}";
            }
        }

        private async Task InstallUpdate()
        {
            try
            {
                IsInstallButtonEnabled = false;
                
                await UpdateManager.DownloadFileAsync(_downloadUrl, _downloadPath);
                
                UpdateManager.RunInstallerAndExit(_downloadPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to download and install the update: {ex.Message}", "Update Error");
                IsInstallButtonEnabled = true;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
