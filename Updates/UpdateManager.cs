using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DeckXPToolbox.Updates
{
    internal class UpdateManager
    {
        private static readonly string CurrentVersion = "1.0.0";
        private static readonly string GitHubOwner = "veygax";
        private static readonly string GitHubRepo = "DeckXPToolbox";
        private static readonly string GitHubApiUrl = $"https://api.github.com/repos/{GitHubOwner}/{GitHubRepo}/releases/latest";

        public static async Task<UpdateInfo> GetLatestUpdateInfoAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "WPFApp");

                    // Fetch the latest release information from GitHub
                    var response = await client.GetStringAsync(GitHubApiUrl);
                    var json = JObject.Parse(response);

                    var latestVersion = json["tag_name"]?.ToString();
                    var assetUrl = json["assets"]?[0]?["browser_download_url"]?.ToString();

                    if (string.IsNullOrEmpty(latestVersion) || string.IsNullOrEmpty(assetUrl))
                        return null;

                    return new UpdateInfo
                    {
                        Version = latestVersion,
                        DownloadUrl = assetUrl
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool IsNewerVersion(string latestVersion)
        {
            Version latest = new Version(latestVersion);
            Version current = new Version(CurrentVersion);
            return latest > current;
        }

        public static async Task DownloadFileAsync(string url, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fileStream);
                }
            }
        }

        public static void RunInstallerAndExit(string installerPath)
        {
            try
            {
                // Start the installer process
                Process.Start(installerPath);

                // Close the application
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start installer: {ex.Message}", "Update Error");
            }
        }
    }
}
