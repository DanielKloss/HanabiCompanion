namespace HanabiCompanion.UI.Models
{
    public class OverallStat
    {
        public int mostWins { get; set; }
        public string mostWinsPlayers { get; set; }

        public int mostLoses { get; set; }
        public string mostLosesPlayers { get; set; }

        public int bestScore { get; set; }
        public string bestScorePlayers { get; set; }

        public int worstScore { get; set; }
        public string worstScorePlayers { get; set; }
    }
}
