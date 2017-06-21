using Windows.Storage;

namespace HanabiCompanion.UI
{
    public class SettingsService
    {
        private const string _multicolour = "multicolour";
        private const string _multicolourIsWild = "multicolourIsWild";

        public string GetMulticolour()
        {
            return ApplicationData.Current.LocalSettings.Values[_multicolour]?.ToString() ?? "false";
        }

        public void SetMulticolour(string multicolour)
        {
            ApplicationData.Current.LocalSettings.Values[_multicolour] = multicolour;
        }

        public string GetMulticolourIsWild()
        {
            return ApplicationData.Current.LocalSettings.Values[_multicolourIsWild]?.ToString() ?? "false";
        }

        public void SetMulticolourIsWild(string multicolourIsWild)
        {
            ApplicationData.Current.LocalSettings.Values[_multicolourIsWild] = multicolourIsWild;
        }
    }
}
