using SQLite.Net.Attributes;
using System;
using System.Collections.ObjectModel;

namespace HanabiCompanion.Data.Models
{
    public class Game : Entity
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

        private DateTime _dateTime;
        public DateTime dateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(dateTime));
            }
        }

        private bool _multicolour;
        [Ignore]
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
        [Ignore]
        public bool multiColourIsWild
        {
            get { return _multicolourIsWild; }
            set
            {
                _multicolourIsWild = value;
                OnPropertyChanged(nameof(multiColourIsWild));
            }
        }

        private ObservableCollection<Player> _players;
        [Ignore]
        public ObservableCollection<Player> players
        {
            get { return _players; }
            set
            {
                _players = value;
                OnPropertyChanged(nameof(players));
            }
        }
    }
}
