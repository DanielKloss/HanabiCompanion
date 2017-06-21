using System.Collections.ObjectModel;

namespace HanabiCompanion.UI.Models
{
    public class RulesCategory
    {
        public string header { get; set; }
        public ObservableCollection<string> instructions { get; set; }
    }
}
