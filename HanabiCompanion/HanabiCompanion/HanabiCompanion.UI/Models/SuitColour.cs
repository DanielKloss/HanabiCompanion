namespace HanabiCompanion.UI.Models
{
    public class SuitColour : TrackPropertyChanged
    {
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

        private int _score;
        public int score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged(nameof(score));
            }
        }

        private string _image;
        public string image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(image));
            }
        }

        public SuitColour(string Name)
        {
            name = Name;
            score = 0;
            image = "Assets/Colours/" + Name;
        }
    }
}
