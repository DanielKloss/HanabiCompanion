using System.Collections.Generic;
using HanabiCompanion.Data.Models;

namespace HanabiCompanion.UI.Models
{
    public class PlayerStat
    {
        public string name { get; set; }
        public int numberOfGames { get; set; }
        public int bestScore { get; set; }
        public int worstScore { get; set; }
        public int numberOfWins { get; set; }
        public int numberOfLoses { get; set; }
        public double winPercentage { get; set; }
        public int averageScore { get; set; }

        public List<Achievement> achievements { get; set; }
    }
}
