using MvvmDialogs;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HanabiCompanion.Data;
using HanabiCompanion.Data.Models;
using HanabiCompanion.Data.Repositories;
using HanabiCompanion.UI.Dialogs;
using HanabiCompanion.UI.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HanabiCompanion.UI.ViewModels
{
    public class SetupGameViewModel : BaseViewModel
    {
        private SettingsService _settingsService;
        private IDialogService _dialogService;
        private PlayerRepository _playerRepo;

        private ObservableCollection<Player> _allPlayers;
        public ObservableCollection<Player> allPlayers
        {
            get { return _allPlayers; }
            set
            {
                _allPlayers = value;
                OnPropertyChanged(nameof(allPlayers));
            }
        }

        private ICommand _startGameCommand;
        public ICommand startGameCommand
        {
            get
            {
                if (_startGameCommand == null)
                {
                    _startGameCommand = new Command(StartGame);
                }
                return _startGameCommand;
            }
            set { _startGameCommand = value; }
        }

        private ICommand _createPlayerCommand;
        public ICommand createPlayerCommand
        {
            get
            {
                if (_createPlayerCommand == null)
                {
                    _createPlayerCommand = new Command(CreatePlayer);
                }
                return _createPlayerCommand;
            }
            set { _createPlayerCommand = value; }
        }

        private ICommand _deletePlayerCommand;
        public ICommand deletePlayerCommand
        {
            get
            {
                if (_deletePlayerCommand == null)
                {
                    _deletePlayerCommand = new Command<Player>(DeletePlayer);
                }
                return _deletePlayerCommand;
            }
            set { _deletePlayerCommand = value; }
        }

        private ICommand _editPlayerCommand;
        public ICommand editPlayerCommand
        {
            get
            {
                if (_editPlayerCommand == null)
                {
                    _editPlayerCommand = new Command<Player>(EditPlayer);
                }
                return _editPlayerCommand;
            }
            set { _editPlayerCommand = value; }
        }

        private ICommand _toggleGameSettingsPopupCommand;
        public ICommand toggleGameSettingsPopupCommand
        {
            get
            {
                if (_toggleGameSettingsPopupCommand == null)
                {
                    _toggleGameSettingsPopupCommand = new Command(ChangeGameSettings);
                }
                return _toggleGameSettingsPopupCommand;
            }
            set { _toggleGameSettingsPopupCommand = value; }
        }

        public SetupGameViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _settingsService = new SettingsService();
            _playerRepo = new PlayerRepository();

            GetAllPlayers();
        }

        private async void StartGame()
        {
            if (allPlayers.Where(p => p.selectedToPlay).Count() > 1)
            {
                ((App)Application.Current).rootFrame.Navigate(typeof(ScoreboardView), allPlayers.Where(p => p.selectedToPlay == true));
            }
            else
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Not Enough Players", "Please select at least two players to start a game"));
            }
        }

        private async void GetAllPlayers()
        {
            try
            {
                allPlayers = new ObservableCollection<Player>(_playerRepo.GetAllPlayers().OrderByDescending(p => _playerRepo.GetNumberOfGamesById(p.id)));
            }
            catch (SQLiteException)
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Error", "Something went wrong, please try again"));
            }
        }

        private async void CreatePlayer()
        {
            try
            {
                TextEntryDialogViewModel dialogViewModel = new TextEntryDialogViewModel("Create New Player", "Enter a name for the new player", "Player Name");
                ContentDialogResult result = await _dialogService.ShowContentDialogAsync(dialogViewModel);

                if (result == ContentDialogResult.Primary)
                {
                    if (!_playerRepo.PlayerExists(dialogViewModel.input))
                    {
                        _playerRepo.AddPlayer(new Player { name = dialogViewModel.input });

                        IEnumerable<Player> newAllPlayers;

                        newAllPlayers = _playerRepo.GetAllPlayers();

                        foreach (Player player in newAllPlayers)
                        {
                            if (allPlayers.SingleOrDefault(p => p.id == player.id) == null)
                            {
                                player.selectedToPlay = true;
                                allPlayers.Add(player);
                            }
                        }
                    }
                    else
                    {
                        await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Player Already Exists", "A player with that name already exists in the database. Please choose a new name"));
                    }
                }
            }
            catch (SQLiteException)
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Error", "Something went wrong, please try again"));
            }
        }

        private async void DeletePlayer(Player playerToDelete)
        {
            try
            {
                ContentDialogResult result = await _dialogService.ShowContentDialogAsync(new ConfirmDialogViewModel("Are You Sure?", string.Format("Permanently delete {0} from the database?", playerToDelete.name)));

                if (result == ContentDialogResult.Primary)
                {
                    _playerRepo.DeletePlayerById(playerToDelete.id);

                    IEnumerable<Player> newAllPlayers;

                    newAllPlayers = _playerRepo.GetAllPlayers();

                    for (int i = allPlayers.Count() - 1; i > -1; i--)
                    {
                        if (newAllPlayers.SingleOrDefault(p => p.id == allPlayers[i].id) == null)
                        {
                            allPlayers.RemoveAt(i);
                        }
                    }
                }
            }
            catch (SQLiteException)
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Error", "Something went wrong, please try again"));
            }
        }

        private async void EditPlayer(Player playerToEdit)
        {
            try
            {
                TextEntryDialogViewModel dialogViewModel = new TextEntryDialogViewModel("Edit Player", string.Format("Enter a new name for {0}", playerToEdit.name), playerToEdit.name);
                ContentDialogResult result = await _dialogService.ShowContentDialogAsync(dialogViewModel);

                if (result == ContentDialogResult.Primary)
                {
                    if (!_playerRepo.PlayerExists(dialogViewModel.input))
                    {
                        playerToEdit.name = dialogViewModel.input;
                        _playerRepo.EditPlayer(playerToEdit);
                    }
                    else
                    {
                        await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Player Already Exists", "A player with that name already exists in the database. Please choose a new name"));
                    }
                }
            }
            catch (SQLiteException)
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Error", "Something went wrong, please try again"));
            }
        }

        private async void ChangeGameSettings()
        {
            GameOptionsDialogViewModel dialogViewModel = new GameOptionsDialogViewModel("Game Settings", Convert.ToBoolean(_settingsService.GetMulticolour()), Convert.ToBoolean(_settingsService.GetMulticolourIsWild()));
            ContentDialogResult result = await _dialogService.ShowContentDialogAsync(dialogViewModel);

            if (result == ContentDialogResult.Primary)
            {
                _settingsService.SetMulticolour(dialogViewModel.multicolour.ToString());
                _settingsService.SetMulticolourIsWild(dialogViewModel.multicolourIsWild.ToString());
            }
        }
    }
}
