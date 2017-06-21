using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HanabiCompanion.Data;
using HanabiCompanion.Data.Repositories;
using HanabiCompanion.UI.Models;
using HanabiCompanion.UI.Views;
using Windows.UI.Xaml;

namespace HanabiCompanion.UI.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        GameRepository _gameRepo;

        private ObservableCollection<MenuItem> _menuItems;
        public ObservableCollection<MenuItem> menuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                OnPropertyChanged(nameof(menuItems));
            }
        }

        private ICommand _navigateCommand;
        public ICommand navigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                {
                    _navigateCommand = new Command<MenuItem>(menuItem => ((App)Application.Current).rootFrame.Navigate(menuItem.destination));
                }
                return _navigateCommand;
            }
            set { _navigateCommand = value; }
        }

        public MainMenuViewModel()
        {
            _gameRepo = new GameRepository();
            _gameRepo.CreateTables();

            menuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { label = "Start", destination = typeof(SetupGameView) },
                new MenuItem { label = "Rules Reference", destination = typeof(RulesView) },
                new MenuItem { label = "Stats", destination = typeof(StatsView) },
                new MenuItem { label = "About", destination = typeof(AboutView) }
            };
        }
    }
}
