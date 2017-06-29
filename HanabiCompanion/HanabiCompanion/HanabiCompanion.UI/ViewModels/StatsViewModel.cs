using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HanabiCompanion.Data;
using HanabiCompanion.Data.Models;
using HanabiCompanion.Data.Repositories;
using HanabiCompanion.UI.Models;
using HanabiCompanion.UI.Views;
using Windows.UI.Xaml;

namespace HanabiCompanion.UI.ViewModels
{
    public class StatsViewModel : BaseViewModel
    {
        private GameRepository _gameRepo;
        private PlayerRepository _playerRepo;
        private AchievementRepository _achievementRepo;
        private OneDriveService _oneDriveService;

        private int _totalGames;
        public int totalGames
        {
            get { return _totalGames; }
            set
            {
                _totalGames = value;
                OnPropertyChanged(nameof(totalGames));
            }
        }

        private ObservableCollection<PlayerStat> _playerStats;
        public ObservableCollection<PlayerStat> playerStats
        {
            get { return _playerStats; }
            set
            {
                _playerStats = value;
                OnPropertyChanged(nameof(playerStats));
            }
        }

        private OverallStat _overallStats;
        public OverallStat overallStats
        {
            get { return _overallStats; }
            set
            {
                _overallStats = value;
                OnPropertyChanged(nameof(overallStats));
            }
        }

        private ICommand _backupCommand;
        public ICommand backupCommand
        {
            get
            {
                if (_backupCommand == null)
                {
                    _backupCommand = new Command(Backup);
                }
                return _backupCommand;
            }
            set { _backupCommand = value; }
        }

        private ICommand _restoreCommand;
        public ICommand restoreCommand
        {
            get
            {
                if (_restoreCommand == null)
                {
                    _restoreCommand = new Command(Restore);
                }
                return _restoreCommand;
            }
            set { _restoreCommand = value; }
        }

        private ICommand _navigateToPlayerStatCommand;
        public ICommand navigateToPlayerStatCommand
        {
            get
            {
                if (_navigateToPlayerStatCommand == null)
                {
                    _navigateToPlayerStatCommand = new Command<PlayerStat>(stat => ((App)Application.Current).rootFrame.Navigate(typeof(PlayerStatsView), stat));
                }
                return _navigateToPlayerStatCommand;
            }
            set { _navigateToPlayerStatCommand = value; }
        }

        public StatsViewModel()
        {
            _gameRepo = new GameRepository();
            _playerRepo = new PlayerRepository();
            _achievementRepo = new AchievementRepository();
            _oneDriveService = new OneDriveService();
            Setup();
        }

        public void Setup()
        {
            totalGames = _gameRepo.GetTotalGames();

            GetPlayersData();
            GetOverallData();
        }

        private void GetOverallData()
        {
            overallStats = new OverallStat();

            if (playerStats.Count > 0)
            {
                overallStats.mostPerfectGames = playerStats.OrderByDescending(p => p.perfectGames).First().perfectGames;
                overallStats.mostPerfectGamesPlayers = overallStats.mostPerfectGames == 0 ? string.Empty : ConstructListOfPlayers(playerStats.Where(p => p.perfectGames == overallStats.mostPerfectGames).ToList());

                overallStats.mostLivesLost = playerStats.OrderByDescending(p => p.livesLost).First().livesLost;
                overallStats.mostLivesLostPlayers = overallStats.mostLivesLost == 0 ? string.Empty : ConstructListOfPlayers(playerStats.Where(p => p.livesLost == overallStats.mostLivesLost).ToList());

                overallStats.bestScore = playerStats.OrderByDescending(p => p.bestScore).First().bestScore;
                overallStats.bestScorePlayers = ConstructListOfPlayers(playerStats.Where(p => p.bestScore == overallStats.bestScore).ToList());

                overallStats.worstScore = playerStats.OrderBy(p => p.worstScore).First().worstScore;
                overallStats.worstScorePlayers = ConstructListOfPlayers(playerStats.Where(p => p.worstScore == overallStats.worstScore).ToList());
            }
        }

        private void GetPlayersData()
        {
            playerStats = new ObservableCollection<PlayerStat>();

            try
            {
                foreach (Player player in _playerRepo.GetAllPlayers())
                {
                    playerStats.Add(new PlayerStat
                    {
                        name = player.name,
                        bestScore = _playerRepo.GetBestScoreById(player.id),
                        worstScore = _playerRepo.GetWorstScoreById(player.id),
                        numberOfGames = _playerRepo.GetNumberOfGamesById(player.id),
                        averageScore = _playerRepo.GetAverageScoreById(player.id),
                        perfectGames = _playerRepo.GetNumberOfPerfectGames(player.id),
                        livesLost = _playerRepo.GetNumberOfLivesLost(player.id),
                        achievements = new List<Achievement>(_achievementRepo.GetAchievementsById(player.id))
                    });
                }
            }
            catch (SQLiteException)
            {
            }
        }

        private async void Backup()
        {
            await _oneDriveService.BackUp();
        }

        private async void Restore()
        {
            await _oneDriveService.Restore();
        }


        private string ConstructListOfPlayers(List<PlayerStat> list)
        {
            string playersString = "(";

            for (int i = 0; i < list.Count(); i++)
            {
                if (i == 0)
                {
                    playersString += list[i].name;
                }
                else
                {
                    playersString += (", " + list[i].name);
                }
            }

            playersString += ")";

            return playersString;
        }
    }
}
