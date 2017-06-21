using HanabiCompanion.UI.ViewModels;

namespace HanabiCompanion.UI.Dialogs
{
    public class ConfirmDialogViewModel : BaseViewModel
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

        private string _text;
        public string text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(text));
            }
        }

        public ConfirmDialogViewModel(string Title, string Text)
        {
            title = Title;
            text = Text;
        }
    }
}
