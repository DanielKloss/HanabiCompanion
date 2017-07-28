using HanabiCompanion.Data.Models;
using HanabiCompanion.Data.Repositories;
using HanabiCompanion.UI.Dialogs;
using HanabiCompanion.UI.Models;
using HanabiCompanion.UI.Views;
using MvvmDialogs;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HanabiCompanion.UI.ViewModels
{
    public class ScoreboardViewModel : BaseViewModel
    {
        private GameRepository _gameRepo;
        private PlayerGameRepository _playerGameRepo;
        private SettingsService _settingsService;
        private IDialogService _dialogService;

        private Game _game;
        public Game game
        {
            get { return _game; }
            set
            {
                _game = value;
                OnPropertyChanged(nameof(game));
            }
        }

        private ObservableCollection<SuitColour> _colours;
        public ObservableCollection<SuitColour> colours
        {
            get { return _colours; }
            set
            {
                _colours = value;
                OnPropertyChanged(nameof(colours));
            }
        }

        private int _livesLost;
        public int livesLost
        {
            get { return _livesLost; }
            set
            {
                _livesLost = value;
                OnPropertyChanged(nameof(livesLost));
            }
        }

        private ICommand _toggleLifeLostCommand;
        public ICommand toggleLifeLostCommand
        {
            get
            {
                if (_toggleLifeLostCommand == null)
                {
                    _toggleLifeLostCommand = new Command<bool>(toggleLifeLost);
                }
                return _toggleLifeLostCommand;
            }
            set { _toggleLifeLostCommand = value; }
        }

        private ICommand _finishGameCommand;
        public ICommand finishGameCommand
        {
            get
            {
                if (_finishGameCommand == null)
                {
                    _finishGameCommand = new Command(() => SaveGame());
                }
                return _finishGameCommand;
            }
            set { _finishGameCommand = value; }
        }

        private ICommand _cancelGameCommand;
        public ICommand cancelGameCommand
        {
            get
            {
                if (_cancelGameCommand == null)
                {
                    _cancelGameCommand = new Command(EndGame);
                }
                return _cancelGameCommand;
            }
            set { _cancelGameCommand = value; }
        }

        public ScoreboardViewModel(IEnumerable<Player> players)
        {
            _dialogService = new DialogService();
            _settingsService = new SettingsService();
            _gameRepo = new GameRepository();
            _playerGameRepo = new PlayerGameRepository();

            game = new Game();
            game.multicolour = Convert.ToBoolean(_settingsService.GetMulticolour());
            game.multiColourIsWild = Convert.ToBoolean(_settingsService.GetMulticolourIsWild());
            game.players = new ObservableCollection<Player>(players);
            game.dateTime = DateTime.Now;

            colours = new ObservableCollection<SuitColour>
            {
                new SuitColour("Red"),
                new SuitColour("Blue"),
                new SuitColour("Green"),
                new SuitColour("Yellow"),
                new SuitColour("White")
            };

            if (game.multicolour && !game.multiColourIsWild)
            {
                colours.Add(new SuitColour("Multi"));
            }

            foreach (Player player in game.players)
            {
                player.achievements = new ObservableCollection<Achievement>();
            }
        }

        private void toggleLifeLost(bool isChecked)
        {
            if (!isChecked)
            {
                livesLost--;
            }
            else
            {
                livesLost++;
            }
        }

        public async void EndGame()
        {
            ContentDialogResult result = await _dialogService.ShowContentDialogAsync(new ConfirmDialogViewModel("End Game", "Are you sure you want to quit the game without logging the stats?"));

            if (result == ContentDialogResult.Primary)
            {
                ((App)Application.Current).rootFrame.Navigate(typeof(MainMenuView));
            }
        }

        private async void SaveGame()
        {
            ContentDialogResult result = await _dialogService.ShowContentDialogAsync(new ConfirmDialogViewModel("End Game", "Are you sure you want to quit the game and log the stats?"));

            if (result == ContentDialogResult.Secondary)
            {
                return;
            }

            CalculateScores();

            AchievementService achievementService = new AchievementService(game);

            achievementService.CheckForPersonalScores();
            achievementService.CheckForEverScores();

            try
            {
                _gameRepo.AddGame(game);

                foreach (Player player in game.players)
                {
                    _playerGameRepo.AddPlayerGame(player, game);
                }

                achievementService.CheckForFirsts();
                achievementService.CheckForMileStones();

                achievementService.AddAchievements();

                ((App)Application.Current).rootFrame.Navigate(typeof(StandingsView), game);
            }
            catch (SQLiteException)
            {
                await _dialogService.ShowContentDialogAsync(new MessageDialogViewModel("Error", "Something went wrong, please try again"));
            }
        }

        private void CalculateScores()
        {
            foreach (Player player in game.players)
            {
                if (livesLost == 3)
                {
                    player.totalScore = 0;
                }
                else
                {
                    foreach (SuitColour suitColour in colours)
                    {
                        player.totalScore += suitColour.score;
                    }
                }

                player.livesLost = livesLost;
            }
        }
    }
}
