using SQLite.Net.Attributes;

namespace HanabiCompanion.Data.Models
{
    public class PlayerGame : Entity
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(id));
            }
        }

        private int _playerId;
        public int playerId
        {
            get { return _playerId; }
            set
            {
                _playerId = value;
                OnPropertyChanged(nameof(playerId));
            }
        }

        private int _gameId;
        public int gameId
        {
            get { return _gameId; }
            set
            {
                _gameId = value;
                OnPropertyChanged(nameof(gameId));
            }
        }

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

        private int _redScore;
        public int redScore
        {
            get { return _redScore; }
            set
            {
                _redScore = value;
                OnPropertyChanged(nameof(redScore));
            }
        }

        private int _blueScore;
        public int blueScore
        {
            get { return _blueScore; }
            set
            {
                _blueScore = value;
                OnPropertyChanged(nameof(blueScore));
            }
        }

        private int _yellowScore;
        public int yellowScore
        {
            get { return _yellowScore; }
            set
            {
                _yellowScore = value;
                OnPropertyChanged(nameof(yellowScore));
            }
        }

        private int _greenScore;
        public int greenScore
        {
            get { return _greenScore; }
            set
            {
                _greenScore = value;
                OnPropertyChanged(nameof(greenScore));
            }
        }

        private int _whiteScore;
        public int whiteScore
        {
            get { return _whiteScore; }
            set
            {
                _whiteScore = value;
                OnPropertyChanged(nameof(whiteScore));
            }
        }

        private int _multiScore;
        public int multiScore
        {
            get { return _multiScore; }
            set
            {
                _multiScore = value;
                OnPropertyChanged(nameof(multiScore));
            }
        }

        private bool _multicolour;
        public bool multicolour
        {
            get { return _multicolour; }
            set
            {
                _multicolour = value;
                OnPropertyChanged(nameof(multicolour));
            }
        }

        private bool _multicolourIsWild;
        public bool multiColourIsWild
        {
            get { return _multicolourIsWild; }
            set
            {
                _multicolourIsWild = value;
                OnPropertyChanged(nameof(multiColourIsWild));
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
    }
}
