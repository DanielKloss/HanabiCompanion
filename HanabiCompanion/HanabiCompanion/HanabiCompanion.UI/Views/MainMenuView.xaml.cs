using Microsoft.Toolkit.Uwp.UI.Extensions;
using HanabiCompanion.UI.ViewModels;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Controls;

namespace HanabiCompanion.UI.Views
{
    public sealed partial class MainMenuView : Page
    {
        public MainMenuView()
        {
            InitializeComponent();

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                StatusBar.SetIsVisible(this, false);
            }

            DataContext = new MainMenuViewModel();
        }
    }
}
