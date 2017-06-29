using HanabiCompanion.Data.Models;
using HanabiCompanion.UI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace HanabiCompanion.UI.ViewModels
{
    public class StandingsViewModel : BaseViewModel
    {
        private const string _bombs = "Explosion! you made too many mistakes...";
        private const string _horrible = "Horrible, booed by the crowd...";
        private const string _mediocre = "Mediocre, just a hint of scattered applause...";
        private const string _honorable = "Honourable attempt, but quickly forgotten...";
        private const string _excellent = "Excellent, crowd pleasing.";
        private const string _amazing = "Amazing, they will be talking about it for weeks!";
        private const string _legendary = "Legendary, everyone left speechless, stars in their eyes!";

        private int _totalScore;
        public int totalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                OnPropertyChanged(nameof(totalScore));
            }
        }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> players
        {
            get { return _players; }
            set
            {
                _players = value;
                OnPropertyChanged(nameof(players));
            }
        }

        private string _rating;
        public string rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(rating));
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

        private ICommand _finishCommand;
        public ICommand finishCommand
        {
            get
            {
                if (_finishCommand == null)
                {
                    _finishCommand = new Command(FinishGame);
                }

                return _finishCommand;
            }
            set { _finishCommand = value; }
        }

        public StandingsViewModel(Game game)
        {
            totalScore = game.players[0].totalScore;
            livesLost = game.players[0].livesLost;
            players = new ObservableCollection<Player>(game.players);

            int modifier = game.multicolour ? 5 : 0;

            if (totalScore == 0)
            {
                rating = _bombs;
            }
            else if (totalScore - modifier <= 5)
            {
                rating = _horrible;
            }
            else if (totalScore - modifier <= 10)
            {
                rating = _mediocre;
            }
            else if (totalScore - modifier <= 15)
            {
                rating = _honorable;
            }
            else if (totalScore - modifier <= 20)
            {
                rating = _excellent;
            }
            else if (totalScore - modifier < 25)
            {
                rating = _amazing;
            }
            else
            {
                rating = _legendary;
            }
        }

        public void FinishGame()
        {
            ((App)Application.Current).rootFrame.Navigate(typeof(MainMenuView));
        }
    }
}
