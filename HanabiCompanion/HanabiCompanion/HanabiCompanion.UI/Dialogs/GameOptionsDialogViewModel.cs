using HanabiCompanion.UI.ViewModels;

namespace HanabiCompanion.UI.Dialogs
{
    public class GameOptionsDialogViewModel : BaseViewModel
    {
        private string _title;
        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(title));
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
        public bool multicolourIsWild
        {
            get { return _multicolourIsWild; }
            set
            {
                _multicolourIsWild = value;
                OnPropertyChanged(nameof(multicolourIsWild));
            }
        }

        public GameOptionsDialogViewModel(string Title, bool Multicolour, bool MulticolourIsWild)
        {
            title = Title;
            multicolour = Multicolour;
            multicolour = MulticolourIsWild;
        }
    }
}
