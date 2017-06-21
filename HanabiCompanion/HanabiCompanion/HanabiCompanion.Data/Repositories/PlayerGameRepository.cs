using HanabiCompanion.Data.Models;

namespace HanabiCompanion.Data.Repositories
{
    public class PlayerGameRepository : Repository
    {
        public void AddPlayerGame(Player player, Game game)
        {
            connection.Insert(new PlayerGame
            {
                gameId = game.id,
                playerId = player.id,
                totalScore = player.totalScore,
                multicolour = game.multicolour,
                multiColourIsWild = game.multiColourIsWild,
                livesLost = player.livesLost
            });
        }
    }
}
