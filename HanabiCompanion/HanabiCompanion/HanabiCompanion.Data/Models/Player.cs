using SQLite.Net.Attributes;
using System.Collections.ObjectModel;

namespace HanabiCompanion.Data.Models
{
    public class Player : Entity
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

        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
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

        private int _livesLost;
        [Ignore]
        public int livesLost
        {
            get { return _livesLost; }
            set
            {
                _livesLost = value;
                OnPropertyChanged(nameof(livesLost));
            }
        }

        private bool _selectedToPlay;
        [Ignore]
        public bool selectedToPlay
        {
            get { return _selectedToPlay; }
            set
            {
                _selectedToPlay = value;
                OnPropertyChanged(nameof(selectedToPlay));
            }
        }

        private ObservableCollection<Achievement> _achievements;
        [Ignore]
        public ObservableCollection<Achievement> achievements
        {
            get { return _achievements; }
            set
            {
                _achievements = value;
                OnPropertyChanged(nameof(achievements));
            }
        }
    }
}
