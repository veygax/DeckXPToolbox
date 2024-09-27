using DeckXPToolbox.ViewModels.Pages;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using Wpf.Ui.Controls;

namespace DeckXPToolbox.Views.Pages
{
    public partial class UpdatesPage : INavigableView<UpdatesViewModel>
    {
        public UpdatesViewModel ViewModel { get; }

        public UpdatesPage(UpdatesViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        
    }
}
