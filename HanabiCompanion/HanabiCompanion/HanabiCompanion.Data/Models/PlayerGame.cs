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
        [Ignore]
        public int totalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                OnPropertyChanged(nameof(totalScore));
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
