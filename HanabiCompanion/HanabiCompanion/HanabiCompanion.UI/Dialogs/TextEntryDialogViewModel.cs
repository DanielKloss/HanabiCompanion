using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanabiCompanion.UI.ViewModels;

namespace HanabiCompanion.UI.Dialogs
{
    public class TextEntryDialogViewModel : BaseViewModel
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

        private string _placeholder;
        public string placeholder
        {
            get { return _placeholder; }
            set
            {
                _placeholder = value;
                OnPropertyChanged(nameof(placeholder));
            }
        }

        private string _input;
        public string input
        {
            get { return _input; }
            set
            {
                _input = value;
                OnPropertyChanged(nameof(input));
            }
        }

        public TextEntryDialogViewModel(string Title, string Text, string Placeholder)
        {
            title = Title;
            text = Text;
            placeholder = Placeholder;
        }
    }
}
