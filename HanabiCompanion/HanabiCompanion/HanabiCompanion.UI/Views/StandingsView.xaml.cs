using HanabiCompanion.Data.Models;
using HanabiCompanion.UI.ViewModels;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using System;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HanabiCompanion.UI.Views
{
    public sealed partial class StandingsView : Page
    {
        public StandingsView()
        {
            InitializeComponent();

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                StatusBar.SetIsVisible(this, false);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new StandingsViewModel((Game)e.Parameter);

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += ScoreboardView_BackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void ScoreboardView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;

            ((App)Application.Current).rootFrame.Navigate(typeof(MainMenuView));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested -= ScoreboardView_BackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
    }
}
