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
        public int perfectGames { get; set; }
        public int livesLost { get; set; }
        public double averageScore { get; set; }
        public string highestColour { get; set; }

        public List<Achievement> achievements { get; set; }
    }
}
