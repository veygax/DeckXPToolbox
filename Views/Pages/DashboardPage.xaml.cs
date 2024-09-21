using DeckXPToolbox.ViewModels.Pages;
using System.Diagnostics;
using Wpf.Ui.Controls;

namespace DeckXPToolbox.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void GitHubButton_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl("https://github.com/veygax/DeckXP");
        }

        private void DiscordButton_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl("https://dsc.gg/veygax");
        }

        private static void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open URL: {ex.Message}");
            }
        }
    }
}
