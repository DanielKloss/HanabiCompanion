using System.Collections.ObjectModel;
using HanabiCompanion.UI.Models;

namespace HanabiCompanion.UI.ViewModels
{
    public class RulesViewModel : BaseViewModel
    {
        private ObservableCollection<RulesCategory> _ruleCategories;
        public ObservableCollection<RulesCategory> ruleCategories
        {
            get { return _ruleCategories; }
            set
            {
                _ruleCategories = value;
                OnPropertyChanged(nameof(ruleCategories));
            }
        }

        public RulesViewModel()
        {
            ruleCategories = new ObservableCollection<RulesCategory>
            {
            new RulesCategory
            {
                header = "Setup",
                instructions = new ObservableCollection<string>
                    {
                        "Deal 5 cards to each player with 2 or 3 players",
                        "Deal 4 cards to each player if there are 4 or 5 players",
                        "Remeber not to look at your cards and hold them so everyone else can see them!",
                        "Set out the 8 clue clock tokens on the table and stack the 4 fuse tokens"
                    }
                },
                new RulesCategory
                {
                    header = "Gameplay",
                    instructions = new ObservableCollection<string>
                    {
                        "Players may do one of three things on their turn - ",
                        "1 - Give another player some information",
                        "2 - Discard a card",
                        "3 - Play a card",
                        "Players play cards in number order to build fireworks,",
                        "Players recieve time tokens back for discarding cards and each time they successfully play a 5 to the board",
                        "The game ends if all the fuse tokens are used up, all the 5's are successfully played to the board or after the last card is drawn (each player gets one more turn after the last card is drawn)"
                    }
                },
                new RulesCategory
                {
                    header = "Variants",
                    instructions = new ObservableCollection<string>
                    {
                        "MULTICOLOUR - Add the 6th multicoloured suit to the game and try to create 6 sets of fireworks. Maximum score becomes 30",
                        "HARD - Add the 6th multicoloured suit to the game but only use one of each number. Maximum score becomes 30",
                        "WILD - Add the 6th multicoloured suit to the game but use these cards as wild jokers. Maximum score becomes 20",
                        "EXTRA TIME - The game does not end when the last card is drawn, instead it ends when the last card is played",
                        "IKEBANA - A totally new way to use the Hanabi deck - more information can be found at www.Ikebana.com "
                    }
                }
            };
        }
    }
}
