namespace HanabiCompanion.UI.Models
{
    public class OverallStat
    {
        public int mostPerfectGames { get; set; }
        public string mostPerfectGamesPlayers { get; set; }

        public int mostLivesLost { get; set; }
        public string mostLivesLostPlayers { get; set; }

        public int bestScore { get; set; }
        public string bestScorePlayers { get; set; }

        public int worstScore { get; set; }
        public string worstScorePlayers { get; set; }
    }
}
