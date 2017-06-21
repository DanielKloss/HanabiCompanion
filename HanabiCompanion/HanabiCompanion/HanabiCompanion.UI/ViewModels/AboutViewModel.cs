using Microsoft.Services.Store.Engagement;
using System;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.System;

namespace HanabiCompanion.UI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string _version;
        public string version
        {
            get { return _version; }
            set
            {
                _version = value;
                OnPropertyChanged(nameof(version));
            }
        }

        private ICommand _moreInfoCommand;
        public ICommand moreInfoCommand
        {
            get
            {
                if (_moreInfoCommand == null)
                {
                    _moreInfoCommand = new Command(MoreInfo);
                }
                return _moreInfoCommand;
            }
            set { _moreInfoCommand = value; }
        }

        private ICommand _rateAndReviewCommand;
        public ICommand rateAndReviewCommand
        {
            get
            {
                if (_rateAndReviewCommand == null)
                {
                    _rateAndReviewCommand = new Command(RateAndReview);
                }
                return _rateAndReviewCommand;
            }
            set { _rateAndReviewCommand = value; }
        }

        private ICommand _leaveFeedbackCommand;
        public ICommand leaveFeedbackCommand
        {
            get
            {
                if (_leaveFeedbackCommand == null)
                {
                    _leaveFeedbackCommand = new Command(LeaveFeedback);
                }
                return _leaveFeedbackCommand;
            }
            set { _leaveFeedbackCommand = value; }
        }

        public AboutViewModel()
        {
            version = String.Format("{0}.{1}", Package.Current.Id.Version.Major.ToString(), Package.Current.Id.Version.Minor.ToString());
        }

        private async void MoreInfo()
        {
            await Launcher.LaunchUriAsync(new Uri("http://www.boardgamegeek.com/boardgame/"));
        }

        private async void RateAndReview()
        {
            string packageFamilyName = Package.Current.Id.FamilyName;
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=" + packageFamilyName));
        }

        private async void LeaveFeedback()
        {
            var launcher = StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }
    }
}
