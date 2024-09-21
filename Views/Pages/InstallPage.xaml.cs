using DeckXPToolbox.ViewModels.Pages;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using Wpf.Ui.Controls;

namespace DeckXPToolbox.Views.Pages
{
    public partial class InstallPage : INavigableView<InstallViewModel>
    {
        public InstallViewModel ViewModel { get; }

        public InstallPage(InstallViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private async void InstallSteamDeckTools_Click(object sender, RoutedEventArgs e)
        {
            const string url = "https://github.com/ayufan/steam-deck-tools/releases/download/0.7.3/SteamDeckTools-0.7.3-setup.exe";
            const string fileName = "SteamDeckTools-setup.exe";
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            // Download the file using HttpClient
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("Downloading file...");
                byte[] fileBytes = await client.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(filePath, fileBytes);
                Console.WriteLine("Download complete.");
            }

            // Run the downloaded file
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = true;
                process.Start();

                Console.WriteLine("Running the setup...");

                await process.WaitForExitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private async void InstallHandheldCompanion_Click(object sender, RoutedEventArgs e)
        {
            const string url = "https://github.com/Valkirie/HandheldCompanion/releases/download/0.21.5.4/HandheldCompanion-0.21.5.4.exe";
            const string fileName = "HandheldCompanion-setup.exe";
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            // Download the file using HttpClient
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("Downloading file...");
                byte[] fileBytes = await client.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(filePath, fileBytes);
                Console.WriteLine("Download complete.");
            }

            // Run the downloaded file
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = true;
                process.Start();

                Console.WriteLine("Running the setup...");

                await process.WaitForExitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
